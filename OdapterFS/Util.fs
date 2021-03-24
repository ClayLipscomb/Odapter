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
module internal UtilUnion =
    open System
    open Microsoft.FSharp.Reflection

    let getUnionCases<'union> = FSharpType.GetUnionCases typeof<'union> |> Array.toSeq

    let createUnionCase<'union> (unionCases:seq<UnionCaseInfo>) str =
        match unionCases |> Seq.toArray |> Array.filter (fun case -> case.Name = str) with
        |[|case|] -> Some(FSharpValue.MakeUnion(case,[||]) :?> 'union)
        |_ -> None

    // convert DU value to string
    //let fromDuCaseToString (unionCase:'union) = 
    //    let (case, _ ) = FSharpValue.GetUnionFields(unionCase, typeof<'union>)
    //    case.Name

    let fromDuCaseToString<'union> (unionCase:'union) = 
        let (case, _ ) = FSharpValue.GetUnionFields(unionCase, typeof<'union>)
        case.Name

    // works but BAD PERF!!!!
    let _fromStringToDuCase<'a> (s:string) =
        match FSharpType.GetUnionCases typeof<'a> |> Array.filter (fun case -> case.Name = s) with
        |[|case|] -> Some(FSharpValue.MakeUnion(case,[||]) :?> 'a)
        |_ -> None