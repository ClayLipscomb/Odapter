//------------------------------------------------------------------------------
//    Odapter - a C# code generator for Oracle packages
//    Copyright(C) 2018 Clay Lipscomb
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.If not, see<http://www.gnu.org/licenses/>.
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Dapper;

namespace Odapter {
    internal class Loader {
        #region Member variables
        private string _dataSource, _schema, _filter, _login, _password;
        private Action<string> _displayMessageMethod;

        private List<IEntity> _objectTypes;
        private List<IEntity> _tables;
        private List<IEntity> _views;
        private List<IEntityAttribute> _objectTypeAttributes; 
        private List<IEntityAttribute> _columns;                // holds all columns for both tables and views
        #endregion

        #region Properties
        internal List<IPackage> Packages { get; private set; }
        internal List<IPackageRecord> PackageRecordTypes { get; private set; }
        private List<IArgument> ArgumentsPackaged { get; set; }
        internal List<IEntity> ObjectTypes { get { return _objectTypes; } }
        //private List<ObjectTypeAttribute> ObjectTypeAttributes { get; set; }
        internal List<IEntity> Tables { get { return _tables; } }
        internal List<IEntity> Views { get { return _views; } }
        //private List<Column> Columns { get; set; } // contains all columns for both tables and views
        #endregion

        #region Enums
        private enum SytemTableObjectType { PACKAGE, TRIGGER, TYPE, OBJECT };
        #endregion

        #region Constructors
        internal Loader(IParameterDatabase param, Action<string> messageMethod) {
            _dataSource = param.DatabaseInstance;
            _schema = param.Schema;
            _filter = String.IsNullOrEmpty(param.Filter) ? null : param.Filter;
            _login = param.UserLogin;
            _password = param.Password;
            _displayMessageMethod = messageMethod;
        }
        #endregion

        /// <summary>
        /// proxy to to display message in UI
        /// </summary>
        /// <param name="msg"></param>
        private void DisplayMessage(string msg) { _displayMessageMethod(msg); }

        #region Database
        private string GetConnectionString() {
            return "data source=" + _dataSource + ";user id=" + _login + ";password=" + _password;
        }

        private OracleConnection GetConnection() {
            OracleConnection connection = new OracleConnection(GetConnectionString());
            connection.Open();
            return connection;
        }
        #endregion

        #region Loading Methods
        public void Load() {
            Load<Package, Procedure, PackageRecord, Field, Argument, ObjectType, ObjectTypeAttribute, Table, View, Column>();
        }

        private void Load<T_Package, T_Procedure, T_PackageRecord, T_Field, T_Argument, T_ObjectType, T_ObjectTypeAttribute, T_Table, T_View, T_Column>()
            where T_Package : class, IPackage, new()
            where T_Procedure : class, IProcedure, new()
            where T_PackageRecord : class, IPackageRecord, new()
            where T_Field : class, IField, new()
            where T_Argument : class, IArgument, new()
            where T_ObjectType : class, IObjectType, new()
            where T_ObjectTypeAttribute : class, IObjectTypeAttribute, new()
            where T_Table : class, ITable, new()
            where T_View : class, IView, new()
            where T_Column : class, IColumn, new() {

            _displayMessageMethod(_dataSource + " " + _schema + (String.IsNullOrEmpty(_filter) ? String.Empty : " " + _filter + "*") + " generation:");

            using (OracleConnection connection = (OracleConnection)GetConnection()) {
                if (Parameter.Instance.IsGeneratePackage) LoadPackages<T_Package, T_Procedure, T_PackageRecord, T_Field, T_Argument>(connection);
                if (Parameter.Instance.IsGenerateObjectType) LoadNonPackagedEntities<T_ObjectType, T_ObjectTypeAttribute>(connection, ref _objectTypes, ref _objectTypeAttributes);
                if (Parameter.Instance.IsGenerateTable) LoadNonPackagedEntities<T_Table, T_Column>(connection, ref _tables, ref _columns);
                if (Parameter.Instance.IsGenerateView) LoadNonPackagedEntities<T_View, T_Column>(connection, ref _views, ref _columns);
            }
        }

