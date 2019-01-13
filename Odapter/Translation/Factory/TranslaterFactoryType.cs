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

namespace Odapter {
    /// <summary>
    /// Handle translation from Oracle to C#
    /// </summary>
    public static class TranslaterFactoryType {

        private static IList<ITranslaterType> OracleTypeTranslaters;
        private static void InitTypeTranslaters() {
            OracleTypeTranslaters = new List<ITranslaterType> { // *** Order of list matters. Modify with care. ***
                // Keep these 10 specific cases at top of list in this order
                new TranslaterInteger(CSharpTypeUsedForOracleInteger, IsConvertOracleNumberToIntegerIfColumnNameIsId),
                new TranslaterNumber9(),
                new TranslaterNumber8(),
                new TranslaterNumber7(),
                new TranslaterNumber6(),
                new TranslaterNumber5(),
                new TranslaterNumber4(),
                new TranslaterNumber3(),
                new TranslaterNumber2(),
                new TranslaterNumber1(),

                new TranslaterBfile(),
                new TranslaterBinaryDouble(),
                new TranslaterBinaryFloat(),
                new TranslaterBinaryInteger(),
                new TranslaterBlob(CSharpTypeUsedForOracleBlob),
                new TranslaterBoolean(),
                new TranslaterChar(),
                new TranslaterClob(CSharpTypeUsedForOracleClob),
                new TranslaterDate(CSharpTypeUsedForOracleDate),
                new TranslaterDecimal(),
                new TranslaterDoublePrecision(CSharpTypeUsedForOracleNumber),
                new TranslaterFloat(CSharpTypeUsedForOracleNumber),
                new TranslaterIntervalDayToSecond(),
                new TranslaterIntervalYearToMonth(),
                new TranslaterLong(),
                new TranslaterLongRaw(),
                new TranslaterNatural(),
                new TranslaterNaturaln(),
                new TranslaterNchar(),
                new TranslaterNclob(CSharpTypeUsedForOracleClob),
                new TranslaterNvarchar2(),
                new TranslaterNumber(CSharpTypeUsedForOracleNumber),
                new TranslaterNumeric(),
                new TranslaterPlsInteger(),
                new TranslaterPlsqlBoolean(),
                new TranslaterPositive(),
                new TranslaterPositiven(),
                new TranslaterRaw(),
                new TranslaterRef(),
                new TranslaterReal(CSharpTypeUsedForOracleNumber),
                new TranslaterRowId(),
                new TranslaterSmallint(CSharpTypeUsedForOracleInteger),
                new TranslaterString(),
                new TranslaterTimestamp(CSharpTypeUsedForOracleTimeStamp),
                new TranslaterTimestampWithLocalTimeZone(),
                new TranslaterTimestampWithTimeZone(),
                new TranslaterVarray(),
                new TranslaterURowId(),
                new TranslaterVarchar(),
                new TranslaterVarchar2(),
                new TranslaterNullType(),
                new TranslaterXmltype()
            };
        }

        internal static void Initialize(IParameterTranslation param) {
            CSharpTypeUsedForOracleRefCursor                = param.CSharpTypeUsedForOracleRefCursor;
            CSharpTypeUsedForOracleAssociativeArray         = param.CSharpTypeUsedForOracleAssociativeArray;
            CSharpTypeUsedForOracleInteger                  = param.CSharpTypeUsedForOracleInteger;
            CSharpTypeUsedForOracleNumber                   = param.CSharpTypeUsedForOracleNumber;
            IsConvertOracleNumberToIntegerIfColumnNameIsId  = param.IsConvertOracleNumberToIntegerIfColumnNameIsId;
            CSharpTypeUsedForOracleDate                     = param.CSharpTypeUsedForOracleDate;
            CSharpTypeUsedForOracleTimeStamp                = param.CSharpTypeUsedForOracleTimeStamp;
            CSharpTypeUsedForOracleIntervalDayToSecond      = param.CSharpTypeUsedForOracleIntervalDayToSecond;
            CSharpTypeUsedForOracleBlob                     = param.CSharpTypeUsedForOracleBlob;
            CSharpTypeUsedForOracleClob                     = param.CSharpTypeUsedForOracleClob;

            InitTypeTranslaters();
        }

        #region Properties for Custom Type Translation
        private static string CSharpTypeUsedForOracleRefCursor { get; set; }
        private static string CSharpTypeUsedForOracleAssociativeArray { get; set; }
        private static string CSharpTypeUsedForOracleInteger { get; set; }
        private static string CSharpTypeUsedForOracleNumber { get; set; }
        private static bool IsConvertOracleNumberToIntegerIfColumnNameIsId;
        private static string CSharpTypeUsedForOracleDate { get; set; }
        private static string CSharpTypeUsedForOracleTimeStamp { get; set; }
        private static string CSharpTypeUsedForOracleIntervalDayToSecond { get; set; }
        private static string CSharpTypeUsedForOracleBlob { get; set; }
        private static string CSharpTypeUsedForOracleClob { get; set; }
        #endregion

        internal static ITranslaterType GetTranslater(ITyped dataType) {
            string dataTypeFull = String.Empty;

            if (OracleTypeTranslaters.Any(t => t.IsValid(dataType))) {  
                dataTypeFull = OracleTypeTranslaters.First(t => t.IsValid(dataType)).DataTypeFull;
            } else {
                dataTypeFull = dataType.OrclType.BuildDataTypeFullName(dataType);
                switch (dataType.DataType) { // dynamically create custom type translaters for complex types
                    case Orcl.ASSOCIATITVE_ARRAY:
                        OracleTypeTranslaters.Add(new TranslaterAssociativeArray(dataTypeFull, CSharpTypeUsedForOracleAssociativeArray, dataType));
                        break;
                    case Orcl.REF_CURSOR:
                        if (dataType.SubType == null)
                            OracleTypeTranslaters.Add(new TranslaterRefCursorUntyped(dataTypeFull, CSharpTypeUsedForOracleRefCursor, dataType));
                        else
                            OracleTypeTranslaters.Add(new TranslaterRefCursorTyped(dataTypeFull, CSharpTypeUsedForOracleRefCursor, dataType));
                        break;
                    case Orcl.OBJECT:
                        OracleTypeTranslaters.Add(new TranslaterObjectType(dataTypeFull));
                        break;
                    case Orcl.RECORD:
                        OracleTypeTranslaters.Add(new TranslaterRecordType(dataTypeFull));
                        break;
                    default:
                        OracleTypeTranslaters.Add(new TranslaterUndefinedType(dataTypeFull));
                        break;
                }
            }

            return OracleTypeTranslaters.First(t => t.DataTypeFull.Equals(dataTypeFull));
        }

        internal static ITranslaterType GetTranslaterNull() { return OracleTypeTranslaters.First(t => t is TranslaterNullType); }
    }
}