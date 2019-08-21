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
//    along with this program. If not, see<http://www.gnu.org/licenses/>.
//------------------------------------------------------------------------------

using System;

namespace Odapter {
    internal sealed class TranslaterObjectEntity : ITranslaterEntity {
        public string DataTypeFull { get; private set; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.OBJECT); }

        internal TranslaterObjectEntity(string dataTypeFull) {
            DataTypeFull = dataTypeFull;
        }

        public string CSharpAccessModifier { get => CSharp.PUBLIC; }
        public string CSharpName { get => TranslaterName.ConvertToPascal(DataTypeFull); }
        public string CSharpType { get => CSharp.CLASS; }

        public override string ToString() { return DataTypeFull; }
    }
}