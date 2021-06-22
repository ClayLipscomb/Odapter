//------------------------------------------------------------------------------
//    Odapter - a C# code generator for Oracle packages
//    Copyright(C) 2021 Clay Lipscomb
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
//    along with this program. If not, see<http://www.gnu.org/licenses/>.
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using Trns = Odapter.Translation.Api;

namespace Odapter {
    internal sealed class Loader {
        #region Parameter properties
        private string DatabaseInstance { get; set; }
        private string Schema { get; set; }
        private string Filter { get; set; }
        private string UserLogin { get; set; }
        private string Password { get; set; }
        private string OracleVersionBanner { get; set; }
        private bool IsExcludeObjectsNamesWithSpecificChars { get; set; }
        private char[] ObjectNameCharsToExclude { get; set; }

        private bool IsLoadPackage { get; set; }
        private bool IsLoadObjectType { get; set; }
        private bool IsLoadTable { get; set; }
        private bool IsLoadView { get; set; }
        private Action<string> DisplayMessageMethod { get; set; }
        #endregion

        #region Data set private fields
        private List<IEntity> _objectTypes;
        private List<IEntity> _tables;
        private List<IEntity> _views;
        private List<IEntityAttribute> _objectTypeAttributes; 
        private List<IEntityAttribute> _columns;                // holds all columns for both tables and views
        #endregion

        #region Data Set Properties
        internal List<IPackage> Packages { get; private set; }
        internal List<IPackageRecord> PackageRecordTypes { get; private set; }
        internal List<IArgument> ArgumentsPackaged { get; set; }
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
        internal Loader(IParameterLoad param, Action<string> messageMethod) {
            DatabaseInstance                        = param.DatabaseInstance;
            Schema                                  = param.Schema;
            Filter                                  = param.Filter;
            UserLogin                               = param.UserLogin;
            Password                                = param.Password;
            IsExcludeObjectsNamesWithSpecificChars  = param.IsExcludeObjectsNamesWithSpecificChars;
            ObjectNameCharsToExclude                = param.ObjectNameCharsToExclude;

            IsLoadPackage       = param.IsGeneratePackage;
            IsLoadObjectType    = param.IsGenerateObjectType;
            IsLoadTable         = param.IsGenerateTable;
            IsLoadView          = param.IsGenerateView;

            DisplayMessageMethod = messageMethod;
        }
        #endregion

        /// <summary>
        /// proxy to to display message in UI
        /// </summary>
        /// <param name="msg"></param>
        private void DisplayMessage(string msg) { DisplayMessageMethod(msg); }

        #region Database
        private string GetConnectionString() {
            return "data source=" + DatabaseInstance + ";user id=" + UserLogin + ";password=" + Password;
        }

        private OracleConnection GetConnection() {
            OracleConnection connection = new OracleConnection(GetConnectionString());
            connection.Open();
            return connection;
        }
        #endregion

        #region Loading Methods

        private void LoadOracleBannerVersion(OracleConnection connection) {
            try {
                var banner = connection.Query<string>(
                        @"SELECT banner FROM v$version WHERE banner LIKE :bannerPrefix || '%' ",
                        new { bannerPrefix = "Oracle" })
                    .FirstOrDefault();
                OracleVersionBanner = banner?.Replace(@"Edition ", String.Empty)?.Replace(@"Release ", String.Empty);
                DisplayMessage(OracleVersionBanner);
            } catch (Exception ex) {
                DisplayMessage(@"Warning: failed to detect Oracle version");
                DisplayMessage($"Warning: {ex.Message}");
            }
            return;
        }

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

            DisplayMessageMethod(DatabaseInstance + " " + Schema + (String.IsNullOrWhiteSpace(Filter) ? String.Empty : " " + Filter + "*") + " generation:");

            Packages            = new List<IPackage>();
            PackageRecordTypes  = new List<IPackageRecord>();
            ArgumentsPackaged   = new List<IArgument>();
            _objectTypes        = new List<IEntity>();
            _tables             = new List<IEntity>();
            _views              = new List<IEntity>();

