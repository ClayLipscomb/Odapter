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
    internal interface IProcedure {
        string PackageName { get; set; } 
        string ProcedureName { get; set; }
        string Overload { get; set; }
        List<IArgument> Arguments { get; set; }

        /// <summary>
        /// if a function, gets the return Oracle type
        /// </summary>
        string ReturnOracleDataType { get; }

        /// <summary>
        /// Returns whether this stored proc is a function
        /// </summary>
        /// <returns>boolean</returns>
        Boolean IsFunction();

        /// <summary>
        /// Returns whether procedure has at least one OUT (not IN OUT) param, excluding the return 
        /// </summary>
        /// <returns></returns>
        Boolean HasOutArgument();

        /// <summary>
        /// Determine whether procedure should be ignored due to certain data types
        /// </summary>
        /// <param name="reasonMsg"></param>
        /// <returns></returns>
        Boolean IsIgnoredDueToOracleArgumentTypes(out String reasonMsg);

        /// <summary>
        /// Determine if procedure has an argument/return of a given Oracle type. Nested levels of argument
        ///     are not considered, only the main argument type.
        /// </summary>
        /// <param name="oracleType"></param>
        /// <returns></returns>
        Boolean HasArgumentOfOracleType(String oracleType);

        /// <summary>
        /// Does procedure have at least one weakly typed cursor as return or argument?
        /// </summary>
        /// <returns></returns>
        Boolean UsesWeaklyTypedCursor();


        /// <summary>
        /// Does procedure have at least one associative array of an unimplemented type as return or argument?
        /// </summary>
        /// <returns></returns>
        Boolean HasArgumentOfOracleTypeAssocArrayOfUnimplementedType(out String unimplementedType);

        Boolean HasInArgumentOfOracleTypeRefCursor();
    }
}