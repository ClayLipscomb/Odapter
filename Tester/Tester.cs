﻿//------------------------------------------------------------------------------
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

//#define SHORT_INTEGER               // INTEGER as Int32
//#define DECIMAL_INTEGER             // INTEGER as Decimal

#define DATE_TIME                   // DATE as DateTime

//#define SAFETYPE_INTEGER            // INTEGER as safe type OracleDecimal
//#define SAFETYPE_NUMBER             // NUMBER as safe type OracleDecimal
//#define SAFETYPE_DATE               // DATE as safe type OracleDate
//#define SAFETYPE_TIMESTAMP          // TIMESTAMP as safe type OracleTimeStamp
//#define SAFETYPE_BLOB               // BLOB as safe type OracleBlob
//#define SAFETYPE_CLOB               // CLOB, NCLOB as safe type OracleClob

#define ODPT_FILTER_PREFIX          // "ODPT" as filter prefix of schema
#define MAPPING_FOR_TYPED_CURSOR    // optional overloads for typed cursors methods are generated for mapping
//#define SEED_TABLES                 // seed all tables with test data

//#define LARGE_LOB_SIZE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

#if ODPT_FILTER_PREFIX
using Schema.Odpt.Odpt;
using Schema.Odpt.Odpt.Package;
using Schema.Odpt.Odpt.Table;
using Schema.Odpt.Odpt.Type.Object;
using Schema.Odpt.Odpt.View;
#else
using Schema.Odpt;
using Schema.Odpt.Package;
using Schema.Odpt.Table;
using Schema.Odpt.Type.Object;
using Schema.Odpt.View;
#endif

namespace Odapter.Tester {
    class Program {
        static void Main(string[] args) {
            Tester test = new Tester();
            test.Run();
        }

        public class Tester {
            private const uint TABLE_BIG_ROWS_TO_INSERT = 1000;
            private const uint TABLE_BIG_ROWS_TO_RETRIEVE = 100;

            // VARCHAR2, VARCHAR, NVARCHAR2, STRING
            private const int MAX_STRING_SIZE_FOR_VARCHAR_ARG = 8191;    // >= 8192 overflows against XE with managed 
            private const int MAX_STRING_SIZE_FOR_VARCHAR_IN_ASSOC_ARRAY = 4000;
            private const int MAX_STRING_SIZE_FOR_NVARCHAR_IN_ASSOC_ARRAY = 1000;
            private const int MAX_STRING_SIZE_FOR_CHAR = 2000;
            private const int MAX_STRING_SIZE_FOR_CHAR_IN_ASSOC_ARRAY = 2000;
            private const int MAX_STRING_SIZE_FOR_NCHAR_IN_ASSOC_ARRAY = 500;

            private const int MAX_STRING_SIZE_FOR_VARCHAR_COL = 4000;
            private const int MAX_STRING_SIZE_FOR_NVARCHAR_COL = 2000;
            private const int MAX_STRING_SIZE_FOR_CHAR_COL = 2000;
            private const int MAX_STRING_SIZE_FOR_NCHAR_COL = 1000;

            // actual max of LOB *column* is 4 GB (4*1024*1024*1024-1 bytes) 4,294,967,295
            private const int LOB_SIZE =
#if LARGE_LOB_SIZE
                (100 * 1024 * 1024) - 1;    // out of memory occurs at 1GB, will test at 0.1 GB
#else
                (10 * 1024 * 1024) - 1;    // faster tests with 0.01 GB
#endif
            private const int MAX_BYTES_FOR_BLOB_COL = LOB_SIZE;
            private const int MAX_STRING_SIZE_FOR_CLOB_COL = LOB_SIZE / 2;  // allow for 2 bytes per String character
            private const int MAX_STRING_SIZE_FOR_NCLOB_COL = LOB_SIZE / 2;  // allow for 2 bytes per String character

            private const Double MIN_BINARY_DOUBLE = 2.2250738585072014e-130d;
            private const Double MAX_BINARY_DOUBLE = 1.7976931348623157e125d;
            private const Decimal MAX_DECIMAL = 792281625142643375935439.5033M;
            private const Decimal MIN_DECIMAL = -792281625142643375935439.5033M;
            //792281625142643375935439 50335
            //private const Decimal MAX_ORACLE_DECIMAL = 792281625142643375935439.5033M;
            //private const Decimal MIN_ORACLE_DECIMAL = -792281625142643375935439.5033M;
            //private readonly OracleDecimal MAX_ORACLE_DECIMAL = new OracleDecimal("999999999999999999999999999999999999990000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            // 999999999999999999999999999999999999990000000000000000000000000000000000000000000000000000000000000000000000000000000000000000

            internal void Run() {
                Debug.Assert(Database.Instance.TestConnection());
#if SEED_TABLES
                SeedTableBig();
                SeedTableNumber();
#endif
                TestStringCalls();
                TestStringLobCalls();
                TestByteArrayLobCalls();

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

                TestDuplicateSignatureCalls();
                TestNoParamCalls();
                TestOptionalParamCalls();
                TestMiscCalls();
                TestSingleton();
                CompileTimeChecks();

                //Console.WriteLine( "Complete." );
                //Console.ReadLine();
            }

            #region Seeding Methods
            private void SeedTableBig() {
                OracleConnection conn =
#if SAFETYPE_BLOB || SAFETYPE_CLOB
                Database.Instance.GetConnection();  // connection required to instantiate LOB classes
#else
                null;
#endif
#if SAFETYPE_BLOB 
                OracleBlob oracleBlob = new OracleBlob(conn);
#endif
#if SAFETYPE_CLOB 
                OracleClob oracleClob = new OracleClob(conn);
                OracleClob oracleNClob = new OracleClob(conn);
#endif
                OdptPkgTableBig.Instance.TruncTable(null);

                // misc values (zero, NaN, empty string, null)
#if SAFETYPE_BLOB
                oracleBlob.Append(new byte[] { byte.MinValue }, 0, 1);
#endif
#if SAFETYPE_CLOB
                oracleClob.Append(new char[] { ' ' }, 0, 1);
                oracleNClob.Append(new char[] { ' ' }, 0, 1);
#endif
                OdptPkgTableBig.Instance.InsertRow(0, 0, 0, 0, 0, 0, 0.0M, 0.0M, 0.0M, 0.0M, Single.NaN, Double.NaN, "", "", "", "", "", "", "", "", "", "", null, null, null, null,
#if SAFETYPE_BLOB
                    oracleBlob,
#else
                    null,
#endif
#if SAFETYPE_CLOB
                    oracleClob,
#else
                    "",
#endif
#if SAFETYPE_CLOB
                    oracleNClob,
#else
                    "",
#endif
                    conn);

                // all explicitly null values
                OdptPkgTableBig.Instance.InsertRow(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, conn);

                // min values for INTEGER, NUMBER, DATE, TIMESTAMP, LOB 
#if SAFETYPE_BLOB
                oracleBlob.SetLength(0);
                oracleBlob.Append(new byte[] { byte.MinValue }, 0, 1);
#endif
#if SAFETYPE_CLOB
                oracleClob.SetLength(0);
                oracleClob.Append(new char[] { 'F' }, 0, 1);
                oracleNClob.SetLength(0);
                oracleNClob.Append(new char[] { 'G' }, 0, 1);
#endif
                OdptPkgTableBig.Instance.InsertRow(
#if SAFETYPE_INTEGER
                    OracleDecimal.MinValue, OracleDecimal.Truncate(OracleDecimal.MinValue, 0), OracleDecimal.Truncate(OracleDecimal.MinValue, 0), OracleDecimal.Truncate(OracleDecimal.MinValue, 0), OracleDecimal.Truncate(OracleDecimal.MinValue, 0), OracleDecimal.Truncate(OracleDecimal.MinValue, 0),
#elif DECIMAL_INTEGER
                    MIN_DECIMAL, MIN_DECIMAL, MIN_DECIMAL, MIN_DECIMAL, MIN_DECIMAL, MIN_DECIMAL,
#elif SHORT_INTEGER
                    Int32.MinValue, Int32.MinValue, Int32.MinValue, Int32.MinValue, Int32.MinValue, Int32.MinValue,
#else
                    Int64.MinValue, Int64.MinValue, Int64.MinValue, Int64.MinValue, Int64.MinValue, Int64.MinValue,
#endif
#if !SAFETYPE_NUMBER
                    MIN_DECIMAL, MIN_DECIMAL, MIN_DECIMAL, MIN_DECIMAL,
#else
                    OracleDecimal.MinValue, OracleDecimal.MinValue, OracleDecimal.MinValue, OracleDecimal.MinValue, 
#endif
                    Single.MinValue, MIN_BINARY_DOUBLE,
                    "A", new string('?', MAX_STRING_SIZE_FOR_VARCHAR_COL), "B", new string('?', MAX_STRING_SIZE_FOR_VARCHAR_COL), "C", new string('?', MAX_STRING_SIZE_FOR_NVARCHAR_COL),
                    "D", new string('?', MAX_STRING_SIZE_FOR_CHAR_COL), "E", new string('?', MAX_STRING_SIZE_FOR_NCHAR_COL),
#if !SAFETYPE_DATE
                    DateTime.MinValue.AddMilliseconds(1),
#else
                    OracleDate.MinValue, 
#endif
#if !SAFETYPE_TIMESTAMP
                    DateTime.MinValue.AddMilliseconds(1),
#else
                    OracleTimeStamp.MinValue.AddMilliseconds(1), 
#endif
#if !SAFETYPE_TIMESTAMP
                    DateTime.MinValue.AddMilliseconds(1),
#else
                    OracleTimeStamp.MinValue.AddMilliseconds(1), 
#endif
#if !SAFETYPE_TIMESTAMP
                    DateTime.MinValue.AddMilliseconds(1),
#else
                    OracleTimeStamp.MinValue.AddMilliseconds(1),
#endif
#if !SAFETYPE_BLOB
                    new Byte[] { 0x00 },
#else
                    oracleBlob,
#endif
#if !SAFETYPE_CLOB
                    "F",
#else
                    oracleClob,
#endif
#if !SAFETYPE_CLOB
                    "G",
#else
                    oracleNClob,
#endif
                    conn);

                // max values for INTEGER, NUMBER, DATE, TIMESTAMP, LOB
#if SAFETYPE_BLOB
                oracleBlob.SetLength(0);
                oracleBlob.Append(Enumerable.Repeat<byte>((byte)0x00, MAX_BYTES_FOR_BLOB_COL).ToArray(), 0, MAX_BYTES_FOR_BLOB_COL);
#endif
#if SAFETYPE_CLOB
                oracleClob.SetLength(0);
                oracleClob.Append(Enumerable.Repeat<char>('?', MAX_STRING_SIZE_FOR_CLOB_COL).ToArray(), 0, MAX_STRING_SIZE_FOR_CLOB_COL);
                //Console.WriteLine("oracleClob.Length: " + oracleClob.Length);
                oracleNClob.SetLength(0);
                oracleNClob.Append(Enumerable.Repeat<char>('?', MAX_STRING_SIZE_FOR_NCLOB_COL).ToArray(), 0, MAX_STRING_SIZE_FOR_NCLOB_COL);
                //Console.WriteLine("oracleNClob.Length: " + oracleNClob.Length);
#endif
                OdptPkgTableBig.Instance.InsertRow(
#if SAFETYPE_INTEGER
                    OracleDecimal.MaxValue, OracleDecimal.Truncate(OracleDecimal.MaxValue, 0), OracleDecimal.Truncate(OracleDecimal.MaxValue, 0), OracleDecimal.Truncate(OracleDecimal.MaxValue, 0), OracleDecimal.Truncate(OracleDecimal.MaxValue, 0), OracleDecimal.Truncate(OracleDecimal.MaxValue, 0),
#elif DECIMAL_INTEGER
                    MAX_DECIMAL, MAX_DECIMAL, MAX_DECIMAL, MAX_DECIMAL, MAX_DECIMAL, MAX_DECIMAL,
#elif SHORT_INTEGER
                    Int32.MaxValue, Int32.MaxValue, Int32.MaxValue, Int32.MaxValue, Int32.MaxValue, Int32.MaxValue,
#else
                    Int64.MaxValue, Int64.MaxValue, Int64.MaxValue, Int64.MaxValue, Int64.MaxValue, Int64.MaxValue,
#endif

#if !SAFETYPE_NUMBER
                    MAX_DECIMAL, MAX_DECIMAL, MAX_DECIMAL, MAX_DECIMAL,
#else
                    OracleDecimal.MaxValue, OracleDecimal.MaxValue, OracleDecimal.MaxValue, OracleDecimal.MaxValue,
#endif
                    Single.MaxValue, MAX_BINARY_DOUBLE,
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

#if !SAFETYPE_BLOB
                    Enumerable.Repeat((byte)0x00, MAX_BYTES_FOR_BLOB_COL).ToArray(),
#else
                    oracleBlob,
#endif
#if !SAFETYPE_CLOB
                    new string('?', MAX_STRING_SIZE_FOR_CLOB_COL),
#else
                    oracleClob,
#endif
#if !SAFETYPE_CLOB
                    new string('?', MAX_STRING_SIZE_FOR_NCLOB_COL),
#else
                    oracleNClob,
#endif
                    conn);

                // filler values
#if SAFETYPE_BLOB
                oracleBlob.SetLength(0);
                oracleBlob.Append(new Byte[] { 0x00 }, 0, 1);
#endif
#if SAFETYPE_CLOB
                oracleClob.SetLength(0);
                oracleClob.Append(new char[] { 'F' }, 0, 1);
                oracleNClob.SetLength(0);
                oracleNClob.Append(new char[] { 'G' }, 0, 1);
#endif
                for (int i = 0; i < TABLE_BIG_ROWS_TO_INSERT; i++) {
                    OdptPkgTableBig.Instance.InsertRow(i + 1, i + 2, i + 3, i + 4,
                        i + 5, i + 6, i + 7, i + 8, i + 9, i + 10, i + 11, i + 12,
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

#if !SAFETYPE_BLOB
                        new Byte[] { 0x00 },
#else
                        oracleBlob,
#endif
#if !SAFETYPE_CLOB
                        "F",
#else
                        oracleClob,
#endif
#if !SAFETYPE_CLOB
                        "G",
#else
                        oracleNClob,
#endif
                        conn);
                }

#if SAFETYPE_BLOB 
                oracleBlob.Dispose();
#endif
#if SAFETYPE_CLOB 
                oracleClob.Dispose();
                oracleNClob.Dispose();
#endif
                if (conn != null) {
                    conn.Close();
                    conn.Dispose();
                }
            }

