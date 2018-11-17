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
using System.Linq;

namespace Odapter {
    internal class Package : IPackage {
        public string PackageName { get { return objectName; } set { objectName = value; } }  private string objectName { get; set; } // object_name is underlying sys view column
        public string Owner { get; set; }
        public List<IProcedure> Procedures { get; set; }

        /// <summary>
        /// Determine if proc has a duplicate signature of another proc in the package. Signatures are duplicate if the procs
        /// have the same name, same param count, same respective param directions and same respective param types. In PL/SQL,
        /// they would not considered a duplicate as long there was a difference in param names. In C#, theses type of duplicate
        /// methods would not complie.
        /// </summary>
        /// <param name="proc"></param>
        /// <returns></returns>
        public Boolean HasDuplicateSignature(IProcedure proc) {
            return Procedures.Exists(p =>
                p.ProcedureName.Equals(proc.ProcedureName)  // same proc name
                && !(p.Overload ?? String.Empty).Equals(proc.Overload ?? String.Empty)  // different overload
                && ((p.Arguments.Where(a => !a.IsReturnArgument).OrderBy(a => a.Sequence).Select(a => a.InOut + a.DataType))
                        .SequenceEqual  // params count, direction and type are exact match (excl. return arg)
                    (proc.Arguments.Where(a => !a.IsReturnArgument).OrderBy(a => a.Sequence).Select(a => a.InOut + a.DataType)))
                );
        }
    }
}
