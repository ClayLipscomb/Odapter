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

open System
open System.Globalization

[<AutoOpen>]
module internal StringExtension =
    let toUpper (str: string) = str.ToUpper()
    let toLower (str: string) = str.ToLower() 
    let startsWith prefix (str: string) = str.StartsWith(prefix)
    let endsWith (prefix: string) (str : string) = str.EndsWith(prefix)
    let toCharArray (str: string) = str.ToCharArray()
    let replace (oldValue: string, newValue: string) (str: string) = str.Replace(oldValue, newValue)
    let trimStart chars (str: string) = str.TrimStart(chars)
    let trimEnd chars (str: string) = str.TrimEnd(chars)
    let trim (str: string) = str.Trim()
    let concat (sep: string) (strings: string seq) = String.concat sep strings
    let replicate count str = String.replicate count str
    let isNullOrWhiteSpace str = String.IsNullOrWhiteSpace str
    let isNull (str: string) = str = null
    let emptyString = String.Empty
    let length = String.length
    let subString startIndex length (str: string) = str.Substring(startIndex, length)
    let subStringToEnd startIndex (str: string) = str.Substring(startIndex)
    let toTitleCase (str: string) = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(str)
    let join sep (strings: string seq) = String.Join(sep, strings)
    let split (separator: string) (str: string) = str.Split([|separator|], StringSplitOptions.None)

    let (|IsNullOrWhiteSpace|_|) str =
        match isNullOrWhiteSpace str with
        | true -> Some ()
        | false -> None