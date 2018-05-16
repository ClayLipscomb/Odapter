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

//#define SHORT_INTEGER             // INTEGER as Int32
//#define DECIMAL_INTEGER           // INTEGER as Decimal

//#define SAFETYPE_INTEGER          // INTEGER as safe type OracleDecimal
//#define SAFETYPE_NUMBER           // NUMBER as safe type OracleDecimal
//#define SAFETYPE_DATE             // DATE as safe type OracleDate
//#define SAFETYPE_TIMESTAMP        // TIMESTAMP as safe type OracleTimeStamp

//#define SAFETYPE_INTERVAL         // INTERVAL DAY TO SECOND as safe type OracleIntervalDS

#define FILTER_PREFIX
#define DYNAMIC_MAPPING_FOR_TYPED_CURSOR
#define SEED_TABLES
//#define CSHARP30
#define EXAMPLE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
#if CSHARP30
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
#else
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
#endif
#if EXAMPLE
using Odapter.Example;
#endif
using Schema.Odpt.Package;
using Schema.Odpt.Table;
using Schema.Odpt.Type.Object;
using Schema.Odpt.View;

namespace Odapter.Tester {
    class Program {
        static void Main(string[] args) {
            Tester test = new Tester();
            test.Run();
        }
        
        public class Tester {
            private const uint TABLE_BIG_ROWS_TO_INSERT = 5000;
            private const uint TABLE_BIG_ROWS_TO_RETRIEVE = 500;
            //private const uint TABLE_NUMBER_ROWS_TO_INSERT = 5000;

            private const int MAX_STRING_SIZE_FOR_VARCHAR = 
#if CSHARP30
                5    // >= 4096 overflows against XE with managed
#else
                8191    // >= 8192 overflows against XE with managed 
#endif
                ; // >= 8192 overflows against XE with managed
            private const int MAX_STRING_SIZE_FOR_VARCHAR_IN_ASSOC_ARRAY = 4000;
            private const int MAX_STRING_SIZE_FOR_NVARCHAR_IN_ASSOC_ARRAY = 1000;
            private const int MAX_STRING_SIZE_FOR_CHAR = 2000;
            private const int MAX_STRING_SIZE_FOR_CHAR_IN_ASSOC_ARRAY = 2000;
            private const int MAX_STRING_SIZE_FOR_NCHAR_IN_ASSOC_ARRAY = 500;

            private const int MAX_STRING_SIZE_FOR_VARCHAR_COL = 4000;
            private const int MAX_STRING_SIZE_FOR_NVARCHAR_COL = 2000;
            private const int MAX_STRING_SIZE_FOR_CHAR_COL = 2000;
            private const int MAX_STRING_SIZE_FOR_NCHAR_COL = 1000;

            private const Decimal MAX_DECIMAL =  792281625142643375935439.5033M;
            private const Decimal MIN_DECIMAL = -792281625142643375935439.5033M;
                                               //792281625142643375935439 50335
            //private const Decimal MAX_ORACLE_DECIMAL = 792281625142643375935439.5033M;
            //private const Decimal MIN_ORACLE_DECIMAL = -792281625142643375935439.5033M;
            //private readonly OracleDecimal MAX_ORACLE_DECIMAL = new OracleDecimal("999999999999999999999999999999999999990000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
                                                                                // 999999999999999999999999999999999999990000000000000000000000000000000000000000000000000000000000000000000000000000000000000000

