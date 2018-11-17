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
    internal class Argument : IArgument {
        public IArgument NextArgument { get; set; }
        public string Owner { get; set; }
        public string PackageName { get; set; }
        public string ProcedureName { get { return objectName; } set { objectName = value; } }
        private string objectName { get; set; }
        public string Overload { get; set; }
        public int DataLevel { get; set; }
        public string ArgumentName { get; set; }
        public int Position { get; set; }
        public int Sequence { get; set; }
        public string DataType { get; set; }
        public string InOut { get; set; }
        public int? DataLength { get; set; }
        public int? DataPrecision { get; set; }
        public int? DataScale { get; set; }
        public string PlsType { get; set; }
        public string TypeOwner { get; set; }
        public string TypeName { get; set; }
        public string TypeSubname { get; set; }
        public string TypeLink { get; set; }
        public int? CharLength { get; set; }
        public bool Defaulted { get { return (defaulted == "Y" ? true : false); } }
        private string defaulted { get; set; }
        public bool IsReturnArgument { get { return (Position == 0 && DataLevel == 0 && ArgumentName == null && InOut == Orcl.OUT); } }
    }
}
