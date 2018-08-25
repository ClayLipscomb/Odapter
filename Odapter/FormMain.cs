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
using System.Threading;
using System.Linq;
using System.Globalization;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace Odapter {
    public partial class FormMain : Form {
        private TnsNamesReader tnsNamesReader = new TnsNamesReader();

        public FormMain() {

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

            this.Text = Generator.APPLICATION_NAME + " " + fvi.ProductVersion;
            InitializeComponent();
            InitMessageConsole();
            BindAllComboBoxes();

            if (cmbSettingsFile.Items.Count > 1) {
                // automatically select 1st config file and params will be loaded into form
                cmbSettingsFile.SelectedIndex = 1;
            } else {
                // set form with default parameters
                Parameter.Instance.RestoreDefaults();
                SetFromParameters();
            }

            SetEnabledDisabled();
        }

#region Messaging
        private void InitMessageConsole() {
            ListViewMessage.Clear() ;
            ListViewMessage.Refresh();

            ColumnHeader columnHeader = new ColumnHeader();
            columnHeader.Text = "Status";
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

        private void Run() {
            // load all source data from schema
            if (cbGeneratePackage.Checked || cbGenerateObjectType.Checked || cbGenerateTable.Checked || cbGenerateView.Checked) {

                ExtractToParameters();

                // Set these options first since Loader does some translation. In the future, we need to modify Loader to do no translation (if possible).
                Translater.CSharpTypeUsedForOracleInteger = Parameter.Instance.CSharpTypeUsedForOracleInteger;
                Translater.CSharpTypeUsedForOracleNumber = Parameter.Instance.CSharpTypeUsedForOracleNumber;
                Translater.CSharpTypeUsedForOracleDate = Parameter.Instance.CSharpTypeUsedForOracleDate;
                Translater.CSharpTypeUsedForOracleTimeStamp = Parameter.Instance.CSharpTypeUsedForOracleTimeStamp;
                Translater.CSharpTypeUsedForOracleIntervalDayToSecond = Parameter.Instance.CSharpTypeUsedForOracleIntervalDayToSecond;
                Translater.ConvertOracleNumberToIntegerIfColumnNameIsId = Parameter.Instance.IsConvertOracleNumberToIntegerIfColumnNameIsId;
                Translater.ObjectTypeNamespace = Parameter.Instance.NamespaceObjectType;

                // retrieve necessary data from schema
                Loader loader = new Loader(DisplayMessage);
                try {
                    DisplayMessage(DbInstance + " " + txtSchema.Text + (String.IsNullOrEmpty(txtFilter.Text) ? "" : " " + txtFilter.Text + "*") + " generation:");
                    loader.Load();
                } catch (Exception e) {
                    DisplayMessage(e.Message);
                    return;
                } finally {
                }

                // generate code
                Generator generator = new Generator(loader.Schema, txtOutputPath.Text, DisplayMessage,
                    DbInstance, txtLogin.Text, txtPassword.Text, txtBaseNamespace.Text,
                    txtObjectTypeNamespace.Text);

                generator.CSharpVersion = Parameter.Instance.CSharpVersion;

                ////////////////////////
                // generate base classes
                if (cbGenerateBaseAdapterClass.Checked) // base class used by a package, proc wrapper class and function wrapper class
                    generator.WriteBasePackageClass(txtBasePackageClass.Text);
                if (cbGenerateBaseDtoClasses.Checked) // base class used by all record types in a package
                    generator.WriteBaseEntityClasses(txtBaseRecordTypeClass.Text);

                //////////////////////////////////
                // generate schema-derived classes
                if (cbGeneratePackage.Checked)
                    generator.WritePackageClasses(loader.Packages, txtPackageNamespace.Text, txtBasePackageClass.Text, cbPartialPackageClasses.Checked,
                        loader.PacakgeRecordTypes, txtBaseRecordTypeClass.Text, cbSerializablePOCOs.Checked, cbPartialPackageClasses.Checked);
                if (cbGenerateObjectType.Checked)
                    generator.WriteObjectTypeClasses(loader.ObjectTypes, txtObjectTypeNamespace.Text, Generator.GenerateBaseObjectTypeClassName(txtSchema.Text), cbSerializableObjectTypes.Checked, cbPartialObjectTypes.Checked);
                if (cbGenerateTable.Checked)
                    generator.WriteTableClasses(loader.Tables, txtTableNamespace.Text, Generator.GenerateBaseTableClassName(txtSchema.Text), cbSerializableTables.Checked, cbPartialTables.Checked);
                if (cbGenerateView.Checked)
                    generator.WriteViewClasses(loader.Views, txtViewNamespace.Text, Generator.GenerateBaseViewClassName(txtSchema.Text), cbSerializableViews.Checked, cbPartialViews.Checked);

                generator.DeployUtilityClasses(cbDeployResources.Checked);
                DisplayMessage("Generation completed.");
            } else {
                DisplayMessage("No 'Code To Generate' options have been selected.");
            }
        }

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
            //return;
            if (cmbSettingsFile.SelectedIndex <= 0) return;
            LoadSettingsFromFile(cmbSettingsFile.Text);
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
            SaveSettings(cmbSettingsFile.Text);
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
            Run();
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
            cbGenerateRecordType.Checked = txtPackageNamespace.Enabled = txtBaseRecordTypeClass.Enabled = cbGeneratePackage.Checked;
            if (cbGeneratePackage.Checked) {
                txtBasePackageClass.Enabled = cbGenerateBaseAdapterClass.Checked = cbGenerateBaseDtoClasses.Checked = true;
            }
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
        private void BindAllComboBoxes() {
            BindOracleHome();
            BindOracleToCSharpTypes();
            BindSettingsFiles();
            BindCSharpVersion();
        }

        private void BindCSharpVersion() {
            cmbCSharpVersion.DisplayMember = "Text";
            cmbCSharpVersion.ValueMember = "Value";
            cmbCSharpVersion.DataSource = new[] { 
                new { Value = CSharpVersion.ThreeZero,   Text = "3.0" }, 
                new { Value = CSharpVersion.FourZero,    Text = "4.0 +" }
            };
        }

        private void BindSettingsFiles() {
            // retrieve list of all config files from directory sorted alphabetically
            List<FileInfo> files = (new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
                .GetFiles("*.config", SearchOption.TopDirectoryOnly)
                .Where(n => !n.Name.EndsWith("exe.config", true, CultureInfo.CurrentCulture))
                .OrderBy(f => f.Name).ToList();
            cmbSettingsFile.Items.Clear();
            cmbSettingsFile.Items.Add("");
            foreach (FileInfo fi in files) cmbSettingsFile.Items.Add(fi.Name);
            cmbSettingsFile.Text = "";
        }

        private void BindOracleToCSharpTypes() {
            // INTEGER
            cmbCSharpTypeUsedForOracleInteger.DisplayMember = "Text";
            cmbCSharpTypeUsedForOracleInteger.ValueMember = "Value";
            cmbCSharpTypeUsedForOracleInteger.DataSource = new[] { 
                new { Value = CSharp.INT32,          Text = CSharp.INT32 + " (9 digit limit, not recommended)" }, 
                new { Value = CSharp.INT64,          Text = CSharp.INT64 + " (18 digit limit, usually safe)" }, 
                new { Value = CSharp.DECIMAL,        Text = CSharp.DECIMAL + " (28 digit limit)"  }, 
                new { Value = CSharp.ORACLE_DECIMAL, Text = CSharp.ORACLE_DECIMAL + " (ODP.NET safe type)" }
            };

            // NUMBER
            cmbCSharpTypeUsedForOracleNumber.DisplayMember = "Text";
            cmbCSharpTypeUsedForOracleNumber.ValueMember = "Value";
            cmbCSharpTypeUsedForOracleNumber.DataSource = new[] { 
                new { Value = CSharp.DECIMAL,           Text = CSharp.DECIMAL + " (28 dig limit, auto rounding)"  },
                new { Value = CSharp.ORACLE_DECIMAL,    Text = CSharp.ORACLE_DECIMAL + " (ODP.NET safe type)" }//, 
                //new { Value = CSharp.STRING,            Text = CSharp.STRING + " (unlimited, non-numeric type)" }
            };

            // DATE
            cmbCSharpTypeUsedForOracleDate.DisplayMember = "Text";
            cmbCSharpTypeUsedForOracleDate.ValueMember = "Value";
            cmbCSharpTypeUsedForOracleDate.DataSource = new[] { 
                new { Value = CSharp.DATE_TIME,     Text = CSharp.DATE_TIME + " (no BC)" }, 
                new { Value = CSharp.ORACLE_DATE,   Text = CSharp.ORACLE_DATE + " (ODP.NET safe type)" }
            };

            // TIMESTAMP
            cmbCSharpTypeUsedForOracleTimestamp.DisplayMember = "Text";
            cmbCSharpTypeUsedForOracleTimestamp.ValueMember = "Value";
            cmbCSharpTypeUsedForOracleTimestamp.DataSource = new[] { 
                new { Value = CSharp.DATE_TIME,         Text = CSharp.DATE_TIME + " (e-7 max, no BC, no time zone)" },
                new { Value = CSharp.ORACLE_TIMESTAMP,  Text = CSharp.ORACLE_TIMESTAMP + " (ODP.NET safe type)" }
            };

            // INTERVAL DAY TO SECOND
            cmbCSharpTypeUsedForOracleIntervalDayToSecond.DisplayMember = "Text";
            cmbCSharpTypeUsedForOracleIntervalDayToSecond.ValueMember = "Value";
            cmbCSharpTypeUsedForOracleIntervalDayToSecond.DataSource = new[] { 
                new { Value = CSharp.TIME_SPAN,             Text = CSharp.TIME_SPAN + " (e-7 max)" },
                new { Value = CSharp.ORACLE_INTERVAL_DS,    Text = CSharp.ORACLE_INTERVAL_DS + " (ODP.NET safe type)" }
            };
        }

        private void BindOracleHome() {
            cmbClientHome.DisplayMember = "Description";
            cmbClientHome.ValueMember = "Value";
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
                List<string> tnsNames = tnsNamesReader.LoadTnsNames(oraHomeKey);
                if (tnsNames.Count > 0)
                    cmbDBInstance.DataSource = tnsNames;
                else
                    DisplayMessage("Warning: TNSNAMES.ORA not found.");
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
            cbPartialPOCOs.Checked = Parameter.Instance.IsPartialPackageRecord;
            cbPartialObjectTypes.Checked = Parameter.Instance.IsPartialObjectType;
            cbPartialTables.Checked = Parameter.Instance.IsPartialTable;
            cbPartialViews.Checked = Parameter.Instance.IsPartialView;

            cbIncludeFilterPrefixInNaming.Checked = Parameter.Instance.IsIncludeFilterPrefixInNaming;
            txtMaxAssocArraySize.Text = Parameter.Instance.MaxAssocArraySize.ToString();
            txtMaxReturnArgStringSize.Text = Parameter.Instance.MaxReturnAndOutArgStringSize.ToString();
            cbDeployResources.Checked = Parameter.Instance.IsDeployResources;
            cbGenerateBaseAdapterClass.Checked = Parameter.Instance.IsGenerateBaseAdapter;
            cbGenerateBaseDtoClasses.Checked = Parameter.Instance.IsGenerateBaseEntities;

            cmbCSharpTypeUsedForOracleInteger.SelectedValue = Parameter.Instance.CSharpTypeUsedForOracleInteger;
            cmbCSharpTypeUsedForOracleNumber.SelectedValue = Parameter.Instance.CSharpTypeUsedForOracleNumber;
            cmbCSharpTypeUsedForOracleDate.SelectedValue = Parameter.Instance.CSharpTypeUsedForOracleDate;
            cmbCSharpTypeUsedForOracleTimestamp.SelectedValue = Parameter.Instance.CSharpTypeUsedForOracleTimeStamp;
            cmbCSharpTypeUsedForOracleIntervalDayToSecond.SelectedValue = Parameter.Instance.CSharpTypeUsedForOracleIntervalDayToSecond;
            cbConvertOracleNumberToIntegerIfColumnNameIsId.Checked = Parameter.Instance.IsConvertOracleNumberToIntegerIfColumnNameIsId;

            cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Checked = Parameter.Instance.IsDuplicatePackageRecordOriginatingOutsideFilterAndSchema;
            cbExcludeObjectNamesWithSpecificChars.Checked = Parameter.Instance.IsExcludeObjectsNamesWithSpecificChars;
            txtExcludeChars.Text = String.Join<Char>("", Parameter.Instance.ObjectNameCharsToExclude);
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
            Parameter.Instance.CSharpVersion =cmbCSharpVersion.SelectedValue.ToString() == CSharpVersion.ThreeZero.ToString() ? CSharpVersion.ThreeZero: CSharpVersion.FourZero;

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

            Parameter.Instance.CSharpTypeUsedForOracleInteger = cmbCSharpTypeUsedForOracleInteger.SelectedValue.ToString();// GetCSharpTypeUsedForOracleInteger();
            Parameter.Instance.CSharpTypeUsedForOracleNumber = cmbCSharpTypeUsedForOracleNumber.SelectedValue.ToString();// CSharp.DECIMAL;
            Parameter.Instance.CSharpTypeUsedForOracleDate = cmbCSharpTypeUsedForOracleDate.SelectedValue.ToString();// CSharp.DATE_TIME;
            Parameter.Instance.CSharpTypeUsedForOracleTimeStamp = cmbCSharpTypeUsedForOracleTimestamp.SelectedValue.ToString(); // CSharp.DATE_TIME;
            Parameter.Instance.CSharpTypeUsedForOracleIntervalDayToSecond = cmbCSharpTypeUsedForOracleIntervalDayToSecond.SelectedValue.ToString(); // CSharp.TIME_SPAN;
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
            Parameter.Instance.IsPartialPackageRecord = cbPartialPOCOs.Checked;
            Parameter.Instance.IsPartialObjectType = cbPartialObjectTypes.Checked;
            Parameter.Instance.IsPartialTable = cbPartialTables.Checked;
            Parameter.Instance.IsPartialView = cbPartialViews.Checked;

            Parameter.Instance.IsIncludeFilterPrefixInNaming = cbIncludeFilterPrefixInNaming.Checked;
            Parameter.Instance.IsDeployResources = cbDeployResources.Checked;
            Parameter.Instance.IsGenerateBaseAdapter = cbGenerateBaseAdapterClass.Checked;
            Parameter.Instance.IsGenerateBaseEntities = cbGenerateBaseDtoClasses.Checked;

            Parameter.Instance.IsDuplicatePackageRecordOriginatingOutsideFilterAndSchema = cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Checked;
            Parameter.Instance.IsExcludeObjectsNamesWithSpecificChars = cbExcludeObjectNamesWithSpecificChars.Checked;
            Parameter.Instance.ObjectNameCharsToExclude = txtExcludeChars.Text.Trim().Replace(" ", "").ToCharArray();
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

#region Settings
        private String GetExecutablePath() {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return path;
        }

        private void SaveSettings(string fileName) {
            ExtractToParameters();
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(Parameter));
            Stream fs = new FileStream(GetExecutablePath() + @"\" + fileName, FileMode.Create);
            XmlTextWriter xtw = new XmlTextWriter(fs, Encoding.UTF8);
            xtw.Formatting = Formatting.Indented;
            xtw.WriteStartDocument(true);
            xs.Serialize(xtw, Parameter.Instance);
            xtw.Flush();
            xtw.Close();
            BindSettingsFiles();
        }

        private void LoadSettingsFromFile(string fileName) {
            try {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(Parameter));
                StreamReader reader = new StreamReader(GetExecutablePath() + @"\" + fileName);
                Parameter.Instance = (Parameter)xs.Deserialize(reader);
                reader.Close();
                SetFromParameters();
            } catch (Exception ex) {
                DisplayMessage("Failed to load settings: " + ex.Message);
            }
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
