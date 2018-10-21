Odapter - a C# code generator for Oracle packages
========================================

Odapter generates C# adapter classes that provide integration with an Oracle schema's packages. An adapter class handles the invocation of a package's procedures including the hydration of DTO Lists from returned cursor results sets, both typed (record type based) and untyped (simple REF CURSOR). From within the IDE, the generated C# provides the developer de facto compile-time resolution with the packages. Optionally, standalone C# DTOs can be generated from Oracle object types, tables and views. Odapter is a [single executable](/OdapterWnFrm/bin/x64/Release).

### Minimum System Requirements

* Oracle RDBMS 11g
* Windows 64-bit OS
* .NET Framework 
    - 4.0 minimum for code generator
    - 3.5 minimum for project
* ODP.NET for destination project
    - Managed Driver 12.2.1100 if targeting .NET Framework 4.0 or higher
    - Unmanaged Driver 12.2.0.1 if targeting .NET Framework 3.5

### Oracle to C# Translation Examples

| PL/SQL and Schema Objects                           | C# |
| --------------------------------------------------- | -------- |
| Case insensitive                                    | Case sensitive | 
| underscore_delimited naming                         | PascalCase & camelCase naming     |
| Package Record Type Field                           | Property     |
| Package Record Type                                 | Nested public class of properties    |
| Function or Stored Procedure (packaged)             | Method    |
| Package                                             | Singleton class of methods and nested classes    |
| Schema                                              | Namespace (with nesting)    |
| Object Type, Table, View                            | Class of properties    |

### Code Generation Features

* Generates adapter class for each package with respective method for each procedure/function
* Generates nested DTO class and interface for each record type
* Translates all common Oracle data types to C#
* Translates Oracle integer-indexed associative array type to C# List of the value type
* Configurable translation of Oracle INTEGER, NUMBER, DATE, TIMESTAMP types to C#, including options for ODP.NET safe types (OracleDecimal, OracleDate, OracleTimestamp)
* Configurable translation of Oracle BLOB and CLOB/NCLOB types to C#, including options for ODP.NET safe types (OracleBlob, OracleClob)
* Translates Oracle IN, OUT and IN OUT parameters to C#
* Translates Oracle optional (defaulted) parameters to C# (4.0+)
* Translates typed and untyped cursors (both as function return and OUT parameters) to C#
* Generates standalone DTO class for each object type, table, and view
* Configurable for either auto-implemented, or protected field wrapped, DTO properties
* Generates ancestor adapter class and DTO classes for customization
* Generates default database connection logic for customization
* Configurable C# namespaces and ancestor class names
* Generates post hook for profiling a package procedure invoked from C#
* Optionally filters schema objects via prefix and/or special characters
* Optionally generates C# classes as partial for packages, package record types, object types, tables and views
* Optionally generates C# DTOs with Serializable, DataContract/DataMember (incl. namespace) or XmlElementAttribute attributes for package record types, object types, tables and views
* Generates C# 3.0 or 4.0+ code (respectively .NET 3.5 or 4.0+)
* Generates single C# file for all packages, object types, tables and views, respectively
* Handles package referencing a record type defined in a different package (including filtered)
* Easily adaptable to legacy .NET projects and Oracle schemas
* Locates and parses local TNSNAMES.ORA for Oracle instances
* Persists custom generation settings to config file for multiple projects or schemas

### Run Time Features - Packages

* Invokes packaged functions and stored procedures
* Hydrates a List of (record type derived) DTOs from a returned (incl. OUT param) typed cursor result set
* Hydrates a List of DTOs from a returned untyped cursor result set using configurable mapping:
    - Mapping by name: column name to property name (underscore_delimited column translated to public PascalCase, protected camelCase or privated underscore prefixed camelCase property)
    - Mapping by position: column position to property position via attribute
    - For performance, uses thread-safe static cache for mappings of C# DTO to Oracle result set
* Constructs (from underlying columns) and hydrates DataTable from returned untyped cursor result set; captions are created from column name or alias
* Optionally limits the number of rows returned from any cursor result set

### Getting Started: Generating Code for Packages

