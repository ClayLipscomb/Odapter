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

namespace Odapter {
    internal static class TranslaterName  {
        private const string CHARACTER_ABBREV = "char";

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
                && !(p.Overload ?? String.Empty).Equals(proc.Overload ?? String.Empty)  // different overload
                && ((p.Arguments.Where(a => !a.IsReturnArgument).OrderBy(a => a.Sequence).Select(a => a.InOut + a.Translater.GetCSharpType()))
                        .SequenceEqual  // params count, direction and type are exact match (excl. return arg)
                    (proc.Arguments.Where(a => !a.IsReturnArgument).OrderBy(a => a.Sequence).Select(a => a.InOut + a.Translater.GetCSharpType())))
                );
        }

        /// <summary>
        /// Convert an Oracle entity/object name (table, package, argument, column etc.) to a valid C# equivalent 
        /// </summary>
        /// <param name="oracleArgName"></param>
        /// <param name="useCamelCase">convert to camelCase, otherwise defaults to PascalCase</param>
        /// <returns></returns>
        private static string Convert(string oracleName, bool useCamelCase) {
            if (String.IsNullOrEmpty(oracleName)) return null; // this occurs with a return arg

            string oracleNameAdjusted = oracleName;

            oracleNameAdjusted = oracleNameAdjusted.Replace(".", Orcl.UNDERSCORE); // fully qualified Oracle name will have a dot notation (e.g., "MyPackage.MyRecordType")

            // replace special characters with alphabetic equivalent
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"!", "exclamationpoint" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"@", "at" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"#", "pound" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"$", "dollar" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"%", "percent" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"^", "caret" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"&", "ampersand" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"*", "asterisk" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"-", "dash" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"+", "plus" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"=", "equals" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"?", "questionmark" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@":", "colon" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@";", "semicolon" + CHARACTER_ABBREV);

            string cSharpName = (useCamelCase
                ? CaseConverter.ConvertUnderscoreDelimitedToCamelCase(oracleNameAdjusted)
                : CaseConverter.ConvertUnderscoreDelimitedToPascalCase(oracleNameAdjusted));
            if (Char.IsDigit(cSharpName, 0)) cSharpName = (useCamelCase ? "t" : "T") + "he" + cSharpName; // a C# arg cannot start with number
            if (CSharp.IsKeyword(cSharpName)) cSharpName = cSharpName + "Cs"; // append text to avoid the C# keyword
            return cSharpName;
        }

        internal static string ConvertToPascal(string oracleName) { return Convert(oracleName, false); }

        internal static string ConvertToCamel(string oracleName) { return Convert(oracleName, true); }

        /// <summary>
        /// Create C# method name for a procedure
        /// </summary>
        /// <param name="proc"></param>
        /// <returns></returns>
        internal static string Convert(IProcedure proc, IPackage package) {
            string methodName = ConvertToPascal(proc.ProcedureName);

            // prevent identical class name and method name - yes, I've seen this happen in Oracle
            if (proc.PackageName != null && proc.PackageName == proc.ProcedureName) methodName += "Proc";

            // if proc has duplicate sig, append overload number to name to keep unique in C#
            if (HasDuplicateSignatureTranslated(proc, package)) methodName += proc.Overload;

            return methodName;
        }

        internal static string Convert(IPackage package) { return ConvertToPascal(package.PackageName); }

        internal static string Convert(IArgument arg) { return ConvertToCamel(arg.ArgumentName); }

        /// <summary>
        /// Convert an Oracle entity attribute name to a C# property name
        /// </summary>
        /// <param name="attrib"></param>
        /// <returns></returns>
        internal static string Convert(IEntityAttribute attrib) {
            string propertyName = ConvertToPascal(attrib.AttrName);
            // prevent identical class name and property name (not allowed by C#) by "doubling" the name
            if ((attrib.EntityName ?? "").Equals(attrib.AttrName)) propertyName += propertyName;

            return propertyName; 
        }
    }
}