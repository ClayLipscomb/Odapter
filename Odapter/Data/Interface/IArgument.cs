//------------------------------------------------------------------------------
//    Odapter - a C# code generator for Oracle packages
//    Copyright(C) 2019 Clay Lipscomb
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

namespace Odapter {
    internal interface IArgument : ITyped {
        IArgument NextArgument { get; set; }
        string Owner { get; set; }
        string PackageName { get; set; }        // package containing argument
        string ProcedureName { get; set; } 
        string Overload { get; set; }
        int DataLevel { get; set; }
        string ArgumentName { get; set; }
        int Position { get; set; }
        int Sequence { get; set; }
        string InOut { get; set; }
        string TypeOwner { get; set; }          // schema containing of TypeSubName definition
        string TypeName { get; set; }           // 1) package containing TypeSubName defintion or 2) proper name of object type
        string TypeSubname { get; set; }        // proper name of DataType (e.g., record name)
        string TypeLink { get; set; }
        bool Defaulted { get; } 
        bool IsReturnArgument { get; }
        bool IsUntypedCursor { get; }
    }
}