1. Download Odapter.exe from [OdapterWnFrm/bin/x64/Release](/OdapterWnFrm/bin/x64/Release) and run
2. If Oracle Client Homes are found, select appropriate value
3. Select Instance (If TNSNAMES.ORA file is not found/parsed in Step 2, Instance can be entered)
4. Enter Schema, Login and Password
5. If your project uses only a prefixed subset of the schema's packages, enter Filter Prefix value
6. Enter the Output Path for all generated files (your project folder)
7. If your project targets the .NET 3.5 framework, select 3.0 as the C# Version
8. For all other fields, use default settings
9. Click Generate 
10. After successful generation, enter a project based .config file name in File Source and click Save Current
11. Open your project and add the generated files
12. If your project targets .NET 3.5, add a reference for the unmanaged ODP.NET driver; if .NET 4.0 or higher, add a reference for the managed ODP.NET driver.
13. Add "using Schema.YourSchemaName.YourFilterPrefixIfAny.Package" to project files in order to access packages

For examples, see code below and Tester/Tester.cs.

### Code Example
###### Package Specification - Tester/schema/package/xmpl_pkg_example.pks

```SQLPL
CREATE OR REPLACE PACKAGE ODPT.xmpl_pkg_example AS

    -- assoc array of integers
    TYPE t_assocarray_integer IS TABLE OF INTEGER INDEX BY PLS_INTEGER;  

    -- typed cursor
    TYPE t_table_big_partial IS RECORD (
        id                  odpt_table_big.id%TYPE,                 -- NUMBER
        col_integer         odpt_table_big.col_integer%TYPE,        -- INTEGER
        col_number          odpt_table_big.col_number%TYPE,         -- NUMBER
        col_varchar2_max    odpt_table_big.col_varchar2_max%TYPE,   -- VARCHAR2(4000)
        col_date            odpt_table_big.col_date%TYPE,           -- DATE
        col_timestamp       odpt_table_big.col_timestamp%TYPE);     -- TIMESTAMP
    TYPE t_ref_cursor_table_big_partial IS REF CURSOR RETURN t_table_big_partial;
	
    -- untyped cursor 
    TYPE t_ref_cursor IS REF CURSOR;
    
    FUNCTION get_rows_typed_ret (p_in_number IN NUMBER, p_in_out_varchar2 IN OUT VARCHAR2, p_in_out_assocarray_integer IN OUT t_assocarray_integer, 
        p_out_date OUT DATE) RETURN t_ref_cursor_table_big_partial;
    FUNCTION get_rows_untyped_ret (p_in_integer IN INTEGER) RETURN t_ref_cursor;
    
END xmpl_pkg_example;
/
```

###### Package Body  - Tester/schema/package/xmpl_pkg_example.pkb

```SQLPL
CREATE OR REPLACE PACKAGE BODY ODPT.xmpl_pkg_example AS
                                     
    FUNCTION get_rows_typed_ret (p_in_number IN NUMBER, p_in_out_varchar2 IN OUT VARCHAR2, p_in_out_assocarray_integer IN OUT t_assocarray_integer, 
            p_out_date OUT DATE) RETURN t_ref_cursor_table_big_partial IS
        l_cursor    t_ref_cursor_table_big_partial;
        l_idx		INTEGER;        
    BEGIN
        OPEN l_cursor FOR
        SELECT      id, col_integer, col_number, col_varchar2_max, col_date, col_timestamp
        FROM        odpt_table_big
        ORDER BY    id;    

        -- multiply each value in assoc array by 7 before returning
        l_idx := p_in_out_assocarray_integer.FIRST;
        WHILE l_idx IS NOT NULL LOOP
            p_in_out_assocarray_integer(l_idx) := p_in_out_assocarray_integer(l_idx) * 7;
            l_idx := p_in_out_assocarray_integer.NEXT(l_idx);
        END LOOP;
        
        p_in_out_varchar2 := 'Goodbye';        
        p_out_date := TO_DATE ('31-DEC-1999');        
        RETURN l_cursor;
    END;	

    FUNCTION get_rows_untyped_ret (p_in_integer IN INTEGER) RETURN t_ref_cursor IS
        l_cursor    t_ref_cursor;
    BEGIN    
        OPEN l_cursor FOR
           'SELECT      id, col_integer, col_number, 
                        col_varchar2_max varchar2_max_col /* use alias to test DataTable caption */, 
                        col_date, col_timestamp 
            FROM        odpt_table_big
            ORDER BY    id';    

        RETURN l_cursor;
    END;	
    
END xmpl_pkg_example;
/
```

###### Generation 

![](Example/ExampleCodeScreenShot.png "")

###### Generated Code - Example/generated/OdptXmplPackage.cs

