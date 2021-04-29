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
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Text;
using Odapter;
using Odapter.CSharp;
using System.IO;
using System.Reflection;

namespace OdapterWnFrm {
    public partial class FormMain : Form {        
        private readonly TnsNamesReader tnsNamesReader = new TnsNamesReader();
        private readonly Color BackgroundColor = Color.FromArgb(36, 36, 36);
        private readonly Color BackgroundColorInput = Color.Black;
        private readonly Color ForeColorInput = Color.Yellow;
        private readonly Color LabelColor = Color.DeepSkyBlue;

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
            this.BackColor = this.BtnStart.BackColor = this.btnRestoreDefaults.BackColor = this.btnSaveSettings.BackColor = this.lblGenerateStatus.BackColor
                = BackgroundColor;
        }

        #region Messaging
        private void InitMessageConsole() {
            ListViewMessage.Clear();
            ListViewMessage.Refresh();

            ColumnHeader columnHeader = new ColumnHeader {
                Text = String.Empty, Width = ListViewMessage.Width - 32               
                
            };
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
        private bool ValidateFieldValues() {
            bool validationErrorsFound = false;
            int maxAssocArraySize = Convert.ToInt32(txtMaxAssocArraySize.Text);
            if (maxAssocArraySize < 1 || maxAssocArraySize > ushort.MaxValue) {
                DisplayMessage($"Max Size Assoc Array range is 1-{ushort.MaxValue}.");
                validationErrorsFound = true;
            }

            int maxReturnArgStringSize = Convert.ToInt32(txtMaxReturnArgStringSize.Text);
            if (maxReturnArgStringSize < 1 || maxReturnArgStringSize > short.MaxValue) {
                DisplayMessage($"Max Length VARCHAR2 range is 1-{short.MaxValue}.");
                validationErrorsFound = true;
            }

            // 2. check required text box fields that have respectively named label
            var fileNameTextBoxes = new List<TextBox> { txtPackageFileName, txtObjectFileName, txtTableFileName, txtViewFileName, txtBaseAdapterFileName, txtBaseEntityFileName };
            foreach (TextBox tb in fileNameTextBoxes) {
                CheckBox cb = (CheckBox)FindControl(this, "cbGenerate" + tb.Name.Substring(3, tb.Name.Length - 11));
                if (cb.Checked && !tb.Text.EndsWith(@".cs")) {
                    DisplayMessage(@"All generated files must end in "".cs.""");
                    validationErrorsFound = true;
                    break;
                }
            }

            return !validationErrorsFound;
        }
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
                new List<TextBox> { txtPackageNamespace, txtObjectNamespace, txtTableNamespace, txtViewNamespace, txtBaseAdapterNamespace, txtBaseEntityNamespace };
            var labelNamespace = this.lblNamespace.Text;
            foreach (TextBox tb in reqTexBoxesWithCheckBox) {
                CheckBox cb = (CheckBox)FindControl(this, "cbGenerate" + tb.Name.Substring(3, tb.Name.Length - 12));
                if (cb.Checked && String.IsNullOrEmpty(tb.Text)) {
                    DisplayMessage($"{labelNamespace} for {cb.Text.TrimEnd(new char[] { '?' })} is required.");
                    missingRequiredFields = true;
                }
            }

            // 4. check required class name text box fields that have respective check box
            reqTexBoxesWithCheckBox = new List<TextBox> { txtPackageAncestorClass, txtRecordTypeAncestorClass, txtObjectAncestorClass, txtTableAncestorClass, txtViewAncestorClass };
            var labelAncestorClass = this.lblAncestorClass.Text;
            foreach (TextBox tb in reqTexBoxesWithCheckBox) {
                CheckBox cb = (CheckBox)FindControl(this, "cbGenerate" + tb.Name.Substring(3, tb.Name.Length - 16));
                if (cb.Checked && String.IsNullOrEmpty(tb.Text)) {
                    DisplayMessage($"{labelAncestorClass} for {cb.Text.TrimEnd(new char[] { '?' })} is required.");
                    missingRequiredFields = true;
                }
            }

            // 5. check required file names
            var labelText = lblFileName.Text;
            if (cbGeneratePackage.Checked && String.IsNullOrWhiteSpace(txtPackageFileName.Text)) {
                DisplayMessage($"{labelText} for Package Adapter is required.");
                missingRequiredFields = true;
            }
            if (cbGenerateObject.Checked && String.IsNullOrWhiteSpace(txtObjectFileName.Text)) {
                DisplayMessage($"{labelText} for Object DTOs is required.");
                missingRequiredFields = true;
            }
            if (cbGenerateTable.Checked && String.IsNullOrWhiteSpace(txtTableFileName.Text)) {
                DisplayMessage($"{labelText} for Table DTOs is required.");
                missingRequiredFields = true;
            }
            if (cbGenerateView.Checked && String.IsNullOrWhiteSpace(txtViewFileName.Text)) {
                DisplayMessage($"{labelText} for View DTOs is required.");
                missingRequiredFields = true;
            }
            if (cbGenerateBaseAdapter.Checked && String.IsNullOrWhiteSpace(txtBaseAdapterFileName.Text)) {
                DisplayMessage($"{labelText} for Base Adapter is required.");
                missingRequiredFields = true;
            }
            if (cbGenerateBaseEntity.Checked && String.IsNullOrWhiteSpace(txtBaseEntityFileName.Text)) {
                DisplayMessage($"{labelText} for Base DTOs is required.");
                missingRequiredFields = true;
            }

            return !missingRequiredFields;
        }
        #endregion

