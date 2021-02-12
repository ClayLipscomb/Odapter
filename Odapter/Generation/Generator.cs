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
//    along with this program.If not, see<http://www.gnu.org/licenses/>.
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using CS = Odapter.CSharp;

namespace Odapter {
    public sealed class Generator {
        #region User Defined Options
        private readonly string _outputPath;
        private readonly string _schema;
        private readonly string _databaseInstance;
        private readonly string _login;
        private readonly string _password;
        private readonly string _baseNamespace = "Schema"; // default

        private string _objectTypeNamespace { get; set; }

        private bool IsCSharp30 { get { return Parameter.Instance.CSharpVersion == CSharpVersion.ThreeZero; } }
        private bool IsCSharp40 { get { return Parameter.Instance.CSharpVersion == CSharpVersion.FourZero; } }
        #endregion

        #region Member Variables
        private readonly List<string> GeneratedPacakgeRecordTypes = new List<string>();
        private readonly Action<string> _displayMessageMethod;
        #endregion

        #region Constants/Readonly
        // method parameter names - over 30 characters to avoid Oracle clash
        private const string _oracleConnectionParamName = "optionalPreexistingOpenConnection"; // over 30 characters to avoid Oracle clash
        private const string PARAM_NAME_MAP_BY_POSITION                     = "mapColumnToObjectPropertyByPosition";
        private const string PARAM_NAME_ALLOW_UNMAPPED_COLUMNS              = "allowUnmappedColumnsToBeExcluded"; 
        private const string PARAM_NAME_MAXIMUM_ROWS_CURSOR                 = "optionalMaxNumberRowsToReadFromAnyCursor"; 
        private const string PARAM_NAME_CONVERT_COLUMN_NAME_TO_TITLE_CASE   = "convertColumnNameToTitleCaseInCaption"; 

        // local variable names generated from a base name
        private readonly string LOCAL_VAR_NAME_RETURN           = GenerateLocalVariableName(@"ret");
        private readonly string LOCAL_VAR_NAME_READER           = GenerateLocalVariableName(@"rdr");
        private readonly string LOCAL_VAR_NAME_COMMAND          = GenerateLocalVariableName(@"cmd");
        private readonly string LOCAL_VAR_NAME_COMMAND_PARAMS   = GenerateLocalVariableName(@"cmd") + @".Parameters";
        private readonly string LOCAL_VAR_NAME_COMMAND_TRACE    = GenerateLocalVariableName(@"cmdTrace");
        private readonly string LOCAL_VAR_NAME_CONNECTION       = GenerateLocalVariableName(@"conn");
        private readonly string LOCAL_VAR_NAME_ROWS_AFFECTED    = GenerateLocalVariableName(@"rowsAffected");

        private const string FUNC_RETURN_PARAM_NAME = "!RETURN";
        private const string ORCL_UTIL_NAMESPACE = "Odapter";
        private const string ORCL_UTIL_CLASS = "Hydrator";
        public const string APPLICATION_NAME = "Odapter";

        private const string USING = "using";
        private readonly string USING_ORACLE_DATAACCESS_CLIENT = USING + " " + "Oracle." + (Parameter.Instance.IsCSharp40 ? "Managed" : "") + "DataAccess.Client";
        private readonly string USING_ORACLE_DATAACCESS_TYPES = USING + " " + "Oracle." + (Parameter.Instance.IsCSharp40 ? "Managed" : "") + "DataAccess.Types";
        #endregion

        #region Nested Classes
        private class GenericType {
            internal string PackageTypeName { get; set; }
            internal string TypeName { get; set; }
            internal bool Untyped { get; set; }
            internal GenericType(string packageTypeName, string typeName, bool untyped) {
                PackageTypeName = packageTypeName;
                TypeName = typeName;
                Untyped = untyped;
            }
        }
        #endregion

        #region Constructors
        public Generator(string schema, string outputPath, Action<string> messageMethod,
                        string instance, string login, string password, 
                        string baseNamespace,
                        string objectTypeNameSpace) {
            _displayMessageMethod = messageMethod;
            _outputPath = outputPath;
            _schema = schema;
            _databaseInstance = instance;
            _login = login;
            _password = password;
            _baseNamespace = baseNamespace;
            _objectTypeNamespace = objectTypeNameSpace; // must have internally in order to generate "using" for other entity types
        }
        #endregion

        #region Namespace Generation
        /// <summary>
        /// Generate the de facto namespace for the schema, which will include a filter component if provided.
        /// </summary>
        /// <param name="baseNamespace"></param>
        /// <param name="schema"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static string GenerateNamespaceSchema(string baseNamespace, string schema, string filter) {
            return String.IsNullOrEmpty(schema) 
                    ? "" 
                    : (String.IsNullOrEmpty(baseNamespace) 
                        ? ""
                        : baseNamespace + ".") 
                        + CaseConverter.ConvertUnderscoreDelimitedToPascalCase(schema)
                        + (String.IsNullOrEmpty(filter) 
                            ? ""
                            : "." + CaseConverter.ConvertUnderscoreDelimitedToPascalCase(filter)); 
        }

        //public string GenerateNamespaceSchema() { return Generator.GenerateNamespaceSchema(_baseNamespace, _schema, GetFilterValueIfUsedInNaming()); }

        public static string GenerateNamespacePackage(string baseNamespace, string schema, string filter) {
            return String.IsNullOrEmpty(schema) ? "" : Generator.GenerateNamespaceSchema(baseNamespace, schema, filter) + @".Package"; 
        }

        public static string GenerateNamespaceObjectType(string baseNamespace, string schema, string filter) {
            return String.IsNullOrEmpty(schema) ? "" : Generator.GenerateNamespaceSchema(baseNamespace, schema, filter) + @".Type.Object";
        }

        public static string GenerateNamespaceTable(string baseNamespace, string schema, string filter) {
            return String.IsNullOrEmpty(schema) ? "" : Generator.GenerateNamespaceSchema(baseNamespace, schema, filter) + @".Table";
        }

        public static string GenerateNamespaceView(string baseNamespace, string schema, string filter) {
            return String.IsNullOrEmpty(schema) ? "" : Generator.GenerateNamespaceSchema(baseNamespace, schema, filter) + @".View";
        }

        public static string GetFilterValueIfUsedInNaming() {
            return Parameter.Instance.IsIncludeFilterPrefixInNaming ? Parameter.Instance.Filter : String.Empty;
        }
        #endregion

        #region Base Class Name Generation
        public static string GenerateBaseAdapterClassName(string schema) {
            return String.IsNullOrEmpty(schema) ? "" : CaseConverter.ConvertUnderscoreDelimitedToPascalCase(schema) + "Adapter";
        }

        public static string GenerateBaseRecordClassName(string schema) {
            return String.IsNullOrEmpty(schema) ? "" : CaseConverter.ConvertUnderscoreDelimitedToPascalCase(schema) + "PackageRecord";
        }

        public static string GenerateBaseObjectTypeClassName(string schema) {
            return String.IsNullOrEmpty(schema) ? "" : CaseConverter.ConvertUnderscoreDelimitedToPascalCase(schema) + "ObjectType";
        }

        public static string GenerateBaseTableClassName(string schema) {
            return String.IsNullOrEmpty(schema) ? "" : CaseConverter.ConvertUnderscoreDelimitedToPascalCase(schema) + "Table";
        }

        public static string GenerateBaseViewClassName(string schema) {
            return String.IsNullOrEmpty(schema) ? "" : CaseConverter.ConvertUnderscoreDelimitedToPascalCase(schema) + "View";
        }

        //private string GetEntityNamespace<TEntity>() {
        //    if (typeof(TEntity).Equals(typeof(ObjectType))) {
        //        return ObjectTypeNamespace;
        //    } else if (typeof(TEntity).Equals(typeof(Table))) {
        //        return TableNamespace;
        //    } else if (typeof(TEntity).Equals(typeof(View))) {
        //        return ViewNamespace;
        //    }
        //    return "Undefined_namespace_for_type_" + typeof(TEntity).Name;
        //}
        #endregion

        #region Package Method Generation
        /// <summary>
        /// create C# return type for the method that wraps a procedure
        /// </summary>
        /// <returns></returns>
        private string GenerateMethodReturnType(IProcedure proc) {
            return (proc.IsFunction() ? proc.Arguments[0].Translater : TranslaterFactoryType.GetTranslaterNull()).GetCSharpType();
        }

        /// <summary>
        /// Return a list of the C# generic types of a proc's out cursor arguments
        /// </summary>
        /// <param name="args"></param>
        /// <returns>list of types</returns>
        private List<GenericType> GetMethodGenericTypes(IProcedure proc, IPackage pack) {
            List<GenericType> genericTypes = new List<GenericType>(); // created empty list

            string typeName, packageTypeName;
            foreach (IArgument arg in proc.Arguments) {
                if (arg.DataLevel != 0) continue; // all signature arguments are initially found at 0 data level
                if (arg.OrclType is OrclRefCursor && arg.InOut.Equals(Orcl.OUT)) { // only out cursor args use generics
                    typeName = CSharp.ExtractSubtypeFromGenericCollectionType(arg.Translater.GetCSharpType(false), false);
                    packageTypeName = arg.NextArgument != null && arg.NextArgument.OrclType is OrclRecord
                           && !Parameter.Instance.IsUsingSchemaFilter
                           && !arg.PackageName.Equals(arg.NextArgument.TypeName)       // record not defined in this package
                           && !pack.ShouldGenerateRecordFromArgument(arg.NextArgument) // record not *generated* in this package adapter
                        ? TranslaterName.ConvertToPascal(arg.NextArgument.TypeName)
                        : null;
                    if (!genericTypes.Exists(a => a.TypeName == typeName))
                        genericTypes.Add(new GenericType(packageTypeName, typeName, arg.IsUntypedCursor));
                }
            }
            return genericTypes;
        }

