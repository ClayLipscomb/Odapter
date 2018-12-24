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

namespace Odapter {
    /// <summary>
    /// Oracle specific types and logic
    /// </summary>
    public static class OrclUtil {
        private static readonly List<string> _complexDataTypes = new List<string> { Orcl.ASSOCIATITVE_ARRAY, Orcl.OBJECT_TYPE, Orcl.RECORD, Orcl.REF_CURSOR, Orcl.VARRAY, Orcl.XMLTYPE };

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

        /// <summary>
        /// Determine if Oracle type is an unqualified NUMBER or an equivalent (i.e., no precision or scale)
        /// </summary>
        /// <param name="oracleType"></param>
        /// <returns></returns>
        internal static bool IsOracleNumberEquivalent(String oracleType) {
            if (new List<String>() { Orcl.NUMBER, Orcl.FLOAT, Orcl.BINARY_FLOAT }.Contains(oracleType)) return true;
            return true;
        }

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

        private static string NormalizeDataType(ITyped dataType) {
            var dataTypeBase = dataType.DataType;

            if (dataTypeBase == null) dataTypeBase = Orcl.NULL; // procedure with *no parameters* will have a placeholder NULL-typed parameter representing no return

            if (dataTypeBase.StartsWith(Orcl.TIMESTAMP) && dataTypeBase.EndsWith(" TZ")) dataTypeBase = dataTypeBase.Replace(" TZ", " TIME ZONE"); // object attributes use "TZ" instead of "TIME ZONE"!

            if (dataTypeBase.Contains("(")) { // strip any inline parenthetical precision from data type - columns can have this
                int openParenIndex = dataTypeBase.IndexOf("(");
                dataTypeBase = dataTypeBase.Remove(openParenIndex, dataTypeBase.IndexOf(")") - openParenIndex + 1);
            }

            if (dataTypeBase == Orcl.UNDEFINED && dataType.DataTypeProperName == Orcl.XMLTYPE) dataTypeBase = Orcl.XMLTYPE;

            return dataTypeBase;
        }
    
        internal static void Normalize(ITyped dataType) {
            dataType.PreNormalizedValues = String.Format("DataType: {0}, DataPrecison: {1}, DataScale: {2}, CharLength: {3}", 
                dataType.DataType, dataType.DataPrecision.HasValue ? dataType.DataPrecision.ToString() : "null", dataType.DataScale.HasValue ? dataType.DataScale.ToString() : "null", dataType.CharLength.HasValue ? dataType.CharLength.ToString() : "null");

            var dataTypeSearch = NormalizeDataType(dataType);
            if (!OrclUtil.IsExistsType(dataTypeSearch)) dataTypeSearch = Orcl.OBJECT_TYPE;
            dataType.OrclType = OrclUtil.GetType(dataTypeSearch);
            if ( !dataType.OrclType.GetType().Equals(typeof(OrclUndefinedType)) && !dataType.OrclType.GetType().Equals(typeof(OrclObjectType)) ) dataType.DataType = dataType.OrclType.DataType;

            int? precision, scale;
            dataType.OrclType.NormalizePrecisionScale(dataType, out precision, out scale);
            dataType.DataPrecision = precision;
            dataType.DataScale = scale;
            dataType.CharLength = dataType.OrclType.NormalizeCharLength(dataType);
        }

        internal static string BuildAggregateType(ITyped dataType) {
            if (dataType.CharLength.HasValue) {
                return dataType.DataType + "(" + dataType.CharLength + ")";   // VARCHAR2, CHAR2, etc.
            } else if (dataType.DataPrecision.HasValue) {
                if (dataType.DataType.StartsWith(Orcl.TIMESTAMP)) {  // TIMESTAMP, TIMESTAMP WITH TIME ZONE, etc.
                    return dataType.DataType.Insert(9, "(" + dataType.DataPrecision + ")");
                } else {
                    return dataType.DataType + "(" + dataType.DataPrecision + (dataType.DataScale > 0 ? "," + dataType.DataScale : "") + ")"; // a number
                }
            } else {
                return dataType.DataType;
            }
        }

        internal static string AppendPrecision(string oracleType, int precision) { return oracleType + $"({precision})"; }
    }
}