        private void GenerateNamespacesAndBaseClassNames() {
            String schema = String.IsNullOrEmpty(txtSchema.Text) ? null : txtSchema.Text;
            String baseNamespace = txtBaseNamespace.Text;
            String filterInName = (cbIncludeFilterPrefixInNaming.Checked && !String.IsNullOrWhiteSpace(txtFilter.Text)) ? txtFilter.Text.Trim() : String.Empty;

            // namespaces
            //Parameter.Instance.NamespaceSchema = Generator.GenerateNamespaceSchema(baseNamespace, schema, filterInName); // immediately store because UI has no field
            txtPackageNamespace.Text = Generator.GenerateNamespacePackage(baseNamespace, schema, filterInName);
            txtRecordTypeNamespace.Text = Generator.GenerateNamespacePackage(baseNamespace, schema, filterInName);
            txtObjectNamespace.Text = Generator.GenerateNamespaceObjectType(baseNamespace, schema, filterInName);
            txtTableNamespace.Text = Generator.GenerateNamespaceTable(baseNamespace, schema, filterInName);
            txtViewNamespace.Text = Generator.GenerateNamespaceView(baseNamespace, schema, filterInName);
            txtBaseAdapterNamespace.Text = Generator.GenerateNamespaceSchema(baseNamespace, schema, filterInName);
            txtBaseEntityNamespace.Text = Generator.GenerateNamespaceSchema(baseNamespace, schema, filterInName);

            // base classes
            txtPackageAncestorClass.Text = Generator.GenerateBaseAdapterClassName(schema);
            txtRecordTypeAncestorClass.Text = Generator.GenerateBaseRecordClassName(schema);
            txtObjectAncestorClass.Text = Generator.GenerateBaseObjectTypeClassName(schema);
            txtTableAncestorClass.Text = Generator.GenerateBaseTableClassName(schema);
            txtViewAncestorClass.Text = Generator.GenerateBaseViewClassName(schema);

            // file names
            txtPackageFileName.Text = Generator.GenerateFileNamePackage(schema, filterInName);
            txtObjectFileName.Text = Generator.GenerateFileNameObject(schema, filterInName);
            txtTableFileName.Text = Generator.GenerateFileNameTable(schema, filterInName);
            txtViewFileName.Text = Generator.GenerateFileNameView(schema, filterInName);
            txtBaseAdapterFileName.Text = Generator.GenerateFileNameBaseAdapter(schema, filterInName);
            txtBaseEntityFileName.Text = Generator.GenerateFileNameBaseEntity(schema, filterInName);
        }