        /// <summary>
        /// Given all arguments for package's procs, load all package specific types (e.g., a record) into memory
        /// </summary>
        /// <param name="arguments"></param>
        private void LoadPackageRecordTypes<T_PackageRecord, T_Field, T_Argument>(List<IArgument> arguments)
            where T_PackageRecord : class, IPackageRecord, new() 
            where T_Field : class, IField, new() 
            where T_Argument : class, IArgument, new() {

            foreach (IArgument arg in arguments) {
                switch (arg.DataType) {
                    case Orcl.RECORD:
                        if (arg.DataLevel == 0) continue; // ignore straight record type (without cursor)

                        // send the record argument and a list of all subsequent arguments
                        LoadRecordType<T_PackageRecord, T_Field, T_Argument>(arg, arguments.GetRange(arguments.IndexOf(arg) + 1, arguments.Count - arguments.IndexOf(arg) - 1), null);
                        continue;
                    case Orcl.ASSOCIATITVE_ARRAY:
                        // For an associative array of a record, we will need to create a class for the record.
                        if (arguments[arguments.IndexOf(arg) + 1].DataType == Orcl.RECORD) {
                            // First get type of the associated array by converting to C#. This will be a list of a class. We need the class name
                            //  in order to to load into our Oracle record types.
                            string assocArrayCSharpType = Translater.ConvertOracleArgTypeToCSharpType(arg, false);

                            // Send the arg following the assoc array arg since it holds the record, a list of all args following the record arg,
                            //  and the C# name of the record parsed out of the assoc array C# type.
                            LoadRecordType<T_PackageRecord, T_Field, T_Argument>(arguments[arguments.IndexOf(arg) + 1],
                                            arguments.GetRange(arguments.IndexOf(arg) + 2, arguments.Count - arguments.IndexOf(arg) - 2),
                                            CSharp.ExtractSubtypeFromGenericCollectionType(assocArrayCSharpType, false));
                        }
                        continue;
                    default:
                        continue;
                }
            }
            return;
        }

        /// <summary>
        /// Given a record type argument, extract and store the record type and its fields (recurse if necessary)
        /// </summary>
        /// <param name="recordArg">Argument with record</param>
        /// <param name="args">List of arguments following record argument</param>
        private void LoadRecordType<T_PackageRecord, T_Field, T_Argument>(IArgument recordArg, List<IArgument> args, string cSharpType)
            where T_PackageRecord : class, IPackageRecord, new() 
            where T_Field : class, IField, new() 
            where T_Argument : class, IArgument, new() {
            if (recordArg.DataType != Orcl.RECORD) throw new Exception("Argument sent to LoadRecordType() that is not a PL/SQL RECORD");

            // happens for type OBJECT, TABLE, VARRAY or UNDEFINED
            if ((recordArg.TypeName == null && recordArg.TypeSubname != null) || (recordArg.TypeName != null && recordArg.TypeSubname == null)) return;

            if (cSharpType == null) cSharpType = Translater.ConvertOracleArgTypeToCSharpType(recordArg, false);

            // if the record type has already been stored, do not proceeed
            if (PackageRecordTypes.Any(r => (r.SubName ?? "") == (recordArg.TypeSubname ?? "")
                                            && (r.Name ?? "") == (recordArg.TypeName ?? "")
                                            && (r.Owner  ?? "") == (recordArg.TypeOwner ?? "") 
                                            && (r.PackageName ?? "" ) == (recordArg.PackageName ?? "") ) ) {
                return;
            }

            // begin creation of record type
            IPackageRecord newRec = new T_PackageRecord();
            newRec.PackageName = recordArg.PackageName; // package containing argument
            newRec.Name = recordArg.TypeName;           // package containing *record type*
            newRec.SubName = recordArg.TypeSubname;     // name of record type if outside argument's package
            newRec.CSharpType = cSharpType;
            newRec.Owner = recordArg.TypeOwner;
            newRec.Attributes = new List<IEntityAttribute>();

            int recordDataLevel = recordArg.DataLevel;

            // loop the args to find this record's fields or a nested record type
            int columnPosition = 0;
            foreach (IArgument arg in args) {
                if (arg.DataLevel == recordDataLevel + 1) { // found a record field
                    // each of these fields are to be added to the record
                    IField f = new T_Field();
                    f.Name = arg.ArgumentName;

                    // convert to C# now - this should to be adjusted so it's done later
                    f.CSharpType = Translater.ConvertOracleArgTypeToCSharpType(arg, false);

                    // set the containing class from the package name
                    if (!String.IsNullOrEmpty(arg.TypeName) && !arg.TypeName.Equals(arg.PackageName)) {
                        if (!Parameter.Instance.IsDuplicatePackageRecordOriginatingOutsideFilterAndSchema
                                // owned by another schema or owned by package that was filtered out 
                            && (    !(arg.Owner ?? "").Equals(arg.TypeOwner) 
                                || !Packages.Any(p => p.PackageName.Equals(arg.TypeName)) )   ) {
                                f.ContainerClassName = Translater.ConvertOracleNameToCSharpName(arg.TypeName, false);
                        }

                        if (    !(arg.TypeName ?? "").Equals(arg.PackageName)
                                && Packages.Any(p => p.PackageName.Equals(arg.TypeName)) // package of origin of record being created
                                && PackageRecordTypes.Exists(r => r.PackageName.Equals(arg.TypeName) && r.SubName.Equals(arg.TypeSubname))) {
                            f.ContainerClassName = Translater.ConvertOracleNameToCSharpName(arg.TypeName, false);
                        }
                    }

                    f.DataType = arg.DataType;
                    f.DataLength = arg.DataLength;
                    f.DataPrecision = arg.DataPrecision;
                    f.DataScale = arg.DataScale;

                    f.MapPosition = columnPosition++;
                    newRec.Attributes.Add(f);
                } else if (arg.DataLevel == recordDataLevel + 2) { // found a lower level field, so skip
                    continue;
                } else if (arg.DataLevel <= recordDataLevel) { // we are past the last record field, we are done
                    break;
                }

                // if field is nested record, recurse into it
                if (arg.DataType == Orcl.RECORD) LoadRecordType<T_PackageRecord, T_Field, T_Argument>(arg, args.GetRange(args.IndexOf(arg) + 1, args.Count - args.IndexOf(arg) - 1), null);
            }

            PackageRecordTypes.Add(newRec);
            return;
        }

