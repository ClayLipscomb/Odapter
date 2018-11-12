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

using System;

namespace Odapter {
    // an argument to a funtion/proc
    /// <summary>
    /// An argument to a funtion/proc
    /// </summary>
    internal class Argument {
        internal Argument NextArgument { get; set; }
        internal string Owner { get; set; }
        internal string PackageName { get; set; }
        internal string ProcedureName { get { return objectName; } set { objectName = value; } }
        private string objectName { get; set; }
        internal string Overload { get; set; }
        internal int DataLevel { get; set; }
        internal string ArgumentName { get; set; }
        internal int Position { get; set; }
        internal int Sequence { get; set; }
        internal string DataType { get; set; }
        internal string InOut { get; set; }
        internal int? DataLength { get; set; }
        internal int? DataPrecision { get; set; }
        internal int? DataScale { get; set; }
        internal string PlsType { get; set; }
        internal string TypeOwner { get; set; }
        internal string TypeName { get; set; }
        internal string TypeSubname { get; set; }
        internal string TypeLink { get; set; }
        internal int? CharLength { get; set; }
        internal bool Defaulted { get { return (defaulted == "Y" ? true : false); } }
        private string defaulted { get; set; }
        internal bool IsReturnArgument { get { return (Position == 0 && DataLevel == 0 && ArgumentName == null && InOut == Orcl.OUT); } }
    }
}
