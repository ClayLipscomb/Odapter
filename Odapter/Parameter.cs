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
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.Xml.Serialization;

namespace Odapter {
    [Serializable]
    public class Parameter {
        private Parameter() { RestoreDefaults(); }
        private static Parameter _instance = new Parameter();
        public static Parameter Instance { get { return _instance; } set { _instance = value; } }

        public void RestoreDefaults() {
            OracleHome = "";
            DatabaseInstance = "";
            Filter = "";
            Schema = "";
            UserLogin = "";
            Password = "";
            OutputPath = "";

            IsGeneratePackage = true;
            IsGenerateObjectType = IsGenerateTable = IsGenerateView = false;
            IsPartialPackage = IsPartialPackageRecord = IsPartialObjectType = IsPartialTable = IsPartialView = false;
            IsSerializablePackageRecord = IsSerializableObjectType = IsSerializableTable = IsSerializableView = false;
            IsXmlElementPackageRecord = IsXmlElementObjectType = IsXmlElementTable = IsXmlElementView = false;
            IsDataContractPackageRecord = IsDataContractObjectType = IsDataContractTable = IsDataContractView = false;
            IsIncludeFilterPrefixInNaming = true;

            NamespaceBase = "Schema";
            NamespacePackage = NamespaceObjectType = NamespaceTable = NamespaceView = "";
            NamespaceSchema = "";
            NamespaceDataContract = "";

            AncestorClassNamePackage = AncestorClassNamePackageRecord = AncestorClassNameObjectType = AncestorClassNameTable = AncestorClassNameView = "";

            MaxAssocArraySize = 1000;
            MaxReturnAndOutArgStringSize = 8000;
            CSharpVersion = CSharpVersion.FourZero;
            IsDuplicatePackageRecordOriginatingOutsideFilterAndSchema = true;
            IsExcludeObjectsNamesWithSpecificChars = true;
            ObjectNameCharsToExclude = new char[] { '$', '#' };
            LocalVariableNameSuffix = "__";
            IsGenerateDynamicMappingMethodForTypedCursor = false;
            IsConvertOracleNumberToIntegerIfColumnNameIsId = true;
            IsUseAutoImplementedProperties = true;

            CSharpTypeUsedForOracleInteger = CSharp.INT64;
            CSharpTypeUsedForOracleNumber = CSharp.DECIMAL;
            CSharpTypeUsedForOracleDate = CSharp.DATE_TIME;
            CSharpTypeUsedForOracleTimeStamp = CSharp.DATE_TIME;
            CSharpTypeUsedForOracleIntervalDayToSecond = CSharp.TIME_SPAN;
            CSharpTypeUsedForOracleBlob = CSharp.BYTE_ARRAY;
            CSharpTypeUsedForOracleClob = CSharp.STRING;
            CSharpTypeUsedForOracleBFile = CSharp.BYTE_ARRAY;

            IsDeployResources = IsGenerateBaseAdapter = IsGenerateBaseEntities = true;
        }

        // schema and connection
        public String OracleHome { get; set; }
        public String DatabaseInstance { get; set; }
        public String Schema { get; set; }
        public String Filter { get; set; }
        public String UserLogin { get; set; }
        public String Password { get; set; }

        // .NET/C# version
        public CSharpVersion CSharpVersion { get; set; }
        [XmlIgnore]
        public Boolean IsCSharp30 { get { return CSharpVersion == CSharpVersion.ThreeZero; } }
        [XmlIgnore]
        public Boolean IsCSharp40 { get { return CSharpVersion == CSharpVersion.FourZero; } }

        // namespaces
        public String NamespaceBase { get; set; }
        public String NamespacePackage { get; set; }
        public String NamespaceObjectType { get; set; } // must have internally in order to generate "using" for other entity types
        public String NamespaceTable { get; set; }
        public String NamespaceView { get; set; }
        public String NamespaceDataContract { get; set; }
        public String NamespaceSchema { get; set; } // de facto full namespace for schema (includes filter, if any)

        // ancestor class names
        public string AncestorClassNamePackage { get; set; }
        public string AncestorClassNamePackageRecord { get; set; }
        public string AncestorClassNameObjectType { get; set; }
        public string AncestorClassNameTable { get; set; }
        public string AncestorClassNameView { get; set; }

        // code to generate
        public String OutputPath { get; set; }
        public Boolean IsGeneratePackage { get; set; }
        public Boolean IsGenerateObjectType { get; set; }
        public Boolean IsGenerateTable { get; set; }
        public Boolean IsGenerateView { get; set; }

        public Boolean IsDataContractPackageRecord { get; set; }
        public Boolean IsDataContractObjectType { get; set; }
        public Boolean IsDataContractTable { get; set; }
        public Boolean IsDataContractView { get; set; }

        public Boolean IsXmlElementPackageRecord { get; set; }
        public Boolean IsXmlElementObjectType { get; set; }
        public Boolean IsXmlElementTable { get; set; }
        public Boolean IsXmlElementView { get; set; }

        public Boolean IsSerializablePackageRecord { get; set; }
        public Boolean IsSerializableObjectType { get; set; }
        public Boolean IsSerializableTable { get; set; }
        public Boolean IsSerializableView { get; set; }

        public Boolean IsPartialPackage { get; set; }
        public Boolean IsPartialPackageRecord { get; set; }
        public Boolean IsPartialObjectType { get; set; }
        public Boolean IsPartialTable { get; set; }
        public Boolean IsPartialView { get; set; }

        public Boolean IsIncludeFilterPrefixInNaming { get; set; }

        // sizing
        public Int16 MaxAssocArraySize { get; set; }
        public Int16 MaxReturnAndOutArgStringSize { get; set; }

        // type mapping
        public String CSharpTypeUsedForOracleInteger { get; set; }
        public String CSharpTypeUsedForOracleNumber { get; set; }
        public String CSharpTypeUsedForOracleDate { get; set; }
        public String CSharpTypeUsedForOracleTimeStamp { get; set; }
        public String CSharpTypeUsedForOracleIntervalDayToSecond { get; set; }
        public String CSharpTypeUsedForOracleBlob { get; set; }
        public String CSharpTypeUsedForOracleClob { get; set; }
        public String CSharpTypeUsedForOracleBFile { get; set; }    // pending implementation
        public Boolean IsConvertOracleNumberToIntegerIfColumnNameIsId { get; set; }

        // advanced options
        public Boolean IsDuplicatePackageRecordOriginatingOutsideFilterAndSchema { get; set; }
        public Boolean IsExcludeObjectsNamesWithSpecificChars { get; set; }
        public char[] ObjectNameCharsToExclude;// = new char[] { '$', '#' };
        public Boolean IsGenerateDynamicMappingMethodForTypedCursor { get; set; }
        public Boolean IsUseAutoImplementedProperties { get; set; }
        public String LocalVariableNameSuffix { get; set; }

        // miscellaneous
        public Boolean IsDeployResources { get; set; }  // will overwrite existing file
        public Boolean IsGenerateBaseAdapter { get; set; }  // will not overwrite existing file
        public Boolean IsGenerateBaseEntities { get; set; } // will not overwrite existing file
    }
}