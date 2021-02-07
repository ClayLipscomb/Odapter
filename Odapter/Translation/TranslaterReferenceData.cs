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
//    along with this program.If not, see<http://www.gnu.org/licenses/>.
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Odapter {
    /// <summary>
    /// Handle translation from Oracle to C#
    /// </summary>
    public static class TranslaterReferenceData {
        public class CSharpVersionOption {
            public CSharpVersion Version { get; private set; }
            public string DisplayDescription { get; private set; }
            public CSharpVersionOption(CSharpVersion version, string displayDescription) {
                Version = version;
                DisplayDescription = displayDescription;
            }
        }

        public static readonly IList<CSharpVersionOption> CSharpOptions = new List<CSharpVersionOption> {
            new CSharpVersionOption(CSharpVersion.FourZero , @"4.0 (.NET 4.0 minimum, managed ODP.NET)")
        };

        public class CustomTranslatedCSharpType {
            public string CSharpType { get; private set; }
            private string TranslationNote { get; set; }
            public string DisplayDescription { get; private set; }
            public CustomTranslatedCSharpType(string cSharpType, string translationNote) {
                CSharpType = cSharpType;
                TranslationNote = translationNote;
                DisplayDescription = cSharpType + (string.IsNullOrWhiteSpace(translationNote) ? "" : " (" + translationNote + ")");
            }
        }

        public static readonly IDictionary<string, List<CustomTranslatedCSharpType>> CustomTypeTranslationOptions = new Dictionary<string, List<CustomTranslatedCSharpType>>() {
            {Orcl.REF_CURSOR, new List<CustomTranslatedCSharpType> {                new CustomTranslatedCSharpType(CSharp.ILIST, @""),
                                                                                    new CustomTranslatedCSharpType(CSharp.ICOLLECTION, @""),
                                                                                    new CustomTranslatedCSharpType(CSharp.LIST, @"concrete, not recommended") } },
            {Orcl.ASSOCIATITVE_ARRAY, new List<CustomTranslatedCSharpType> {        new CustomTranslatedCSharpType(CSharp.ILIST, @""),
                                                                                    new CustomTranslatedCSharpType(CSharp.LIST, @"concrete, not recommended") } },
            {Orcl.INTEGER, new List<CustomTranslatedCSharpType> {                   new CustomTranslatedCSharpType(CSharp.INT32, @"9 digit limit, not recommended"),
                                                                                    new CustomTranslatedCSharpType(CSharp.INT64, @"18 digit limit, usually safe"),
                                                                                    new CustomTranslatedCSharpType(CSharp.DECIMAL, @"28 digit limit"),
                                                                                    new CustomTranslatedCSharpType(CSharp.ODP_NET_SAFE_TYPE_DECIMAL, @"ODP.NET safe type struct") } },
            {Orcl.NUMBER, new List<CustomTranslatedCSharpType> {                    new CustomTranslatedCSharpType(CSharp.DECIMAL, @"28 dig limit, auto rounding"),
                                                                                    new CustomTranslatedCSharpType(CSharp.ODP_NET_SAFE_TYPE_DECIMAL, @"ODP.NET safe type struct") } },
            {Orcl.DATE, new List<CustomTranslatedCSharpType> {                      new CustomTranslatedCSharpType(CSharp.DATE_TIME, @"no BC"),                                                                                    
                                                                                    new CustomTranslatedCSharpType(CSharp.ODP_NET_SAFE_TYPE_DATE, @"ODP.NET safe type struct") } },
            {Orcl.TIMESTAMP, new List<CustomTranslatedCSharpType> {                 new CustomTranslatedCSharpType(CSharp.DATE_TIME, @"e-7 max, no BC"),
                                                                                    new CustomTranslatedCSharpType(CSharp.ODP_NET_SAFE_TYPE_TIMESTAMP, @"ODP.NET safe type struct") } },
            {Orcl.INTERVAL_DAY_TO_SECOND, new List<CustomTranslatedCSharpType> {    new CustomTranslatedCSharpType(CSharp.TIME_SPAN, @"e-7 max"),
                                                                                    new CustomTranslatedCSharpType(CSharp.ODP_NET_SAFE_TYPE_INTERVAL_DS, @"ODP.NET safe type struct") } },
            {Orcl.BLOB, new List<CustomTranslatedCSharpType> {                      new CustomTranslatedCSharpType(CSharp.BYTE_ARRAY, @""),
                                                                                    new CustomTranslatedCSharpType(CSharp.ODP_NET_SAFE_TYPE_BLOB, @"ODP.NET safe type class") } },
            {Orcl.CLOB, new List<CustomTranslatedCSharpType> {                      new CustomTranslatedCSharpType(CSharp.STRING, @""),
                                                                                    new CustomTranslatedCSharpType(CSharp.ODP_NET_SAFE_TYPE_CLOB, @"ODP.NET safe type class") } }
        };
    }
}