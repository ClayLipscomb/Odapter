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

namespace Odapter.CSharp

open System;
[<AutoOpen>]
module Attribute =
    let [<Literal>] SERIALIZABLE = @"[Serializable()]"
    let [<Literal>] DATA_CONTRACT = @"DataContract"
[<AutoOpen>]
module Literal =
    let [<Literal>] internal GENERIC_PREFIX = @"Type"
    let [<Literal>] internal PERIOD = @"."
    let [<Literal>] COLON = @":"
    let [<Literal>] internal SEMICOLON = @";"
    let [<Literal>] internal SPACE = @" "
    let internal NEWLINE = Environment.NewLine// @"\r\n"

    // code constants
    let [<Literal>] internal BRACKETS = @"[]"
    let [<Literal>] internal NULLABLE_SUFFIX = @"?"
    let [<Literal>] internal LT = @"<"
    let [<Literal>] internal GT = @">"
    let [<Literal>] internal CURLY_OPEN = @"{"
    let [<Literal>] internal CURLY_CLOSE = @"}"
    let [<Literal>] internal EQUALS = @"="
