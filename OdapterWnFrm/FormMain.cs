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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using Odapter;

namespace OdapterWnFrm {
    public partial class FormMain : Form {
        private TnsNamesReader tnsNamesReader = new TnsNamesReader();

        public FormMain() {
            this.Text = Generator.GetAppNameVersionLabel();

            InitializeComponent();
            InitMessageConsole();
            BindComboBoxes();

            if (cmbSettingsFile.Items.Count > 1) {
                // automatically select 1st config file and params will be loaded into form
                cmbSettingsFile.SelectedIndex = 1;
            } else {
                // set form with default parameters
                Parameter.Instance.RestoreDefaults();
                SetFromParameters();
            }

            SetEnabledDisabled();
            AcceptButton = this.BtnStart;
        }

#region Messaging
        private void InitMessageConsole() {
            ListViewMessage.Clear() ;
            ListViewMessage.Refresh();

            ColumnHeader columnHeader = new ColumnHeader();
            columnHeader.Text = @"Status";
            columnHeader.Width = ListViewMessage.Width - 30;
            this.ListViewMessage.Columns.AddRange(new ColumnHeader[] { columnHeader });
            // this.ListViewMessage.Colum .ColumnHeadersDefaultCellStyle.Font = new Font(this.dataGridView1.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold);
        }

        public void DisplayMessage(string txt) {
            ListViewItem itm = new ListViewItem();
            itm.Text = txt;
            itm.ToolTipText = txt;
            this.ListViewMessage.Items.Add(itm);
            this.ListViewMessage.EnsureVisible(ListViewMessage.Items.Count - 1);
            ListViewMessage.ShowItemToolTips = true;
            ListViewMessage.Refresh();
        }

        public Action<string> DisplayMessageMethod;
#endregion

#region Utility
        /// <summary>
        /// find any control recursively given an id
        /// </summary>
        /// <param name="root"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private Control FindControl(Control root, string name) {
            if (root == null) throw new ArgumentNullException("root");
            foreach (Control child in root.Controls) {
                if (child.Name == name) return child;
                Control found = FindControl(child, name);
                if (found != null) return found;
            }
            return null;
        }
#endregion

#region Validations
        private bool ValidateRequiredFields() {
            bool missingRequiredFields = false;

            // 1. check required DB Instance
            if (String.IsNullOrWhiteSpace(DbInstance)) {
                DisplayMessage(((Label)FindControl(this, "lbl" + cmbDBInstance.Name.Substring(3))).Text.Replace("*", "").Trim() + " is required.");
                missingRequiredFields = true;
            }

            // 2. check required text box fields that have respectively named label
            List<TextBox> reqTexBoxes =
                new List<TextBox> { txtSchema, txtLogin, txtPassword, txtOutputPath, txtMaxAssocArraySize, txtMaxReturnArgStringSize, txtBaseNamespace,
                    txtLocalVariableNameSuffix };
            foreach (TextBox tb in reqTexBoxes) {
                if (String.IsNullOrEmpty(tb.Text)) {
                    DisplayMessage(((Label)FindControl(this, "lbl" + tb.Name.Substring(3))).Text.Replace("*", "").Trim() + " is required.");
                    missingRequiredFields = true;
                }
            }

            // 3. check required namespace text box fields that have respective check box
            List<TextBox> reqTexBoxesWithCheckBox =
                new List<TextBox> { txtPackageNamespace, txtObjectTypeNamespace, txtTableNamespace, txtViewNamespace };
            foreach (TextBox tb in reqTexBoxesWithCheckBox) {
                if (String.IsNullOrEmpty(tb.Text)) {
                    CheckBox cb = (CheckBox)FindControl(this, "cbGenerate" + tb.Name.Substring(3, tb.Name.Length - 12));
                    DisplayMessage(cb.Text.TrimEnd(new char[] { '?' }) + " Namespace is required.");
                    missingRequiredFields = true;
                }
            }

            // 4. check required class name text box fields that have respective check box
            reqTexBoxesWithCheckBox = new List<TextBox> { txtBasePackageClass, txtBaseRecordTypeClass, txtBaseObjectTypeClass, txtBaseTableClass, txtBaseViewClass};
            foreach (TextBox tb in reqTexBoxesWithCheckBox) {
                if (String.IsNullOrEmpty(tb.Text)) {
                    CheckBox cb = (CheckBox)FindControl(this, "cbGenerate" + tb.Name.Substring(7, tb.Name.Length - 12));
                    DisplayMessage(cb.Text.TrimEnd(new char[] { '?' }) + " Ancestor Class is required.");
                    missingRequiredFields = true;
                }
            }

            return !missingRequiredFields;
        }
#endregion

