﻿//------------------------------------------------------------------------------
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
//    along with this program.If not, see<http://www.gnu.org/licenses/>.
//------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Odapter {
    /// <summary>
    /// Converts between various "cases" like camelCase, PascalCase, underscore_delimited
    /// </summary>
    internal class CaseConverter {
        #region Constant and Static members
        private const char UNDERSCORE = '_';
        private static readonly TextInfo _textInfo = new CultureInfo("en-US", false).TextInfo;
        private const string DEFAULT_NON_STANDARD_UNDERSCORE_REPLACEMENT = @"EXTRAUNDERSCORE";
        //private const string PREFIX_FOR_NUMERIC_WORD = @"N";
        #endregion

        #region Case Conversion Methods
        /// <summary>
        /// Convert an underscore_delimited string to PascalCase
        /// </summary>
        /// <param name="oldText"></param>
        /// <returns></returns>
        internal static string ConvertSnakeCaseToPascalCase(String oldText, String nonStandardUnderscoreReplacement = null) {
            if (String.IsNullOrEmpty(oldText)) return string.Empty;
            string newText = oldText.Trim();

            // treat any non-underscore special characters like an underscore
            newText = Regex.Replace(newText, "[^0-9a-zA-Z_]+", UNDERSCORE.ToString());

            string[] token = newText?.ToLower()?.Trim()?.Split(UNDERSCORE);
            newText = string.Empty;
            foreach (string t in token) {
                string word = String.IsNullOrWhiteSpace(t) ? ConvertToCapitalized(nonStandardUnderscoreReplacement ?? DEFAULT_NON_STANDARD_UNDERSCORE_REPLACEMENT) : t;
                //word = (!String.IsNullOrWhiteSpace(word) && Char.IsDigit(word[0]) ? PREFIX_FOR_NUMERIC_WORD : String.Empty) + word;
                newText += _textInfo.ToTitleCase(word);
            }

            // We must guarantee uniqueness with leading/trailing underscores. In other words, if you 
            //  begin or end an underscore-delimited name with an underscore, we will either preserve
            //  the underscore as a character (non-standard for PascalCase) or replace it with the word 
            //  "Underscore" to keep a pure PascalCase (preferred).
            //if (!preserveLeadingAndTrailingUnderscores && oldText.EndsWith(UNDERSCORE.ToString())) newText += ConvertToCapitalized(UNDERSCORECHAR_WORD);
            //if (!preserveLeadingAndTrailingUnderscores && oldText.StartsWith(UNDERSCORE.ToString())) newText = ConvertToCapitalized(UNDERSCORECHAR_WORD) + newText;
            return newText;
        }

        /// <summary>
        /// Convert an underscore_delimited string to camelCase
        /// </summary>
        /// <param name="oldText"></param>
        /// <returns>camelCase string</returns>
        internal static string ConvertSnakeCaseToCamelCase(String oldText, String nonStandardUnderscoreReplacement = null) {
            if (String.IsNullOrEmpty(oldText)) return string.Empty;
            String pascalCase = ConvertSnakeCaseToPascalCase(oldText, nonStandardUnderscoreReplacement);
            return pascalCase?.Substring(0, 1)?.ToLower() + (pascalCase?.Length == 1 ? string.Empty : pascalCase?.Substring(1));
        }

        /// <summary>
        /// Convert underscored_delimited string into a Title Case 
        /// </summary>
        /// <param name="columnName">underscore deliminted string</param>
        /// <returns>title case label</returns>
        internal static string ConvertSnakeCaseToLabel(string columnName) =>
            // assume words are delimited by underscore
            CultureInfo.CurrentCulture.TextInfo.ToTitleCase(columnName.ToLower().Replace(UNDERSCORE.ToString(), " "));

        /// <summary>
        /// Convert string to title case 
        /// </summary>
        /// <param name="columnName">underscore delimited string</param>
        /// <returns>title case label</returns>
        internal static string ConvertToCapitalized(string value) {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            value = value.Trim();
            return char.ToUpper(value[0]) + (value.Length > 1 ? value.Substring(1).ToLower() : "");
        }

        /// <summary>
        /// Convert an underscore_delimited string to _camelCasePrefixedWithUnderscore
        /// </summary>
        /// <param name="oldText"></param>
        /// <returns></returns>
        internal static string ConvertSnakeCaseToCamelCasePrefixedWithUnderscore(String oldText, String nonStandardUnderscoreReplacement = null) =>
            UNDERSCORE + ConvertSnakeCaseToCamelCase(oldText, nonStandardUnderscoreReplacement);
        #endregion
    }
}