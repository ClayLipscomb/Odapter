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

namespace Odapter

open System
open System.Globalization

[<AutoOpen>]
module internal Case =
    let splitUnderscore str = split @"_" str
    let joinUnderscore str = join @"_" str
    let capitalize (str:string) = 
        if str |> trim |> isNullOrWhiteSpace then  
            emptyString
        else    
            str |> Seq.mapi (fun i ch -> match i with | 0 -> (Char.ToUpper(ch)) | _ -> Char.ToLower(ch)) |> String.Concat
    let deCapitalize (str:string) = 
        if str |> trim |> isNullOrWhiteSpace then  
            emptyString
        else    
            str |> Seq.mapi (fun i ch -> match i with | 0 -> (Char.ToLower(ch)) | _ -> Char.ToLower(ch)) |> String.Concat

[<AutoOpen>]
module internal PascalCase =
    let create input = WrappedString.create (*canonicalize:*) singleLineTrimmed PascalCase input
    let value = WrappedString.value
    let [<Literal>] private Ltrl_EXTRA_UNDERSCORE_SUBSTITUTE = @"extraunderscore"
    let private fromSnakeCaseRawToPascalCaseRaw str = 
        if str |> trim |> isNullOrWhiteSpace then  // is this check necessary?
            emptyString
        else
            // treat any non-underscore special characters like an underscore
            //newText = Regex.Replace(newText, "[^0-9a-zA-Z_]+", UNDERSCORE.ToString());
            str |> toLower |> splitUnderscore 
            |> Seq.map (fun word -> (if isNullOrWhiteSpace word then Ltrl_EXTRA_UNDERSCORE_SUBSTITUTE else word) |> capitalize)
            |> (String.concat emptyString)
    let ofSnakeCaseRaw str =
        if str |> trim |> isNullOrWhiteSpace then  
            None
        else    
            str |> fromSnakeCaseRawToPascalCaseRaw |> PascalCase |> Some
    let ofSnakeCase (SnakeCase snakeCaseStr) = snakeCaseStr |> fromSnakeCaseRawToPascalCaseRaw |> PascalCase

[<AutoOpen>]
module internal CamelCase =
    let create input = WrappedString.create (*canonicalize:*)singleLineTrimmed CamelCase input
    let value = WrappedString.value
    let ofSnakeCaseRaw str =
        match PascalCase.ofSnakeCaseRaw str with 
            | Some pc -> pc |> PascalCase.value |> deCapitalize |> CamelCase |> Some
            | None    -> None

[<AutoOpen>]
module internal CamelCaseUnderscorePrefixed =
    let create input = WrappedString.create (*canonicalize:*)singleLineTrimmed CamelCaseUnderscorePrefixed input
    let value = WrappedString.value

[<AutoOpen>]
module internal SnakeCase =
    let [<Literal>] private Ltrl_EXTRA_UNDERSCORE_SUBSTITUTE = @"extraunderscore"
    let private canonicalizeRawSnakeCase str = 
        if str |> isNullOrWhiteSpace then  // is this check necessary?
            emptyString
        else
            // ?? treat any non-underscore special characters like an underscore
            //newText = Regex.Replace(newText, "[^0-9a-zA-Z_]+", UNDERSCORE.ToString());
            str |> singleLineTrimmed|> toLower |> splitUnderscore 
            |> Seq.map (fun word -> (if isNullOrWhiteSpace word then Ltrl_EXTRA_UNDERSCORE_SUBSTITUTE else word))
            |> Case.joinUnderscore

    let create input = WrappedString.create (*canonicalize:*)canonicalizeRawSnakeCase SnakeCase input
    let value = WrappedString.value
