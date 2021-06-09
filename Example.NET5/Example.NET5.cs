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
using System.Data;
using System.Diagnostics;
using Schema.Odpt.Xmpl.Package;         // generated code for packages
using Oracle.ManagedDataAccess.Types;   // ODP.NET safe types
using Odapter;                          // attribute used to map by position

namespace OdapterExample.NET5 {
    class Program {
        static void Main(string[] args) {
            (new Example()).Run();
        }
    }

    // The following DTO classes will be used in different ways for the same result set.

    // Implements the package record's immutable interface
    public record DtoImplemented : XmplPkgExample.ITTableBigPartial {   // no mapping required
        public Int64? Id { get; init; }
        public Int64? ColInteger { get; init; }
        public Decimal? ColNumber { get; init; }
        public String ColVarchar2Max { get; init; }
        public DateTime? ColDate { get; init; }
        public OracleTimeStamp? ColTimestamp { get; init; }      // ODP.NET safe type struct
    }

    // Implements the package record's immutable interface, adding custom properties 
    public record DtoImplementedWithCustom : XmplPkgExample.ITTableBigPartial {  // no mapping required
        public Int64? Id { get; init; }
        public Int64? ColInteger { get; init; }
        public Decimal? ColNumber { get; init; }
        public String ColVarchar2Max { get; init; }
        public DateTime? ColDate { get; init; }
        public OracleTimeStamp? ColTimestamp { get; init; }      // ODP.NET safe type struct
        public String StringPropertyExtra { get; init; }         // custom property, can be ignored during hydration
        public List<Int32> Int32ListPropertyExtra { get; init; } // custom property, can be ignored during hydration
    }

    // Custom positional record (w/ parameterless constructor) DTO with only 4 column properties (Date and Timestamp columns are excluded)
    public record DtoCustomMapByNamePositionalRecord(          // Column type and name must match, order and alias irrelvant
        Int64? Id,                                              // maps id to PascalCase public property
        Int64? ColInteger,                                      // maps col_integer to PascalCase public property
        Decimal? ColNumber,                                     // maps col_number to PascalCase public property
        String ColVarchar2Max,                                  // maps col_varchar2_max to PascalCase public property
        String StringPropertyExtra,                             // custom property, can be ignored during hydration
        List<Int32> Int32ListPropertyExtra                      // custom property, can be ignored during hydration
    ) {
        public DtoCustomMapByNamePositionalRecord() : this(default, default, default, default, default, default) { }
    }

    // Custom DTO for mapping by name hydration with only 4 column properties (Date and Timestamp columns are excluded)
    public record DtoCustomMapByName {                          // Column type and name must match, order and alias irrelvant
        public Int64? Id { get; init; }                         // maps id to PascalCase public property
        public Int64? ColInteger { get; init; }                 // maps col_integer to PascalCase public property

        protected Decimal? colNumber;                           // maps col_number to camelCase non-public field
        public Decimal? MyNumber {                              // PascalCase public property will not map
            get { return colNumber; } set { colNumber = value; } 
        } 

        private String _colVarchar2Max;                         // maps col_varchar2_max to underscore prefixed camelCase non-public field
        public virtual String MyVarchar2Max                     // PascalCase public property will not map
            { get { return _colVarchar2Max; } set { _colVarchar2Max = value; } 
        } 

        public String StringPropertyExtra { get; init; }         // custom property, can be ignored during hydration
        public List<Int32> Int32ListPropertyExtra { get; init; } // custom property, can be ignored during hydration
    }

    // Custom DTO for mappping by position for hydration with only 4 column properties (Date and Timestamp columns are excluded)
    public record DtoCustomMapByPosition {                      // Column type and order must match, name and alias irrelevant.
        [HydratorMapAttribute(Position = 0)]                    // maps to column 0 (first column)
        public Int64? MyCol1 { get; init; }
        [HydratorMapAttribute(Position = 1)]                    // maps to column 1
        public Int64? MyCol2 { get; init; }
        [HydratorMapAttribute(Position = 2)]                    // maps to column 2
        public Decimal? MyCol3 { get; init; }
        [HydratorMapAttribute(Position = 3)]                    // maps to column 3
        public String MyCol4 { get; init; }

        public String StringPropertyExtra { get; init; }         // custom property, can be ignored during hydration
        public List<Int32> Int32ListPropertyExtra { get; init; } // custom property, can be ignored during hydration
    }

    public class Example {
        private const String HELLO = "Hello", GOODBYE = "Goodbye";

        public void Run() {
            (new OdapterExample.NET5.Example()).Test();
        }