        /// <summary>
        /// Determine all optional Oracle proc params that can be implemented in C# as optional params. For C# 4.0 optional params,
        ///  an optional param must follow all required params. For C# 3.0, there are no optional params.
        /// </summary>
        /// <param name="args">Oracle argument list for function</param>
        /// <returns>A list of the Oracle param names that can be optional in C#</returns>
        private List<string> GetOptionalCSharpParameters(List<IArgument> args) {
            List<string> optionalParamNames = new List<string>();
            if (!IsCSharp30) {
                for (int i = args.Count - 1; i >= 0; i--) { // loop in reverse - C# optinal params must be declared after req params
                    if (args[i].Defaulted) optionalParamNames.Add(args[i].ArgumentName);
                    else break; // quit upon finding first required arg
                }
            }
            return optionalParamNames;
        }

        /// <summary>
        /// Create C# code of methods' arguments with types, comma delimited as in a method signature
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private string GenerateMethodArgumentsCommaDelimited(List<IArgument> args, bool methodHasGenerics, bool dynamicMapping,
                                                            bool commentOutOnWrap, bool excludeTypes) {

            // based on Oracle params, determine all the C# optional params that can be implemented
            List<String> optionalParamNamesInCSharp = GetOptionalCSharpParameters(args);

            // loop arguments and build list
            List<string> argList = new List<string>();
            int argNum = 1; // start our arg numbering at 1 for the sake of the modulus check below

            foreach (IArgument arg in args) {
                if (arg.DataLevel != 0) continue; // all signature arguments are initially found at 0 data level
                if (arg.IsReturnArgument) continue; // ignore return value, only doing true args at this point

                if (arg.DataLevel == 0 && !string.IsNullOrEmpty(arg.ArgumentName)) {
                    argList.Add(
                        (((argNum++ - 5) % 6 == 0) ? "\r\n" + Tab(2) + (commentOutOnWrap ? "//" : "") + Tab(2) : "") // wrap as argument count increases
                        + (arg.InOut.Equals(Orcl.INOUT) ? "ref " : (arg.InOut.Equals(Orcl.OUT) ? "out " : "")) // pass inout/out args as ref/out in C#, respectively
                        + (excludeTypes
                            ? ""
                            : arg.Translater.GetCSharpType(false) + " ")
                        + TranslaterName.Convert(arg)
                        + (optionalParamNamesInCSharp.Contains(arg.ArgumentName) ? " = null" : "") // an optional C# 4.0 param defaulted to null
                        );
                }
            }

            // if method is using generics (i.e., proc has an out cursor) then add optional arguments 
            if (methodHasGenerics) {

                if (dynamicMapping) {
                    // mapping arguments with defaults 
                    argList.Add(("\r\n" + Tab(2) + (commentOutOnWrap ? "//" : "") + Tab(2)) // wrap as argument count increases
                        + (excludeTypes ? "" : "bool ") + PARAM_NAME_MAP_BY_POSITION
                        + (excludeTypes
                            ? ""
                            : (IsCSharp30 ? "" : " = false"))
                            );
                    argNum++;
                    argList.Add("" // wrap as argument count increases
                        + (excludeTypes ? "" : "bool ") + PARAM_NAME_ALLOW_UNMAPPED_COLUMNS
                        + (excludeTypes
                            ? ""
                            : (IsCSharp30 ? "" : " = false"))
                            );
                    argNum++;
                } 
            }

            // Datatable column name conversion to title case arg
            if (dynamicMapping && TranslaterManager.UseDatatableForUntypedCursor) {
                argList.Add((((argNum++ - 5) % 6 == 0) ? "\r\n" + Tab(2) + (commentOutOnWrap ? "//" : "") + Tab(2) : "") // wrap as argument count increases
                    + CSharp.BOOLEAN + " " + PARAM_NAME_CONVERT_COLUMN_NAME_TO_TITLE_CASE + (IsCSharp30 ? "" : " = false"));
            }

            // row count limit argument for any method with cursor (List or Datatable)
            if (methodHasGenerics || (dynamicMapping && TranslaterManager.UseDatatableForUntypedCursor)) {
                argList.Add((((argNum++ - 5) % 6 == 0) ? "\r\n" + Tab(2) + (commentOutOnWrap ? "//" : "") + Tab(2) : "") // wrap as argument count increases
                    + CSharp.UINT32 + "? " + PARAM_NAME_MAXIMUM_ROWS_CURSOR + (IsCSharp30 ? "" : " = null"));
            }

            // add optional Oracle connection arg for all methods
            argList.Add((((argNum++ - 5) % 6 == 0) ? "\r\n" + Tab(2) + (commentOutOnWrap ? "//" : "") + Tab(2) : "") // wrap as argument count increases
                + "OracleConnection" + " " + _oracleConnectionParamName + (IsCSharp30 ? "" : " = null"));

            return String.Join(", ", argList.ToArray());
        } // GenerateMethodArgumentsCommaDelimited

        /// <summary>
        /// Generate constraint code for generic types
        /// </summary>
        /// <param name="genericTypes"></param>
        /// <returns></returns>
        private string GenerateMethodConstraintsCode(List<GenericType> genericTypes, bool dynamicMapping) {
            StringBuilder sb = new StringBuilder("");
            foreach (GenericType gt in genericTypes) {
                sb.AppendLine();
                sb.Append(Tab(4) + "where " + gt.TypeName + " : class"
                    + (dynamicMapping || gt.Untyped 
                        ? "" 
                        : ", " 
                            + (gt.PackageTypeName == null ? "" : gt.PackageTypeName + ".")
                            + CSharp.ToInterface(gt.TypeName.Substring(CSharp.GENERIC_TYPE_PREFIX.Length)))
                    + ", new()");
            }
            return sb.ToString();
        }

        private string GenerateRefCursorOutArgumentRetrieveCode(List<GenericType> genericTypesUsed, string cSharpArgType, string cSharpArgName, string oracleArgName,
                int tabIndentCount, bool dynamicMapping = false) {

            string returnListSubTypeFullyQualifiedPackageTypeName = null;
            string subType = CSharp.ExtractSubtypeFromGenericCollectionType(cSharpArgType, false);
            if (genericTypesUsed.Count > 0) {
                GenericType gt = genericTypesUsed.Find(g => g.TypeName == subType && g.PackageTypeName != null);
                if (gt != null) returnListSubTypeFullyQualifiedPackageTypeName = gt.PackageTypeName;
            }

            StringBuilder sb = new StringBuilder("");
            sb.AppendLine(Tab(tabIndentCount) + "if (" + "!((" + CSharp.ODP_NET_SAFE_TYPE_REF_CURSOR + ")" + LOCAL_VAR_NAME_COMMAND_PARAMS + "[\"" + oracleArgName + "\"].Value).IsNull" + ")");
            sb.AppendLine(Tab(tabIndentCount + 1) + "using (OracleDataReader " + LOCAL_VAR_NAME_READER + " = ((" + CSharp.ODP_NET_SAFE_TYPE_REF_CURSOR + ")" + LOCAL_VAR_NAME_COMMAND_PARAMS + "[\"" + oracleArgName + "\"].Value).GetDataReader()) {");
            sb.Append(Tab(tabIndentCount + 2) + cSharpArgName + " = ");
            if (dynamicMapping) {
                sb.AppendLine(ORCL_UTIL_CLASS + ".ReadResult"
                    + (cSharpArgType == CSharp.DATATABLE ? "" : "<" + subType + ">")
                    + "(" + LOCAL_VAR_NAME_READER
                            + (cSharpArgType == CSharp.DATATABLE
                                ? ", " + PARAM_NAME_CONVERT_COLUMN_NAME_TO_TITLE_CASE
                                : ", " + PARAM_NAME_MAP_BY_POSITION + ", " + PARAM_NAME_ALLOW_UNMAPPED_COLUMNS)
                                    + ", " + PARAM_NAME_MAXIMUM_ROWS_CURSOR // max rows to read
                    + ");");
            } else {
                sb.AppendLine((returnListSubTypeFullyQualifiedPackageTypeName == null ? "" : returnListSubTypeFullyQualifiedPackageTypeName + ".Instance.")
                    + CSharp.READ_RESULT + CSharp.ToInterface(subType.Substring(2))
                    + "<" + subType + ">"
                    + "(" + LOCAL_VAR_NAME_READER
                    + ", " + PARAM_NAME_MAXIMUM_ROWS_CURSOR // max rows to read
                    + ");");
            }
            sb.AppendLine(Tab(tabIndentCount + 1) + "}" + " // using OracleDataReader");
            return sb.ToString();
        }

