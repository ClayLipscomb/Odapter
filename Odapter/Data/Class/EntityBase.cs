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
using System.Collections.Generic;

namespace Odapter {
    internal abstract class EntityBase : IEntityBase {
        public List<IEntityAttribute> Attributes { get; set; }
        public string Owner { get; set; }

        /// <summary>
        /// Determine whether entity should be ignored due to certain data types
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="reasonMsg"></param>
        /// <returns></returns>
        public bool IsIgnoredDueToOracleTypes(out string reasonMsg) {
            if (this is PackageRecord) {
                var attr = this.Attributes.Find(a => a.Translater.IsIgnoredAsRecordField().isIgnored);
                if (attr != null) {
                    reasonMsg = attr.Translater.IsIgnoredAsRecordField().reasonMsg;
                    return true;
                }
            } else {
                var attr = this.Attributes.Find(a => a.Translater.IsIgnoredAsAttribute);
                if (attr != null) {
                    reasonMsg = attr.Translater.IgnoredReasonAsAttribute;
                    return true;
                }
            }

            reasonMsg = String.Empty;
            return false;
        }
    }
}