            using (OracleConnection connection = (OracleConnection)GetConnection()) {
                LoadOracleBannerVersion(connection);
                if (IsLoadPackage) LoadPackages<T_Package, T_Procedure, T_PackageRecord, T_Field, T_Argument>(connection);
                if (IsLoadObjectType) LoadNonPackagedEntities<T_ObjectType, T_ObjectTypeAttribute>(connection, ref _objectTypes, ref _objectTypeAttributes);
                if (IsLoadTable) LoadNonPackagedEntities<T_Table, T_Column>(connection, ref _tables, ref _columns);
                if (IsLoadView) LoadNonPackagedEntities<T_View, T_Column>(connection, ref _views, ref _columns);
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
                        // send the record argument and a list of all subsequent arguments
                        LoadRecordType<T_PackageRecord, T_Field, T_Argument>(arg, arguments.GetRange(arguments.IndexOf(arg) + 1, arguments.Count - arguments.IndexOf(arg) - 1));
                        continue;
                    default:
                        continue;
                }
            }
            return;
        }
        
        /// <summary>
        /// Build a field from argument data
        /// </summary>
        /// <typeparam name="T_Field"></typeparam>
        /// <param name="arg"></param>
        /// <param name="recordArg"></param>
        /// <param name="mapPosition"></param>
        /// <returns></returns>
        private IField BuildField<T_Field>(IArgument arg, IArgument recordArg, int mapPosition) where T_Field : class, IField, new() { 

            IField field = new T_Field {
                Name = arg.ArgumentName,
                EntityName = recordArg.TypeSubname,
                DataType = arg.DataType,
                DataPrecision = arg.DataPrecision,
                DataScale = arg.DataScale,
                CharLength = arg.CharLength,
                OrclType = arg.OrclType,
                MapPosition = mapPosition,
                SubField = (arg?.NextArgument?.DataLevel == arg.DataLevel + 1) 
                    ? BuildField<T_Field>(arg.NextArgument, recordArg, mapPosition) 
                    : null
            };

            //if (arg?.NextArgument?.DataLevel == arg.DataLevel + 1) field.SubField = BuildField<T_Field>(arg.NextArgument, recordArg, mapPosition);

            // set the containing class from the package name
            if (!String.IsNullOrEmpty(arg.TypeName) && !arg.TypeName.Equals(arg.PackageName)) {
                if (!Parameter.Instance.IsDuplicatePackageRecordOriginatingOutsideFilterAndSchema
                    // owned by another schema or owned by package that was filtered out 
                    && (!(arg.Owner ?? "").Equals(arg.TypeOwner)
                        || !Packages.Any(p => p.PackageName.Equals(arg.TypeName)))) {
                    field.ContainerClassName = Trns.ClassNameOfOracleIdentifier(arg.TypeName).Code;
                }

                if (!(arg.TypeName ?? "").Equals(arg.PackageName)
                        && Packages.Any(p => p.PackageName.Equals(arg.TypeName)) // package of origin of record being created
                        && PackageRecordTypes.Exists(r => r.PackageName.Equals(arg.TypeName) && r.TypeSubName.Equals(arg.TypeSubname))) {
                    field.ContainerClassName = Trns.ClassNameOfOracleIdentifier(arg.TypeName).Code;
                }
            }

            return field;
        }

        /// <summary>
        /// Given a record type argument, extract and store the record type and its fields (recurse if necessary)
        /// </summary>
        /// <param name="recordArg">Argument with record</param>
        /// <param name="args">List of arguments following record argument</param>
        private void LoadRecordType<T_PackageRecord, T_Field, T_Argument>(IArgument recordArg, List<IArgument> args)
            where T_PackageRecord : class, IPackageRecord, new() 
            where T_Field : class, IField, new() 
            where T_Argument : class, IArgument, new() {
            if (recordArg.DataType != Orcl.RECORD) return;

            if (recordArg.DataLevel == 0) return;                                       // ignore straight record type (without cursor/array)
            if (recordArg.TypeName == null && recordArg.TypeSubname == null) return;    // ignore cursor/array of (table) row

            // if the record type has already been stored, do not proceeed
            if (PackageRecordTypes.Any(r => (r.TypeSubName ?? "") == (recordArg.TypeSubname ?? "")
                                            && (r.TypeName ?? "") == (recordArg.TypeName ?? "")
                                            && (r.Owner  ?? "") == (recordArg.TypeOwner ?? "") 
                                            && (r.PackageName ?? "" ) == (recordArg.PackageName ?? "") ) ) {
                return;
            }

            // begin creation of record type
            IPackageRecord newRec = new T_PackageRecord {
                PackageName = recordArg.PackageName,    // package containing argument
                TypeName = recordArg.TypeName,          // package containing *record type*
                TypeSubName = recordArg.TypeSubname,    // name of record type 
                Owner = recordArg.TypeOwner,
                RecordArgument = recordArg,
                Attributes = new List<IEntityAttribute>()
            };

            int recordDataLevel = recordArg.DataLevel;

            // loop the args to find this record's fields or a nested record type
            int columnPosition = 0;
            foreach (IArgument arg in args) {
                if (arg.DataLevel == recordDataLevel + 1) { // found a record field
                    newRec.Attributes.Add(BuildField<T_Field>(arg, recordArg, columnPosition++));
                } else if (arg.DataLevel == recordDataLevel + 2) { // found a lower level field, so skip
                    continue;
                } else if (arg.DataLevel <= recordDataLevel) { // we are past the last record field, we are done
                    break;
                }

                // if field is nested record, recurse into it
                if (arg.DataType == Orcl.RECORD) LoadRecordType<T_PackageRecord, T_Field, T_Argument>(arg, args.GetRange(args.IndexOf(arg) + 1, args.Count - args.IndexOf(arg) - 1));
            }

            PackageRecordTypes.Add(newRec);
            return;
        }

        /// <summary>
        /// Load all proc arguments for given schema and filter
        /// </summary>
        /// <param name="connection"></param>
        private void LoadArguments<T_Argument>(OracleConnection connection)
            where T_Argument : class, IArgument, new() {

            DisplayMessage("Reading package arguments...");
            bool isFiltering = !String.IsNullOrWhiteSpace(Filter);

            string sql = " SELECT CAST(a.position as NUMBER(9,0)) position, a.overload, "
                            + " CAST(a.data_level as NUMBER(9,0)) data_level, a.argument_name, "
                            + " CAST(a.sequence as NUMBER(9,0)) sequence, a.data_type, a.in_out, CAST(a.data_length as NUMBER(9,0)) data_length, "
                            + " CAST(a.data_precision as NUMBER(9,0)) data_precision, CAST(a.char_length as NUMBER(9,0)) char_length, "
                            + " CAST(a.data_scale as NUMBER(9,0)) data_scale, "
                            + " a.type_owner, a.type_name, a.type_subname, a.pls_type, "
                            + " a.object_name, a.package_name, a.defaulted, a.owner, a.type_link, "
                            + " o.owner owner_object "
                        + " FROM sys.all_arguments a, sys.all_objects o "
                        + " WHERE a.owner = :owner "

                        //  This join logic is necessary but it can cause the query to never return. We cannot require that an Oracle instance
                        //      be configured or tuned in order for the system views to perform. Instead, we need to enforce this condition 
                        //      in C# (see below **).
                        // + " AND a.owner = o.owner " !!

                        + " AND a.package_name = o.object_name "
                        + " AND UPPER(o.object_type) = :objectType "    // required to restrict to package spec only
                        +  (isFiltering ? " AND UPPER(a.package_name) LIKE :packageNamePrefix || '%' " : String.Empty)
                        + " ORDER BY a.package_name, a.object_name, a.overload, a.defaulted, a.sequence ";  // moves all defaulted (defaulted="Y") past required (defaulted="N")

            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("owner", Schema);
            dynamicParameters.Add("objectType", SytemTableObjectType.PACKAGE.ToString());
            if (isFiltering) dynamicParameters.Add("packageNamePrefix", Filter.ToUpper());

            List<IArgument> args = connection.Query<T_Argument>(sql, dynamicParameters)
                .Where(a => a.Owner == a.OwnerObject)   // ** prevents inclusion of identically named package argument from another schema
                .ToList<IArgument>();
            if (IsExcludeObjectsNamesWithSpecificChars) args = args.FindAll(a => a.PackageName.IndexOfAny(ObjectNameCharsToExclude) == -1);
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

            foreach (IArgument arg in args) OrclUtil.Normalize(arg);

            ArgumentsPackaged = args;
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
                    new { owner = Schema, 
                        objectType = SytemTableObjectType.PACKAGE.ToString(), 
                        objectNamePrefix = Filter.ToUpper() }).ToList<IPackage>();

                if (IsExcludeObjectsNamesWithSpecificChars) Packages = Packages.Where<IPackage>(p => p.PackageName.IndexOfAny(ObjectNameCharsToExclude) == -1).ToList();
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
                    new { owner = Schema, 
                        objectType = SytemTableObjectType.PACKAGE.ToString(),
                        objectNamePrefix = Filter.ToUpper()}).ToList<IProcedure>();

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
                    ? " SELECT a.type_name, "
                            + " a.attr_name, "
                            + " CAST(a.attr_no as NUMBER(9,0)) attr_no, "
                            //+ " a.attr_type_name , "
                            // attribute type can be NULL when type is actually XMLTYPE; need better way to handle this
                            + $" (CASE WHEN a.attr_type_name IS NULL THEN '{Orcl.XMLTYPE}' ELSE a.attr_type_name END) attr_type_name, "
                            + " a.attr_type_owner , "
                            + " a.attr_type_mod , "
                            + " CAST(a.length as NUMBER(9,0)) length, "
                            + " CAST(a.precision as NUMBER(9,0)) precision, "
                            + " CAST(a.scale as NUMBER(9,0)) scale, "
                            + " t.typecode "
                        + " FROM sys.all_type_attrs a, sys.all_types t "
                        + " WHERE a.inherited = 'NO' "
                            + " AND UPPER(a.owner) = :owner "
                            + " AND UPPER(a.type_name) LIKE :objectNamePrefix || '%'"
                            //+ " AND a.attr_type_name = t.type_name(+) "
                            // attribute type can be NULL when type is actually XMLTYPE; need better way to handle this
                            + $" AND (CASE WHEN a.attr_type_name IS NULL THEN '{Orcl.XMLTYPE}' ELSE a.attr_type_name END) = t.type_name(+) "
                            + " AND a.owner = t.owner(+) "
                        + " ORDER BY a.type_name, a.attr_no "
                    : " SELECT c.table_name "
                            + ", c.column_name "
                            + ", CAST(c.column_id as NUMBER(9,0)) column_id "
                            + ", c.data_type "
                            + ", c.data_type_owner "
                            + ", c.data_type_mod "
                            + ", CAST(c.data_length as NUMBER(9,0)) data_length "
                            + ", CAST(c.data_precision as NUMBER(9,0)) data_precision "
                            + ", CAST(c.data_scale as NUMBER(9,0)) data_scale "
                            + ", c.nullable "
                            + ", CAST(c.char_length as NUMBER(9,0)) char_length "
                            + ",  t.typecode "
                        + " FROM all_tab_columns c, all_types t "
                        + " WHERE UPPER(c.owner) = :owner "
                            + " AND UPPER(c.table_name) LIKE :objectNamePrefix || '%'"
                            + " AND c.data_type = t.type_name(+) "
                            + " AND c.owner = t.owner(+) "
                        + " ORDER BY c.table_name, c.column_id ";

            string attribType = typeof(T_EntityAttribute).Name.ToLower();
            DisplayMessage("Reading "
                + (typeof(T_EntityAttribute).Equals(typeof(ObjectTypeAttribute)) ? "" : "table or view ")
                + attribType + "s...");
            attributes = connection.Query<T_EntityAttribute>(sql,
                        new { owner = Schema, objectNamePrefix = Filter.ToUpper() }).ToList<IEntityAttribute>();

            foreach (IEntityAttribute attrib in attributes) OrclUtil.Normalize(attrib);

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
                ? " SELECT owner, type_name, supertype_name, instantiable "
                    + " FROM all_types "
                    + " WHERE UPPER(typecode) = '" + SytemTableObjectType.OBJECT.ToString() + "'"
                        + " AND UPPER(owner) = :owner "
                        + " AND UPPER(type_name) LIKE :objectNamePrefix || '%'"
                    + " ORDER BY type_name "
                : " SELECT owner, " + source + "_name "
                    + " FROM sys.all_" + source + "s "
                    + " WHERE UPPER(owner) = :owner "
                        + " AND UPPER(" + source + "_name" + ") LIKE :objectNamePrefix || '%'"
                    + " ORDER BY " + source + "_name ";

            // load views or tables accordingly
            DisplayMessage("Reading " + source + "s...");
            entities = connection.Query<T_Entity>(sql,
                new { owner = Schema, objectNamePrefix = Filter.ToUpper() }).ToList<IEntity>();
            if (IsExcludeObjectsNamesWithSpecificChars) entities = entities.FindAll(e => e.EntityName.IndexOfAny(ObjectNameCharsToExclude) == -1);
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