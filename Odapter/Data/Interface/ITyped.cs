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

namespace Odapter {
    internal interface ITyped : ITypeNameable {
        IOrclType OrclType { get; set; }
        string DataType { get; set; }

        int? DataPrecision { get; set; }
        int? DataScale { get; set; }
        int? CharLength { get; set; }
        string PreNormalizedValues { get; set; }
        string Aggregated { get; }

        string PlsType { get; set; }
        // Option holder for SYS.all_types.typecode
        string Typecode { get; set; }
        ITyped SubType { get; }
        ITranslaterType Translater { get; set; }
    }
}