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
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program. If not, see<http://www.gnu.org/licenses/>.
//------------------------------------------------------------------------------

/// Oracle domain types
namespace Odapter.Oracle

open System

// An Oracle type that be a translation source
type ITypeSource = interface end

type NativeType = // all possible SQL and PL/SQL data types found for an Oracle column, object attribute, record type field, etc.
    // PL/SQL-only types
    | AssociativeArray
    | BinaryInteger
    | Natural | Naturaln
    | NullType              // represents a NULL return type found only a procedure "parameter"
    | PlsqlBoolean | Boolean // Boolean found in pls_type column 
    | PlsInteger        // same as BINARY_INTEGER
    | Positive | Positiven
    | Record
    | RefCursor | Ref
    | Rowid | Urowid                        
    | String
    | Xmltype                       

    // SQL types, etc.
    | ObjectType
    | Integer | Int | Smallint | UnsignedInteger               
    | BinaryDouble | BinaryFloat                   
    | Number | Numeric | Decimal
    | DoublePrecision               
    | Float | Real                        
    | Char | Nchar | Varchar | Varchar2 | Nvarchar2 | Long                    
    | Bfile | Blob | Clob | Nclob | Raw | LongRaw
    | Date | TimeWithTimeZone | Timestamp | TimestampWithLocalTimeZone | TimestampWithTimeZone | IntervalDayToSecond | IntervalYearToMonth           
    | Mlslabel                       // deprecated
    interface ITypeSource 
    //| Undefined        

[<Struct>]
type TypeProcedureReturn = | ProcedureReturn with
    interface ITypeSource

type Entity = Record | ObjectEntity | Table | View
type Collection = AssociativeArray | NestedTable | Varray
//type DataTypeComplex = Record | AssociativeArray | Object | RefCursor | Varray | Xmltype
//type DataTypeProperName = DataTypeProperName of string option
type DataPrecision = DataPrecision of uint32 option
type DataScale = DataScale of int32 option
type DataLength = DataLength of uint32 option
type CharLength = CharLength of uint32 option