            public void Run() {
#if SEED_TABLES
                SeedTableBig();
                SeedTableNumber();
#endif
#if EXAMPLE
                (new Odapter.Example.Example()).Test();
#endif
                TestStringCalls();

                TestInt32Calls();
                TestInt64Calls();
                TestDecimalCalls();
                TestSingleCalls();
                TestDoubleCalls();

                TestDateCalls();
                TestTimeSpanCalls();

                TestCursorTypedTableBig();
                TestCursorUntypedTableBig();
                TestCursorTypedTableNumber();
                TestCursorUntypedTableNumber();

                TestCursorFilteredPackageTableBig();

                TestNoParamCalls();
                TestOptionalParamCalls();
                CompileTimeChecks();

                //Console.WriteLine( "Complete." );
                //Console.ReadLine();
            }

#region Seeding Methods
            public void SeedTableBig() {
                OdptPkgTableBig.Instance.TruncTable(null);

                // misc values
                OdptPkgTableBig.Instance.InsertRow(0, 0, 0, 0, 0.0M, 0.0M, 0.0M, Single.NaN, Double.NaN, "", "", "", "", "", "", "", "", "", "", null, null, null, null, null);

                // all null values
                OdptPkgTableBig.Instance.InsertRow(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

                // min values for INTEGER, NUMBER, DATE, TIMESTAMP
                OdptPkgTableBig.Instance.InsertRow(
#if SAFETYPE_INTEGER
                    OracleDecimal.MinValue, OracleDecimal.Truncate(OracleDecimal.MinValue, 0), OracleDecimal.Truncate(OracleDecimal.MinValue, 0), OracleDecimal.Truncate(OracleDecimal.MinValue, 0),
#elif DECIMAL_INTEGER
                    MIN_DECIMAL, MIN_DECIMAL, MIN_DECIMAL, MIN_DECIMAL,
#elif SHORT_INTEGER
                    Int32.MinValue, Int32.MinValue, Int32.MinValue, Int32.MinValue,
#else
                    Int64.MinValue, Int64.MinValue, Int64.MinValue, Int64.MinValue,
#endif
#if !SAFETYPE_NUMBER
                    MIN_DECIMAL, MIN_DECIMAL, MIN_DECIMAL,
#else
                    OracleDecimal.MinValue, OracleDecimal.MinValue, OracleDecimal.MinValue, 
#endif
                    Single.MinValue, Double.MinValue, 
                    "A", new string('?', MAX_STRING_SIZE_FOR_VARCHAR_COL), "B", new string('?', MAX_STRING_SIZE_FOR_VARCHAR_COL), "C", new string('?', MAX_STRING_SIZE_FOR_NVARCHAR_COL),
                    "D", new string('?', MAX_STRING_SIZE_FOR_CHAR_COL), "E", new string('?', MAX_STRING_SIZE_FOR_NCHAR_COL),
#if !SAFETYPE_DATE
                    DateTime.MinValue,
#else
                    OracleDate.MinValue, 
#endif
#if !SAFETYPE_TIMESTAMP
                    DateTime.MinValue, 
#else
                    OracleTimeStamp.MinValue.AddMilliseconds(1), 
#endif
#if !SAFETYPE_TIMESTAMP
                    DateTime.MinValue, 
#else
                    OracleTimeStamp.MinValue.AddMilliseconds(1), 
#endif
#if !SAFETYPE_TIMESTAMP
                    DateTime.MinValue, 
#else
                    OracleTimeStamp.MinValue.AddMilliseconds(1), 
#endif

                    null);

                // max values for INTEGER, NUMBER, DATE, TIMESTAMP
                OdptPkgTableBig.Instance.InsertRow(
#if SAFETYPE_INTEGER
                    OracleDecimal.MaxValue, OracleDecimal.Truncate(OracleDecimal.MaxValue, 0), OracleDecimal.Truncate(OracleDecimal.MaxValue, 0), OracleDecimal.Truncate(OracleDecimal.MaxValue, 0),
#elif DECIMAL_INTEGER
                    MAX_DECIMAL, MAX_DECIMAL, MAX_DECIMAL, MAX_DECIMAL,
#elif SHORT_INTEGER
                    Int32.MaxValue, Int32.MaxValue, Int32.MaxValue, Int32.MaxValue,
#else
                    Int64.MaxValue, Int64.MaxValue, Int64.MaxValue, Int64.MaxValue,
#endif

#if !SAFETYPE_NUMBER
                    MAX_DECIMAL, MAX_DECIMAL, MAX_DECIMAL,
#else
                    OracleDecimal.MaxValue, OracleDecimal.MaxValue, OracleDecimal.MaxValue,
#endif
                    Single.MaxValue, Double.MaxValue, 
                    "A", new string('?', MAX_STRING_SIZE_FOR_VARCHAR_COL), "B", new string('?', MAX_STRING_SIZE_FOR_VARCHAR_COL), "C", new string('?', MAX_STRING_SIZE_FOR_NVARCHAR_COL),
                    "D", new string('?', MAX_STRING_SIZE_FOR_CHAR_COL), "E", new string('?', MAX_STRING_SIZE_FOR_NCHAR_COL),
#if !SAFETYPE_DATE
                    DateTime.MaxValue, 
#else
                    OracleDate.MaxValue, 
#endif
#if !SAFETYPE_TIMESTAMP
                    DateTime.MaxValue.AddMilliseconds(-1), /* max value alone causes overflow when retrieved into .NET */
#else
                    OracleTimeStamp.MaxValue.AddMilliseconds(-1),  /* max value alone causes overflow when retrieved into .NET */ 
#endif
#if !SAFETYPE_TIMESTAMP
                    DateTime.MaxValue.AddSeconds(-1), /* max value alone causes overflow when retrieved into .NET TimeStamp(0) has no milliseconds */
#else
                    OracleTimeStamp.MaxValue.AddSeconds(-1),  /* max value alone causes overflow when retrieved into .NET , TimeStamp(0) has no milliseconds */
#endif
#if !SAFETYPE_TIMESTAMP
                    DateTime.MaxValue.AddMilliseconds(-1), /* max value alone causes overflow when retrieved into .NET */
#else
                    OracleTimeStamp.MaxValue.AddMilliseconds(-1),  /* max value alone causes overflow when retrieved into .NET */
#endif
                    null);

                for (int i = 0; i < TABLE_BIG_ROWS_TO_INSERT; i++) {
                    OdptPkgTableBig.Instance.InsertRow(i + 1, i + 2, i + 3, i + 4,
                        i + 5, i + 6, i + 7, i + 8, i + 9,
                        "A", new string('?', MAX_STRING_SIZE_FOR_VARCHAR_COL), "B", new string('?', MAX_STRING_SIZE_FOR_VARCHAR_COL), "C", new string('?', MAX_STRING_SIZE_FOR_NVARCHAR_COL),
                        "D", new string('?', MAX_STRING_SIZE_FOR_CHAR_COL), "E", new string('?', MAX_STRING_SIZE_FOR_NCHAR_COL),
#if !SAFETYPE_DATE
                        DateTime.Today.AddDays(i), 
#else
                        new OracleDate(OracleDate.GetSysDate().Value.AddDays(i)), 
#endif
#if !SAFETYPE_TIMESTAMP
                        DateTime.Today.AddDays(i), 
#else
                        OracleTimeStamp.GetSysDate().AddDays(i), 
#endif
#if !SAFETYPE_TIMESTAMP
                        DateTime.Today.AddDays(i), 
#else
                        OracleTimeStamp.GetSysDate().AddDays(i), 
#endif
#if !SAFETYPE_TIMESTAMP
                        DateTime.Today.AddDays(i), 
#else
                        OracleTimeStamp.GetSysDate().AddDays(i), 
#endif
                        null);
                }
            }

            public void SeedTableNumber() {
                OdptPkgTableNumber.Instance.TruncTable(null);

                // all zero values
                OdptPkgTableNumber.Instance.InsertRow(  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, null);
                // all null values
                OdptPkgTableNumber.Instance.InsertRow(  null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 
                                                        null, null, null, null, null, null, null, null, null, null, null, null, null);
                // min values 
                OdptPkgTableNumber.Instance.InsertRow(  MIN_DECIMAL, -9, -99, -999, -9999, -99999, 
                                                        -Convert.ToInt32(new string('9', 6)), -Convert.ToInt32(new string('9', 7)), -Convert.ToInt32(new string('9', 8)), -Convert.ToInt32(new string('9', 9)),

#if SAFETYPE_INTEGER
                                                        -new OracleDecimal(new string('9', 10)), -new OracleDecimal(new string('9', 18)), -new OracleDecimal(new string('9', 19)), -new OracleDecimal(new string('9', 38)),
#elif DECIMAL_INTEGER
                                                        -Convert.ToDecimal(new string('9', 10)), -Convert.ToDecimal(new string('9', 18)), -Convert.ToDecimal(new string('9', 19)), MIN_DECIMAL,
#elif SHORT_INTEGER
                                                        Int32.MinValue, Int32.MinValue, Int32.MinValue, Int32.MinValue,
#else
                                                        -Convert.ToInt64(new string('9', 10)), -Convert.ToInt64(new string('9', 18)), Int64.MinValue, Int64.MinValue,
#endif
                                                        -9, -99, -999, -9999, -99999, -Convert.ToInt32(new string('9', 9)),

#if SAFETYPE_INTEGER
                                                        -new OracleDecimal(new string('9', 10)), -new OracleDecimal(new string('9', 18)), -new OracleDecimal(new string('9', 19)), -new OracleDecimal(new string('9', 28)), -new OracleDecimal(new string('9', 29)), -new OracleDecimal(new string('9', 38)),
#elif DECIMAL_INTEGER
                                                        -Convert.ToDecimal(new string('9', 10)), -Convert.ToDecimal(new string('9', 18)), -Convert.ToDecimal(new string('9', 19)), MIN_DECIMAL, MIN_DECIMAL, MIN_DECIMAL,
#elif SHORT_INTEGER
                                                         Int32.MinValue, Int32.MinValue, Int32.MinValue, Int32.MinValue, Int32.MinValue, Int32.MinValue,
#else
                                                        -Convert.ToInt64(new string('9', 10)), -Convert.ToInt64(new string('9', 18)), Int64.MinValue, Int64.MinValue, Int64.MinValue, Int64.MinValue,
#endif
                                                        -9.9M, -99.999M, -Convert.ToDecimal(new string('9', 4) + "." + new string('9', 11)), /* 15.11 */ -Convert.ToDecimal(new string('9', 10) + "." + new string('8', 21)), /* 31.21 */ 
                                                        -Convert.ToDecimal(new string('9', 1) + "." + new string('8', 20)), /* 38.37 */ MIN_DECIMAL, null);

                // max values 
                OdptPkgTableNumber.Instance.InsertRow( MAX_DECIMAL, 9, 99, 999, 9999, 99999,
                                                        Convert.ToInt32(new string('9', 6)), Convert.ToInt32(new string('9', 7)), Convert.ToInt32(new string('9', 8)), Convert.ToInt32(new string('9', 9)),
#if SAFETYPE_INTEGER
                                                        new OracleDecimal(new string('9', 10)), new OracleDecimal(new string('9', 18)), new OracleDecimal(new string('9', 19)), new OracleDecimal(new string('9', 38)),
#elif DECIMAL_INTEGER
                                                        Convert.ToDecimal(new string('9', 10)), Convert.ToDecimal(new string('9', 18)), Convert.ToDecimal(new string('9', 19)), MAX_DECIMAL,
#elif SHORT_INTEGER
                                                        Int32.MaxValue, Int32.MaxValue, Int32.MaxValue, Int32.MaxValue,
#else
                                                        Convert.ToInt64(new string('9', 10)), Convert.ToInt64(new string('9', 18)), Int64.MaxValue, Int64.MaxValue,
#endif
                                                        9, 99, 999, 9999, 99999, Convert.ToInt32(new string('9', 9)),
#if SAFETYPE_INTEGER
                                                        new OracleDecimal(new string('9', 10)), new OracleDecimal(new string('9', 18)), new OracleDecimal(new string('9', 19)), new OracleDecimal(new string('9', 28)), new OracleDecimal(new string('9', 29)), new OracleDecimal(new string('9', 38)),
#elif DECIMAL_INTEGER
                                                        Convert.ToDecimal(new string('9', 10)), Convert.ToDecimal(new string('9', 18)), Convert.ToDecimal(new string('9', 19)), MAX_DECIMAL, MAX_DECIMAL, MAX_DECIMAL,
#elif SHORT_INTEGER
                                                        Int32.MaxValue, Int32.MaxValue, Int32.MaxValue, Int32.MaxValue, Int32.MaxValue, Int32.MaxValue,
#else
                                                        Convert.ToInt64(new string('9', 10)), Convert.ToInt64(new string('9', 18)), Int64.MaxValue, Int64.MaxValue, Int64.MaxValue, Int64.MaxValue,
#endif
                                                        9.9M, 99.999M, Convert.ToDecimal(new string('9', 4) + "." + new string('9', 11)), /* 15.11 */ Convert.ToDecimal(new string('9', 10) + "." + new string('8', 21)), /* 31.21 */ 
                                                        Convert.ToDecimal(new string('9', 1) + "." + new string('8', 20)), /* 38.37 */ MAX_DECIMAL, null);
            }
#endregion Seeding

#region Cursors
            //public List<T> TestCursorTypedStaticMappingRet<T>()
            //    where T : class, OdptPkgTableBig.ITTableBig, new() {
            //    uint? rowLimit = TABLE_BIG_ROWS_TO_RETRIEVE;

            //    List<T> retTableBigList = OdptPkgTableBig.Instance.GetRowsTypedRet<T>(rowLimit, null);
            //    return retTableBigList;
            //}

            public void TestCursorFilteredPackageTableBig() {
                uint? rowLimit = TABLE_BIG_ROWS_TO_RETRIEVE;
#if FILTER_PREFIX
                List<OdptPkgTableBig.FilteredPkgTTableBigFiltered> retList, outList, outList2;
#else
                List<FilteredPkg.TTableBigFiltered> retList, outList, outList2;
#endif
                DataTable retDataTable, outDataTable, outDataTable2;

                retList = OdptPkgTableBig.Instance.GetRowsTypedFilteredPkg<
#if FILTER_PREFIX
                    OdptPkgTableBig.FilteredPkgTTableBigFiltered
#else
                    FilteredPkg.TTableBigFiltered
#endif
                        >(out outList, out outList2, rowLimit, null);
                Debug.Assert(retList.Count == rowLimit);
                Debug.Assert(outList.Count == rowLimit);
                Debug.Assert(outList2.Count == rowLimit);

                retList = OdptPkgTableBig.Instance.GetRowsUntypedFilteredPkg<
#if FILTER_PREFIX
                    OdptPkgTableBig.FilteredPkgTTableBigFiltered, OdptPkgTableBig.FilteredPkgTTableBigFiltered, OdptPkgTableBig.FilteredPkgTTableBigFiltered
#else
                    FilteredPkg.TTableBigFiltered, FilteredPkg.TTableBigFiltered, FilteredPkg.TTableBigFiltered
#endif
                        >(out outList, out outList2, false, false, rowLimit, null);
                Debug.Assert(retList.Count == rowLimit);
                Debug.Assert(outList.Count == rowLimit);
                Debug.Assert(outList2.Count == rowLimit);

                retDataTable = OdptPkgTableBig.Instance.GetRowsUntypedFilteredPkg(out outDataTable, out outDataTable2, true, rowLimit, null);
                Debug.Assert(retDataTable.Rows.Count == rowLimit);
                Debug.Assert(outDataTable.Rows.Count == rowLimit);
                Debug.Assert(outDataTable2.Rows.Count == rowLimit);
            }

            public void TestCursorTypedTableBig() {
                uint? rowLimit = TABLE_BIG_ROWS_TO_RETRIEVE;
                List<OdptPkgTableBig.TTableBig> retTableBigList, outTableBigList;
                List<OdptPkgTableBig.TTableBigChar> outTableBigCharList;

                // static mapping
                // ret
                retTableBigList = OdptPkgTableBig.Instance.GetRowsTypedRet<OdptPkgTableBig.TTableBig>(rowLimit, null);
                Debug.Assert(retTableBigList.Count == rowLimit);

                // 1 out
                OdptPkgTableBig.Instance.GetRowsTypedOut<OdptPkgTableBig.TTableBig>(out outTableBigList, rowLimit, null);
                Debug.Assert(outTableBigList.Count == rowLimit);

                // ret and 1 out
                retTableBigList = OdptPkgTableBig.Instance.GetRowsTypedOutRet<OdptPkgTableBig.TTableBig>(out outTableBigList, rowLimit, null);
                Debug.Assert(retTableBigList.Count == rowLimit && outTableBigList.Count == rowLimit);

                // ret and 2 out
                retTableBigList = OdptPkgTableBig.Instance.GetRowsTypedOut2Ret<OdptPkgTableBig.TTableBig, OdptPkgTableBig.TTableBigChar>(out outTableBigList, out outTableBigCharList, rowLimit, null);
                Debug.Assert(retTableBigList.Count == rowLimit && outTableBigList.Count == rowLimit && outTableBigCharList.Count == rowLimit);

#if DYNAMIC_MAPPING_FOR_TYPED_CURSOR
                // dynamic mapping
                List<TTableBigMapByPositionAll> retTableBigMapByPositionAllList, outTableBigMapByPositionAllList;
                List<TTableBigMapByPositionPartial> retTableBigMapByPositionPartialList, outTableBigMapByPositionPartialList;
                List<TTableBigCharMapByPositionAll> outTableBigCharMapByPositionAllList;
                List<TTableBigCharMapByPositionPartial> outTableBigCharMapByPositionPartialList;

                // list ret - dynamic mapping by name
                retTableBigList = OdptPkgTableBig.Instance.GetRowsTypedRet<OdptPkgTableBig.TTableBig>(false, false, rowLimit, null);

                // list ret - dynamic mapping by position, NOT allowing unmapped columns
                retTableBigMapByPositionAllList = OdptPkgTableBig.Instance.GetRowsTypedRet<TTableBigMapByPositionAll>(true, false, rowLimit, null);

                // list ret - dynamic mapping by position, allowing unmapped columns
                retTableBigMapByPositionPartialList = OdptPkgTableBig.Instance.GetRowsTypedRet<TTableBigMapByPositionPartial>(true, true, rowLimit, null);
                Debug.Assert(retTableBigList.Count == rowLimit && retTableBigMapByPositionAllList.Count == rowLimit && retTableBigMapByPositionPartialList.Count == rowLimit);

                // all above cases using 1 out
                outTableBigList = null; outTableBigMapByPositionAllList = null; outTableBigMapByPositionPartialList = null;
                OdptPkgTableBig.Instance.GetRowsTypedOut<OdptPkgTableBig.TTableBig>(out outTableBigList, false, false, rowLimit, null);
                OdptPkgTableBig.Instance.GetRowsTypedOut<TTableBigMapByPositionAll>(out outTableBigMapByPositionAllList, true, false, rowLimit, null);
                OdptPkgTableBig.Instance.GetRowsTypedOut<TTableBigMapByPositionPartial>(out outTableBigMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(outTableBigList.Count == rowLimit && outTableBigMapByPositionAllList.Count == rowLimit && outTableBigMapByPositionPartialList.Count == rowLimit);

                // all above cases using ret and 1 out arg
                outTableBigList = null; outTableBigMapByPositionAllList = null; outTableBigMapByPositionPartialList = null;
                retTableBigList = OdptPkgTableBig.Instance.GetRowsTypedOutRet<OdptPkgTableBig.TTableBig>(out outTableBigList, false, false, rowLimit, null);
                retTableBigMapByPositionAllList = OdptPkgTableBig.Instance.GetRowsTypedOutRet<TTableBigMapByPositionAll>(out outTableBigMapByPositionAllList, true, false, rowLimit, null);
                retTableBigMapByPositionPartialList = OdptPkgTableBig.Instance.GetRowsTypedOutRet<TTableBigMapByPositionPartial>(out outTableBigMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(retTableBigList.Count == rowLimit && retTableBigMapByPositionAllList.Count == rowLimit && retTableBigMapByPositionPartialList.Count == rowLimit);
                Debug.Assert(outTableBigList.Count == rowLimit && outTableBigMapByPositionAllList.Count == rowLimit && outTableBigMapByPositionPartialList.Count == rowLimit);

                // all above cases using ret and 2 out args 
                outTableBigList = null; outTableBigMapByPositionAllList = null; outTableBigMapByPositionPartialList = null;
                retTableBigList = OdptPkgTableBig.Instance.GetRowsTypedOut2Ret<OdptPkgTableBig.TTableBig, OdptPkgTableBig.TTableBigChar>(out outTableBigList, out outTableBigCharList, false, false, rowLimit, null);
                retTableBigMapByPositionAllList = OdptPkgTableBig.Instance.GetRowsTypedOut2Ret<TTableBigMapByPositionAll, TTableBigCharMapByPositionAll>(out outTableBigMapByPositionAllList, out outTableBigCharMapByPositionAllList, true, false, rowLimit, null);
                retTableBigMapByPositionPartialList = OdptPkgTableBig.Instance.GetRowsTypedOut2Ret<TTableBigMapByPositionPartial, TTableBigCharMapByPositionPartial>(out outTableBigMapByPositionPartialList, out outTableBigCharMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(retTableBigList.Count == rowLimit && retTableBigMapByPositionAllList.Count == rowLimit && retTableBigMapByPositionPartialList.Count == rowLimit);
                Debug.Assert(outTableBigList.Count == rowLimit && outTableBigMapByPositionAllList.Count == rowLimit && outTableBigMapByPositionPartialList.Count == rowLimit);
                Debug.Assert(outTableBigCharList.Count == rowLimit && outTableBigCharMapByPositionAllList.Count == rowLimit && outTableBigCharMapByPositionPartialList.Count == rowLimit);
#endif
            }

            public void TestCursorTypedTableNumber() {
                uint? rowLimit = null;
                List<OdptPkgTableNumber.TTableNumber> retTableNumberList, outTableNumberList;
                List<OdptPkgTableNumber.TTableNumberDec> outTableNumberDecList;

                // static mapping
                // ret
                retTableNumberList = OdptPkgTableNumber.Instance.GetRowsTypedRet<OdptPkgTableNumber.TTableNumber>(rowLimit, null);
                Debug.Assert(retTableNumberList.Count > 0);

                // 1 out
                OdptPkgTableNumber.Instance.GetRowsTypedOut<OdptPkgTableNumber.TTableNumber>(out outTableNumberList, rowLimit, null);
                Debug.Assert(outTableNumberList.Count > 0);

                // ret and 1 out
                retTableNumberList = OdptPkgTableNumber.Instance.GetRowsTypedOutRet<OdptPkgTableNumber.TTableNumber>(out outTableNumberList, rowLimit, null);
                Debug.Assert(retTableNumberList.Count > 0 && outTableNumberList.Count > 0);

                // ret and 2 out
                retTableNumberList = OdptPkgTableNumber.Instance.GetRowsTypedOut2Ret<OdptPkgTableNumber.TTableNumber, OdptPkgTableNumber.TTableNumberDec>(out outTableNumberList, out outTableNumberDecList, rowLimit, null);
                Debug.Assert(retTableNumberList.Count > 0 && outTableNumberList.Count > 0 && outTableNumberDecList.Count > 0);

#if DYNAMIC_MAPPING_FOR_TYPED_CURSOR
                // dynamic mapping
                List<TTableNumberMapByPositionAll> retTableNumberMapByPositionAllList, outTableNumberMapByPositionAllList;
                List<TTableNumberMapByPositionPartial> retTableNumberMapByPositionPartialList, outTableNumberMapByPositionPartialList;
                List<TTableNumberDecMapByPositionAll> outTableNumberDecMapByPositionAllList;
                List<TTableNumberDecMapByPositionPartial> outTableNumberDecMapByPositionPartialList;

                // list ret - dynamic mapping by name
                retTableNumberList = OdptPkgTableNumber.Instance.GetRowsTypedRet<OdptPkgTableNumber.TTableNumber>(false, false, rowLimit, null);

                // list ret - dynamic mapping by position, NOT allowing unmapped columns
                retTableNumberMapByPositionAllList = OdptPkgTableNumber.Instance.GetRowsTypedRet<TTableNumberMapByPositionAll>(true, false, rowLimit, null);

                // list ret - dynamic mapping by position, allowing unmapped columns
                retTableNumberMapByPositionPartialList = OdptPkgTableNumber.Instance.GetRowsTypedRet<TTableNumberMapByPositionPartial>(true, true, rowLimit, null);
                Debug.Assert(retTableNumberList.Count > 0 && retTableNumberMapByPositionAllList.Count > 0 && retTableNumberMapByPositionPartialList.Count > 0);

                // all above cases using 1 out
                outTableNumberList = null; outTableNumberMapByPositionAllList = null; outTableNumberMapByPositionPartialList = null;
                OdptPkgTableNumber.Instance.GetRowsTypedOut<OdptPkgTableNumber.TTableNumber>(out outTableNumberList, false, false, rowLimit, null);
                OdptPkgTableNumber.Instance.GetRowsTypedOut<TTableNumberMapByPositionAll>(out outTableNumberMapByPositionAllList, true, false, rowLimit, null);
                OdptPkgTableNumber.Instance.GetRowsTypedOut<TTableNumberMapByPositionPartial>(out outTableNumberMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(outTableNumberList.Count > 0 && outTableNumberMapByPositionAllList.Count > 0 && outTableNumberMapByPositionPartialList.Count > 0);

                // all above cases using ret and 1 out arg
                outTableNumberList = null; outTableNumberMapByPositionAllList = null; outTableNumberMapByPositionPartialList = null;
                retTableNumberList = OdptPkgTableNumber.Instance.GetRowsTypedOutRet<OdptPkgTableNumber.TTableNumber>(out outTableNumberList, false, false, rowLimit, null);
                retTableNumberMapByPositionAllList = OdptPkgTableNumber.Instance.GetRowsTypedOutRet<TTableNumberMapByPositionAll>(out outTableNumberMapByPositionAllList, true, false, rowLimit, null);
                retTableNumberMapByPositionPartialList = OdptPkgTableNumber.Instance.GetRowsTypedOutRet<TTableNumberMapByPositionPartial>(out outTableNumberMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(retTableNumberList.Count > 0 && retTableNumberMapByPositionAllList.Count > 0 && retTableNumberMapByPositionPartialList.Count > 0);
                Debug.Assert(outTableNumberList.Count > 0 && outTableNumberMapByPositionAllList.Count > 0 && outTableNumberMapByPositionPartialList.Count > 0);

                // all above cases using ret and 2 out args 
                outTableNumberList = null; outTableNumberMapByPositionAllList = null; outTableNumberMapByPositionPartialList = null;
                retTableNumberList = OdptPkgTableNumber.Instance.GetRowsTypedOut2Ret<OdptPkgTableNumber.TTableNumber, OdptPkgTableNumber.TTableNumberDec>(out outTableNumberList, out outTableNumberDecList, false, false, rowLimit, null);
                retTableNumberMapByPositionAllList = OdptPkgTableNumber.Instance.GetRowsTypedOut2Ret<TTableNumberMapByPositionAll, TTableNumberDecMapByPositionAll>(out outTableNumberMapByPositionAllList, out outTableNumberDecMapByPositionAllList, true, false, rowLimit, null);
                retTableNumberMapByPositionPartialList = OdptPkgTableNumber.Instance.GetRowsTypedOut2Ret<TTableNumberMapByPositionPartial, TTableNumberDecMapByPositionPartial>(out outTableNumberMapByPositionPartialList, out outTableNumberDecMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(retTableNumberList.Count > 0 && retTableNumberMapByPositionAllList.Count > 0 && retTableNumberMapByPositionPartialList.Count > 0);
                Debug.Assert(outTableNumberList.Count > 0 && outTableNumberMapByPositionAllList.Count > 0 && outTableNumberMapByPositionPartialList.Count > 0);
                Debug.Assert(outTableNumberDecList.Count > 0 && outTableNumberDecMapByPositionAllList.Count > 0 && outTableNumberDecMapByPositionPartialList.Count > 0);
#endif
            }

            public void TestCursorUntypedTableBig() {
                uint? rowLimit = TABLE_BIG_ROWS_TO_RETRIEVE;
                List<OdptPkgTableBig.TTableBig> retTableBigList, outTableBigList;
                List<TTableBigMapByPositionAll> retTableBigMapByPositionAllList, outTableBigMapByPositionAllList;
                List<TTableBigMapByPositionPartial> retTableBigMapByPositionPartialList, outTableBigMapByPositionPartialList;
                List<OdptPkgTableBig.TTableBigChar>      outTableBigCharList;
                List<TTableBigCharMapByPositionAll>      outTableBigCharMapByPositionAllList;
                List<TTableBigCharMapByPositionPartial>  outTableBigCharMapByPositionPartialList;
                DataTable retTableBigDataTable, outTableBigDataTable, outTableBigDataTable2;

                // DataTable ret
                retTableBigDataTable = OdptPkgTableBig.Instance.GetRowsUntypedRet(true, rowLimit, null);

                // list ret - dynamic mapping by name
                retTableBigList = OdptPkgTableBig.Instance.GetRowsUntypedRet<OdptPkgTableBig.TTableBig>(false, false, rowLimit, null);

                // list ret - dynamic mapping by position, NOT allowing unmapped columns
                retTableBigMapByPositionAllList = OdptPkgTableBig.Instance.GetRowsUntypedRet<TTableBigMapByPositionAll>(true, false, rowLimit, null);

                // list ret - dynamic mapping by position, allowing unmapped columns
                retTableBigMapByPositionPartialList = OdptPkgTableBig.Instance.GetRowsUntypedRet<TTableBigMapByPositionPartial>(true, true, rowLimit, null);
                Debug.Assert(retTableBigDataTable.Rows.Count == rowLimit && retTableBigList.Count == rowLimit && retTableBigMapByPositionAllList.Count == rowLimit && retTableBigMapByPositionPartialList.Count == rowLimit);

                // all above cases using 1 out
                outTableBigDataTable = null; outTableBigList = null; outTableBigMapByPositionAllList = null; outTableBigMapByPositionPartialList = null;
                OdptPkgTableBig.Instance.GetRowsUntypedOut(out outTableBigDataTable, true, rowLimit, null);
                OdptPkgTableBig.Instance.GetRowsUntypedOut<OdptPkgTableBig.TTableBig>(out outTableBigList, false, false, rowLimit, null);
                OdptPkgTableBig.Instance.GetRowsUntypedOut<TTableBigMapByPositionAll>(out outTableBigMapByPositionAllList, true, false, rowLimit, null);
                OdptPkgTableBig.Instance.GetRowsUntypedOut<TTableBigMapByPositionPartial>(out outTableBigMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(outTableBigDataTable.Rows.Count == rowLimit && outTableBigList.Count == rowLimit && outTableBigMapByPositionAllList.Count == rowLimit && outTableBigMapByPositionPartialList.Count == rowLimit);

                // all above cases using ret and 1 out arg
                outTableBigDataTable = null; outTableBigList = null; outTableBigMapByPositionAllList = null; outTableBigMapByPositionPartialList = null;
                retTableBigDataTable = OdptPkgTableBig.Instance.GetRowsUntypedOutRet(out outTableBigDataTable, true, rowLimit, null);
                retTableBigList = OdptPkgTableBig.Instance.GetRowsUntypedOutRet<OdptPkgTableBig.TTableBig, OdptPkgTableBig.TTableBig>(out outTableBigList, false, false, rowLimit, null);
                retTableBigMapByPositionAllList = OdptPkgTableBig.Instance.GetRowsUntypedOutRet<TTableBigMapByPositionAll, TTableBigMapByPositionAll>(out outTableBigMapByPositionAllList, true, false, rowLimit, null);
                retTableBigMapByPositionPartialList = OdptPkgTableBig.Instance.GetRowsUntypedOutRet<TTableBigMapByPositionPartial, TTableBigMapByPositionPartial>(out outTableBigMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(retTableBigDataTable.Rows.Count == rowLimit && retTableBigList.Count == rowLimit && retTableBigMapByPositionAllList.Count == rowLimit && retTableBigMapByPositionPartialList.Count == rowLimit);
                Debug.Assert(outTableBigDataTable.Rows.Count == rowLimit && outTableBigList.Count == rowLimit && outTableBigMapByPositionAllList.Count == rowLimit && outTableBigMapByPositionPartialList.Count == rowLimit);

                // all above cases using ret and 2 out args 
                outTableBigDataTable = null; outTableBigList = null; outTableBigMapByPositionAllList = null; outTableBigMapByPositionPartialList = null;
                retTableBigDataTable = OdptPkgTableBig.Instance.GetRowsUntypedOut2Ret(out outTableBigDataTable, out outTableBigDataTable2, true, rowLimit, null);
                retTableBigList = OdptPkgTableBig.Instance.GetRowsUntypedOut2Ret<OdptPkgTableBig.TTableBig, OdptPkgTableBig.TTableBig, OdptPkgTableBig.TTableBigChar>(out outTableBigList, out outTableBigCharList, false, false, rowLimit, null);
                retTableBigMapByPositionAllList = OdptPkgTableBig.Instance.GetRowsUntypedOut2Ret<TTableBigMapByPositionAll, TTableBigMapByPositionAll, TTableBigCharMapByPositionAll> (out outTableBigMapByPositionAllList, out outTableBigCharMapByPositionAllList, true, false, rowLimit, null);
                retTableBigMapByPositionPartialList = OdptPkgTableBig.Instance.GetRowsUntypedOut2Ret<TTableBigMapByPositionPartial, TTableBigMapByPositionPartial, TTableBigCharMapByPositionPartial> (out outTableBigMapByPositionPartialList, out outTableBigCharMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(retTableBigDataTable.Rows.Count == rowLimit && retTableBigList.Count == rowLimit && retTableBigMapByPositionAllList.Count == rowLimit && retTableBigMapByPositionPartialList.Count == rowLimit);
                Debug.Assert(outTableBigDataTable.Rows.Count == rowLimit && outTableBigList.Count == rowLimit && outTableBigMapByPositionAllList.Count == rowLimit && outTableBigMapByPositionPartialList.Count == rowLimit);
                Debug.Assert(outTableBigDataTable2.Rows.Count == rowLimit && outTableBigCharList.Count == rowLimit && outTableBigCharMapByPositionAllList.Count == rowLimit && outTableBigCharMapByPositionPartialList.Count == rowLimit);
            }

            public void TestCursorUntypedTableNumber() {
                uint? rowLimit = null;
                List<OdptPkgTableNumber.TTableNumber> retTableNumberList, outTableNumberList;
                List<TTableNumberMapByPositionAll> retTableNumberMapByPositionAllList, outTableNumberMapByPositionAllList;
                List<TTableNumberMapByPositionPartial> retTableNumberMapByPositionPartialList, outTableNumberMapByPositionPartialList;
                List<OdptPkgTableNumber.TTableNumberDec>      outTableNumberDecList;
                List<TTableNumberDecMapByPositionAll>      outTableNumberDecMapByPositionAllList;
                List<TTableNumberDecMapByPositionPartial>  outTableNumberDecMapByPositionPartialList;
                DataTable retTableNumberDataTable, outTableNumberDataTable, outTableNumberDataTable2;

                // DataTable ret
                retTableNumberDataTable = OdptPkgTableNumber.Instance.GetRowsUntypedRet(true, rowLimit, null);

                // list ret - dynamic mapping by name
                retTableNumberList = OdptPkgTableNumber.Instance.GetRowsUntypedRet<OdptPkgTableNumber.TTableNumber>(false, false, rowLimit, null);

                // list ret - dynamic mapping by position, NOT allowing unmapped columns
                retTableNumberMapByPositionAllList = OdptPkgTableNumber.Instance.GetRowsUntypedRet<TTableNumberMapByPositionAll>(true, false, rowLimit, null);

                // list ret - dynamic mapping by position, allowing unmapped columns
                retTableNumberMapByPositionPartialList = OdptPkgTableNumber.Instance.GetRowsUntypedRet<TTableNumberMapByPositionPartial>(true, true, rowLimit, null);
                Debug.Assert(retTableNumberDataTable.Rows.Count > 0 && retTableNumberList.Count > 0 && retTableNumberMapByPositionAllList.Count > 0 && retTableNumberMapByPositionPartialList.Count > 0);

                // all above cases using 1 out
                outTableNumberDataTable = null; outTableNumberList = null; outTableNumberMapByPositionAllList = null; outTableNumberMapByPositionPartialList = null;
                OdptPkgTableNumber.Instance.GetRowsUntypedOut(out outTableNumberDataTable, true, rowLimit, null);
                OdptPkgTableNumber.Instance.GetRowsUntypedOut<OdptPkgTableNumber.TTableNumber>(out outTableNumberList, false, false, rowLimit, null);
                OdptPkgTableNumber.Instance.GetRowsUntypedOut<TTableNumberMapByPositionAll>(out outTableNumberMapByPositionAllList, true, false, rowLimit, null);
                OdptPkgTableNumber.Instance.GetRowsUntypedOut<TTableNumberMapByPositionPartial>(out outTableNumberMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(outTableNumberDataTable.Rows.Count > 0 && outTableNumberList.Count > 0 && outTableNumberMapByPositionAllList.Count > 0 && outTableNumberMapByPositionPartialList.Count > 0);

                // all above cases using ret and 1 out arg
                outTableNumberDataTable = null; outTableNumberList = null; outTableNumberMapByPositionAllList = null; outTableNumberMapByPositionPartialList = null;
                retTableNumberDataTable = OdptPkgTableNumber.Instance.GetRowsUntypedOutRet(out outTableNumberDataTable, true, rowLimit, null);
                retTableNumberList = OdptPkgTableNumber.Instance.GetRowsUntypedOutRet<OdptPkgTableNumber.TTableNumber, OdptPkgTableNumber.TTableNumber>(out outTableNumberList, false, false, rowLimit, null);
                retTableNumberMapByPositionAllList = OdptPkgTableNumber.Instance.GetRowsUntypedOutRet<TTableNumberMapByPositionAll, TTableNumberMapByPositionAll>(out outTableNumberMapByPositionAllList, true, false, rowLimit, null);
                retTableNumberMapByPositionPartialList = OdptPkgTableNumber.Instance.GetRowsUntypedOutRet<TTableNumberMapByPositionPartial, TTableNumberMapByPositionPartial>(out outTableNumberMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(retTableNumberDataTable.Rows.Count > 0 && retTableNumberList.Count > 0 && retTableNumberMapByPositionAllList.Count > 0 && retTableNumberMapByPositionPartialList.Count > 0);
                Debug.Assert(outTableNumberDataTable.Rows.Count > 0 && outTableNumberList.Count > 0 && outTableNumberMapByPositionAllList.Count > 0 && outTableNumberMapByPositionPartialList.Count > 0);

                // all above cases using ret and 2 out args 
                outTableNumberDataTable = null; outTableNumberList = null; outTableNumberMapByPositionAllList = null; outTableNumberMapByPositionPartialList = null;
                retTableNumberDataTable = OdptPkgTableNumber.Instance.GetRowsUntypedOut2Ret(out outTableNumberDataTable, out outTableNumberDataTable2, true, rowLimit, null);
                retTableNumberList = OdptPkgTableNumber.Instance.GetRowsUntypedOut2Ret<OdptPkgTableNumber.TTableNumber, OdptPkgTableNumber.TTableNumber, OdptPkgTableNumber.TTableNumberDec>(out outTableNumberList, out outTableNumberDecList, false, false, rowLimit, null);
                retTableNumberMapByPositionAllList = OdptPkgTableNumber.Instance.GetRowsUntypedOut2Ret<TTableNumberMapByPositionAll, TTableNumberMapByPositionAll, TTableNumberDecMapByPositionAll> (out outTableNumberMapByPositionAllList, out outTableNumberDecMapByPositionAllList, true, false, rowLimit, null);
                retTableNumberMapByPositionPartialList = OdptPkgTableNumber.Instance.GetRowsUntypedOut2Ret<TTableNumberMapByPositionPartial, TTableNumberMapByPositionPartial, TTableNumberDecMapByPositionPartial> (out outTableNumberMapByPositionPartialList, out outTableNumberDecMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(retTableNumberDataTable.Rows.Count > 0 && retTableNumberList.Count > 0 && retTableNumberMapByPositionAllList.Count > 0 && retTableNumberMapByPositionPartialList.Count > 0);
                Debug.Assert(outTableNumberDataTable.Rows.Count > 0 && outTableNumberList.Count > 0 && outTableNumberMapByPositionAllList.Count > 0 && outTableNumberMapByPositionPartialList.Count > 0);
                Debug.Assert(outTableNumberDataTable2.Rows.Count > 0 && outTableNumberDecList.Count > 0 && outTableNumberDecMapByPositionAllList.Count > 0 && outTableNumberDecMapByPositionPartialList.Count > 0);
            }
#endregion

#region Tests for isolated data types (primitives, ODP.NET SafeTypes, etc.) and respective associative arrays
            /// <summary>
            /// Test use of Int32
            /// </summary>
            public void TestInt32Calls() {
                Int32? pInInt = 0, pInOutInt = 0, pOutInt, retInt;
                List<Int32?> pInListInt = new List<Int32?>(), pInOutListInt = new List<Int32?>(), pOutListInt = new List<Int32?>();
                List<Int32?> intTestValues = new List<Int32?>() { Int32.MaxValue, Int32.MinValue, 0, null };

                // BINARY_INTEGER and equivalents
                // BINARY_INTEGER
                foreach (Int32? it in intTestValues) {
                    pInInt = it; pInOutInt = null;
                    retInt = OdptPkgMain.Instance.FuncBinaryInteger(pInInt, ref pInOutInt, out pOutInt, null);
                    Debug.Assert(pInInt.Equals(pInOutInt) && pInInt.Equals(pOutInt) && pInInt.Equals(retInt));
                }

                // PLS_INTEGER
                foreach (Int32? it in intTestValues) {
                    pInInt = it; pInOutInt = null;
                    retInt = OdptPkgMain.Instance.FuncPlsInteger(pInInt, ref pInOutInt, out pOutInt, null);
                    Debug.Assert(pInInt.Equals(pInOutInt) && pInInt.Equals(pOutInt) && pInInt.Equals(retInt));
                }

                // binding an associative array of BINARY_INTEGER and PLS_INTEGER is not possible
            }

            /// <summary>
            /// Test use of Int64
            /// </summary>
            public void TestInt64Calls() {
#if SAFETYPE_INTEGER
                OracleDecimal? pIn, pInOut, pOut, ret;
                List<OracleDecimal?> pInList = new List<OracleDecimal?>(), pInOutList = new List<OracleDecimal?>(), pOutList = new List<OracleDecimal?>(), retList;
                List<OracleDecimal?> testValues = new List<OracleDecimal?>() { OracleDecimal.Truncate(OracleDecimal.MaxValue, 0), OracleDecimal.Truncate(OracleDecimal.MinValue, 0), 0, OracleDecimal.Null };
#elif DECIMAL_INTEGER
                Decimal? pIn, pInOut, pOut, ret;
                List<Decimal?> pInList = new List<Decimal?>(), pInOutList = new List<Decimal?>(), pOutList = new List<Decimal?>(), retList;
                List<Decimal?> testValues = new List<Decimal?>() { MAX_DECIMAL, MIN_DECIMAL, 0, null };
#elif SHORT_INTEGER
                Int32? pIn, pInOut, pOut, ret;
                List<Int32?> pInList = new List<Int32?>(), pInOutList = new List<Int32?>(), pOutList = new List<Int32?>(), retList;
                List<Int32?> testValues = new List<Int32?>() { Int32.MaxValue, Int32.MinValue, 0, null };
#else
                Int64? pIn, pInOut, pOut, ret;
                List<Int64?> pInList = new List<Int64?>(), pInOutList = new List<Int64?>(), pOutList = new List<Int64?>(), retList;
                List<Int64?> testValues = new List<Int64?>() { Int64.MaxValue, Int64.MinValue, 0, null };
#endif
                // INTEGER and equivalents
                for (int i = 0; i < testValues.Count; i++){
                    pIn = testValues[i]; pInOut = null;
                    ret = OdptPkgMain.Instance.FuncInteger(pIn, ref pInOut, out pOut, null);
                    Debug.Assert(pIn.Equals(pInOut) && pIn.Equals(pOut) && pIn.Equals(ret));
                }

                pInList = testValues;
                pInOutList = testValues;
                retList = OdptPkgMain.Instance.FuncAaInteger(pInList, ref pInOutList, out pOutList, null);
                for (int i = 0; i < pInList.Count; i++) if (!pInList[i].Equals(pInOutList[i])) throw new Exception("Error");
                for (int i = 0; i < pInList.Count; i++) if (!pInList[i].Equals(pOutList[i])) throw new Exception("Error");
                for (int i = 0; i < pInList.Count; i++) if (!pInList[i].Equals(retList[i])) throw new Exception("Error");

                // INT
                for (int i = 0; i < testValues.Count; i++){
                    pIn = testValues[i]; pInOut = null;
                    ret = OdptPkgMain.Instance.FuncInt(pIn, ref pInOut, out pOut, null);
                    Debug.Assert(pIn.Equals(pInOut) && pIn.Equals(pOut) && pIn.Equals(ret));
                }

                // SMALLINT
                for (int i = 0; i < testValues.Count; i++){
                    pIn = testValues[i]; pInOut = null;
                    ret = OdptPkgMain.Instance.FuncSmallint(pIn, ref pInOut, out pOut, null);
                    Debug.Assert(pIn.Equals(pInOut) && pIn.Equals(pOut) && pIn.Equals(ret));
                }

                pInList = testValues;
                pInOutList = pInList;
                retList = OdptPkgMain.Instance.FuncAaSmallint(pInList, ref pInOutList, out pOutList, null);
                for (int i = 0; i < pInList.Count; i++) {
                    Debug.Assert(pInList[i].Equals(pInOutList[i]));
                    Debug.Assert(pInList[i].Equals(pOutList[i]));
                    Debug.Assert(pInList[i].Equals(retList[i]));
                }
            }

            /// <summary>
            /// Test use of Decimal, OracleDecimal
            /// </summary>
            public void TestDecimalCalls() {
#if SAFETYPE_NUMBER
                OracleDecimal? pInDecimal, pInOutDecimal, pOutDecimal, retDecimal;
                List<OracleDecimal?> pInListDecimal, pInOutListDecimal, pOutListDecimal, retListDecimal;
                List<OracleDecimal?> decimalTestValues = new List<OracleDecimal?>() { OracleDecimal.Null, OracleDecimal.MinValue, 0.0M, OracleDecimal.MaxValue };
#else
                Decimal? pInDecimal, pInOutDecimal, pOutDecimal, retDecimal;
                List<Decimal?> pInListDecimal, pInOutListDecimal, pOutListDecimal, retListDecimal;
                List<Decimal?> decimalTestValues = new List<Decimal?>() { null, MIN_DECIMAL, 0.0M, MAX_DECIMAL };
#endif
                // NUMBER and equivalents
                // NUMBER
                for (int i = 0; i < decimalTestValues.Count; i++) {
                    pInDecimal = decimalTestValues[i]; pInOutDecimal = null;
                    retDecimal = OdptPkgMain.Instance.FuncNumber(pInDecimal, ref pInOutDecimal, out pOutDecimal, null);
                    Debug.Assert(pInDecimal.Equals(pInOutDecimal) || pInDecimal.Equals(pOutDecimal) || pInDecimal.Equals(retDecimal));
                }

                pInListDecimal = decimalTestValues;
                pInOutListDecimal = pInListDecimal;
                retListDecimal = OdptPkgMain.Instance.FuncAaNumber(pInListDecimal, ref pInOutListDecimal, out pOutListDecimal, null);
                for (int i = 0; i < pInListDecimal.Count; i++) if (!pInListDecimal[i].Equals(pInOutListDecimal[i])) throw new Exception("Error");
                for (int i = 0; i < pInListDecimal.Count; i++) if (!pInListDecimal[i].Equals(pOutListDecimal[i])) throw new Exception("Error");
                for (int i = 0; i < pInListDecimal.Count; i++) if (!pInListDecimal[i].Equals(retListDecimal[i])) throw new Exception("Error");

                // FLOAT
                for (int i = 0; i < decimalTestValues.Count; i++) {
                    pInDecimal = decimalTestValues[i]; pInOutDecimal = null;
                    retDecimal = OdptPkgMain.Instance.FuncFloat(pInDecimal, ref pInOutDecimal, out pOutDecimal, null);
                    if (!pInDecimal.Equals(pInOutDecimal) || !pInDecimal.Equals(pOutDecimal) || !pInDecimal.Equals(retDecimal)) throw new Exception("Error");
                }

                pInListDecimal = decimalTestValues;
                pInOutListDecimal = pInListDecimal;
                retListDecimal = OdptPkgMain.Instance.FuncAaFloat(pInListDecimal, ref pInOutListDecimal, out pOutListDecimal, null);
                for (int i = 0; i < pInListDecimal.Count; i++) if (!pInListDecimal[i].Equals(pInOutListDecimal[i])) throw new Exception("Error");
                for (int i = 0; i < pInListDecimal.Count; i++) if (!pInListDecimal[i].Equals(pOutListDecimal[i])) throw new Exception("Error");
                for (int i = 0; i < pInListDecimal.Count; i++) if (!pInListDecimal[i].Equals(retListDecimal[i])) throw new Exception("Error");

                // DOUBLE_PRECISION
                for (int i = 0; i < decimalTestValues.Count; i++) {
                    pInDecimal = decimalTestValues[i]; pInOutDecimal = null;
                    retDecimal = OdptPkgMain.Instance.FuncDoublePrecision(pInDecimal, ref pInOutDecimal, out pOutDecimal, null);
                    if (!pInDecimal.Equals(pInOutDecimal) || !pInDecimal.Equals(pOutDecimal) || !pInDecimal.Equals(retDecimal)) throw new Exception("Error");
                }

                pInListDecimal = decimalTestValues;
                pInOutListDecimal = pInListDecimal;
                retListDecimal = OdptPkgMain.Instance.FuncAaDoublePrecision(pInListDecimal, ref pInOutListDecimal, out pOutListDecimal, null);
                for (int i = 0; i < pInListDecimal.Count; i++) if (!pInListDecimal[i].Equals(pInOutListDecimal[i])) throw new Exception("Error");
                for (int i = 0; i < pInListDecimal.Count; i++) if (!pInListDecimal[i].Equals(pOutListDecimal[i])) throw new Exception("Error");
                for (int i = 0; i < pInListDecimal.Count; i++) if (!pInListDecimal[i].Equals(retListDecimal[i])) throw new Exception("Error");
            }

            /// <summary>
            /// Test use of Double
            /// </summary>
            public void TestDoubleCalls() {
                Double? pInDouble, pInOutDouble, pOutDouble, retDouble;
                List<Double?> pInListDouble = new List<Double?>(), pInOutListDouble = new List<Double?>(), pOutListDouble = new List<Double?>(), retListDouble;

                // BINARY_DOUBLE - underlying OracleDecimal has max scale of 127 which restricts the C# double range

                // call fails with arithmetic overflow inside proc before returning to .NET
                //Double? binaryDoubleMinNormal, binaryDoubleMaxNormal;
                //OdptPkgMain.Instance.ProcBinaryDoubleConst(out binaryDoubleMinNormal, out binaryDoubleMaxNormal);

                //pInDouble = 1.7976931348623157e308d; // BINARY_DOUBLE_MAX_NORMAL fails to return
                //pInDouble = 2.2250738585072014e-308d; // BINARY_DOUBLE_MIN_NORMAL fails to return
                List<Double?> doubleTestValues = new List<Double?>() { 1.7976931348623157e125d, 2.2250738585072014e-130d, 0.0d, null
#if !CSHARP30
                    , Double.NaN
#endif
                };

                // standard call
                foreach (Double? dt in doubleTestValues) {
                    pInDouble = dt; pInOutDouble = null;
                    retDouble = OdptPkgMain.Instance.FuncBinaryDouble(pInDouble, ref pInOutDouble, out pOutDouble, null);
                    if (!Util.IsEqual(pInDouble, pInOutDouble) || !Util.IsEqual(pInDouble, pOutDouble) || !Util.IsEqual(pInDouble, retDouble)) throw new Exception("Error");
                }

                // assoc array
                pInListDouble = doubleTestValues;
                pInOutListDouble = new List<Double?>(pInListDouble);
                retListDouble = OdptPkgMain.Instance.FuncAaBinaryDouble(pInListDouble, ref pInOutListDouble, out pOutListDouble, null);
                for (int i = 0; i < pInListDouble.Count; i++) if (!Util.IsEqual(pInListDouble[i], pInOutListDouble[i])) throw new Exception("Error");
                for (int i = 0; i < pInListDouble.Count; i++) if (!Util.IsEqual(pInListDouble[i], pOutListDouble[i])) throw new Exception("Error");
                for (int i = 0; i < pInListDouble.Count; i++) if (!Util.IsEqual(pInListDouble[i], retListDouble[i])) throw new Exception("Error");
            }

            /// <summary>
            /// Test use of Single(float)
            /// </summary>
            public void TestSingleCalls() {
                Single? pInSingle, pInOutSingle, pOutSingle, retSingle;
                List<Single?> pInListSingle = new List<Single?>(), pInOutListSingle = new List<Single?>(), pOutListSingle = new List<Single?>(), retListSingle;

                // BINARY_FLOAT
                Single? binaryFloatMinNormal, binaryFloatMaxNormal;
                OdptPkgMain.Instance.ProcBinaryFloatConst(out binaryFloatMinNormal, out binaryFloatMaxNormal, null);

                List<Single?> singleTestValues = new List<Single?>() { binaryFloatMaxNormal, binaryFloatMinNormal, 0.0f, null
#if !CSHARP30
                    , Single.NaN
#endif
                };

                // standard call
                foreach (Single? st in singleTestValues) {
                    pInSingle = st; pInOutSingle = null;
                    retSingle = OdptPkgMain.Instance.FuncBinaryFloat(pInSingle, ref pInOutSingle, out pOutSingle, null);
                    if (!Util.IsEqual(pInSingle, pInOutSingle) || !Util.IsEqual(pInSingle, pOutSingle) || !Util.IsEqual(pInSingle, retSingle)) throw new Exception("Error");
                }

                // assoc array
                pInListSingle = singleTestValues;
                pInOutListSingle = new List<Single?>(pInListSingle);
                retListSingle = OdptPkgMain.Instance.FuncAaBinaryFloat(pInListSingle, ref pInOutListSingle, out pOutListSingle, null);
                for (int i = 0; i < pInListSingle.Count; i++) if (!Util.IsEqual(pInListSingle[i], pInOutListSingle[i])) throw new Exception("Error");
                for (int i = 0; i < pInListSingle.Count; i++) if (!Util.IsEqual(pInListSingle[i], pOutListSingle[i])) throw new Exception("Error");
                for (int i = 0; i < pInListSingle.Count; i++) if (!Util.IsEqual(pInListSingle[i], retListSingle[i])) throw new Exception("Error");
            }

            /// <summary>
            /// Test use of String 
            /// </summary>
            public void TestStringCalls() {
                String pInString, pInOutString, pOutString, retString;
                List<String> pInListString = new List<String>(), pInOutListString = new List<String>(), pOutListString, retListString;

                // VARCHAR2 and equivalents
                // VARCHAR
                pInString = new string('?', MAX_STRING_SIZE_FOR_VARCHAR); pInOutString = null;
                retString = OdptPkgMain.Instance.FuncVarchar(pInString, ref pInOutString, out pOutString, null);
                Debug.Assert(pInString.Equals(pInOutString) && pInString.Equals(pOutString) && pInString.Equals(retString));

                // VARCHAR2
                pInString = new string('?', MAX_STRING_SIZE_FOR_VARCHAR); pInOutString = null;
                retString = OdptPkgMain.Instance.FuncVarchar2(pInString, ref pInOutString, out pOutString, null);
                Debug.Assert(pInString.Equals(pInOutString) && pInString.Equals(pOutString) && pInString.Equals(retString));

                pInListString = new List<String>() { null, "TEST", new string('?', MAX_STRING_SIZE_FOR_VARCHAR_IN_ASSOC_ARRAY) };
                pInOutListString = new List<String>(pInListString);
                retListString = OdptPkgMain.Instance.FuncAaVarchar2(pInListString, ref pInOutListString, out pOutListString, null);
                Debug.Assert(pInListString.Except(pInOutListString).Count() == 0 && pInListString.Except(pOutListString).Count() == 0 && pInListString.Except(retListString).Count() == 0);

                // NVARCHAR2
                pInString = new string('?', MAX_STRING_SIZE_FOR_VARCHAR); pInOutString = null;
                retString = OdptPkgMain.Instance.FuncNvarchar2(pInString, ref pInOutString, out pOutString, null);
                Debug.Assert(pInString.Equals(pInOutString) && pInString.Equals(pOutString) && pInString.Equals(retString));

                pInListString = new List<String>() { null, "TEST", new string('?', MAX_STRING_SIZE_FOR_NVARCHAR_IN_ASSOC_ARRAY) };
                pInOutListString = new List<String>(pInListString);
                retListString = OdptPkgMain.Instance.FuncAaNvarchar2(pInListString, ref pInOutListString, out pOutListString, null);
                Debug.Assert(pInListString.Except(pInOutListString).Count() == 0 && pInListString.Except(pOutListString).Count() == 0 && pInListString.Except(retListString).Count() == 0);

                // STRING
                pInString = new string('?', MAX_STRING_SIZE_FOR_VARCHAR); pInOutString = null;
                retString = OdptPkgMain.Instance.FuncString(pInString, ref pInOutString, out pOutString, null);
                Debug.Assert(pInString.Equals(pInOutString) && pInString.Equals(pOutString) && pInString.Equals(retString));

                pInListString = new List<String>() { null, "TEST", new string('?', MAX_STRING_SIZE_FOR_VARCHAR_IN_ASSOC_ARRAY) };
                pInOutListString = new List<String>(pInListString);
                retListString = OdptPkgMain.Instance.FuncAaString(pInListString, ref pInOutListString, out pOutListString, null);
                Debug.Assert(pInListString.Except(pInOutListString).Count() == 0 && pInListString.Except(pOutListString).Count() == 0 && pInListString.Except(retListString).Count() == 0);

                // CHAR and equivalents
                // CHAR
                pInString = new string('?', MAX_STRING_SIZE_FOR_CHAR); pInOutString = null;
                retString = OdptPkgMain.Instance.FuncChar(pInString, ref pInOutString, out pOutString, null);
                Debug.Assert(pInString.Equals(pInOutString) && pInString.Equals(pOutString) && pInString.Equals(retString));

                pInListString = new List<String>() { null, "TEST", new string('?', MAX_STRING_SIZE_FOR_CHAR_IN_ASSOC_ARRAY) };
                pInOutListString = new List<String>(pInListString);
                retListString = OdptPkgMain.Instance.FuncAaChar(pInListString, ref pInOutListString, out pOutListString, null);
                Debug.Assert(pInListString.Except(pInOutListString).Count() == 0 && pInListString.Except(pOutListString).Count() == 0 && pInListString.Except(retListString).Count() == 0);

                // NCHAR
                pInString = new string('?', MAX_STRING_SIZE_FOR_CHAR); pInOutString = null;
                retString = OdptPkgMain.Instance.FuncNchar(pInString, ref pInOutString, out pOutString, null);
                Debug.Assert(pInString.Equals(pInOutString) && pInString.Equals(pOutString) && pInString.Equals(retString));

                pInListString = new List<String>() { null, "TEST", new string('?', MAX_STRING_SIZE_FOR_NCHAR_IN_ASSOC_ARRAY) };
                pInOutListString = new List<String>(pInListString);
                retListString = OdptPkgMain.Instance.FuncAaNchar(pInListString, ref pInOutListString, out pOutListString, null);
                Debug.Assert(pInListString.Except(pInOutListString).Count() == 0 && pInListString.Except(pOutListString).Count() == 0 && pInListString.Except(retListString).Count() == 0);
            }

            /// <summary>
            /// Test use of DateTime, OracleDate, OracleTimeStamp 
            /// </summary>
            public void TestDateCalls() {
#if !SAFETYPE_DATE
                DateTime? pInDateTime, pInOutDateTime, pOutDateTime, retDateTime;
                List<DateTime?> pInListDateTime, pInOutListDateTime, pOutListDateTime, retListDateTime;
                List<DateTime?> dateTimeTestValues = new List<DateTime?>() { 
#if !CSHARP30 // Unmanaged has issues with time portion, min value and max value
                    DateTime.Now, DateTime.MaxValue.AddMilliseconds(-1), DateTime.MinValue.AddMilliseconds(1),
#endif
                    null };
#else
                OracleDate? pInDateTime, pInOutDateTime, pOutDateTime, retDateTime;
                List<OracleDate?> pInListDateTime, pInOutListDateTime, pOutListDateTime, retListDateTime;
                List<OracleDate?> dateTimeTestValues = new List<OracleDate?>() { OracleDate.GetSysDate(), OracleDate.MaxValue, OracleDate.MinValue, OracleDate.Null };
#endif
                // DATE
                // standard call
                for (int i = 0; i < dateTimeTestValues.Count; i++) {
                    pInDateTime = dateTimeTestValues[i]; pInOutDateTime = null;
                    retDateTime = OdptPkgMain.Instance.FuncDate(pInDateTime, ref pInOutDateTime, out pOutDateTime, null);
#if !SAFETYPE_DATE
                    if (pInDateTime == null)
                        Debug.Assert(pInOutDateTime == null && pOutDateTime == null && retDateTime == null);
                    else {
                        Debug.Assert(pInDateTime - pInOutDateTime < TimeSpan.FromSeconds(1) && pInDateTime - pOutDateTime < TimeSpan.FromSeconds(1) && pInDateTime - retDateTime < TimeSpan.FromSeconds(1));
                    }
#else
                    if (pInDateTime.Value.IsNull) {
                        Debug.Assert(pInOutDateTime.Value.IsNull && pOutDateTime.Value.IsNull && retDateTime.Value.IsNull);
                    } else {
                        Debug.Assert(pInDateTime.Equals(pInOutDateTime) && pInDateTime.Equals(pOutDateTime) && pInDateTime.Equals(retDateTime));
                    }    
#endif
                }

                // assoc array
                pInListDateTime = dateTimeTestValues;
                pInOutListDateTime = pInListDateTime;
                retListDateTime = OdptPkgMain.Instance.FuncAaDate(pInListDateTime, ref pInOutListDateTime, out pOutListDateTime, null);
#if !SAFETYPE_DATE
                for (int i = 0; i < pInListDateTime.Count; i++) if (pInListDateTime[i] - pInOutListDateTime[i] > TimeSpan.FromSeconds(1)) throw new Exception("Error");
                for (int i = 0; i < pInListDateTime.Count; i++) if (pInListDateTime[i] - pOutListDateTime[i] > TimeSpan.FromSeconds(1)) throw new Exception("Error");
                for (int i = 0; i < pInListDateTime.Count; i++) if (pInListDateTime[i] - retListDateTime[i] > TimeSpan.FromSeconds(1)) throw new Exception("Error");
#else
                for (int i = 0; i < pInListDateTime.Count; i++) if (!pInListDateTime[i].Equals(pInOutListDateTime[i])) throw new Exception("Error");
                for (int i = 0; i < pInListDateTime.Count; i++) if (!pInListDateTime[i].Equals(pOutListDateTime[i])) throw new Exception("Error");
                for (int i = 0; i < pInListDateTime.Count; i++) if (!pInListDateTime[i].Equals(retListDateTime[i])) throw new Exception("Error");
#endif

#if !SAFETYPE_TIMESTAMP
                DateTime? pInTimeStamp, pInOutTimeStamp, pOutTimeStamp, retTimeStamp;
                List<DateTime?> pInListTimeStamp, pInOutListTimeStamp; //, pOutListTimeStamp, retListTimeStamp;
                List<DateTime?> timeStampTestValues = new List<DateTime?>() {
#if !CSHARP30   // Unmanaged has issues with non-null TIMESTAMP values
                    DateTime.Now, DateTime.MaxValue.AddMilliseconds(-1), DateTime.MinValue.AddMilliseconds(1),
#endif
                    null };
#else
                    OracleTimeStamp? pInTimeStamp, pInOutTimeStamp, pOutTimeStamp, retTimeStamp;
                List<OracleTimeStamp?> pInListTimeStamp, pInOutListTimeStamp; //, pOutListTimeStamp, retListTimeStamp;
                List<OracleTimeStamp?> timeStampTestValues = new List<OracleTimeStamp?>() { OracleTimeStamp.GetSysDate(), OracleTimeStamp.MaxValue.AddMilliseconds(-1), OracleTimeStamp.MinValue.AddMilliseconds(1), OracleTimeStamp.Null };
#endif
                // TIMESTAMP 
                // standard call
                for (int i = 0; i < timeStampTestValues.Count; i++) {
                    pInTimeStamp = timeStampTestValues[i]; pInOutTimeStamp = null;
                    retTimeStamp = OdptPkgMain.Instance.FuncTimestamp(pInTimeStamp, ref pInOutTimeStamp, out pOutTimeStamp, null);
#if !SAFETYPE_TIMESTAMP
                    if (pInTimeStamp - pInOutTimeStamp > TimeSpan.FromSeconds(1)) throw new Exception( "Error" );
                    if (pInTimeStamp - pOutTimeStamp > TimeSpan.FromSeconds(1)) throw new Exception( "Error" );
                    if (pInTimeStamp - retTimeStamp > TimeSpan.FromSeconds(1)) throw new Exception( "Error" );
#else
                    if (pInTimeStamp.Value.IsNull) {
                        if (!pInOutTimeStamp.Value.IsNull || !pOutTimeStamp.Value.IsNull || !retTimeStamp.Value.IsNull) throw new Exception("Error");
                    } else {
                        if (!OracleTimeStamp.SetPrecision(pInTimeStamp.Value, 6).Equals(OracleTimeStamp.SetPrecision(pInOutTimeStamp.Value, 6))) throw new Exception( "Error" );
                        if (!OracleTimeStamp.SetPrecision(pInTimeStamp.Value, 6).Equals(OracleTimeStamp.SetPrecision(pOutTimeStamp.Value, 6))) throw new Exception( "Error" );
                        if (!OracleTimeStamp.SetPrecision(pInTimeStamp.Value, 6).Equals(OracleTimeStamp.SetPrecision(retTimeStamp.Value, 6))) throw new Exception( "Error" );
                    }
#endif
                }

                // assoc array - failing inside proc
                pInListTimeStamp = timeStampTestValues;
                pInOutListTimeStamp = pInListTimeStamp;
                //retListTimeStamp = OdptPkgMain.Instance.FuncAaTimestamp( pInListTimeStamp, ref pInOutListTimeStamp, out pOutListTimeStamp );
                //for (int i = 0; i < pInListTimeStamp.Count; i++) if (pInListTimeStamp[i] - pInOutListTimeStamp[i] > TimeSpan.FromSeconds( 1 )) throw new Exception( "Error" );
                //for (int i = 0; i < pInListTimeStamp.Count; i++) if (pInListTimeStamp[i] - pOutListTimeStamp[i] > TimeSpan.FromSeconds( 1 )) throw new Exception( "Error" );
                //for (int i = 0; i < pInListTimeStamp.Count; i++) if (pInListTimeStamp[i] - retListTimeStamp[i] > TimeSpan.FromSeconds( 1 )) throw new Exception( "Error" );
            }
            public void TestTimeSpanCalls() {
            }
#endregion

#region Miscellaneous tests
            public void CompileTimeChecks() {
                OdptPkgEmpty pkgEmpty = OdptPkgEmpty.Instance;
                OdptPkgSql pkgSql = OdptPkgSql.Instance;
                OdptPkgLog pkgLog = OdptPkgLog.Instance;

                OdptTableBig tablBig = new OdptTableBig(); ;
                OdptTableNumber tableNumber = new OdptTableNumber();
                OdptTableObject tableObject = new OdptTableObject();
                OdptLogs tableLogs = new OdptLogs();

                OdptViewBigV viewBig = new OdptViewBigV();

                OdptBigOt objectBig = new OdptBigOt();
                OdptPoVendorOt objectPoVendor = new OdptPoVendorOt();
            }

            public void TestNoParamCalls() {
                OdptPkgMain.Instance.ProcNoParam(null);
#if SAFETYPE_NUMBER
                OracleDecimal? 
#else
                Decimal?
#endif
                ret = OdptPkgMain.Instance.FuncNoParam(null);
            }

            public void TestOptionalParamCalls() {
#if SAFETYPE_NUMBER
                List<OracleDecimal?> decimalTestValues = new List<OracleDecimal?>() { OracleDecimal.Null, OracleDecimal.MinValue, 0.0M, OracleDecimal.MaxValue };
#else
                List<Decimal?> decimalTestValues = new List<Decimal?>() { null, MIN_DECIMAL, 0.0M, MAX_DECIMAL };
#endif

#if SAFETYPE_NUMBER
                OracleDecimal? 
#else
                Decimal?
#endif
                            pInNumberRequired, pInOutNumberRequired, pInNumberOptional, pRetNumber;
                String pInVarchar2Optional = "TEST";

                for (int i = 0; i < decimalTestValues.Count; i++) {
                    pInNumberRequired = decimalTestValues[i]; pInOutNumberRequired = decimalTestValues[i]; pInNumberOptional = decimalTestValues[i];

                    // proc
                    OdptPkgMain.Instance.ProcOptionalParam(pInNumberRequired, ref pInOutNumberRequired, pInNumberOptional, pInVarchar2Optional, null);
#if !CSHARP30
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired));
                    OdptPkgMain.Instance.ProcOptionalParam(pInNumberRequired, ref pInOutNumberRequired, pInNumberOptional, pInVarchar2Optional);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired));
                    OdptPkgMain.Instance.ProcOptionalParam(pInNumberRequired, ref pInOutNumberRequired, pInNumberOptional);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired));
                    OdptPkgMain.Instance.ProcOptionalParam(pInNumberRequired, ref pInOutNumberRequired);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired));
                    OdptPkgMain.Instance.ProcOptionalParam(pInNumberRequired, ref pInOutNumberRequired, optionalPreexistingOpenConnection : null);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired));
#endif

                    // func
                    pRetNumber = OdptPkgMain.Instance.FuncOptionalParam(pInNumberRequired, ref pInOutNumberRequired, pInNumberOptional, pInVarchar2Optional, null);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired) && pInNumberRequired.Equals(pRetNumber));
#if !CSHARP30
                    pRetNumber = OdptPkgMain.Instance.FuncOptionalParam(pInNumberRequired, ref pInOutNumberRequired, pInNumberOptional, pInVarchar2Optional);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired) && pInNumberRequired.Equals(pRetNumber));
                    pRetNumber = OdptPkgMain.Instance.FuncOptionalParam(pInNumberRequired, ref pInOutNumberRequired, pInNumberOptional);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired) && pInNumberRequired.Equals(pRetNumber));
                    pRetNumber = OdptPkgMain.Instance.FuncOptionalParam(pInNumberRequired, ref pInOutNumberRequired);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired) && pInNumberRequired.Equals(pRetNumber));
                    pRetNumber = OdptPkgMain.Instance.FuncOptionalParam(pInNumberRequired, ref pInOutNumberRequired, optionalPreexistingOpenConnection : null);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired) && pInNumberRequired.Equals(pRetNumber));