```C#
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by Odapter on Sat, 20 Oct 2018 02:30:55 GMT.
//     Direct edits will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Collections;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using Odapter;

namespace Schema.Odpt.Xmpl.Package {

    public partial class XmplPkgExample : Schema.Odpt.Xmpl.OdptAdapter {
        private XmplPkgExample() { }
        private static XmplPkgExample _instance = new XmplPkgExample();
        public static XmplPkgExample Instance { get { return _instance; } }

        public interface ITTableBigPartial {
            Int64? Id { get; set; }
            Int64? ColInteger { get; set; }
            Decimal? ColNumber { get; set; }
            String ColVarchar2Max { get; set; }
            DateTime? ColDate { get; set; }
            OracleTimeStamp? ColTimestamp { get; set; }
        } // ITTableBigPartial

        [DataContract(Namespace="http://corp.xmpl.com")][Serializable()]
        public partial class TTableBigPartial : Schema.Odpt.Xmpl.OdptPackageRecord, ITTableBigPartial {
            private Byte propertyToEnsuresPartialClassNamesAreUniqueAtCompileTime { get; set; }
            [DataMember(Order=0, IsRequired=false)][XmlElement(Order=0, IsNullable=true)]
            public virtual Int64? Id { get; set; }
            [DataMember(Order=1, IsRequired=false)][XmlElement(Order=1, IsNullable=true)]
            public virtual Int64? ColInteger { get; set; }
            [DataMember(Order=2, IsRequired=false)][XmlElement(Order=2, IsNullable=true)]
            public virtual Decimal? ColNumber { get; set; }
            [DataMember(Order=3, IsRequired=false)][XmlElement(Order=3, IsNullable=true)]
            public virtual String ColVarchar2Max { get; set; }
            [DataMember(Order=4, IsRequired=false)][XmlElement(Order=4, IsNullable=true)]
            public virtual DateTime? ColDate { get; set; }
            [DataMember(Order=5, IsRequired=false)][XmlElement(Order=5, IsNullable=true)]
            public virtual OracleTimeStamp? ColTimestamp { get; set; }
        } // TTableBigPartial

        public List<T_TTableBigPartial> ReadResultITTableBigPartial<T_TTableBigPartial>(OracleDataReader rdr, UInt32? optionalMaxNumberRowsToReadFromAnyCursor = null)
                where T_TTableBigPartial : class, ITTableBigPartial, new()   {
            List<T_TTableBigPartial> __ret = new List<T_TTableBigPartial>();
            if (rdr != null && rdr.HasRows) {
                while (rdr.Read()) {
                    T_TTableBigPartial obj = new T_TTableBigPartial();
                    if (!rdr.IsDBNull(0)) obj.Id = Convert.ToInt64(rdr.GetValue(0));
                    if (!rdr.IsDBNull(1)) obj.ColInteger = Convert.ToInt64(rdr.GetValue(1));
                    if (!rdr.IsDBNull(2)) obj.ColNumber = (Decimal?)OracleDecimal.SetPrecision(rdr.GetOracleDecimal(2), 29);
                    if (!rdr.IsDBNull(3)) obj.ColVarchar2Max = Convert.ToString(rdr.GetValue(3));
                    if (!rdr.IsDBNull(4)) obj.ColDate = Convert.ToDateTime(rdr.GetValue(4));
                    if (!rdr.IsDBNull(5)) obj.ColTimestamp = (OracleTimeStamp?)rdr.GetOracleValue(5);
                    __ret.Add(obj);
                    if (optionalMaxNumberRowsToReadFromAnyCursor != null && __ret.Count >= optionalMaxNumberRowsToReadFromAnyCursor) break;
                }
            }
            return __ret;
        } // ReadResultITTableBigPartial

        public List<T_TTableBigPartial> GetRowsTypedRet<T_TTableBigPartial>(Decimal? pInNumber, ref String pInOutVarchar2, ref List<Int64?> pInOutAssocarrayInteger, out DateTime? pOutDate, 
                UInt32? optionalMaxNumberRowsToReadFromAnyCursor = null, OracleConnection optionalPreexistingOpenConnection = null)
                where T_TTableBigPartial : class, ITTableBigPartial, new() {
            List<T_TTableBigPartial> __ret = new List<T_TTableBigPartial>(); pOutDate = null; 
            OracleConnection __conn = optionalPreexistingOpenConnection ?? GetConnection();
            try {
                using (OracleCommand __cmd = new OracleCommand("ODPT.XMPL_PKG_EXAMPLE.GET_ROWS_TYPED_RET", __conn)) {
                    __cmd.CommandType = CommandType.StoredProcedure;
                    __cmd.BindByName = true;
                    __cmd.Parameters.Add(new OracleParameter("!RETURN", OracleDbType.RefCursor, null, ParameterDirection.ReturnValue));
                    __cmd.Parameters.Add(new OracleParameter("P_IN_NUMBER", OracleDbType.Decimal, pInNumber, ParameterDirection.Input));
                    __cmd.Parameters.Add(new OracleParameter("P_IN_OUT_VARCHAR2", OracleDbType.Varchar2, 32767, pInOutVarchar2, ParameterDirection.InputOutput));

                    __cmd.Parameters.Add(new OracleParameter("P_IN_OUT_ASSOCARRAY_INTEGER", OracleDbType.Int64, 1000, null, ParameterDirection.InputOutput));
                    __cmd.Parameters["P_IN_OUT_ASSOCARRAY_INTEGER"].Value = (pInOutAssocarrayInteger == null || pInOutAssocarrayInteger.Count == 0 ? new Int64?[]{} : pInOutAssocarrayInteger.ToArray());
                    __cmd.Parameters["P_IN_OUT_ASSOCARRAY_INTEGER"].CollectionType = OracleCollectionType.PLSQLAssociativeArray;
                    __cmd.Parameters.Add(new OracleParameter("P_OUT_DATE", OracleDbType.Date, null, ParameterDirection.Output));

                    OracleCommandTrace __cmdTrace = IsTracing(__cmd) ? new OracleCommandTrace(__cmd) : null;
                    int __rowsAffected = __cmd.ExecuteNonQuery();
                    if (!((OracleRefCursor)__cmd.Parameters["!RETURN"].Value).IsNull)
                        using (OracleDataReader __rdr = ((OracleRefCursor)__cmd.Parameters["!RETURN"].Value).GetDataReader()) {
                            __ret = ReadResultITTableBigPartial<T_TTableBigPartial>(__rdr, optionalMaxNumberRowsToReadFromAnyCursor);
                        } // using OracleDataReader
                    pInOutVarchar2 = __cmd.Parameters["P_IN_OUT_VARCHAR2"].Status == OracleParameterStatus.NullFetched
                        ? (String)null
                        : Convert.ToString(__cmd.Parameters["P_IN_OUT_VARCHAR2"].Value.ToString());

                    pInOutAssocarrayInteger = new List<Int64?>();
                    for (int _i = 0; _i < (__cmd.Parameters["P_IN_OUT_ASSOCARRAY_INTEGER"].Value as OracleDecimal[]).Length; _i++)
                        pInOutAssocarrayInteger.Add((__cmd.Parameters["P_IN_OUT_ASSOCARRAY_INTEGER"].Value as OracleDecimal[])[_i].IsNull
                            ? (Int64?)null 
                            : Convert.ToInt64(((__cmd.Parameters["P_IN_OUT_ASSOCARRAY_INTEGER"].Value as OracleDecimal[])[_i].ToString())));

                    pOutDate = __cmd.Parameters["P_OUT_DATE"].Status == OracleParameterStatus.NullFetched
                        ? (DateTime?)null
                        : Convert.ToDateTime(__cmd.Parameters["P_OUT_DATE"].Value.ToString());
                    if (__cmdTrace != null) TraceCompletion(__cmdTrace, __ret.Count);
                } // using OracleCommand
            } finally {
                if (optionalPreexistingOpenConnection == null) {
                    __conn.Close();
                    __conn.Dispose();
                }
            }
            return __ret;
        } // GetRowsTypedRet

        public List<T_returnUntyped> GetRowsUntypedRet<T_returnUntyped>(Int64? pInInteger, 
                bool mapColumnToObjectPropertyByPosition = false, bool allowUnmappedColumnsToBeExcluded = false, UInt32? optionalMaxNumberRowsToReadFromAnyCursor = null, 
                OracleConnection optionalPreexistingOpenConnection = null)
                where T_returnUntyped : class, new() {
            List<T_returnUntyped> __ret = new List<T_returnUntyped>(); 
            OracleConnection __conn = optionalPreexistingOpenConnection ?? GetConnection();
            try {
                using (OracleCommand __cmd = new OracleCommand("ODPT.XMPL_PKG_EXAMPLE.GET_ROWS_UNTYPED_RET", __conn)) {
                    __cmd.CommandType = CommandType.StoredProcedure;
                    __cmd.BindByName = true;
                    __cmd.Parameters.Add(new OracleParameter("!RETURN", OracleDbType.RefCursor, null, ParameterDirection.ReturnValue));
                    __cmd.Parameters.Add(new OracleParameter("P_IN_INTEGER", OracleDbType.Int64, pInInteger, ParameterDirection.Input));

                    OracleCommandTrace __cmdTrace = IsTracing(__cmd) ? new OracleCommandTrace(__cmd) : null;
                    int __rowsAffected = __cmd.ExecuteNonQuery();
                    if (!((OracleRefCursor)__cmd.Parameters["!RETURN"].Value).IsNull)
                        using (OracleDataReader __rdr = ((OracleRefCursor)__cmd.Parameters["!RETURN"].Value).GetDataReader()) {
                            __ret = Hydrator.ReadResult<T_returnUntyped>(__rdr, mapColumnToObjectPropertyByPosition, allowUnmappedColumnsToBeExcluded, optionalMaxNumberRowsToReadFromAnyCursor);
                        } // using OracleDataReader
                    if (__cmdTrace != null) TraceCompletion(__cmdTrace, __ret.Count);
                } // using OracleCommand
            } finally {
                if (optionalPreexistingOpenConnection == null) {
                    __conn.Close();
                    __conn.Dispose();
                }
            }
            return __ret;
        } // GetRowsUntypedRet

        public DataTable GetRowsUntypedRet(Int64? pInInteger, Boolean convertColumnNameToTitleCaseInCaption = false, UInt32? optionalMaxNumberRowsToReadFromAnyCursor = null, OracleConnection optionalPreexistingOpenConnection = null) {
            DataTable __ret = null; 
            OracleConnection __conn = optionalPreexistingOpenConnection ?? GetConnection();
            try {
                using (OracleCommand __cmd = new OracleCommand("ODPT.XMPL_PKG_EXAMPLE.GET_ROWS_UNTYPED_RET", __conn)) {
                    __cmd.CommandType = CommandType.StoredProcedure;
                    __cmd.BindByName = true;
                    __cmd.Parameters.Add(new OracleParameter("!RETURN", OracleDbType.RefCursor, null, ParameterDirection.ReturnValue));
                    __cmd.Parameters.Add(new OracleParameter("P_IN_INTEGER", OracleDbType.Int64, pInInteger, ParameterDirection.Input));

                    OracleCommandTrace __cmdTrace = IsTracing(__cmd) ? new OracleCommandTrace(__cmd) : null;
                    int __rowsAffected = __cmd.ExecuteNonQuery();
                    if (!((OracleRefCursor)__cmd.Parameters["!RETURN"].Value).IsNull)
                        using (OracleDataReader __rdr = ((OracleRefCursor)__cmd.Parameters["!RETURN"].Value).GetDataReader()) {
                            __ret = Hydrator.ReadResult(__rdr, convertColumnNameToTitleCaseInCaption, optionalMaxNumberRowsToReadFromAnyCursor);
                        } // using OracleDataReader
                    if (__cmdTrace != null) TraceCompletion(__cmdTrace, __ret.Rows.Count);
                } // using OracleCommand
            } finally {
                if (optionalPreexistingOpenConnection == null) {
                    __conn.Close();
                    __conn.Dispose();
                }
            }
            return __ret;
        } // GetRowsUntypedRet
    } // XmplPkgExample
} // Schema.Odpt.Xmpl.Package
```