        private void GenerateNamespacesAndBaseClassNames() {
            String schema = String.IsNullOrEmpty(txtSchema.Text) ? null : txtSchema.Text;
            String baseNamespace = txtBaseNamespace.Text;
            String filterInName = (cbIncludeFilterPrefixInNaming.Checked && !String.IsNullOrWhiteSpace(txtFilter.Text)) ? txtFilter.Text.Trim() : String.Empty;

            // namespaces
            Parameter.Instance.NamespaceSchema = Generator.GenerateNamespaceSchema(baseNamespace, schema, filterInName); // immediately store because UI has no field
            txtPackageNamespace.Text = Generator.GenerateNamespacePackage(baseNamespace, schema, filterInName);
            txtRecordTypeNamespace.Text = Generator.GenerateNamespacePackage(baseNamespace, schema, filterInName);
            txtObjectTypeNamespace.Text = Generator.GenerateNamespaceObjectType(baseNamespace, schema, filterInName);
            txtTableNamespace.Text = Generator.GenerateNamespaceTable(baseNamespace, schema, filterInName);
            txtViewNamespace.Text = Generator.GenerateNamespaceView(baseNamespace, schema, filterInName);

            // base classes
            txtBasePackageClass.Text = Generator.GenerateBaseAdapterClassName(schema);
            txtBaseRecordTypeClass.Text = Generator.GenerateBaseRecordClassName(schema);
            txtBaseObjectTypeClass.Text = Generator.GenerateBaseObjectTypeClassName(schema);
            txtBaseTableClass.Text = Generator.GenerateBaseTableClassName(schema);
            txtBaseViewClass.Text = Generator.GenerateBaseViewClassName(schema);
        }

#region Enable/Disable
        private void SetEnabledDisabled() {
            SetEnabledDisabledbFilterRelatedFields();
            cmbCSharpTypeUsedForOracleIntervalDayToSecond.Enabled = false; // pending implementation
        }

        private void SetEnabledDisabledbFilterRelatedFields() {
            cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Enabled = cbIncludeFilterPrefixInNaming.Enabled 
                = !String.IsNullOrEmpty(txtFilter.Text);
        }
#endregion

#region Events
        private void cbGeneratePOCOExtension_CheckStateChanged(object sender, EventArgs e) {
            //SetEnabledGenerateExtensionControls(cbGeneratePOCOExtension.Checked);
        }

        private void FormMain_Load(object sender, EventArgs e) {
        }

        private void txtSchema_Leave(object sender, EventArgs e) {
            if (String.IsNullOrEmpty(txtLogin.Text)) txtLogin.Text = txtSchema.Text;
            if (String.IsNullOrEmpty(txtBasePackageClass.Text)) txtBasePackageClass.Text = Generator.GenerateBaseAdapterClassName(txtSchema.Text);
        }

        private void btnSelectPath_Click(object sender, EventArgs e) {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Select the output path.";
            dialog.RootFolder = Environment.SpecialFolder.Desktop;
            if (!String.IsNullOrEmpty(txtOutputPath.Text)) dialog.SelectedPath = txtOutputPath.Text;
            DialogResult result = dialog.ShowDialog(this);
            if (result == DialogResult.OK) txtOutputPath.Text = dialog.SelectedPath;
        }