        public void Test() {
            uint? rowLimit = 25;                                    // limit result sets to 25 rows, underlying table has over 1000 rows
            Int64? pInInt64 = 999999999999999999;                   // 18 digit long
            Decimal? pInDecimal = 79228162514264337593543950335M;   // 28 digit decimal (Decimal.MaxValue)
            String pInOutString = HELLO;
            DateTime? pOutDate;

            // List used as argument for Oracle associative array
            IList<Int64?> pInOutListInt64, somePrimeNumbers = new List<Int64?> { 2, 3, 5, 7, 11, 13, 17, 19, 29, 31 };

            // DTO IList<T>s and a datatable to be hydrated from Oracle cursor
            IList<DtoImplemented> dtoImplementedResultSet;
            IList<DtoImplementedWithCustom> dtoImplementedWithCustomResultSet;
            IList<DtoCustomMapByNamePositionalRecord> dtoOriginalMapByNamePositionalRecordResultSet;
            IList<DtoCustomMapByName> dtoOriginalMapByNameResultSet;
            IList<DtoCustomMapByPosition> dtoOriginalMapByPositionLResultSet;
            DataTable dataTable;

            // 1. Hydrate DTO IList<T> from typed result set by using DTO implementing package record interface.
            pInOutListInt64 = somePrimeNumbers;
            dtoImplementedResultSet = XmplPkgExample.Instance.GetRowsTypedRet<DtoImplemented>(pInDecimal, ref pInOutString, ref pInOutListInt64, out pOutDate, rowLimit);
            Debug.Assert(dtoImplementedResultSet.Count == rowLimit);
            Debug.Assert(pInOutString.Equals(GOODBYE));                             // confirm OUT string arg from package function
            for (int i = 0; i < pInOutListInt64.Count; i++)
                Debug.Assert(pInOutListInt64[i].Equals(somePrimeNumbers[i] * 7));   // confirm all values were multiplied by 7 in func
            Debug.Assert(pOutDate.Equals(new DateTime(1999, 12, 31)));              // confirm OUT date arg from package function

            // 2. Hydrate DTO IList<T> from typed result set by using DTO implementing package record interface with additional properties.
            pInOutListInt64 = somePrimeNumbers;
            dtoImplementedWithCustomResultSet = XmplPkgExample.Instance.GetRowsTypedRet<DtoImplementedWithCustom>(pInDecimal, ref pInOutString, ref pInOutListInt64, out pOutDate, rowLimit);
            Debug.Assert(dtoImplementedWithCustomResultSet.Count == rowLimit);
            Debug.Assert(pInOutString.Equals(GOODBYE));                             // confirm OUT string arg from package function
            for (int i = 0; i < pInOutListInt64.Count; i++)
                Debug.Assert(pInOutListInt64[i].Equals(somePrimeNumbers[i] * 7));   // confirm all values were multiplied by 7 in func
            Debug.Assert(pOutDate.Equals(new DateTime(1999, 12, 31)));              // confirm OUT date arg from package function

            // 3a. Hydrate DTO IList<T> from untyped result set by mapping column name to property name (arg default) using custom DTO; 
            //      unmapped columns will be ignored (arg non-default).
            dtoOriginalMapByNameResultSet = XmplPkgExample.Instance.GetRowsUntypedRet<DtoCustomMapByName>(pInInt64, false, true, rowLimit);
            Debug.Assert(dtoOriginalMapByNameResultSet.Count == rowLimit);

            // 3b. Hydrate DTO IList<T> from untyped result set by mapping column name to property name (arg default) using custom positional record DTO; 
            //      unmapped columns will be ignored (arg non-default).
            dtoOriginalMapByNamePositionalRecordResultSet = XmplPkgExample.Instance.GetRowsUntypedRet<DtoCustomMapByNamePositionalRecord>(pInInt64, false, true, rowLimit);
            Debug.Assert(dtoOriginalMapByNamePositionalRecordResultSet.Count == rowLimit);

            // 4. Hydrate DTO IList<T> from untyped result set by mapping column name to property name (default); 
            //      an unmapped column will throw (default).
            try {
                dtoOriginalMapByNameResultSet = XmplPkgExample.Instance.GetRowsUntypedRet<DtoCustomMapByName>(pInInt64, false, false, rowLimit);
                Debug.Assert(false);
            } catch (Exception ex) {
                if (!ex.Message.StartsWith("Hydrator.BuildMappings")) Debug.Assert(false);
            }

            // 5. Hydrate DTO IList<T> from untyped result set by mapping column position to property position (non-default); 
            //      unmapped columns will be ignored (non-default)
            dtoOriginalMapByPositionLResultSet = XmplPkgExample.Instance.GetRowsUntypedRet<DtoCustomMapByPosition>(pInInt64, true, true, rowLimit);

            // 6. Hydrate DTO IList<T> from untyped result set by mapping column position to property position (non-default); 
            //      an unmapped column will throw (default).
            try {
                dtoOriginalMapByPositionLResultSet = XmplPkgExample.Instance.GetRowsUntypedRet<DtoCustomMapByPosition>(pInInt64, true, false, rowLimit);
                Debug.Assert(false);
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