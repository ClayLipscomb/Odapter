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

namespace Odapter {
    internal sealed class TranslaterMessage  {
        private static readonly string IGNORE_NOT_IMPLEMENTED                       = "Code generation for {0} type has not been implemented";
        private static readonly string IGNORE_NOT_AVAILABLE_ODP_NET_MANAGED         = "{0} type is not available in ODP.NET managed";
        private static readonly string IGNORE_NO_SEND_RECEIVE                       = ".NET cannot send/receive a {0} type";
        private static readonly string IGNORE_ORACLE_DEPRECATION                    = "Code generation for {0} type will not be implemented due to Oracle deprecation";
        private static readonly string IGNORE_NO_SEND_RECEIVE_RECORD                = $".NET cannot send/receive a {Orcl.RECORD} type (apart from cursor)";
        private static readonly string IGNORE_ORACLE_NO_RECEIVE                     = "Oracle cannot receive a {0} type as a parameter";
        private static readonly string IGNORE_NO_SEND_REF_CURSOR                    = $".NET cannot send a {Orcl.REF_CURSOR} type";
        private static readonly string IGNORE_NO_SEND_ASSOC_ARRAY_UNIMPLEMENTED     = $".NET cannot send/receive an {Orcl.ASSOCIATITVE_ARRAY}" + " of a {0} type";
        private static readonly string IGNORE_NO_SEND_RECEIVE_RECORD_FIELD_TYPE     = ".NET cannot send/receive an {0} type field in a record";

        private static string FormatOracleTypes(string txt) {
            return txt.Replace(@"PLSQL", @"PL/SQL").Replace(Orcl.ASSOCIATITVE_ARRAY, "associative array");
        }

        internal static string FormatMsg(string msg, string dataTypeInsert = null) {
            return FormatOracleTypes(String.Format(msg, dataTypeInsert));
        }

        internal static string IgnoreNotImplemented(string orclType)                            { return FormatMsg(IGNORE_NOT_IMPLEMENTED, orclType); }
        internal static string IgnoreNotImplemented(IOrclType orclType)                         { return FormatMsg(IGNORE_NOT_IMPLEMENTED, orclType.DataType); }
        internal static string IgnoreNotAvailableOdpNetMananged(IOrclType orclType)             { return FormatMsg(IGNORE_NOT_AVAILABLE_ODP_NET_MANAGED, orclType.DataType); }
        internal static string IgnoreNoSendReceive(IOrclType orclType)                          { return FormatMsg(IGNORE_NO_SEND_RECEIVE, orclType.DataType); }
        internal static string IgnoreNoSendReceiveRecord()                                      { return FormatMsg(IGNORE_NO_SEND_RECEIVE_RECORD); }
        internal static string IgnoreOracleNoReceive(IOrclType orclType)                        { return FormatMsg(IGNORE_ORACLE_NO_RECEIVE, orclType.DataType); }
        internal static string IgnoreOracleDeprecation(IOrclType orclType)                      { return FormatMsg(IGNORE_ORACLE_DEPRECATION, orclType.DataType); }
        internal static string IgnoreNoSendRefCusror()                                          { return FormatMsg(IGNORE_NO_SEND_REF_CURSOR); }
        internal static string IgnoreNoSendAssocArrayUnimplemented(IOrclType orclType)          { return FormatMsg(IGNORE_NO_SEND_ASSOC_ARRAY_UNIMPLEMENTED, orclType.DataType); }
        internal static string IgnoreNoSendReceiveRecordFieldTyped(IOrclType orclType)          { return FormatMsg(IGNORE_NO_SEND_RECEIVE_RECORD_FIELD_TYPE, orclType.DataType); }
    }
}