        #region Enable/Disable
        private void SetEnabledDisabled() {
            SetEnabledDisabledbFilterRelatedFields();
            cmbCSharpTypeUsedForOracleIntervalDayToSecond.Enabled = false; // pending implementation
        }

        private void SetEnabledDisabledbFilterRelatedFields() {
            cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Enabled = lblDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Enabled 
                = cbIncludeFilterPrefixInNaming.Enabled = lblIncludeFilterPrefixInNaming.Enabled
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
            if (String.IsNullOrEmpty(txtPackageAncestorClass.Text)) txtPackageAncestorClass.Text = Generator.GenerateBaseAdapterClassName(txtSchema.Text);
        }

        /// <summary>
        /// Creates a relative path from one file or folder to another.
        /// </summary>
        /// <param name="fromPath">Contains the directory that defines the start of the relative path.</param>
        /// <param name="toPath">Contains the path that defines the endpoint of the relative path.</param>
        /// <returns>The relative path from the start directory to the end path.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="fromPath"/> or <paramref name="toPath"/> is <c>null</c>.</exception>
        /// <exception cref="UriFormatException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        private string GetRelativePath(string fromPath, string toPath) {
            if (string.IsNullOrEmpty(fromPath)) throw new ArgumentNullException("fromPath");
            if (string.IsNullOrEmpty(toPath)) throw new ArgumentNullException("toPath");

