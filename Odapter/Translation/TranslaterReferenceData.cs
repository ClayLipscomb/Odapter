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
using Odapter.CSharp;
using CSL = Odapter.CSharp.Logic.Api;

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
            new CSharpVersionOption(CSharpVersion.FourZero , @"4.0 (.NET 4.0) minimum")
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
            {Orcl.REF_CURSOR, new List<CustomTranslatedCSharpType> {                        new CustomTranslatedCSharpType(TypeCollection.IList.Code, @""),
                                                                                            new CustomTranslatedCSharpType(TypeCollection.ICollection.Code, @""),
                                                                                            new CustomTranslatedCSharpType(TypeCollection.List.Code, @"concrete, not recommended") } },
            {Orcl.ASSOCIATITVE_ARRAY, new List<CustomTranslatedCSharpType> {                new CustomTranslatedCSharpType(TypeCollection.IList.Code, @""),
                                                                                            new CustomTranslatedCSharpType(TypeCollection.List.Code, @"concrete, not recommended") } },
            {Orcl.INTEGER, new List<CustomTranslatedCSharpType> {                           new CustomTranslatedCSharpType(TypeValue.Int32.Code, @"9 digit limit, not recommended"),
                                                                                            new CustomTranslatedCSharpType(TypeValue.Int64.Code, @"18 digit limit, usually safe"),
                                                                                            new CustomTranslatedCSharpType(TypeValue.Decimal.Code, @"28 digit limit"),
                                                                                            new CustomTranslatedCSharpType(TypeValue.OracleDecimal.Code, @"ODP.NET safe type struct") } },
            {Orcl.NUMBER, new List<CustomTranslatedCSharpType> {                            new CustomTranslatedCSharpType(TypeValue.Decimal.Code, @"28 dig limit, auto rounding"),
                                                                                            new CustomTranslatedCSharpType(TypeValue.OracleDecimal.Code, @"ODP.NET safe type struct") } },
            {Orcl.DATE, new List<CustomTranslatedCSharpType> {                              new CustomTranslatedCSharpType(TypeValue.DateTime.Code, @"no BC"),                                                                                    
                                                                                            new CustomTranslatedCSharpType(TypeValue.OracleDate.Code, @"ODP.NET safe type struct") } },
            {Orcl.TIMESTAMP, new List<CustomTranslatedCSharpType> {                         new CustomTranslatedCSharpType(TypeValue.DateTime.Code, @"e-7 max, no BC"),
                                                                                            new CustomTranslatedCSharpType(TypeValue.OracleTimeStamp.Code, @"ODP.NET safe type struct") } },
            {Orcl.TIMESTAMP_WITH_TIME_ZONE, new List<CustomTranslatedCSharpType> {          new CustomTranslatedCSharpType(TypeValue.DateTimeOffset.Code, @"e-7 max, no BC"),
                                                                                            new CustomTranslatedCSharpType(TypeValue.OracleTimeStampTZ.Code, @"ODP.NET safe type struct") } },
            {Orcl.TIMESTAMP_WITH_LOCAL_TIME_ZONE, new List<CustomTranslatedCSharpType> {    new CustomTranslatedCSharpType(TypeValue.DateTime.Code, @"e-7 max, no BC"),
                                                                                            new CustomTranslatedCSharpType(TypeValue.OracleTimeStampLTZ.Code, @"ODP.NET safe type struct") } },
            {Orcl.INTERVAL_DAY_TO_SECOND, new List<CustomTranslatedCSharpType> {            new CustomTranslatedCSharpType(TypeValue.TimeSpan.Code, @"e-7 max"),
                                                                                            new CustomTranslatedCSharpType(TypeValue.OracleIntervalDS.Code, @"ODP.NET safe type struct") } },
            {Orcl.BLOB, new List<CustomTranslatedCSharpType> {                              new CustomTranslatedCSharpType(CSL.TypeArrayOf(TypeValue.Byte).Code, @""),
                                                                                            new CustomTranslatedCSharpType(TypeReference.OracleBlob.Code, @"ODP.NET safe type class") } },
            {Orcl.CLOB, new List<CustomTranslatedCSharpType> {                              new CustomTranslatedCSharpType(TypeReference.String.Code, @""),
                                                                                            new CustomTranslatedCSharpType(TypeReference.OracleClob.Code, @"ODP.NET safe type class") } }
        };
    }
}