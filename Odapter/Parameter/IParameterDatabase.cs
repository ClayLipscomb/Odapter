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
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.If not, see<http://www.gnu.org/licenses/>.
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Odapter {
    public interface IParameterDatabase {
        string DatabaseInstance { get; set; }
        string Schema { get; set; }
        string Filter { get; set; }
        string UserLogin { get; set; }
        string Password { get; set; }

        bool IsExcludeObjectsNamesWithSpecificChars { get; set; }
        char[] ObjectNameCharsToExclude { get; set; }
        string ObjectNameCharsToExcludeAsString { get; set; }
    }
}