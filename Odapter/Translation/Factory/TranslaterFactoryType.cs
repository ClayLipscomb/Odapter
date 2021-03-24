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
using CS = Odapter.CSharp;
using CSL = Odapter.CSharp.Logic.Api;

namespace Odapter {
    /// <summary>
    /// Handle translation from Oracle to C#
    /// </summary>
    public static class TranslaterFactoryType {

        private static int TypesInitializedCount;
        private static IList<ITranslaterType> OracleTypeTranslaters;
        private static void InitTypeTranslaters() {
            OracleTypeTranslaters = new List<ITranslaterType> { // *** Order of list matters. Modify with care. ***
                // Keep these 10 specific cases at top of list in this order
                new TranslaterInteger(TypeTargetForOracleInteger, IsConvertOracleNumberToIntegerIfColumnNameIsId),
                new TranslaterNumber9(),
                new TranslaterNumber8(),
                new TranslaterNumber7(),
                new TranslaterNumber6(),
                new TranslaterNumber5(),
                new TranslaterNumber4(),
                new TranslaterNumber3(),
                new TranslaterNumber2(),
                new TranslaterNumber1(),

                new TranslaterAnydata(),
                new TranslaterAnydataset(),
                new TranslaterAnytype(),
                new TranslaterBfile(),
                new TranslaterBinaryDouble(),
                new TranslaterBinaryFloat(),
                new TranslaterBinaryInteger(),
                new TranslaterBlob(TypeTargetForOracleBlob),
                new TranslaterBoolean(),
                new TranslaterChar(),
                new TranslaterClob(TypeTargetForOracleClob),
                new TranslaterDate(TypeTargetForOracleDate),
                new TranslaterDecimal(),
                new TranslaterDoublePrecision(TypeTargetForOracleNumber),
                new TranslaterFloat(TypeTargetForOracleNumber),
                new TranslaterIntervalDayToSecond(),
                new TranslaterIntervalYearToMonth(),
                new TranslaterLong(),
                new TranslaterLongRaw(),
                new TranslaterNatural(),
                new TranslaterNaturaln(),
                new TranslaterNchar(),
                new TranslaterNclob(TypeTargetForOracleClob),
                new TranslaterNumber(TypeTargetForOracleNumber),
                new TranslaterNumeric(),
                new TranslaterNvarchar2(),
                new TranslaterPlsInteger(),
                new TranslaterPlsqlBoolean(),
                new TranslaterPositive(),
                new TranslaterPositiven(),
                new TranslaterRaw(),
                new TranslaterReal(TypeTargetForOracleNumber),
                new TranslaterRef(),
                new TranslaterRowId(),
                new TranslaterSmallint(TypeTargetForOracleInteger),
                new TranslaterString(),
                new TranslaterTimestamp(TypeTargetForOracleTimestamp),
                new TranslaterTimestampTZ(TypeTargetForOracleTimestampTZ),
                new TranslaterTimestampLTZ(TypeTargetForOracleTimestampLTZ),
                new TranslaterURowId(),
                new TranslaterVarchar(),
                new TranslaterVarchar2(),
                new TranslaterXmltype(),

                new TranslaterProcedureReturn()
            };
            TypesInitializedCount = OracleTypeTranslaters.Count;
        }

        internal static void Initialize(IParameterTranslation param) {
            TypeTargetForOracleRefCursor                        = param.TypeTargetForOracleRefCursor;
            TypeTargetForOracleAssociativeArray                 = param.TypeTargetForOracleAssociativeArray;
            TypeTargetForOracleInteger                          = param.TypeTargetForOracleInteger;
            TypeTargetForOracleNumber                           = param.TypeTargetForOracleNumber;
            IsConvertOracleNumberToIntegerIfColumnNameIsId      = param.IsConvertOracleNumberToIntegerIfColumnNameIsId;
            TypeTargetForOracleDate                             = param.TypeTargetForOracleDate;
            TypeTargetForOracleTimestamp                        = param.TypeTargetForOracleTimestamp;
            TypeTargetForOracleTimestampTZ                      = param.TypeTargetForOracleTimestampTZ;
            TypeTargetForOracleTimestampLTZ                     = param.TypeTargetForOracleTimestampLTZ;
            CSharpTypeUsedForOracleIntervalDayToSecond          = param.CSharpTypeUsedForOracleIntervalDayToSecond;
            TypeTargetForOracleBlob                             = param.TypeTargetForOracleBlob;
            TypeTargetForOracleClob                             = param.TypeTargetForOracleClob;

            InitTypeTranslaters();
        }

