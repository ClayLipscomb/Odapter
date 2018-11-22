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

//using System;
using System.Collections.Generic;

namespace Odapter {
    /// <summary>
    /// A procedure/function
    /// </summary>
    internal class Procedure : IProcedure {
        public string PackageName { get { return objectName; } set { objectName = value; } }
        private string objectName { get; set; }
        public string ProcedureName { get; set; }
        public string Overload { get; set; }
        public List<IArgument> Arguments { get; set; }

        /// <summary>
        /// if a function, gets the return Oracle type
        /// </summary>
        public string ReturnOracleDataType {
            get { return (Arguments.Count == 0 || Arguments[0].DataLevel != 0 || Arguments[0].Position != 0 ? null : Arguments[0].DataType); }
        }

        /// <summary>
        /// Returns whether this stored proc is a function
        /// </summary>
        /// <returns>boolean</returns>
        public bool IsFunction() {
            return (Arguments.Count == 0 ? false : (Arguments[0].DataLevel == 0 && Arguments[0].Position == 0 && Arguments[0].InOut == Orcl.OUT));
        }

        /// <summary>
        /// Returns whether procedure has at least one OUT (not IN OUT) param, excluding the return 
        /// </summary>
        /// <returns></returns>
        public bool HasOutArgument() {
            foreach (IArgument arg in Arguments) if ((arg.DataLevel != 0 || arg.Position != 0) && arg.InOut.Equals(Orcl.OUT)) return true; // explicit OUT
            return false;
        }

        /// <summary>
        /// Determine if a given Oracle type is found in any of the arguments/return
        /// </summary>
        /// <param name="oracleType"></param>
        /// <param name="checkNestedArgs">Whether nestted levels should be examined</param>
        /// <returns></returns>
        public bool HasArgumentOfOracleType(string oracleType, bool checkNestedArgs = false) {
            if (Arguments == null) return false;
            // only search nested arguments if specified
            return Arguments.FindIndex(a => (a.DataType == oracleType || a.TypeName == oracleType || a.PlsType == oracleType) 
                                            && (checkNestedArgs || a.DataLevel == 0)) != -1;
        }

        /// <summary>
        /// Does procedure have at least one weakly typed cursor as return or argument?
        /// </summary>
        /// <returns></returns>
        public bool UsesWeaklyTypedCursor() {
            foreach (IArgument arg in Arguments) {
                // when we reach last arg, we must return here with a simple check: a cursor at the end is weakly typed
                if (Arguments.IndexOf(arg) == Arguments.Count - 1) return (arg.DataType == Orcl.REF_CURSOR ? true : false);

                // check if argument is cursor and its subsequent argument is on the same data level - this reveals a weakly typed cursro
                if (arg.DataType == Orcl.REF_CURSOR && Arguments[Arguments.IndexOf(arg) + 1].DataLevel == arg.DataLevel) return true;
            }
            return false; // we found weakly typed cursors
        }

        /// <summary>
        /// Does procedure have at least one associative array of an unimplemented type as return or argument?
        /// </summary>
        /// <returns></returns>
        public bool HasArgumentOfOracleTypeAssocArrayOfUnimplementedType(out string unimplementedType) {
            unimplementedType = null;
            foreach (IArgument arg in Arguments) {
                if (Arguments.IndexOf(arg) == Arguments.Count - 1) return false; // reached end of arg list since assoc array uses "2 args"
                // check type of argument and its subsequent argument
                if (arg.DataType == Orcl.ASSOCIATITVE_ARRAY
                        && !Translater.TypesImplementedForAssociativeArrays.Contains(Arguments[Arguments.IndexOf(arg) + 1].DataType)) {
                    unimplementedType = Arguments[Arguments.IndexOf(arg) + 1].DataType;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines if proc has a cursor IN parameter
        /// </summary>
        /// <returns></returns>
        public bool HasInArgumentOfOracleTypeRefCursor() {
            foreach (IArgument arg in Arguments) if (arg.DataType.Equals(Orcl.REF_CURSOR) && arg.InOut.StartsWith(Orcl.IN)) return true; // IN or IN OUT
            return false;
        }
    }
}
