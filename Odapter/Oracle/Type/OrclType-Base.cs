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
//    along with this program. If not, see<http://www.gnu.org/licenses/>.
//------------------------------------------------------------------------------

using System;

namespace Odapter {
    internal abstract class OrclTypeBase {

        private readonly INormalizable _normalizer;

        public virtual string BuildDataTypeFullName(ITyped dbDataType) {
            return dbDataType.OrclType.DataType;
        }

        public void NormalizePrecisionScale(ITyped dbDataType, out int? precision, out int? scale) {
            _normalizer.NormalizePrecisionScale(dbDataType, out precision, out scale);
        }

        public int? NormalizeCharLength(ITyped dbDataType) {
            return _normalizer.NormalizeCharLength(dbDataType);
        }

        protected OrclTypeBase() : this(new NormalizableDefault()) {  }

        protected OrclTypeBase(INormalizable normalizer) {
            _normalizer = normalizer;
        }
    }
}