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

namespace Odapter.CSharp.Logic

open System
open Odapter.CSharp

[<AutoOpen>]
module internal Coder =
    // Code a series of tabs
    let rec internal codeTab n = if n = 0u then String.Empty else replicate 4 SPACE + codeTab (n - 1u)

    //let toCodeTabbed : ToCodeTabbed = fun(codeable:ICodeable, tabCnt) -> 
    //    let splitNewLine str = (split NEWLINE str) |> Array.toSeq
    //    let joinNewLine strings = join NEWLINE strings
    //    codeable.Code |> splitNewLine |> Seq.map(fun s -> (if isNullOrWhiteSpace s then emptyString else codeTab tabCnt) + s) |> joinNewLine
    let codeTabbed tabCnt (object: Object) =
        let splitNewLine str = (split NEWLINE str) |> Array.toSeq
        let joinNewLine strings = join NEWLINE strings
        object.ToString() |> splitNewLine |> Seq.map(fun s -> (if isNullOrWhiteSpace s then emptyString else codeTab tabCnt) + s) |> joinNewLine
        // Before: abc NL def NL ghi NL ""
        // Split: abc def ghi ""
        // After: TABBEDabc NL TABBEDdef NL TABBEDghi NL ""

    let codeTabbedLines(objects, tabCnt) =
        let codeLinesPair (pc1: Object) (pc2: Object) = (pc1.ToString() + NEWLINE + pc2.ToString()) :> Object
        objects |> Seq.map (fun x -> x :> Object)
        |> Seq.reduce codeLinesPair
        |> codeTabbed tabCnt

    let codeTabbed1 object = codeTabbed 1u object
    let codeTabbed2 object = codeTabbed 2u object
    let codeTabbed3 object = codeTabbed 3u object
    let codeTabbed4 object = codeTabbed 4u object
    let codeTabbed5 object = codeTabbed 5u object
    let codeTabbed6 object = codeTabbed 6u object

    let private codeDelimited delimiter (objects: Object seq) = join delimiter (objects |> Seq.filter(fun x -> x.ToString() |> isNullOrWhiteSpace |> not) |> Seq.map(fun x -> x.ToString() ) )
    /// Code a collection of codeables as space delimited
    let codeAdjacent (objects: Object seq) = codeDelimited emptyString objects
    /// Code a collection of codeables as space delimited
    let codeSpaced (objects: Object seq) = codeDelimited SPACE objects
    /// Code a collection of codeables delimited by a comma and space
    let codeCommaSpaced (objects: Object seq) = codeDelimited ", " objects
    /// Code a collection of codeables as dot delimited
    let codeDotted (objects: Object seq) = codeDelimited PERIOD objects

    let inParens code = $"({code})"
    let inAngles code = $"<{code}>"
    let inBraces code = @"{" + $"{code}" + @"}"
    let inBrackets code = $"[{code}]"