        /// <summary>
        /// Load all proc arguments for given schema and filter
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="packaged">If true load only packaged arguments, else load non-packaged.</param>
        private void LoadArguments<T_Argument>(OracleConnection connection, bool packaged = true)
            where T_Argument : class, IArgument, new() {

                string sql = " SELECT CAST(position as NUMBER(9,0)) position, overload, "
                            + " CAST(data_level as NUMBER(9,0)) data_level, argument_name, "
                            + " CAST(sequence as NUMBER(9,0)) sequence, data_type, in_out, CAST(data_length as NUMBER(9,0)) data_length, "
                            + " CAST(data_precision as NUMBER(9,0)) data_precision, CAST(char_length as NUMBER(9,0)) char_length, "
                            + " CAST(data_scale as NUMBER(9,0)) data_scale, "
                            + " type_owner, type_name, type_subname, pls_type, "
                            + " object_name, package_name, defaulted, owner, type_link "
                        + " FROM sys.all_arguments "
                        + " WHERE owner = :owner "
                        + " AND package_name IS " + (packaged ? "NOT" : "") + " NULL "
                        + " AND UPPER(package_name) LIKE :packageNamePrefix || '%' "
                        + " ORDER BY package_name, object_name, overload, sequence ";

            if ((packaged && ArgumentsPackaged == null) || (!packaged)) {
                DisplayMessage("Reading" + (packaged ? " packaged" : " non-packaged") + " arguments...");
                List<IArgument> args = connection.Query<T_Argument>(sql, 
                    new { owner = _schema, packageNamePrefix = (String.IsNullOrEmpty(_filter) ? "" : _filter.ToUpper()) }).ToList<IArgument>();
                if (Parameter.Instance.IsExcludeObjectsNamesWithSpecificChars) args = args.FindAll(a => a.PackageName.IndexOfAny(Parameter.Instance.ObjectNameCharsToExclude) == -1);
                DisplayMessage(args.Count.ToString() + " arguments read.");

                // set next argument
                if (args.Count > 0) {
                    IArgument nextArg = null;
                    for (int i = args.Count - 1; i >= 0; i--) {
                        if (i == args.Count - 1) {
                            nextArg = args[i];
                            continue;
                        }

                        if ((args[i].PackageName ?? "") == (nextArg.PackageName ?? "")
                            && args[i].ProcedureName == nextArg.ProcedureName) {
                            args[i].NextArgument = nextArg;
                        }
                        nextArg = args[i];
                    }
                }

                ArgumentsPackaged = args;
            }
        }

