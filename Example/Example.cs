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
using System.Data;
using System.Diagnostics;
using Schema.Odpt.Xmpl.Package;         // generated code for packages
using Oracle.ManagedDataAccess.Types;   // ODP.NET safe types
using Odapter;                          // map by position attribute

namespace OdapterExample {
    class Program {
        static void Main(string[] args) {
            (new Example()).Run();
        }
    }

    // The following DTO classes will be used in different ways for the same result set.

    // Inherits the package record type DTO, adding custom properties 
    public class DtoInherited : XmplPkgExample.TTableBigPartial {   // no mapping required
        public String StringPropertyExtra { get; set; }             // custom property
        public List<Int32> Int32ListPropertyExtra { get; set; }     // custom property
    }

    // Implements the package record type interface, adding custom properties 
    public class DtoImplemented : XmplPkgExample.ITTableBigPartial {  // no mapping required
        public Int64? Id { get; set; }
        public Int64? ColInteger { get; set; }
        public Decimal? ColNumber { get; set; }
        public String ColVarchar2Max { get; set; }
        public DateTime? ColDate { get; set; }
        public OracleTimeStamp? ColTimestamp { get; set; }
        public String StringPropertyExtra { get; set; }         // custom property
        public List<Int32> Int32ListPropertyExtra { get; set; } // custom property
    }

    // Custom DTO for map by name with only 4 column properties (Date, Timestamp col excluded)
    public class DtoCustomMapByName {         // Column type and name must match, order and alias irrelvant
        public Int64? Id { get; set; }          // maps id to PascalCase public property
        public Int64? ColInteger { get; set; }  // maps col_integer to PascalCase public property

        protected Decimal? colNumber;           // maps col_number to camelCase non-public field
        public Decimal? MyNumber { get { return colNumber; } set { colNumber = value; } } // PascalCase public property will not map

        private String _colVarchar2Max;         // maps col_varchar2_max to underscore prefixed camelCase non-public field
        public virtual String MyVarchar2Max { get { return _colVarchar2Max; } set { _colVarchar2Max = value; } } // PascalCase public property will not map

        public String StringPropertyExtra { get; set; }         // custom property
        public List<Int32> Int32ListPropertyExtra { get; set; } // custom property
    }

    // Custom DTO for map by position with only 4 column properties (Date, Timestamp cols excluded)
    public class DtoCustomMapByPosition {     // Column type and order must match, name and alias irrelevant.
        [MapAttribute(Position = 0)]            // maps to column 0 (first column)
        public Int64? MyCol1 { get; set; }
        [MapAttribute(Position = 1)]            // maps to column 1
        public Int64? MyCol2 { get; set; }
        [MapAttribute(Position = 2)]            // maps to column 2
        public Decimal? MyCol3 { get; set; }
        [MapAttribute(Position = 3)]            // maps to column 3
        public String MyCol4 { get; set; }

        public String StringPropertyExtra { get; set; }         // custom property
        public List<Int32> Int32ListPropertyExtra { get; set; } // custom property
    }

    public class Example {
        private const String HELLO = "Hello", GOODBYE = "Goodbye";

        public void Run() {
            (new OdapterExample.Example()).Test();
        }