###### Executing Generated Code - Example/Example.cs

```C#
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Schema.Odpt.Xmpl.Package;         // generated code for packages
using Oracle.ManagedDataAccess.Types;   // ODP.NET safe types
using Odapter;                          // attribute used to map by position

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
        public OracleTimeStamp? ColTimestamp { get; set; }      // ODP.NET safe type struct
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
        [HydratorMapAttribute(Position = 0)]            // maps to column 0 (first column)
        public Int64? MyCol1 { get; set; }
        [HydratorMapAttribute(Position = 1)]            // maps to column 1
        public Int64? MyCol2 { get; set; }
        [HydratorMapAttribute(Position = 2)]            // maps to column 2
        public Decimal? MyCol3 { get; set; }
        [HydratorMapAttribute(Position = 3)]            // maps to column 3
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
            uint? rowLimit = 25;                                    // limit result sets to 25 rows, underlying table has over 1000 rows
            Int64? pInInt64 = 999999999999999999;                   // 18 digit long
            Decimal? pInDecimal = 79228162514264337593543950335M;   // 28 digit decimal (Decimal.MaxValue)
            String pInOutString = HELLO;
            DateTime? pOutDate;

            // List used as argument for Oracle associative array
            List<Int64?> pInOutListInt64, somePrimeNumbers = new List<Int64?> { 2, 3, 5, 7, 11, 13, 17, 19, 29, 31 };

            // DTO Lists and a datatable to be hydrated from Oracle cursor
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
```
