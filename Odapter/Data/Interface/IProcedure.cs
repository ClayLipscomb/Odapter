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
        bool IsFunction();

        /// <summary>
        /// Returns whether procedure has at least one OUT (not IN OUT) param, excluding the return 
        /// </summary>
        /// <returns></returns>
        bool HasOutArgument();

        /// <summary>
        /// Determine if procedure has an argument/return of a given Oracle type. Nested levels of argument
        ///     are not considered, only the main argument type.
        /// </summary>
        /// <param name="oracleType"></param>
        /// <returns></returns>
        bool HasArgumentOfOracleType(string oracleType);

        bool HasArgumentOfOracleType(IOrclType orclType);

        /// <summary>
        /// Does procedure have at least one weakly typed cursor as return or argument?
        /// </summary>
        /// <returns></returns>
        bool HasUntypedCursor();

        bool IsIgnoredDueToOracleTypes(out string reasonMsg);

        bool HasInArgumentOfOracleTypeRefCursor();
    }
}