        public void Test() {
            uint? rowLimit = 25;                    // limit result set to 25 rows, underlying table has over 1000 rows
            Int64? pInInt64 = 100000000000000000;   // 18 digit long
            Decimal? pInDecimal = 10.0M;
            String pInOutString = HELLO;
            DateTime? pOutDate;
            List<Int64?> pInOutListInt64, somePrimeNumbers = new List<Int64?> { 2, 3, 5, 7, 11, 13, 17, 19, 29, 31 };
            List<DtoInherited> dtoInheritedList;
            List<DtoImplemented> dtoImplementedList;
            List<DtoCustomMapByName> dtoOriginalMapByNameList;
            List<DtoCustomMapByPosition> dtoOriginalMapByPositionList;
            DataTable dataTable;

            // 1. Hydrate DTO List from typed result set by using DTO inherited from package record type DTO.
            pInOutListInt64 = somePrimeNumbers; 
            dtoInheritedList = XmplPkgExample.Instance.GetRowsTypedRet<DtoInherited>(pInDecimal, ref pInOutString, ref pInOutListInt64, out pOutDate, rowLimit);
            Debug.Assert(dtoInheritedList.Count == rowLimit);
            Debug.Assert(pInOutString.Equals(GOODBYE));                             // confirm OUT string arg from package function
            for (int i = 0; i < pInOutListInt64.Count; i++)
                Debug.Assert(pInOutListInt64[i].Equals(somePrimeNumbers[i] * 7));   // confirm all values were multiplied by 7 in func
            Debug.Assert(pOutDate.Equals(new DateTime(1999, 12, 31)));              // confirm OUT date arg from package function

            // 2. Hydrate DTO List from typed result set by using DTO implementing package record type interface.
            pInOutListInt64 = somePrimeNumbers;
            dtoImplementedList = XmplPkgExample.Instance.GetRowsTypedRet<DtoImplemented>(pInDecimal, ref pInOutString, ref pInOutListInt64, out pOutDate, rowLimit);
            Debug.Assert(dtoImplementedList.Count == rowLimit);
            Debug.Assert(pInOutString.Equals(GOODBYE));                             // confirm OUT string arg from package function
            for (int i = 0; i < pInOutListInt64.Count; i++)
                Debug.Assert(pInOutListInt64[i].Equals(somePrimeNumbers[i] * 7));   // confirm all values were multiplied by 7 in func
            Debug.Assert(pOutDate.Equals(new DateTime(1999, 12, 31)));              // confirm OUT date arg from package function

            // 3. Hydrate DTO List from untyped result set by mapping column name to property name (default); 
            //      unmapped columns will be ignored (non-default).
            dtoOriginalMapByNameList = XmplPkgExample.Instance.GetRowsUntypedRet<DtoCustomMapByName>(pInInt64, false, true, rowLimit);
            Debug.Assert(dtoOriginalMapByNameList.Count == rowLimit);

            // 4. Hydrate DTO List from untyped result set by mapping column name to property name (default); 
            //      an unmapped column will throw (default).
            try {
                dtoOriginalMapByNameList = XmplPkgExample.Instance.GetRowsUntypedRet<DtoCustomMapByName>(pInInt64, false, false, rowLimit);
            } catch (Exception ex) {
                if (!ex.Message.StartsWith("Hydrator.BuildMappings")) Debug.Assert(false);
            }

            // 5. Hydrate DTO List from untyped result set by mapping column position to property position (non-default); 
            //      unmapped columns will be ignored (non-default)
            dtoOriginalMapByPositionList = XmplPkgExample.Instance.GetRowsUntypedRet<DtoCustomMapByPosition>(pInInt64, true, true, rowLimit);

            // 6. Hydrate DTO List from untyped result set by mapping column position to property position (non-default); 
            //      an unmapped column will throw (default).
            try {
                dtoOriginalMapByPositionList = XmplPkgExample.Instance.GetRowsUntypedRet<DtoCustomMapByPosition>(pInInt64, true, false, rowLimit);
            } catch (Exception ex) {
                if (!ex.Message.StartsWith("Hydrator.BuildMappings")) Debug.Assert(false);
            }

            // 7. Hydrate Datatable from all columns in untyped result set, column names are converted to DataTable captions.
            //      No DTO or generic required.
            dataTable = XmplPkgExample.Instance.GetRowsUntypedRet(pInInt64, true, rowLimit);
            Debug.Assert(dataTable.Rows.Count == rowLimit);
            List<String> dataTableCaptions = new List<string> { "Id", "Col Integer", "Col Number", "Varchar2 Max Col", "Col Date", "Col Timestamp" };
            for (int i = 0; i < dataTableCaptions.Count; i++)
                Debug.Assert(dataTable.Columns[i].Caption.Equals(dataTableCaptions[i]));  // confirm captions were created from column name
        }
    }
}