        #region Properties for Custom Type Translation
        private static CS.TypeCollection TypeTargetForOracleRefCursor { get; set; }
        private static CS.TypeCollection TypeTargetForOracleAssociativeArray { get; set; }
        private static CS.TypeValue TypeTargetForOracleInteger { get; set; }
        private static CS.TypeValue TypeTargetForOracleNumber { get; set; }
        private static bool IsConvertOracleNumberToIntegerIfColumnNameIsId;
        private static CS.TypeValue TypeTargetForOracleDate { get; set; }
        private static CS.TypeValue TypeTargetForOracleTimestamp { get; set; }
        private static CS.TypeValue TypeTargetForOracleTimestampTZ { get; set; }
        private static CS.TypeValue TypeTargetForOracleTimestampLTZ { get; set; }
        private static string CSharpTypeUsedForOracleIntervalDayToSecond { get; set; }
        private static CS.ITypeTargetable TypeTargetForOracleBlob { get; set; }
        private static CS.TypeReference TypeTargetForOracleClob { get; set; }
        #endregion

        internal static ITranslaterType GetTranslater(ITyped dataType) {
            string dataTypeFull = String.Empty;

            if (OracleTypeTranslaters.Any(t => t.IsValid(dataType))) {  
                dataTypeFull = OracleTypeTranslaters.First(t => t.IsValid(dataType)).DataTypeFull;
            } else {
                dataTypeFull = dataType.OrclType.BuildDataTypeFullName(dataType);
                switch (OrclUtil.NormalizeDataType(dataType)) { // dynamically create custom type translaters for complex types
                    case Orcl.ASSOCIATITVE_ARRAY:
                        OracleTypeTranslaters.Add(new TranslaterAssociativeArray(dataTypeFull, TypeTargetForOracleAssociativeArray, dataType));
                        break;
                    case Orcl.REF_CURSOR:
                        if (dataType.SubType == null)
                            OracleTypeTranslaters.Add(new TranslaterRefCursorUntyped(dataTypeFull, TypeTargetForOracleRefCursor, dataType));
                        else
                            OracleTypeTranslaters.Add(new TranslaterRefCursorTyped(dataTypeFull, TypeTargetForOracleRefCursor, dataType));
                        break;
                    case Orcl.OBJECT:
                        OracleTypeTranslaters.Add(new TranslaterObjectType(dataTypeFull));
                        break;
                    case Orcl.RECORD:
                        OracleTypeTranslaters.Add(new TranslaterRecordType(dataTypeFull));
                        break;
                    case Orcl.ROWTYPE:
                        OracleTypeTranslaters.Add(new TranslaterRowtype(dataTypeFull));
                        break;
                    case Orcl.NESTED_TABLE:
                        OracleTypeTranslaters.Add(new TranslaterNestedTable(dataTypeFull, TypeTargetForOracleAssociativeArray, dataType));
                        break;
                    case Orcl.VARRAY:
                        OracleTypeTranslaters.Add(new TranslaterVarray(dataTypeFull, TypeTargetForOracleAssociativeArray, dataType));
                        break;
                    default:
                        OracleTypeTranslaters.Add(new TranslaterNoneType(dataTypeFull));
                        break;
                }
            }

            return OracleTypeTranslaters.First(t => t.DataTypeFull.Equals(dataTypeFull));
        }

        internal static ITranslaterType GetTranslaterProcedureReturn() { return OracleTypeTranslaters.First(t => t is TranslaterProcedureReturn); }
        internal static long FactoryCountCustom { get => OracleTypeTranslaters.Count - TypesInitializedCount; }
        internal static long FactoryCountStandard { get => TypesInitializedCount; }
    }
}