#endif
                }
            }
#endregion
        }

#region DTOs
#region Table Big
        public class TTableBigMapByPositionAll : OdptPkgTableBig.ITTableBig {
            [MapAttribute(Position = 0)]
            public virtual
#if SAFETYPE_INTEGER
                            OracleDecimal?
#elif DECIMAL_INTEGER
                            Decimal?
#elif SHORT_INTEGER
                            Int32?
#else
                            Int64?
#endif
                                    Id
            { get; set; }
            [MapAttribute(Position = 1)]
            public virtual
#if SAFETYPE_INTEGER
                            OracleDecimal?
#elif DECIMAL_INTEGER
                            Decimal?
#elif SHORT_INTEGER
                            Int32?
#else
                            Int64?
#endif
                                    ColNumberId
            { get; set; }
            [MapAttribute(Position = 2)]
            public virtual
#if SAFETYPE_INTEGER
                            OracleDecimal?
#elif DECIMAL_INTEGER
                            Decimal?
#elif SHORT_INTEGER
                            Int32?
#else
                            Int64?
#endif
                                    ColInteger
            { get; set; }
            [MapAttribute(Position = 3)]
            public virtual
#if SAFETYPE_INTEGER
                            OracleDecimal?
#elif DECIMAL_INTEGER
                            Decimal?
#elif SHORT_INTEGER
                            Int32?
#else
                            Int64?
#endif
                                    ColInt
            { get; set; }
            [MapAttribute(Position = 4)]
            public virtual
#if SAFETYPE_INTEGER
                            OracleDecimal?
#elif DECIMAL_INTEGER
                            Decimal?
#elif SHORT_INTEGER
                            Int32?
#else
                            Int64?
#endif
                                    ColSmallint
            { get; set; }
            [MapAttribute(Position = 5)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber
            { get; set; }
            [MapAttribute(Position = 6)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColDoublePrecision
            { get; set; }
            [MapAttribute(Position = 7)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColFloat
            { get; set; }
            [MapAttribute(Position = 8)]
            public virtual Single? ColBinaryFloat { get; set; }
            [MapAttribute(Position = 9)]
            public virtual Double? ColBinaryDouble { get; set; }
            [MapAttribute(Position = 10)]
            public virtual String ColVarcharMin { get; set; }
            [MapAttribute(Position = 11)]
            public virtual String ColVarcharMax { get; set; }
            [MapAttribute(Position = 12)]
            public virtual String ColVarchar2Min { get; set; }
            [MapAttribute(Position = 13)]
            public virtual String ColVarchar2Max { get; set; }
            [MapAttribute(Position = 14)]
            public virtual String ColNvarchar2Min { get; set; }
            [MapAttribute(Position = 15)]
            public virtual String ColNvarchar2Max { get; set; }
            [MapAttribute(Position = 16)]
            public virtual String ColCharMin { get; set; }
            [MapAttribute(Position = 17)]
            public virtual String ColCharMax { get; set; }
            [MapAttribute(Position = 18)]
            public virtual String ColNcharMin { get; set; }
            [MapAttribute(Position = 19)]
            public virtual String ColNcharMax { get; set; }
            [MapAttribute(Position = 20)]
            public virtual
#if SAFETYPE_DATE
                            OracleDate?
#else
                            DateTime?
#endif
                                    ColDate
            { get; set; }
            [MapAttribute(Position = 21)]
            public virtual
#if SAFETYPE_TIMESTAMP
                            OracleTimeStamp? 
#else
                            DateTime?
#endif
                                    ColTimestamp
            { get; set; }

            [MapAttribute(Position = 22)]
            public virtual
#if SAFETYPE_TIMESTAMP
                            OracleTimeStamp? 
#else
                            DateTime?
#endif
                                    ColTimestampPrec0
            { get; set; }


            [MapAttribute(Position = 23)]
            public virtual
#if SAFETYPE_TIMESTAMP
                            OracleTimeStamp? 
#else
                            DateTime?
#endif
                                    ColTimestampPrec9
            { get; set; }

            [MapAttribute(Position = 24)]
            public virtual String ColLast { get; set; }
        } // TTableBig

        public class TTableBigMapByPositionPartial : TTableBigMapByPositionAll {
            public override String ColLast { get; set; }
        }

        public class TTableBigCharMapByPositionAll : OdptPkgTableBig.ITTableBigChar {
            [MapAttribute(Position = 0)]
            public virtual
#if SAFETYPE_INTEGER
                            OracleDecimal?
#elif DECIMAL_INTEGER
                            Decimal?
#elif SHORT_INTEGER
                            Int32?
#else
                            Int64?
#endif
                                    Id { get; set; }
            [MapAttribute(Position = 1)]
            public virtual String ColVarcharMin { get; set; }
            [MapAttribute(Position = 2)]
            public virtual String ColVarcharMax { get; set; }
            [MapAttribute(Position = 3)]
            public virtual String ColVarchar2Min { get; set; }
            [MapAttribute(Position = 4)]
            public virtual String ColVarchar2Max { get; set; }
            [MapAttribute(Position = 5)]
            public virtual String ColNvarchar2Min { get; set; }
            [MapAttribute(Position = 6)]
            public virtual String ColNvarchar2Max { get; set; }
            [MapAttribute(Position = 7)]
            public virtual String ColCharMin { get; set; }
            [MapAttribute(Position = 8)]
            public virtual String ColCharMax { get; set; }
            [MapAttribute(Position = 9)]
            public virtual String ColNcharMin { get; set; }
            [MapAttribute(Position = 10)]
            public virtual String ColNcharMax { get; set; }
            [MapAttribute(Position = 11)]
            public virtual String ColLast { get; set; }
        } // TTableBig

        public class TTableBigCharMapByPositionPartial : TTableBigCharMapByPositionAll {
            public override String ColLast { get; set; }
        }
#endregion
#region Table Number
        public class TTableNumberMapByPositionAll : OdptPkgTableNumber.ITTableNumber {
            [MapAttribute(Position = 0)]
            public virtual
#if SAFETYPE_INTEGER
                            OracleDecimal? 
#elif DECIMAL_INTEGER
                            Decimal?
#elif SHORT_INTEGER
                            Int32?
#else
                            Int64?
#endif
                                    Id
            { get; set; }
            [MapAttribute(Position = 1)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber
            { get; set; }
            [MapAttribute(Position = 2)]
            public virtual 
                            SByte? 
                                    ColNumber1 { get; set; }
            [MapAttribute(Position = 3)]
            public virtual 
                            SByte? 
                                    ColNumber2 { get; set; }
            [MapAttribute(Position = 4)]
            public virtual 
                            Int16? 
                                    ColNumber3 { get; set; }
            [MapAttribute(Position = 5)]
            public virtual 
                            Int16? 
                                    ColNumber4 { get; set; }
            [MapAttribute(Position = 6)]
            public virtual 
                            Int32? 
                                    ColNumber5 { get; set; }
            [MapAttribute(Position = 7)]
            public virtual 
                            Int32? 
                                    ColNumber6 { get; set; }
            [MapAttribute(Position = 8)]
            public virtual 
                            Int32? 
                                    ColNumber7 { get; set; }
            [MapAttribute(Position = 9)]
            public virtual 
                            Int32? 
                                    ColNumber8 { get; set; }
            [MapAttribute(Position = 10)]
            public virtual 
                            Int32? 
                                    ColNumber9 { get; set; }
            [MapAttribute(Position = 11)]
            public virtual
#if SAFETYPE_INTEGER
                            OracleDecimal? 
#elif DECIMAL_INTEGER
                            Decimal?
#elif SHORT_INTEGER
                            Int32?
#else
                            Int64?
#endif
                                    ColNumber10 { get; set; }
            [MapAttribute(Position = 12)]
            public virtual
#if SAFETYPE_INTEGER
                            OracleDecimal? 
#elif DECIMAL_INTEGER
                            Decimal?
#elif SHORT_INTEGER
                            Int32?
#else
                            Int64?
#endif
                                    ColNumber18
            { get; set; }
            [MapAttribute(Position = 13)]
            public virtual
#if SAFETYPE_INTEGER
                            OracleDecimal? 
#elif DECIMAL_INTEGER
                            Decimal?
#elif SHORT_INTEGER
                            Int32?
#else
                            Int64?
#endif
                                    ColNumber19 { get; set; }
            [MapAttribute(Position = 14)]
            public virtual
#if SAFETYPE_INTEGER
                            OracleDecimal? 
#elif DECIMAL_INTEGER
                            Decimal?
#elif SHORT_INTEGER
                            Int32?
#else
                            Int64?
#endif
                                    ColNumber38
            { get; set; }
            [MapAttribute(Position = 15)]
            public virtual 
                            SByte? 
                                    ColNumber1Scale0 { get; set; }
            [MapAttribute(Position = 16)]
            public virtual 
                            SByte? 
                                    ColNumber2Scale0 { get; set; }
            [MapAttribute(Position = 17)]
            public virtual 
                            Int16? 
                                    ColNumber3Scale0 { get; set; }
            [MapAttribute(Position = 18)]
            public virtual 
                            Int16? 
                                    ColNumber4Scale0 { get; set; }
            [MapAttribute(Position = 19)]
            public virtual 
                            Int32? 
                                    ColNumber5Scale0 { get; set; }
            [MapAttribute(Position = 20)]
            public virtual 
                            Int32? 
                                    ColNumber9Scale0 { get; set; }
            [MapAttribute(Position = 21)]
            public virtual
#if SAFETYPE_INTEGER
                            OracleDecimal? 
#elif DECIMAL_INTEGER
                            Decimal?
#elif SHORT_INTEGER
                            Int32?
#else
                            Int64?
#endif
                                    ColNumber10Scale0
            { get; set; }
            [MapAttribute(Position = 22)]
            public virtual
#if SAFETYPE_INTEGER
                            OracleDecimal? 
#elif DECIMAL_INTEGER
                            Decimal?
#elif SHORT_INTEGER
                            Int32?
#else
                            Int64?
#endif
                                    ColNumber18Scale0
            { get; set; }
            [MapAttribute(Position = 23)]
            public virtual
#if SAFETYPE_INTEGER
                            OracleDecimal? 
#elif DECIMAL_INTEGER
                            Decimal?
#elif SHORT_INTEGER
                            Int32?
#else
                            Int64?
#endif
                                    ColNumber19Scale0
            { get; set; }
            [MapAttribute(Position = 24)]
            public virtual
#if SAFETYPE_INTEGER
                            OracleDecimal? 
#elif DECIMAL_INTEGER
                            Decimal?
#elif SHORT_INTEGER
                            Int32?
#else
                            Int64?
#endif
                                    ColNumber28Scale0
            { get; set; }
            [MapAttribute(Position = 25)]
            public virtual
#if SAFETYPE_INTEGER
                            OracleDecimal? 
#elif DECIMAL_INTEGER
                            Decimal?
#elif SHORT_INTEGER
                            Int32?
#else
                            Int64?
#endif
                                    ColNumber29Scale0
            { get; set; }
            [MapAttribute(Position = 26)]
            public virtual
#if SAFETYPE_INTEGER
                            OracleDecimal? 
#elif DECIMAL_INTEGER
                            Decimal?
#elif SHORT_INTEGER
                            Int32?
#else
                            Int64?
#endif
                                    ColNumber38Scale0
            { get; set; }
            [MapAttribute(Position = 27)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber2Scale1
            { get; set; }
            [MapAttribute(Position = 28)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber5Scale3
            { get; set; }
            [MapAttribute(Position = 29)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber15Scale11
            { get; set; }
            [MapAttribute(Position = 30)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber31Scale21
            { get; set; }
            [MapAttribute(Position = 31)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber38Scale37
            { get; set; }
            [MapAttribute(Position = 32)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumberLast
            { get; set; }
        }

        public class TTableNumberMapByPositionPartial : TTableNumberMapByPositionAll {
            public override
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumberLast
            { get; set; }
        }

        public class TTableNumberDecMapByPositionAll : OdptPkgTableNumber.ITTableNumberDec {
            [MapAttribute(Position = 0)]
            public virtual
#if SAFETYPE_INTEGER
                            OracleDecimal?
#elif DECIMAL_INTEGER
                            Decimal?
#elif SHORT_INTEGER
                            Int32?
#else
                            Int64?
#endif
                                    Id { get; set; }
            [MapAttribute(Position = 1)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber { get; set; }
            [MapAttribute(Position = 2)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber2Scale1 { get; set; }
            [MapAttribute(Position = 3)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber5Scale3 { get; set; }
            [MapAttribute(Position = 4)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber15Scale11 { get; set; }
            [MapAttribute(Position = 5)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber31Scale21 { get; set; }
            [MapAttribute(Position = 6)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber38Scale37 { get; set; }
            [MapAttribute(Position = 7)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumberLast { get; set; }
        } // TTableBig

        public class TTableNumberDecMapByPositionPartial : TTableNumberDecMapByPositionAll {
            public override
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumberLast { get; set; }
        }
#endregion
#endregion DTOs

        public class Util {
            /// <summary>
            /// A make shift compare for two double values
            /// </summary>
            /// <param name="d1"></param>
            /// <param name="d2"></param>
            /// <returns></returns>
            public static bool IsEqual(Double? v1, Double? v2) {
                if ((v1 == null && v2 == null) || (v1 == 0.0 && v2 == 0.0) || (v1.Equals(Double.NaN) && v2.Equals(Double.NaN)) || 
                    (v1.Equals(Double.PositiveInfinity) && v2.Equals(Double.PositiveInfinity)) || v1.Equals(Double.NegativeInfinity) && v2.Equals(Double.NegativeInfinity)) return true;
                double allowedDelta = Math.Abs(v1.Value * 0.00001);
                double delta = Math.Abs(v1.Value - v2.Value);
                return delta <= allowedDelta;
            }

            public static bool IsEqual(Single? v1, Single? v2) {
                if ((v1 == null && v2 == null) || (v1 == 0.0 && v2 == 0.0) || (v1.Equals(Single.NaN) && v2.Equals(Single.NaN)) || 
                    (v1.Equals(Single.PositiveInfinity) && v2.Equals(Single.PositiveInfinity)) || v1.Equals(Single.NegativeInfinity) && v2.Equals(Single.NegativeInfinity)) return true;
                double allowedDelta = Math.Abs(v1.Value * 0.00001);
                double delta = Math.Abs(v1.Value - v2.Value);
                return delta <= allowedDelta;
            }
        }

        public class Database {
            private string _dataSource = "localhost", _login = "ODPT", _password = "odpt";

            public long TestConnection() {
                using (OracleConnection connection = (OracleConnection)GetConnection()) {
                    return 0;
                }
            }

            public string GetConnectionString() {
                return "data source=" + _dataSource + ";user id=" + _login + ";password=" + _password + ";enlist=false";
            }

            public OracleConnection GetConnection() {
                OracleConnection connection = new OracleConnection(GetConnectionString());
                connection.Open();
                return connection;
            }
        }
    }
}