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
using System.Linq;
using CS = Odapter.CSharp;
using Trns = Odapter.Translation.Api;

namespace Odapter {
    internal static class TranslaterName  {
        /// <summary>
        /// Determine if proc translated to C# has a duplicate signature of another translated proc in the package. PL/SQL signatures are 
        /// duplicate only if the procs have the same proc name, same parameter count, same respective parameter directions, 
        /// and same respective parameter types. So as long as there is a difference in parameter names, a duplicate signature will not occur. 
        /// But in C#, both parameter names and parameter (translated) types must be different to prevent a duplicate signature (and compiler error).
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="package"></param>
        /// <returns></returns>
        private static bool HasDuplicateSignatureTranslated(IProcedure proc, IPackage package) {
            return package.Procedures.Exists(p =>
                p.ProcedureName.Equals(proc.ProcedureName)  // same proc name
                && !p.IsIgnoredDueToOracleTypes(out _)
                && !(p.Overload ?? String.Empty).Equals(proc.Overload ?? String.Empty)  // different overload
                && ((p.Arguments.Where(a => !a.IsReturnArgument).OrderBy(a => a.Defaulted).ThenBy(a => a.Sequence).Select(a => a.InOut + a.Translater.CSharpType))
                        .SequenceEqual  // params count, direction and type are exact match (excl. return arg)
                    (proc.Arguments.Where(a => !a.IsReturnArgument).OrderBy(a => a.Defaulted).ThenBy(a => a.Sequence).Select(a => a.InOut + a.Translater.CSharpType)))
                    // ordering moves all defaulted (defaulted="Y") past required (defaulted="N")
                );
        }

        /// <summary>
        /// Create C# method name for a procedure
        /// </summary>
        /// <param name="proc"></param>
        /// <returns></returns>
        internal static CS.MethodName MethodNameOf(IProcedure proc, IPackage package) {
            return Trns.MethodNameOfOracleIdentifier(proc.ProcedureName
                + (((proc.PackageName ?? String.Empty) == (proc.ProcedureName ?? String.Empty)) ? @"_PROC" : String.Empty)
                + ((HasDuplicateSignatureTranslated(proc, package) && !proc.IsIgnoredDueToOracleTypes(out _)) ? proc.Overload : String.Empty) );
        }
    }
}