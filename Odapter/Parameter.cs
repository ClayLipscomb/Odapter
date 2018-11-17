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
using System.Xml;
using System.Reflection;
using System.ComponentModel;
using System.Diagnostics;

namespace Odapter {
    /// <summary>
    /// Contains all parameter data sent to code generator
    /// </summary>
    [Serializable]
    public sealed class Parameter : INotifyPropertyChanged {
        private Parameter() { RestoreDefaults(); }
        private static Parameter _instance = new Parameter();
        public static Parameter Instance { get { return _instance; } }

        #region INotifyPropertyChanged Interface
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void RaisePropertyChanged(string propertyName) {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public void RestoreDefaults() {
            OracleHome = DatabaseInstance = Filter = Schema = UserLogin = Password =  OutputPath = String.Empty;

            IsGeneratePackage = true;
            IsGenerateObjectType = IsGenerateTable = IsGenerateView = false;
            IsPartialPackage = IsPartialObjectType = IsPartialTable = IsPartialView = false;
            IsSerializablePackageRecord = IsSerializableObjectType = IsSerializableTable = IsSerializableView = false;
            IsXmlElementPackageRecord = IsXmlElementObjectType = IsXmlElementTable = IsXmlElementView = false;
            IsDataContractPackageRecord = IsDataContractObjectType = IsDataContractTable = IsDataContractView = false;
            IsIncludeFilterPrefixInNaming = true;

            NamespaceBase = "Schema";
            NamespacePackage = NamespaceObjectType = NamespaceTable = NamespaceView = String.Empty;
            NamespaceSchema = String.Empty;
            NamespaceDataContract = String.Empty;

            AncestorClassNamePackage = AncestorClassNamePackageRecord = AncestorClassNameObjectType = AncestorClassNameTable = AncestorClassNameView = String.Empty;

            MaxAssocArraySize = 1000;
            MaxReturnAndOutArgStringSize = 32767;
            CSharpVersion = CSharpVersion.FourZero;
            IsDuplicatePackageRecordOriginatingOutsideFilterAndSchema = true;
            IsExcludeObjectsNamesWithSpecificChars = true;
            ObjectNameCharsToExclude = new char[] { '$', '#' };
            LocalVariableNameSuffix = "__";
            IsGenerateDynamicMappingMethodForTypedCursor = false;
            IsConvertOracleNumberToIntegerIfColumnNameIsId = true;
            IsUseAutoImplementedProperties = true;

            CSharpTypeUsedForOracleRefCursor = CSharp.ILIST_OF_T;
            CSharpTypeUsedForOracleAssociativeArray = CSharp.ILIST_OF_T;
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

        #region Properties
        // schema and connection
        public String OracleHome { get; set; }

        private String _databaseInstance;
        public String DatabaseInstance {
            get => _databaseInstance;
            set { if (value != _databaseInstance) { _databaseInstance = value; RaisePropertyChanged("DatabaseInstance"); } }
        }

        private String _schema;
        public String Schema {
            get => _schema;
            set { if (value != _schema) { _schema = value; RaisePropertyChanged("Schema"); } }
        }

        private String _filter;
        public String Filter {
            get => _filter;
            set { if (value != _filter) { _filter = value; RaisePropertyChanged("Filter"); } }
        }

        public String UserLogin { get; set; }
        public String Password { get; set; }

        // .NET/C# version
        public CSharpVersion CSharpVersion { get; set; }
        [XmlIgnore]
        public Boolean IsCSharp30 { get { return CSharpVersion == CSharpVersion.ThreeZero; } }
        [XmlIgnore]
        public Boolean IsCSharp40 { get { return CSharpVersion == CSharpVersion.FourZero; } }

        // namespaces
        private String _namespaceBase;
        public String NamespaceBase {
            get => _namespaceBase;
            set { if (value != _namespaceBase) { _namespaceBase = value; RaisePropertyChanged("NamespaceBase"); } }
        }

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
        private String _outputPath;
        public String OutputPath {
            get => _outputPath;
            set { if (value != _outputPath) { _outputPath = value; RaisePropertyChanged("OutputPath"); }  } 
        }

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
        public Boolean IsPartialObjectType { get; set; }
        public Boolean IsPartialTable { get; set; }
        public Boolean IsPartialView { get; set; }

        private Boolean _isIncludeFilterPrefixInNaming;
        public Boolean IsIncludeFilterPrefixInNaming {
            get => _isIncludeFilterPrefixInNaming;
            set { _isIncludeFilterPrefixInNaming = value; RaisePropertyChanged("IsIncludeFilterPrefixInNaming"); }
        }

        // sizing
        public Int16 MaxAssocArraySize { get; set; }
        public Int16 MaxReturnAndOutArgStringSize { get; set; }

        // type mapping
        public String CSharpTypeUsedForOracleRefCursor { get; set; }
        public String CSharpTypeUsedForOracleAssociativeArray { get; set; }
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
        public char[] ObjectNameCharsToExclude { get; set; }
        public Boolean IsGenerateDynamicMappingMethodForTypedCursor { get; set; }
        public Boolean IsUseAutoImplementedProperties { get; set; }
        public String LocalVariableNameSuffix { get; set; }

        // miscellaneous
        public Boolean IsDeployResources { get; set; }  // will overwrite existing file
        public Boolean IsGenerateBaseAdapter { get; set; }  // will not overwrite existing file
        public Boolean IsGenerateBaseEntities { get; set; } // will not overwrite existing file
        [XmlIgnore]
        public List<String> ConfigFileNames { get { return GetLocalConfigFileNames(); }  }

        [XmlIgnore]
        public String ObjectNameCharsToExcludeAsString {
            get => String.Join<Char>("", this.ObjectNameCharsToExclude); 
            set { this.ObjectNameCharsToExclude = value.Trim().Replace(" ", "").ToCharArray(); }
        }
        #endregion Properties

        #region File Methods
        private String GetExecutablePath() {
            return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }

        private List<String> GetLocalConfigFileNames() {
            List<String> files = (new DirectoryInfo(GetExecutablePath()))
                .GetFiles(@"*.config", SearchOption.TopDirectoryOnly)
                .Where(n => !n.Name.EndsWith(@"exe.config", true, CultureInfo.CurrentCulture))
                .OrderBy(f => f.Name)
                .Select(f => f.Name).ToList();
            return files;
        }

        /// <summary>
        /// Save all params to config file
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveToFile(string fileName) {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(Parameter));
            Stream fs = new FileStream(GetExecutablePath() + @"\" + fileName, FileMode.Create);
            XmlTextWriter xtw = new XmlTextWriter(fs, Encoding.UTF8);
            xtw.Formatting = Formatting.Indented;
            xtw.WriteStartDocument(true);
            xs.Serialize(xtw, Parameter.Instance);
            xtw.Flush();
            xtw.Close();
        }

        /// <summary>
        /// Load all params from config file
        /// </summary>
        /// <param name="fileName"></param>
        public void LoadFromFile(string fileName) {
            if (String.IsNullOrWhiteSpace(fileName)) return;
            StreamReader reader = new StreamReader(GetExecutablePath() + @"\" + fileName);
            try {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(Parameter));
                _instance = (Parameter)xs.Deserialize(reader);
            } catch {
                throw;
            } finally {
                reader.Close();
            }
        }
        #endregion File Methods
    }
}