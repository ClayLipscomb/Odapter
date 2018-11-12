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
using System.Collections.Generic;

namespace Odapter {
    /// <summary>
    /// Base abstract class Entity with list of attribute
    /// </summary>
    internal abstract class Entity {
        public List<IEntityAttribute> Attributes { get; set; }

        /// <summary>
        /// Determine if a given Oracle type is found in any of the attributes
        /// </summary>
        /// <param name="oracleType"></param>
        /// <returns></returns>
        private Boolean UsesOracleType(String oracleType) {
            if (Attributes == null) return false;
            return Attributes.FindIndex(a => a.AttrType.Equals(oracleType)) != -1;
        }

        internal Boolean IsIgnoredDueToOracleTypes(out String reasonMsg) {
            reasonMsg = "";

            foreach (String oraType in Translater.OracleTypesIgnored)
                if (UsesOracleType(oraType)) {
                    Translater.IsOracleTypeIgnored(oraType, out reasonMsg, Attributes[0].GetType().Name.ToLower()); // get reason
                    return true;
                }

            return false;
        }
    }
}
