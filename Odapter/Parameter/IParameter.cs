﻿//------------------------------------------------------------------------------
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
    public interface IParameter  {
        string AncestorClassNameObjectType { get; set; }
        string AncestorClassNamePackage { get; set; }
        string AncestorClassNamePackageRecord { get; set; }
        string AncestorClassNameTable { get; set; }
        string AncestorClassNameView { get; set; }
        IList<string> ConfigFileNames { get; }
        string CSharpTypeUsedForOracleAssociativeArray { get; set; }
        string CSharpTypeUsedForOracleBFile { get; set; }
        string CSharpTypeUsedForOracleBlob { get; set; }
        string CSharpTypeUsedForOracleClob { get; set; }
        string CSharpTypeUsedForOracleDate { get; set; }
        string CSharpTypeUsedForOracleInteger { get; set; }
        string CSharpTypeUsedForOracleIntervalDayToSecond { get; set; }
        string CSharpTypeUsedForOracleNumber { get; set; }
        string CSharpTypeUsedForOracleRefCursor { get; set; }
        string CSharpTypeUsedForOracleTimeStamp { get; set; }
        CSharpVersion CSharpVersion { get; set; }
        string DatabaseInstance { get; set; }
        string Filter { get; set; }
        bool IsConvertOracleNumberToIntegerIfColumnNameIsId { get; set; }
        bool IsCSharp30 { get; }
        bool IsCSharp40 { get; }
        bool IsDataContractObjectType { get; set; }
        bool IsDataContractPackageRecord { get; set; }
        bool IsDataContractTable { get; set; }
        bool IsDataContractView { get; set; }
        bool IsDeployResources { get; set; }
        bool IsDuplicatePackageRecordOriginatingOutsideFilterAndSchema { get; set; }
        bool IsExcludeObjectsNamesWithSpecificChars { get; set; }
        bool IsGenerateBaseAdapter { get; set; }
        bool IsGenerateBaseEntities { get; set; }
        bool IsGenerateDynamicMappingMethodForTypedCursor { get; set; }
        bool IsGenerateObjectType { get; set; }
        bool IsGeneratePackage { get; set; }
        bool IsGenerateTable { get; set; }
        bool IsGenerateView { get; set; }
        bool IsIncludeFilterPrefixInNaming { get; set; }
        bool IsPartialObjectType { get; set; }
        bool IsPartialPackage { get; set; }
        bool IsPartialTable { get; set; }
        bool IsPartialView { get; set; }
        bool IsSavePassword { get; set; }
        bool IsSerializableObjectType { get; set; }
        bool IsSerializablePackageRecord { get; set; }
        bool IsSerializableTable { get; set; }
        bool IsSerializableView { get; set; }
        bool IsUseAutoImplementedProperties { get; set; }
        bool IsXmlElementObjectType { get; set; }
        bool IsXmlElementPackageRecord { get; set; }
        bool IsXmlElementTable { get; set; }
        bool IsXmlElementView { get; set; }
        string LocalVariableNameSuffix { get; set; }
        short MaxAssocArraySize { get; set; }
        short MaxReturnAndOutArgStringSize { get; set; }
        string NamespaceBase { get; set; }
        string NamespaceDataContract { get; set; }
        string NamespaceObjectType { get; set; }
        string NamespacePackage { get; set; }
        string NamespaceSchema { get; set; }
        string NamespaceTable { get; set; }
        string NamespaceView { get; set; }
        char[] ObjectNameCharsToExclude { get; set; }
        string ObjectNameCharsToExcludeAsString { get; set; }
        string OracleHome { get; set; }
        string OutputPath { get; set; }
        string Password { get; set; }
        string Schema { get; set; }
        string UserLogin { get; set; }
    }
}