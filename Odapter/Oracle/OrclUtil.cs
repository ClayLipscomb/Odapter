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

using OU = Odapter.OrclUtil;

namespace Odapter {
    /// <summary>
    /// Oracle specific types and logic
    /// </summary>
    public static class OrclUtil {
        private static readonly List<string> _complexDataTypes = 
            new List<string> { Orcl.OBJECT, Orcl.NESTED_TABLE, Orcl.VARRAY, Orcl.XMLTYPE, Orcl.ANYDATA, Orcl.ANYDATASET, Orcl.ANYTYPE, Orcl.ASSOCIATITVE_ARRAY, Orcl.RECORD, Orcl.REF_CURSOR };
        private static readonly List<string> _sqlComplexDataTypes =
            new List<string> { Orcl.OBJECT, Orcl.NESTED_TABLE, Orcl.VARRAY, Orcl.XMLTYPE, Orcl.ANYDATA, Orcl.ANYDATASET, Orcl.ANYTYPE };

        private static readonly IEnumerable<IOrclType> OracleTypes = new HashSet<IOrclType> {
                new OrclAssociativeArray(),
                new OrclAnydata(),
                new OrclAnydataset(),
                new OrclAnytype(),
                new OrclBinaryDouble(),
                new OrclBinaryFloat(),
                new OrclBinaryInteger(),
                new OrclBfile(),
                new OrclBlob(),
                new OrclBoolean(),
                new OrclChar(),
                new OrclClob(),
                new OrclDate(),
                new OrclDoublePrecision(),
                new OrclDecimal(),
                new OrclFloat(),
                new OrclInteger(),
                new OrclIntervalDayToSecond(),
                new OrclIntervalYearToMonth(),
                new OrclLong(),
                new OrclLongRaw(),
                new OrclNumber(),
                new OrclNatural(),
                new OrclNaturaln(),
                new OrclNchar(),
                new OrclNclob(),
                new OrclNestedTable(),
                new OrclNumeric(),
                new OrclNvarchar2(),
                new OrclObjectType(),
                new OrclPlsInteger(),
                new OrclPlsqlBoolean(),
                new OrclPositive(),
                new OrclPositiven(),
                new OrclRaw(),
                new OrclReal(),
                new OrclRecord(),
                new OrclRef(),
                new OrclRefCursor(),
                new OrclRowtype(),
                new OrclRowid(),
                new OrclSmallint(),
                new OrclString(),
                new OrclTimestamp(),
                new OrclTimestampLTZ(),
                new OrclTimestampTZ(),
                new OrclUndefinedType(),
                new OrclUrowid(),
                new OrclVarchar(),
                new OrclVarchar2(),
                new OrclVarray(),
                new OrclXmltype(),

                new OrclProcedureReturn()
        };

        internal static bool IsComplexDataType(string dataType) { return _complexDataTypes.Contains(dataType); }
        internal static bool IsSqlComplexDataType(string dataType) { return _sqlComplexDataTypes.Contains(dataType); }
        internal static String GetDataTypeProperName(IEntityAttribute entityAttribute) {
            return (IsExistsType(entityAttribute.DataType) ? String.Empty : entityAttribute.DataType);
        }
        private static bool IsExistsType(string dataType) { return OracleTypes.Any(ot => ot.DataType.Equals(dataType)); }

        internal static bool IsWholeNumberOfPrecision(ITyped dataType, int precision) {
            return dataType.DataType == Orcl.NUMBER && dataType.DataPrecision == precision && (dataType.DataScale ?? 0) == 0;
        }

        internal static bool IsIdLabel(string label) { return !String.IsNullOrEmpty(label) && (label.Trim().ToUpper().EndsWith("_ID") || label.Trim().ToUpper().Equals("ID")); }

        internal static IOrclType GetType(string dataType) {
            string dataTypeSearch;
            if (IsExistsType(dataType))
                dataTypeSearch = dataType;
            else
                dataTypeSearch = Orcl.UNDEFINED;

            return OracleTypes.SingleOrDefault(ot => ot.DataType.Equals(dataTypeSearch));
        }

        /// <summary>
        /// Procedure with *no parameters* will have a placeholder NULL-typed parameter representing no return
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        private static string NormalizeDataTypeProcedureReturn(string dataType) => dataType ?? Orcl.PROCEDURE_RETURN;

        private static string NormalizeDataTypeTimestamp(string dataType) {
            var tzSuffix = @" TZ"; // object attributes use "TZ" instead of "TIME ZONE"!
            return dataType.StartsWith(Orcl.TIMESTAMP) && dataType.EndsWith(tzSuffix) ? dataType.Replace(tzSuffix, " TIME ZONE") : dataType;
        }

