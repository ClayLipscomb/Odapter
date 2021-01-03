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

namespace Odapter {
    internal interface ITranslaterType  {
        /// <summary>
        /// The fully qualified effective data type that is unique to instance of any translater.
        /// </summary>
        string DataTypeFull { get; }

        /// <summary>
        /// The base Oracle data type. Any attributes such as precision or subtype are excluded.
        /// </summary>
        IOrclType OrclType { get; }

        // translation to C#
        string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false);
        bool IsValid(ITyped dataType);
        string CSharpOracleDbType { get; }
        string CSharpOdpNetType { get; }
        /// <summary>
        /// Should type be ignored during translation as a procedure parameter
        /// </summary>
        bool IsIgnoredAsParameter { get; }
        /// <summary>
        /// Reason if ignored during translation as a procedure parameter
        /// </summary>
        string IgnoredReasonAsParameter { get; }
        /// <summary>
        /// Should type be ignored during translation as an entity attribute
        /// </summary>
        bool IsIgnoredAsAttribute { get; }
        /// <summary>
        /// Reason if ignored during translation as an entity attribute
        /// </summary>
        string IgnoredReasonAsAttribute { get; }
    }

    // types explicitly not implemented in ODP.NET managed: ARRAY (Varray, Nested Table), BOOLEAN, OBJECT, REF, XML_TYPE
    // https://docs.oracle.com/database/121/ODPNT/OracleDbTypeEnumerationType.htm#ODPNT2286
    // VARRAY, NESTED_TABLE, PLSQL_BOOLEAN, BOOLEAN, OBJECT_TYPE, REF, XMLTYPE,
}