        private void cmbSettingsFile_SelectedIndexChanged(object sender, EventArgs e) {
            if (cmbSettingsFile.SelectedIndex <= 0) return;
            try {
                Parameter.Instance.LoadFromFile(cmbSettingsFile.Text);
                SetFromParameters();
            } catch (Exception ex) {
                DisplayMessage("Failed to load all settings: " + ex.Message);
            }
        }

        private void btnRestoreDefaults_Click(object sender, EventArgs e) {
            RestoreDefaultParamters();
        }

        private void btnSaveCurrentSettings_Click(object sender, EventArgs e) {
            if (cmbSettingsFile.SelectedIndex <= 0) {
                if (String.IsNullOrEmpty(cmbSettingsFile.Text)) {
                    MessageBox.Show("A .config file must be selected/entered in " + lblSettingsFile.Text, "File name required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            ExtractToParameters();
            Parameter.Instance.SaveToFile(cmbSettingsFile.Text);
            BindSettingsFiles(); // refreshes list to reflect new file, if any
        }

        private void txtDBInstance_TextChanged(object sender, EventArgs e) {
        }

        private void cmbDBInstance_KeyPress(object sender, KeyPressEventArgs e) {
            if (Char.IsLetter(e.KeyChar)) e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void btnStart_Clicked(object sender, EventArgs e) {
            InitMessageConsole();
            if (!ValidateRequiredFields()) return;
            Cursor.Current = Cursors.WaitCursor;
            ExtractToParameters();
            Generator.Run(DisplayMessage);
            Cursor.Current = Cursors.Default;
        }

        private void txtSchema_TextChanged(object sender, EventArgs e) {
            txtLogin.Text = txtSchema.Text;
            GenerateNamespacesAndBaseClassNames();
        }

        private void txtBaseNamespace_TextChanged(object sender, EventArgs e) {
            GenerateNamespacesAndBaseClassNames();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e) {
            GenerateNamespacesAndBaseClassNames();
            SetEnabledDisabledbFilterRelatedFields();
        }

        private void txtPackageNamespace_TextChanged(object sender, EventArgs e) {
            txtRecordTypeNamespace.Text = txtPackageNamespace.Text;
        }

        private void cbGeneratePackage_CheckedChanged(object sender, EventArgs e) {
            cbGenerateRecordType.Checked = txtPackageNamespace.Enabled = txtBasePackageClass.Enabled = txtBaseRecordTypeClass.Enabled = cbGeneratePackage.Checked;
            if (cbGeneratePackage.Checked) {
                cbGenerateBaseAdapterClass.Checked = cbGenerateBaseDtoClasses.Checked = true;
            }
        }

        private void cbPartialPackage_CheckedChanged(object sender, EventArgs e) {
            cbPartialPOCOs.Checked = cbPartialPackageClasses.Checked;
        }

        private void cmbClientHome_SelectedIndexChanged(object sender, EventArgs e) {
            BindTnsNames();
        }

        private void cbPartialPOCOs_CheckedChanged(object sender, EventArgs e) {
            if (cbPartialPOCOs.Checked) cbPartialPackageClasses.Checked = true;
        }

        private void cbGenerateTable_CheckedChanged(object sender, EventArgs e) {
            txtTableNamespace.Enabled = txtBaseTableClass.Enabled = cbGenerateTable.Checked;
            if (cbGenerateTable.Checked) cbGenerateObjectType.Checked = txtObjectTypeNamespace.Enabled = true;
        }

        private void cbGenerateView_CheckedChanged(object sender, EventArgs e) {
            txtViewNamespace.Enabled = txtBaseViewClass.Enabled = cbGenerateView.Checked;
            if (cbGenerateView.Checked) cbGenerateObjectType.Checked = txtObjectTypeNamespace.Enabled = true;
        }

        private void cbGenerateObjectType_CheckedChanged(object sender, EventArgs e) {
            txtObjectTypeNamespace.Enabled = txtBaseObjectTypeClass.Enabled = cbGenerateObjectType.Checked;
        }

        private void txtBaseConnectionClass_TextChanged(object sender, EventArgs e) {
            txtBaseConnectionClassFunction.Text = txtBasePackageClass.Text;
            txtBaseConnectionClassProcedure.Text = txtBasePackageClass.Text;
        }

        private void cbExcludeObjectNamesWithSpecificChars_CheckedChanged(object sender, EventArgs e) {
            txtExcludeChars.Enabled = cbExcludeObjectNamesWithSpecificChars.Checked;
        }

        private void cbXmlElementPackageRecord_CheckedChanged(object sender, EventArgs e) {
        }

        private void cbXmlElementObjectType_CheckedChanged(object sender, EventArgs e) {
        }

        private void cbXmlElementView_CheckedChanged(object sender, EventArgs e) {
        }

        private void cbXmlElementTable_CheckedChanged(object sender, EventArgs e) {
        }
#endregion

#region Binding
        private void BindComboBoxes() {
            BindOracleHome();
            BindOracleToCSharpTypes();
            BindSettingsFiles();    // this triggers the setting of all param data if at least one file exists
            BindCSharpVersion();
        }

        private void BindCSharpVersion() {
            cmbCSharpVersion.DisplayMember = "DisplayDescription";
            cmbCSharpVersion.ValueMember = "Version";
            cmbCSharpVersion.DataSource = Translater.CSharpOptions;
        }

        private void BindSettingsFiles() {
            // retrieve list of all config files from directory sorted alphabetically
            cmbSettingsFile.Items.Clear();
            cmbSettingsFile.Items.Add("");  // empty item at top
            foreach (String fn in Parameter.Instance.ConfigFileNames) cmbSettingsFile.Items.Add(fn);
            cmbSettingsFile.Text = "";
        }

        private void BindOracleToCSharpTypes() {
            // map combobox to Oracle type
            IDictionary<ComboBox, String> comboBoxOracleTypes = new Dictionary<ComboBox, String>() {
                { cmbCSharpTypeUsedForOracleRefCursor,              Orcl.REF_CURSOR },
                { cmbCSharpTypeUsedForOracleAssociativeArray,       Orcl.ASSOCIATITVE_ARRAY },
                { cmbCSharpTypeUsedForOracleInteger,                Orcl.INTEGER },
                { cmbCSharpTypeUsedForOracleNumber,                 Orcl.NUMBER },
                { cmbCSharpTypeUsedForOracleDate,                   Orcl.DATE },
                { cmbCSharpTypeUsedForOracleTimestamp,              Orcl.TIMESTAMP },
                { cmbCSharpTypeUsedForOracleIntervalDayToSecond,    Orcl.INTERVAL_DAY_TO_SECOND },
                { cmbCSharpTypeUsedForOracleBlob,                   Orcl.BLOB },
                { cmbCSharpTypeUsedForOracleClob,                   Orcl.CLOB }
            };

            // bind each combobox
            foreach (var cmbBoxOracleType in comboBoxOracleTypes) {
                cmbBoxOracleType.Key.ValueMember = @"CSharpType";
                cmbBoxOracleType.Key.DisplayMember = @"DisplayDescription";
                cmbBoxOracleType.Key.DataSource = Translater.CustomTypeTranslationOptions[cmbBoxOracleType.Value];
            };
        }

        private void BindOracleHome() {
            cmbClientHome.DisplayMember = @"Description";
            cmbClientHome.ValueMember = @"Value";
            List<OracleHome> oraHomes = new List<OracleHome>();
            oraHomes = tnsNamesReader.GetOracleHomes();
            oraHomes.Insert(0, new OracleHome("", ""));
            if (oraHomes == null || oraHomes.Count == 0) DisplayMessage("Warning: Client homes not found.");
            //oraHomes.Add(new OracleHome("OraHomeValue", "OraHomeDescription"));
            cmbClientHome.DataSource = oraHomes;
        }

        private void BindTnsNames() {
            if (cmbClientHome.Items.Count > 0) {
                string oraHomeKey = (string)this.cmbClientHome.SelectedValue;
                if (String.IsNullOrWhiteSpace(oraHomeKey)) return;
                try {
                    List<string> tnsNames = tnsNamesReader.LoadTnsNames(oraHomeKey);
                    if (tnsNames.Count > 0)
                        cmbDBInstance.DataSource = tnsNames;
                    else
                        DisplayMessage("Warning: TNSNAMES.ORA not found.");
                } catch {
                    DisplayMessage("Warning: TNSNAMES.ORA parse failed.");
                }
            }
        }

        private void SetFromParameters() {
            cmbClientHome.SelectedValue = Parameter.Instance.OracleHome;
            DbInstance = Parameter.Instance.DatabaseInstance;
            txtSchema.Text = Parameter.Instance.Schema;
            txtFilter.Text = Parameter.Instance.Filter;
            txtLogin.Text = Parameter.Instance.UserLogin;
            txtPassword.Text = Parameter.Instance.Password;

            txtOutputPath.Text = Parameter.Instance.OutputPath;
            cmbCSharpVersion.SelectedValue = Parameter.Instance.CSharpVersion;

            txtBaseNamespace.Text = Parameter.Instance.NamespaceBase;
            txtPackageNamespace.Text = Parameter.Instance.NamespacePackage;
            txtObjectTypeNamespace.Text = Parameter.Instance.NamespaceObjectType;
            txtTableNamespace.Text = Parameter.Instance.NamespaceTable;
            txtViewNamespace.Text = Parameter.Instance.NamespaceView;
            txtDataContractNamespace.Text = Parameter.Instance.NamespaceDataContract;

            txtBasePackageClass.Text = Parameter.Instance.AncestorClassNamePackage;
            txtBaseRecordTypeClass.Text = Parameter.Instance.AncestorClassNamePackageRecord;
            txtBaseObjectTypeClass.Text = Parameter.Instance.AncestorClassNameObjectType;
            txtBaseTableClass.Text = Parameter.Instance.AncestorClassNameTable;
            txtBaseViewClass.Text = Parameter.Instance.AncestorClassNameView;

            cbGeneratePackage.Checked = Parameter.Instance.IsGeneratePackage;
            cbGenerateObjectType.Checked = Parameter.Instance.IsGenerateObjectType;
            cbGenerateTable.Checked = Parameter.Instance.IsGenerateTable;
            cbGenerateView.Checked = Parameter.Instance.IsGenerateView;

            cbDataContractPackageRecord.Checked = Parameter.Instance.IsDataContractPackageRecord;
            cbDataContractObjectType.Checked = Parameter.Instance.IsDataContractObjectType;
            cbDataContractTable.Checked = Parameter.Instance.IsDataContractTable;
            cbDataContractView.Checked = Parameter.Instance.IsDataContractView;

            cbXmlElementPackageRecord.Checked = Parameter.Instance.IsXmlElementPackageRecord;
            cbXmlElementObjectType.Checked = Parameter.Instance.IsXmlElementObjectType;
            cbXmlElementTable.Checked = Parameter.Instance.IsXmlElementTable;
            cbXmlElementView.Checked = Parameter.Instance.IsXmlElementView;

            cbSerializablePOCOs.Checked = Parameter.Instance.IsSerializablePackageRecord;
            cbSerializableObjectTypes.Checked = Parameter.Instance.IsSerializableObjectType;
            cbSerializableTables.Checked = Parameter.Instance.IsSerializableTable;
            cbSerializableViews.Checked = Parameter.Instance.IsSerializableView;

            cbPartialPackageClasses.Checked = Parameter.Instance.IsPartialPackage;
            cbPartialPOCOs.Checked = Parameter.Instance.IsPartialPackage;
            cbPartialObjectTypes.Checked = Parameter.Instance.IsPartialObjectType;
            cbPartialTables.Checked = Parameter.Instance.IsPartialTable;
            cbPartialViews.Checked = Parameter.Instance.IsPartialView;

            cbIncludeFilterPrefixInNaming.Checked = Parameter.Instance.IsIncludeFilterPrefixInNaming;
            txtMaxAssocArraySize.Text = Parameter.Instance.MaxAssocArraySize.ToString();
            txtMaxReturnArgStringSize.Text = Parameter.Instance.MaxReturnAndOutArgStringSize.ToString();
            cbDeployResources.Checked = Parameter.Instance.IsDeployResources;
            cbGenerateBaseAdapterClass.Checked = Parameter.Instance.IsGenerateBaseAdapter;
            cbGenerateBaseDtoClasses.Checked = Parameter.Instance.IsGenerateBaseEntities;

            cmbCSharpTypeUsedForOracleRefCursor.SelectedValue = Parameter.Instance.CSharpTypeUsedForOracleRefCursor;
            cmbCSharpTypeUsedForOracleAssociativeArray.SelectedValue = Parameter.Instance.CSharpTypeUsedForOracleAssociativeArray;
            cmbCSharpTypeUsedForOracleInteger.SelectedValue = Parameter.Instance.CSharpTypeUsedForOracleInteger;
            cmbCSharpTypeUsedForOracleNumber.SelectedValue = Parameter.Instance.CSharpTypeUsedForOracleNumber;
            cmbCSharpTypeUsedForOracleDate.SelectedValue = Parameter.Instance.CSharpTypeUsedForOracleDate;
            cmbCSharpTypeUsedForOracleTimestamp.SelectedValue = Parameter.Instance.CSharpTypeUsedForOracleTimeStamp;
            cmbCSharpTypeUsedForOracleIntervalDayToSecond.SelectedValue = Parameter.Instance.CSharpTypeUsedForOracleIntervalDayToSecond;
            cmbCSharpTypeUsedForOracleBlob.SelectedValue = Parameter.Instance.CSharpTypeUsedForOracleBlob;
            cmbCSharpTypeUsedForOracleClob.SelectedValue = Parameter.Instance.CSharpTypeUsedForOracleClob;
            cbConvertOracleNumberToIntegerIfColumnNameIsId.Checked = Parameter.Instance.IsConvertOracleNumberToIntegerIfColumnNameIsId;

            cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Checked = Parameter.Instance.IsDuplicatePackageRecordOriginatingOutsideFilterAndSchema;
            cbExcludeObjectNamesWithSpecificChars.Checked = Parameter.Instance.IsExcludeObjectsNamesWithSpecificChars;
            txtExcludeChars.Text = Parameter.Instance.ObjectNameCharsToExcludeAsString;
            cbGeneratedDynamicMethodForTypedCursor.Checked = Parameter.Instance.IsGenerateDynamicMappingMethodForTypedCursor;
            cbUseAutoImplementedProperties.Checked = Parameter.Instance.IsUseAutoImplementedProperties;
            txtLocalVariableNameSuffix.Text = Parameter.Instance.LocalVariableNameSuffix ?? "";
        }

        private void ExtractToParameters() {
            Parameter.Instance.OracleHome = cmbClientHome.SelectedValue.ToString();
            Parameter.Instance.DatabaseInstance = DbInstance;
            Parameter.Instance.Schema = txtSchema.Text;
            Parameter.Instance.Filter = txtFilter.Text;
            Parameter.Instance.UserLogin = txtLogin.Text;
            Parameter.Instance.Password = txtPassword.Text;
            Parameter.Instance.OutputPath = txtOutputPath.Text;

            Parameter.Instance.OutputPath = txtOutputPath.Text;
            Parameter.Instance.CSharpVersion = cmbCSharpVersion.SelectedValue.ToString() == CSharpVersion.ThreeZero.ToString() ? CSharpVersion.ThreeZero: CSharpVersion.FourZero;

            Parameter.Instance.NamespaceBase = txtBaseNamespace.Text;
            Parameter.Instance.NamespacePackage = txtPackageNamespace.Text;
            Parameter.Instance.NamespaceObjectType = txtObjectTypeNamespace.Text;
            Parameter.Instance.NamespaceTable = txtTableNamespace.Text;
            Parameter.Instance.NamespaceView = txtViewNamespace.Text;
            Parameter.Instance.NamespaceDataContract = txtDataContractNamespace.Text;

            Parameter.Instance.AncestorClassNamePackage = txtBasePackageClass.Text;
            Parameter.Instance.AncestorClassNamePackageRecord = txtBaseRecordTypeClass.Text;
            Parameter.Instance.AncestorClassNameObjectType = txtBaseObjectTypeClass.Text;
            Parameter.Instance.AncestorClassNameTable = txtBaseTableClass.Text;
            Parameter.Instance.AncestorClassNameView = txtBaseViewClass.Text;

            Parameter.Instance.MaxAssocArraySize = Convert.ToInt16(txtMaxAssocArraySize.Text);
            Parameter.Instance.MaxReturnAndOutArgStringSize = Convert.ToInt16(txtMaxReturnArgStringSize.Text);

            Parameter.Instance.CSharpTypeUsedForOracleRefCursor = cmbCSharpTypeUsedForOracleRefCursor.SelectedValue.ToString();
            Parameter.Instance.CSharpTypeUsedForOracleAssociativeArray = cmbCSharpTypeUsedForOracleAssociativeArray.SelectedValue.ToString();
            Parameter.Instance.CSharpTypeUsedForOracleInteger = cmbCSharpTypeUsedForOracleInteger.SelectedValue.ToString();
            Parameter.Instance.CSharpTypeUsedForOracleNumber = cmbCSharpTypeUsedForOracleNumber.SelectedValue.ToString();
            Parameter.Instance.CSharpTypeUsedForOracleDate = cmbCSharpTypeUsedForOracleDate.SelectedValue.ToString();
            Parameter.Instance.CSharpTypeUsedForOracleTimeStamp = cmbCSharpTypeUsedForOracleTimestamp.SelectedValue.ToString();
            Parameter.Instance.CSharpTypeUsedForOracleIntervalDayToSecond = cmbCSharpTypeUsedForOracleIntervalDayToSecond.SelectedValue.ToString();
            Parameter.Instance.CSharpTypeUsedForOracleBlob = cmbCSharpTypeUsedForOracleBlob.SelectedValue.ToString();
            Parameter.Instance.CSharpTypeUsedForOracleClob = cmbCSharpTypeUsedForOracleClob.SelectedValue.ToString();

            Parameter.Instance.IsConvertOracleNumberToIntegerIfColumnNameIsId = cbConvertOracleNumberToIntegerIfColumnNameIsId.Checked;

            Parameter.Instance.IsGeneratePackage = cbGeneratePackage.Checked;
            Parameter.Instance.IsGenerateObjectType = cbGenerateObjectType.Checked;
            Parameter.Instance.IsGenerateTable = cbGenerateTable.Checked;
            Parameter.Instance.IsGenerateView = cbGenerateView.Checked;

            Parameter.Instance.IsGenerateBaseAdapter = cbGenerateBaseAdapterClass.Checked;
            Parameter.Instance.IsGenerateBaseEntities = cbGenerateBaseDtoClasses.Checked;

            Parameter.Instance.IsDataContractPackageRecord = cbDataContractPackageRecord.Checked;
            Parameter.Instance.IsDataContractObjectType = cbDataContractObjectType.Checked;
            Parameter.Instance.IsDataContractTable = cbDataContractTable.Checked;
            Parameter.Instance.IsDataContractView = cbDataContractView.Checked;

            Parameter.Instance.IsXmlElementPackageRecord = cbXmlElementPackageRecord.Checked;
            Parameter.Instance.IsXmlElementObjectType = cbXmlElementObjectType.Checked;
            Parameter.Instance.IsXmlElementTable = cbXmlElementTable.Checked;
            Parameter.Instance.IsXmlElementView = cbXmlElementView.Checked;

            Parameter.Instance.IsSerializablePackageRecord = cbSerializablePOCOs.Checked;
            Parameter.Instance.IsSerializableObjectType = cbSerializableObjectTypes.Checked;
            Parameter.Instance.IsSerializableTable = cbSerializableTables.Checked;
            Parameter.Instance.IsSerializableView = cbSerializableViews.Checked;

            Parameter.Instance.IsPartialPackage = cbPartialPackageClasses.Checked;
            Parameter.Instance.IsPartialObjectType = cbPartialObjectTypes.Checked;
            Parameter.Instance.IsPartialTable = cbPartialTables.Checked;
            Parameter.Instance.IsPartialView = cbPartialViews.Checked;

            Parameter.Instance.IsIncludeFilterPrefixInNaming = cbIncludeFilterPrefixInNaming.Checked;
            Parameter.Instance.IsDeployResources = cbDeployResources.Checked;
            Parameter.Instance.IsGenerateBaseAdapter = cbGenerateBaseAdapterClass.Checked;
            Parameter.Instance.IsGenerateBaseEntities = cbGenerateBaseDtoClasses.Checked;

            Parameter.Instance.IsDuplicatePackageRecordOriginatingOutsideFilterAndSchema = cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Checked;
            Parameter.Instance.IsExcludeObjectsNamesWithSpecificChars = cbExcludeObjectNamesWithSpecificChars.Checked;
            Parameter.Instance.ObjectNameCharsToExcludeAsString = txtExcludeChars.Text;
            Parameter.Instance.IsGenerateDynamicMappingMethodForTypedCursor = cbGeneratedDynamicMethodForTypedCursor.Checked;
            Parameter.Instance.IsUseAutoImplementedProperties = cbUseAutoImplementedProperties.Checked;
            Parameter.Instance.LocalVariableNameSuffix = txtLocalVariableNameSuffix.Text ?? "";
        }

        private void RestoreDefaultParamters() {
            Parameter.Instance.RestoreDefaults();
            SetFromParameters();
            BindSettingsFiles();
        }
#endregion

        public String DbInstance { 
            get {
                return cmbDBInstance.Items.Count > 0 && cmbDBInstance.SelectedItem != null ? (String)cmbDBInstance.SelectedItem : cmbDBInstance.Text;
            }
            set {
                if (cmbDBInstance.Items.Count > 0) {
                    cmbDBInstance.SelectedItem = value;
                    cmbDBInstance.Text = value;
                } else {
                    cmbDBInstance.Text = value;
                }
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e) {

        }

        private void cbGenerateBaseAdapterClass_CheckedChanged(object sender, EventArgs e) {

        }

        private void cbDeployResources_CheckedChanged(object sender, EventArgs e) {

        }

        private void lblMaxReturnArgStringSize_Click(object sender, EventArgs e) {

        }

        private void cbGeneratedDynamicMethodForTypedCursor_CheckedChanged(object sender, EventArgs e) {

        }

        private void txtLocalVariableNameSuffix_TextChanged(object sender, EventArgs e) {

        }

        private void cbSerializablePOCOs_CheckedChanged(object sender, EventArgs e) {

        }

        private void cbDataContractPackageRecord_CheckedChanged(object sender, EventArgs e) {

        }

        private void cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema_CheckedChanged(object sender, EventArgs e) {

        }
    }
}