            private void SeedTableNumber() {
                OdptPkgTableNumber.Instance.TruncTable(null);

                // all explicitly zero values
                OdptPkgTableNumber.Instance.InsertRow(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, null);

                // all explictly null values
                OdptPkgTableNumber.Instance.InsertRow(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                                                        null, null, null, null, null, null, null, null, null, null, null, null, null);
                // min values 
                OdptPkgTableNumber.Instance.InsertRow(MIN_DECIMAL / 100000000,
                                                        -9, -99,
                                                        -999, -9999, -99999,
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
                                                        -9, -99,
                                                        -999, -9999, -99999, -Convert.ToInt32(new string('9', 9)),

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
                                                        -Convert.ToDecimal(new string('9', 1) + "." + new string('8', 20)), /* 38.37 */ MIN_DECIMAL / 100000000, null);

                // max values 
                OdptPkgTableNumber.Instance.InsertRow(MAX_DECIMAL, 9, 99, 999, 9999, 99999,
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

            private void TestCursorFilteredPackageTableBig() {
                uint? rowLimit = TABLE_BIG_ROWS_TO_RETRIEVE;
#if ODPT_FILTER_PREFIX
                ICollection<FilteredPkgTTableBigFiltered> retList, outList, outList2;
#else
                ICollection<TTableBigFiltered> retList, outList, outList2;
#endif
                DataTable retDataTable, outDataTable, outDataTable2;

                retList = OdptPkgTableBig.Instance.GetRowsTypedFltrUsed< 
#if ODPT_FILTER_PREFIX
                    FilteredPkgTTableBigFiltered
#else
                    TTableBigFiltered
#endif
                        >(out outList, out outList2, rowLimit, null);
                Debug.Assert(retList.Count == rowLimit);
                Debug.Assert(outList.Count == rowLimit);
                Debug.Assert(outList2.Count == rowLimit);

                retList = OdptPkgTableBig.Instance.GetRowsUntypedFltrUnused<
#if ODPT_FILTER_PREFIX
                    FilteredPkgTTableBigFiltered, FilteredPkgTTableBigFiltered, FilteredPkgTTableBigFiltered
#else
                    TTableBigFiltered, TTableBigFiltered, TTableBigFiltered
#endif
                        >(out outList, out outList2, false, false, rowLimit, null);
                Debug.Assert(retList.Count == rowLimit);
                Debug.Assert(outList.Count == rowLimit);
                Debug.Assert(outList2.Count == rowLimit);

                retDataTable = OdptPkgTableBig.Instance.GetRowsUntypedFltrUnused(out outDataTable, out outDataTable2, true, rowLimit, null);
                Debug.Assert(retDataTable.Rows.Count == rowLimit);
                Debug.Assert(outDataTable.Rows.Count == rowLimit);
                Debug.Assert(outDataTable2.Rows.Count == rowLimit);
            }

            private void TestCursorTypedTableBig() {
                uint? rowLimit = TABLE_BIG_ROWS_TO_RETRIEVE;
                ICollection<TTableBig> retTableBigList, outTableBigList;
                ICollection<TTableBigChar> outTableBigCharList;

                // static, no mapping
                // ret
                retTableBigList = OdptPkgTableBig.Instance.GetRowsTypedRet<TTableBig>(rowLimit, null);
                Debug.Assert(retTableBigList.Count == rowLimit);

                // 1 out
                OdptPkgTableBig.Instance.GetRowsTypedOut<TTableBig>(out outTableBigList, rowLimit, null);
                Debug.Assert(outTableBigList.Count == rowLimit);

                // ret and 1 out
                retTableBigList = OdptPkgTableBig.Instance.GetRowsTypedOutRet<TTableBig>(out outTableBigList, rowLimit, null);
                Debug.Assert(retTableBigList.Count == rowLimit && outTableBigList.Count == rowLimit);

                // ret and 2 out
                retTableBigList = OdptPkgTableBig.Instance.GetRowsTypedOut2Ret<TTableBig, TTableBigChar>(out outTableBigList, out outTableBigCharList, rowLimit, null);
                Debug.Assert(retTableBigList.Count == rowLimit && outTableBigList.Count == rowLimit && outTableBigCharList.Count == rowLimit);

#if MAPPING_FOR_TYPED_CURSOR
                // mapping
                ICollection<TTableBigMapByPositionAll> retTableBigMapByPositionAllList, outTableBigMapByPositionAllList;
                ICollection<TTableBigMapByPositionPartial> retTableBigMapByPositionPartialList, outTableBigMapByPositionPartialList;
                ICollection<TTableBigCharMapByPositionAll> outTableBigCharMapByPositionAllList;
                ICollection<TTableBigCharMapByPositionPartial> outTableBigCharMapByPositionPartialList;

                // list ret - mapping by name
                retTableBigList = OdptPkgTableBig.Instance.GetRowsTypedRet<TTableBig>(false, false, rowLimit, null);

                // list ret - mapping by position, NOT allowing unmapped columns
                retTableBigMapByPositionAllList = OdptPkgTableBig.Instance.GetRowsTypedRet<TTableBigMapByPositionAll>(true, false, rowLimit, null);

                // list ret - mapping by position, allowing unmapped columns
                retTableBigMapByPositionPartialList = OdptPkgTableBig.Instance.GetRowsTypedRet<TTableBigMapByPositionPartial>(true, true, rowLimit, null);
                Debug.Assert(retTableBigList.Count == rowLimit && retTableBigMapByPositionAllList.Count == rowLimit && retTableBigMapByPositionPartialList.Count == rowLimit);

                // all above cases using 1 out
                outTableBigList = null; outTableBigMapByPositionAllList = null; outTableBigMapByPositionPartialList = null;
                OdptPkgTableBig.Instance.GetRowsTypedOut<TTableBig>(out outTableBigList, false, false, rowLimit, null);
                OdptPkgTableBig.Instance.GetRowsTypedOut<TTableBigMapByPositionAll>(out outTableBigMapByPositionAllList, true, false, rowLimit, null);
                OdptPkgTableBig.Instance.GetRowsTypedOut<TTableBigMapByPositionPartial>(out outTableBigMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(outTableBigList.Count == rowLimit && outTableBigMapByPositionAllList.Count == rowLimit && outTableBigMapByPositionPartialList.Count == rowLimit);

                // all above cases using ret and 1 out arg
                outTableBigList = null; outTableBigMapByPositionAllList = null; outTableBigMapByPositionPartialList = null;
                retTableBigList = OdptPkgTableBig.Instance.GetRowsTypedOutRet<TTableBig>(out outTableBigList, false, false, rowLimit, null);
                retTableBigMapByPositionAllList = OdptPkgTableBig.Instance.GetRowsTypedOutRet<TTableBigMapByPositionAll>(out outTableBigMapByPositionAllList, true, false, rowLimit, null);
                retTableBigMapByPositionPartialList = OdptPkgTableBig.Instance.GetRowsTypedOutRet<TTableBigMapByPositionPartial>(out outTableBigMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(retTableBigList.Count == rowLimit && retTableBigMapByPositionAllList.Count == rowLimit && retTableBigMapByPositionPartialList.Count == rowLimit);
                Debug.Assert(outTableBigList.Count == rowLimit && outTableBigMapByPositionAllList.Count == rowLimit && outTableBigMapByPositionPartialList.Count == rowLimit);

                // all above cases using ret and 2 out args 
                outTableBigList = null; outTableBigMapByPositionAllList = null; outTableBigMapByPositionPartialList = null;
                retTableBigList = OdptPkgTableBig.Instance.GetRowsTypedOut2Ret<TTableBig, TTableBigChar>(out outTableBigList, out outTableBigCharList, false, false, rowLimit, null);
                retTableBigMapByPositionAllList = OdptPkgTableBig.Instance.GetRowsTypedOut2Ret<TTableBigMapByPositionAll, TTableBigCharMapByPositionAll>(out outTableBigMapByPositionAllList, out outTableBigCharMapByPositionAllList, true, false, rowLimit, null);
                retTableBigMapByPositionPartialList = OdptPkgTableBig.Instance.GetRowsTypedOut2Ret<TTableBigMapByPositionPartial, TTableBigCharMapByPositionPartial>(out outTableBigMapByPositionPartialList, out outTableBigCharMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(retTableBigList.Count == rowLimit && retTableBigMapByPositionAllList.Count == rowLimit && retTableBigMapByPositionPartialList.Count == rowLimit);
                Debug.Assert(outTableBigList.Count == rowLimit && outTableBigMapByPositionAllList.Count == rowLimit && outTableBigMapByPositionPartialList.Count == rowLimit);
                Debug.Assert(outTableBigCharList.Count == rowLimit && outTableBigCharMapByPositionAllList.Count == rowLimit && outTableBigCharMapByPositionPartialList.Count == rowLimit);
#endif
            }

            private void TestCursorTypedTableNumber() {
                uint? rowLimit = null;
                ICollection<TTableNumber> retTableNumberList, outTableNumberList;
                ICollection<TTableNumberDec> outTableNumberDecList;

                // static, no mapping
                // ret
                retTableNumberList = OdptPkgTableNumber.Instance.GetRowsTypedRet<TTableNumber>(rowLimit, null);
                Debug.Assert(retTableNumberList.Count > 0);

                // 1 out
                OdptPkgTableNumber.Instance.GetRowsTypedOut<TTableNumber>(out outTableNumberList, rowLimit, null);
                Debug.Assert(outTableNumberList.Count > 0);

                // ret and 1 out
                retTableNumberList = OdptPkgTableNumber.Instance.GetRowsTypedOutRet<TTableNumber>(out outTableNumberList, rowLimit, null);
                Debug.Assert(retTableNumberList.Count > 0 && outTableNumberList.Count > 0);

                // ret and 2 out
                retTableNumberList = OdptPkgTableNumber.Instance.GetRowsTypedOut2Ret<TTableNumber, TTableNumberDec>(out outTableNumberList, out outTableNumberDecList, rowLimit, null);
                Debug.Assert(retTableNumberList.Count > 0 && outTableNumberList.Count > 0 && outTableNumberDecList.Count > 0);

#if MAPPING_FOR_TYPED_CURSOR
                // mapping
                ICollection<TTableNumberMapByPositionAll> retTableNumberMapByPositionAllList, outTableNumberMapByPositionAllList;
                ICollection<TTableNumberMapByPositionPartial> retTableNumberMapByPositionPartialList, outTableNumberMapByPositionPartialList;
                ICollection<TTableNumberDecMapByPositionAll> outTableNumberDecMapByPositionAllList;
                ICollection<TTableNumberDecMapByPositionPartial> outTableNumberDecMapByPositionPartialList;

                // list ret - mapping by name
                retTableNumberList = OdptPkgTableNumber.Instance.GetRowsTypedRet<TTableNumber>(false, false, rowLimit, null);

                // list ret - mapping by position, NOT allowing unmapped columns
                retTableNumberMapByPositionAllList = OdptPkgTableNumber.Instance.GetRowsTypedRet<TTableNumberMapByPositionAll>(true, false, rowLimit, null);

                // list ret - mapping by position, allowing unmapped columns
                retTableNumberMapByPositionPartialList = OdptPkgTableNumber.Instance.GetRowsTypedRet<TTableNumberMapByPositionPartial>(true, true, rowLimit, null);
                Debug.Assert(retTableNumberList.Count > 0 && retTableNumberMapByPositionAllList.Count > 0 && retTableNumberMapByPositionPartialList.Count > 0);

                // all above cases using 1 out
                outTableNumberList = null; outTableNumberMapByPositionAllList = null; outTableNumberMapByPositionPartialList = null;
                OdptPkgTableNumber.Instance.GetRowsTypedOut<TTableNumber>(out outTableNumberList, false, false, rowLimit, null);
                OdptPkgTableNumber.Instance.GetRowsTypedOut<TTableNumberMapByPositionAll>(out outTableNumberMapByPositionAllList, true, false, rowLimit, null);
                OdptPkgTableNumber.Instance.GetRowsTypedOut<TTableNumberMapByPositionPartial>(out outTableNumberMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(outTableNumberList.Count > 0 && outTableNumberMapByPositionAllList.Count > 0 && outTableNumberMapByPositionPartialList.Count > 0);

                // all above cases using ret and 1 out arg
                outTableNumberList = null; outTableNumberMapByPositionAllList = null; outTableNumberMapByPositionPartialList = null;
                retTableNumberList = OdptPkgTableNumber.Instance.GetRowsTypedOutRet<TTableNumber>(out outTableNumberList, false, false, rowLimit, null);
                retTableNumberMapByPositionAllList = OdptPkgTableNumber.Instance.GetRowsTypedOutRet<TTableNumberMapByPositionAll>(out outTableNumberMapByPositionAllList, true, false, rowLimit, null);
                retTableNumberMapByPositionPartialList = OdptPkgTableNumber.Instance.GetRowsTypedOutRet<TTableNumberMapByPositionPartial>(out outTableNumberMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(retTableNumberList.Count > 0 && retTableNumberMapByPositionAllList.Count > 0 && retTableNumberMapByPositionPartialList.Count > 0);
                Debug.Assert(outTableNumberList.Count > 0 && outTableNumberMapByPositionAllList.Count > 0 && outTableNumberMapByPositionPartialList.Count > 0);

                // all above cases using ret and 2 out args 
                outTableNumberList = null; outTableNumberMapByPositionAllList = null; outTableNumberMapByPositionPartialList = null;
                retTableNumberList = OdptPkgTableNumber.Instance.GetRowsTypedOut2Ret<TTableNumber, TTableNumberDec>(out outTableNumberList, out outTableNumberDecList, false, false, rowLimit, null);
                retTableNumberMapByPositionAllList = OdptPkgTableNumber.Instance.GetRowsTypedOut2Ret<TTableNumberMapByPositionAll, TTableNumberDecMapByPositionAll>(out outTableNumberMapByPositionAllList, out outTableNumberDecMapByPositionAllList, true, false, rowLimit, null);
                retTableNumberMapByPositionPartialList = OdptPkgTableNumber.Instance.GetRowsTypedOut2Ret<TTableNumberMapByPositionPartial, TTableNumberDecMapByPositionPartial>(out outTableNumberMapByPositionPartialList, out outTableNumberDecMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(retTableNumberList.Count > 0 && retTableNumberMapByPositionAllList.Count > 0 && retTableNumberMapByPositionPartialList.Count > 0);
                Debug.Assert(outTableNumberList.Count > 0 && outTableNumberMapByPositionAllList.Count > 0 && outTableNumberMapByPositionPartialList.Count > 0);
                Debug.Assert(outTableNumberDecList.Count > 0 && outTableNumberDecMapByPositionAllList.Count > 0 && outTableNumberDecMapByPositionPartialList.Count > 0);
#endif
            }

            private void TestCursorUntypedTableBig() {
                uint? rowLimit = TABLE_BIG_ROWS_TO_RETRIEVE;
                ICollection<TTableBig> retTableBigList, outTableBigList;
                ICollection<TTableBigMapByPositionAll> retTableBigMapByPositionAllList, outTableBigMapByPositionAllList;
                ICollection<TTableBigMapByPositionPartial> retTableBigMapByPositionPartialList, outTableBigMapByPositionPartialList;
                ICollection<TTableBigChar> outTableBigCharList;
                ICollection<TTableBigCharMapByPositionAll> outTableBigCharMapByPositionAllList;
                ICollection<TTableBigCharMapByPositionPartial> outTableBigCharMapByPositionPartialList;
                DataTable retTableBigDataTable, outTableBigDataTable, outTableBigDataTable2;

                // DataTable ret
                retTableBigDataTable = OdptPkgTableBig.Instance.GetRowsUntypedRet(true, rowLimit, null);

                // list ret - mapping by name
                retTableBigList = OdptPkgTableBig.Instance.GetRowsUntypedRet<TTableBig>(false, false, rowLimit, null);

                // list ret - mapping by position, NOT allowing unmapped columns
                retTableBigMapByPositionAllList = OdptPkgTableBig.Instance.GetRowsUntypedRet<TTableBigMapByPositionAll>(true, false, rowLimit, null);

                // list ret - mapping by position, allowing unmapped columns
                retTableBigMapByPositionPartialList = OdptPkgTableBig.Instance.GetRowsUntypedRet<TTableBigMapByPositionPartial>(true, true, rowLimit, null);
                Debug.Assert(retTableBigDataTable.Rows.Count == rowLimit && retTableBigList.Count == rowLimit && retTableBigMapByPositionAllList.Count == rowLimit && retTableBigMapByPositionPartialList.Count == rowLimit);

                // all above cases using 1 out
                outTableBigDataTable = null; outTableBigList = null; outTableBigMapByPositionAllList = null; outTableBigMapByPositionPartialList = null;
                OdptPkgTableBig.Instance.GetRowsUntypedOut(out outTableBigDataTable, true, rowLimit, null);
                OdptPkgTableBig.Instance.GetRowsUntypedOut<TTableBig>(out outTableBigList, false, false, rowLimit, null);
                OdptPkgTableBig.Instance.GetRowsUntypedOut<TTableBigMapByPositionAll>(out outTableBigMapByPositionAllList, true, false, rowLimit, null);
                OdptPkgTableBig.Instance.GetRowsUntypedOut<TTableBigMapByPositionPartial>(out outTableBigMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(outTableBigDataTable.Rows.Count == rowLimit && outTableBigList.Count == rowLimit && outTableBigMapByPositionAllList.Count == rowLimit && outTableBigMapByPositionPartialList.Count == rowLimit);

                // all above cases using ret and 1 out arg
                outTableBigDataTable = null; outTableBigList = null; outTableBigMapByPositionAllList = null; outTableBigMapByPositionPartialList = null;
                retTableBigDataTable = OdptPkgTableBig.Instance.GetRowsUntypedOutRet(out outTableBigDataTable, true, rowLimit, null);
                retTableBigList = OdptPkgTableBig.Instance.GetRowsUntypedOutRet<TTableBig, TTableBig>(out outTableBigList, false, false, rowLimit, null);
                retTableBigMapByPositionAllList = OdptPkgTableBig.Instance.GetRowsUntypedOutRet<TTableBigMapByPositionAll, TTableBigMapByPositionAll>(out outTableBigMapByPositionAllList, true, false, rowLimit, null);
                retTableBigMapByPositionPartialList = OdptPkgTableBig.Instance.GetRowsUntypedOutRet<TTableBigMapByPositionPartial, TTableBigMapByPositionPartial>(out outTableBigMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(retTableBigDataTable.Rows.Count == rowLimit && retTableBigList.Count == rowLimit && retTableBigMapByPositionAllList.Count == rowLimit && retTableBigMapByPositionPartialList.Count == rowLimit);
                Debug.Assert(outTableBigDataTable.Rows.Count == rowLimit && outTableBigList.Count == rowLimit && outTableBigMapByPositionAllList.Count == rowLimit && outTableBigMapByPositionPartialList.Count == rowLimit);

                // all above cases using ret and 2 out args 
                outTableBigDataTable = null; outTableBigList = null; outTableBigMapByPositionAllList = null; outTableBigMapByPositionPartialList = null;
                retTableBigDataTable = OdptPkgTableBig.Instance.GetRowsUntypedOut2Ret(out outTableBigDataTable, out outTableBigDataTable2, true, rowLimit, null);
                retTableBigList = OdptPkgTableBig.Instance.GetRowsUntypedOut2Ret<TTableBig, TTableBig, TTableBigChar>(out outTableBigList, out outTableBigCharList, false, false, rowLimit, null);
                retTableBigMapByPositionAllList = OdptPkgTableBig.Instance.GetRowsUntypedOut2Ret<TTableBigMapByPositionAll, TTableBigMapByPositionAll, TTableBigCharMapByPositionAll>(out outTableBigMapByPositionAllList, out outTableBigCharMapByPositionAllList, true, false, rowLimit, null);
                retTableBigMapByPositionPartialList = OdptPkgTableBig.Instance.GetRowsUntypedOut2Ret<TTableBigMapByPositionPartial, TTableBigMapByPositionPartial, TTableBigCharMapByPositionPartial>(out outTableBigMapByPositionPartialList, out outTableBigCharMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(retTableBigDataTable.Rows.Count == rowLimit && retTableBigList.Count == rowLimit && retTableBigMapByPositionAllList.Count == rowLimit && retTableBigMapByPositionPartialList.Count == rowLimit);
                Debug.Assert(outTableBigDataTable.Rows.Count == rowLimit && outTableBigList.Count == rowLimit && outTableBigMapByPositionAllList.Count == rowLimit && outTableBigMapByPositionPartialList.Count == rowLimit);
                Debug.Assert(outTableBigDataTable2.Rows.Count == rowLimit && outTableBigCharList.Count == rowLimit && outTableBigCharMapByPositionAllList.Count == rowLimit && outTableBigCharMapByPositionPartialList.Count == rowLimit);
            }

            private void TestCursorUntypedTableNumber() {
                uint? rowLimit = null;
                ICollection<TTableNumber> retTableNumberList, outTableNumberList;
                ICollection<TTableNumberMapByPositionAll> retTableNumberMapByPositionAllList, outTableNumberMapByPositionAllList;
                ICollection<TTableNumberMapByPositionPartial> retTableNumberMapByPositionPartialList, outTableNumberMapByPositionPartialList;
                ICollection<TTableNumberDec> outTableNumberDecList;
                ICollection<TTableNumberDecMapByPositionAll> outTableNumberDecMapByPositionAllList;
                ICollection<TTableNumberDecMapByPositionPartial> outTableNumberDecMapByPositionPartialList;
                DataTable retTableNumberDataTable, outTableNumberDataTable, outTableNumberDataTable2;

                // DataTable ret
                retTableNumberDataTable = OdptPkgTableNumber.Instance.GetRowsUntypedRet(true, rowLimit, null);

                // list ret - mapping by name
                retTableNumberList = OdptPkgTableNumber.Instance.GetRowsUntypedRet<TTableNumber>(false, false, rowLimit, null);

                // list ret - mapping by position, NOT allowing unmapped columns
                retTableNumberMapByPositionAllList = OdptPkgTableNumber.Instance.GetRowsUntypedRet<TTableNumberMapByPositionAll>(true, false, rowLimit, null);

                // list ret - mapping by position, allowing unmapped columns
                retTableNumberMapByPositionPartialList = OdptPkgTableNumber.Instance.GetRowsUntypedRet<TTableNumberMapByPositionPartial>(true, true, rowLimit, null);
                Debug.Assert(retTableNumberDataTable.Rows.Count > 0 && retTableNumberList.Count > 0 && retTableNumberMapByPositionAllList.Count > 0 && retTableNumberMapByPositionPartialList.Count > 0);

                // all above cases using 1 out
                outTableNumberDataTable = null; outTableNumberList = null; outTableNumberMapByPositionAllList = null; outTableNumberMapByPositionPartialList = null;
                OdptPkgTableNumber.Instance.GetRowsUntypedOut(out outTableNumberDataTable, true, rowLimit, null);
                OdptPkgTableNumber.Instance.GetRowsUntypedOut<TTableNumber>(out outTableNumberList, false, false, rowLimit, null);
                OdptPkgTableNumber.Instance.GetRowsUntypedOut<TTableNumberMapByPositionAll>(out outTableNumberMapByPositionAllList, true, false, rowLimit, null);
                OdptPkgTableNumber.Instance.GetRowsUntypedOut<TTableNumberMapByPositionPartial>(out outTableNumberMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(outTableNumberDataTable.Rows.Count > 0 && outTableNumberList.Count > 0 && outTableNumberMapByPositionAllList.Count > 0 && outTableNumberMapByPositionPartialList.Count > 0);

                // all above cases using ret and 1 out arg
                outTableNumberDataTable = null; outTableNumberList = null; outTableNumberMapByPositionAllList = null; outTableNumberMapByPositionPartialList = null;
                retTableNumberDataTable = OdptPkgTableNumber.Instance.GetRowsUntypedOutRet(out outTableNumberDataTable, true, rowLimit, null);
                retTableNumberList = OdptPkgTableNumber.Instance.GetRowsUntypedOutRet<TTableNumber, TTableNumber>(out outTableNumberList, false, false, rowLimit, null);
                retTableNumberMapByPositionAllList = OdptPkgTableNumber.Instance.GetRowsUntypedOutRet<TTableNumberMapByPositionAll, TTableNumberMapByPositionAll>(out outTableNumberMapByPositionAllList, true, false, rowLimit, null);
                retTableNumberMapByPositionPartialList = OdptPkgTableNumber.Instance.GetRowsUntypedOutRet<TTableNumberMapByPositionPartial, TTableNumberMapByPositionPartial>(out outTableNumberMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(retTableNumberDataTable.Rows.Count > 0 && retTableNumberList.Count > 0 && retTableNumberMapByPositionAllList.Count > 0 && retTableNumberMapByPositionPartialList.Count > 0);
                Debug.Assert(outTableNumberDataTable.Rows.Count > 0 && outTableNumberList.Count > 0 && outTableNumberMapByPositionAllList.Count > 0 && outTableNumberMapByPositionPartialList.Count > 0);

                // all above cases using ret and 2 out args 
                outTableNumberDataTable = null; outTableNumberList = null; outTableNumberMapByPositionAllList = null; outTableNumberMapByPositionPartialList = null;
                retTableNumberDataTable = OdptPkgTableNumber.Instance.GetRowsUntypedOut2Ret(out outTableNumberDataTable, out outTableNumberDataTable2, true, rowLimit, null);
                retTableNumberList = OdptPkgTableNumber.Instance.GetRowsUntypedOut2Ret<TTableNumber, TTableNumber, TTableNumberDec>(out outTableNumberList, out outTableNumberDecList, false, false, rowLimit, null);
                retTableNumberMapByPositionAllList = OdptPkgTableNumber.Instance.GetRowsUntypedOut2Ret<TTableNumberMapByPositionAll, TTableNumberMapByPositionAll, TTableNumberDecMapByPositionAll>(out outTableNumberMapByPositionAllList, out outTableNumberDecMapByPositionAllList, true, false, rowLimit, null);
                retTableNumberMapByPositionPartialList = OdptPkgTableNumber.Instance.GetRowsUntypedOut2Ret<TTableNumberMapByPositionPartial, TTableNumberMapByPositionPartial, TTableNumberDecMapByPositionPartial>(out outTableNumberMapByPositionPartialList, out outTableNumberDecMapByPositionPartialList, true, true, rowLimit, null);
                Debug.Assert(retTableNumberDataTable.Rows.Count > 0 && retTableNumberList.Count > 0 && retTableNumberMapByPositionAllList.Count > 0 && retTableNumberMapByPositionPartialList.Count > 0);
                Debug.Assert(outTableNumberDataTable.Rows.Count > 0 && outTableNumberList.Count > 0 && outTableNumberMapByPositionAllList.Count > 0 && outTableNumberMapByPositionPartialList.Count > 0);
                Debug.Assert(outTableNumberDataTable2.Rows.Count > 0 && outTableNumberDecList.Count > 0 && outTableNumberDecMapByPositionAllList.Count > 0 && outTableNumberDecMapByPositionPartialList.Count > 0);
            }
            #endregion

            #region Tests for isolated data types (built-ins, ODP.NET SafeTypes, etc.) and respective associative arrays
            /// <summary>
            /// Test use of Int32
            /// </summary>
            private void TestInt32Calls() {
                Int32? pInInt = 0, pInOutInt = 0, pOutInt, retInt;
                IList<Int32?> pInListInt = new List<Int32?>(), pInOutListInt = new List<Int32?>(), pOutListInt = new List<Int32?>();
                IList<Int32?> intTestValues = new List<Int32?>() { Int32.MaxValue, Int32.MinValue, 0, null };

                // BINARY_INTEGER and equivalents

                // BINARY_INTEGER
                // standard call
                foreach (Int32? it in intTestValues) {
                    pInInt = pInOutInt = it;
                    retInt = OdptPkgMain.Instance.FuncBinaryInteger(pInInt, ref pInOutInt, out pOutInt, null);
                    Debug.Assert(pInInt.Equals(pInOutInt) && pInInt.Equals(pOutInt) && pInInt.Equals(retInt));
                }

                // PLS_INTEGER
                // standard call
                foreach (Int32? it in intTestValues) {
                    pInInt = pInOutInt = it;
                    retInt = OdptPkgMain.Instance.FuncPlsInteger(pInInt, ref pInOutInt, out pOutInt, null);
                    Debug.Assert(pInInt.Equals(pInOutInt) && pInInt.Equals(pOutInt) && pInInt.Equals(retInt));
                }

                // NATURAL, POSITIVE
                foreach (Int32? it in new List<Int32?>() { Int32.MaxValue, 0, null }) { // exclude negative values for test
                    pInInt = pInOutInt = it;
                    retInt = OdptPkgMain.Instance.FuncNatural(pInInt, ref pInOutInt, out pOutInt, null);
                    Debug.Assert(pInInt.Equals(pInOutInt) && pInInt.Equals(pOutInt) && pInInt.Equals(retInt));
                }

                foreach (Int32? it in new List<Int32?>() { Int32.MaxValue, null }) { // exclude non-positive values for test
                    pInInt = pInOutInt = it;
                    retInt = OdptPkgMain.Instance.FuncPositive(pInInt, ref pInOutInt, out pOutInt, null);
                    Debug.Assert(pInInt.Equals(pInOutInt) && pInInt.Equals(pOutInt) && pInInt.Equals(retInt));
                }

                // NATURALN, POSITIVEN - functions are failing in package regardless of value
                foreach (Int32? it in new List<Int32?>() { Int32.MaxValue, 0 }) { // exclude negative and null values for test
                    pInInt = pInOutInt = it;
                    //retInt = OdptPkgMain.Instance.FuncNaturaln(pInInt, ref pInOutInt, out pOutInt, null);
                    //Debug.Assert(pInInt.Equals(pInOutInt) && pInInt.Equals(pOutInt) && pInInt.Equals(retInt));
                }

                foreach (Int32? it in new List<Int32?>() { Int32.MaxValue }) { // exclude non-positive and null values for test
                    pInInt = pInOutInt = it;
                    //retInt = OdptPkgMain.Instance.FuncPositiven(pInInt, ref pInOutInt, out pOutInt, null);
                    //Debug.Assert(pInInt.Equals(pInOutInt) && pInInt.Equals(pOutInt) && pInInt.Equals(retInt));
                }

                // binding an associative array of BINARY_INTEGER and PLS_INTEGER or their subtypes is not suported by ODP.NET
            }

            /// <summary>
            /// Test use of Int64
            /// </summary>
            private void TestInt64Calls() {
#if SAFETYPE_INTEGER
                OracleDecimal? pIn, pInOut, pOut, ret;
                IList<OracleDecimal?> pInList = new List<OracleDecimal?>(), pInOutList = new List<OracleDecimal?>(), pOutList = new List<OracleDecimal?>(), retList;
                IList<OracleDecimal?> testValues = new List<OracleDecimal?>() { OracleDecimal.Truncate(OracleDecimal.MaxValue, 0), OracleDecimal.Truncate(OracleDecimal.MinValue, 0), 0, OracleDecimal.Null };
#elif DECIMAL_INTEGER
                Decimal? pIn, pInOut, pOut, ret;
                IList<Decimal?> pInList = new List<Decimal?>(), pInOutList = new List<Decimal?>(), pOutList = new List<Decimal?>(), retList;
                IList<Decimal?> testValues = new List<Decimal?>() { MAX_DECIMAL, MIN_DECIMAL, 0, null };
#elif SHORT_INTEGER
                Int32? pIn, pInOut, pOut, ret;
                IList<Int32?> pInList = new List<Int32?>(), pInOutList = new List<Int32?>(), pOutList = new List<Int32?>(), retList;
                IList<Int32?> testValues = new List<Int32?>() { Int32.MaxValue, Int32.MinValue, 0, null };
#else
                Int64? pIn, pInOut, pOut, ret;
                IList<Int64?> pInList = new List<Int64?>(), pInOutList = new List<Int64?>(), pOutList = new List<Int64?>(), retList;
                IList<Int64?> testValues = new List<Int64?>() { Int64.MaxValue, Int64.MinValue, 0, null };
#endif
                // INTEGER and equivalents

                // INTEGER
                // standard call
                for (int i = 0; i < testValues.Count; i++) {
                    pIn = testValues[i]; pInOut = null;
                    ret = OdptPkgMain.Instance.FuncInteger(pIn, ref pInOut, out pOut, null);
                    Debug.Assert(pIn.Equals(pInOut) && pIn.Equals(pOut) && pIn.Equals(ret));
                }

                // assoc array (INTEGER indexed)
                pInList = pInOutList = testValues;
                retList = OdptPkgMain.Instance.FuncAaInteger(pInList, ref pInOutList, out pOutList, null);
                for (int i = 0; i < pInList.Count; i++) Debug.Assert(pInList[i].Equals(pInOutList[i]));
                for (int i = 0; i < pInList.Count; i++) Debug.Assert(pInList[i].Equals(pOutList[i]));
                for (int i = 0; i < pInList.Count; i++) Debug.Assert(pInList[i].Equals(retList[i]));

                // VARCHAR2 indexed associative array will not execute successfully due to limiation of ODP.NET
                //pInList = testValues;
                //pInOutList = testValues;
                //retList = OdptPkgMain.Instance.FuncAaIntegerV(pInList, ref pInOutList, out pOutList, null);
                //for (int i = 0; i < pInList.Count; i++) if (!pInList[i].Equals(pInOutList[i])) throw new Exception("Error");
                //for (int i = 0; i < pInList.Count; i++) if (!pInList[i].Equals(pOutList[i])) throw new Exception("Error");
                //for (int i = 0; i < pInList.Count; i++) if (!pInList[i].Equals(retList[i])) throw new Exception("Error");

                // INT
                // stanard call
                for (int i = 0; i < testValues.Count; i++) {
                    pIn = testValues[i]; pInOut = null;
                    ret = OdptPkgMain.Instance.FuncInt(pIn, ref pInOut, out pOut, null);
                    Debug.Assert(pIn.Equals(pInOut) && pIn.Equals(pOut) && pIn.Equals(ret));
                }

                // SMALLINT
                // standard call
                for (int i = 0; i < testValues.Count; i++) {
                    pIn = testValues[i]; pInOut = null;
                    ret = OdptPkgMain.Instance.FuncSmallint(pIn, ref pInOut, out pOut, null);
                    Debug.Assert(pIn.Equals(pInOut) && pIn.Equals(pOut) && pIn.Equals(ret));
                }

                // assoc array
                pInList = pInOutList = testValues;
                retList = OdptPkgMain.Instance.FuncAaSmallint(pInList, ref pInOutList, out pOutList, null);
                for (int i = 0; i < pInList.Count; i++) {
                    Debug.Assert(pInList[i].Equals(pInOutList[i]));
                    Debug.Assert(pInList[i].Equals(pOutList[i]));
                    Debug.Assert(pInList[i].Equals(retList[i]));
                }

                // DECIMAL
                // standard call
                for (int i = 0; i < testValues.Count; i++) {
                    pIn = testValues[i]; pInOut = null;
                    ret = OdptPkgMain.Instance.FuncDecimal(pIn, ref pInOut, out pOut, null);
                    Debug.Assert(pIn.Equals(pInOut) && pIn.Equals(pOut) && pIn.Equals(ret));
                }

                // assoc array 
                pInList = pInOutList = testValues;
                retList = OdptPkgMain.Instance.FuncAaDecimal(pInList, ref pInOutList, out pOutList, null);
                for (int i = 0; i < pInList.Count; i++) Debug.Assert(pInList[i].Equals(pInOutList[i]));
                for (int i = 0; i < pInList.Count; i++) Debug.Assert(pInList[i].Equals(pOutList[i]));
                for (int i = 0; i < pInList.Count; i++) Debug.Assert(pInList[i].Equals(retList[i]));

                // NUMERIC
                // standard call
                for (int i = 0; i < testValues.Count; i++) {
                    pIn = testValues[i]; pInOut = null;
                    ret = OdptPkgMain.Instance.FuncNumeric(pIn, ref pInOut, out pOut, null);
                    Debug.Assert(pIn.Equals(pInOut) && pIn.Equals(pOut) && pIn.Equals(ret));
                }

                // assoc array
                pInList = pInOutList = testValues;
                retList = OdptPkgMain.Instance.FuncAaNumeric(pInList, ref pInOutList, out pOutList, null);
                for (int i = 0; i < pInList.Count; i++) Debug.Assert(pInList[i].Equals(pInOutList[i]));
                for (int i = 0; i < pInList.Count; i++) Debug.Assert(pInList[i].Equals(pOutList[i]));
                for (int i = 0; i < pInList.Count; i++) Debug.Assert(pInList[i].Equals(retList[i]));
            }

            /// <summary>
            /// Test use of Decimal, OracleDecimal
            /// </summary>
            private void TestDecimalCalls() {
#if SAFETYPE_NUMBER
                OracleDecimal? pInDecimal, pInOutDecimal, pOutDecimal, retDecimal;
                IList<OracleDecimal?> pInListDecimal, pInOutListDecimal, pOutListDecimal, retListDecimal;
                IList<OracleDecimal?> decimalTestValues = new List<OracleDecimal?>() { OracleDecimal.Null, OracleDecimal.MinValue, 0.0M, OracleDecimal.MaxValue };
#else
                Decimal? pInDecimal, pInOutDecimal, pOutDecimal, retDecimal;
                IList<Decimal?> pInListDecimal, pInOutListDecimal, pOutListDecimal, retListDecimal;
                IList<Decimal?> decimalTestValues = new List<Decimal?>() { null, MIN_DECIMAL, 0.0M, MAX_DECIMAL };
#endif
                // NUMBER and equivalents

                // NUMBER
                // standard call
                for (int i = 0; i < decimalTestValues.Count; i++) {
                    pInDecimal = pInOutDecimal = decimalTestValues[i];
                    retDecimal = OdptPkgMain.Instance.FuncNumber(pInDecimal, ref pInOutDecimal, out pOutDecimal, null);
                    Debug.Assert(pInDecimal.Equals(pInOutDecimal) || pInDecimal.Equals(pOutDecimal) || pInDecimal.Equals(retDecimal));
                }

                // assoc array
                pInListDecimal = pInOutListDecimal = decimalTestValues;
                retListDecimal = OdptPkgMain.Instance.FuncAaNumber(pInListDecimal, ref pInOutListDecimal, out pOutListDecimal, null);
                for (int i = 0; i < pInListDecimal.Count; i++) Debug.Assert(pInListDecimal[i].Equals(pInOutListDecimal[i]));
                for (int i = 0; i < pInListDecimal.Count; i++) Debug.Assert(pInListDecimal[i].Equals(pOutListDecimal[i]));
                for (int i = 0; i < pInListDecimal.Count; i++) Debug.Assert(pInListDecimal[i].Equals(retListDecimal[i]));

                // FLOAT
                // standard call
                for (int i = 0; i < decimalTestValues.Count; i++) {
                    pInDecimal = pInOutDecimal = decimalTestValues[i];
                    retDecimal = OdptPkgMain.Instance.FuncFloat(pInDecimal, ref pInOutDecimal, out pOutDecimal, null);
                    Debug.Assert(pInDecimal.Equals(pInOutDecimal) && pInDecimal.Equals(pOutDecimal) && pInDecimal.Equals(retDecimal));
                }

                // assoc array
                pInListDecimal = pInOutListDecimal = decimalTestValues;
                retListDecimal = OdptPkgMain.Instance.FuncAaFloat(pInListDecimal, ref pInOutListDecimal, out pOutListDecimal, null);
                for (int i = 0; i < pInListDecimal.Count; i++) Debug.Assert(pInListDecimal[i].Equals(pInOutListDecimal[i]));
                for (int i = 0; i < pInListDecimal.Count; i++) Debug.Assert(pInListDecimal[i].Equals(pOutListDecimal[i]));
                for (int i = 0; i < pInListDecimal.Count; i++) Debug.Assert(pInListDecimal[i].Equals(retListDecimal[i]));

                // REAL
                // standard call
                for (int i = 0; i < decimalTestValues.Count; i++) {
                    pInDecimal = pInOutDecimal = decimalTestValues[i];
                    retDecimal = OdptPkgMain.Instance.FuncReal(pInDecimal, ref pInOutDecimal, out pOutDecimal, null);
                    Debug.Assert(pInDecimal.Equals(pInOutDecimal) && pInDecimal.Equals(pOutDecimal) && pInDecimal.Equals(retDecimal));
                }

                // assoc array
                pInListDecimal = pInOutListDecimal = decimalTestValues;
                retListDecimal = OdptPkgMain.Instance.FuncAaReal(pInListDecimal, ref pInOutListDecimal, out pOutListDecimal, null);
                for (int i = 0; i < pInListDecimal.Count; i++) Debug.Assert(pInListDecimal[i].Equals(pInOutListDecimal[i]));
                for (int i = 0; i < pInListDecimal.Count; i++) Debug.Assert(pInListDecimal[i].Equals(pOutListDecimal[i]));
                for (int i = 0; i < pInListDecimal.Count; i++) Debug.Assert(pInListDecimal[i].Equals(retListDecimal[i]));

                // DOUBLE_PRECISION
                // standard call
                for (int i = 0; i < decimalTestValues.Count; i++) {
                    pInDecimal = pInOutDecimal = decimalTestValues[i];
                    retDecimal = OdptPkgMain.Instance.FuncDoublePrecision(pInDecimal, ref pInOutDecimal, out pOutDecimal, null);
                    Debug.Assert(pInDecimal.Equals(pInOutDecimal) && pInDecimal.Equals(pOutDecimal) && pInDecimal.Equals(retDecimal));
                }

                // assoc array
                pInListDecimal = pInOutListDecimal = decimalTestValues;
                retListDecimal = OdptPkgMain.Instance.FuncAaDoublePrecision(pInListDecimal, ref pInOutListDecimal, out pOutListDecimal, null);
                for (int i = 0; i < pInListDecimal.Count; i++) Debug.Assert(pInListDecimal[i].Equals(pInOutListDecimal[i]));
                for (int i = 0; i < pInListDecimal.Count; i++) Debug.Assert(pInListDecimal[i].Equals(pOutListDecimal[i]));
                for (int i = 0; i < pInListDecimal.Count; i++) Debug.Assert(pInListDecimal[i].Equals(retListDecimal[i]));
            }

            /// <summary>
            /// Test use of Double
            /// </summary>
            private void TestDoubleCalls() {
                Double? pInDouble, pInOutDouble, pOutDouble, retDouble;
                IList<Double?> pInListDouble = new List<Double?>(), pInOutListDouble = new List<Double?>(), pOutListDouble = new List<Double?>(), retListDouble;

                // BINARY_DOUBLE - underlying OracleDecimal has max scale of 127 which restricts the C# double range

                // call fails with arithmetic overflow inside proc before returning to .NET
                //Double? binaryDoubleMinNormal, binaryDoubleMaxNormal;
                //OdptPkgMain.Instance.ProcBinaryDoubleConst(out binaryDoubleMinNormal, out binaryDoubleMaxNormal);
                //pInDouble = 1.7976931348623157e308d; // BINARY_DOUBLE_MAX_NORMAL fails to return
                //pInDouble = 2.2250738585072014e-308d; // BINARY_DOUBLE_MIN_NORMAL fails to return

                IList<Double?> doubleTestValues = new List<Double?>() { MAX_BINARY_DOUBLE, MIN_BINARY_DOUBLE, 0.0d, null, Double.NaN };

                // BINARY_DOUBLE
                // standard call
                foreach (Double? dt in doubleTestValues) {
                    pInDouble = pInOutDouble = dt;
                    retDouble = OdptPkgMain.Instance.FuncBinaryDouble(pInDouble, ref pInOutDouble, out pOutDouble, null);
                    Debug.Assert(Util.IsEqual(pInDouble, pInOutDouble) && Util.IsEqual(pInDouble, pOutDouble) && Util.IsEqual(pInDouble, retDouble));
                }

                // assoc array
                pInListDouble = pInOutListDouble = doubleTestValues;
                retListDouble = OdptPkgMain.Instance.FuncAaBinaryDouble(pInListDouble, ref pInOutListDouble, out pOutListDouble, null);
                for (int i = 0; i < pInListDouble.Count; i++) {
                    Debug.Assert(Util.IsEqual(pInListDouble[i], pInOutListDouble[i]));
                    Debug.Assert(Util.IsEqual(pInListDouble[i], pOutListDouble[i]));
                    Debug.Assert(Util.IsEqual(pInListDouble[i], retListDouble[i])); 
                }
            }

            /// <summary>
            /// Test use of Single(float)
            /// </summary>
            private void TestSingleCalls() {
                Single? pInSingle, pInOutSingle, pOutSingle, retSingle;
                IList<Single?> pInListSingle = new List<Single?>(), pInOutListSingle = new List<Single?>(), pOutListSingle = new List<Single?>(), retListSingle;

                // BINARY_FLOAT
                Single? binaryFloatMinNormal, binaryFloatMaxNormal;
                OdptPkgMain.Instance.ProcBinaryFloatConst(out binaryFloatMinNormal, out binaryFloatMaxNormal, null);

                IList<Single?> singleTestValues = new List<Single?>() { binaryFloatMaxNormal, binaryFloatMinNormal, 0.0f, null, Single.NaN };

                // standard call
                foreach (Single? st in singleTestValues) {
                    pInSingle = pInOutSingle = st;
                    retSingle = OdptPkgMain.Instance.FuncBinaryFloat(pInSingle, ref pInOutSingle, out pOutSingle, null);
                    Debug.Assert(Util.IsEqual(pInSingle, pInOutSingle) && Util.IsEqual(pInSingle, pOutSingle) && Util.IsEqual(pInSingle, retSingle));
                }

                // assoc array
                pInListSingle = pInOutListSingle = singleTestValues;
                retListSingle = OdptPkgMain.Instance.FuncAaBinaryFloat(pInListSingle, ref pInOutListSingle, out pOutListSingle, null);
                for (int i = 0; i < pInListSingle.Count; i++) Debug.Assert(Util.IsEqual(pInListSingle[i], pInOutListSingle[i]));
                for (int i = 0; i < pInListSingle.Count; i++) Debug.Assert(Util.IsEqual(pInListSingle[i], pOutListSingle[i]));
                for (int i = 0; i < pInListSingle.Count; i++) Debug.Assert(Util.IsEqual(pInListSingle[i], retListSingle[i]));
            }

            /// <summary>
            /// Test use of String 
            /// </summary>
            private void TestStringCalls() {
                String pInString, pInOutString, pOutString, retString;
                IList<String> pInListString = new List<String>(), pInOutListString = new List<String>(), pOutListString, retListString;

                // VARCHAR
                // standard call
                pInString = pInOutString = new string('?', MAX_STRING_SIZE_FOR_VARCHAR_ARG);
                retString = OdptPkgMain.Instance.FuncVarchar(pInString, ref pInOutString, out pOutString, null);
                Debug.Assert(pInString.Equals(pInOutString) && pInString.Equals(pOutString) && pInString.Equals(retString));

                // VARCHAR2
                // standard call
                pInString = pInOutString = new string('?', MAX_STRING_SIZE_FOR_VARCHAR_ARG);
                retString = OdptPkgMain.Instance.FuncVarchar2(pInString, ref pInOutString, out pOutString, null);
                Debug.Assert(pInString.Equals(pInOutString) && pInString.Equals(pOutString) && pInString.Equals(retString));

                // assoc array
                pInListString = pInOutListString = new List<String>() { null, "TEST", new string('?', MAX_STRING_SIZE_FOR_VARCHAR_IN_ASSOC_ARRAY) };
                pOutListString = null;
                retListString = OdptPkgMain.Instance.FuncAaVarchar2(pInListString, ref pInOutListString, out pOutListString, null);
                Debug.Assert(pInListString.Except(pInOutListString).Count() == 0 && pInListString.Except(pOutListString).Count() == 0 && pInListString.Except(retListString).Count() == 0);

                // NVARCHAR2
                // standard call
                pInString = pInOutString = new string('?', MAX_STRING_SIZE_FOR_VARCHAR_ARG);
                retString = OdptPkgMain.Instance.FuncNvarchar2(pInString, ref pInOutString, out pOutString, null);
                Debug.Assert(pInString.Equals(pInOutString) && pInString.Equals(pOutString) && pInString.Equals(retString));

                // assoc array
                pInListString = pInOutListString = new List<String>() { null, "TEST", new string('?', MAX_STRING_SIZE_FOR_NVARCHAR_IN_ASSOC_ARRAY) };
                pOutListString = null;
                retListString = OdptPkgMain.Instance.FuncAaNvarchar2(pInListString, ref pInOutListString, out pOutListString, null);
                Debug.Assert(pInListString.Except(pInOutListString).Count() == 0 && pInListString.Except(pOutListString).Count() == 0 && pInListString.Except(retListString).Count() == 0);

                // STRING
                // standard call
                pInString = pInOutString = new string('?', MAX_STRING_SIZE_FOR_VARCHAR_ARG);
                retString = OdptPkgMain.Instance.FuncString(pInString, ref pInOutString, out pOutString, null);
                Debug.Assert(pInString.Equals(pInOutString) && pInString.Equals(pOutString) && pInString.Equals(retString));

                // assoc array
                pInListString = pInOutListString = new List<String>() { null, "TEST", new string('?', MAX_STRING_SIZE_FOR_VARCHAR_IN_ASSOC_ARRAY) };
                pOutListString = null;
                retListString = OdptPkgMain.Instance.FuncAaString(pInListString, ref pInOutListString, out pOutListString, null);
                Debug.Assert(pInListString.Except(pInOutListString).Count() == 0 && pInListString.Except(pOutListString).Count() == 0 && pInListString.Except(retListString).Count() == 0);

                // CHAR
                // standard call
                pInString = pInOutString = new string('?', MAX_STRING_SIZE_FOR_CHAR);
                retString = OdptPkgMain.Instance.FuncChar(pInString, ref pInOutString, out pOutString, null);
                Debug.Assert(pInString.Equals(pInOutString) && pInString.Equals(pOutString) && pInString.Equals(retString));

                // assoc array
                pInListString = pInOutListString = new List<String>() { null, "TEST", new string('?', MAX_STRING_SIZE_FOR_CHAR_IN_ASSOC_ARRAY) };
                pOutListString = null;
                retListString = OdptPkgMain.Instance.FuncAaChar(pInListString, ref pInOutListString, out pOutListString, null);
                Debug.Assert(pInListString.Except(pInOutListString).Count() == 0 && pInListString.Except(pOutListString).Count() == 0 && pInListString.Except(retListString).Count() == 0);

                // NCHAR
                // standard call
                pInString = pInOutString = new string('?', MAX_STRING_SIZE_FOR_CHAR);
                retString = OdptPkgMain.Instance.FuncNchar(pInString, ref pInOutString, out pOutString, null);
                Debug.Assert(pInString.Equals(pInOutString) && pInString.Equals(pOutString) && pInString.Equals(retString));

                // assoc array
                pInListString = pInOutListString = new List<String>() { null, "TEST", new string('?', MAX_STRING_SIZE_FOR_NCHAR_IN_ASSOC_ARRAY) };
                pOutListString = null;
                retListString = OdptPkgMain.Instance.FuncAaNchar(pInListString, ref pInOutListString, out pOutListString, null);
                Debug.Assert(pInListString.Except(pInOutListString).Count() == 0 && pInListString.Except(pOutListString).Count() == 0 && pInListString.Except(retListString).Count() == 0);
            }

            /// <summary>
            /// Test use of String LOB
            /// </summary>
            private void TestStringLobCalls() {
                OracleConnection conn =
#if SAFETYPE_CLOB
                    Database.Instance.GetConnection();  // connection required to instantiate LOB classes
#else
                    null;
#endif
#if SAFETYPE_CLOB
                OracleClob
#else
                String
#endif
                            pInString, pInOutString, pOutString, retString;
#if SAFETYPE_CLOB
                pInString = new OracleClob(conn);
                pInOutString = new OracleClob(conn);
                pOutString = new OracleClob(conn);
                retString = new OracleClob(conn);
#endif
#if SAFETYPE_CLOB // for ODP.NET safe type, set in and in/out argument one time for all calls below
                pInString.Append(Enumerable.Repeat<char>('?', MAX_STRING_SIZE_FOR_CLOB_COL).ToArray(), 0, MAX_STRING_SIZE_FOR_CLOB_COL);
                pInOutString = (OracleClob)pInString.Clone();
#endif
                // CLOB
                // standard call
#if !SAFETYPE_CLOB
                pInString = pInOutString = "CLOB IN ARG " + new string('?', MAX_STRING_SIZE_FOR_CLOB_COL);
#endif
                retString = OdptPkgMain.Instance.FuncClob(pInString, ref pInOutString, out pOutString, conn);
                Debug.Assert(
#if SAFETYPE_CLOB
                    (pInString.Value.Equals(pInOutString.Value)) && (pInString.Value.Equals(pOutString.Value)) && (pInString.Value.Equals(retString.Value))
#else
                    pInString.Equals(pInOutString) && pInString.Equals(pOutString) && pInString.Equals(retString)
#endif
                    );

                // NCLOB
                // standard call
#if !SAFETYPE_CLOB
                pInString = pInOutString = "NCLOB IN ARG " + new string('?', MAX_STRING_SIZE_FOR_CLOB_COL);
#endif
                retString = OdptPkgMain.Instance.FuncNclob(pInString, ref pInOutString, out pOutString, conn);
                Debug.Assert(
#if SAFETYPE_CLOB
                    (pInString.Value.Equals(pInOutString.Value))
                        && (pInString.Value.Equals(pOutString.Value)) && (pInString.Value.Equals(retString.Value))
#else
                    pInString.Equals(pInOutString) && pInString.Equals(pOutString) && pInString.Equals(retString)
#endif
                    );

                // assoc array of CLOBs not supported by ODP.NET

#if SAFETYPE_CLOB
                pInString.Dispose(); pInOutString.Dispose(); pOutString.Dispose(); retString.Dispose();
#endif
                if (conn != null) {
                    conn.Close();
                    conn.Dispose();
                }
            }

            /// <summary>
            /// Test use of Byte array LOB
            /// </summary>
            private void TestByteArrayLobCalls() {
                OracleConnection conn =
#if SAFETYPE_BLOB
                    Database.Instance.GetConnection();  // connection required to instantiate LOB classes
#else
                    null;
#endif
#if SAFETYPE_BLOB
                OracleBlob
#else
                Byte[]
#endif
                    pInByteArray, pInOutByteArray, pOutByteArray, retByteArray;
#if SAFETYPE_BLOB
                pInByteArray = new OracleBlob(conn);
                pInOutByteArray = new OracleBlob(conn);
                pOutByteArray = new OracleBlob(conn);
                retByteArray = new OracleBlob(conn);
#endif
                // BLOB
                // standard call
                pInByteArray
#if SAFETYPE_BLOB
                    .Append(Enumerable.Repeat((byte)0x00, MAX_BYTES_FOR_BLOB_COL).ToArray(), 0, MAX_BYTES_FOR_BLOB_COL);
                    pInOutByteArray = (OracleBlob)pInByteArray.Clone();
#else
                    = pInOutByteArray = Enumerable.Repeat((byte)0x00, MAX_BYTES_FOR_BLOB_COL).ToArray();
#endif
                retByteArray = OdptPkgMain.Instance.FuncBlob(pInByteArray, ref pInOutByteArray, out pOutByteArray, conn);
                Debug.Assert(
#if SAFETYPE_BLOB
                    (pInByteArray.Value.SequenceEqual(pInOutByteArray.Value)) && (pInByteArray.Value.SequenceEqual(pOutByteArray.Value)) && (pInByteArray.Value.SequenceEqual(retByteArray.Value))
#else
                    (pInByteArray.SequenceEqual(pInOutByteArray)) && (pInByteArray.SequenceEqual(pOutByteArray)) && (pInByteArray.SequenceEqual(retByteArray))
#endif
                );

                // assoc array of BLOBs not supported by ODP.NET
#if SAFETYPE_BLOB
                pInByteArray.Dispose(); pInOutByteArray.Dispose(); pOutByteArray.Dispose(); retByteArray.Dispose();
#endif
                if (conn != null) {
                    conn.Close();
                    conn.Dispose();
                }
            }

            /// <summary>
            /// Test use of DateTime, OracleDate, OracleTimeStamp 
            /// </summary>
            private void TestDateCalls() {
#if SAFETYPE_DATE
                OracleDate? pInDateTime, pInOutDateTime, pOutDateTime, retDateTime;
                IList<OracleDate?> pInListDateTime, pInOutListDateTime, pOutListDateTime, retListDateTime;
                IList<OracleDate?> dateTimeTestValues = new List<OracleDate?>() { OracleDate.GetSysDate(), OracleDate.MaxValue, OracleDate.MinValue, OracleDate.Null };
#elif DATE_TIME
                DateTime? pInDateTime, pInOutDateTime, pOutDateTime, retDateTime;
                IList<DateTime?> pInListDateTime, pInOutListDateTime, pOutListDateTime, retListDateTime;
                IList<DateTime?> dateTimeTestValues = new List<DateTime?>() { 
                    DateTime.Now, DateTime.MaxValue.AddMilliseconds(-1), DateTime.MinValue.AddMilliseconds(1),
                    null };
#else // DateTimeOffset
                DateTimeOffset? pInDateTime, pInOutDateTime, pOutDateTime, retDateTime;
                IList<DateTimeOffset?> pInListDateTime, pInOutListDateTime, pOutListDateTime, retListDateTime;
                IList<DateTimeOffset?> dateTimeTestValues = new List<DateTimeOffset?>() { 
                    DateTimeOffset.Now, DateTimeOffset.MaxValue.AddMilliseconds(-1), DateTimeOffset.MinValue.AddMilliseconds(1),
                    null };
#endif
                // DATE
                // standard call
                for (int i = 0; i < dateTimeTestValues.Count; i++) {
                    pInDateTime = pInOutDateTime = dateTimeTestValues[i];
                    retDateTime = OdptPkgMain.Instance.FuncDate(pInDateTime, ref pInOutDateTime, out pOutDateTime, null);
#if SAFETYPE_DATE
                    Debug.Assert(Util.IsEqual(pInDateTime, pInOutDateTime) && Util.IsEqual(pInDateTime, pOutDateTime) && Util.IsEqual(pInDateTime, retDateTime));
#else
                    if (pInDateTime == null)
                        Debug.Assert(pInOutDateTime == null && pOutDateTime == null && retDateTime == null);
                    else {
                        Debug.Assert(pInDateTime - pInOutDateTime < TimeSpan.FromSeconds(1) && pInDateTime - pOutDateTime < TimeSpan.FromSeconds(1) && pInDateTime - retDateTime < TimeSpan.FromSeconds(1));
                    }
#endif
                }

                // assoc array
                pInListDateTime = pInOutListDateTime = dateTimeTestValues;
                retListDateTime = OdptPkgMain.Instance.FuncAaDate(pInListDateTime, ref pInOutListDateTime, out pOutListDateTime, null);
#if SAFETYPE_DATE
                for (int i = 0; i < pInListDateTime.Count; i++) {
                    Debug.Assert(Util.IsEqual(pInListDateTime[i], pInOutListDateTime[i]));
                    Debug.Assert(Util.IsEqual(pInListDateTime[i], pOutListDateTime[i]));
                    Debug.Assert(Util.IsEqual(pInListDateTime[i], retListDateTime[i]));
                }
#else
                for (int i = 0; i < pInListDateTime.Count; i++) Debug.Assert(!(pInListDateTime[i] - pInOutListDateTime[i] > TimeSpan.FromSeconds(1)));
                for (int i = 0; i < pInListDateTime.Count; i++) Debug.Assert(!(pInListDateTime[i] - pOutListDateTime[i] > TimeSpan.FromSeconds(1)));
                for (int i = 0; i < pInListDateTime.Count; i++) Debug.Assert(!(pInListDateTime[i] - retListDateTime[i] > TimeSpan.FromSeconds(1)));
#endif

#if SAFETYPE_TIMESTAMP
                OracleTimeStamp? pInTimeStamp, pInOutTimeStamp, pOutTimeStamp, retTimeStamp;
                //List<OracleTimeStamp?> pInListTimeStamp, pInOutListTimeStamp, pOutListTimeStamp, retListTimeStamp;
                IList<OracleTimeStamp?> timeStampTestValues = new List<OracleTimeStamp?>() { OracleTimeStamp.GetSysDate(), OracleTimeStamp.MaxValue.AddMilliseconds(-1), OracleTimeStamp.MinValue.AddMilliseconds(1), OracleTimeStamp.Null };
#elif DATE_TIME
                DateTime? pInTimeStamp, pInOutTimeStamp, pOutTimeStamp, retTimeStamp;
                IList<DateTime?> timeStampTestValues = new List<DateTime?>() {
                    DateTime.Now, DateTime.MaxValue.AddMilliseconds(-1), DateTime.MinValue.AddMilliseconds(1),
                    null };
#else // DateTimeOffset
                DateTimeOffset? pInTimeStamp, pInOutTimeStamp, pOutTimeStamp, retTimeStamp;
                IList<DateTimeOffset?> timeStampTestValues = new List<DateTimeOffset?>() {
                    DateTimeOffset.Now, DateTimeOffset.MaxValue.AddMilliseconds(-1), DateTimeOffset.MinValue.AddMilliseconds(1),
                    null };
#endif
                // TIMESTAMP 
                // standard call
                for (int i = 0; i < timeStampTestValues.Count; i++) {
                    pInTimeStamp = pInOutTimeStamp = timeStampTestValues[i];
                    retTimeStamp = OdptPkgMain.Instance.FuncTimestamp(pInTimeStamp, ref pInOutTimeStamp, out pOutTimeStamp, null);
#if SAFETYPE_TIMESTAMP
                    Debug.Assert(Util.IsEqual(pInTimeStamp, pInOutTimeStamp));
                    Debug.Assert(Util.IsEqual(pInTimeStamp, pOutTimeStamp));
                    Debug.Assert(Util.IsEqual(pInTimeStamp, retTimeStamp));
#else
                    Debug.Assert(!(pInTimeStamp - pInOutTimeStamp > TimeSpan.FromSeconds(1)));
                    Debug.Assert(!(pInTimeStamp - pOutTimeStamp > TimeSpan.FromSeconds(1)));
                    Debug.Assert(!(pInTimeStamp - retTimeStamp > TimeSpan.FromSeconds(1)));
#endif
                }

                // assoc array - fails inside proc, Timestamp not supported by ODP.NET
                //pInListTimeStamp = pInOutListTimeStamp = timeStampTestValues;
                //retListTimeStamp = OdptPkgMain.Instance.FuncAaTimestamp( pInListTimeStamp, ref pInOutListTimeStamp, out pOutListTimeStamp );
                //for (int i = 0; i < pInListTimeStamp.Count; i++) if (pInListTimeStamp[i] - pInOutListTimeStamp[i] > TimeSpan.FromSeconds( 1 )) throw new Exception( "Error" );
                //for (int i = 0; i < pInListTimeStamp.Count; i++) if (pInListTimeStamp[i] - pOutListTimeStamp[i] > TimeSpan.FromSeconds( 1 )) throw new Exception( "Error" );
                //for (int i = 0; i < pInListTimeStamp.Count; i++) if (pInListTimeStamp[i] - retListTimeStamp[i] > TimeSpan.FromSeconds( 1 )) throw new Exception( "Error" );
            }

            private void TestTimeSpanCalls() {
                // TBD
            }
            #endregion

            #region Miscellaneous tests
            private void CompileTimeChecks() {
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

                // package names with special characters
                OdptPkgIncludeexclamationpointchar odptPkgIncludeexclamationpointchar = OdptPkgIncludeexclamationpointchar.Instance;
                OdptPkgIncludepercentchar odptPkgIncludepercentchar = OdptPkgIncludepercentchar.Instance;
                OdptPkgIncludeampersandchar odptPkgIncludeampersandchar = OdptPkgIncludeampersandchar.Instance;
            }

            private void TestSingleton() {
                OdptPkgMain first = null, second = null, third = null;
                Parallel.Invoke(() => first = OdptPkgMain.Instance, () => second = OdptPkgMain.Instance, () => third = OdptPkgMain.Instance);
                Debug.Assert(Object.ReferenceEquals(first, second) && Object.ReferenceEquals(first, third));
            }

            private void TestMiscCalls() {
                OdptPkgMain.Instance.ProcUnderscoreSuffix(null);
                OdptPkgMain.Instance.ProcUnderscoreSuffixUnderscorechar(null);

                try {
                    OdptPkgMain.Instance.ProcRaiseException(null);
                    Debug.Assert(false);
                } catch (Exception ex) {
                    Debug.Assert(ex.Message.StartsWith("ORA-21000"));
                }

                // NOCOPY test
#if SAFETYPE_INTEGER
                OracleDecimal? 
#elif DECIMAL_INTEGER
                Decimal?
#elif SHORT_INTEGER
                Int32?
#else
                Int64?
#endif
                    pIn, pInOut, pOut;
                for (pIn = 1; pIn <= 3; pIn = pIn + 1) {
                    pInOut = pIn;
                    OdptPkgMain.Instance.ProcNocopyIncrement(pIn, ref pInOut, out pOut, null);
                    Debug.Assert(pInOut.Equals(pIn + 1) && pOut.Equals(pIn + 1));
                }
            }

            private void TestDuplicateSignatureCalls() {
#if SAFETYPE_CLOB
                OracleConnection conn = Database.Instance.GetConnection();  // connection required to instantiate LOB classes
#endif

#if SAFETYPE_INTEGER
                OracleDecimal?
#elif DECIMAL_INTEGER
                Decimal?
#elif SHORT_INTEGER
                Int32?
#else
                Int64?
#endif
                pIn = 1, pInOut = -1, pOut, ret;
                OdptPkgMain.Instance.DupSignature1(pIn, ref pInOut, out pOut, null);
                Debug.Assert(pInOut.Equals(pIn + 1) && pOut.Equals(pIn + 1));
                OdptPkgMain.Instance.DupSignature2(pIn, ref pInOut, out pOut, null);
                Debug.Assert(pInOut.Equals(pIn + 2) && pOut.Equals(pIn + 2));
                OdptPkgMain.Instance.DupSignature3(pIn, ref pInOut, out pOut, null);
                Debug.Assert(pInOut.Equals(pIn + 3) && pOut.Equals(pIn + 3));
                ret = OdptPkgMain.Instance.DupSignature4(pIn, ref pInOut, out pOut, null);
                Debug.Assert(pInOut.Equals(pIn + 1) && pOut.Equals(pIn + 1) && ret.Equals(pIn + 1));
                ret = OdptPkgMain.Instance.DupSignature5(pIn, ref pInOut, out pOut, null);
                Debug.Assert(pInOut.Equals(pIn + 2) && pOut.Equals(pIn + 2) && ret.Equals(pIn + 2));
                ret = OdptPkgMain.Instance.DupSignature6(pIn, ref pInOut, out pOut, null);
                Debug.Assert(pInOut.Equals(pIn + 3) && pOut.Equals(pIn + 3) && ret.Equals(pIn + 3));

                if (false) { // These should compile. But they will fail at runtime with Oracle "too many declarations" error
                    string pInStr = "TEST_IN", pInOutStr = "TEST_INOUT", pOutStr, retStr;
                    retStr = OdptPkgMain.Instance.DupSignatureTranslatedStr1(pInStr, ref pInOutStr, out pOutStr, null);
                    Debug.Assert(pInOut.Equals(pIn) && pOut.Equals(pIn) && ret.Equals(pIn));
                    retStr = OdptPkgMain.Instance.DupSignatureTranslatedStr2(pInStr, ref pInOutStr, out pOutStr, null);
                    Debug.Assert(pInOut.Equals(pIn) && pOut.Equals(pIn) && ret.Equals(pIn));
                    retStr = OdptPkgMain.Instance.DupSignatureTranslatedStr3(pInStr, ref pInOutStr, out pOutStr, null);
                    Debug.Assert(pInOut.Equals(pIn) && pOut.Equals(pIn) && ret.Equals(pIn));
                    retStr = OdptPkgMain.Instance.DupSignatureTranslatedStr4(pInStr, ref pInOutStr, out pOutStr, null);
                    Debug.Assert(pInOut.Equals(pIn) && pOut.Equals(pIn) && ret.Equals(pIn));
#if SAFETYPE_CLOB
                    OracleClob pInClob, pInOutClob, pOutClob, retClob;
                    pInClob = pInOutClob = new OracleClob(conn);
                    retClob = OdptPkgMain.Instance.DupSignatureTranslatedStr5(pInClob, ref pInOutClob, out pOutClob, conn);
                    Debug.Assert(pInClob.Value.Equals(pInOutClob.Value) && pInClob.Value.Equals(pOutClob.Value) && pInClob.Value.Equals(retClob.Value));
                    retClob = OdptPkgMain.Instance.DupSignatureTranslatedStr6(pInClob, ref pInOutClob, out pOutClob, conn);
                    Debug.Assert(pInClob.Value.Equals(pInOutClob.Value) && pInClob.Value.Equals(pOutClob.Value) && pInClob.Value.Equals(retClob.Value));
#endif
                }
                

#if SAFETYPE_DATE
                OracleDate?
#elif DATE_TIME
                DateTime?
#else // DateTimeOffset
                // TBD
#endif
                pInDate, pInOutDate, pOutDate, retDate;
                pInDate = pInOutDate =
#if SAFETYPE_DATE
                OracleDate.GetSysDate();
#elif DATE_TIME
                DateTime.Now;
#else // DateTimeOffset
                // TBD
#endif
                retDate = OdptPkgMain.Instance
#if SAFETYPE_DATE
                .DupSignatureTranslatedDate(pInDate, ref pInOutDate, out pOutDate, null);
                Debug.Assert(pInDate.Equals(pInOutDate) && pInDate.Equals(pOutDate) && pInDate.Equals(retDate));
#elif DATE_TIME
                .DupSignatureTranslatedDate1(pInDate, ref pInOutDate, out pOutDate, null);
                Debug.Assert(Util.IsEqual(pInDate, pInOutDate) && Util.IsEqual(pInDate, pOutDate) && Util.IsEqual(pInDate, retDate));
#else // DateTimeOffset

#endif

#if SAFETYPE_TIMESTAMP
                OracleTimeStamp?
#elif DATE_TIME
                DateTime?
#else // DateTimeOffset
                // TBD
#endif
                pInTimestamp, pInOutTimestamp, pOutTimestamp, retTimestamp;
                pInTimestamp = pInOutTimestamp =
#if SAFETYPE_DATE
                OracleTimeStamp.GetSysDate();
#elif DATE_TIME
                DateTime.Now;
#else // DateTimeOffset
                // TBD
#endif
                retTimestamp = OdptPkgMain.Instance
#if SAFETYPE_TIMESTAMP
                .DupSignatureTranslatedDate(pInTimestamp, ref pInOutTimestamp, out pOutTimestamp, null);
                Debug.Assert(Util.IsEqual(pInTimestamp, pInOutTimestamp) && Util.IsEqual(pInTimestamp, pOutTimestamp) && Util.IsEqual(pInTimestamp, retTimestamp));
#elif DATE_TIME
                .DupSignatureTranslatedDate2(pInTimestamp, ref pInOutTimestamp, out pOutTimestamp, null);
                Debug.Assert(Util.IsEqual(pInTimestamp, pInOutTimestamp) && Util.IsEqual(pInTimestamp, pOutTimestamp) && Util.IsEqual(pInTimestamp, retTimestamp));
#else // DateTimeOffset
                // TBD
#endif
                // Byte array test here
                byte[] byteArray = new byte[] { byte.MaxValue };
#if SAFETYPE_BLOB
                //conn = Database.Instance.GetConnection();  // connection required to instantiate LOB classes
                OracleBlob
#else
                Byte[]
#endif
                    pInByteArray, pInOutByteArray, pOutByteArray, retByteArray;
#if SAFETYPE_BLOB
                pInByteArray = pInOutByteArray = pOutByteArray = retByteArray = new OracleBlob(conn);
                pInByteArray.Append(byteArray, 0, 1);
                pInOutByteArray.Append(byteArray, 0, 1);
#else
                pInByteArray = pInOutByteArray = byteArray;
#endif
#if SAFETYPE_BLOB
                retByteArray = OdptPkgMain.Instance.DupSigTranslatedByteArr(pInByteArray, ref pInOutByteArray, out pOutByteArray, conn);
#else                
                if (false) { // runtime error will occur due to ambiguous package call
                    retByteArray = OdptPkgMain.Instance.DupSigTranslatedByteArr(pInByteArray, ref pInOutByteArray, out pOutByteArray);
                }
#endif
#if SAFETYPE_BLOB
                Debug.Assert( (pInByteArray.Value.SequenceEqual(pInOutByteArray.Value)) && (pInByteArray.Value.SequenceEqual(pOutByteArray.Value)) && (pInByteArray.Value.SequenceEqual(retByteArray.Value)) );
#else
                //Debug.Assert(pInByteArray.SequenceEqual(pInOutByteArray) && pInByteArray.SequenceEqual(pOutByteArray) && pInByteArray.SequenceEqual(retByteArray));
#endif
#if SAFETYPE_BLOB
                if (conn != null) {
                    conn.Close();
                    conn.Dispose();
                }
#endif
            }

            private void TestNoParamCalls() {
                OdptPkgMain.Instance.ProcNoParam(null);
#if SAFETYPE_NUMBER
                OracleDecimal? 
#else
                Decimal?
#endif
                ret = OdptPkgMain.Instance.FuncNoParam(null);
            }

            private void TestOptionalParamCalls() {
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
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired));
                    OdptPkgMain.Instance.ProcOptionalParam(pInNumberRequired, ref pInOutNumberRequired, pInNumberOptional, pInVarchar2Optional);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired));
                    OdptPkgMain.Instance.ProcOptionalParam(pInNumberRequired, ref pInOutNumberRequired, pInNumberOptional);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired));
                    OdptPkgMain.Instance.ProcOptionalParam(pInNumberRequired, ref pInOutNumberRequired);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired));
                    OdptPkgMain.Instance.ProcOptionalParam(pInNumberRequired, ref pInOutNumberRequired, optionalPreexistingOpenConnection : null);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired));

                    OdptPkgMain.Instance.ProcOptionalParamReversed(pInNumberRequired, ref pInOutNumberRequired, pInNumberOptional, pInVarchar2Optional, null);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired));
                    OdptPkgMain.Instance.ProcOptionalParamReversed(pInNumberRequired, ref pInOutNumberRequired, pInNumberOptional, pInVarchar2Optional);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired));
                    OdptPkgMain.Instance.ProcOptionalParamReversed(pInNumberRequired, ref pInOutNumberRequired, pInNumberOptional);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired));
                    OdptPkgMain.Instance.ProcOptionalParamReversed(pInNumberRequired, ref pInOutNumberRequired);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired));
                    OdptPkgMain.Instance.ProcOptionalParamReversed(pInNumberRequired, ref pInOutNumberRequired, optionalPreexistingOpenConnection: null);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired));

                    // func
                    pRetNumber = OdptPkgMain.Instance.FuncOptionalParam(pInNumberRequired, ref pInOutNumberRequired, pInNumberOptional, pInVarchar2Optional, null);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired) && pInNumberRequired.Equals(pRetNumber));
                    pRetNumber = OdptPkgMain.Instance.FuncOptionalParam(pInNumberRequired, ref pInOutNumberRequired, pInNumberOptional, pInVarchar2Optional);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired) && pInNumberRequired.Equals(pRetNumber));
                    pRetNumber = OdptPkgMain.Instance.FuncOptionalParam(pInNumberRequired, ref pInOutNumberRequired, pInNumberOptional);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired) && pInNumberRequired.Equals(pRetNumber));
                    pRetNumber = OdptPkgMain.Instance.FuncOptionalParam(pInNumberRequired, ref pInOutNumberRequired);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired) && pInNumberRequired.Equals(pRetNumber));
                    pRetNumber = OdptPkgMain.Instance.FuncOptionalParam(pInNumberRequired, ref pInOutNumberRequired, optionalPreexistingOpenConnection : null);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired) && pInNumberRequired.Equals(pRetNumber));

                    pRetNumber = OdptPkgMain.Instance.FuncOptionalParamReversed(pInNumberRequired, ref pInOutNumberRequired, pInNumberOptional, pInVarchar2Optional, null);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired) && pInNumberRequired.Equals(pRetNumber));
                    pRetNumber = OdptPkgMain.Instance.FuncOptionalParamReversed(pInNumberRequired, ref pInOutNumberRequired, pInNumberOptional, pInVarchar2Optional);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired) && pInNumberRequired.Equals(pRetNumber));
                    pRetNumber = OdptPkgMain.Instance.FuncOptionalParamReversed(pInNumberRequired, ref pInOutNumberRequired, pInNumberOptional);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired) && pInNumberRequired.Equals(pRetNumber));
                    pRetNumber = OdptPkgMain.Instance.FuncOptionalParamReversed(pInNumberRequired, ref pInOutNumberRequired);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired) && pInNumberRequired.Equals(pRetNumber));
                    pRetNumber = OdptPkgMain.Instance.FuncOptionalParamReversed(pInNumberRequired, ref pInOutNumberRequired, optionalPreexistingOpenConnection: null);
                    Debug.Assert(pInNumberRequired.Equals(pInOutNumberRequired) && pInNumberRequired.Equals(pRetNumber));
                }
            }
