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

namespace Odapter.Oracle.Logic

open Odapter;
open Odapter.Oracle
open System
open System.Text.RegularExpressions

module NativeType =
    let isImplementedForAssocArray dataType = 
        match dataType with
        | Char | Nchar | Nvarchar2 | String | Varchar | Varchar2 
        | Date 
        | BinaryDouble | BinaryFloat | Decimal | DoublePrecision | Float | Integer | Number | Numeric | PlsInteger | Real | Smallint -> true
        | _ -> false

    let isTypeCustomizable nativeType = 
        match nativeType with
        | NativeType.ObjectType | NativeType.AssociativeArray | NativeType.Record -> true
        | _ -> false

    let toNativeType value =
        match value with
        // PL/SQL-specific scalar types
        | "REF CURSOR"                      -> NativeType.RefCursor
        | "PL/SQL RECORD"                   -> NativeType.Record
        | "PL/SQL TABLE"                    -> NativeType.AssociativeArray
        | "BINARY_INTEGER"                  -> BinaryInteger 
        | "NATURAL"                         -> Natural               
        | "NATURALN"                        -> Naturaln
        | "POSITIVE"                        -> Positive
        | "POSITIVEN"                       -> Positiven
        | "PLS_INTEGER"                     -> PlsInteger // same as BINARY_INTEGER
        | "PL/SQL BOOLEAN"                  -> PlsqlBoolean          
        | "BOOLEAN"                         -> NativeType.Boolean

        // SQL types, etc.
        | "OBJECT"                          -> ObjectType
        | "INTEGER"                         -> Integer   
        | "INT"                             -> Int        
        | "SMALLINT"                        -> Smallint
        | "UNSIGNED INTEGER"                -> UnsignedInteger
        | "STRING"                          -> NativeType.String
        | "NCHAR"                           -> Nchar                         
        | "VARCHAR"                         -> Varchar
        | "VARCHAR2"                        -> Varchar2
        | "NVARCHAR2"                       -> Nvarchar2
        | "CHAR"                            -> NativeType.Char
        | "BLOB"                            -> Blob                          
        | "CLOB"                            -> Clob
        | "NCLOB"                           -> Nclob
        | "LONG"                            -> Long
        | "ROWID"                           -> Rowid
        | "UROWID"                          -> Urowid
        | "REF"                             -> Ref
        | Ltrl_XMLTYPE                      -> Xmltype
        | "NUMBER"                          -> Number
        | "NUMERIC"                         -> Numeric
        | "FLOAT"                           -> Float
        | "DECIMAL"                         -> NativeType.Decimal
        | "DOUBLE PRECISION"                -> DoublePrecision
        | "REAL"                            -> Real   
        | "DATE"                            -> Date
        | "TIME WITH TIME ZONE"             -> TimeWithTimeZone
        | "RAW"                             -> Raw 
        | "LONG RAW"                        -> LongRaw
        | "BFILE"                           -> Bfile
        | "BINARY_DOUBLE"                   -> BinaryDouble
        | "BINARY_FLOAT"                    -> BinaryFloat
        | "TIMESTAMP"                       -> Timestamp 
        | "TIMESTAMP WITH LOCAL TIME ZONE"  -> TimestampWithLocalTimeZone 
        | "TIMESTAMP WITH TIME ZONE"        -> TimestampWithTimeZone
        | "INTERVAL DAY TO SECOND"          -> IntervalDayToSecond
        | "INTERVAL YEAR TO MONTH"          -> IntervalYearToMonth
        | "MLSLABEL"                        -> Mlslabel         // deprecated
        //| Ltrl_UNDEFINED                    -> Undefined
        | Ltrl_NULL                         -> NullType         // represents a NULL return type found only a procedure "parameter"
        | x -> failwithf "%s is invalid NativeType type" x
      

/// API for consumption by C#
module Api =
    let IsImplementedForAssocArray (dataType:NativeType) = NativeType.isImplementedForAssocArray dataType    
