
namespace OdapterWnFrm {
    partial class FormMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.ListViewMessage = new System.Windows.Forms.ListView();
            this.BtnStart = new System.Windows.Forms.Button();
            this.lblDBInstance = new System.Windows.Forms.Label();
            this.lblSchema = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.lblOutputPath = new System.Windows.Forms.Label();
            this.txtSchema = new System.Windows.Forms.TextBox();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.gbCodeToGenerate = new System.Windows.Forms.GroupBox();
            this.txtRecordFileName = new System.Windows.Forms.TextBox();
            this.txtRecordNamespace = new System.Windows.Forms.TextBox();
            this.lblRecord = new System.Windows.Forms.Label();
            this.lblDtoInterfaceCategory = new System.Windows.Forms.Label();
            this.lblBaseAdapter = new System.Windows.Forms.Label();
            this.lblView = new System.Windows.Forms.Label();
            this.lblPackage = new System.Windows.Forms.Label();
            this.lblTable = new System.Windows.Forms.Label();
            this.lblIncludeFilterPrefixInNaming = new System.Windows.Forms.Label();
            this.lblObject = new System.Windows.Forms.Label();
            this.txtBaseAdapterNamespace = new System.Windows.Forms.TextBox();
            this.txtBaseAdapterFileName = new System.Windows.Forms.TextBox();
            this.txtViewFileName = new System.Windows.Forms.TextBox();
            this.txtTableFileName = new System.Windows.Forms.TextBox();
            this.txtObjectFileName = new System.Windows.Forms.TextBox();
            this.txtPackageFileName = new System.Windows.Forms.TextBox();
            this.lblFileName = new System.Windows.Forms.Label();
            this.txtProcedureNamespace = new System.Windows.Forms.TextBox();
            this.txtFunctionNamespace = new System.Windows.Forms.TextBox();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.lblBaseNamespace = new System.Windows.Forms.Label();
            this.txtBaseNamespace = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtViewNamespace = new System.Windows.Forms.TextBox();
            this.txtTableNamespace = new System.Windows.Forms.TextBox();
            this.txtObjectNamespace = new System.Windows.Forms.TextBox();
            this.txtPackageNamespace = new System.Windows.Forms.TextBox();
            this.lblNamespace = new System.Windows.Forms.Label();
            this.lblPartial = new System.Windows.Forms.Label();
            this.txtPackageAncestorClass = new System.Windows.Forms.TextBox();
            this.lblAncestorClass = new System.Windows.Forms.Label();
            this.lblCSharpVersion = new System.Windows.Forms.Label();
            this.txtMaxReturnArgStringSize = new System.Windows.Forms.TextBox();
            this.lblMaxReturnArgStringSize = new System.Windows.Forms.Label();
            this.txtMaxAssocArraySize = new System.Windows.Forms.TextBox();
            this.lblMaxAssocArraySize = new System.Windows.Forms.Label();
            this.lblFilter = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.gbDatabase = new System.Windows.Forms.GroupBox();
            this.lblSavePassword = new System.Windows.Forms.Label();
            this.lblClientHome = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblCSharpTypeUsedForOracleNumber = new System.Windows.Forms.Label();
            this.lblCSharpTypeUsedForOracleInteger = new System.Windows.Forms.Label();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.lblCSharpTypeUsedForOracleTimestamp = new System.Windows.Forms.Label();
            this.lblCSharpUsedForOracleIntervalDayToSecond = new System.Windows.Forms.Label();
            this.lblCSharpTypeUsedForOracleDate = new System.Windows.Forms.Label();
            this.lblLocalVariableNameSuffix = new System.Windows.Forms.Label();
            this.lblSettingsFile = new System.Windows.Forms.Label();
            this.lblCSharpTypeUsedForOracleBlob = new System.Windows.Forms.Label();
            this.lblCSharpTypeUsedForOracleClob = new System.Windows.Forms.Label();
            this.lblCSharpTypeUsedForOracleRefCursor = new System.Windows.Forms.Label();
            this.lblCSharpTypeUsedForOracleAssociativeArray = new System.Windows.Forms.Label();
            this.lblCSharpTypeUsedForOracleTimestampTZ = new System.Windows.Forms.Label();
            this.lblCSharpTypeUsedForOracleTimestampLTZ = new System.Windows.Forms.Label();
            this.lblDeployResources = new System.Windows.Forms.Label();
            this.lblDuplicatePackageRecordOriginatingOutsideFilterAndSchema = new System.Windows.Forms.Label();
            this.lblExcludeObjectNamesWithSpecificChars = new System.Windows.Forms.Label();
            this.lblGeneratedDynamicMethodForTypedCursor = new System.Windows.Forms.Label();
            this.lblConvertOracleNumberToIntegerIfColumnNameIsId = new System.Windows.Forms.Label();
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.btnRestoreDefaults = new System.Windows.Forms.Button();
            this.gbOracleToCSharpCustomTranslation = new System.Windows.Forms.GroupBox();
            this.gbAdvancedProcOptions = new System.Windows.Forms.GroupBox();
            this.txtLocalVariableNameSuffix = new System.Windows.Forms.TextBox();
            this.txtExcludeChars = new System.Windows.Forms.TextBox();
            this.lblGenerateStatus = new System.Windows.Forms.Label();
            this.cbGeneratedDynamicMethodForTypedCursor = new OdapterWnFrm.Controls.OdatperCheckBox();
            this.cbExcludeObjectNamesWithSpecificChars = new OdapterWnFrm.Controls.OdatperCheckBox();
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema = new OdapterWnFrm.Controls.OdatperCheckBox();
            this.cmbCSharpVersion = new OdapterWnFrm.Controls.OdapterComboBox();
            this.cbDeployResources = new OdapterWnFrm.Controls.OdatperCheckBox();
            this.cbPartialPackageClasses = new OdapterWnFrm.Controls.OdatperCheckBox();
            this.cmbCSharpTypeUsedForOracleTimestampLTZ = new OdapterWnFrm.Controls.OdapterComboBox();
            this.cmbCSharpTypeUsedForOracleTimestampTZ = new OdapterWnFrm.Controls.OdapterComboBox();
            this.cmbCSharpTypeUsedForOracleAssociativeArray = new OdapterWnFrm.Controls.OdapterComboBox();
            this.cmbCSharpTypeUsedForOracleRefCursor = new OdapterWnFrm.Controls.OdapterComboBox();
            this.cmbCSharpTypeUsedForOracleClob = new OdapterWnFrm.Controls.OdapterComboBox();
            this.cmbCSharpTypeUsedForOracleBlob = new OdapterWnFrm.Controls.OdapterComboBox();
            this.cmbCSharpTypeUsedForOracleIntervalDayToSecond = new OdapterWnFrm.Controls.OdapterComboBox();
            this.cmbCSharpTypeUsedForOracleTimestamp = new OdapterWnFrm.Controls.OdapterComboBox();
            this.cmbCSharpTypeUsedForOracleDate = new OdapterWnFrm.Controls.OdapterComboBox();
            this.cmbCSharpTypeUsedForOracleNumber = new OdapterWnFrm.Controls.OdapterComboBox();
            this.cmbCSharpTypeUsedForOracleInteger = new OdapterWnFrm.Controls.OdapterComboBox();
            this.cbConvertOracleNumberToIntegerIfColumnNameIsId = new OdapterWnFrm.Controls.OdatperCheckBox();
            this.cmbSettingsFile = new OdapterWnFrm.Controls.OdapterComboBox();
            this.cbIsSavePassword = new OdapterWnFrm.Controls.OdatperCheckBox();
            this.cmbDBInstance = new OdapterWnFrm.Controls.OdapterComboBox();
            this.cmbClientHome = new OdapterWnFrm.Controls.OdapterComboBox();
            this.cbGenerateRecord = new OdapterWnFrm.Controls.OdatperCheckBox();
            this.cmbDtoInterfaceCategoryView = new OdapterWnFrm.Controls.OdapterComboBox();
            this.cmbDtoInterfaceCategoryTable = new OdapterWnFrm.Controls.OdapterComboBox();
            this.cmbDtoInterfaceCategoryObject = new OdapterWnFrm.Controls.OdapterComboBox();
            this.cmbDtoInterfaceCategoryRecord = new OdapterWnFrm.Controls.OdapterComboBox();
            this.cbIncludeFilterPrefixInNaming = new OdapterWnFrm.Controls.OdatperCheckBox();
            this.cbGenerateBaseAdapter = new OdapterWnFrm.Controls.OdatperCheckBox();
            this.cbGenerateView = new OdapterWnFrm.Controls.OdatperCheckBox();
            this.cbGenerateTable = new OdapterWnFrm.Controls.OdatperCheckBox();
            this.cbGenerateObject = new OdapterWnFrm.Controls.OdatperCheckBox();
            this.cbGeneratePackage = new OdapterWnFrm.Controls.OdatperCheckBox();
            this.gbCodeToGenerate.SuspendLayout();
            this.gbDatabase.SuspendLayout();
            this.gbSettings.SuspendLayout();
            this.gbOracleToCSharpCustomTranslation.SuspendLayout();
            this.gbAdvancedProcOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListViewMessage
            // 
            this.ListViewMessage.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.ListViewMessage.BackColor = System.Drawing.Color.Black;
            this.ListViewMessage.ForeColor = System.Drawing.Color.Yellow;
            this.ListViewMessage.HideSelection = false;
            this.ListViewMessage.Location = new System.Drawing.Point(837, 10);
            this.ListViewMessage.Margin = new System.Windows.Forms.Padding(4);
            this.ListViewMessage.Name = "ListViewMessage";
            this.ListViewMessage.OwnerDraw = true;
            this.ListViewMessage.Size = new System.Drawing.Size(443, 408);
            this.ListViewMessage.TabIndex = 7;
            this.ListViewMessage.UseCompatibleStateImageBehavior = false;
            this.ListViewMessage.View = System.Windows.Forms.View.Details;
            this.ListViewMessage.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.ListViewMessage_DrawColumnHeader);
            this.ListViewMessage.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.ListViewMessage_DrawItem);
            // 
            // BtnStart
            // 
            this.BtnStart.BackColor = System.Drawing.Color.Black;
            this.BtnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnStart.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.BtnStart.Location = new System.Drawing.Point(707, 14);
            this.BtnStart.Margin = new System.Windows.Forms.Padding(4);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(110, 43);
            this.BtnStart.TabIndex = 1;
            this.BtnStart.Text = "Generate Code";
            this.BtnStart.UseVisualStyleBackColor = false;
            this.BtnStart.Click += new System.EventHandler(this.btnStart_Clicked);
            // 
            // lblDBInstance
            // 
            this.lblDBInstance.AutoSize = true;
            this.lblDBInstance.BackColor = System.Drawing.Color.Transparent;
            this.lblDBInstance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBInstance.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblDBInstance.Location = new System.Drawing.Point(393, 21);
            this.lblDBInstance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDBInstance.Name = "lblDBInstance";
            this.lblDBInstance.Size = new System.Drawing.Size(80, 17);
            this.lblDBInstance.TabIndex = 3;
            this.lblDBInstance.Text = "* Instance";
            // 
            // lblSchema
            // 
            this.lblSchema.AutoSize = true;
            this.lblSchema.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSchema.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblSchema.Location = new System.Drawing.Point(41, 50);
            this.lblSchema.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSchema.Name = "lblSchema";
            this.lblSchema.Size = new System.Drawing.Size(76, 17);
            this.lblSchema.TabIndex = 42;
            this.lblSchema.Text = "* Schema";
            this.lblSchema.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblPassword.Location = new System.Drawing.Point(385, 78);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(88, 17);
            this.lblPassword.TabIndex = 56;
            this.lblPassword.Text = "* Password";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.BackColor = System.Drawing.Color.Transparent;
            this.lblLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogin.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblLogin.Location = new System.Drawing.Point(59, 78);
            this.lblLogin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(59, 17);
            this.lblLogin.TabIndex = 55;
            this.lblLogin.Text = "* Login";
            this.lblLogin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOutputPath
            // 
            this.lblOutputPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutputPath.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblOutputPath.Location = new System.Drawing.Point(335, 20);
            this.lblOutputPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOutputPath.Name = "lblOutputPath";
            this.lblOutputPath.Size = new System.Drawing.Size(121, 16);
            this.lblOutputPath.TabIndex = 0;
            this.lblOutputPath.Text = "* Output Path";
            this.lblOutputPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblOutputPath, "The project path in which C# files will be generated.");
            // 
            // txtSchema
            // 
            this.txtSchema.BackColor = System.Drawing.Color.Black;
            this.txtSchema.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSchema.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSchema.ForeColor = System.Drawing.Color.Yellow;
            this.txtSchema.Location = new System.Drawing.Point(123, 46);
            this.txtSchema.Margin = new System.Windows.Forms.Padding(4);
            this.txtSchema.Name = "txtSchema";
            this.txtSchema.Size = new System.Drawing.Size(243, 23);
            this.txtSchema.TabIndex = 3;
            this.txtSchema.TextChanged += new System.EventHandler(this.txtSchema_TextChanged);
            this.txtSchema.Leave += new System.EventHandler(this.txtSchema_Leave);
            // 
            // txtLogin
            // 
            this.txtLogin.BackColor = System.Drawing.Color.Black;
            this.txtLogin.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogin.ForeColor = System.Drawing.Color.Yellow;
            this.txtLogin.Location = new System.Drawing.Point(124, 73);
            this.txtLogin.Margin = new System.Windows.Forms.Padding(4);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(243, 23);
            this.txtLogin.TabIndex = 5;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.Black;
            this.txtPassword.ForeColor = System.Drawing.Color.Yellow;
            this.txtPassword.Location = new System.Drawing.Point(480, 73);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(129, 23);
            this.txtPassword.TabIndex = 6;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.BackColor = System.Drawing.Color.Black;
            this.txtOutputPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutputPath.ForeColor = System.Drawing.Color.Yellow;
            this.txtOutputPath.Location = new System.Drawing.Point(464, 16);
            this.txtOutputPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(285, 23);
            this.txtOutputPath.TabIndex = 1;
            // 
            // gbCodeToGenerate
            // 
            this.gbCodeToGenerate.BackColor = System.Drawing.Color.Transparent;
            this.gbCodeToGenerate.Controls.Add(this.txtRecordFileName);
            this.gbCodeToGenerate.Controls.Add(this.txtRecordNamespace);
            this.gbCodeToGenerate.Controls.Add(this.cbGenerateRecord);
            this.gbCodeToGenerate.Controls.Add(this.lblRecord);
            this.gbCodeToGenerate.Controls.Add(this.cmbDtoInterfaceCategoryView);
            this.gbCodeToGenerate.Controls.Add(this.cmbDtoInterfaceCategoryTable);
            this.gbCodeToGenerate.Controls.Add(this.cmbDtoInterfaceCategoryObject);
            this.gbCodeToGenerate.Controls.Add(this.cmbDtoInterfaceCategoryRecord);
            this.gbCodeToGenerate.Controls.Add(this.lblDtoInterfaceCategory);
            this.gbCodeToGenerate.Controls.Add(this.lblBaseAdapter);
            this.gbCodeToGenerate.Controls.Add(this.lblView);
            this.gbCodeToGenerate.Controls.Add(this.lblPackage);
            this.gbCodeToGenerate.Controls.Add(this.lblTable);
            this.gbCodeToGenerate.Controls.Add(this.lblIncludeFilterPrefixInNaming);
            this.gbCodeToGenerate.Controls.Add(this.lblObject);
            this.gbCodeToGenerate.Controls.Add(this.txtBaseAdapterNamespace);
            this.gbCodeToGenerate.Controls.Add(this.txtBaseAdapterFileName);
            this.gbCodeToGenerate.Controls.Add(this.txtViewFileName);
            this.gbCodeToGenerate.Controls.Add(this.txtTableFileName);
            this.gbCodeToGenerate.Controls.Add(this.txtObjectFileName);
            this.gbCodeToGenerate.Controls.Add(this.txtPackageFileName);
            this.gbCodeToGenerate.Controls.Add(this.lblFileName);
            this.gbCodeToGenerate.Controls.Add(this.txtProcedureNamespace);
            this.gbCodeToGenerate.Controls.Add(this.txtFunctionNamespace);
            this.gbCodeToGenerate.Controls.Add(this.cbIncludeFilterPrefixInNaming);
            this.gbCodeToGenerate.Controls.Add(this.btnSelectPath);
            this.gbCodeToGenerate.Controls.Add(this.txtOutputPath);
            this.gbCodeToGenerate.Controls.Add(this.lblOutputPath);
            this.gbCodeToGenerate.Controls.Add(this.lblBaseNamespace);
            this.gbCodeToGenerate.Controls.Add(this.txtBaseNamespace);
            this.gbCodeToGenerate.Controls.Add(this.label9);
            this.gbCodeToGenerate.Controls.Add(this.cbGenerateBaseAdapter);
            this.gbCodeToGenerate.Controls.Add(this.txtViewNamespace);
            this.gbCodeToGenerate.Controls.Add(this.txtTableNamespace);
            this.gbCodeToGenerate.Controls.Add(this.txtObjectNamespace);
            this.gbCodeToGenerate.Controls.Add(this.txtPackageNamespace);
            this.gbCodeToGenerate.Controls.Add(this.lblNamespace);
            this.gbCodeToGenerate.Controls.Add(this.cbGenerateView);
            this.gbCodeToGenerate.Controls.Add(this.cbGenerateTable);
            this.gbCodeToGenerate.Controls.Add(this.cbGenerateObject);
            this.gbCodeToGenerate.Controls.Add(this.cbGeneratePackage);
            this.gbCodeToGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbCodeToGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbCodeToGenerate.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.gbCodeToGenerate.Location = new System.Drawing.Point(14, 166);
            this.gbCodeToGenerate.Margin = new System.Windows.Forms.Padding(4);
            this.gbCodeToGenerate.Name = "gbCodeToGenerate";
            this.gbCodeToGenerate.Padding = new System.Windows.Forms.Padding(4);
            this.gbCodeToGenerate.Size = new System.Drawing.Size(816, 252);
            this.gbCodeToGenerate.TabIndex = 2;
            this.gbCodeToGenerate.TabStop = false;
            this.gbCodeToGenerate.Text = "Code to Generate";
            // 
            // txtRecordFileName
            // 
            this.txtRecordFileName.BackColor = System.Drawing.Color.Black;
            this.txtRecordFileName.Enabled = false;
            this.txtRecordFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecordFileName.ForeColor = System.Drawing.Color.Yellow;
            this.txtRecordFileName.Location = new System.Drawing.Point(591, 95);
            this.txtRecordFileName.Margin = new System.Windows.Forms.Padding(4);
            this.txtRecordFileName.Name = "txtRecordFileName";
            this.txtRecordFileName.Size = new System.Drawing.Size(213, 23);
            this.txtRecordFileName.TabIndex = 84;
            // 
            // txtRecordNamespace
            // 
            this.txtRecordNamespace.BackColor = System.Drawing.Color.Black;
            this.txtRecordNamespace.Enabled = false;
            this.txtRecordNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecordNamespace.ForeColor = System.Drawing.Color.Yellow;
            this.txtRecordNamespace.Location = new System.Drawing.Point(189, 95);
            this.txtRecordNamespace.Margin = new System.Windows.Forms.Padding(4);
            this.txtRecordNamespace.Name = "txtRecordNamespace";
            this.txtRecordNamespace.Size = new System.Drawing.Size(238, 23);
            this.txtRecordNamespace.TabIndex = 83;
            // 
            // lblRecord
            // 
            this.lblRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecord.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblRecord.Location = new System.Drawing.Point(6, 96);
            this.lblRecord.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Size = new System.Drawing.Size(154, 17);
            this.lblRecord.TabIndex = 81;
            this.lblRecord.Text = "Record Interfaces";
            this.lblRecord.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDtoInterfaceCategory
            // 
            this.lblDtoInterfaceCategory.AutoSize = true;
            this.lblDtoInterfaceCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDtoInterfaceCategory.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblDtoInterfaceCategory.Location = new System.Drawing.Point(445, 48);
            this.lblDtoInterfaceCategory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDtoInterfaceCategory.Name = "lblDtoInterfaceCategory";
            this.lblDtoInterfaceCategory.Size = new System.Drawing.Size(111, 17);
            this.lblDtoInterfaceCategory.TabIndex = 80;
            this.lblDtoInterfaceCategory.Text = "DTO Category";
            this.lblDtoInterfaceCategory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblDtoInterfaceCategory, resources.GetString("lblDtoInterfaceCategory.ToolTip"));
            // 
            // lblBaseAdapter
            // 
            this.lblBaseAdapter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBaseAdapter.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblBaseAdapter.Location = new System.Drawing.Point(0, 122);
            this.lblBaseAdapter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBaseAdapter.Name = "lblBaseAdapter";
            this.lblBaseAdapter.Size = new System.Drawing.Size(160, 17);
            this.lblBaseAdapter.TabIndex = 74;
            this.lblBaseAdapter.Text = "Base Adapter";
            this.lblBaseAdapter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblBaseAdapter, "Ancestor class for all package adapters. Only needs to be deployed once.");
            // 
            // lblView
            // 
            this.lblView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblView.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblView.Location = new System.Drawing.Point(0, 200);
            this.lblView.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblView.Name = "lblView";
            this.lblView.Size = new System.Drawing.Size(160, 17);
            this.lblView.TabIndex = 78;
            this.lblView.Text = "View Interfaces";
            this.lblView.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPackage
            // 
            this.lblPackage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPackage.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblPackage.Location = new System.Drawing.Point(6, 70);
            this.lblPackage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPackage.Name = "lblPackage";
            this.lblPackage.Size = new System.Drawing.Size(154, 17);
            this.lblPackage.TabIndex = 69;
            this.lblPackage.Text = "Package Adapters";
            this.lblPackage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTable
            // 
            this.lblTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTable.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblTable.Location = new System.Drawing.Point(2, 174);
            this.lblTable.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(158, 17);
            this.lblTable.TabIndex = 77;
            this.lblTable.Text = "Table Interfaces";
            this.lblTable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIncludeFilterPrefixInNaming
            // 
            this.lblIncludeFilterPrefixInNaming.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIncludeFilterPrefixInNaming.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblIncludeFilterPrefixInNaming.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblIncludeFilterPrefixInNaming.Location = new System.Drawing.Point(159, 227);
            this.lblIncludeFilterPrefixInNaming.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIncludeFilterPrefixInNaming.Name = "lblIncludeFilterPrefixInNaming";
            this.lblIncludeFilterPrefixInNaming.Size = new System.Drawing.Size(622, 18);
            this.lblIncludeFilterPrefixInNaming.TabIndex = 72;
            this.lblIncludeFilterPrefixInNaming.Text = "Automatically Include Filter Prefix in Namespace and Generated File Naming?";
            this.lblIncludeFilterPrefixInNaming.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblIncludeFilterPrefixInNaming, "Incorporate the Filter Prefix value into namespaces and generated file names. Thi" +
        "s is \r\nuseful in cases where packages of multiple projects exists in the same sc" +
        "hema (e.g., APPS).");
            // 
            // lblObject
            // 
            this.lblObject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObject.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblObject.Location = new System.Drawing.Point(2, 148);
            this.lblObject.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblObject.Name = "lblObject";
            this.lblObject.Size = new System.Drawing.Size(158, 17);
            this.lblObject.TabIndex = 76;
            this.lblObject.Text = "Object Interfaces";
            this.lblObject.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBaseAdapterNamespace
            // 
            this.txtBaseAdapterNamespace.BackColor = System.Drawing.Color.Black;
            this.txtBaseAdapterNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBaseAdapterNamespace.ForeColor = System.Drawing.Color.Yellow;
            this.txtBaseAdapterNamespace.Location = new System.Drawing.Point(189, 121);
            this.txtBaseAdapterNamespace.Margin = new System.Windows.Forms.Padding(4);
            this.txtBaseAdapterNamespace.Name = "txtBaseAdapterNamespace";
            this.txtBaseAdapterNamespace.Size = new System.Drawing.Size(238, 23);
            this.txtBaseAdapterNamespace.TabIndex = 8;
            // 
            // txtBaseAdapterFileName
            // 
            this.txtBaseAdapterFileName.BackColor = System.Drawing.Color.Black;
            this.txtBaseAdapterFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBaseAdapterFileName.ForeColor = System.Drawing.Color.Yellow;
            this.txtBaseAdapterFileName.Location = new System.Drawing.Point(591, 121);
            this.txtBaseAdapterFileName.Margin = new System.Windows.Forms.Padding(4);
            this.txtBaseAdapterFileName.Name = "txtBaseAdapterFileName";
            this.txtBaseAdapterFileName.Size = new System.Drawing.Size(213, 23);
            this.txtBaseAdapterFileName.TabIndex = 9;
            // 
            // txtViewFileName
            // 
            this.txtViewFileName.BackColor = System.Drawing.Color.Black;
            this.txtViewFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViewFileName.ForeColor = System.Drawing.Color.Yellow;
            this.txtViewFileName.Location = new System.Drawing.Point(591, 199);
            this.txtViewFileName.Margin = new System.Windows.Forms.Padding(4);
            this.txtViewFileName.Name = "txtViewFileName";
            this.txtViewFileName.Size = new System.Drawing.Size(213, 23);
            this.txtViewFileName.TabIndex = 21;
            // 
            // txtTableFileName
            // 
            this.txtTableFileName.BackColor = System.Drawing.Color.Black;
            this.txtTableFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTableFileName.ForeColor = System.Drawing.Color.Yellow;
            this.txtTableFileName.Location = new System.Drawing.Point(591, 173);
            this.txtTableFileName.Margin = new System.Windows.Forms.Padding(4);
            this.txtTableFileName.Name = "txtTableFileName";
            this.txtTableFileName.Size = new System.Drawing.Size(213, 23);
            this.txtTableFileName.TabIndex = 17;
            // 
            // txtObjectFileName
            // 
            this.txtObjectFileName.BackColor = System.Drawing.Color.Black;
            this.txtObjectFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObjectFileName.ForeColor = System.Drawing.Color.Yellow;
            this.txtObjectFileName.Location = new System.Drawing.Point(591, 147);
            this.txtObjectFileName.Margin = new System.Windows.Forms.Padding(4);
            this.txtObjectFileName.Name = "txtObjectFileName";
            this.txtObjectFileName.Size = new System.Drawing.Size(213, 23);
            this.txtObjectFileName.TabIndex = 13;
            // 
            // txtPackageFileName
            // 
            this.txtPackageFileName.BackColor = System.Drawing.Color.Black;
            this.txtPackageFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPackageFileName.ForeColor = System.Drawing.Color.Yellow;
            this.txtPackageFileName.Location = new System.Drawing.Point(591, 69);
            this.txtPackageFileName.Margin = new System.Windows.Forms.Padding(4);
            this.txtPackageFileName.Name = "txtPackageFileName";
            this.txtPackageFileName.Size = new System.Drawing.Size(213, 23);
            this.txtPackageFileName.TabIndex = 6;
            this.txtPackageFileName.TextChanged += new System.EventHandler(this.txtPackageFileName_TextChanged);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblFileName.Location = new System.Drawing.Point(610, 49);
            this.lblFileName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(173, 17);
            this.lblFileName.TabIndex = 65;
            this.lblFileName.Text = "* Generated File Name";
            this.lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProcedureNamespace
            // 
            this.txtProcedureNamespace.Enabled = false;
            this.txtProcedureNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProcedureNamespace.ForeColor = System.Drawing.Color.Black;
            this.txtProcedureNamespace.Location = new System.Drawing.Point(209, 551);
            this.txtProcedureNamespace.Margin = new System.Windows.Forms.Padding(4);
            this.txtProcedureNamespace.Name = "txtProcedureNamespace";
            this.txtProcedureNamespace.Size = new System.Drawing.Size(224, 23);
            this.txtProcedureNamespace.TabIndex = 7;
            // 
            // txtFunctionNamespace
            // 
            this.txtFunctionNamespace.Enabled = false;
            this.txtFunctionNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFunctionNamespace.Location = new System.Drawing.Point(209, 577);
            this.txtFunctionNamespace.Margin = new System.Windows.Forms.Padding(4);
            this.txtFunctionNamespace.Name = "txtFunctionNamespace";
            this.txtFunctionNamespace.Size = new System.Drawing.Size(235, 23);
            this.txtFunctionNamespace.TabIndex = 8;
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.BackColor = System.Drawing.Color.Black;
            this.btnSelectPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectPath.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSelectPath.Location = new System.Drawing.Point(752, 16);
            this.btnSelectPath.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(35, 25);
            this.btnSelectPath.TabIndex = 2;
            this.btnSelectPath.Text = "...";
            this.btnSelectPath.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSelectPath.UseVisualStyleBackColor = false;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // lblBaseNamespace
            // 
            this.lblBaseNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBaseNamespace.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblBaseNamespace.Location = new System.Drawing.Point(8, 17);
            this.lblBaseNamespace.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBaseNamespace.Name = "lblBaseNamespace";
            this.lblBaseNamespace.Size = new System.Drawing.Size(169, 20);
            this.lblBaseNamespace.TabIndex = 52;
            this.lblBaseNamespace.Text = "* Base Namespace";
            this.lblBaseNamespace.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBaseNamespace
            // 
            this.txtBaseNamespace.BackColor = System.Drawing.Color.Black;
            this.txtBaseNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBaseNamespace.ForeColor = System.Drawing.Color.Yellow;
            this.txtBaseNamespace.Location = new System.Drawing.Point(183, 17);
            this.txtBaseNamespace.Margin = new System.Windows.Forms.Padding(4);
            this.txtBaseNamespace.Name = "txtBaseNamespace";
            this.txtBaseNamespace.Size = new System.Drawing.Size(141, 23);
            this.txtBaseNamespace.TabIndex = 0;
            this.txtBaseNamespace.TextChanged += new System.EventHandler(this.txtBaseNamespace_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label9.Location = new System.Drawing.Point(149, 48);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 17);
            this.label9.TabIndex = 50;
            this.label9.Text = "Gen?";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.label9, "Generate code.");
            // 
            // txtViewNamespace
            // 
            this.txtViewNamespace.BackColor = System.Drawing.Color.Black;
            this.txtViewNamespace.Enabled = false;
            this.txtViewNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViewNamespace.ForeColor = System.Drawing.Color.Yellow;
            this.txtViewNamespace.Location = new System.Drawing.Point(189, 199);
            this.txtViewNamespace.Margin = new System.Windows.Forms.Padding(4);
            this.txtViewNamespace.Name = "txtViewNamespace";
            this.txtViewNamespace.Size = new System.Drawing.Size(238, 23);
            this.txtViewNamespace.TabIndex = 19;
            // 
            // txtTableNamespace
            // 
            this.txtTableNamespace.BackColor = System.Drawing.Color.Black;
            this.txtTableNamespace.Enabled = false;
            this.txtTableNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTableNamespace.ForeColor = System.Drawing.Color.Yellow;
            this.txtTableNamespace.Location = new System.Drawing.Point(189, 173);
            this.txtTableNamespace.Margin = new System.Windows.Forms.Padding(4);
            this.txtTableNamespace.Name = "txtTableNamespace";
            this.txtTableNamespace.Size = new System.Drawing.Size(238, 23);
            this.txtTableNamespace.TabIndex = 15;
            // 
            // txtObjectNamespace
            // 
            this.txtObjectNamespace.BackColor = System.Drawing.Color.Black;
            this.txtObjectNamespace.Enabled = false;
            this.txtObjectNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObjectNamespace.ForeColor = System.Drawing.Color.Yellow;
            this.txtObjectNamespace.Location = new System.Drawing.Point(189, 147);
            this.txtObjectNamespace.Margin = new System.Windows.Forms.Padding(4);
            this.txtObjectNamespace.Name = "txtObjectNamespace";
            this.txtObjectNamespace.Size = new System.Drawing.Size(238, 23);
            this.txtObjectNamespace.TabIndex = 11;
            // 
            // txtPackageNamespace
            // 
            this.txtPackageNamespace.BackColor = System.Drawing.Color.Black;
            this.txtPackageNamespace.Enabled = false;
            this.txtPackageNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPackageNamespace.ForeColor = System.Drawing.Color.Yellow;
            this.txtPackageNamespace.Location = new System.Drawing.Point(189, 69);
            this.txtPackageNamespace.Margin = new System.Windows.Forms.Padding(4);
            this.txtPackageNamespace.Name = "txtPackageNamespace";
            this.txtPackageNamespace.Size = new System.Drawing.Size(238, 23);
            this.txtPackageNamespace.TabIndex = 4;
            this.txtPackageNamespace.TextChanged += new System.EventHandler(this.txtPackageNamespace_TextChanged);
            // 
            // lblNamespace
            // 
            this.lblNamespace.AutoSize = true;
            this.lblNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNamespace.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblNamespace.Location = new System.Drawing.Point(255, 48);
            this.lblNamespace.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNamespace.Name = "lblNamespace";
            this.lblNamespace.Size = new System.Drawing.Size(103, 17);
            this.lblNamespace.TabIndex = 19;
            this.lblNamespace.Text = "* Namespace";
            this.lblNamespace.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPartial
            // 
            this.lblPartial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartial.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblPartial.Location = new System.Drawing.Point(150, 144);
            this.lblPartial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPartial.Name = "lblPartial";
            this.lblPartial.Size = new System.Drawing.Size(275, 17);
            this.lblPartial.TabIndex = 38;
            this.lblPartial.Text = "Partial Package Adapters?";
            this.lblPartial.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblPartial, "Generate adapter classes as partial?");
            // 
            // txtPackageAncestorClass
            // 
            this.txtPackageAncestorClass.BackColor = System.Drawing.Color.Black;
            this.txtPackageAncestorClass.Enabled = false;
            this.txtPackageAncestorClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPackageAncestorClass.ForeColor = System.Drawing.Color.Yellow;
            this.txtPackageAncestorClass.Location = new System.Drawing.Point(435, 245);
            this.txtPackageAncestorClass.Margin = new System.Windows.Forms.Padding(4);
            this.txtPackageAncestorClass.Name = "txtPackageAncestorClass";
            this.txtPackageAncestorClass.Size = new System.Drawing.Size(192, 23);
            this.txtPackageAncestorClass.TabIndex = 10;
            this.txtPackageAncestorClass.TextChanged += new System.EventHandler(this.txtBaseConnectionClass_TextChanged);
            // 
            // lblAncestorClass
            // 
            this.lblAncestorClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAncestorClass.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblAncestorClass.Location = new System.Drawing.Point(95, 245);
            this.lblAncestorClass.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAncestorClass.Name = "lblAncestorClass";
            this.lblAncestorClass.Size = new System.Drawing.Size(330, 17);
            this.lblAncestorClass.TabIndex = 20;
            this.lblAncestorClass.Text = "* Package Adapter Ancestor Class";
            this.lblAncestorClass.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblAncestorClass, "Ancestor class name of every package adapter class.");
            // 
            // lblCSharpVersion
            // 
            this.lblCSharpVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSharpVersion.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblCSharpVersion.Location = new System.Drawing.Point(183, 17);
            this.lblCSharpVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCSharpVersion.Name = "lblCSharpVersion";
            this.lblCSharpVersion.Size = new System.Drawing.Size(242, 17);
            this.lblCSharpVersion.TabIndex = 6;
            this.lblCSharpVersion.Text = "C# Version Generated";
            this.lblCSharpVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMaxReturnArgStringSize
            // 
            this.txtMaxReturnArgStringSize.BackColor = System.Drawing.Color.Black;
            this.txtMaxReturnArgStringSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxReturnArgStringSize.ForeColor = System.Drawing.Color.Yellow;
            this.txtMaxReturnArgStringSize.Location = new System.Drawing.Point(435, 193);
            this.txtMaxReturnArgStringSize.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaxReturnArgStringSize.Name = "txtMaxReturnArgStringSize";
            this.txtMaxReturnArgStringSize.Size = new System.Drawing.Size(71, 23);
            this.txtMaxReturnArgStringSize.TabIndex = 8;
            this.txtMaxReturnArgStringSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblMaxReturnArgStringSize
            // 
            this.lblMaxReturnArgStringSize.BackColor = System.Drawing.Color.Transparent;
            this.lblMaxReturnArgStringSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxReturnArgStringSize.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblMaxReturnArgStringSize.Location = new System.Drawing.Point(29, 194);
            this.lblMaxReturnArgStringSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaxReturnArgStringSize.Name = "lblMaxReturnArgStringSize";
            this.lblMaxReturnArgStringSize.Size = new System.Drawing.Size(396, 17);
            this.lblMaxReturnArgStringSize.TabIndex = 1;
            this.lblMaxReturnArgStringSize.Text = "* Max Length of VARCHAR2 Returned Argument";
            this.lblMaxReturnArgStringSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblMaxReturnArgStringSize, "Maximum length of VARCHAR2 for OUT, IN/OUT arguments or function return.");
            this.lblMaxReturnArgStringSize.Click += new System.EventHandler(this.lblMaxReturnArgStringSize_Click);
            // 
            // txtMaxAssocArraySize
            // 
            this.txtMaxAssocArraySize.BackColor = System.Drawing.Color.Black;
            this.txtMaxAssocArraySize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxAssocArraySize.ForeColor = System.Drawing.Color.Yellow;
            this.txtMaxAssocArraySize.Location = new System.Drawing.Point(435, 219);
            this.txtMaxAssocArraySize.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaxAssocArraySize.Name = "txtMaxAssocArraySize";
            this.txtMaxAssocArraySize.Size = new System.Drawing.Size(71, 23);
            this.txtMaxAssocArraySize.TabIndex = 9;
            this.txtMaxAssocArraySize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblMaxAssocArraySize
            // 
            this.lblMaxAssocArraySize.BackColor = System.Drawing.Color.Transparent;
            this.lblMaxAssocArraySize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxAssocArraySize.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblMaxAssocArraySize.Location = new System.Drawing.Point(26, 220);
            this.lblMaxAssocArraySize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaxAssocArraySize.Name = "lblMaxAssocArraySize";
            this.lblMaxAssocArraySize.Size = new System.Drawing.Size(399, 17);
            this.lblMaxAssocArraySize.TabIndex = 0;
            this.lblMaxAssocArraySize.Text = "* Max Size of Associative Array Returned Argument";
            this.lblMaxAssocArraySize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblMaxAssocArraySize, "Maximum size of associative array for OUT, IN/OUT arguments or function return.");
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblFilter.Location = new System.Drawing.Point(382, 50);
            this.lblFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(91, 17);
            this.lblFilter.TabIndex = 54;
            this.lblFilter.Text = "Filter Prefix";
            this.lblFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblFilter, "All schema packages, object types, tables, and views not prefixed with this value" +
        " will be excluded from generation.");
            // 
            // txtFilter
            // 
            this.txtFilter.BackColor = System.Drawing.Color.Black;
            this.txtFilter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilter.ForeColor = System.Drawing.Color.Yellow;
            this.txtFilter.Location = new System.Drawing.Point(480, 46);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(4);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(319, 23);
            this.txtFilter.TabIndex = 4;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // gbDatabase
            // 
            this.gbDatabase.BackColor = System.Drawing.Color.Transparent;
            this.gbDatabase.Controls.Add(this.lblSavePassword);
            this.gbDatabase.Controls.Add(this.cbIsSavePassword);
            this.gbDatabase.Controls.Add(this.cmbDBInstance);
            this.gbDatabase.Controls.Add(this.cmbClientHome);
            this.gbDatabase.Controls.Add(this.lblClientHome);
            this.gbDatabase.Controls.Add(this.lblSchema);
            this.gbDatabase.Controls.Add(this.txtSchema);
            this.gbDatabase.Controls.Add(this.txtPassword);
            this.gbDatabase.Controls.Add(this.txtFilter);
            this.gbDatabase.Controls.Add(this.lblFilter);
            this.gbDatabase.Controls.Add(this.txtLogin);
            this.gbDatabase.Controls.Add(this.lblLogin);
            this.gbDatabase.Controls.Add(this.lblPassword);
            this.gbDatabase.Controls.Add(this.lblDBInstance);
            this.gbDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDatabase.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.gbDatabase.Location = new System.Drawing.Point(16, 59);
            this.gbDatabase.Margin = new System.Windows.Forms.Padding(4);
            this.gbDatabase.Name = "gbDatabase";
            this.gbDatabase.Padding = new System.Windows.Forms.Padding(4);
            this.gbDatabase.Size = new System.Drawing.Size(814, 103);
            this.gbDatabase.TabIndex = 1;
            this.gbDatabase.TabStop = false;
            this.gbDatabase.Text = "Oracle Database";
            // 
            // lblSavePassword
            // 
            this.lblSavePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSavePassword.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblSavePassword.Location = new System.Drawing.Point(639, 75);
            this.lblSavePassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSavePassword.Name = "lblSavePassword";
            this.lblSavePassword.Size = new System.Drawing.Size(176, 17);
            this.lblSavePassword.TabIndex = 64;
            this.lblSavePassword.Text = "Save? (unencrypted)";
            this.lblSavePassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.lblSavePassword, "Save unencryped password to settings file?");
            // 
            // lblClientHome
            // 
            this.lblClientHome.AutoSize = true;
            this.lblClientHome.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClientHome.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblClientHome.Location = new System.Drawing.Point(23, 21);
            this.lblClientHome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClientHome.Name = "lblClientHome";
            this.lblClientHome.Size = new System.Drawing.Size(95, 17);
            this.lblClientHome.TabIndex = 41;
            this.lblClientHome.Text = "Client Home";
            this.lblClientHome.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 15000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // lblCSharpTypeUsedForOracleNumber
            // 
            this.lblCSharpTypeUsedForOracleNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSharpTypeUsedForOracleNumber.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblCSharpTypeUsedForOracleNumber.Location = new System.Drawing.Point(16, 102);
            this.lblCSharpTypeUsedForOracleNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCSharpTypeUsedForOracleNumber.Name = "lblCSharpTypeUsedForOracleNumber";
            this.lblCSharpTypeUsedForOracleNumber.Size = new System.Drawing.Size(225, 16);
            this.lblCSharpTypeUsedForOracleNumber.TabIndex = 58;
            this.lblCSharpTypeUsedForOracleNumber.Text = "NUMBER && NUMBER(p,s) ";
            this.lblCSharpTypeUsedForOracleNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblCSharpTypeUsedForOracleNumber, "An Oracle NUMBER or a NUMBER with a non-zero precision and scale.");
            // 
            // lblCSharpTypeUsedForOracleInteger
            // 
            this.lblCSharpTypeUsedForOracleInteger.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSharpTypeUsedForOracleInteger.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblCSharpTypeUsedForOracleInteger.Location = new System.Drawing.Point(16, 74);
            this.lblCSharpTypeUsedForOracleInteger.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCSharpTypeUsedForOracleInteger.Name = "lblCSharpTypeUsedForOracleInteger";
            this.lblCSharpTypeUsedForOracleInteger.Size = new System.Drawing.Size(225, 16);
            this.lblCSharpTypeUsedForOracleInteger.TabIndex = 55;
            this.lblCSharpTypeUsedForOracleInteger.Text = "INTEGER && NUMBER(p>9)";
            this.lblCSharpTypeUsedForOracleInteger.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblCSharpTypeUsedForOracleInteger, "An Oracle INTEGER, INTEGER equivalent, or any NUMBER with a precision > 9 and no " +
        "scale.");
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.BackColor = System.Drawing.Color.Black;
            this.btnSaveSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveSettings.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSaveSettings.Location = new System.Drawing.Point(374, 14);
            this.btnSaveSettings.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(125, 28);
            this.btnSaveSettings.TabIndex = 1;
            this.btnSaveSettings.Text = "Save Current";
            this.toolTip1.SetToolTip(this.btnSaveSettings, "Save current settings to config file in File Source");
            this.btnSaveSettings.UseVisualStyleBackColor = false;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveCurrentSettings_Click);
            // 
            // lblCSharpTypeUsedForOracleTimestamp
            // 
            this.lblCSharpTypeUsedForOracleTimestamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSharpTypeUsedForOracleTimestamp.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblCSharpTypeUsedForOracleTimestamp.Location = new System.Drawing.Point(83, 181);
            this.lblCSharpTypeUsedForOracleTimestamp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCSharpTypeUsedForOracleTimestamp.Name = "lblCSharpTypeUsedForOracleTimestamp";
            this.lblCSharpTypeUsedForOracleTimestamp.Size = new System.Drawing.Size(158, 16);
            this.lblCSharpTypeUsedForOracleTimestamp.TabIndex = 61;
            this.lblCSharpTypeUsedForOracleTimestamp.Text = "TIMESTAMP";
            this.lblCSharpTypeUsedForOracleTimestamp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblCSharpTypeUsedForOracleTimestamp, "An Oracle TIMESTAMP");
            // 
            // lblCSharpUsedForOracleIntervalDayToSecond
            // 
            this.lblCSharpUsedForOracleIntervalDayToSecond.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSharpUsedForOracleIntervalDayToSecond.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblCSharpUsedForOracleIntervalDayToSecond.Location = new System.Drawing.Point(21, 378);
            this.lblCSharpUsedForOracleIntervalDayToSecond.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCSharpUsedForOracleIntervalDayToSecond.Name = "lblCSharpUsedForOracleIntervalDayToSecond";
            this.lblCSharpUsedForOracleIntervalDayToSecond.Size = new System.Drawing.Size(191, 16);
            this.lblCSharpUsedForOracleIntervalDayToSecond.TabIndex = 60;
            this.lblCSharpUsedForOracleIntervalDayToSecond.Text = "INTERVAL DAY TO SEC";
            this.lblCSharpUsedForOracleIntervalDayToSecond.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblCSharpUsedForOracleIntervalDayToSecond, "An Oracle INTERVAL DAY TO SECOND");
            // 
            // lblCSharpTypeUsedForOracleDate
            // 
            this.lblCSharpTypeUsedForOracleDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSharpTypeUsedForOracleDate.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblCSharpTypeUsedForOracleDate.Location = new System.Drawing.Point(70, 154);
            this.lblCSharpTypeUsedForOracleDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCSharpTypeUsedForOracleDate.Name = "lblCSharpTypeUsedForOracleDate";
            this.lblCSharpTypeUsedForOracleDate.Size = new System.Drawing.Size(171, 16);
            this.lblCSharpTypeUsedForOracleDate.TabIndex = 59;
            this.lblCSharpTypeUsedForOracleDate.Text = "DATE";
            this.lblCSharpTypeUsedForOracleDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblCSharpTypeUsedForOracleDate, "An Oracle DATE");
            // 
            // lblLocalVariableNameSuffix
            // 
            this.lblLocalVariableNameSuffix.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalVariableNameSuffix.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblLocalVariableNameSuffix.Location = new System.Drawing.Point(29, 169);
            this.lblLocalVariableNameSuffix.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLocalVariableNameSuffix.Name = "lblLocalVariableNameSuffix";
            this.lblLocalVariableNameSuffix.Size = new System.Drawing.Size(396, 17);
            this.lblLocalVariableNameSuffix.TabIndex = 2;
            this.lblLocalVariableNameSuffix.Text = "* Prefix For Generated Local Variable Names";
            this.lblLocalVariableNameSuffix.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblLocalVariableNameSuffix, "Prefix that will be used for all local variable names in generated code.");
            // 
            // lblSettingsFile
            // 
            this.lblSettingsFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSettingsFile.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblSettingsFile.Location = new System.Drawing.Point(11, 16);
            this.lblSettingsFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSettingsFile.Name = "lblSettingsFile";
            this.lblSettingsFile.Size = new System.Drawing.Size(95, 21);
            this.lblSettingsFile.TabIndex = 3;
            this.lblSettingsFile.Text = "File Source";
            this.lblSettingsFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblSettingsFile, "A *.config file to save/restore user settings.");
            // 
            // lblCSharpTypeUsedForOracleBlob
            // 
            this.lblCSharpTypeUsedForOracleBlob.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSharpTypeUsedForOracleBlob.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblCSharpTypeUsedForOracleBlob.Location = new System.Drawing.Point(114, 261);
            this.lblCSharpTypeUsedForOracleBlob.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCSharpTypeUsedForOracleBlob.Name = "lblCSharpTypeUsedForOracleBlob";
            this.lblCSharpTypeUsedForOracleBlob.Size = new System.Drawing.Size(127, 16);
            this.lblCSharpTypeUsedForOracleBlob.TabIndex = 62;
            this.lblCSharpTypeUsedForOracleBlob.Text = "BLOB";
            this.lblCSharpTypeUsedForOracleBlob.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblCSharpTypeUsedForOracleBlob, "An Oracle BLOB");
            // 
            // lblCSharpTypeUsedForOracleClob
            // 
            this.lblCSharpTypeUsedForOracleClob.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSharpTypeUsedForOracleClob.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblCSharpTypeUsedForOracleClob.Location = new System.Drawing.Point(86, 288);
            this.lblCSharpTypeUsedForOracleClob.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCSharpTypeUsedForOracleClob.Name = "lblCSharpTypeUsedForOracleClob";
            this.lblCSharpTypeUsedForOracleClob.Size = new System.Drawing.Size(155, 16);
            this.lblCSharpTypeUsedForOracleClob.TabIndex = 64;
            this.lblCSharpTypeUsedForOracleClob.Text = "CLOB && NCLOB";
            this.lblCSharpTypeUsedForOracleClob.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblCSharpTypeUsedForOracleClob, "An Oracle CLOB or NCLOB");
            // 
            // lblCSharpTypeUsedForOracleRefCursor
            // 
            this.lblCSharpTypeUsedForOracleRefCursor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSharpTypeUsedForOracleRefCursor.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblCSharpTypeUsedForOracleRefCursor.Location = new System.Drawing.Point(70, 22);
            this.lblCSharpTypeUsedForOracleRefCursor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCSharpTypeUsedForOracleRefCursor.Name = "lblCSharpTypeUsedForOracleRefCursor";
            this.lblCSharpTypeUsedForOracleRefCursor.Size = new System.Drawing.Size(171, 16);
            this.lblCSharpTypeUsedForOracleRefCursor.TabIndex = 65;
            this.lblCSharpTypeUsedForOracleRefCursor.Text = "REF CURSOR";
            this.lblCSharpTypeUsedForOracleRefCursor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblCSharpTypeUsedForOracleRefCursor, "An Oracle REF CURSOR, typed or untyped.");
            // 
            // lblCSharpTypeUsedForOracleAssociativeArray
            // 
            this.lblCSharpTypeUsedForOracleAssociativeArray.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSharpTypeUsedForOracleAssociativeArray.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblCSharpTypeUsedForOracleAssociativeArray.Location = new System.Drawing.Point(34, 48);
            this.lblCSharpTypeUsedForOracleAssociativeArray.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCSharpTypeUsedForOracleAssociativeArray.Name = "lblCSharpTypeUsedForOracleAssociativeArray";
            this.lblCSharpTypeUsedForOracleAssociativeArray.Size = new System.Drawing.Size(207, 16);
            this.lblCSharpTypeUsedForOracleAssociativeArray.TabIndex = 66;
            this.lblCSharpTypeUsedForOracleAssociativeArray.Text = "Associative Array";
            this.lblCSharpTypeUsedForOracleAssociativeArray.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblCSharpTypeUsedForOracleAssociativeArray, "An Oracle associative array");
            // 
            // lblCSharpTypeUsedForOracleTimestampTZ
            // 
            this.lblCSharpTypeUsedForOracleTimestampTZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSharpTypeUsedForOracleTimestampTZ.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblCSharpTypeUsedForOracleTimestampTZ.Location = new System.Drawing.Point(25, 206);
            this.lblCSharpTypeUsedForOracleTimestampTZ.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCSharpTypeUsedForOracleTimestampTZ.Name = "lblCSharpTypeUsedForOracleTimestampTZ";
            this.lblCSharpTypeUsedForOracleTimestampTZ.Size = new System.Drawing.Size(216, 16);
            this.lblCSharpTypeUsedForOracleTimestampTZ.TabIndex = 70;
            this.lblCSharpTypeUsedForOracleTimestampTZ.Text = "TIMESTAMP WITH TZ";
            this.lblCSharpTypeUsedForOracleTimestampTZ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblCSharpTypeUsedForOracleTimestampTZ, "An Oracle TIMESTAMP WITH TIME ZONE");
            // 
            // lblCSharpTypeUsedForOracleTimestampLTZ
            // 
            this.lblCSharpTypeUsedForOracleTimestampLTZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSharpTypeUsedForOracleTimestampLTZ.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblCSharpTypeUsedForOracleTimestampLTZ.Location = new System.Drawing.Point(22, 233);
            this.lblCSharpTypeUsedForOracleTimestampLTZ.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCSharpTypeUsedForOracleTimestampLTZ.Name = "lblCSharpTypeUsedForOracleTimestampLTZ";
            this.lblCSharpTypeUsedForOracleTimestampLTZ.Size = new System.Drawing.Size(219, 16);
            this.lblCSharpTypeUsedForOracleTimestampLTZ.TabIndex = 71;
            this.lblCSharpTypeUsedForOracleTimestampLTZ.Text = "TIMESTAMP WITH LTZ";
            this.lblCSharpTypeUsedForOracleTimestampLTZ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblCSharpTypeUsedForOracleTimestampLTZ, "An Oracle TIMESTAMP WITH LOCAL TIME ZONE");
            // 
            // lblDeployResources
            // 
            this.lblDeployResources.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeployResources.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblDeployResources.Location = new System.Drawing.Point(46, 43);
            this.lblDeployResources.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDeployResources.Name = "lblDeployResources";
            this.lblDeployResources.Size = new System.Drawing.Size(379, 17);
            this.lblDeployResources.TabIndex = 3;
            this.lblDeployResources.Text = "Deploy/Update Odapter Utility Classes?";
            this.lblDeployResources.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblDeployResources, "OrclPower.cs and CaseConversion.cs need to be deployed once for each new version " +
        "of generator.");
            // 
            // lblDuplicatePackageRecordOriginatingOutsideFilterAndSchema
            // 
            this.lblDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuplicatePackageRecordOriginatingOutsideFilterAndSchema.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Location = new System.Drawing.Point(11, 69);
            this.lblDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Name = "lblDuplicatePackageRecordOriginatingOutsideFilterAndSchema";
            this.lblDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Size = new System.Drawing.Size(414, 17);
            this.lblDuplicatePackageRecordOriginatingOutsideFilterAndSchema.TabIndex = 7;
            this.lblDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Text = "Duplicate Referenced Records Outside Filter Prefix?";
            this.lblDuplicatePackageRecordOriginatingOutsideFilterAndSchema.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblDuplicatePackageRecordOriginatingOutsideFilterAndSchema, resources.GetString("lblDuplicatePackageRecordOriginatingOutsideFilterAndSchema.ToolTip"));
            // 
            // lblExcludeObjectNamesWithSpecificChars
            // 
            this.lblExcludeObjectNamesWithSpecificChars.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExcludeObjectNamesWithSpecificChars.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblExcludeObjectNamesWithSpecificChars.Location = new System.Drawing.Point(19, 94);
            this.lblExcludeObjectNamesWithSpecificChars.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExcludeObjectNamesWithSpecificChars.Name = "lblExcludeObjectNamesWithSpecificChars";
            this.lblExcludeObjectNamesWithSpecificChars.Size = new System.Drawing.Size(406, 17);
            this.lblExcludeObjectNamesWithSpecificChars.TabIndex = 4;
            this.lblExcludeObjectNamesWithSpecificChars.Text = "Exclude Object Names With Specific Characters?";
            this.lblExcludeObjectNamesWithSpecificChars.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblExcludeObjectNamesWithSpecificChars, "Packages, object types, tables, and views with given characters in the name will " +
        "be excluded from generation.\r\n");
            // 
            // lblGeneratedDynamicMethodForTypedCursor
            // 
            this.lblGeneratedDynamicMethodForTypedCursor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGeneratedDynamicMethodForTypedCursor.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblGeneratedDynamicMethodForTypedCursor.Location = new System.Drawing.Point(8, 119);
            this.lblGeneratedDynamicMethodForTypedCursor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGeneratedDynamicMethodForTypedCursor.Name = "lblGeneratedDynamicMethodForTypedCursor";
            this.lblGeneratedDynamicMethodForTypedCursor.Size = new System.Drawing.Size(417, 17);
            this.lblGeneratedDynamicMethodForTypedCursor.TabIndex = 4;
            this.lblGeneratedDynamicMethodForTypedCursor.Text = "Generate Mapping Driven Method For Typed Cursor?";
            this.lblGeneratedDynamicMethodForTypedCursor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblGeneratedDynamicMethodForTypedCursor, resources.GetString("lblGeneratedDynamicMethodForTypedCursor.ToolTip"));
            // 
            // lblConvertOracleNumberToIntegerIfColumnNameIsId
            // 
            this.lblConvertOracleNumberToIntegerIfColumnNameIsId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConvertOracleNumberToIntegerIfColumnNameIsId.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblConvertOracleNumberToIntegerIfColumnNameIsId.Location = new System.Drawing.Point(266, 125);
            this.lblConvertOracleNumberToIntegerIfColumnNameIsId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConvertOracleNumberToIntegerIfColumnNameIsId.Name = "lblConvertOracleNumberToIntegerIfColumnNameIsId";
            this.lblConvertOracleNumberToIntegerIfColumnNameIsId.Size = new System.Drawing.Size(295, 18);
            this.lblConvertOracleNumberToIntegerIfColumnNameIsId.TabIndex = 69;
            this.lblConvertOracleNumberToIntegerIfColumnNameIsId.Text = "Map \"ID\" NUMBER  as INTEGER?";
            this.lblConvertOracleNumberToIntegerIfColumnNameIsId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.lblConvertOracleNumberToIntegerIfColumnNameIsId, "If column, attribute, or parameter type is a NUMBER without precision or scale, \r" +
        "\nand its name is \"ID\" or ends with \"_ID\", then translate as an Oracle INTEGER.\r\n" +
        "");
            // 
            // gbSettings
            // 
            this.gbSettings.BackColor = System.Drawing.Color.Transparent;
            this.gbSettings.Controls.Add(this.lblSettingsFile);
            this.gbSettings.Controls.Add(this.cmbSettingsFile);
            this.gbSettings.Controls.Add(this.btnSaveSettings);
            this.gbSettings.Controls.Add(this.btnRestoreDefaults);
            this.gbSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSettings.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.gbSettings.Location = new System.Drawing.Point(16, 4);
            this.gbSettings.Margin = new System.Windows.Forms.Padding(4);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Padding = new System.Windows.Forms.Padding(4);
            this.gbSettings.Size = new System.Drawing.Size(671, 52);
            this.gbSettings.TabIndex = 0;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Settings";
            // 
            // btnRestoreDefaults
            // 
            this.btnRestoreDefaults.BackColor = System.Drawing.Color.Black;
            this.btnRestoreDefaults.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestoreDefaults.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.btnRestoreDefaults.Location = new System.Drawing.Point(505, 14);
            this.btnRestoreDefaults.Margin = new System.Windows.Forms.Padding(4);
            this.btnRestoreDefaults.Name = "btnRestoreDefaults";
            this.btnRestoreDefaults.Size = new System.Drawing.Size(153, 28);
            this.btnRestoreDefaults.TabIndex = 2;
            this.btnRestoreDefaults.Text = "Restore Defaults";
            this.btnRestoreDefaults.UseVisualStyleBackColor = false;
            this.btnRestoreDefaults.Click += new System.EventHandler(this.btnRestoreDefaults_Click);
            // 
            // gbOracleToCSharpCustomTranslation
            // 
            this.gbOracleToCSharpCustomTranslation.BackColor = System.Drawing.Color.Transparent;
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.lblConvertOracleNumberToIntegerIfColumnNameIsId);
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.lblCSharpTypeUsedForOracleTimestampLTZ);
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.lblCSharpTypeUsedForOracleTimestampTZ);
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.cmbCSharpTypeUsedForOracleTimestampLTZ);
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.cmbCSharpTypeUsedForOracleTimestampTZ);
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.cmbCSharpTypeUsedForOracleAssociativeArray);
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.lblCSharpTypeUsedForOracleAssociativeArray);
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.cmbCSharpTypeUsedForOracleRefCursor);
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.lblCSharpTypeUsedForOracleRefCursor);
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.cmbCSharpTypeUsedForOracleClob);
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.lblCSharpTypeUsedForOracleClob);
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.cmbCSharpTypeUsedForOracleBlob);
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.lblCSharpTypeUsedForOracleBlob);
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.cmbCSharpTypeUsedForOracleIntervalDayToSecond);
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.cmbCSharpTypeUsedForOracleTimestamp);
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.cmbCSharpTypeUsedForOracleDate);
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.cmbCSharpTypeUsedForOracleNumber);
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.cmbCSharpTypeUsedForOracleInteger);
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.cbConvertOracleNumberToIntegerIfColumnNameIsId);
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.lblCSharpTypeUsedForOracleTimestamp);
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.lblCSharpUsedForOracleIntervalDayToSecond);
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.lblCSharpTypeUsedForOracleDate);
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.lblCSharpTypeUsedForOracleNumber);
            this.gbOracleToCSharpCustomTranslation.Controls.Add(this.lblCSharpTypeUsedForOracleInteger);
            this.gbOracleToCSharpCustomTranslation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbOracleToCSharpCustomTranslation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbOracleToCSharpCustomTranslation.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.gbOracleToCSharpCustomTranslation.Location = new System.Drawing.Point(16, 418);
            this.gbOracleToCSharpCustomTranslation.Margin = new System.Windows.Forms.Padding(4);
            this.gbOracleToCSharpCustomTranslation.Name = "gbOracleToCSharpCustomTranslation";
            this.gbOracleToCSharpCustomTranslation.Padding = new System.Windows.Forms.Padding(4);
            this.gbOracleToCSharpCustomTranslation.Size = new System.Drawing.Size(602, 317);
            this.gbOracleToCSharpCustomTranslation.TabIndex = 3;
            this.gbOracleToCSharpCustomTranslation.TabStop = false;
            this.gbOracleToCSharpCustomTranslation.Text = "Oracle to C# Custom Translation";
            // 
            // gbAdvancedProcOptions
            // 
            this.gbAdvancedProcOptions.BackColor = System.Drawing.Color.Transparent;
            this.gbAdvancedProcOptions.Controls.Add(this.lblGeneratedDynamicMethodForTypedCursor);
            this.gbAdvancedProcOptions.Controls.Add(this.lblExcludeObjectNamesWithSpecificChars);
            this.gbAdvancedProcOptions.Controls.Add(this.lblDuplicatePackageRecordOriginatingOutsideFilterAndSchema);
            this.gbAdvancedProcOptions.Controls.Add(this.lblDeployResources);
            this.gbAdvancedProcOptions.Controls.Add(this.lblLocalVariableNameSuffix);
            this.gbAdvancedProcOptions.Controls.Add(this.txtLocalVariableNameSuffix);
            this.gbAdvancedProcOptions.Controls.Add(this.cbGeneratedDynamicMethodForTypedCursor);
            this.gbAdvancedProcOptions.Controls.Add(this.txtExcludeChars);
            this.gbAdvancedProcOptions.Controls.Add(this.lblAncestorClass);
            this.gbAdvancedProcOptions.Controls.Add(this.cbExcludeObjectNamesWithSpecificChars);
            this.gbAdvancedProcOptions.Controls.Add(this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema);
            this.gbAdvancedProcOptions.Controls.Add(this.lblCSharpVersion);
            this.gbAdvancedProcOptions.Controls.Add(this.lblMaxAssocArraySize);
            this.gbAdvancedProcOptions.Controls.Add(this.txtMaxAssocArraySize);
            this.gbAdvancedProcOptions.Controls.Add(this.lblMaxReturnArgStringSize);
            this.gbAdvancedProcOptions.Controls.Add(this.cmbCSharpVersion);
            this.gbAdvancedProcOptions.Controls.Add(this.txtMaxReturnArgStringSize);
            this.gbAdvancedProcOptions.Controls.Add(this.cbDeployResources);
            this.gbAdvancedProcOptions.Controls.Add(this.txtPackageAncestorClass);
            this.gbAdvancedProcOptions.Controls.Add(this.cbPartialPackageClasses);
            this.gbAdvancedProcOptions.Controls.Add(this.lblPartial);
            this.gbAdvancedProcOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbAdvancedProcOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAdvancedProcOptions.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.gbAdvancedProcOptions.Location = new System.Drawing.Point(633, 418);
            this.gbAdvancedProcOptions.Margin = new System.Windows.Forms.Padding(4);
            this.gbAdvancedProcOptions.Name = "gbAdvancedProcOptions";
            this.gbAdvancedProcOptions.Padding = new System.Windows.Forms.Padding(4);
            this.gbAdvancedProcOptions.Size = new System.Drawing.Size(645, 317);
            this.gbAdvancedProcOptions.TabIndex = 4;
            this.gbAdvancedProcOptions.TabStop = false;
            this.gbAdvancedProcOptions.Text = "Advanced Options";
            // 
            // txtLocalVariableNameSuffix
            // 
            this.txtLocalVariableNameSuffix.BackColor = System.Drawing.Color.Black;
            this.txtLocalVariableNameSuffix.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocalVariableNameSuffix.ForeColor = System.Drawing.Color.Yellow;
            this.txtLocalVariableNameSuffix.Location = new System.Drawing.Point(435, 167);
            this.txtLocalVariableNameSuffix.Margin = new System.Windows.Forms.Padding(4);
            this.txtLocalVariableNameSuffix.Name = "txtLocalVariableNameSuffix";
            this.txtLocalVariableNameSuffix.Size = new System.Drawing.Size(71, 23);
            this.txtLocalVariableNameSuffix.TabIndex = 7;
            this.txtLocalVariableNameSuffix.TextChanged += new System.EventHandler(this.txtLocalVariableNameSuffix_TextChanged);
            // 
            // txtExcludeChars
            // 
            this.txtExcludeChars.BackColor = System.Drawing.Color.Black;
            this.txtExcludeChars.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExcludeChars.ForeColor = System.Drawing.Color.Yellow;
            this.txtExcludeChars.Location = new System.Drawing.Point(459, 92);
            this.txtExcludeChars.Margin = new System.Windows.Forms.Padding(4);
            this.txtExcludeChars.Name = "txtExcludeChars";
            this.txtExcludeChars.Size = new System.Drawing.Size(91, 23);
            this.txtExcludeChars.TabIndex = 4;
            // 
            // lblGenerateStatus
            // 
            this.lblGenerateStatus.BackColor = System.Drawing.Color.Black;
            this.lblGenerateStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGenerateStatus.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblGenerateStatus.Location = new System.Drawing.Point(839, 11);
            this.lblGenerateStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGenerateStatus.Name = "lblGenerateStatus";
            this.lblGenerateStatus.Size = new System.Drawing.Size(441, 32);
            this.lblGenerateStatus.TabIndex = 0;
            this.lblGenerateStatus.Text = "Generate Status";
            this.lblGenerateStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbGeneratedDynamicMethodForTypedCursor
            // 
            this.cbGeneratedDynamicMethodForTypedCursor.BackColor = System.Drawing.Color.Transparent;
            this.cbGeneratedDynamicMethodForTypedCursor.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGeneratedDynamicMethodForTypedCursor.Checked = true;
            this.cbGeneratedDynamicMethodForTypedCursor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGeneratedDynamicMethodForTypedCursor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbGeneratedDynamicMethodForTypedCursor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGeneratedDynamicMethodForTypedCursor.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.cbGeneratedDynamicMethodForTypedCursor.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGeneratedDynamicMethodForTypedCursor.Location = new System.Drawing.Point(435, 119);
            this.cbGeneratedDynamicMethodForTypedCursor.Margin = new System.Windows.Forms.Padding(4);
            this.cbGeneratedDynamicMethodForTypedCursor.Name = "cbGeneratedDynamicMethodForTypedCursor";
            this.cbGeneratedDynamicMethodForTypedCursor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbGeneratedDynamicMethodForTypedCursor.Size = new System.Drawing.Size(17, 21);
            this.cbGeneratedDynamicMethodForTypedCursor.TabIndex = 5;
            this.cbGeneratedDynamicMethodForTypedCursor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.cbGeneratedDynamicMethodForTypedCursor, resources.GetString("cbGeneratedDynamicMethodForTypedCursor.ToolTip"));
            this.cbGeneratedDynamicMethodForTypedCursor.UseVisualStyleBackColor = false;
            this.cbGeneratedDynamicMethodForTypedCursor.CheckedChanged += new System.EventHandler(this.cbGeneratedDynamicMethodForTypedCursor_CheckedChanged);
            // 
            // cbExcludeObjectNamesWithSpecificChars
            // 
            this.cbExcludeObjectNamesWithSpecificChars.BackColor = System.Drawing.Color.Transparent;
            this.cbExcludeObjectNamesWithSpecificChars.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbExcludeObjectNamesWithSpecificChars.Checked = true;
            this.cbExcludeObjectNamesWithSpecificChars.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbExcludeObjectNamesWithSpecificChars.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbExcludeObjectNamesWithSpecificChars.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbExcludeObjectNamesWithSpecificChars.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.cbExcludeObjectNamesWithSpecificChars.Location = new System.Drawing.Point(435, 94);
            this.cbExcludeObjectNamesWithSpecificChars.Margin = new System.Windows.Forms.Padding(4);
            this.cbExcludeObjectNamesWithSpecificChars.Name = "cbExcludeObjectNamesWithSpecificChars";
            this.cbExcludeObjectNamesWithSpecificChars.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbExcludeObjectNamesWithSpecificChars.Size = new System.Drawing.Size(17, 21);
            this.cbExcludeObjectNamesWithSpecificChars.TabIndex = 3;
            this.cbExcludeObjectNamesWithSpecificChars.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.cbExcludeObjectNamesWithSpecificChars, "Packages, object types, tables, and views with given characters in the name will " +
        "be excluded from generation.");
            this.cbExcludeObjectNamesWithSpecificChars.UseVisualStyleBackColor = false;
            this.cbExcludeObjectNamesWithSpecificChars.CheckedChanged += new System.EventHandler(this.cbExcludeObjectNamesWithSpecificChars_CheckedChanged);
            // 
            // cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema
            // 
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.BackColor = System.Drawing.Color.Transparent;
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Checked = true;
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Location = new System.Drawing.Point(435, 69);
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Margin = new System.Windows.Forms.Padding(4);
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Name = "cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema";
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Size = new System.Drawing.Size(17, 21);
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.TabIndex = 2;
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.UseVisualStyleBackColor = false;
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.CheckedChanged += new System.EventHandler(this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema_CheckedChanged);
            // 
            // cmbCSharpVersion
            // 
            this.cmbCSharpVersion.BackColor = System.Drawing.Color.Black;
            this.cmbCSharpVersion.BorderColor = System.Drawing.Color.White;
            this.cmbCSharpVersion.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCSharpVersion.ButtonColor = System.Drawing.SystemColors.Control;
            this.cmbCSharpVersion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCSharpVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCSharpVersion.ForeColor = System.Drawing.Color.Yellow;
            this.cmbCSharpVersion.FormattingEnabled = true;
            this.cmbCSharpVersion.Location = new System.Drawing.Point(435, 15);
            this.cmbCSharpVersion.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCSharpVersion.Name = "cmbCSharpVersion";
            this.cmbCSharpVersion.Size = new System.Drawing.Size(187, 25);
            this.cmbCSharpVersion.TabIndex = 0;
            this.cmbCSharpVersion.SelectedIndexChanged += new System.EventHandler(this.cmbCSharpVersion_SelectedIndexChanged);
            // 
            // cbDeployResources
            // 
            this.cbDeployResources.BackColor = System.Drawing.Color.Transparent;
            this.cbDeployResources.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbDeployResources.Checked = true;
            this.cbDeployResources.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDeployResources.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDeployResources.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDeployResources.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.cbDeployResources.Location = new System.Drawing.Point(435, 44);
            this.cbDeployResources.Margin = new System.Windows.Forms.Padding(4);
            this.cbDeployResources.Name = "cbDeployResources";
            this.cbDeployResources.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbDeployResources.Size = new System.Drawing.Size(17, 21);
            this.cbDeployResources.TabIndex = 1;
            this.cbDeployResources.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbDeployResources.UseVisualStyleBackColor = false;
            this.cbDeployResources.CheckedChanged += new System.EventHandler(this.cbDeployResources_CheckedChanged);
            // 
            // cbPartialPackageClasses
            // 
            this.cbPartialPackageClasses.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbPartialPackageClasses.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbPartialPackageClasses.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPartialPackageClasses.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbPartialPackageClasses.Location = new System.Drawing.Point(435, 144);
            this.cbPartialPackageClasses.Margin = new System.Windows.Forms.Padding(4);
            this.cbPartialPackageClasses.Name = "cbPartialPackageClasses";
            this.cbPartialPackageClasses.Size = new System.Drawing.Size(17, 21);
            this.cbPartialPackageClasses.TabIndex = 6;
            this.cbPartialPackageClasses.UseVisualStyleBackColor = true;
            // 
            // cmbCSharpTypeUsedForOracleTimestampLTZ
            // 
            this.cmbCSharpTypeUsedForOracleTimestampLTZ.BackColor = System.Drawing.Color.Black;
            this.cmbCSharpTypeUsedForOracleTimestampLTZ.BorderColor = System.Drawing.Color.White;
            this.cmbCSharpTypeUsedForOracleTimestampLTZ.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCSharpTypeUsedForOracleTimestampLTZ.ButtonColor = System.Drawing.SystemColors.Control;
            this.cmbCSharpTypeUsedForOracleTimestampLTZ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCSharpTypeUsedForOracleTimestampLTZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCSharpTypeUsedForOracleTimestampLTZ.ForeColor = System.Drawing.Color.Yellow;
            this.cmbCSharpTypeUsedForOracleTimestampLTZ.FormattingEnabled = true;
            this.cmbCSharpTypeUsedForOracleTimestampLTZ.Location = new System.Drawing.Point(244, 230);
            this.cmbCSharpTypeUsedForOracleTimestampLTZ.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCSharpTypeUsedForOracleTimestampLTZ.Name = "cmbCSharpTypeUsedForOracleTimestampLTZ";
            this.cmbCSharpTypeUsedForOracleTimestampLTZ.Size = new System.Drawing.Size(337, 25);
            this.cmbCSharpTypeUsedForOracleTimestampLTZ.TabIndex = 8;
            // 
            // cmbCSharpTypeUsedForOracleTimestampTZ
            // 
            this.cmbCSharpTypeUsedForOracleTimestampTZ.BackColor = System.Drawing.Color.Black;
            this.cmbCSharpTypeUsedForOracleTimestampTZ.BorderColor = System.Drawing.Color.White;
            this.cmbCSharpTypeUsedForOracleTimestampTZ.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCSharpTypeUsedForOracleTimestampTZ.ButtonColor = System.Drawing.SystemColors.Control;
            this.cmbCSharpTypeUsedForOracleTimestampTZ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCSharpTypeUsedForOracleTimestampTZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCSharpTypeUsedForOracleTimestampTZ.ForeColor = System.Drawing.Color.Yellow;
            this.cmbCSharpTypeUsedForOracleTimestampTZ.FormattingEnabled = true;
            this.cmbCSharpTypeUsedForOracleTimestampTZ.Location = new System.Drawing.Point(244, 203);
            this.cmbCSharpTypeUsedForOracleTimestampTZ.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCSharpTypeUsedForOracleTimestampTZ.Name = "cmbCSharpTypeUsedForOracleTimestampTZ";
            this.cmbCSharpTypeUsedForOracleTimestampTZ.Size = new System.Drawing.Size(337, 25);
            this.cmbCSharpTypeUsedForOracleTimestampTZ.TabIndex = 7;
            // 
            // cmbCSharpTypeUsedForOracleAssociativeArray
            // 
            this.cmbCSharpTypeUsedForOracleAssociativeArray.BackColor = System.Drawing.Color.Black;
            this.cmbCSharpTypeUsedForOracleAssociativeArray.BorderColor = System.Drawing.Color.White;
            this.cmbCSharpTypeUsedForOracleAssociativeArray.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCSharpTypeUsedForOracleAssociativeArray.ButtonColor = System.Drawing.SystemColors.Control;
            this.cmbCSharpTypeUsedForOracleAssociativeArray.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCSharpTypeUsedForOracleAssociativeArray.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCSharpTypeUsedForOracleAssociativeArray.ForeColor = System.Drawing.Color.Yellow;
            this.cmbCSharpTypeUsedForOracleAssociativeArray.FormattingEnabled = true;
            this.cmbCSharpTypeUsedForOracleAssociativeArray.Location = new System.Drawing.Point(244, 43);
            this.cmbCSharpTypeUsedForOracleAssociativeArray.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCSharpTypeUsedForOracleAssociativeArray.Name = "cmbCSharpTypeUsedForOracleAssociativeArray";
            this.cmbCSharpTypeUsedForOracleAssociativeArray.Size = new System.Drawing.Size(337, 25);
            this.cmbCSharpTypeUsedForOracleAssociativeArray.TabIndex = 1;
            // 
            // cmbCSharpTypeUsedForOracleRefCursor
            // 
            this.cmbCSharpTypeUsedForOracleRefCursor.BackColor = System.Drawing.Color.Black;
            this.cmbCSharpTypeUsedForOracleRefCursor.BorderColor = System.Drawing.Color.White;
            this.cmbCSharpTypeUsedForOracleRefCursor.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCSharpTypeUsedForOracleRefCursor.ButtonColor = System.Drawing.SystemColors.Control;
            this.cmbCSharpTypeUsedForOracleRefCursor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCSharpTypeUsedForOracleRefCursor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCSharpTypeUsedForOracleRefCursor.ForeColor = System.Drawing.Color.Yellow;
            this.cmbCSharpTypeUsedForOracleRefCursor.FormattingEnabled = true;
            this.cmbCSharpTypeUsedForOracleRefCursor.Location = new System.Drawing.Point(244, 16);
            this.cmbCSharpTypeUsedForOracleRefCursor.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCSharpTypeUsedForOracleRefCursor.Name = "cmbCSharpTypeUsedForOracleRefCursor";
            this.cmbCSharpTypeUsedForOracleRefCursor.Size = new System.Drawing.Size(337, 25);
            this.cmbCSharpTypeUsedForOracleRefCursor.TabIndex = 0;
            // 
            // cmbCSharpTypeUsedForOracleClob
            // 
            this.cmbCSharpTypeUsedForOracleClob.BackColor = System.Drawing.Color.Black;
            this.cmbCSharpTypeUsedForOracleClob.BorderColor = System.Drawing.Color.White;
            this.cmbCSharpTypeUsedForOracleClob.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCSharpTypeUsedForOracleClob.ButtonColor = System.Drawing.SystemColors.Control;
            this.cmbCSharpTypeUsedForOracleClob.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCSharpTypeUsedForOracleClob.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCSharpTypeUsedForOracleClob.ForeColor = System.Drawing.Color.LightYellow;
            this.cmbCSharpTypeUsedForOracleClob.FormattingEnabled = true;
            this.cmbCSharpTypeUsedForOracleClob.Location = new System.Drawing.Point(244, 284);
            this.cmbCSharpTypeUsedForOracleClob.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCSharpTypeUsedForOracleClob.Name = "cmbCSharpTypeUsedForOracleClob";
            this.cmbCSharpTypeUsedForOracleClob.Size = new System.Drawing.Size(337, 25);
            this.cmbCSharpTypeUsedForOracleClob.TabIndex = 10;
            // 
            // cmbCSharpTypeUsedForOracleBlob
            // 
            this.cmbCSharpTypeUsedForOracleBlob.BackColor = System.Drawing.Color.Black;
            this.cmbCSharpTypeUsedForOracleBlob.BorderColor = System.Drawing.Color.White;
            this.cmbCSharpTypeUsedForOracleBlob.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCSharpTypeUsedForOracleBlob.ButtonColor = System.Drawing.SystemColors.Control;
            this.cmbCSharpTypeUsedForOracleBlob.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCSharpTypeUsedForOracleBlob.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCSharpTypeUsedForOracleBlob.ForeColor = System.Drawing.Color.Yellow;
            this.cmbCSharpTypeUsedForOracleBlob.FormattingEnabled = true;
            this.cmbCSharpTypeUsedForOracleBlob.Location = new System.Drawing.Point(244, 257);
            this.cmbCSharpTypeUsedForOracleBlob.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCSharpTypeUsedForOracleBlob.Name = "cmbCSharpTypeUsedForOracleBlob";
            this.cmbCSharpTypeUsedForOracleBlob.Size = new System.Drawing.Size(337, 25);
            this.cmbCSharpTypeUsedForOracleBlob.TabIndex = 9;
            // 
            // cmbCSharpTypeUsedForOracleIntervalDayToSecond
            // 
            this.cmbCSharpTypeUsedForOracleIntervalDayToSecond.BorderColor = System.Drawing.Color.White;
            this.cmbCSharpTypeUsedForOracleIntervalDayToSecond.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCSharpTypeUsedForOracleIntervalDayToSecond.ButtonColor = System.Drawing.SystemColors.Control;
            this.cmbCSharpTypeUsedForOracleIntervalDayToSecond.Enabled = false;
            this.cmbCSharpTypeUsedForOracleIntervalDayToSecond.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCSharpTypeUsedForOracleIntervalDayToSecond.FormattingEnabled = true;
            this.cmbCSharpTypeUsedForOracleIntervalDayToSecond.Location = new System.Drawing.Point(213, 375);
            this.cmbCSharpTypeUsedForOracleIntervalDayToSecond.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCSharpTypeUsedForOracleIntervalDayToSecond.Name = "cmbCSharpTypeUsedForOracleIntervalDayToSecond";
            this.cmbCSharpTypeUsedForOracleIntervalDayToSecond.Size = new System.Drawing.Size(351, 25);
            this.cmbCSharpTypeUsedForOracleIntervalDayToSecond.TabIndex = 7;
            // 
            // cmbCSharpTypeUsedForOracleTimestamp
            // 
            this.cmbCSharpTypeUsedForOracleTimestamp.BackColor = System.Drawing.Color.Black;
            this.cmbCSharpTypeUsedForOracleTimestamp.BorderColor = System.Drawing.Color.White;
            this.cmbCSharpTypeUsedForOracleTimestamp.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCSharpTypeUsedForOracleTimestamp.ButtonColor = System.Drawing.SystemColors.Control;
            this.cmbCSharpTypeUsedForOracleTimestamp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCSharpTypeUsedForOracleTimestamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCSharpTypeUsedForOracleTimestamp.ForeColor = System.Drawing.Color.Yellow;
            this.cmbCSharpTypeUsedForOracleTimestamp.FormattingEnabled = true;
            this.cmbCSharpTypeUsedForOracleTimestamp.Location = new System.Drawing.Point(244, 176);
            this.cmbCSharpTypeUsedForOracleTimestamp.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCSharpTypeUsedForOracleTimestamp.Name = "cmbCSharpTypeUsedForOracleTimestamp";
            this.cmbCSharpTypeUsedForOracleTimestamp.Size = new System.Drawing.Size(337, 25);
            this.cmbCSharpTypeUsedForOracleTimestamp.TabIndex = 6;
            // 
            // cmbCSharpTypeUsedForOracleDate
            // 
            this.cmbCSharpTypeUsedForOracleDate.BackColor = System.Drawing.Color.Black;
            this.cmbCSharpTypeUsedForOracleDate.BorderColor = System.Drawing.Color.White;
            this.cmbCSharpTypeUsedForOracleDate.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCSharpTypeUsedForOracleDate.ButtonColor = System.Drawing.SystemColors.Control;
            this.cmbCSharpTypeUsedForOracleDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCSharpTypeUsedForOracleDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCSharpTypeUsedForOracleDate.ForeColor = System.Drawing.Color.Yellow;
            this.cmbCSharpTypeUsedForOracleDate.FormattingEnabled = true;
            this.cmbCSharpTypeUsedForOracleDate.Location = new System.Drawing.Point(244, 149);
            this.cmbCSharpTypeUsedForOracleDate.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCSharpTypeUsedForOracleDate.Name = "cmbCSharpTypeUsedForOracleDate";
            this.cmbCSharpTypeUsedForOracleDate.Size = new System.Drawing.Size(337, 25);
            this.cmbCSharpTypeUsedForOracleDate.TabIndex = 5;
            // 
            // cmbCSharpTypeUsedForOracleNumber
            // 
            this.cmbCSharpTypeUsedForOracleNumber.BackColor = System.Drawing.Color.Black;
            this.cmbCSharpTypeUsedForOracleNumber.BorderColor = System.Drawing.Color.White;
            this.cmbCSharpTypeUsedForOracleNumber.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCSharpTypeUsedForOracleNumber.ButtonColor = System.Drawing.SystemColors.Control;
            this.cmbCSharpTypeUsedForOracleNumber.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCSharpTypeUsedForOracleNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCSharpTypeUsedForOracleNumber.ForeColor = System.Drawing.Color.Yellow;
            this.cmbCSharpTypeUsedForOracleNumber.FormattingEnabled = true;
            this.cmbCSharpTypeUsedForOracleNumber.Location = new System.Drawing.Point(244, 97);
            this.cmbCSharpTypeUsedForOracleNumber.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCSharpTypeUsedForOracleNumber.Name = "cmbCSharpTypeUsedForOracleNumber";
            this.cmbCSharpTypeUsedForOracleNumber.Size = new System.Drawing.Size(337, 25);
            this.cmbCSharpTypeUsedForOracleNumber.TabIndex = 3;
            // 
            // cmbCSharpTypeUsedForOracleInteger
            // 
            this.cmbCSharpTypeUsedForOracleInteger.BackColor = System.Drawing.Color.Black;
            this.cmbCSharpTypeUsedForOracleInteger.BorderColor = System.Drawing.Color.White;
            this.cmbCSharpTypeUsedForOracleInteger.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCSharpTypeUsedForOracleInteger.ButtonColor = System.Drawing.SystemColors.Control;
            this.cmbCSharpTypeUsedForOracleInteger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCSharpTypeUsedForOracleInteger.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCSharpTypeUsedForOracleInteger.ForeColor = System.Drawing.Color.Yellow;
            this.cmbCSharpTypeUsedForOracleInteger.FormattingEnabled = true;
            this.cmbCSharpTypeUsedForOracleInteger.Location = new System.Drawing.Point(244, 70);
            this.cmbCSharpTypeUsedForOracleInteger.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCSharpTypeUsedForOracleInteger.Name = "cmbCSharpTypeUsedForOracleInteger";
            this.cmbCSharpTypeUsedForOracleInteger.Size = new System.Drawing.Size(337, 25);
            this.cmbCSharpTypeUsedForOracleInteger.TabIndex = 2;
            // 
            // cbConvertOracleNumberToIntegerIfColumnNameIsId
            // 
            this.cbConvertOracleNumberToIntegerIfColumnNameIsId.BackColor = System.Drawing.Color.Transparent;
            this.cbConvertOracleNumberToIntegerIfColumnNameIsId.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbConvertOracleNumberToIntegerIfColumnNameIsId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbConvertOracleNumberToIntegerIfColumnNameIsId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbConvertOracleNumberToIntegerIfColumnNameIsId.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.cbConvertOracleNumberToIntegerIfColumnNameIsId.Location = new System.Drawing.Point(246, 126);
            this.cbConvertOracleNumberToIntegerIfColumnNameIsId.Margin = new System.Windows.Forms.Padding(4);
            this.cbConvertOracleNumberToIntegerIfColumnNameIsId.Name = "cbConvertOracleNumberToIntegerIfColumnNameIsId";
            this.cbConvertOracleNumberToIntegerIfColumnNameIsId.Size = new System.Drawing.Size(18, 21);
            this.cbConvertOracleNumberToIntegerIfColumnNameIsId.TabIndex = 4;
            this.cbConvertOracleNumberToIntegerIfColumnNameIsId.UseVisualStyleBackColor = false;
            // 
            // cmbSettingsFile
            // 
            this.cmbSettingsFile.BackColor = System.Drawing.Color.Black;
            this.cmbSettingsFile.BorderColor = System.Drawing.Color.White;
            this.cmbSettingsFile.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbSettingsFile.ButtonColor = System.Drawing.SystemColors.Control;
            this.cmbSettingsFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSettingsFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSettingsFile.ForeColor = System.Drawing.Color.Yellow;
            this.cmbSettingsFile.FormattingEnabled = true;
            this.cmbSettingsFile.Location = new System.Drawing.Point(111, 16);
            this.cmbSettingsFile.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSettingsFile.Name = "cmbSettingsFile";
            this.cmbSettingsFile.Size = new System.Drawing.Size(255, 25);
            this.cmbSettingsFile.TabIndex = 0;
            this.cmbSettingsFile.SelectedIndexChanged += new System.EventHandler(this.cmbSettingsFile_SelectedIndexChanged);
            // 
            // cbIsSavePassword
            // 
            this.cbIsSavePassword.BackColor = System.Drawing.Color.Transparent;
            this.cbIsSavePassword.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbIsSavePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbIsSavePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIsSavePassword.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.cbIsSavePassword.Location = new System.Drawing.Point(617, 74);
            this.cbIsSavePassword.Margin = new System.Windows.Forms.Padding(4);
            this.cbIsSavePassword.Name = "cbIsSavePassword";
            this.cbIsSavePassword.Size = new System.Drawing.Size(17, 21);
            this.cbIsSavePassword.TabIndex = 68;
            this.cbIsSavePassword.UseVisualStyleBackColor = false;
            // 
            // cmbDBInstance
            // 
            this.cmbDBInstance.BackColor = System.Drawing.Color.Black;
            this.cmbDBInstance.BorderColor = System.Drawing.Color.White;
            this.cmbDBInstance.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbDBInstance.ButtonColor = System.Drawing.SystemColors.Control;
            this.cmbDBInstance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDBInstance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDBInstance.ForeColor = System.Drawing.Color.Yellow;
            this.cmbDBInstance.FormattingEnabled = true;
            this.cmbDBInstance.Location = new System.Drawing.Point(480, 16);
            this.cmbDBInstance.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDBInstance.Name = "cmbDBInstance";
            this.cmbDBInstance.Size = new System.Drawing.Size(319, 25);
            this.cmbDBInstance.TabIndex = 2;
            this.cmbDBInstance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDBInstance_KeyPress);
            // 
            // cmbClientHome
            // 
            this.cmbClientHome.BackColor = System.Drawing.Color.Black;
            this.cmbClientHome.BorderColor = System.Drawing.Color.White;
            this.cmbClientHome.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbClientHome.ButtonColor = System.Drawing.SystemColors.Control;
            this.cmbClientHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbClientHome.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbClientHome.ForeColor = System.Drawing.Color.Yellow;
            this.cmbClientHome.FormattingEnabled = true;
            this.cmbClientHome.Location = new System.Drawing.Point(123, 16);
            this.cmbClientHome.Margin = new System.Windows.Forms.Padding(4);
            this.cmbClientHome.Name = "cmbClientHome";
            this.cmbClientHome.Size = new System.Drawing.Size(243, 25);
            this.cmbClientHome.TabIndex = 1;
            this.cmbClientHome.SelectedIndexChanged += new System.EventHandler(this.cmbClientHome_SelectedIndexChanged);
            // 
            // cbGenerateRecord
            // 
            this.cbGenerateRecord.BackColor = System.Drawing.Color.Transparent;
            this.cbGenerateRecord.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGenerateRecord.Enabled = false;
            this.cbGenerateRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbGenerateRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGenerateRecord.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.cbGenerateRecord.Location = new System.Drawing.Point(165, 96);
            this.cbGenerateRecord.Margin = new System.Windows.Forms.Padding(4);
            this.cbGenerateRecord.Name = "cbGenerateRecord";
            this.cbGenerateRecord.Size = new System.Drawing.Size(17, 21);
            this.cbGenerateRecord.TabIndex = 82;
            this.cbGenerateRecord.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGenerateRecord.UseVisualStyleBackColor = false;
            // 
            // cmbDtoInterfaceCategoryView
            // 
            this.cmbDtoInterfaceCategoryView.BackColor = System.Drawing.Color.Black;
            this.cmbDtoInterfaceCategoryView.BorderColor = System.Drawing.Color.White;
            this.cmbDtoInterfaceCategoryView.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbDtoInterfaceCategoryView.ButtonColor = System.Drawing.SystemColors.Control;
            this.cmbDtoInterfaceCategoryView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDtoInterfaceCategoryView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDtoInterfaceCategoryView.ForeColor = System.Drawing.Color.Yellow;
            this.cmbDtoInterfaceCategoryView.FormattingEnabled = true;
            this.cmbDtoInterfaceCategoryView.Location = new System.Drawing.Point(437, 198);
            this.cmbDtoInterfaceCategoryView.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDtoInterfaceCategoryView.Name = "cmbDtoInterfaceCategoryView";
            this.cmbDtoInterfaceCategoryView.Size = new System.Drawing.Size(143, 25);
            this.cmbDtoInterfaceCategoryView.TabIndex = 20;
            // 
            // cmbDtoInterfaceCategoryTable
            // 
            this.cmbDtoInterfaceCategoryTable.BackColor = System.Drawing.Color.Black;
            this.cmbDtoInterfaceCategoryTable.BorderColor = System.Drawing.Color.White;
            this.cmbDtoInterfaceCategoryTable.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbDtoInterfaceCategoryTable.ButtonColor = System.Drawing.SystemColors.Control;
            this.cmbDtoInterfaceCategoryTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDtoInterfaceCategoryTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDtoInterfaceCategoryTable.ForeColor = System.Drawing.Color.Yellow;
            this.cmbDtoInterfaceCategoryTable.FormattingEnabled = true;
            this.cmbDtoInterfaceCategoryTable.Location = new System.Drawing.Point(437, 172);
            this.cmbDtoInterfaceCategoryTable.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDtoInterfaceCategoryTable.Name = "cmbDtoInterfaceCategoryTable";
            this.cmbDtoInterfaceCategoryTable.Size = new System.Drawing.Size(143, 25);
            this.cmbDtoInterfaceCategoryTable.TabIndex = 16;
            // 
            // cmbDtoInterfaceCategoryObject
            // 
            this.cmbDtoInterfaceCategoryObject.BackColor = System.Drawing.Color.Black;
            this.cmbDtoInterfaceCategoryObject.BorderColor = System.Drawing.Color.White;
            this.cmbDtoInterfaceCategoryObject.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbDtoInterfaceCategoryObject.ButtonColor = System.Drawing.SystemColors.Control;
            this.cmbDtoInterfaceCategoryObject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDtoInterfaceCategoryObject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDtoInterfaceCategoryObject.ForeColor = System.Drawing.Color.Yellow;
            this.cmbDtoInterfaceCategoryObject.FormattingEnabled = true;
            this.cmbDtoInterfaceCategoryObject.Location = new System.Drawing.Point(437, 146);
            this.cmbDtoInterfaceCategoryObject.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDtoInterfaceCategoryObject.Name = "cmbDtoInterfaceCategoryObject";
            this.cmbDtoInterfaceCategoryObject.Size = new System.Drawing.Size(143, 25);
            this.cmbDtoInterfaceCategoryObject.TabIndex = 12;
            // 
            // cmbDtoInterfaceCategoryRecord
            // 
            this.cmbDtoInterfaceCategoryRecord.BackColor = System.Drawing.Color.Black;
            this.cmbDtoInterfaceCategoryRecord.BorderColor = System.Drawing.Color.White;
            this.cmbDtoInterfaceCategoryRecord.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbDtoInterfaceCategoryRecord.ButtonColor = System.Drawing.SystemColors.Control;
            this.cmbDtoInterfaceCategoryRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDtoInterfaceCategoryRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDtoInterfaceCategoryRecord.ForeColor = System.Drawing.Color.Yellow;
            this.cmbDtoInterfaceCategoryRecord.FormattingEnabled = true;
            this.cmbDtoInterfaceCategoryRecord.Location = new System.Drawing.Point(437, 94);
            this.cmbDtoInterfaceCategoryRecord.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDtoInterfaceCategoryRecord.Name = "cmbDtoInterfaceCategoryRecord";
            this.cmbDtoInterfaceCategoryRecord.Size = new System.Drawing.Size(143, 25);
            this.cmbDtoInterfaceCategoryRecord.TabIndex = 5;
            // 
            // cbIncludeFilterPrefixInNaming
            // 
            this.cbIncludeFilterPrefixInNaming.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbIncludeFilterPrefixInNaming.Checked = true;
            this.cbIncludeFilterPrefixInNaming.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIncludeFilterPrefixInNaming.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbIncludeFilterPrefixInNaming.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIncludeFilterPrefixInNaming.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.cbIncludeFilterPrefixInNaming.Location = new System.Drawing.Point(785, 227);
            this.cbIncludeFilterPrefixInNaming.Margin = new System.Windows.Forms.Padding(4);
            this.cbIncludeFilterPrefixInNaming.Name = "cbIncludeFilterPrefixInNaming";
            this.cbIncludeFilterPrefixInNaming.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbIncludeFilterPrefixInNaming.Size = new System.Drawing.Size(19, 21);
            this.cbIncludeFilterPrefixInNaming.TabIndex = 22;
            this.cbIncludeFilterPrefixInNaming.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbIncludeFilterPrefixInNaming.UseVisualStyleBackColor = true;
            // 
            // cbGenerateBaseAdapter
            // 
            this.cbGenerateBaseAdapter.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGenerateBaseAdapter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbGenerateBaseAdapter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGenerateBaseAdapter.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.cbGenerateBaseAdapter.Location = new System.Drawing.Point(165, 122);
            this.cbGenerateBaseAdapter.Margin = new System.Windows.Forms.Padding(4);
            this.cbGenerateBaseAdapter.Name = "cbGenerateBaseAdapter";
            this.cbGenerateBaseAdapter.Size = new System.Drawing.Size(17, 21);
            this.cbGenerateBaseAdapter.TabIndex = 7;
            this.cbGenerateBaseAdapter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGenerateBaseAdapter.UseVisualStyleBackColor = true;
            this.cbGenerateBaseAdapter.CheckedChanged += new System.EventHandler(this.cbGenerateBaseAdapterClass_CheckedChanged);
            // 
            // cbGenerateView
            // 
            this.cbGenerateView.BackColor = System.Drawing.Color.Transparent;
            this.cbGenerateView.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGenerateView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbGenerateView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGenerateView.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.cbGenerateView.Location = new System.Drawing.Point(165, 200);
            this.cbGenerateView.Margin = new System.Windows.Forms.Padding(4);
            this.cbGenerateView.Name = "cbGenerateView";
            this.cbGenerateView.Size = new System.Drawing.Size(17, 21);
            this.cbGenerateView.TabIndex = 18;
            this.cbGenerateView.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGenerateView.UseVisualStyleBackColor = false;
            this.cbGenerateView.CheckedChanged += new System.EventHandler(this.cbGenerateView_CheckedChanged);
            // 
            // cbGenerateTable
            // 
            this.cbGenerateTable.BackColor = System.Drawing.Color.Transparent;
            this.cbGenerateTable.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGenerateTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbGenerateTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGenerateTable.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.cbGenerateTable.Location = new System.Drawing.Point(165, 174);
            this.cbGenerateTable.Margin = new System.Windows.Forms.Padding(4);
            this.cbGenerateTable.Name = "cbGenerateTable";
            this.cbGenerateTable.Size = new System.Drawing.Size(17, 21);
            this.cbGenerateTable.TabIndex = 14;
            this.cbGenerateTable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGenerateTable.UseVisualStyleBackColor = false;
            this.cbGenerateTable.CheckedChanged += new System.EventHandler(this.cbGenerateTable_CheckedChanged);
            // 
            // cbGenerateObject
            // 
            this.cbGenerateObject.BackColor = System.Drawing.Color.Transparent;
            this.cbGenerateObject.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGenerateObject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbGenerateObject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGenerateObject.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.cbGenerateObject.Location = new System.Drawing.Point(165, 148);
            this.cbGenerateObject.Margin = new System.Windows.Forms.Padding(4);
            this.cbGenerateObject.Name = "cbGenerateObject";
            this.cbGenerateObject.Size = new System.Drawing.Size(17, 21);
            this.cbGenerateObject.TabIndex = 10;
            this.cbGenerateObject.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGenerateObject.UseVisualStyleBackColor = false;
            this.cbGenerateObject.CheckedChanged += new System.EventHandler(this.cbGenerateObjectType_CheckedChanged);
            // 
            // cbGeneratePackage
            // 
            this.cbGeneratePackage.BackColor = System.Drawing.Color.Transparent;
            this.cbGeneratePackage.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGeneratePackage.FlatAppearance.CheckedBackColor = System.Drawing.Color.Black;
            this.cbGeneratePackage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            this.cbGeneratePackage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbGeneratePackage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGeneratePackage.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.cbGeneratePackage.Location = new System.Drawing.Point(165, 70);
            this.cbGeneratePackage.Margin = new System.Windows.Forms.Padding(4);
            this.cbGeneratePackage.Name = "cbGeneratePackage";
            this.cbGeneratePackage.Size = new System.Drawing.Size(17, 21);
            this.cbGeneratePackage.TabIndex = 3;
            this.cbGeneratePackage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGeneratePackage.UseVisualStyleBackColor = false;
            this.cbGeneratePackage.CheckedChanged += new System.EventHandler(this.cbGeneratePackage_CheckedChanged);
            // 
            // FormMain
            // 
            this.AcceptButton = this.BtnStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1330, 745);
            this.Controls.Add(this.lblGenerateStatus);
            this.Controls.Add(this.gbAdvancedProcOptions);
            this.Controls.Add(this.gbOracleToCSharpCustomTranslation);
            this.Controls.Add(this.gbSettings);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.gbDatabase);
            this.Controls.Add(this.ListViewMessage);
            this.Controls.Add(this.gbCodeToGenerate);
            this.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.gbCodeToGenerate.ResumeLayout(false);
            this.gbCodeToGenerate.PerformLayout();
            this.gbDatabase.ResumeLayout(false);
            this.gbDatabase.PerformLayout();
            this.gbSettings.ResumeLayout(false);
            this.gbOracleToCSharpCustomTranslation.ResumeLayout(false);
            this.gbAdvancedProcOptions.ResumeLayout(false);
            this.gbAdvancedProcOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ListViewMessage;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Label lblDBInstance;
        private System.Windows.Forms.Label lblSchema;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Label lblOutputPath;
        private System.Windows.Forms.TextBox txtSchema;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtOutputPath;
        private OdapterWnFrm.Controls.OdatperCheckBox cbGeneratePackage;
        private System.Windows.Forms.GroupBox gbCodeToGenerate;
        private System.Windows.Forms.Button btnSelectPath;
        private OdapterWnFrm.Controls.OdatperCheckBox cbPartialPackageClasses;
        private System.Windows.Forms.TextBox txtPackageNamespace;
        private System.Windows.Forms.Label lblNamespace;
        private System.Windows.Forms.GroupBox gbDatabase;
        private System.Windows.Forms.TextBox txtProcedureNamespace;
        private System.Windows.Forms.TextBox txtFunctionNamespace;
        private System.Windows.Forms.Label lblClientHome;
        private OdapterWnFrm.Controls.OdapterComboBox cmbClientHome;
        private OdapterWnFrm.Controls.OdapterComboBox cmbDBInstance;
        private System.Windows.Forms.Label lblAncestorClass;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox txtObjectNamespace;
        private OdapterWnFrm.Controls.OdatperCheckBox cbGenerateObject;
        private OdapterWnFrm.Controls.OdatperCheckBox cbGenerateTable;
        private OdapterWnFrm.Controls.OdatperCheckBox cbGenerateView;
        private System.Windows.Forms.TextBox txtViewNamespace;
        private System.Windows.Forms.TextBox txtTableNamespace;
        private System.Windows.Forms.Label lblPartial;
        private System.Windows.Forms.TextBox txtPackageAncestorClass;
        private OdapterWnFrm.Controls.OdatperCheckBox cbGenerateBaseAdapter;
        private OdapterWnFrm.Controls.OdatperCheckBox cbDeployResources;
        private OdapterWnFrm.Controls.OdatperCheckBox cbConvertOracleNumberToIntegerIfColumnNameIsId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label lblBaseNamespace;
        private System.Windows.Forms.TextBox txtBaseNamespace;
        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.GroupBox gbOracleToCSharpCustomTranslation;
        private System.Windows.Forms.Label lblCSharpTypeUsedForOracleInteger;
        private System.Windows.Forms.Label lblCSharpTypeUsedForOracleNumber;
        private System.Windows.Forms.Label lblCSharpTypeUsedForOracleDate;
        private System.Windows.Forms.Label lblCSharpUsedForOracleIntervalDayToSecond;
        private System.Windows.Forms.Label lblCSharpTypeUsedForOracleTimestamp;
        private OdapterWnFrm.Controls.OdapterComboBox cmbCSharpTypeUsedForOracleInteger;
        private OdapterWnFrm.Controls.OdapterComboBox cmbCSharpTypeUsedForOracleNumber;
        private OdapterWnFrm.Controls.OdapterComboBox cmbCSharpTypeUsedForOracleIntervalDayToSecond;
        private OdapterWnFrm.Controls.OdapterComboBox cmbCSharpTypeUsedForOracleTimestamp;
        private OdapterWnFrm.Controls.OdapterComboBox cmbCSharpTypeUsedForOracleDate;
        private System.Windows.Forms.TextBox txtMaxReturnArgStringSize;
        private System.Windows.Forms.Label lblMaxReturnArgStringSize;
        private System.Windows.Forms.TextBox txtMaxAssocArraySize;
        private System.Windows.Forms.Label lblMaxAssocArraySize;
        private System.Windows.Forms.GroupBox gbAdvancedProcOptions;
        private System.Windows.Forms.Button btnRestoreDefaults;
        private System.Windows.Forms.Button btnSaveSettings;
        private OdapterWnFrm.Controls.OdatperCheckBox cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema;
        private System.Windows.Forms.TextBox txtExcludeChars;
        private OdapterWnFrm.Controls.OdatperCheckBox cbExcludeObjectNamesWithSpecificChars;
        private OdapterWnFrm.Controls.OdatperCheckBox cbGeneratedDynamicMethodForTypedCursor;
        private System.Windows.Forms.Label lblSettingsFile;
        private OdapterWnFrm.Controls.OdapterComboBox cmbSettingsFile;
        private System.Windows.Forms.Label lblLocalVariableNameSuffix;
        private System.Windows.Forms.TextBox txtLocalVariableNameSuffix;
        private OdapterWnFrm.Controls.OdapterComboBox cmbCSharpVersion;
        private System.Windows.Forms.Label lblCSharpVersion;
        private OdapterWnFrm.Controls.OdatperCheckBox cbIncludeFilterPrefixInNaming;
        private System.Windows.Forms.Label lblCSharpTypeUsedForOracleBlob;
        private OdapterWnFrm.Controls.OdapterComboBox cmbCSharpTypeUsedForOracleBlob;
        private System.Windows.Forms.Label lblCSharpTypeUsedForOracleClob;
        private OdapterWnFrm.Controls.OdapterComboBox cmbCSharpTypeUsedForOracleClob;
        private System.Windows.Forms.Label lblCSharpTypeUsedForOracleRefCursor;
        private OdapterWnFrm.Controls.OdapterComboBox cmbCSharpTypeUsedForOracleAssociativeArray;
        private System.Windows.Forms.Label lblCSharpTypeUsedForOracleAssociativeArray;
        private OdapterWnFrm.Controls.OdatperCheckBox cbIsSavePassword;
        private OdapterWnFrm.Controls.OdapterComboBox cmbCSharpTypeUsedForOracleTimestampLTZ;
        private OdapterWnFrm.Controls.OdapterComboBox cmbCSharpTypeUsedForOracleTimestampTZ;
        private System.Windows.Forms.Label lblCSharpTypeUsedForOracleTimestampTZ;
        private System.Windows.Forms.Label lblCSharpTypeUsedForOracleTimestampLTZ;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.TextBox txtPackageFileName;
        private System.Windows.Forms.TextBox txtObjectFileName;
        private System.Windows.Forms.TextBox txtViewFileName;
        private System.Windows.Forms.TextBox txtTableFileName;
        private System.Windows.Forms.TextBox txtBaseAdapterFileName;
        private System.Windows.Forms.TextBox txtBaseAdapterNamespace;
        private System.Windows.Forms.Label lblGenerateStatus;
        private System.Windows.Forms.Label lblSavePassword;
        private System.Windows.Forms.Label lblDeployResources;
        private System.Windows.Forms.Label lblDuplicatePackageRecordOriginatingOutsideFilterAndSchema;
        private System.Windows.Forms.Label lblExcludeObjectNamesWithSpecificChars;
        private System.Windows.Forms.Label lblGeneratedDynamicMethodForTypedCursor;
        private System.Windows.Forms.Label lblConvertOracleNumberToIntegerIfColumnNameIsId;
        private System.Windows.Forms.Label lblIncludeFilterPrefixInNaming;
        private System.Windows.Forms.Label lblPackage;
        private System.Windows.Forms.Label lblBaseAdapter;
        private System.Windows.Forms.Label lblObject;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.Label lblView;
        private Controls.OdapterComboBox cmbCSharpTypeUsedForOracleRefCursor;
        private System.Windows.Forms.Label lblDtoInterfaceCategory;
        private Controls.OdapterComboBox cmbDtoInterfaceCategoryRecord;
        private Controls.OdapterComboBox cmbDtoInterfaceCategoryView;
        private Controls.OdapterComboBox cmbDtoInterfaceCategoryTable;
        private Controls.OdapterComboBox cmbDtoInterfaceCategoryObject;
        private System.Windows.Forms.TextBox txtRecordFileName;
        private System.Windows.Forms.TextBox txtRecordNamespace;
        private Controls.OdatperCheckBox cbGenerateRecord;
        private System.Windows.Forms.Label lblRecord;
    }
}