        private static string NormalizeDataTypeAggregated(string dataType) {
            if (dataType.Contains("(")) { // strip any inline parentheses - columns can have this
                int openParenIndex = dataType.IndexOf("(");
                dataType = dataType.Remove(openParenIndex, dataType.IndexOf(")") - openParenIndex + 1);
            }
            return dataType;
        }

        /// <summary>
        /// Handle case when the DataType returned from Oracle view is "UNDEFINED"
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string NormalizeDataTypeUndefined(string dataType, ITyped type) => dataType == Orcl.UNDEFINED ? type.DataTypeProperName : dataType;

        private static string NormalizeDataTypeRowtype(string dataType, ITyped type) => dataType == Orcl.RECORD && type.DataTypeProperName == null ? Orcl.ROWTYPE : dataType;

        private static string NormalizeDataTypeFromTypeCode(string dataType, ITyped type) {
            if ( !(type is Argument) && !OrclUtil.IsExistsType(dataType) )
                switch (type.Typecode) {   // Typecode only used for table, view, object attributes (not argument)
                    case Orcl.TYPECODE_NESTED_TABLE:
                        dataType = Orcl.NESTED_TABLE;
                        break;
                    case Orcl.TYPECODE_OBJECT:
                        dataType = Orcl.OBJECT;
                        break;
                    case Orcl.TYPECODE_ANYDATA:
                        dataType = Orcl.ANYDATA;
                        break;
                    case Orcl.TYPECODE_ANYDATASET:
                        dataType = Orcl.ANYDATASET;
                        break;
                    case Orcl.TYPECODE_ANYTYPE:
                        dataType = Orcl.ANYTYPE;
                        break;
                    //case Orcl.TYPECODE_LCR_DDL_RECORD:
                    //case Orcl.TYPECODE_LCR_PROCEDURE_RECORD:
                    //case Orcl.TYPECODE_LCR_ROW_RECORD:
                    case Orcl.TYPECODE_XMLTYPE:
                    default:
                        dataType = Orcl.XMLTYPE;
                        break;
                    //default:
                    //    break;
                }
            return dataType;
        }

        internal static string NormalizeDataType(ITyped type) {
            string dataType = type.DataType;
            dataType = OU.NormalizeDataTypeProcedureReturn(dataType);
            dataType = OU.NormalizeDataTypeTimestamp(dataType);
            dataType = OU.NormalizeDataTypeAggregated(dataType);
            dataType = OU.NormalizeDataTypeUndefined(dataType, type);
            dataType = OU.NormalizeDataTypeRowtype(dataType, type);
            dataType = OU.NormalizeDataTypeFromTypeCode(dataType, type);
            return dataType;
        }
    
        internal static void Normalize(ITyped type) {
            type.PreNormalizedValues = String.Format("DataType: {0}, DataPrecison: {1}, DataScale: {2}, CharLength: {3}", 
                type.DataType, type.DataPrecision.HasValue ? type.DataPrecision.ToString() : "null", type.DataScale.HasValue ? type.DataScale.ToString() : "null", type.CharLength.HasValue ? type.CharLength.ToString() : "null");
            var dataTypeNormalized = NormalizeDataType(type);
            type.OrclType = OrclUtil.GetType(dataTypeNormalized);

            type.OrclType.NormalizePrecisionScale(type, out int? precision, out int? scale);
            type.DataPrecision = precision;
            type.DataScale = scale;
            type.CharLength = type.OrclType.NormalizeCharLength(type);
        }

        internal static string BuildAggregateType(ITyped type) {
            if (type.CharLength.HasValue && type.CharLength.Value != 0) {
                return type.DataType + "(" + type.CharLength + ")";   // VARCHAR2, CHAR2, etc.
            } else if (type.DataPrecision.HasValue) {
                if (type.DataType.StartsWith(Orcl.TIMESTAMP)) {  // TIMESTAMP, TIMESTAMP TZ, TIMESTAMP LTZ
                    return type.DataType.Insert(9, "(" + type.DataPrecision + ")");
                } else {
                    return type.DataType + "(" + type.DataPrecision + (type.DataScale > 0 ? "," + type.DataScale : "") + ")"; // a number
                }
            } else if (type.DataType == Orcl.RECORD || type.DataType == Orcl.ROWTYPE) {
                return type.OrclType.BuildDataTypeFullName(type);
            } else {
                return type.DataType;
            }
        }

        internal static string AppendPrecision(string oracleType, int precision) { return oracleType + $"({precision})"; }
    }
}