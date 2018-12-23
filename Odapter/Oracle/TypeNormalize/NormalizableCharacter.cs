﻿//------------------------------------------------------------------------------
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

namespace Odapter {
    internal class NormalizableCharacter : INormalizable {
        public void NormalizePrecisionScale(ITyped dbDataType, out int? precision, out int? scale) {
            precision = null;
            scale = null;
        }

        public int? NormalizeCharLength(ITyped dbDataType) {
            return dbDataType.CharLength >= 1 ? dbDataType.CharLength : null;
        }
    }
}