#endregion
        }

#region DTOs
        
#region Table Big
        public class TTableBig : OdptPkgTableBig.TTableBig { }

        public class TTableBigChar : OdptPkgTableBig.TTableBigChar { }

        public class TTableBigMapByPositionAll : OdptPkgTableBig.ITTableBig {
            [HydratorMapAttribute(Position = 0)]
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
            [HydratorMapAttribute(Position = 1)]
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
            [HydratorMapAttribute(Position = 2)]
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
            [HydratorMapAttribute(Position = 3)]
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
            [HydratorMapAttribute(Position = 4)]
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
            [HydratorMapAttribute(Position = 5)]
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
                                    ColNumeric { get; set; }
            [HydratorMapAttribute(Position = 6)]
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
                                    ColDecimal { get; set; }
            [HydratorMapAttribute(Position = 7)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber { get; set; }
            [HydratorMapAttribute(Position = 8)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColDoublePrecision
            { get; set; }
            [HydratorMapAttribute(Position = 9)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColFloat
            { get; set; }
            [HydratorMapAttribute(Position = 10)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColReal { get; set; }
            [HydratorMapAttribute(Position = 11)]
            public virtual Single? ColBinaryFloat { get; set; }
            [HydratorMapAttribute(Position = 12)]
            public virtual Double? ColBinaryDouble { get; set; }
            [HydratorMapAttribute(Position = 13)]
            public virtual String ColVarcharMin { get; set; }
            [HydratorMapAttribute(Position = 14)]
            public virtual String ColVarcharMax { get; set; }
            [HydratorMapAttribute(Position = 15)]
            public virtual String ColVarchar2Min { get; set; }
            [HydratorMapAttribute(Position = 16)]
            public virtual String ColVarchar2Max { get; set; }
            [HydratorMapAttribute(Position = 17)]
            public virtual String ColNvarchar2Min { get; set; }
            [HydratorMapAttribute(Position = 18)]
            public virtual String ColNvarchar2Max { get; set; }
            [HydratorMapAttribute(Position = 19)]
            public virtual String ColCharMin { get; set; }
            [HydratorMapAttribute(Position = 20)]
            public virtual String ColCharMax { get; set; }
            [HydratorMapAttribute(Position = 21)]
            public virtual String ColNcharMin { get; set; }
            [HydratorMapAttribute(Position = 22)]
            public virtual String ColNcharMax { get; set; }
            [HydratorMapAttribute(Position = 23)]
            public virtual
#if SAFETYPE_DATE
                            OracleDate?
#elif DATE_TIME
                            DateTime?
#else
                            DateTimeOffset?
#endif
                                    ColDate
            { get; set; }
            [HydratorMapAttribute(Position = 24)]
            public virtual
#if SAFETYPE_TIMESTAMP
                            OracleTimeStamp? 
#elif DATE_TIME
                            DateTime?
#else
                            DateTimeOffset?
#endif
            ColTimestamp
            { get; set; }

            [HydratorMapAttribute(Position = 25)]
            public virtual
#if SAFETYPE_TIMESTAMP
                            OracleTimeStamp? 
#elif DATE_TIME
                            DateTime?
#else
                            DateTimeOffset?
#endif
            ColTimestampPrec0
            { get; set; }


            [HydratorMapAttribute(Position = 26)]
            public virtual
#if SAFETYPE_TIMESTAMP
                            OracleTimeStamp? 
#elif DATE_TIME
                            DateTime?
#else
                            DateTimeOffset?
#endif
            ColTimestampPrec9
            { get; set; }

            [HydratorMapAttribute(Position = 27)]
            public virtual
#if SAFETYPE_BLOB
                            OracleBlob
#else
                            Byte[]
#endif
                                ColBlob { get; set; }
            [HydratorMapAttribute(Position = 28)]
            public virtual
#if SAFETYPE_CLOB
                            OracleClob
#else
                            String
#endif
                                ColClob { get; set; }
            [HydratorMapAttribute(Position = 29)]
            public virtual
#if SAFETYPE_CLOB
                            OracleClob
#else
                            String
#endif
                                ColNclob { get; set; }
            [HydratorMapAttribute(Position = 30)]
            public virtual String ColLast { get; set; }
        } // TTableBig

        public class TTableBigMapByPositionPartial : TTableBigMapByPositionAll {
            public override String ColLast { get; set; }
        }

        public class TTableBigCharMapByPositionAll : OdptPkgTableBig.ITTableBigChar {
            [HydratorMapAttribute(Position = 0)]
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
            [HydratorMapAttribute(Position = 1)]
            public virtual String ColVarcharMin { get; set; }
            [HydratorMapAttribute(Position = 2)]
            public virtual String ColVarcharMax { get; set; }
            [HydratorMapAttribute(Position = 3)]
            public virtual String ColVarchar2Min { get; set; }
            [HydratorMapAttribute(Position = 4)]
            public virtual String ColVarchar2Max { get; set; }
            [HydratorMapAttribute(Position = 5)]
            public virtual String ColNvarchar2Min { get; set; }
            [HydratorMapAttribute(Position = 6)]
            public virtual String ColNvarchar2Max { get; set; }
            [HydratorMapAttribute(Position = 7)]
            public virtual String ColCharMin { get; set; }
            [HydratorMapAttribute(Position = 8)]
            public virtual String ColCharMax { get; set; }
            [HydratorMapAttribute(Position = 9)]
            public virtual String ColNcharMin { get; set; }
            [HydratorMapAttribute(Position = 10)]
            public virtual String ColNcharMax { get; set; }
            [HydratorMapAttribute(Position = 11)]
            public virtual String ColLast { get; set; }
        } // TTableBig

        public class TTableBigCharMapByPositionPartial : TTableBigCharMapByPositionAll {
            public override String ColLast { get; set; }
        }
#endregion
#region Table Big filtered
#if ODPT_FILTER_PREFIX
        public class FilteredPkgTTableBigFiltered : OdptPkgTableBig.FltrPkgRecUsedTTableBigFltUsed { }
#else
        public class TTableBigFiltered : FltrPkgRecUsed.TTableBigFltUsed { }
#endif
#endregion
#region Table Number
        public class TTableNumber : OdptPkgTableNumber.TTableNumber { }

        public class TTableNumberDec : OdptPkgTableNumber.TTableNumberDec { }
              
        public class TTableNumberMapByPositionAll : OdptPkgTableNumber.ITTableNumber {
            [HydratorMapAttribute(Position = 0)]
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
            [HydratorMapAttribute(Position = 1)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber
            { get; set; }
            [HydratorMapAttribute(Position = 2)]
            public virtual 
                            SByte? 
                                    ColNumber1 { get; set; }
            [HydratorMapAttribute(Position = 3)]
            public virtual 
                            SByte? 
                                    ColNumber2 { get; set; }
            [HydratorMapAttribute(Position = 4)]
            public virtual 
                            Int16? 
                                    ColNumber3 { get; set; }
            [HydratorMapAttribute(Position = 5)]
            public virtual 
                            Int16? 
                                    ColNumber4 { get; set; }
            [HydratorMapAttribute(Position = 6)]
            public virtual 
                            Int32? 
                                    ColNumber5 { get; set; }
            [HydratorMapAttribute(Position = 7)]
            public virtual 
                            Int32? 
                                    ColNumber6 { get; set; }
            [HydratorMapAttribute(Position = 8)]
            public virtual 
                            Int32? 
                                    ColNumber7 { get; set; }
            [HydratorMapAttribute(Position = 9)]
            public virtual 
                            Int32? 
                                    ColNumber8 { get; set; }
            [HydratorMapAttribute(Position = 10)]
            public virtual 
                            Int32? 
                                    ColNumber9 { get; set; }
            [HydratorMapAttribute(Position = 11)]
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
            [HydratorMapAttribute(Position = 12)]
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
            [HydratorMapAttribute(Position = 13)]
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
            [HydratorMapAttribute(Position = 14)]
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
            [HydratorMapAttribute(Position = 15)]
            public virtual 
                            SByte? 
                                    ColNumber1Scale0 { get; set; }
            [HydratorMapAttribute(Position = 16)]
            public virtual 
                            SByte? 
                                    ColNumber2Scale0 { get; set; }
            [HydratorMapAttribute(Position = 17)]
            public virtual 
                            Int16? 
                                    ColNumber3Scale0 { get; set; }
            [HydratorMapAttribute(Position = 18)]
            public virtual 
                            Int16? 
                                    ColNumber4Scale0 { get; set; }
            [HydratorMapAttribute(Position = 19)]
            public virtual 
                            Int32? 
                                    ColNumber5Scale0 { get; set; }
            [HydratorMapAttribute(Position = 20)]
            public virtual 
                            Int32? 
                                    ColNumber9Scale0 { get; set; }
            [HydratorMapAttribute(Position = 21)]
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
            [HydratorMapAttribute(Position = 22)]
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
            [HydratorMapAttribute(Position = 23)]
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
            [HydratorMapAttribute(Position = 24)]
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
            [HydratorMapAttribute(Position = 25)]
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
            [HydratorMapAttribute(Position = 26)]
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
            [HydratorMapAttribute(Position = 27)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber2Scale1
            { get; set; }
            [HydratorMapAttribute(Position = 28)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber5Scale3
            { get; set; }
            [HydratorMapAttribute(Position = 29)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber15Scale11
            { get; set; }
            [HydratorMapAttribute(Position = 30)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber31Scale21
            { get; set; }
            [HydratorMapAttribute(Position = 31)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber38Scale37
            { get; set; }
            [HydratorMapAttribute(Position = 32)]
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
            [HydratorMapAttribute(Position = 0)]
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
            [HydratorMapAttribute(Position = 1)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber { get; set; }
            [HydratorMapAttribute(Position = 2)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber2Scale1 { get; set; }
            [HydratorMapAttribute(Position = 3)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber5Scale3 { get; set; }
            [HydratorMapAttribute(Position = 4)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber15Scale11 { get; set; }
            [HydratorMapAttribute(Position = 5)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber31Scale21 { get; set; }
            [HydratorMapAttribute(Position = 6)]
            public virtual
#if SAFETYPE_NUMBER
                            OracleDecimal?
#else
                            Decimal?
#endif
                                    ColNumber38Scale37 { get; set; }
            [HydratorMapAttribute(Position = 7)]
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

        private static class Util {
            /// <summary>
            /// A make shift compare for two double values
            /// </summary>
            /// <param name="d1"></param>
            /// <param name="d2"></param>
            /// <returns></returns>
            internal static bool IsEqual(Double? v1, Double? v2) {
                if ((v1 == null && v2 == null) || (v1 == 0.0 && v2 == 0.0) || (v1.Equals(Double.NaN) && v2.Equals(Double.NaN)) || 
                    (v1.Equals(Double.PositiveInfinity) && v2.Equals(Double.PositiveInfinity)) || v1.Equals(Double.NegativeInfinity) && v2.Equals(Double.NegativeInfinity)) return true;
                double allowedDelta = Math.Abs(v1.Value * 0.00001);
                double delta = Math.Abs(v1.Value - v2.Value);
                return delta <= allowedDelta;
            }
            internal static bool IsEqual(Single? v1, Single? v2) {
                if ((v1 == null && v2 == null) || (v1 == 0.0 && v2 == 0.0) || (v1.Equals(Single.NaN) && v2.Equals(Single.NaN)) || 
                    (v1.Equals(Single.PositiveInfinity) && v2.Equals(Single.PositiveInfinity)) || v1.Equals(Single.NegativeInfinity) && v2.Equals(Single.NegativeInfinity)) return true;
                double allowedDelta = Math.Abs(v1.Value * 0.00001);
                double delta = Math.Abs(v1.Value - v2.Value);
                return delta <= allowedDelta;
            }
            internal static bool IsEqual(DateTime? val1, DateTime? val2) {
                if (val1.HasValue != val2.HasValue)
                    return false;
                else if (!val1.HasValue && !val2.HasValue)
                    return true;
                else
                    return (val1 - val2 < TimeSpan.FromSeconds(1));
            }
            private static readonly int OracleTimeStampPrecisionCompare = 5;
            internal static bool IsEqual(OracleTimeStamp? val1, OracleTimeStamp? val2) =>
                val1.Value.IsNull != val2.Value.IsNull
                    ? false
                    : (val1.Value.IsNull && val2.Value.IsNull) 
                        || OracleTimeStamp.SetPrecision(val1.Value, OracleTimeStampPrecisionCompare).Equals(OracleTimeStamp.SetPrecision(val2.Value, OracleTimeStampPrecisionCompare));
            internal static bool IsEqual(OracleDate? val1, OracleDate? val2) =>
                val1.Value.IsNull != val2.Value.IsNull
                    ? false
                    : (val1.Value.IsNull && val2.Value.IsNull) || val1.Equals(val2);
        }

        private class Database : OdptAdapter {
            private Database() { }
            private static Database _instance = new Database();
            public static Database Instance { get { return _instance; } }

            public bool TestConnection() {
                using (OracleConnection connection = (OracleConnection)GetConnection()) {
                    return true;
                }
            }

            public new OracleConnection GetConnection() {
                return base.GetConnection();
            }
        }
    }
}