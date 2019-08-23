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

using System;

namespace Odapter {
    // an argument to a funtion/proc
    /// <summary>
    /// An argument to a funtion/proc
    /// </summary>
    internal sealed class Argument : IArgument {
        public IArgument NextArgument { get; set; }
        public string Owner { get; set; }
        public string PackageName { get; set; }     // package containing argument
        public string ProcedureName { get { return objectName; } set { objectName = value; } } private string objectName { get; set; }
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
        public int? CharLength { get; set; }
        public string PlsType { get; set; }
        public string TypeOwner { get; set; }       // schema containing of TypeSubName definition
        public string TypeName { get; set; }        // 1) package containing TypeSubName defintion or 2) proper name of object type
        public string TypeSubname { get; set; }     // proper name of DataType (e.g., record name)
        public string TypeLink { get; set; }
        public bool Defaulted { get { return (defaulted.Equals("Y")); } } private string defaulted { get; set; }

        public bool IsReturnArgument { get { return (Position == 0 && DataLevel == 0 && ArgumentName == null && InOut == Orcl.OUT); } }
        public bool IsUntypedCursor { get => DataType == Orcl.REF_CURSOR && (NextArgument == null || NextArgument.DataLevel == DataLevel); }

        private string DataTypeDistilled { get => DataType == Orcl.UNDEFINED ? TypeName : DataType; }

        // ITyped specific
        public IOrclType OrclType { get; set; }
        public string PreNormalizedValues { get; set; }
        public string Aggregated { get => OrclUtil.BuildAggregateType(this); }

        // ITypedNameable specific
        public string DataTypeLabel { get => ArgumentName; }
        public string DataTypeProperName { get => OrclUtil.IsComplexDataType(DataTypeDistilled) ? (TypeSubname ?? TypeName) : String.Empty; }
        public string ContainerType { get => OrclUtil.IsComplexDataType(DataTypeDistilled) ? (TypeName ?? PackageName) : String.Empty; }
        public bool IsDefinedExternally { get => TypeName != null && PackageName != null && TypeName != PackageName; }
        public string NamingHelpValue { get => ProcedureName; }

        public ITyped SubType { get => 
                (DataType == Orcl.ASSOCIATITVE_ARRAY || DataType == Orcl.NESTED_TABLE || DataType == Orcl.VARRAY || (DataType == Orcl.REF_CURSOR && !IsUntypedCursor)) 
                ? NextArgument 
                : null; }
        public ITranslaterType Translater { get; set; }
    }
}