            Uri fromUri = new Uri(AppendDirectorySeparatorChar(fromPath));
            Uri toUri = new Uri(AppendDirectorySeparatorChar(toPath));
            if (fromUri.Scheme != toUri.Scheme) return toPath;

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());
            if (string.Equals(toUri.Scheme, Uri.UriSchemeFile, StringComparison.OrdinalIgnoreCase)) 
                relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);            

            return relativePath;
        }

        private string AppendDirectorySeparatorChar(string path) {
            if (!Path.HasExtension(path) && !path.EndsWith(Path.DirectorySeparatorChar.ToString())) 
                return path + Path.DirectorySeparatorChar; // Append a slash only if the path is a directory and does not have a slash.
            else
                return path;
        }

        private void btnSelectPath_Click(object sender, EventArgs e) {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Select the output path.";
            dialog.RootFolder = Environment.SpecialFolder.Desktop;
            if (!String.IsNullOrEmpty(txtOutputPath.Text)) dialog.SelectedPath = Path.GetFullPath(txtOutputPath.Text);
            if (dialog.ShowDialog(this) == DialogResult.OK) {
                var relativePathSelected = GetRelativePath(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    Path.GetFullPath(dialog.SelectedPath));
                txtOutputPath.Text = relativePathSelected;
            }
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

        private void btnRestoreDefaults_Click(object sender, EventArgs e) => RestoreDefaultParamters();

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

        private void txtDBInstance_TextChanged(object sender, EventArgs e) { }

        private void cmbDBInstance_KeyPress(object sender, KeyPressEventArgs e) { 
            if (Char.IsLetter(e.KeyChar)) e.KeyChar = Char.ToUpper(e.KeyChar); 
        }

        private void btnStart_Clicked(object sender, EventArgs e) {
            InitMessageConsole();
            if (!ValidateRequiredFields() || !ValidateFieldValues()) return;
            Cursor.Current = Cursors.WaitCursor;
            ExtractToParameters();
            Generator.Run(DisplayMessage);
            Cursor.Current = Cursors.Default;
        }

        private void txtSchema_TextChanged(object sender, EventArgs e) {
            txtLogin.Text = txtSchema.Text;
            GenerateNamespacesAndBaseClassNames();
        }

        private void txtBaseNamespace_TextChanged(object sender, EventArgs e) => GenerateNamespacesAndBaseClassNames();

        private void txtFilter_TextChanged(object sender, EventArgs e) {
            GenerateNamespacesAndBaseClassNames();
            SetEnabledDisabledbFilterRelatedFields();
        }

        private void txtPackageNamespace_TextChanged(object sender, EventArgs e) {
            txtRecordTypeNamespace.Text = txtPackageNamespace.Text;
        }

        private void cbGeneratePackage_CheckedChanged(object sender, EventArgs e) {
            // enable/disable
            txtPackageNamespace.Enabled = txtPackageAncestorClass.Enabled = txtPackageFileName.Enabled = cbPartialPackageClasses.Enabled =         
                cbGenerateRecordType.Enabled = txtRecordTypeAncestorClass.Enabled = lblRecordDto.Enabled = cbSerializablePOCOs.Enabled = cbDataContractPackageRecord.Enabled = cbXmlElementPackageRecord.Enabled =
                txtBaseAdapterFileName.Enabled = txtBaseAdapterNamespace.Enabled = //lblBaseAdapter.Enabled =
                // set from:
                cbGeneratePackage.Checked;

            cbGenerateBaseAdapter.Enabled = false;  // always disabled since it must be same as cbGeneratePackage

            // check/uncheck
            cbGenerateRecordType.Checked = cbGenerateBaseAdapter.Checked = cbGeneratePackage.Checked;

            if (cbGeneratePackage.Checked) cbGenerateBaseAdapter.Checked = true;
        }

        private void cbPartialPackage_CheckedChanged(object sender, EventArgs e) {
            cbPartialPOCOs.Checked = cbPartialPackageClasses.Checked;
        }

        private void cmbClientHome_SelectedIndexChanged(object sender, EventArgs e) => BindTnsNames();

        private void cbPartialPOCOs_CheckedChanged(object sender, EventArgs e) {
            if (cbPartialPOCOs.Checked) cbPartialPackageClasses.Checked = true;
        }

        private void cbGenerateTable_CheckedChanged(object sender, EventArgs e) {
            txtTableNamespace.Enabled = txtTableAncestorClass.Enabled = txtTableFileName.Enabled = cbGenerateTable.Checked;
            if (cbGenerateTable.Checked) cbGenerateObject.Checked = txtObjectNamespace.Enabled = txtObjectFileName.Enabled = cbGenerateBaseEntity.Checked = true;
        }

        private void cbGenerateView_CheckedChanged(object sender, EventArgs e) {
            txtViewNamespace.Enabled = txtViewAncestorClass.Enabled = txtViewFileName.Enabled = cbGenerateView.Checked;
            if (cbGenerateView.Checked) cbGenerateObject.Checked = txtObjectNamespace.Enabled = txtObjectFileName.Enabled = cbGenerateBaseEntity.Checked = true;
        }

        private void cbGenerateObjectType_CheckedChanged(object sender, EventArgs e) {
            txtObjectNamespace.Enabled = txtObjectAncestorClass.Enabled = txtObjectFileName.Enabled = cbGenerateObject.Checked;
            if (cbGenerateObject.Checked) cbGenerateBaseEntity.Checked = true;
        }
        private void cbGenerateBaseAdapterClass_CheckedChanged(object sender, EventArgs e) {
            txtBaseAdapterNamespace.Enabled = txtBaseAdapterFileName.Enabled = cbGenerateBaseAdapter.Checked;
        }
        private void cbGenerateBaseEntity_CheckedChanged(object sender, EventArgs e) {
            txtBaseEntityNamespace.Enabled = txtBaseEntityFileName.Enabled = cbGenerateBaseEntity.Checked;
        }
        private void txtBaseConnectionClass_TextChanged(object sender, EventArgs e) {
            txtBaseConnectionClassFunction.Text = txtPackageAncestorClass.Text;
            txtBaseConnectionClassProcedure.Text = txtPackageAncestorClass.Text;
        }

        private void cbExcludeObjectNamesWithSpecificChars_CheckedChanged(object sender, EventArgs e) {
            txtExcludeChars.Enabled = cbExcludeObjectNamesWithSpecificChars.Checked;
        }

        private void cbXmlElementPackageRecord_CheckedChanged(object sender, EventArgs e) { }

        private void cbXmlElementObjectType_CheckedChanged(object sender, EventArgs e) { }

        private void cbXmlElementView_CheckedChanged(object sender, EventArgs e) { }

        private void cbXmlElementTable_CheckedChanged(object sender, EventArgs e) { }
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
            cmbCSharpVersion.DataSource = TranslaterReferenceData.CSharpOptions;
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
                { cmbCSharpTypeUsedForOracleTimestampTZ,            Orcl.TIMESTAMP_WITH_TIME_ZONE },
                { cmbCSharpTypeUsedForOracleTimestampLTZ,           Orcl.TIMESTAMP_WITH_LOCAL_TIME_ZONE },
                { cmbCSharpTypeUsedForOracleIntervalDayToSecond,    Orcl.INTERVAL_DAY_TO_SECOND },
                { cmbCSharpTypeUsedForOracleBlob,                   Orcl.BLOB },
                { cmbCSharpTypeUsedForOracleClob,                   Orcl.CLOB }
            };

            // bind each combobox
            foreach (var cmbBoxOracleType in comboBoxOracleTypes) {
                cmbBoxOracleType.Key.ValueMember = @"CSharpType";
                cmbBoxOracleType.Key.DisplayMember = @"DisplayDescription";
                cmbBoxOracleType.Key.DataSource = TranslaterReferenceData.CustomTypeTranslationOptions[cmbBoxOracleType.Value];
            };
        }

        private void BindOracleHome() {
            cmbClientHome.DisplayMember = @"Description";
            cmbClientHome.ValueMember = @"Value";
            List<OracleHome> oraHomes = new List<OracleHome>();
            oraHomes = tnsNamesReader.GetOracleHomes();
            oraHomes.Insert(0, new OracleHome(String.Empty, String.Empty)); // a valid selection for when no homes are used/found
            cmbClientHome.DataSource = oraHomes;
            if (oraHomes == null || oraHomes.Count == 1)
                DisplayMessage("Warning: Client homes not found.");
            else
                cmbClientHome.SelectedIndex = 1;    // assume the first home found is approriate and default to it
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

        private static string GetFilterValueIfUsedInNaming() => Parameter.Instance.IsIncludeFilterPrefixInNaming ? Parameter.Instance.Filter : String.Empty;

        private void SetFromParameters() {
            DbInstance = Parameter.Instance.DatabaseInstance;
            txtSchema.Text = Parameter.Instance.Schema;
            txtFilter.Text = Parameter.Instance.Filter;
            txtLogin.Text = Parameter.Instance.UserLogin;
            txtPassword.Text = Parameter.Instance.Password;
            cbIsSavePassword.Checked = Parameter.Instance.IsSavePassword;

            txtOutputPath.Text = Parameter.Instance.OutputPath;
            cmbCSharpVersion.SelectedValue = Parameter.Instance.TargetCSharpVersion;

            txtBaseNamespace.Text = Parameter.Instance.NamespaceBase;
            txtPackageNamespace.Text = Parameter.Instance.NamespacePackage;
            txtObjectNamespace.Text = Parameter.Instance.NamespaceObjectType;
            txtTableNamespace.Text = Parameter.Instance.NamespaceTable;
            txtViewNamespace.Text = Parameter.Instance.NamespaceView;
            // set new namespaces for backward compatibility
            txtBaseAdapterNamespace.Text = String.IsNullOrWhiteSpace(Parameter.Instance.NamespaceBaseAdapter)
                ? Generator.GenerateNamespaceSchema(Parameter.Instance.NamespaceBase, Parameter.Instance.Schema, Parameter.Instance.Filter)
                : Parameter.Instance.NamespaceBaseAdapter;
            txtBaseEntityNamespace.Text = String.IsNullOrWhiteSpace(Parameter.Instance.NamespaceBaseEntity)
                ? Generator.GenerateNamespaceSchema(Parameter.Instance.NamespaceBase, Parameter.Instance.Schema, Parameter.Instance.Filter)
                : Parameter.Instance.NamespaceBaseEntity;

            txtPackageAncestorClass.Text = Parameter.Instance.AncestorClassNamePackage;
            txtRecordTypeAncestorClass.Text = Parameter.Instance.AncestorClassNamePackageRecord;
            txtObjectAncestorClass.Text = Parameter.Instance.AncestorClassNameObjectType;
            txtTableAncestorClass.Text = Parameter.Instance.AncestorClassNameTable;
            txtViewAncestorClass.Text = Parameter.Instance.AncestorClassNameView;

            // set file name fields for backward compatibility 
            var filter = GetFilterValueIfUsedInNaming();
            txtPackageFileName.Text = String.IsNullOrWhiteSpace(Parameter.Instance.FileNamePackage) 
                ? Generator.GenerateFileNamePackage(Parameter.Instance.Schema, filter)
                : Parameter.Instance.FileNamePackage;
            txtObjectFileName.Text = String.IsNullOrWhiteSpace(Parameter.Instance.FileNameObject)
                ? Generator.GenerateFileNameObject(Parameter.Instance.Schema, filter)
                : Parameter.Instance.FileNameObject;
            txtTableFileName.Text = String.IsNullOrWhiteSpace(Parameter.Instance.FileNameTable)
                ? Generator.GenerateFileNameTable(Parameter.Instance.Schema, filter)
                : Parameter.Instance.FileNameTable;
            txtViewFileName.Text = String.IsNullOrWhiteSpace(Parameter.Instance.FileNameView)
                ? Generator.GenerateFileNameView(Parameter.Instance.Schema, filter)
                : Parameter.Instance.FileNameView;
            txtBaseAdapterFileName.Text = String.IsNullOrWhiteSpace(Parameter.Instance.FileNameBaseAdapter)
                ? Generator.GenerateFileNameBaseAdapter(Parameter.Instance.Schema, filter)
                : Parameter.Instance.FileNameBaseAdapter;
            txtBaseEntityFileName.Text = String.IsNullOrWhiteSpace(Parameter.Instance.FileNameBaseEntity)
                ? Generator.GenerateFileNameBaseEntity(Parameter.Instance.Schema, filter)
                : Parameter.Instance.FileNameBaseEntity;

            cbGeneratePackage.Checked = Parameter.Instance.IsGeneratePackage;
            cbGenerateRecordType.Checked = Parameter.Instance.IsGenerateRecord;
            cbGenerateObject.Checked = Parameter.Instance.IsGenerateObjectType;
            cbGenerateTable.Checked = Parameter.Instance.IsGenerateTable;
            cbGenerateView.Checked = Parameter.Instance.IsGenerateView;
            cbGenerateBaseAdapter.Checked = Parameter.Instance.IsGenerateBaseAdapter;
            cbGenerateBaseEntity.Checked = Parameter.Instance.IsGenerateBaseEntities;

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

            cmbCSharpTypeUsedForOracleRefCursor.SelectedValue = Parameter.Instance.CSharpTypeUsedForOracleRefCursor;
            cmbCSharpTypeUsedForOracleAssociativeArray.SelectedValue = Parameter.Instance.CSharpTypeUsedForOracleAssociativeArray;
            cmbCSharpTypeUsedForOracleInteger.SelectedValue = Parameter.Instance.CSharpTypeUsedForOracleInteger;
            cmbCSharpTypeUsedForOracleNumber.SelectedValue = Parameter.Instance.CSharpTypeUsedForOracleNumber;
            cmbCSharpTypeUsedForOracleDate.SelectedValue = Parameter.Instance.CSharpTypeUsedForOracleDate;
            cmbCSharpTypeUsedForOracleTimestamp.SelectedValue = Parameter.Instance.CSharpTypeUsedForOracleTimestamp;
            cmbCSharpTypeUsedForOracleTimestampTZ.SelectedValue = Parameter.Instance.CSharpTypeUsedForOracleTimestampTZ;
            cmbCSharpTypeUsedForOracleTimestampLTZ.SelectedValue = Parameter.Instance.CSharpTypeUsedForOracleTimestampLTZ;
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
            txtDataContractNamespace.Text = Parameter.Instance.NamespaceDataContract;
        }

        private void ExtractToParameters() {
            Parameter.Instance.DatabaseInstance = DbInstance;
            Parameter.Instance.Schema = txtSchema.Text;
            Parameter.Instance.Filter = txtFilter.Text;
            Parameter.Instance.UserLogin = txtLogin.Text;
            Parameter.Instance.Password = txtPassword.Text;
            Parameter.Instance.IsSavePassword = cbIsSavePassword.Checked;
            Parameter.Instance.OutputPath = txtOutputPath.Text;

            Parameter.Instance.OutputPath = txtOutputPath.Text;
            Parameter.Instance.TargetCSharpVersion = CSharpVersion.FourZero;

            Parameter.Instance.NamespaceBase = txtBaseNamespace.Text;
            Parameter.Instance.NamespacePackage = txtPackageNamespace.Text;
            Parameter.Instance.NamespaceObjectType = txtObjectNamespace.Text;
            Parameter.Instance.NamespaceTable = txtTableNamespace.Text;
            Parameter.Instance.NamespaceView = txtViewNamespace.Text;
            Parameter.Instance.NamespaceBaseAdapter = txtBaseAdapterNamespace.Text;
            Parameter.Instance.NamespaceBaseEntity = txtBaseEntityNamespace.Text;

            Parameter.Instance.AncestorClassNamePackage = txtPackageAncestorClass.Text;
            Parameter.Instance.AncestorClassNamePackageRecord = txtRecordTypeAncestorClass.Text;
            Parameter.Instance.AncestorClassNameObjectType = txtObjectAncestorClass.Text;
            Parameter.Instance.AncestorClassNameTable = txtTableAncestorClass.Text;
            Parameter.Instance.AncestorClassNameView = txtViewAncestorClass.Text;

            Parameter.Instance.FileNamePackage = txtPackageFileName.Text;
            Parameter.Instance.FileNameObject = txtObjectFileName.Text;
            Parameter.Instance.FileNameTable = txtTableFileName.Text;
            Parameter.Instance.FileNameView = txtViewFileName.Text;
            Parameter.Instance.FileNameBaseAdapter = txtBaseAdapterFileName.Text;
            Parameter.Instance.FileNameBaseEntity = txtBaseEntityFileName.Text;

            Parameter.Instance.MaxAssocArraySize = Convert.ToInt32(txtMaxAssocArraySize.Text);
            Parameter.Instance.MaxReturnAndOutArgStringSize = Convert.ToInt16(txtMaxReturnArgStringSize.Text);

            Parameter.Instance.CSharpTypeUsedForOracleRefCursor = cmbCSharpTypeUsedForOracleRefCursor.SelectedValue.ToString();
            Parameter.Instance.CSharpTypeUsedForOracleAssociativeArray = cmbCSharpTypeUsedForOracleAssociativeArray.SelectedValue.ToString();
            Parameter.Instance.CSharpTypeUsedForOracleInteger = cmbCSharpTypeUsedForOracleInteger.SelectedValue.ToString();
            Parameter.Instance.CSharpTypeUsedForOracleNumber = cmbCSharpTypeUsedForOracleNumber.SelectedValue.ToString();
            Parameter.Instance.CSharpTypeUsedForOracleDate = cmbCSharpTypeUsedForOracleDate.SelectedValue.ToString();
            Parameter.Instance.CSharpTypeUsedForOracleTimestamp = cmbCSharpTypeUsedForOracleTimestamp.SelectedValue.ToString();
            Parameter.Instance.CSharpTypeUsedForOracleTimestampTZ = cmbCSharpTypeUsedForOracleTimestampTZ.SelectedValue.ToString();
            Parameter.Instance.CSharpTypeUsedForOracleTimestampLTZ = cmbCSharpTypeUsedForOracleTimestampLTZ.SelectedValue.ToString();

            Parameter.Instance.CSharpTypeUsedForOracleIntervalDayToSecond = cmbCSharpTypeUsedForOracleIntervalDayToSecond.SelectedValue.ToString();
            Parameter.Instance.CSharpTypeUsedForOracleBlob = cmbCSharpTypeUsedForOracleBlob.SelectedValue.ToString();
            Parameter.Instance.CSharpTypeUsedForOracleClob = cmbCSharpTypeUsedForOracleClob.SelectedValue.ToString();

            Parameter.Instance.IsConvertOracleNumberToIntegerIfColumnNameIsId = cbConvertOracleNumberToIntegerIfColumnNameIsId.Checked;

            Parameter.Instance.IsGeneratePackage = cbGeneratePackage.Checked;
            Parameter.Instance.IsGenerateRecord = cbGenerateRecordType.Checked;
            Parameter.Instance.IsGenerateObjectType = cbGenerateObject.Checked;
            Parameter.Instance.IsGenerateTable = cbGenerateTable.Checked;
            Parameter.Instance.IsGenerateView = cbGenerateView.Checked;
            Parameter.Instance.IsGenerateBaseAdapter = cbGenerateBaseAdapter.Checked;
            Parameter.Instance.IsGenerateBaseEntities = cbGenerateBaseEntity.Checked;

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
            Parameter.Instance.IsGenerateBaseAdapter = cbGenerateBaseAdapter.Checked;
            Parameter.Instance.IsGenerateBaseEntities = cbGenerateBaseEntity.Checked;

            Parameter.Instance.NamespaceDataContract = txtDataContractNamespace.Text;
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

        private void txtPassword_TextChanged(object sender, EventArgs e) { }
        private void cbDeployResources_CheckedChanged(object sender, EventArgs e) { }
        private void lblMaxReturnArgStringSize_Click(object sender, EventArgs e) { }
        private void cbGeneratedDynamicMethodForTypedCursor_CheckedChanged(object sender, EventArgs e) { }
        private void txtLocalVariableNameSuffix_TextChanged(object sender, EventArgs e) { }
        private void cbSerializablePOCOs_CheckedChanged(object sender, EventArgs e) { }
        private void cbDataContractPackageRecord_CheckedChanged(object sender, EventArgs e) { }
        private void cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema_CheckedChanged(object sender, EventArgs e) { }
        private void cbUseAutoImplementedProperties_CheckedChanged(object sender, EventArgs e) { }

        private void cbGenerateRecordType_CheckedChanged(object sender, EventArgs e) {
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Enabled = lblDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Enabled =
                cbGenerateRecordType.Checked;
        }

        private void ListViewMessage_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e) {
            e.Graphics.FillRectangle(new SolidBrush(BackgroundColorInput), e.Bounds);
            //using (var sf = new StringFormat()) {
            //    sf.Alignment = StringAlignment.Center;
            //    using (var headerFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold)) {
            //        e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.DeepSkyBlue, e.Bounds, sf);
            //    }
            //}
            e.DrawText();
        }

        private void ListViewMessage_DrawItem(object sender, DrawListViewItemEventArgs e) {
            e.DrawDefault = true;
        }
    }
}