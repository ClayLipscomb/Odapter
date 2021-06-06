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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.Xml.Serialization;
using System.Xml;
using System.Reflection;
using System.ComponentModel;
using CS = Odapter.CSharp;
using CSL = Odapter.CSharp.Logic.Api;

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
        private void RaisePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

        #region TypeTarget defaults
        private static readonly CS.TypeCollection TypeTargetForOracleRefCursorDefault = CS.TypeCollection.IList;
        private static readonly CS.TypeCollection TypeTargetForOracleAssociativeArrayDefault = CS.TypeCollection.IList;
        private static readonly CS.TypeValue TypeTargetForOracleDateDefault = CS.TypeValue.DateTime;
        private static readonly CS.TypeValue TypeTargetForOracleNumberDefault = CS.TypeValue.Decimal;
        private static readonly CS.TypeValue TypeTargetForOracleIntegerDefault = CS.TypeValue.Int64;
        private static readonly CS.TypeValue TypeTargetForOracleTimestampDefault = CS.TypeValue.DateTime;
        private static readonly CS.TypeValue TypeTargetForOracleTimestampTZDefault = CS.TypeValue.DateTimeOffset;
        private static readonly CS.TypeValue TypeTargetForOracleTimestampLTZDefault = CS.TypeValue.DateTime;
        private static readonly CS.TypeReference TypeTargetForOracleClobDefault = CS.TypeReference.String;
        private static readonly CS.ITypeTargetable TypeTargetForOracleBlobDefault = CSL.TypeArrayOf(CS.TypeValue.Byte);
        private static readonly CS.ITypeTargetable TypeTargetForOracleBfileDefault = CSL.TypeArrayOf(CS.TypeValue.Byte);
        #endregion

        public void RestoreDefaults() {
            DatabaseInstance = Filter = Schema = UserLogin = Password =  OutputPath = String.Empty;

            IsSavePassword = false;
            IsGeneratePackage = true;
            IsGenerateObjectType = IsGenerateTable = IsGenerateView = IsGenerateBaseEntities = false;
            IsPartialPackage = IsPartialObjectType = IsPartialTable = IsPartialView = false;
            IsSerializableObjectType = IsSerializableTable = IsSerializableView = false;
            IsXmlElementObjectType = IsXmlElementTable = IsXmlElementView = false;
            IsDataContractObjectType = IsDataContractTable = IsDataContractView = false;
            IsIncludeFilterPrefixInNaming = true;

            NamespaceBase = "Schema";
            NamespacePackage = NamespaceObjectType = NamespaceTable = NamespaceView = NamespaceBaseAdapter = NamespaceBaseEntity = String.Empty;
            NamespaceDataContract = String.Empty;

            AncestorClassNamePackage = AncestorClassNameObjectType = AncestorClassNameTable = AncestorClassNameView = String.Empty;
            FileNamePackage = FileNameObject = FileNameTable = FileNameView = FileNameBaseAdapter = FileNameBaseEntity = String.Empty;

            MaxAssocArraySize = UInt16.MaxValue;
            MaxReturnAndOutArgStringSize = Int16.MaxValue;
            TargetDtoInterfaceCategoryRecord = CS.DtoInterfaceCategory.MutableSet;
            TargetCSharpVersion = CS.CSharpVersion.FourZero;
            IsDuplicatePackageRecordOriginatingOutsideFilterAndSchema = true;
            IsExcludeObjectsNamesWithSpecificChars = true;
            ObjectNameCharsToExclude = new char[] { '$', '#' };
            LocalVariableNameSuffix = "__";
            IsGenerateDynamicMappingMethodForTypedCursor = false;
            IsConvertOracleNumberToIntegerIfColumnNameIsId = true;
            IsUseAutoImplementedProperties = true;

            TypeTargetForOracleRefCursor = TypeTargetForOracleRefCursorDefault;
            TypeTargetForOracleAssociativeArray = TypeTargetForOracleAssociativeArrayDefault;
            TypeTargetForOracleInteger = TypeTargetForOracleIntegerDefault;
            TypeTargetForOracleNumber = TypeTargetForOracleNumberDefault;
            TypeTargetForOracleDate = TypeTargetForOracleDateDefault;
            TypeTargetForOracleTimestamp = TypeTargetForOracleTimestampDefault;
            TypeTargetForOracleTimestampTZ = TypeTargetForOracleTimestampTZDefault;
            TypeTargetForOracleTimestampLTZ = TypeTargetForOracleTimestampLTZDefault;
            CSharpTypeUsedForOracleIntervalDayToSecond = CS.TypeValue.TimeSpan.Code;
            TypeTargetForOracleBlob = TypeTargetForOracleBlobDefault;
            TypeTargetForOracleClob = TypeTargetForOracleClobDefault;
            TypeTargetForOracleBfile = TypeTargetForOracleBfileDefault;

            IsDeployResources = IsGenerateBaseAdapter = true;
        }

        #region Properties
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
            set { if (value != _filter) { _filter = (value ?? String.Empty).Trim().ToUpper(); RaisePropertyChanged("Filter"); } }
        }

        public string UserLogin { get; set; }
        public string Password { get; set; }
        public bool IsSavePassword { get; set; }

        [XmlIgnore]
        public CS.DtoInterfaceCategory TargetDtoInterfaceCategoryRecord { get; set; }
        public string DtoInterfaceCategoryRecord {
            get => TargetDtoInterfaceCategoryRecord.ToString();
            set => TargetDtoInterfaceCategoryRecord = CSL.DtoInterfaceCategoryOfStringWithDefault(value, CS.DtoInterfaceCategory.MutableSet);
        }

        // .NET/C# version
        [XmlIgnore]
        public CS.CSharpVersion TargetCSharpVersion { get; set; }
        public string CSharpVersion {
            get => TargetCSharpVersion.ToString();
            set => TargetCSharpVersion = CSL.CSharpVersionOfStringWithDefault(value, CS.CSharpVersion.FourZero);
        }
        [XmlIgnore]
        public bool IsCSharp90 { get => TargetCSharpVersion.Equals(CS.CSharpVersion.NineZero); }
        [XmlIgnore]
        public bool IsRecordDtoInterfaceImmutable { get => TargetDtoInterfaceCategoryRecord.Equals(CS.DtoInterfaceCategory.ImmutableGetInit); }

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
        public string NamespaceBaseAdapter { get; set; }
        public string NamespaceBaseEntity { get; set; }
        public string NamespaceDataContract { get; set; }

        // ancestor class names
        public string AncestorClassNamePackage { get; set; }
        public string AncestorClassNameObjectType { get; set; }
        public string AncestorClassNameTable { get; set; }
        public string AncestorClassNameView { get; set; }

        // file names
        public string FileNamePackage { get; set; }
        public string FileNameObject { get; set; }
        public string FileNameTable { get; set; }
        public string FileNameView { get; set; }
        public string FileNameBaseAdapter { get; set; }
        public string FileNameBaseEntity { get; set; }

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
        public bool IsGenerateBaseAdapter { get; set; } 
        public bool IsGenerateBaseEntities { get; set; }

        public bool IsDataContractObjectType { get; set; }
        public bool IsDataContractTable { get; set; }
        public bool IsDataContractView { get; set; }

        public bool IsXmlElementObjectType { get; set; }
        public bool IsXmlElementTable { get; set; }
        public bool IsXmlElementView { get; set; }

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
        public int MaxAssocArraySize { get; set; }
        public short MaxReturnAndOutArgStringSize { get; set; }

        // type translation - IParameterTranslation
        [XmlIgnore]
        public CS.TypeCollection TypeTargetForOracleRefCursor { get; set; }
        public string CSharpTypeUsedForOracleRefCursor {
            get => TypeTargetForOracleRefCursor.Code;
            set => TypeTargetForOracleRefCursor = CSL.FromCodeTypeCollectionWithDefault(value, TypeTargetForOracleRefCursorDefault);
        }
        [XmlIgnore]
        public CS.TypeCollection TypeTargetForOracleAssociativeArray { get; set; }
        public string CSharpTypeUsedForOracleAssociativeArray { 
            get => TypeTargetForOracleAssociativeArray.Code;
            set => TypeTargetForOracleAssociativeArray = CSL.FromCodeTypeCollectionWithDefault(value, TypeTargetForOracleAssociativeArrayDefault);
        }
        [XmlIgnore]
        public CS.TypeValue TypeTargetForOracleInteger { get; set; }
        public string CSharpTypeUsedForOracleInteger { 
            get => TypeTargetForOracleInteger.Code; 
            set => TypeTargetForOracleInteger = CSL.FromCodeTypeValueWithDefault(value, TypeTargetForOracleIntegerDefault); 
        }
        [XmlIgnore]
        public CS.TypeValue TypeTargetForOracleNumber { get; set; }
        public string CSharpTypeUsedForOracleNumber {
            get => TypeTargetForOracleNumber.Code;
            set => TypeTargetForOracleNumber = CSL.FromCodeTypeValueWithDefault(value, TypeTargetForOracleNumberDefault);
        }
        [XmlIgnore]
        public CS.TypeValue TypeTargetForOracleDate { get; set; }
        public string CSharpTypeUsedForOracleDate { 
            get => TypeTargetForOracleDate.Code; 
            set => TypeTargetForOracleDate = CSL.FromCodeTypeValueWithDefault(value, TypeTargetForOracleDateDefault); 
        }
        [XmlIgnore]
        public CS.TypeValue TypeTargetForOracleTimestamp { get; set; }
        public string CSharpTypeUsedForOracleTimestamp {
            get => TypeTargetForOracleTimestamp.Code;
            set => TypeTargetForOracleTimestamp = CSL.FromCodeTypeValueWithDefault(value, TypeTargetForOracleTimestampDefault);
        }
        [XmlIgnore]
        public CS.TypeValue TypeTargetForOracleTimestampTZ { get; set; }
        public string CSharpTypeUsedForOracleTimestampTZ {
            get => TypeTargetForOracleTimestampTZ.Code;
            set => TypeTargetForOracleTimestampTZ = CSL.FromCodeTypeValueWithDefault(value, TypeTargetForOracleTimestampTZDefault);
        }
        [XmlIgnore]
        public CS.TypeValue TypeTargetForOracleTimestampLTZ { get; set; }
        public string CSharpTypeUsedForOracleTimestampLTZ {
            get => TypeTargetForOracleTimestampLTZ.Code;
            set => TypeTargetForOracleTimestampLTZ = CSL.FromCodeTypeValueWithDefault(value, TypeTargetForOracleTimestampLTZDefault);
        }
        public string CSharpTypeUsedForOracleIntervalDayToSecond { get; set; }
        [XmlIgnore]
        public CS.ITypeTargetable TypeTargetForOracleBlob { get; set; }
        public string CSharpTypeUsedForOracleBlob {
            get => TypeTargetForOracleBlob.Code;
            set => TypeTargetForOracleBlob = CSL.FromCodeTypeTargetableWithDefault(value, TypeTargetForOracleBlobDefault);
        }
        [XmlIgnore]
        public CS.TypeReference TypeTargetForOracleClob { get; set; }
        public string CSharpTypeUsedForOracleClob {
            get => TypeTargetForOracleClob.Code;
            set => TypeTargetForOracleClob = CSL.FromCodeTypeReferenceWithDefault(value, TypeTargetForOracleClobDefault);
        }
        [XmlIgnore]
        public CS.ITypeTargetable TypeTargetForOracleBfile { get; set; }
        [XmlIgnore]
        public string CSharpTypeUsedForOracleBfile { // pending implementation
            get => TypeTargetForOracleBfile.Code;
            set => TypeTargetForOracleBfile = CSL.FromCodeTypeTargetableWithDefault(value, TypeTargetForOracleBfileDefault);
        }
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

                // remove <T> for backward compatibility
                string OF_T = @"<T>";
                _instance.CSharpTypeUsedForOracleRefCursor = _instance.CSharpTypeUsedForOracleRefCursor.Replace(OF_T, String.Empty);
                _instance.CSharpTypeUsedForOracleAssociativeArray = _instance.CSharpTypeUsedForOracleAssociativeArray.Replace(OF_T, String.Empty);
            } catch {
                throw;
            } finally {
                reader.Close();
            }
        }
        #endregion File Methods
    }
}