//------------------------------------------------------------------------------
//    Odapter - a C# code generator for Oracle packages
//    Copyright(C) 2019 Clay Lipscomb
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

using OU = Odapter.OrclUtil;

namespace Odapter {
    /// <summary>
    /// Oracle specific types and logic
    /// </summary>
    public static class OrclUtil {
        private static readonly List<string> _complexDataTypes = new List<string> { Orcl.ASSOCIATITVE_ARRAY, Orcl.OBJECT, Orcl.RECORD, Orcl.REF_CURSOR, Orcl.VARRAY, Orcl.XMLTYPE };

        private static IList<IOrclType> OracleTypes = new List<IOrclType> {
                new OrclAssociativeArray(),
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
                new OrclRowid(),
                new OrclSmallint(),
                new OrclString(),
                new OrclTimestamp(),
                new OrclTimestampWithLocalTimeZone(),
                new OrclTimestampWithTimeZone(),
                new OrclUndefinedType(),
                new OrclUrowid(),
                new OrclVarchar(),
                new OrclVarchar2(),
                new OrclVarray(),
                new OrclNullType(),
                new OrclXmltype()
        };

        internal static bool IsComplexDataType(string dataType) { return _complexDataTypes.Contains(dataType); }

        internal static bool IsExistsType(string dataType) { return OracleTypes.Any(ot => ot.DataType.Equals(dataType)); }

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

        private static ITyped NormalzeDataTypeNull(ITyped type) {
            type.DataType = type.DataType == null ? Orcl.NULL : type.DataType; // procedure with *no parameters* will have a placeholder NULL-typed parameter representing no return
            return type;
        }

        private static ITyped NormalzeDataTypeTimestamp(ITyped type) {
            var tzSuffix = @" TZ"; // object attributes use "TZ" instead of "TIME ZONE"!
            type.DataType = type.DataType.StartsWith(Orcl.TIMESTAMP) && type.DataType.EndsWith(tzSuffix) ? type.DataType.Replace(tzSuffix, " TIME ZONE") : type.DataType;
            return type;
        }

        private static ITyped NormalzeDataTypeAggregated(ITyped type) {
            if (type.DataType.Contains("(")) { // strip any inline parentheses - columns can have this
                int openParenIndex = type.DataType.IndexOf("(");
                type.DataType = type.DataType.Remove(openParenIndex, type.DataType.IndexOf(")") - openParenIndex + 1);
            }
            return type;
        }

        private static ITyped NormalzeDataTypeXmlType(ITyped type) {
            type.DataType = type.DataType == Orcl.UNDEFINED && type.DataTypeProperName == Orcl.XMLTYPE ? Orcl.XMLTYPE : type.DataType;
            return type;
        }

        private static string NormalizeDataType(ITyped type) {
            var typeN = OU.NormalzeDataTypeNull(type);
            typeN = OU.NormalzeDataTypeTimestamp(typeN);
            typeN = OU.NormalzeDataTypeAggregated(typeN);
            typeN = OU.NormalzeDataTypeXmlType(typeN);
            return typeN.DataType;
        }
    
        internal static void Normalize(ITyped type) {
            type.PreNormalizedValues = String.Format("DataType: {0}, DataPrecison: {1}, DataScale: {2}, CharLength: {3}", 
                type.DataType, type.DataPrecision.HasValue ? type.DataPrecision.ToString() : "null", type.DataScale.HasValue ? type.DataScale.ToString() : "null", type.CharLength.HasValue ? type.CharLength.ToString() : "null");

            var dataTypeSearch = NormalizeDataType(type);
            if (!OrclUtil.IsExistsType(dataTypeSearch)) dataTypeSearch = Orcl.OBJECT;
            type.OrclType = OrclUtil.GetType(dataTypeSearch);
            if (type.OrclType.DataType != Orcl.OBJECT) type.DataType = type.OrclType.DataType;

            int? precision, scale;
            type.OrclType.NormalizePrecisionScale(type, out precision, out scale);
            type.DataPrecision = precision;
            type.DataScale = scale;
            type.CharLength = type.OrclType.NormalizeCharLength(type);
        }

        internal static string BuildAggregateType(ITyped type) {
            if (type.CharLength.HasValue) {
                return type.DataType + "(" + type.CharLength + ")";   // VARCHAR2, CHAR2, etc.
            } else if (type.DataPrecision.HasValue) {
                if (type.DataType.StartsWith(Orcl.TIMESTAMP)) {  // TIMESTAMP, TIMESTAMP WITH TIME ZONE, etc.
                    return type.DataType.Insert(9, "(" + type.DataPrecision + ")");
                } else {
                    return type.DataType + "(" + type.DataPrecision + (type.DataScale > 0 ? "," + type.DataScale : "") + ")"; // a number
                }
            } else {
                return type.DataType;
            }
        }

        internal static string AppendPrecision(string oracleType, int precision) { return oracleType + $"({precision})"; }
    }
}