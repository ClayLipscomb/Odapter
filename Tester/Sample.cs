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

#define SAMPLE
#if SAMPLE
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;
using Schema.Odpt.Package;

namespace Odapter.Sample {
    public class Sample {
        private const String HELLO = "Hello", GOODBYE = "Goodbye";

        // declare class dervied from record type DTO package
        private class MyClassDerived : OdptPkgSample.TTableBigPartial { 
            public String      StringPropertyExtra { get; set; }    // custom property
            public List<Int32> Int32ListPropertyExtra { get; set; } // custom property
        }

        // declare custom class to map only 4 columns; properties for the Date and Timestap columns will be excluded
        private class MyClassOriginal {
            public Int64? Id { get; set; }                          // maps to id column
            public Int64? ColInteger { get; set; }                  // maps to col_integer column
            public Decimal? ColNumber { get; set; }                 // maps to col_number column
            public String ColVarchar2Max { get; set; }              // maps to col_varchar2_max column
            public String StringPropertyExtra { get; set; }         // custom property
            public List<Int32> Int32ListPropertyExtra { get; set; } // custom property
        }

        public void Test() {
            uint?       rowLimit = 25;
            Int64?      pInInt64 = 100000000000000;
            Decimal?    pInDecimal = 10.0M;
            String      pInOutString = HELLO;
            DateTime?   pOutDate;

            // hydrate DTO List from typed result set
            List<MyClassDerived> myClassDerivedList = OdptPkgSample.Instance.GetRowsTypedRet<MyClassDerived>(pInDecimal, ref pInOutString, out pOutDate, rowLimit);
            Debug.Assert(myClassDerivedList.Count == rowLimit);
            Debug.Assert(pInOutString.Equals(GOODBYE));                 // confirm OUT arg from package function
            Debug.Assert(pOutDate.Equals(new DateTime (1999, 12, 31))); // confirm OUT arg from package function

            // hydrate DTO List from untyped result set by mapping column name to property name (default); force unmapped columns to be ignored (non-default)
            List<MyClassOriginal> myClassOriginalList = OdptPkgSample.Instance.GetRowsUntypedRet<MyClassOriginal>(pInInt64, false, true, rowLimit);
            Debug.Assert(myClassOriginalList.Count == rowLimit);

            // hydrate Datatable from all columns in untyped result set; convert column names to DataTable captions
            DataTable myDataTable = OdptPkgSample.Instance.GetRowsUntypedRet(pInInt64, true, rowLimit);
            Debug.Assert(myDataTable.Rows.Count == rowLimit);
            Debug.Assert(myDataTable.Columns[0].Caption.Equals("Id"));
            Debug.Assert(myDataTable.Columns[1].Caption.Equals("Col Integer"));
            Debug.Assert(myDataTable.Columns[2].Caption.Equals("Col Number"));
            Debug.Assert(myDataTable.Columns[3].Caption.Equals("Col Varchar2 Max"));
            Debug.Assert(myDataTable.Columns[4].Caption.Equals("Col Date"));
            Debug.Assert(myDataTable.Columns[5].Caption.Equals("Col Timestamp"));
        }
    }
}
#endif