        /// <summary>
        /// load all packages with respective proc and arguments into memory
        /// </summary>
        /// <param name="connection"></param>
        private void LoadPackages<T_Package, T_Procedure, T_PackageRecord, T_Field, T_Argument>(OracleConnection connection)
            where T_Package : class, IPackage, new()
            where T_Procedure : class, IProcedure, new()
            where T_PackageRecord : class, IPackageRecord, new()
            where T_Field : class, IField, new()
            where T_Argument : class, IArgument, new() {

            // read package, procs and arguments from schema
            try {
                // get list of packages
                DisplayMessage("Reading packages...");
                String sql = "SELECT owner, object_name " 
                            + " FROM sys.all_objects "
                            + " WHERE owner = :owner " 
                            + " AND UPPER(object_type) = :objectType " 
                            + " AND UPPER(object_name) LIKE :objectNamePrefix || '%' "
                            + " ORDER BY object_name";
                Packages = connection.Query<T_Package>(sql,
                    new { owner = _schema, 
                        objectType = SytemTableObjectType.PACKAGE.ToString(), 
                        objectNamePrefix = (String.IsNullOrEmpty(_filter) ? "" : _filter.ToUpper()) }).ToList<IPackage>();

                if (Parameter.Instance.IsExcludeObjectsNamesWithSpecificChars) Packages = Packages.Where<IPackage>(p => p.PackageName.IndexOfAny(Parameter.Instance.ObjectNameCharsToExclude) == -1).ToList();
                DisplayMessage(Packages.Count.ToString() + " packages read.");

                // load all arguments for packaged procs/funcs
                LoadArguments<T_Argument>(connection);

                // load all package procs
                DisplayMessage("Reading packaged procs...");
                sql = "SELECT object_name, procedure_name, overload "
                    + " FROM sys.all_procedures "
                    + " WHERE owner = :owner "
                    + " AND UPPER(object_type) = :objectType "
                    + " AND UPPER(object_name) LIKE :objectNamePrefix || '%' "
                    + " AND procedure_name IS NOT NULL "
                    + " ORDER BY object_name, procedure_name, overload "; // !!!!
                List<IProcedure> allProcedures = connection.Query<T_Procedure>(sql,
                    new { owner = _schema, 
                        objectType = SytemTableObjectType.PACKAGE.ToString(),
                        objectNamePrefix = (String.IsNullOrEmpty(_filter) ? "" : _filter.ToUpper())}).ToList<IProcedure>();

                PackageRecordTypes = new List<IPackageRecord>();

                // process each package
                int packagedProcCount = 0;
                foreach (IPackage pack in Packages) {
                    // retrieve all procs in package
                    pack.Procedures = allProcedures.Where<IProcedure>(p => p.PackageName.Equals(pack.PackageName)).ToList();

                    foreach (IProcedure proc in pack.Procedures) {
                        packagedProcCount++;
                        proc.Arguments = ArgumentsPackaged.Where(a => a.PackageName == proc.PackageName 
                            && a.ProcedureName == proc.ProcedureName 
                            && a.Overload == proc.Overload
                            && !String.IsNullOrWhiteSpace(a.DataType)).ToList(); // exclude the "empty arg" case to handle proc with no params

                        // load record types derived from this proc's arguments
                        LoadPackageRecordTypes<T_PackageRecord, T_Field, T_Argument>(proc.Arguments);
                    }
                }
                DisplayMessage(packagedProcCount.ToString() + " packaged procs read.");

                this.PackageRecordTypes.Sort();
            } finally {
            }
        }