        /// <summary>
        /// Generate code to retrieve an associative array value from an out argument or return
        /// </summary>
        /// <param name="cSharpArgType"></param>
        /// <param name="cSharpArgName"></param>
        /// <param name="oracleArgName"></param>
        /// <param name="tabIndentCount"></param>
        /// <returns></returns>
        private string GenerateAssocArrayOutArgumentRetrieveCode(string cSharpArgType, string cSharpArgName, IArgument oracleArg, int tabIndentCount) {
            string subType = CSharp.ExtractSubtypeFromGenericCollectionType(cSharpArgType, false);
            string subTypeNonNullable = CSharp.AsNonNullable(subType);

            StringBuilder sb = new StringBuilder("");
            sb.AppendLine(Tab(5) + cSharpArgName + " = new " + CS.GenericCollectionOf(CS.LIST, subType) + "();"); 
            string oracleArrayCode = "(" + LOCAL_VAR_NAME_COMMAND_PARAMS + "[\"" + (oracleArg.ArgumentName ?? FUNC_RETURN_PARAM_NAME) + "\"].Value as "
                + oracleArg.NextArgument.Translater.CSharpOdpNetType + "[])";
            sb.AppendLine(Tab(5) + "for (int _i = 0; _i < " + oracleArrayCode + ".Length; _i++)");
            sb.AppendLine(Tab(tabIndentCount + 1) + cSharpArgName + ".Add(" + oracleArrayCode + "[_i].IsNull");

            bool isOdpNetType = CSharp.IsOdpNetType(subType);
            if (isOdpNetType) {
                sb.AppendLine(Tab(tabIndentCount + 2) + "? " + subTypeNonNullable + ".Null ");
            } else {
                sb.AppendLine(Tab(tabIndentCount + 2) + "? (" + subType + ")null ");
            }

            if (isOdpNetType) {
                sb.AppendLine(Tab(tabIndentCount + 2) + ": (" + subType + ")"
                    + "(" + oracleArrayCode + "[_i].ToString()));");
            } else {
                sb.AppendLine(Tab(tabIndentCount + 2) 
                    + ": " + (subTypeNonNullable.Equals(CSharp.DATE_TIME_OFFSET) ? CSharp.DATE_TIME_OFFSET + ".Parse" : "Convert.To" + subTypeNonNullable)
                    + "((" + oracleArrayCode + "[_i].ToString())));");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Generate C# code which retrieves data for all out arguments of proc, including return
        /// </summary>
        /// <param name="args"></param>
        /// <param name="parametersVarName"></param>
        /// <returns></returns>
        private string GenerateOutArgumentRetrieveCode(List<IArgument> args, List<GenericType> genericTypesUsed, bool dynamicMapping = false) {
            StringBuilder sb = new StringBuilder("");
            bool prevArgIsAssocArray = false, isAssocArray = false;

            foreach (IArgument arg in args) {
                String cSharpArgType = arg.Translater.GetCSharpType(true);          // type defind as not nullable
                String cSharpArgTypeNullable = arg.Translater.GetCSharpType(false); 
                String cSharpArgName = (arg.IsReturnArgument ? LOCAL_VAR_NAME_RETURN : TranslaterName.Convert(arg));
                String oracleArgName = (arg.ArgumentName ?? FUNC_RETURN_PARAM_NAME);

                // ignore argument if not an out or return parameter
                if (arg.DataLevel != 0 || !arg.InOut.EndsWith(Orcl.OUT)) continue;

                isAssocArray = (arg.DataType == Orcl.ASSOCIATITVE_ARRAY);
                if (isAssocArray || prevArgIsAssocArray) sb.AppendLine(); // visually delimit assoc array code with blank line

                if (arg.DataType == Orcl.REF_CURSOR) {
                    sb.Append(GenerateRefCursorOutArgumentRetrieveCode(genericTypesUsed, cSharpArgType, cSharpArgName, oracleArgName, 5, dynamicMapping));
                } else if (isAssocArray) {
                    sb.Append(GenerateAssocArrayOutArgumentRetrieveCode(cSharpArgType, cSharpArgName, arg, 5));
                } else { // standard types (built-ins)
                    sb.AppendLine(Tab(5) + cSharpArgName + " = " + LOCAL_VAR_NAME_COMMAND_PARAMS
                        + "[\"" + oracleArgName + "\"].Status == OracleParameterStatus.NullFetched"); // check for null value

                    if (CSharp.IsOdpNetType(cSharpArgType)) {
                        sb.AppendLine(Tab(6) + "? " + cSharpArgType + ".Null"); // assign null value
                        sb.AppendLine(Tab(6) + ": (" + cSharpArgTypeNullable + ")" 
                            + LOCAL_VAR_NAME_COMMAND_PARAMS + "[\"" + oracleArgName + "\"].Value;"); // assign non-null value
                    } else {
                        bool isLobDataType = new List<String> { Orcl.BLOB, Orcl.CLOB, Orcl.NCLOB }.Contains(arg.DataType);
                        sb.AppendLine(Tab(6) + "? (" + (isLobDataType ? cSharpArgType : cSharpArgTypeNullable) + ")null"); // assign null value
                        if (isLobDataType) // assign non-null value 
                            sb.AppendLine(Tab(6) + ": ((" + arg.Translater.CSharpOdpNetType + ")" + LOCAL_VAR_NAME_COMMAND_PARAMS + "[\"" + oracleArgName + "\"].Value).Value;");
                        else
                            sb.AppendLine(Tab(6) + ": " 
                                + (new List<String> { CSharp.DATE_TIME_OFFSET }.Contains(cSharpArgType) ? cSharpArgType + ".Parse" : "Convert.To" + cSharpArgType)
                                + "(" + LOCAL_VAR_NAME_COMMAND_PARAMS + "[\"" + oracleArgName + "\"].Value.ToString());");
                    }
                }
                prevArgIsAssocArray = isAssocArray;
            }
            return sb.ToString();
        }

        /// <summary>
        /// Generate C# code that binds all arguments to call proc
        /// </summary>
        /// <param name="args"></param>
        /// <param name="parametersVarName"></param>
        /// <returns></returns>
        private string GenerateArgumentBindCode(List<IArgument> args) {
            StringBuilder sb = new StringBuilder("");
            bool prevArgIsAssocArray = false, isAssocArray = false;

            // determine all C# optional params based on Oracle params
            List<string> optionalParamNamesInCSharp = GetOptionalCSharpParameters(args);

            foreach (IArgument arg in args) {
                isAssocArray = (arg.DataType == Orcl.ASSOCIATITVE_ARRAY);
                string cSharpArgName = TranslaterName.Convert(arg);
                string cSharpArgType = arg.Translater.GetCSharpType(true);
                string clientOracleDbType = (isAssocArray ? arg.NextArgument : arg).Translater.CSharpOracleDbType; 

                if (isAssocArray) sb.AppendLine(); // visually delimit assoc array code with blank line

                if (arg.IsReturnArgument) {
                    // standard init used for all return types
                    sb.AppendLine(Tab(5) + LOCAL_VAR_NAME_COMMAND_PARAMS + ".Add(new OracleParameter("
                        + "\"" + FUNC_RETURN_PARAM_NAME + "\""
                        + ", " + clientOracleDbType
                        + (isAssocArray ? ", " + Parameter.Instance.MaxAssocArraySize.ToString() : "")
                        + (cSharpArgType == CSharp.STRING ? ", " + GetStringArgBindSize(arg.DataType).ToString() : "") // returning String requires size
                        + ", null"
                        + ", ParameterDirection.ReturnValue));");

                    // and for associative arrays
                    if (isAssocArray) {
                        sb.AppendLine(Tab(5) + LOCAL_VAR_NAME_COMMAND_PARAMS + "[\"" + FUNC_RETURN_PARAM_NAME + "\"].CollectionType = OracleCollectionType.PLSQLAssociativeArray;");
                        // for assoc array of variable length types, set the ArrayBindSize with the maximum length of the type
                        if (CSharp.ExtractSubtypeFromGenericCollectionType(cSharpArgType, true).Equals(CSharp.STRING)) {
                                sb.AppendLine(Tab(5) + LOCAL_VAR_NAME_COMMAND_PARAMS + "[\"" + FUNC_RETURN_PARAM_NAME + "\"].ArrayBindSize = new int[" + Parameter.Instance.MaxAssocArraySize.ToString() + "];");
                            sb.AppendLine(Tab(5) 
                                + "for (int _i = 0; _i < " + Parameter.Instance.MaxAssocArraySize.ToString() + "; _i++) { " + LOCAL_VAR_NAME_COMMAND_PARAMS + "[\"" + FUNC_RETURN_PARAM_NAME + "\"].ArrayBindSize[_i] = "
                                + arg.NextArgument.CharLength + "; }");
                        }
                    }
                } else if (arg.DataLevel == 0 && !String.IsNullOrEmpty(arg.ArgumentName)) {
                    // determine whether C# parameter can be defined as optional
                    bool isCSharpParamOptional = optionalParamNamesInCSharp.Contains(arg.ArgumentName);

                    // standard init used for all parameter types
                    sb.AppendLine(Tab(5)
                        + (isCSharpParamOptional ? "if (" + cSharpArgName + " != null) " + (isAssocArray ? "{\r\n" + Tab(6) : "") : "")  // do not bind optional arg if not set
                        + LOCAL_VAR_NAME_COMMAND_PARAMS + ".Add(new OracleParameter("
                        + "\"" + arg.ArgumentName + "\""
                        + ", " + clientOracleDbType
                        + (isAssocArray ? ", " + (arg.InOut.EndsWith(Orcl.OUT.ToString()) ? Parameter.Instance.MaxAssocArraySize.ToString() : "(" + cSharpArgName + " == null ? 0 : " + cSharpArgName + ".Count)") : "")
                        + (arg.InOut.EndsWith(Orcl.OUT.ToString()) && cSharpArgType == CSharp.STRING ? ", " + GetStringArgBindSize(arg.DataType).ToString() : "") // returning String requires size
                        + ", " + (arg.InOut.Equals(Orcl.OUT.ToString()) || isAssocArray ? "null" : cSharpArgName)
                        + ", ParameterDirection." + (arg.InOut.StartsWith(Orcl.IN.ToString()) ? "Input" : "") + (arg.InOut.EndsWith(Orcl.OUT.ToString()) ? "Output" : "") + ")"
                        + ");");

                    // and for associative arrays
                    if (isAssocArray) {
                        string cSharpArgSubTypeNullable = args[args.IndexOf(arg) + 1].Translater.GetCSharpType(); 
                        if (arg.InOut.StartsWith(Orcl.IN.ToString()))
                            sb.AppendLine(Tab(5) + (isCSharpParamOptional ? "\t" : "") + LOCAL_VAR_NAME_COMMAND_PARAMS + "[\"" + arg.ArgumentName + "\"].Value = " 
                                + "(" + cSharpArgName + " == null || " + cSharpArgName + ".Count == 0 ? new "
                                + cSharpArgSubTypeNullable + "[]{} : "
                                + cSharpArgName + ".ToArray()" + ");");
                        sb.AppendLine(Tab(5) + (isCSharpParamOptional ? "\t" : "") + LOCAL_VAR_NAME_COMMAND_PARAMS + "[\"" + arg.ArgumentName + "\"].CollectionType = OracleCollectionType.PLSQLAssociativeArray;");
                        // for assoc array of variable length types, set the ArrayBindSize with the maximum length of the type
                        if (CSharp.ExtractSubtypeFromGenericCollectionType(cSharpArgType, true).Equals(CSharp.STRING)) {
                                sb.AppendLine(Tab(5) + (isCSharpParamOptional ? "\t" : "") + LOCAL_VAR_NAME_COMMAND_PARAMS + "[\"" + arg.ArgumentName + "\"].ArrayBindSize = new int[" + Parameter.Instance.MaxAssocArraySize.ToString() + "];");
                            sb.AppendLine(Tab(5) + (isCSharpParamOptional ? "\t" : "")
                                + "for (int _i = 0; _i < " + Parameter.Instance.MaxAssocArraySize.ToString() + "; _i++) { " + LOCAL_VAR_NAME_COMMAND_PARAMS + "[\"" + arg.ArgumentName + "\"].ArrayBindSize[_i] = "
                                + arg.NextArgument.CharLength + "; }");
                        }
                        if (isCSharpParamOptional) sb.AppendLine(Tab(5) + "}");
                    }
                }

                prevArgIsAssocArray = isAssocArray;
            }
            return sb.ToString();
        }

        /// <summary>
        /// Generate complete C# method for a given stored proc
        /// </summary>
        /// <param name="proc"></param>
        /// <returns></returns>
        private string GenerateMethodCode(IProcedure proc, IPackage pack, bool forceDynamicMapping) {
            StringBuilder methodText = new StringBuilder("");
            string methodName = TranslaterName.Convert(proc, pack);
            string methodReturnType = GenerateMethodReturnType(proc);
            List<GenericType> genericTypesUsed = new List<GenericType>();

            // get generic types (for cursors when in given translation mode) used by the method
            if (!TranslaterManager.UseDatatableForUntypedCursor) genericTypesUsed = GetMethodGenericTypes(proc, pack);

            /////////////////////////////////////////////////////////////////////////
            // bypass creation of methods that use certain types of arguments/returns
            string ignoreReason;
            if (proc.IsIgnoredDueToOracleTypes(out ignoreReason)) {
                methodText.AppendLine();
                methodText.AppendLine(Tab(2) + "// **PROC IGNORED** - " + ignoreReason);
                methodText.Append(Tab(2) + "//" + " public " + methodReturnType + " " + methodName);
                if (genericTypesUsed.Count > 0) methodText.Append("<" + String.Join(", ", genericTypesUsed.Select(gt => gt.TypeName).ToList()) + ">");
                methodText.Append("(" + GenerateMethodArgumentsCommaDelimited(proc.Arguments, genericTypesUsed.Count > 0, forceDynamicMapping, true, false) + ")");
                return methodText.ToString();
            }

            // method header
            methodText.AppendLine();
            methodText.Append(Tab(2) + "public " + methodReturnType + " " + methodName);

            // if the method is using generics for cursors, add all generic lists to sig
            if (genericTypesUsed.Count > 0) methodText.Append("<" + String.Join(", ", genericTypesUsed.Select(gt => gt.TypeName).ToList())  + ">");

            // arguments
            methodText.Append("(" + GenerateMethodArgumentsCommaDelimited(proc.Arguments, genericTypesUsed.Count > 0, forceDynamicMapping, false, false) + ")");

            // generic constraint
            if (genericTypesUsed.Count > 0) methodText.Append(GenerateMethodConstraintsCode(genericTypesUsed, forceDynamicMapping));

            methodText.Append(" {");
            methodText.AppendLine();

            // create/default return variable and default OUT parameters
            if (proc.IsFunction() || proc.HasOutArgument()) {
                methodText.Append(Tab(3));
                foreach (IArgument arg in proc.Arguments) 
                    if (arg.IsReturnArgument || (arg.DataLevel == 0 && arg.InOut.Equals(Orcl.OUT))) {
                        string cSharpType = arg.Translater.GetCSharpType(!arg.IsReturnArgument);
                        string cSharpName = arg.IsReturnArgument ? LOCAL_VAR_NAME_RETURN : TranslaterName.Convert(arg);
                        methodText.Append((arg.IsReturnArgument ? cSharpType + " " : "") + cSharpName +
                             (CSharp.IsValidGenericCollectionType(cSharpType) 
                                ? " = new " + arg.Translater.GetCSharpType(!arg.IsReturnArgument, true) + "()" 
                                : " = null") + "; ");
                    }
                methodText.AppendLine();
            }

            // local connection variable
            methodText.AppendLine(Tab(3) + "OracleConnection " + LOCAL_VAR_NAME_CONNECTION + " = " + _oracleConnectionParamName + " ?? GetConnection();");

            // begin body
            methodText.AppendLine(Tab(3) + "try {");

            // start using of OracleCommand
            methodText.AppendLine(Tab(4) + "using (OracleCommand " + LOCAL_VAR_NAME_COMMAND
                + " = new OracleCommand(" + "\"" 
                + _schema.ToUpper() + "."
                + (String.IsNullOrEmpty(proc.PackageName) ? "" : proc.PackageName + ".") 
                + proc.ProcedureName + "\"" + ", " + LOCAL_VAR_NAME_CONNECTION + ")) {");
            methodText.AppendLine(Tab(5) + LOCAL_VAR_NAME_COMMAND + ".CommandType = CommandType.StoredProcedure;");

            // For versions above C# 3.0, bind by name since it is necessary to handle not binding/settting Oracle optional parameters; 
            // the corresponding C# optional params are defaulted to null. C# 3.0 has no optional params and thus we cannot replicate
            // Oracle optional parameters; so we will use the defauult bind by position for 3.0.
            if (!IsCSharp30) methodText.AppendLine(Tab(5) + LOCAL_VAR_NAME_COMMAND + ".BindByName = true;");

            methodText.Append(GenerateArgumentBindCode(proc.Arguments));

            // initialize trace time
            methodText.AppendLine();
            methodText.AppendLine(Tab(5) + "OracleCommandTrace " + LOCAL_VAR_NAME_COMMAND_TRACE + " = "
                + "IsTracing(" + LOCAL_VAR_NAME_COMMAND + ") ? new OracleCommandTrace(" + LOCAL_VAR_NAME_COMMAND + ") : null;");

            // execute proc call
            methodText.AppendLine(Tab(5) + "int " + LOCAL_VAR_NAME_ROWS_AFFECTED + " = " + LOCAL_VAR_NAME_COMMAND + ".ExecuteNonQuery();");

            // set returned values for OUT parameters and return
            methodText.Append(GenerateOutArgumentRetrieveCode(proc.Arguments, genericTypesUsed, forceDynamicMapping));

            // trace completion of command
            methodText.AppendLine(Tab(5) + "if (" + LOCAL_VAR_NAME_COMMAND_TRACE + " != null) TraceCompletion(" + LOCAL_VAR_NAME_COMMAND_TRACE 
                + (proc.ReturnOracleDataType == Orcl.REF_CURSOR 
                    ? ", " + LOCAL_VAR_NAME_RETURN + (methodReturnType == CSharp.DATATABLE ? ".Rows" : "") + ".Count" 
                    : "") + ");");

            // end using of OracleCommand
            methodText.AppendLine(Tab(4) + "}" + " // using OracleCommand" );

            /////////////////
            // finally clause
            methodText.AppendLine(Tab(3) + "} finally {");
            methodText.AppendLine(  Tab(4) + "if (" + _oracleConnectionParamName + " == null) {");
            methodText.AppendLine(      Tab(5) + LOCAL_VAR_NAME_CONNECTION + ".Close();");
            methodText.AppendLine(      Tab(5) + LOCAL_VAR_NAME_CONNECTION + ".Dispose();");
            methodText.AppendLine(  Tab(4) + "}");
            methodText.AppendLine(Tab(3) + "}");
            /////////////////

            // return a value for function
            if (proc.IsFunction()) methodText.AppendLine(Tab(3) + "return " + LOCAL_VAR_NAME_RETURN + ";");

            // close body
            methodText.Append(Tab(2) + "} // " + methodName);

            return methodText.ToString();
        }

        /// <summary>
        /// Generate all versions of a method required for a proc 
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="classText"></param>
        private void GenerateAllMethodVersions(IProcedure proc, IPackage pack, ref StringBuilder classText) {

            // if method has at least one cursor, main version of method will use generics 
            if (proc.HasArgumentOfOracleType(Orcl.REF_CURSOR)) {
                // mapping version
                if ((proc.HasUntypedCursor() || Parameter.Instance.IsGenerateDynamicMappingMethodForTypedCursor) && !proc.HasInArgumentOfOracleTypeRefCursor()) {
                    classText.AppendLine(GenerateMethodCode(proc, pack, true));
                }
                // no mapping version
                if (!proc.HasUntypedCursor()) classText.AppendLine(GenerateMethodCode(proc, pack, false));

                // create extra method (w/o generics) for DataTable version of weakly typed cursors in return/args
                if (proc.HasUntypedCursor() && !proc.HasInArgumentOfOracleTypeRefCursor()) {
                    TranslaterManager.UseDatatableForUntypedCursor = true;
                    classText.AppendLine(GenerateMethodCode(proc, pack, true));
                    TranslaterManager.UseDatatableForUntypedCursor = false;
                }
            } else {
                // just create basic non-generic method 
                classText.AppendLine(GenerateMethodCode(proc, pack, false));
            }
        }
        #endregion

        #region Base Class Generation
        private string GenerateBaseEntityClass(string baseClassName, string classNamespace, string ancestorClassName) {
            StringBuilder classText = new StringBuilder("");

            // method header
            classText.AppendLine(Tab() + CSharp.ATTRIBUTE_SERIALIZABLE);
            classText.AppendLine(Tab() + "public abstract class " + baseClassName + (String.IsNullOrEmpty(ancestorClassName) ? "" : " : " + ancestorClassName) + " {");
            classText.AppendLine();
            classText.AppendLine(Tab() + "}" + " // " + baseClassName);

            return classText.ToString();
        }
        #endregion

        #region Package Record Type Generation
        private string GenerateRecordTypeReadResultMethod(IPackageRecord rec) {
            string className = rec.Translater.CSharpName;

            StringBuilder classText = new StringBuilder("");
            string interfaceName = CSharp.ToInterface(className);
            string methodName = CSharp.READ_RESULT + interfaceName;
            string genericTypeParam = CSharp.GENERIC_TYPE_PREFIX + className;
            string paramNameOracleReader = "rdr"; // Oracle clash not possible
            string returnType = CSharp.GenericCollectionOf(Parameter.Instance.CSharpTypeUsedForOracleRefCursor, genericTypeParam);

            // signature
            classText.AppendLine(Tab(2) + "public " + returnType + " " + methodName + "<" + genericTypeParam + ">"
                + "(OracleDataReader " + paramNameOracleReader + "" 
                + ", " + CSharp.UINT32 + "? " + PARAM_NAME_MAXIMUM_ROWS_CURSOR + (Parameter.Instance.IsCSharp30 ? "" : " = " + CSharp.NULL) + ")");
            classText.AppendLine(Tab(4) + "where " + genericTypeParam + " : class, " + interfaceName + ", new()  " + " {");

            classText.AppendLine(Tab(3) + returnType + " " + LOCAL_VAR_NAME_RETURN + " = new " + CSharp.GenericCollectionOf(CSharp.LIST, genericTypeParam) + "();");

            classText.AppendLine(Tab(3) + "if (" + paramNameOracleReader + " != " + CSharp.NULL + " && " + paramNameOracleReader + ".HasRows) {");
            classText.AppendLine(Tab(4) + "while (" + paramNameOracleReader + ".Read()) {"); 
            classText.AppendLine(Tab(5) + genericTypeParam + " obj = new " + genericTypeParam + "();");
            foreach (IField f in rec.Attributes) { // loop through all fields
                classText.Append(Tab(5) + "if (!" + paramNameOracleReader + ".IsDBNull(" + f.MapPosition + ")) "
                    + "obj." + TranslaterName.Convert(f) + " = ");

                if (f.Translater.GetCSharpType(true).Equals(CSharp.DECIMAL)) { 
                    classText.Append("(Decimal?)OracleDecimal.SetPrecision(" + paramNameOracleReader + ".GetOracleDecimal(" + f.MapPosition.ToString() + "), 28)");
                } else if (CSharp.IsOdpNetType(f.Translater.GetCSharpType())) {
                    classText.Append("(" + f.Translater.GetCSharpType() + ")" + paramNameOracleReader + ".GetOracleValue(" + f.MapPosition.ToString() + ")"); // ODP.NET
                } else if ((new List<string> { Orcl.BLOB, Orcl.CLOB, Orcl.NCLOB }).Contains(f.DataType)) {
                    classText.Append(paramNameOracleReader + "." + CSharp.GET_ORACLE + CaseConverter.ConvertToCapitalized(f.DataType.Substring(f.DataType.Length - 4, 4)) + "(" + f.MapPosition.ToString() + ").Value");
                } else if ((new List<string> { CSharp.DATE_TIME_OFFSET }).Contains(f.Translater.GetCSharpType(true))) {
                    classText.Append(f.Translater.GetCSharpType(true) + ".Parse" + "(" + paramNameOracleReader + ".GetValue(" + f.MapPosition.ToString() + ").ToString())"); // primitive
                } else {
                    classText.Append("Convert.To" + f.Translater.GetCSharpType(true) + "(" + paramNameOracleReader + ".GetValue(" + f.MapPosition.ToString() + "))"); // primitive
                }
                classText.AppendLine(";");
            }
            classText.AppendLine(Tab(5) + LOCAL_VAR_NAME_RETURN + ".Add(obj);");
            classText.AppendLine(Tab(5) + "if (" + PARAM_NAME_MAXIMUM_ROWS_CURSOR + " != " + CSharp.NULL + " && " + LOCAL_VAR_NAME_RETURN + ".Count >= " + PARAM_NAME_MAXIMUM_ROWS_CURSOR + ") break;");
            classText.AppendLine(Tab(4) + "}");
            classText.AppendLine(Tab(3) + "}");

            classText.AppendLine(Tab(3) + "return " + LOCAL_VAR_NAME_RETURN + ";");
            classText.AppendLine(Tab(2) + "} // " + methodName);
            return classText.ToString();
        }

        private void WriteBaseEntityClasses(string baseClassName) {
            string fileName = _outputPath + @"\" + CaseConverter.ConvertUnderscoreDelimitedToPascalCase(_schema)
                + CaseConverter.ConvertUnderscoreDelimitedToPascalCase(GetFilterValueIfUsedInNaming()) + @"BaseEntity.cs";

            try {
                StreamWriter outFile = new StreamWriter(fileName);

                StringBuilder headerText = new StringBuilder("");
                headerText = new StringBuilder("");
                headerText.AppendLine(Comment.Instance.CommentAutoGeneratedForBaseDto);
                headerText.AppendLine("using System;");
                headerText.AppendLine(USING_ORACLE_DATAACCESS_CLIENT + ";");
                headerText.AppendLine();

                outFile.Write(headerText);

                // namespace should be at schema level to avoid class name clashes
                outFile.WriteLine("namespace " + Parameter.Instance.NamespaceSchema + " {");

                // determine the class name of the base entity
                string baseEntityClassName = CaseConverter.ConvertUnderscoreDelimitedToPascalCase(_schema) + "Entity";

                // create all base entity classes
                outFile.WriteLine(GenerateBaseEntityClass(baseEntityClassName,
                    Parameter.Instance.NamespaceSchema, null));
                outFile.WriteLine(GenerateBaseEntityClass(CaseConverter.ConvertUnderscoreDelimitedToPascalCase(_schema) + @"PackageRecord",
                    Parameter.Instance.NamespaceSchema, baseEntityClassName));
                outFile.WriteLine(GenerateBaseEntityClass(CaseConverter.ConvertUnderscoreDelimitedToPascalCase(_schema) + @"Table",
                    Parameter.Instance.NamespaceSchema,  baseEntityClassName));
                outFile.WriteLine(GenerateBaseEntityClass(CaseConverter.ConvertUnderscoreDelimitedToPascalCase(_schema) + @"View",
                    Parameter.Instance.NamespaceSchema, baseEntityClassName));
                outFile.WriteLine(GenerateBaseEntityClass(CaseConverter.ConvertUnderscoreDelimitedToPascalCase(_schema) + @"ObjectType",
                    Parameter.Instance.NamespaceSchema, baseEntityClassName));

                // close namespace 
                outFile.Write("" + "} // " + Parameter.Instance.NamespaceSchema);

                outFile.Close();
            } catch (UnauthorizedAccessException) {
                DisplayMessage(Message.BASE_PERMISSION_ERROR_MSG + Path.GetFileName(fileName));
            } catch (Exception e) {
                DisplayMessage("Error writing " + Path.GetFileName(fileName) + " - " + e.Message);
            }
        }
        #endregion

        #region Package Class Generation
        private string GenerateBasePackageClass(string className) {
            StringBuilder classText = new StringBuilder("");

            // method header
            classText.AppendLine("\t" + "public abstract class " + className + " {");

            // default GetConnectionString()
            classText.AppendLine(Tab(2) + "protected string GetConnectionString() { return \"data source=" 
                + _databaseInstance + ";user id=" + _login + ";password=" + _password + ";enlist=false\"; }");

            // default GetConnection()
            classText.AppendLine();
            classText.AppendLine(Tab(2) + "protected OracleConnection GetConnection() {");
            classText.AppendLine("\t\t\t" + "OracleConnection connection = new OracleConnection(GetConnectionString());");
            classText.AppendLine("\t\t\t" + "connection.Open();");
            classText.AppendLine("\t\t\t" + "return connection;");
            classText.AppendLine(Tab(2) + "}");

            // default IsTracing()
            classText.AppendLine();
            classText.AppendLine(Tab(2) + "/// <summary>");
            classText.AppendLine(Tab(2) + "/// Determine if completion of OracleCommand execution should be traced (hook)");
            classText.AppendLine(Tab(2) + "/// </summary>");
            classText.AppendLine(Tab(2) + "/// <param name=\"cmd\">An OracleCommand prepared for executing</param>");
            classText.AppendLine(Tab(2) + "/// <returns>true if command should be traced</returns>");
            classText.AppendLine(Tab(2) + "protected bool IsTracing(OracleCommand cmd) {");
            classText.AppendLine("\t\t\t" + "return false;");
            classText.AppendLine(Tab(2) + "}");

            // default Trace()
            classText.AppendLine();
            classText.AppendLine(Tab(2) + "/// <summary>");
            classText.AppendLine(Tab(2) + "/// Perform trace functionality for a completed OracleCommand (hook)");
            classText.AppendLine(Tab(2) + "/// </summary>");
            classText.AppendLine(Tab(2) + "/// <param name=\"cmdTrace\">An OracleCommandTrace just executed</param>");
            classText.AppendLine(Tab(2) + "/// <param name=\"returnRowCount\">Row count returned in cursor</param>");
            classText.AppendLine(Tab(2) + "protected void TraceCompletion(Odapter.OracleCommandTrace cmdTrace, int? returnRowCount) {");
            classText.AppendLine("\t\t\t" + "// stop the timer first");
            classText.AppendLine("\t\t\t" + "cmdTrace.Stopwatch.Stop();");
            classText.AppendLine("\t\t\t" + "// trace logic goes here");
            classText.AppendLine("\t\t\t" + "return;");
            classText.AppendLine(Tab(2) + "}");

            classText.AppendLine();
            classText.AppendLine(Tab(2) + "/// <summary>");
            classText.AppendLine(Tab(2) + "/// Perform trace functionality for a completed OracleCommand (hook)");
            classText.AppendLine(Tab(2) + "/// </summary>");
            classText.AppendLine(Tab(2) + "/// <param name=\"cmdTrace\">An OracleCommandTrace just executed</param>");
            classText.AppendLine(Tab(2) + "protected void TraceCompletion(Odapter.OracleCommandTrace cmdTrace) {");
            classText.AppendLine("\t\t\t" + "TraceCompletion(cmdTrace, null);");
            classText.AppendLine("\t\t\t" + "return;");
            classText.AppendLine(Tab(2) + "}");

            classText.AppendLine(Tab() + "}" + " // " + className);

            return classText.ToString();
        }

        private void WriteBasePackageClass(string baseClassName) {
            string fileName = _outputPath + @"\" + CaseConverter.ConvertUnderscoreDelimitedToPascalCase(_schema) 
                + CaseConverter.ConvertUnderscoreDelimitedToPascalCase(GetFilterValueIfUsedInNaming()) + "BaseAdapter.cs";

            try {
                StreamWriter outFile = new StreamWriter(fileName);

                StringBuilder headerText = new StringBuilder("");
                headerText = new StringBuilder("");
                headerText.AppendLine(Comment.Instance.CommentAutoGeneratedForBaseAdapter);
                headerText.AppendLine("using System;");
                headerText.AppendLine(USING_ORACLE_DATAACCESS_CLIENT + ";");
                headerText.AppendLine();
                outFile.Write(headerText);

                // namespace should be at schema level
                outFile.WriteLine("namespace " + Parameter.Instance.NamespaceSchema + " {");

                // create base package manager class
                outFile.WriteLine(GenerateBasePackageClass(baseClassName));

                // close namespace for 
                outFile.Write("" + "} // " + Parameter.Instance.NamespaceSchema);

                outFile.Close();
            } catch (UnauthorizedAccessException) {
                DisplayMessage(Message.BASE_PERMISSION_ERROR_MSG + Path.GetFileName(fileName));
            } catch (Exception e) {
                DisplayMessage("Error writing " + Path.GetFileName(fileName) + " - " + e.Message);
            }
        }

        private void WritePackageClasses(List<IPackage> packages, IList<IPackageRecord> records, 
            string packageNamespace, string ancestorAdapterClassName, bool partialPackage, string ancestorRecordTypeClassName) {

            if (packages.Count == 0) return;

            string fileName = _outputPath + @"\" + CaseConverter.ConvertUnderscoreDelimitedToPascalCase(_schema)
                + CaseConverter.ConvertUnderscoreDelimitedToPascalCase(GetFilterValueIfUsedInNaming()) + "Package.cs";
            DisplayMessage("Coding packages (" + fileName.Substring(fileName.LastIndexOf('\\') + 1) + ")...");

            try {
                StreamWriter outFilePackage = new StreamWriter(fileName);

                Comment.Instance.WriteAutoGeneratedComment(outFilePackage);

                StringBuilder headerText = new StringBuilder("");

                // create using statements

                // package file
                headerText = new StringBuilder("");
                headerText.AppendLine("using System;");
                headerText.AppendLine("using System.Collections.Generic;");
                headerText.AppendLine("using System.Data;");
                headerText.AppendLine("using System.Data.Common;");
                headerText.AppendLine(USING_ORACLE_DATAACCESS_CLIENT + ";");
                headerText.AppendLine(USING_ORACLE_DATAACCESS_TYPES + ";");
                headerText.AppendLine("using System.Collections;");
                headerText.AppendLine("using System.Diagnostics;");
                headerText.AppendLine("using System.Runtime.Serialization;");
                headerText.AppendLine("using System.Xml;");
                headerText.AppendLine("using System.Xml.Serialization;");
                headerText.AppendLine("using System.Linq;");
                headerText.AppendLine("using " + ORCL_UTIL_NAMESPACE + ";");
                outFilePackage.Write(headerText);

                outFilePackage.WriteLine();

                // write namespace 
                outFilePackage.WriteLine("namespace " + packageNamespace + " {");

                foreach (IPackage pack in packages) {
                    string className = TranslaterName.Convert(pack);
                    StringBuilder classText = new StringBuilder("");
    
                    // class definition
                    classText.AppendLine();
                    classText.AppendLine(Tab() + "public sealed " + (partialPackage ? "partial " : "") + "class " + className + " : " + Parameter.Instance.NamespaceSchema + "." + ancestorAdapterClassName + " {");

                    // created as Singleton
                    classText.AppendLine(Tab(2) + "private " + className + "() { }");
                    classText.AppendLine(Tab(2) + "private static readonly " + className + " _instance = new " + className + "();");
                    classText.AppendLine(Tab(2) + "public static " + className + " Instance { get { return _instance; } }");

                    // for each possible record type in this package
                    foreach (IPackageRecord rec in records
                        .Where(r => (r.PackageName ?? "").Equals(pack.PackageName) || (r.TypeName ?? "").Equals(pack.PackageName) ) // either referenced by package or owned by package
                        .GroupBy(r => new { r.PackageName, r.TypeName, r.EntityName})
                        .Select(g => g.First())
                        .ToList()) {

                        // unless otherwise specified, skip creation of records derived from a package outside of the filter or schema
                        if (!Parameter.Instance.IsDuplicatePackageRecordOriginatingOutsideFilterAndSchema && Parameter.Instance.IsUsingSchemaFilter) {
                            // owned by another schema
                            if (!(rec.Owner ?? "").Equals(pack.Owner)) continue;

                            // owned by package within schema but was filtered out 
                            if (!packages.Exists(p => p.PackageName.Equals(rec.TypeName))) continue;
                        }

                        // always skip record creation if owned and used by another package *within* both the filter and schema
                        if (!(rec.TypeName ?? "").Equals(pack.PackageName) // if rec does not exist in this package
                            && packages.Exists(p => p.PackageName.Equals(rec.TypeName ?? "")                    // package owns record
                                                && p.HasProcedureWithRecordArgument(rec.RecordArgument)) ) {    // package uses record as argument
                            continue;
                        }

                        // do not create record from an instance of the record referenced in a different package
                        if (rec.IsDefinedExternally && (rec.TypeName ?? "").Equals(pack.PackageName)) continue;

                        pack.RecordsToGenerate.Add(rec); // record included in this package's generation (unless ignored due to implementation issue)

                        string reasonMsg;
                        if (!rec.IsIgnoredDueToOracleTypes(out reasonMsg)) {
                            // create interface for record class
                            classText.AppendLine();
                            classText.Append(GenerateEntityInterface(rec, 1));
                        }

                        // create DTO 
                        classText.AppendLine();
                        classText.Append(GenerateEntityClass(rec, ancestorRecordTypeClassName, 
                            Parameter.Instance.IsSerializablePackageRecord, Parameter.Instance.IsPartialPackage,
                            Parameter.Instance.IsDataContractPackageRecord, Parameter.Instance.IsXmlElementPackageRecord, 2));

                        if (!rec.IsIgnoredDueToOracleTypes(out reasonMsg)) {
                            // create custom reader
                            classText.AppendLine();
                            classText.Append(GenerateRecordTypeReadResultMethod(rec));
                        }
                    }

                    // create method for each package proc
                    foreach (IProcedure proc in pack.Procedures) GenerateAllMethodVersions(proc, pack, ref classText);
                    classText.AppendLine(Tab() + "} // " + className);

                    // write entire class to file
                    outFilePackage.Write(classText);
                }

                // close class and namespace for package
                outFilePackage.Write("" + "} // " + packageNamespace);

                outFilePackage.Close();
            } catch (UnauthorizedAccessException) {
                DisplayMessage(Message.BASE_PERMISSION_ERROR_MSG + Path.GetFileName(fileName));
            } catch (Exception e) {
                DisplayMessage("Error writing " + Path.GetFileName(fileName) + " - " + e.Message);
            }
        }
        #endregion

        #region Entity Generation (Record Type, Object Type, Table, View)
        /// <summary>
        /// Generate the class for an Oracle entity 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ancestorClassName">Use as ancestor class if there is no ancestor class already defined in Oracle (e.g., object type)</param>
        /// <param name="isSerializable"></param>
        /// <param name="isPartial"></param>
        /// <param name="tabIndentCount"></param>
        /// <returns></returns>
        private string GenerateEntityClass(IEntity entity, string ancestorClassName, bool isSerializable, bool isPartial, 
            bool isDataContract, bool isXmlElement, int tabIndentCount) {

            string className = entity.Translater.CSharpName;
            bool isPackageRecord = entity is IPackageRecord;
            StringBuilder classText = new StringBuilder("");

            string dbAncestorTypeName = null;   // only object type can have a database ancestor
            if (entity is IObjectType type) dbAncestorTypeName = type.DbAncestorTypeName;

            string classFirstLine = entity.Translater.CSharpAccessModifier + (entity.IsInstantiable ? "" : " abstract") + (isPartial ? " partial" : "") + " " + entity.Translater.CSharpType + " " + className
                + (!String.IsNullOrEmpty(dbAncestorTypeName)
                        ? " : " + TranslaterName.ConvertToPascal(dbAncestorTypeName) // Oracle ancestor gets precedence
                        : (!String.IsNullOrEmpty(ancestorClassName)
                            ? " : " + Parameter.Instance.NamespaceSchema + "." + ancestorClassName + (isPackageRecord ? ", " + CSharp.ToInterface(className) : "")
                            : "")) // user defined ancestor
                + " {"; // start entity type class;

            /////////////////////////////////////////////////////////////////////////////
            // bypass creation of package records that using unimplemented Oracle types
            if (entity.IsIgnoredDueToOracleTypes(out string ignoreReason)) {
                string entityType = entity.GetType().Name.Replace("Package", String.Empty).Replace("Type", String.Empty).ToUpper();
                int tab = isPackageRecord ? 2 : 1;
                classText.AppendLine(Tab(tab) + $"// **{entityType} IGNORED** - {ignoreReason}");
                classText.AppendLine(Tab(tab) + "// " + classFirstLine);
                return classText.ToString();
            }

            // C# attributes: DataContract, Serializable
            if (isDataContract || isSerializable) classText.Append(Tab(tabIndentCount));
            if (isDataContract) classText.Append(GenerateDataContractAttribute());
            if (isSerializable) classText.Append(CSharp.ATTRIBUTE_SERIALIZABLE);
            if (isDataContract || isSerializable) classText.AppendLine();

            classText.AppendLine(Tab(tabIndentCount) + classFirstLine);

            if (isPartial) classText.AppendLine(Tab(tabIndentCount + 1) + CSharp.PRIVATE + " " + CSharp.BYTE + " " + "propertyToEnsuresPartialClassNamesAreUniqueAtCompileTime" + " { get; set; }");

            foreach (IEntityAttribute attr in entity.Attributes) { // generate all attributes
                string nonPublicMemberName = TranslaterName.ConvertToCamel(attr.AttrName);
                string cSharpType = attr.Translater.GetCSharpType();

                if (attr.AttrTypeOwner != null && !attr.AttrTypeOwner.Equals(entity.Owner) && !attr.AttrTypeOwner.Equals("SYS")) {
                    cSharpType = GenerateNamespaceObjectType(_baseNamespace, attr.AttrTypeOwner, GetFilterValueIfUsedInNaming()) + "." + cSharpType;
                }

                // C# attributes
                if (isDataContract || isXmlElement) classText.Append(Tab(tabIndentCount + 1));
                if (isDataContract) classText.Append("[DataMember(Order=" + attr.Position.ToString() 
                    + ", IsRequired=" + (attr.Nullable ? "false" : "true") + ")]");
                if (isXmlElement) classText.Append("[XmlElement(Order=" + attr.Position.ToString() + ", IsNullable=true)]");
                if (isDataContract || isXmlElement) classText.AppendLine();

                classText.Append(Tab(tabIndentCount + 1) + "public virtual "
                    + (attr.ContainerClassName == null ? "" : attr.ContainerClassName + ".")
                    + cSharpType + " " + TranslaterName.Convert(attr)
                    + (Parameter.Instance.IsUseAutoImplementedProperties 
                        ? " { get; set; }"
                        : " { get { return " + nonPublicMemberName + "; } set { " + nonPublicMemberName + " = value; } }"));
                classText.AppendLine(Parameter.Instance.IsUseAutoImplementedProperties 
                    ? ""
                    : " protected " + (attr.ContainerClassName == null ? "" : attr.ContainerClassName + ".") + cSharpType + " " + nonPublicMemberName + ";");
            }

            classText.AppendLine(Tab(tabIndentCount) + "} // " + className); // end entity type class
            return classText.ToString();
        }

        /// <summary>
        /// Generate the interface for an entity class
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tabIndentCount">number of tabs to indent</param>
        /// <returns></returns>
        private string GenerateEntityInterface(IEntity entity, int tabIndentCount) {

            string interfaceName = CSharp.ToInterface(entity.Translater.CSharpName);

            StringBuilder classText = new StringBuilder("");
            classText.AppendLine(Tab(tabIndentCount + 1) + entity.Translater.CSharpAccessModifier + " interface " + interfaceName + " {"); // start record interface
            foreach (IEntityAttribute attr in entity.Attributes) { // loop through all fields
                string cSharpType = attr.Translater.GetCSharpType();
                classText.AppendLine(Tab(tabIndentCount + 2) + (attr.ContainerClassName == null ? "" : attr.ContainerClassName + ".") + cSharpType 
                    + " " + TranslaterName.Convert(attr)
                    + " { set; }");
            }
            classText.AppendLine(Tab(tabIndentCount + 1) + "} // " + interfaceName); // end record interface
            return classText.ToString();
        }

        private void WriteNonPackagedEntityClasses<I_Entity>(List<IEntity> entities, string entityNamespace, string ancestorClassName, 
            bool isSerializable, bool isPartial, bool isDataMember, bool isXmlElement)
            where I_Entity : IEntity {

            string entityTypeName = typeof(I_Entity).Name.TrimStart(@"I".ToCharArray());
            string fileName = _outputPath + @"\" + CaseConverter.ConvertUnderscoreDelimitedToPascalCase(_schema)
                + CaseConverter.ConvertUnderscoreDelimitedToPascalCase(GetFilterValueIfUsedInNaming()) + entityTypeName + ".cs";

            DisplayMessage("Coding " + entityTypeName.ToLower() + "s (" + fileName.Substring(fileName.LastIndexOf('\\') + 1) + ")...");

            try {
                StreamWriter outFile = new StreamWriter(fileName);

                Comment.Instance.WriteAutoGeneratedComment(outFile);

                // create using statements
                StringBuilder headerText = new StringBuilder("");
                headerText.AppendLine("using System;");
                headerText.AppendLine("using System.Runtime.Serialization;");
                headerText.AppendLine("using System.Xml;");
                headerText.AppendLine("using System.Xml.Serialization;");
                headerText.AppendLine(USING_ORACLE_DATAACCESS_TYPES + ";");
                if (typeof(I_Entity).Equals(typeof(ITable)) || typeof(I_Entity).Equals(typeof(IView)))
                    headerText.AppendLine("using " + _objectTypeNamespace + ";"); // tables and views need access to object type in case column uses one as a type
                outFile.Write(headerText);
                outFile.WriteLine();

                // write namespace 
                outFile.WriteLine("namespace " + entityNamespace + " {");

                foreach (IEntity entity in entities) {
                    if (entities.IndexOf(entity) != 0) outFile.WriteLine();
                    outFile.Write(GenerateEntityClass(entity, ancestorClassName, isSerializable, isPartial, isDataMember, isXmlElement, 1));
                }

                // close class and namespace for package
                outFile.Write("" + "} // " + entityNamespace);
                outFile.Close();
            } catch (UnauthorizedAccessException) {
                DisplayMessage(Message.BASE_PERMISSION_ERROR_MSG + Path.GetFileName(fileName));
            } catch (Exception e) {
                DisplayMessage("Error writing " + Path.GetFileName(fileName) + " - " + e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Generator entry point
        /// </summary>
        /// <param name="displayMessageMethod">Method used to display message in UI</param>
        public static void Run(Action<string> displayMessageMethod) {
            if (!(Parameter.Instance.IsGeneratePackage || Parameter.Instance.IsGenerateObjectType || Parameter.Instance.IsGenerateTable || Parameter.Instance.IsGenerateView)) {
                displayMessageMethod(Message.NO_GENERATE_OPTIONS_SELECTED);
                return;
            }

            // initialize Translater first since Loader does some translation. In the future, we need to modify Loader to do no translation (if possible).
            TranslaterManager.Initialize(Parameter.Instance);

            // retrieve necessary data from schema
            Loader loader = new Loader(Parameter.Instance, displayMessageMethod);
            try {
                loader.Load();
            } catch (Exception e) {
                displayMessageMethod(e.Message);
                return;
            }

            TranslaterManager.AssignTranslaters(loader);

            // instantiate generator
            Generator generator = new Generator(Parameter.Instance.Schema, Parameter.Instance.OutputPath, displayMessageMethod, Parameter.Instance.DatabaseInstance,
                                                Parameter.Instance.UserLogin, Parameter.Instance.Password, Parameter.Instance.NamespaceBase, Parameter.Instance.NamespaceObjectType);

            ////////////////////////
            // generate base classes
            if (Parameter.Instance.IsGenerateBaseAdapter) generator.WriteBasePackageClass(Parameter.Instance.AncestorClassNamePackage);
            if (Parameter.Instance.IsGenerateBaseEntities) generator.WriteBaseEntityClasses(Parameter.Instance.AncestorClassNamePackageRecord);

            //////////////////////////////////
            // generate schema-derived classes
            if (Parameter.Instance.IsGeneratePackage)
                generator.WritePackageClasses(loader.Packages, loader.PackageRecordTypes, Parameter.Instance.NamespacePackage, Parameter.Instance.AncestorClassNamePackage, 
                    Parameter.Instance.IsPartialPackage, Parameter.Instance.AncestorClassNamePackageRecord);
            if (Parameter.Instance.IsGenerateObjectType)
                generator.WriteNonPackagedEntityClasses<IObjectType>(loader.ObjectTypes, Parameter.Instance.NamespaceObjectType, Generator.GenerateBaseObjectTypeClassName(Parameter.Instance.Schema),
                    Parameter.Instance.IsSerializableObjectType, Parameter.Instance.IsPartialObjectType, Parameter.Instance.IsDataContractObjectType, Parameter.Instance.IsXmlElementObjectType);
            if (Parameter.Instance.IsGenerateTable)
                generator.WriteNonPackagedEntityClasses<ITable>(loader.Tables, Parameter.Instance.NamespaceTable, Generator.GenerateBaseTableClassName(Parameter.Instance.Schema),
                    Parameter.Instance.IsSerializableTable, Parameter.Instance.IsPartialTable, Parameter.Instance.IsDataContractTable, Parameter.Instance.IsXmlElementTable);
            if (Parameter.Instance.IsGenerateView)
                generator.WriteNonPackagedEntityClasses<IView>(loader.Views, Parameter.Instance.NamespaceView, Generator.GenerateBaseViewClassName(Parameter.Instance.Schema),
                    Parameter.Instance.IsSerializableView, Parameter.Instance.IsPartialView, Parameter.Instance.IsDataContractView, Parameter.Instance.IsXmlElementView);

            generator.DeployUtilityClasses(Parameter.Instance.IsDeployResources);
            displayMessageMethod(Message.GENERATION_COMPLETE);
        }

        #region Miscellanous Methods
        public static string GetAppVersion() {
            string version = "";
            object[] attributes = typeof(Generator).Assembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false);
            if (attributes.Length > 0) version = (attributes[0] as AssemblyInformationalVersionAttribute).InformationalVersion;
            return version;
        }

        public static string GetAppNameVersionLabel() {
            return APPLICATION_NAME + " " + GetAppVersion()
#if DEBUG
                + " *** DEBUG BUILD ***"
#endif
                ;
        }

        /// <summary>
        /// Deploy copy of necessary code that is not generated from schema
        /// </summary>
        private void DeployUtilityClasses(bool overwrite) {
            string fileName, filePath;

            // deploy OrclPower
            fileName = @"OrclPower.cs";
            filePath = _outputPath + @"\" + fileName;
            try {
                if (overwrite || !File.Exists(filePath)) {
                    File.Delete(filePath); // delete existing file since we have to write file in sections
                    File.WriteAllText(filePath, Comment.Instance.CommentAutoGenerated + Environment.NewLine);
                    if (IsCSharp30) File.AppendAllText(filePath, "#define CSHARP30\r\n");   // for C# 3.0, add compiler option
                    File.AppendAllText(filePath, Properties.Resources.OrclPower);           // write body of source code
                }
            } catch (UnauthorizedAccessException) {
                DisplayMessage(Message.BASE_PERMISSION_ERROR_MSG + Path.GetFileName(fileName));
            } catch (Exception e) {
                DisplayMessage("Error writing " + Path.GetFileName(fileName) + " - " + e.Message);
            }

            // deploy CaseConversion
            fileName = @"CaseConversion.cs";
            filePath = _outputPath + @"\" + fileName;
            try {
                if (overwrite || !File.Exists(filePath)) {
                    File.Delete(filePath); // delete existing file since we have to write file in sections
                    File.WriteAllText(filePath, Comment.Instance.CommentAutoGenerated + Environment.NewLine);
                    File.AppendAllText(filePath, Properties.Resources.CaseConversion);  // write body of source code
                }
            } catch (UnauthorizedAccessException) {
                DisplayMessage(Message.BASE_PERMISSION_ERROR_MSG + Path.GetFileName(fileName));
            } catch (Exception e) {
                DisplayMessage("Error writing " + Path.GetFileName(fileName) + " - " + e.Message);
            }

        }

        /// <summary>
        /// Return a string spaces to be used as a single tab 
        /// </summary>
        /// <returns></returns>
        private string Tab() {
            return Tab(1);
        }

        /// <summary>
        /// Return a string of spaces for a given number of tabs 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private string Tab(int count) {
            string tabs = "";
            for (int i = 0; i < count; i++) tabs += new String(' ', 4); // tab is 4 spaces
            return tabs;
        }

        /// <summary>
        /// Determine the size(length) for an OracleParameter bound to a C# String
        /// </summary>
        /// <param name="oracleType"></param>
        /// <returns></returns>
        private static int GetStringArgBindSize(string oracleType) {

            switch (oracleType) {
                case Orcl.CHAR:
                case Orcl.NCHAR:
                    return 2000; // fixed length of 2000

                case Orcl.NCLOB:
                case Orcl.CLOB:
                    return Int32.MaxValue; // max value allowed for OracleParameter size param
                
                default: // VARCHAR2, VARCHAR, NVARCHAR2 or equivalents
                    return Parameter.Instance.MaxReturnAndOutArgStringSize; // custom defined value
            }
        }

        private string GenerateDataContractAttribute() {
            return @"[" + CSharp.ATTRIBUTE_DATA_CONTRACT + "("
                + (String.IsNullOrEmpty(Parameter.Instance.NamespaceDataContract)
                    ? ""
                    : @"Namespace=""" + Parameter.Instance.NamespaceDataContract + @"""") 
                + ")]";
        }

        private static string GenerateLocalVariableName(string baseLocalVarName) {
            return Parameter.Instance.LocalVariableNameSuffix + baseLocalVarName;
        }

        private void DisplayMessage(string msg) { _displayMessageMethod(msg); }
        #endregion
    }
}