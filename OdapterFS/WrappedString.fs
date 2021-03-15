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

namespace global

[<AutoOpen>]
module internal WrappedString = 
    open System

    type IWrappedString = 
        abstract Value : string

    let create canonicalize (*isValid*) ctor str :'wrappedType = 
        if str |> String.IsNullOrWhiteSpace then // converts null to empty string
            emptyString |> canonicalize |> ctor
        else
            str |> canonicalize |> ctor

    let createOption canonicalize isValid ctor str = 
        if str |> String.IsNullOrWhiteSpace then 
            None
        else
            let str' = canonicalize str
            if isValid str' then Some (ctor str') else None

    let apply f (s:IWrappedString) = s.Value |> f 
    let value s = apply id s
    //let convert (s:IWrappedString) ctor = apply ctor s
    let equals left right = (value left) = (value right)
    let compareTo left right = (value left).CompareTo (value right)

    /// converts all whitespace to a space char and trim
    let singleLineTrimmed s = System.Text.RegularExpressions.Regex.Replace(s,"\s"," ").Trim()
    let singleLineTrimmedToTitleCase s = s |> singleLineTrimmed |> toTitleCase
    let lengthValidator len (s:string) = s.Length <= len 