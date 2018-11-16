//------------------------------------------------------------------------------
//    Odapter - a C# code generator for Oracle packages
//    Copyright(C) 2018 Clay Lipscomb
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
    internal interface IArgument {
        Argument NextArgument { get; set; }
        string Owner { get; set; }
        string PackageName { get; set; }
        string ProcedureName { get; set; } 
        string Overload { get; set; }
        int DataLevel { get; set; }
        string ArgumentName { get; set; }
        int Position { get; set; }
        int Sequence { get; set; }
        string DataType { get; set; }
        string InOut { get; set; }
        int? DataLength { get; set; }
        int? DataPrecision { get; set; }
        int? DataScale { get; set; }
        string PlsType { get; set; }
        string TypeOwner { get; set; }
        string TypeName { get; set; }
        string TypeSubname { get; set; }
        string TypeLink { get; set; }
        int? CharLength { get; set; }
        bool Defaulted { get; } 
        bool IsReturnArgument { get; } 
    }
}