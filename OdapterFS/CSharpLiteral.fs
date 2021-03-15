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

namespace OdapterFS.CSharp

[<AutoOpen>]
module internal CSharpLiteral =
    let UNDERSCORE = @"_"
    let PERIOD = @"."
    let SEMICOLON = @";"
    let SPACE = @" "
    let NEWLINE = @"\r\n"

    // general constants
    let [<Literal>] Ltrl_UNDEFINED = @"UNDEFINED"
    let [<Literal>] Ltrl_ORACLE = @"Oracle"

    // code constants
    let [<Literal>] Ltrl_BRACKETS = @"[]"
    let [<Literal>] Ltrl_CODE_NULLABLE_SUFFIX = @"?"
    let [<Literal>] Ltrl_LT = @"<"
    let [<Literal>] Ltrl_GT = @">"
    let [<Literal>] Ltrl_CURLY_OPEN = @"{"
    let [<Literal>] Ltrl_CURLY_CLOSE = @"}"
    let [<Literal>] Ltrl_CODE_INTEFACE_PREFIX = @"I"
    let [<Literal>] Ltrl_CODE_ATTRIBUTE_SERIALIZABLE = @"[Serializable()]"
    let [<Literal>] Ltrl_CODE_ATTRIBUTE_DATA_CONTRACT = @"DataContract"