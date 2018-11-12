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
        private string 
            _dataSource = Parameter.Instance.DatabaseInstance,
            _schema = Parameter.Instance.Schema,
            _filter = String.IsNullOrEmpty(Parameter.Instance.Filter) ? null : Parameter.Instance.Filter,
            _login = Parameter.Instance.UserLogin,
            _password = Parameter.Instance.Password;

        private Action<string> _displayMessageMethod;
        private List<Package> packages = new List<Package>();
        private List<PackageRecord> packageRecordTypes = new List<PackageRecord>();
        private List<ObjectType> objectTypes = new List<ObjectType>();
        private List<Table> tables = new List<Table>();
        private List<View> views = new List<View>();
        private List<ObjectTypeAttribute> objectTypeAttributes = new List<ObjectTypeAttribute>();
        private List<Column> columns = new List<Column>();
        #endregion

        #region Properties
        internal List<Package> Packages { get { return packages; } }
        internal List<PackageRecord> PacakgeRecordTypes { get { return packageRecordTypes; } }
        internal List<Argument> ArgumentsPackaged { get; set; }
        internal List<ObjectType> ObjectTypes { get { return objectTypes; } }
        internal List<Table> Tables { get { return tables; } }
        internal List<View> Views { get { return views; } }
        internal String Schema { get { return _schema; } }
        #endregion

        #region Enums
        private enum SytemTableObjectType { PACKAGE, TRIGGER, TYPE, OBJECT };
        #endregion
        #region Constants
        //private Char[] SystemObjectChars = new Char[2] {'#', '$'};
        #endregion

        #region Constructors
        internal Loader(Action<string> messageMethod) {
            _displayMessageMethod = messageMethod;
        }
        #endregion

        /// <summary>
        /// proxy to to display message in UI
        /// </summary>
        /// <param name="msg"></param>
        private void DisplayMessage(String msg) { _displayMessageMethod(msg); }

        #region Database
        private string GetConnectionString() {
            return "data source=" + _dataSource + ";user id=" + _login + ";password=" + _password;
        }

        private OracleConnection GetConnection() {
            OracleConnection connection = new OracleConnection(GetConnectionString());
            connection.Open();
            return connection;
        }

        public OracleCommand GetCommand(string package, string function, OracleConnection connection) {
            string commandText = package + "." + function;
            OracleCommand command = new OracleCommand(commandText, connection);
            return command;
        }
        #endregion

        #region Loading Methods
        public void Load() {
            using (OracleConnection connection = (OracleConnection)GetConnection()) {
                if (Parameter.Instance.IsGeneratePackage) LoadPackages(connection);
                if (Parameter.Instance.IsGenerateObjectType) LoadTypes(connection);
                if (Parameter.Instance.IsGenerateTable) LoadTables(connection);
                if (Parameter.Instance.IsGenerateView) LoadViews(connection);
            }
        }

        /// <summary>
        /// Given all arguments for package's procs, load all package specific types (e.g., a record) into memory
        /// </summary>
        /// <param name="arguments"></param>
        private void LoadPackageRecordTypes(List<Argument> arguments) {
            foreach (Argument arg in arguments) {
                switch (arg.DataType) {
                    case Orcl.RECORD:
                        if (arg.DataLevel == 0) continue; // ignore straight record type (without cursor)

                        // send the record argument and a list of all subsequent arguments
                        LoadRecordType(arg, arguments.GetRange(arguments.IndexOf(arg) + 1, arguments.Count - arguments.IndexOf(arg) - 1), null);
                        continue;
                    case Orcl.ASSOCIATITVE_ARRAY:
                        // For an associative array of a record, we will need to create a class for the record.
                        if (arguments[arguments.IndexOf(arg) + 1].DataType == Orcl.RECORD) {
                            // First get type of the associated array by converting to C#. This will be a list of a class. We need the class name
                            //  in order to to load into our Oracle record types.
                            String assocArrayCSharpType = Translater.ConvertOracleArgTypeToCSharpType(arg, false);

                            // Send the arg following the assoc array arg since it holds the record, a list of all args following the record arg,
                            //  and the C# name of the record parsed out of the assoc array C# type.
                            LoadRecordType(arguments[arguments.IndexOf(arg) + 1],
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
        private void LoadRecordType(Argument recordArg, List<Argument> args, String cSharpType) {
            if (recordArg.DataType != Orcl.RECORD) throw new Exception("Argument sent to LoadRecordType() that is not a PL/SQL RECORD");

            // happens for type OBJECT, TABLE, VARRAY or UNDEFINED
            if ((recordArg.TypeName == null && recordArg.TypeSubname != null) || (recordArg.TypeName != null && recordArg.TypeSubname == null)) return;

            if (cSharpType == null) cSharpType = Translater.ConvertOracleArgTypeToCSharpType(recordArg, false);

            // if the record type has already been stored, do not proceeed
            if (PacakgeRecordTypes.Exists(r => (r.SubName ?? "") == (recordArg.TypeSubname ?? "")
                                            && (r.Name ?? "") == (recordArg.TypeName ?? "")
                                            && (r.Owner  ?? "") == (recordArg.TypeOwner ?? "") 
                                            && (r.PackageName ?? "" ) == (recordArg.PackageName ?? "") ) ) {
                return;
            }

            // begin creation of record type
            PackageRecord newRec = new PackageRecord();
            newRec.PackageName = recordArg.PackageName; // package containing argument
            newRec.Name = recordArg.TypeName;           // package containing *record type*
            newRec.SubName = recordArg.TypeSubname;     // name of record type if outside argument's package
            newRec.CSharpType = cSharpType;
            newRec.Owner = recordArg.TypeOwner;
            newRec.Attributes = new List<IEntityAttribute>();

            int recordDataLevel = recordArg.DataLevel;

            // loop the args to find this record's fields or a nested record type
            int columnPosition = 0;
            foreach (Argument arg in args) {
                if (arg.DataLevel == recordDataLevel + 1) { // found a record field
                    // each of these fields are to be added to the record
                    Field f = new Field();
                    f.Name = arg.ArgumentName;

                    // convert to C# now - this should to be adjusted so it's done later
                    f.CSharpType = Translater.ConvertOracleArgTypeToCSharpType(arg, false);

                    // set the containing class from the package name
                    if (!String.IsNullOrEmpty(arg.TypeName) && !arg.TypeName.Equals(arg.PackageName)) {
                        if (!Parameter.Instance.IsDuplicatePackageRecordOriginatingOutsideFilterAndSchema
                                // owned by another schema or owned by package that was filtered out 
                            && (    !(arg.Owner ?? "").Equals(arg.TypeOwner) 
                                ||  !packages.Exists(p => p.PackageName.Equals(arg.TypeName)) )   ) { 
                            f.ContainerClassName = Translater.ConvertOracleNameToCSharpName(arg.TypeName, false);
                        }

                        if (    !(arg.TypeName ?? "").Equals(arg.PackageName)
                                && packages.Exists(p => p.PackageName.Equals(arg.TypeName)) // package of origin of record being created
                                && packageRecordTypes.Exists(r => r.PackageName.Equals(arg.TypeName) && r.SubName.Equals(arg.TypeSubname))) {
                            f.ContainerClassName = Translater.ConvertOracleNameToCSharpName(arg.TypeName, false);
                        }
                    }

                    f.AttrType = arg.DataType;
                    f.Length = arg.DataLength;
                    f.Precision = arg.DataPrecision;
                    f.Scale = arg.DataScale;

                    f.MapPosition = columnPosition++;
                    newRec.Attributes.Add(f);
                } else if (arg.DataLevel == recordDataLevel + 2) { // found a lower level field, so skip
                    continue;
                } else if (arg.DataLevel <= recordDataLevel) { // we are past the last record field, we are done
                    break;
                }

                // if field is nested record, recurse into it
                if (arg.DataType == Orcl.RECORD) LoadRecordType(arg, args.GetRange(args.IndexOf(arg) + 1, args.Count - args.IndexOf(arg) - 1), null);
            }

//            newRec.Fields.Sort();
            PacakgeRecordTypes.Add(newRec);
            return;
        }

        /// <summary>
        /// Load all proc arguments for given schema and filter
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="packaged">If true load only packaged arguments, else load non-packaged.</param>
        private void LoadArguments(OracleConnection connection, bool packaged = true) {
            String sql = " SELECT CAST(position as NUMBER(9,0)) position, overload, "
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
                List<Argument> args = connection.Query<Argument>(sql, 
                    new { owner = _schema, packageNamePrefix = (String.IsNullOrEmpty(_filter) ? "" : _filter.ToUpper()) }).ToList();
                if (Parameter.Instance.IsExcludeObjectsNamesWithSpecificChars) args = args.FindAll(a => a.PackageName.IndexOfAny(Parameter.Instance.ObjectNameCharsToExclude) == -1);
                DisplayMessage(args.Count.ToString() + " arguments read.");

                // set next argument
                if (args.Count > 0) {
                    Argument nextArg = null;// args[args.Count - 1];
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
        internal void LoadPackages(OracleConnection connection) {
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
                packages = connection.Query<Package>(sql,
                    new { owner = _schema, 
                        objectType = SytemTableObjectType.PACKAGE.ToString(), 
                        objectNamePrefix = (String.IsNullOrEmpty(_filter) ? "" : _filter.ToUpper()) }).ToList();
                if (Parameter.Instance.IsExcludeObjectsNamesWithSpecificChars) packages = packages.FindAll(p => p.PackageName.IndexOfAny(Parameter.Instance.ObjectNameCharsToExclude) == -1);
                DisplayMessage(packages.Count.ToString() + " packages read.");

                // load all arguments for packaged procs/funcs
                LoadArguments(connection);

                // load all package procs
                DisplayMessage("Reading packaged procs...");
                sql = "SELECT object_name, procedure_name, overload "
                    + " FROM sys.all_procedures "
                    + " WHERE owner = :owner "
                    + " AND UPPER(object_type) = :objectType "
                    + " AND UPPER(object_name) LIKE :objectNamePrefix || '%' "
                    + " AND procedure_name IS NOT NULL "
                    + " ORDER BY object_name, procedure_name, overload "; // !!!!
                List<Procedure> allProcedures = connection.Query<Procedure>(sql,
                    new { owner = _schema, 
                        objectType = SytemTableObjectType.PACKAGE.ToString(),
                        objectNamePrefix = (String.IsNullOrEmpty(_filter) ? "" : _filter.ToUpper())}).ToList();

                // process each package
                int packagedProcCount = 0;
                foreach (Package pack in packages) {
                    // retrieve all procs in package
                    pack.Procedures = allProcedures.FindAll(p => p.PackageName.Equals(pack.PackageName));

                    foreach (Procedure proc in pack.Procedures) {
                        packagedProcCount++;
                        proc.Arguments = ArgumentsPackaged.FindAll(a => a.PackageName == proc.PackageName 
                            && a.ProcedureName == proc.ProcedureName 
                            && a.Overload == proc.Overload
                            && !String.IsNullOrWhiteSpace(a.DataType)); // exclude the "empty arg" case to handle proc with no params

                        // load record types derived from this proc's arguments
                        LoadPackageRecordTypes(proc.Arguments);
                    }
                }
                DisplayMessage(packagedProcCount.ToString() + " packaged procs read.");

                this.packageRecordTypes.Sort();
            } finally {
            }
        }

        /// <summary>
        /// Load all attributes for both tables, views or object types into memory
        /// </summary>
        /// <param name="connection"></param>
        private void LoadEntityAttributes<TEntityAttribute>(OracleConnection connection, ref List<TEntityAttribute> attributes)
            where TEntityAttribute : IEntityAttribute {

                string sql = typeof(TEntityAttribute).Equals(typeof(ObjectTypeAttribute))
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

            string attribType = typeof(TEntityAttribute).Name.ToLower();
            DisplayMessage("Reading "
                + (typeof(TEntityAttribute).Equals(typeof(ObjectTypeAttribute)) ? "" : "table or view ")
                + attribType + "s...");
            attributes = connection.Query<TEntityAttribute>(sql,
                        new { owner = _schema, objectNamePrefix = (String.IsNullOrEmpty(_filter) ? "" : _filter.ToUpper()) })
                        .ToList();
            DisplayMessage(attributes.Count.ToString() + " " 
                + (typeof(TEntityAttribute).Equals(typeof(ObjectTypeAttribute)) ? "" : "table or view ")
                + attribType + "s read.");
        }

        /// <summary>
        /// Load all tables, views or object types into memory
        /// </summary>
        /// <param name="connection"></param>
        private void LoadEntities<TEntity, TEntityAttribute>(OracleConnection connection, ref List<TEntity> entities, ref List<TEntityAttribute> attributes)
            where TEntityAttribute : IEntityAttribute
            where TEntity : IEntity {

            string source = typeof(TEntity).Name.ToLower();
            string sql =typeof(TEntity).Equals(typeof(ObjectType))
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
            entities = connection.Query<TEntity>(sql,
                new { owner = _schema, objectNamePrefix = (String.IsNullOrEmpty(_filter) ? "" : _filter.ToUpper()) }).ToList();
            if (Parameter.Instance.IsExcludeObjectsNamesWithSpecificChars) entities = entities.FindAll(e => e.EntityName.IndexOfAny(Parameter.Instance.ObjectNameCharsToExclude) == -1);
            DisplayMessage(entities.Count.ToString() + " " + source + "s read.");

            // if we have not loaded all columns for tables and views, do so
            if (attributes.Count == 0) LoadEntityAttributes<TEntityAttribute>(connection, ref attributes);

            if (attributes.Count > 0 && entities.Count > 0) {
                // copy each attribute to respective entity
                foreach (TEntity entity in entities) {
                    entity.Attributes = new List<IEntityAttribute>();
                    foreach (IEntityAttribute attr in attributes.FindAll(a => a.EntityName == entity.EntityName)) {
                        entity.Attributes.Add((TEntityAttribute)attr);
                    }
                }
            }
            return;
        }

        internal void LoadTypes(OracleConnection connection) {
            LoadEntities<ObjectType, ObjectTypeAttribute>(connection, ref objectTypes, ref objectTypeAttributes);
        }

        /// <summary>
        /// load all tables into memory
        /// </summary>
        /// <param name="connection"></param>
        internal void LoadTables(OracleConnection connection) {
            LoadEntities<Table, Column>(connection, ref tables, ref columns);
        }

        /// <summary>
        /// load all views into memory
        /// </summary>
        /// <param name="connection"></param>
        internal void LoadViews(OracleConnection connection) {
            LoadEntities<View, Column>(connection, ref views, ref columns);
        }
        #endregion
    }
}