        /// <summary>
        /// Load all attributes for both tables, views or object types into memory
        /// </summary>
        /// <param name="connection"></param>
        private void LoadNonPackagedEntityAttributes<T_EntityAttribute>(OracleConnection connection, ref List<IEntityAttribute> attributes)
            where T_EntityAttribute : class, IEntityAttribute, new() {

                string sql = typeof(T_EntityAttribute).Equals(typeof(ObjectTypeAttribute))
                    ? " SELECT type_name, "
                            + " attr_name, "
                            + " CAST(attr_no as NUMBER(9,0)) attr_no, "
                            + " attr_type_name , "
                            + " attr_type_owner , "
                            + " attr_type_mod , "
                            + " CAST(length as NUMBER(9,0)) length, "
                            + " CAST(precision as NUMBER(9,0)) precision, "
                            + " CAST(scale as NUMBER(9,0)) scale "
                        + " FROM sys.all_type_attrs "
                        + " WHERE inherited = 'NO' "
                            + " AND UPPER(owner) = :owner "
                            + " AND UPPER(type_name) LIKE :objectNamePrefix || '%'"
                        + " ORDER BY type_name, attr_no "
                    : " SELECT table_name "
                            + ", column_name "
                            + ", CAST(column_id as NUMBER(9,0)) column_id "
                            + ", data_type "
                            + ", data_type_owner "
                            + ", data_type_mod "
                            + ", CAST(data_length as NUMBER(9,0)) data_length "
                            + ", CAST(data_precision as NUMBER(9,0)) data_precision "
                            + ", CAST(data_scale as NUMBER(9,0)) data_scale "
                            + ", nullable "
                            + ", CAST(char_length as NUMBER(9,0)) char_length "
                        + " FROM all_tab_columns "
                        + " WHERE UPPER(owner) = :owner "
                            + " AND UPPER(table_name) LIKE :objectNamePrefix || '%'"
                        + " ORDER BY table_name, column_id ";

            string attribType = typeof(T_EntityAttribute).Name.ToLower();
            DisplayMessage("Reading "
                + (typeof(T_EntityAttribute).Equals(typeof(ObjectTypeAttribute)) ? "" : "table or view ")
                + attribType + "s...");
            attributes = connection.Query<T_EntityAttribute>(sql,
                        new { owner = _schema, objectNamePrefix = (String.IsNullOrEmpty(_filter) ? "" : _filter.ToUpper()) }).ToList<IEntityAttribute>();
            DisplayMessage(attributes.Count.ToString() + " " 
                + (typeof(T_EntityAttribute).Equals(typeof(ObjectTypeAttribute)) ? "" : "table or view ")
                + attribType + "s read.");
        }

        /// <summary>
        /// Load all tables, views or object types into memory
        /// </summary>
        /// <param name="connection"></param>
        private void LoadNonPackagedEntities<T_Entity, T_EntityAttribute>(OracleConnection connection, ref List<IEntity> entities, ref List<IEntityAttribute> attributes)
            where T_EntityAttribute : class, IEntityAttribute, new()
            where T_Entity : class, IEntity, new() {

            string source = typeof(T_Entity).Name.ToLower();
            string sql = typeof(T_Entity).Equals(typeof(ObjectType))
                ?   " SELECT owner, type_name, supertype_name, instantiable "
                    + " FROM all_types "
                    + " WHERE UPPER(typecode) = 'OBJECT' "
                        + " AND UPPER(owner) = :owner "
                        + " AND UPPER(type_name) LIKE :objectNamePrefix || '%'"
                    + " ORDER BY type_name "
                :   " SELECT owner, " + source + "_name "
                    + " FROM sys.all_" + source + "s "
                    + " WHERE UPPER(owner) = :owner "
                        + " AND UPPER(" + source + "_name" + ") LIKE :objectNamePrefix || '%'"
                    + " ORDER BY " + source + "_name ";

            // load views or tables accordingly
            DisplayMessage("Reading " + source + "s...");
            entities = connection.Query<T_Entity>(sql,
                new { owner = _schema, objectNamePrefix = (String.IsNullOrEmpty(_filter) ? "" : _filter.ToUpper()) }).ToList<IEntity>();
            if (Parameter.Instance.IsExcludeObjectsNamesWithSpecificChars) entities = entities.FindAll(e => e.EntityName.IndexOfAny(Parameter.Instance.ObjectNameCharsToExclude) == -1);
            DisplayMessage(entities.Count.ToString() + " " + source + "s read.");

            // if we have not loaded all columns for tables and views, do so
            if (attributes == null || attributes.Count == 0) LoadNonPackagedEntityAttributes<T_EntityAttribute>(connection, ref attributes);

            if (attributes.Count > 0 && entities.Count > 0) {
                // copy each attribute to respective entity
                foreach (T_Entity entity in entities) {
                    entity.Attributes = new List<IEntityAttribute>();
                    foreach (IEntityAttribute attr in attributes.FindAll(a => a.EntityName == entity.EntityName)) {
                        entity.Attributes.Add((T_EntityAttribute)attr);
                    }
                }
            }
            return;
        }
        #endregion
    }
}