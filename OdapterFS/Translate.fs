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

namespace Odapter.Translation

open System
open Odapter.CSharp;
open Odapter.Casing;
open Odapter.CSharp.Logic

[<AutoOpen>]
module internal Naming =
    let [<Literal>] private UNDERSCORE = @"_"
    let [<Literal>] private CHARACTER_ABBREV = @"char"
    /// Accept nominally snake_case Oracle identifier and handle any anomalies
    let private normalizeOracleSnakeCase oracleIdentifier = 
        oracleIdentifier
        |> (fun s -> if isNullOrWhiteSpace s then emptyString else s)
        |> replace (@".", UNDERSCORE) // fully qualified Oracle name will have a dot notation (e.g., "MyPackage.MyRecordType")
        |> replace (@"-", UNDERSCORE)
        // replace special characters with alphabetic equivalent
        |> replace (@"!", @"exclamationpoint" + CHARACTER_ABBREV)
        |> replace (@"@", @"at" + CHARACTER_ABBREV)
        |> replace (@"#", @"pound" + CHARACTER_ABBREV)
        |> replace (@"$", @"dollar" + CHARACTER_ABBREV)
        |> replace (@"%", @"percent" + CHARACTER_ABBREV)
        |> replace (@"^", @"caret" + CHARACTER_ABBREV)
        |> replace (@"&", @"ampersand" + CHARACTER_ABBREV)
        |> replace (@"*", @"asterisk" + CHARACTER_ABBREV)
        |> replace (@"+", @"plus" + CHARACTER_ABBREV)
        |> replace (@"=", @"equals" + CHARACTER_ABBREV)
        |> replace (@"?", @"questionmark" + CHARACTER_ABBREV)
        |> replace (@":", @"colon" + CHARACTER_ABBREV)
        |> replace (@";", @"semicolon" + CHARACTER_ABBREV)
    
    let private snakeCaseOfOracleIdentifier = normalizeOracleSnakeCase >> SnakeCase.create
    let internal pascalCaseOfOracleIdentifier = snakeCaseOfOracleIdentifier >> PascalCase.ofSnakeCase 
    let internal camelCaseOfOracleIdentifier = snakeCaseOfOracleIdentifier >> CamelCase.ofSnakeCase 

    let internal classNameOfOracleIdentifier = pascalCaseOfOracleIdentifier >> ClassName.ofPascalCase
    let internal interfaceNameOfOracleIdentifier = classNameOfOracleIdentifier >> InterfaceName.ofClassName
    let internal propertyNameOfOracleIdentifier (identifier, identifierEntity) = 
        let className = identifierEntity |> classNameOfOracleIdentifier
        let propertyName = identifier |> pascalCaseOfOracleIdentifier |> PropertyName.ofPascalCase
        if className.Value = propertyName.Value then PropertyName.doubleName propertyName else propertyName
    let internal methodNameOfOracleIdentifier = pascalCaseOfOracleIdentifier >> MethodName.ofPascalCase
    let internal typeGenericNameOfOracleIdentifier = pascalCaseOfOracleIdentifier >> TypeGenericName.ofPascalCase
    let internal parameterNameOfOracleIdentifier = camelCaseOfOracleIdentifier >> Keyword.escapeKeyword >> ParameterName.ofCamelCase 
    let internal fieldNameProtectedOfOracleIdentifier = camelCaseOfOracleIdentifier >> Keyword.escapeKeyword  >> FieldNameProtected.ofCamelCase

module Api =
    let PascalCaseOfOracleIdentifier identifier = pascalCaseOfOracleIdentifier identifier
    let CamelCaseOfOracleIdentifier identifier = camelCaseOfOracleIdentifier identifier

    let ClassNameOfOracleIdentifier identifier = classNameOfOracleIdentifier identifier
    let InterfaceNameOfOracleIdentifier identfier = interfaceNameOfOracleIdentifier identfier
    let PropertyNameOfOracleIdentifier (identifier, identifierEntity) = propertyNameOfOracleIdentifier (identifier, identifierEntity)
    let MethodNameOfOracleIdentifier identifier = methodNameOfOracleIdentifier identifier
    let TypeGenericNameOfOracleIdentifier identifier = typeGenericNameOfOracleIdentifier identifier
    let ParameterNameOfOracleIdentifier identifier = parameterNameOfOracleIdentifier identifier
    let FieldNameProtectedOfOracleIdentifier identifier = fieldNameProtectedOfOracleIdentifier identifier