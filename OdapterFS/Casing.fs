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

namespace Odapter.Casing

open System
open System.Text.RegularExpressions

[<Struct>]
type SnakeCase = internal SnakeCase of string with
    member this.Value = let (SnakeCase s) = this in s
    interface IWrappedString with
        member this.Value = this.Value
[<Struct>]
type PascalCase = internal PascalCase of string with
    member this.Value = let (PascalCase s) = this in s
    interface IWrappedString with
        member this.Value = this.Value
[<Struct>]
type CamelCase = internal CamelCase of string with
    member this.Value = let (CamelCase s) = this in s
    interface IWrappedString with
        member this.Value = this.Value
[<Struct>]
type CamelCaseUnderscorePrefixed = internal CamelCaseUnderscorePrefixed of string with
    member this.Value = let (CamelCaseUnderscorePrefixed s) = this in s
    interface IWrappedString with
        member this.Value = this.Value

[<AutoOpen>]
module internal Util =
    let [<Literal>] UNDERSCORE = @"_"
    let splitUnderscore str = split UNDERSCORE str

[<AutoOpen>]
module internal SnakeCase =
    let private joinUnderscore str = join Util.UNDERSCORE str
    let [<Literal>] private Ltrl_EXTRA_UNDERSCORE_SUBSTITUTE = @"extraunderscore"
    let value = WrappedString.value
    let private canonicalizeRawSnakeCase str = 
        if str |> isNullOrWhiteSpace then 
            emptyString
        else
            Regex.Replace(str, @"[^0-9a-zA-Z_]+", Util.UNDERSCORE) // treat any non-underscore special characters like an underscore
            |> singleLineTrimmed|> toLower |> Util.splitUnderscore 
            |> Seq.map (fun word -> (if isNullOrWhiteSpace word then Ltrl_EXTRA_UNDERSCORE_SUBSTITUTE else word))
            |> joinUnderscore
    let create input = WrappedString.create (*canonicalize:*)canonicalizeRawSnakeCase SnakeCase input

[<AutoOpen>]
module internal PascalCase =
    let create input = WrappedString.create (*canonicalize:*)singleLineTrimmed PascalCase input
    let value = WrappedString.value
    let map f (PascalCase pascalCaseStr) = pascalCaseStr |> f |> create
    let private fromSnakeCaseStrToPascalCaseStr str = 
        if str |> trim |> isNullOrWhiteSpace then 
            emptyString
        else
            str |> toLower |> Util.splitUnderscore |> Seq.map capitalize |> (String.concat emptyString)
    let ofSnakeCase (SnakeCase snakeCaseStr) = snakeCaseStr |> fromSnakeCaseStrToPascalCaseStr |> create

[<AutoOpen>]
module internal CamelCase =
    let private create input = WrappedString.create (*canonicalize:*)singleLineTrimmed CamelCase input
    let value = WrappedString.value
    let map f (CamelCase camelCaseStr) = camelCaseStr |> f |> create
    let ofPascalCase (PascalCase pascalCaseStr) = pascalCaseStr |> deCapitalize |> create
    let ofSnakeCase snakeCase = snakeCase |> PascalCase.ofSnakeCase |> ofPascalCase

[<AutoOpen>]
module internal CamelCaseUnderscorePrefixed =
    let private create input = WrappedString.create (*canonicalize:*)singleLineTrimmed CamelCaseUnderscorePrefixed input
    let value = WrappedString.value
    let ofCamelCase (CamelCase camelCaseStr) = (Util.UNDERSCORE + camelCaseStr) |> create

//module Api =
//    let Capitalize str = capitalize str
//    let DeCapitalize str = deCapitalize str