//------------------------------------------------------------------------------
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

namespace Odapter {
    /// <summary>
    /// Contains all parameter data sent to code generator
    /// </summary>
    [Serializable]
    public sealed class Parameter : INotifyPropertyChanged, IParameter, IParameterTranslation {
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

            IsSavePassword = false;
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
        public string OracleHome { get; set; }

        private string _databaseInstance;
        public string DatabaseInstance {
            get => _databaseInstance;
            set { if (value != _databaseInstance) { _databaseInstance = value; RaisePropertyChanged("DatabaseInstance"); } }
        }

        private string _schema;
        public string Schema {
            get => _schema;
            set { if (value != _schema) { _schema = value; RaisePropertyChanged("Schema"); } }
        }

        private string _filter;
        public string Filter {
            get => _filter;
            set { if (value != _filter) { _filter = value; RaisePropertyChanged("Filter"); } }
        }

        public string UserLogin { get; set; }
        public string Password { get; set; }
        public bool IsSavePassword { get; set; }

        // .NET/C# version
        public CSharpVersion CSharpVersion { get; set; }
        [XmlIgnore]
        public bool IsCSharp30 { get { return CSharpVersion == CSharpVersion.ThreeZero; } }
        [XmlIgnore]
        public bool IsCSharp40 { get { return CSharpVersion == CSharpVersion.FourZero; } }

        // namespaces
        private string _namespaceBase;
        public string NamespaceBase {
            get => _namespaceBase;
            set { if (value != _namespaceBase) { _namespaceBase = value; RaisePropertyChanged("NamespaceBase"); } }
        }

        public string NamespacePackage { get; set; }
        public string NamespaceObjectType { get; set; } // must have internally in order to generate "using" for other entity types
        public string NamespaceTable { get; set; }
        public string NamespaceView { get; set; }
        public string NamespaceDataContract { get; set; }
        public string NamespaceSchema { get; set; } // de facto full namespace for schema (includes filter, if any)

        // ancestor class names
        public string AncestorClassNamePackage { get; set; }
        public string AncestorClassNamePackageRecord { get; set; }
        public string AncestorClassNameObjectType { get; set; }
        public string AncestorClassNameTable { get; set; }
        public string AncestorClassNameView { get; set; }

        // code to generate
        private string _outputPath;
        public string OutputPath {
            get => _outputPath;
            set { if (value != _outputPath) { _outputPath = value; RaisePropertyChanged("OutputPath"); }  } 
        }

        public bool IsGeneratePackage { get; set; }
        public bool IsGenerateObjectType { get; set; }
        public bool IsGenerateTable { get; set; }
        public bool IsGenerateView { get; set; }

        public bool IsDataContractPackageRecord { get; set; }
        public bool IsDataContractObjectType { get; set; }
        public bool IsDataContractTable { get; set; }
        public bool IsDataContractView { get; set; }

        public bool IsXmlElementPackageRecord { get; set; }
        public bool IsXmlElementObjectType { get; set; }
        public bool IsXmlElementTable { get; set; }
        public bool IsXmlElementView { get; set; }

        public bool IsSerializablePackageRecord { get; set; }
        public bool IsSerializableObjectType { get; set; }
        public bool IsSerializableTable { get; set; }
        public bool IsSerializableView { get; set; }

        public bool IsPartialPackage { get; set; }
        public bool IsPartialObjectType { get; set; }
        public bool IsPartialTable { get; set; }
        public bool IsPartialView { get; set; }

        private bool _isIncludeFilterPrefixInNaming;
        public bool IsIncludeFilterPrefixInNaming {
            get => _isIncludeFilterPrefixInNaming;
            set { _isIncludeFilterPrefixInNaming = value; RaisePropertyChanged("IsIncludeFilterPrefixInNaming"); }
        }

        // sizing
        public short MaxAssocArraySize { get; set; }
        public short MaxReturnAndOutArgStringSize { get; set; }

        // type translation - IParameterTranslation
        public string CSharpTypeUsedForOracleRefCursor { get; set; }
        public string CSharpTypeUsedForOracleAssociativeArray { get; set; }
        public string CSharpTypeUsedForOracleInteger { get; set; }
        public string CSharpTypeUsedForOracleNumber { get; set; }
        public string CSharpTypeUsedForOracleDate { get; set; }
        public string CSharpTypeUsedForOracleTimeStamp { get; set; }
        public string CSharpTypeUsedForOracleIntervalDayToSecond { get; set; }
        public string CSharpTypeUsedForOracleBlob { get; set; }
        public string CSharpTypeUsedForOracleClob { get; set; }
        public string CSharpTypeUsedForOracleBFile { get; set; }    // pending implementation
        public bool IsConvertOracleNumberToIntegerIfColumnNameIsId { get; set; }
        public bool IsUsingSchemaFilter { get => !String.IsNullOrWhiteSpace(Filter); }

        // advanced options
        public bool IsDuplicatePackageRecordOriginatingOutsideFilterAndSchema { get; set; }
        public bool IsExcludeObjectsNamesWithSpecificChars { get; set; }
        public char[] ObjectNameCharsToExclude { get; set; }
        public bool IsGenerateDynamicMappingMethodForTypedCursor { get; set; }
        public bool IsUseAutoImplementedProperties { get; set; }
        public string LocalVariableNameSuffix { get; set; }

        // miscellaneous
        public bool IsDeployResources { get; set; }  // will overwrite existing file
        public bool IsGenerateBaseAdapter { get; set; }  // will not overwrite existing file
        public bool IsGenerateBaseEntities { get; set; } // will not overwrite existing file
        [XmlIgnore]
        public IList<string> ConfigFileNames { get { return GetLocalConfigFileNames(); }  }

        [XmlIgnore]
        public string ObjectNameCharsToExcludeAsString {
            get => string.Join<Char>("", this.ObjectNameCharsToExclude); 
            set { this.ObjectNameCharsToExclude = value.Trim().Replace(" ", "").ToCharArray(); }
        }
        #endregion Properties

        #region File Methods
        private string GetExecutablePath() {
            return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }

        private IList<string> GetLocalConfigFileNames() {
            List<string> files = (new DirectoryInfo(GetExecutablePath()))
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
            string passwordHold = null;
            if (!IsSavePassword) {
                passwordHold = Password;
                Password = null;
            }
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(Parameter));
            Stream fs = new FileStream(GetExecutablePath() + @"\" + fileName, FileMode.Create);
            XmlTextWriter xtw = new XmlTextWriter(fs, Encoding.UTF8);
            xtw.Formatting = Formatting.Indented;
            xtw.WriteStartDocument(true);
            xs.Serialize(xtw, Parameter.Instance);
            xtw.Flush();
            xtw.Close();
            if (!IsSavePassword) Password = passwordHold;
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