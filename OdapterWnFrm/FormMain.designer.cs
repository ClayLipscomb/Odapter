
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
            this.cbPartialPOCOs = new System.Windows.Forms.CheckBox();
            this.cbGeneratePackage = new System.Windows.Forms.CheckBox();
            this.cbGenerateProcedure = new System.Windows.Forms.CheckBox();
            this.cbGenerateFunction = new System.Windows.Forms.CheckBox();
            this.gbCodeToGenerate = new System.Windows.Forms.GroupBox();
            this.lblCSharpVersion = new System.Windows.Forms.Label();
            this.cmbCSharpVersion = new System.Windows.Forms.ComboBox();
            this.txtProcedureNamespace = new System.Windows.Forms.TextBox();
            this.txtBaseConnectionClassFunction = new System.Windows.Forms.TextBox();
            this.txtBaseConnectionClassProcedure = new System.Windows.Forms.TextBox();
            this.txtFunctionNamespace = new System.Windows.Forms.TextBox();
            this.txtDataContractNamespace = new System.Windows.Forms.TextBox();
            this.lblDataContractNamespace = new System.Windows.Forms.Label();
            this.cbDataContractView = new System.Windows.Forms.CheckBox();
            this.cbDataContractTable = new System.Windows.Forms.CheckBox();
            this.cbDataContractObjectType = new System.Windows.Forms.CheckBox();
            this.cbDataContractPackageRecord = new System.Windows.Forms.CheckBox();
            this.cbXmlElementView = new System.Windows.Forms.CheckBox();
            this.cbXmlElementTable = new System.Windows.Forms.CheckBox();
            this.cbXmlElementPackageRecord = new System.Windows.Forms.CheckBox();
            this.cbXmlElementObjectType = new System.Windows.Forms.CheckBox();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBaseNamespace = new System.Windows.Forms.Label();
            this.txtBaseNamespace = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBaseViewClass = new System.Windows.Forms.TextBox();
            this.txtBaseTableClass = new System.Windows.Forms.TextBox();
            this.txtBaseObjectTypeClass = new System.Windows.Forms.TextBox();
            this.cbGenerateBaseDtoClasses = new System.Windows.Forms.CheckBox();
            this.cbGenerateBaseAdapterClass = new System.Windows.Forms.CheckBox();
            this.txtBasePackageClass = new System.Windows.Forms.TextBox();
            this.txtBaseRecordTypeClass = new System.Windows.Forms.TextBox();
            this.cbPartialObjectTypes = new System.Windows.Forms.CheckBox();
            this.cbPartialViews = new System.Windows.Forms.CheckBox();
            this.cbPartialTables = new System.Windows.Forms.CheckBox();
            this.lblBaseClassName = new System.Windows.Forms.Label();
            this.lblPartial = new System.Windows.Forms.Label();
            this.lblSerializable = new System.Windows.Forms.Label();
            this.txtViewNamespace = new System.Windows.Forms.TextBox();
            this.txtTableNamespace = new System.Windows.Forms.TextBox();
            this.cbPartialPackageClasses = new System.Windows.Forms.CheckBox();
            this.txtObjectTypeNamespace = new System.Windows.Forms.TextBox();
            this.txtPackageNamespace = new System.Windows.Forms.TextBox();
            this.txtRecordTypeNamespace = new System.Windows.Forms.TextBox();
            this.lblFacadeNamespace = new System.Windows.Forms.Label();
            this.cbIncludeFilterPrefixInNaming = new System.Windows.Forms.CheckBox();
            this.cbDeployResources = new System.Windows.Forms.CheckBox();
            this.cbSerializableViews = new System.Windows.Forms.CheckBox();
            this.cbSerializableTables = new System.Windows.Forms.CheckBox();
            this.cbSerializableObjectTypes = new System.Windows.Forms.CheckBox();
            this.cbGenerateView = new System.Windows.Forms.CheckBox();
            this.cbGenerateRecordType = new System.Windows.Forms.CheckBox();
            this.cbGenerateTable = new System.Windows.Forms.CheckBox();
            this.cbSerializablePOCOs = new System.Windows.Forms.CheckBox();
            this.cbGenerateObjectType = new System.Windows.Forms.CheckBox();
            this.txtMaxReturnArgStringSize = new System.Windows.Forms.TextBox();
            this.lblMaxReturnArgStringSize = new System.Windows.Forms.Label();
            this.txtMaxAssocArraySize = new System.Windows.Forms.TextBox();
            this.lblMaxAssocArraySize = new System.Windows.Forms.Label();
            this.lblFilter = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.cbConvertOracleNumberToIntegerIfColumnNameIsId = new System.Windows.Forms.CheckBox();
            this.gbDatabase = new System.Windows.Forms.GroupBox();
            this.cmbDBInstance = new System.Windows.Forms.ComboBox();
            this.cmbClientHome = new System.Windows.Forms.ComboBox();
            this.lblClientHome = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema = new System.Windows.Forms.CheckBox();
            this.lblCSharpTypeUsedForOracleNumber = new System.Windows.Forms.Label();
            this.lblCSharpTypeUsedForOracleInteger = new System.Windows.Forms.Label();
            this.cbExcludeObjectNamesWithSpecificChars = new System.Windows.Forms.CheckBox();
            this.cbGeneratedDynamicMethodForTypedCursor = new System.Windows.Forms.CheckBox();
            this.cbUseAutoImplementedProperties = new System.Windows.Forms.CheckBox();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.lblCSharpTypeUsedForOracleTimestamp = new System.Windows.Forms.Label();
            this.lblCSharpUsedForOracleIntervalDayToSecond = new System.Windows.Forms.Label();
            this.lblCSharpTypeUsedForOracleDate = new System.Windows.Forms.Label();
            this.lblLocalVariableNameSuffix = new System.Windows.Forms.Label();
            this.lblSettingsFile = new System.Windows.Forms.Label();
            this.lblCSharpTypeUsedForOracleBlob = new System.Windows.Forms.Label();
            this.lblCSharpTypeUsedForOracleClob = new System.Windows.Forms.Label();
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.cmbSettingsFile = new System.Windows.Forms.ComboBox();
            this.btnRestoreDefaults = new System.Windows.Forms.Button();
            this.gbOracleToCSharpCustomTranslation = new System.Windows.Forms.GroupBox();
            this.cmbCSharpTypeUsedForOracleClob = new System.Windows.Forms.ComboBox();
            this.cmbCSharpTypeUsedForOracleBlob = new System.Windows.Forms.ComboBox();
            this.cmbCSharpTypeUsedForOracleIntervalDayToSecond = new System.Windows.Forms.ComboBox();
            this.cmbCSharpTypeUsedForOracleTimestamp = new System.Windows.Forms.ComboBox();
            this.cmbCSharpTypeUsedForOracleDate = new System.Windows.Forms.ComboBox();
            this.cmbCSharpTypeUsedForOracleNumber = new System.Windows.Forms.ComboBox();
            this.cmbCSharpTypeUsedForOracleInteger = new System.Windows.Forms.ComboBox();
            this.gbAdvancedProcOptions = new System.Windows.Forms.GroupBox();
            this.txtLocalVariableNameSuffix = new System.Windows.Forms.TextBox();
            this.txtExcludeChars = new System.Windows.Forms.TextBox();
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
            this.ListViewMessage.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.ListViewMessage.Location = new System.Drawing.Point(828, 10);
            this.ListViewMessage.Margin = new System.Windows.Forms.Padding(4);
            this.ListViewMessage.Name = "ListViewMessage";
            this.ListViewMessage.Size = new System.Drawing.Size(364, 408);
            this.ListViewMessage.TabIndex = 7;
            this.ListViewMessage.UseCompatibleStateImageBehavior = false;
            this.ListViewMessage.View = System.Windows.Forms.View.Details;
            // 
            // BtnStart
            // 
            this.BtnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnStart.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.BtnStart.Location = new System.Drawing.Point(715, 37);
            this.BtnStart.Margin = new System.Windows.Forms.Padding(4);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(101, 51);
            this.BtnStart.TabIndex = 1;
            this.BtnStart.Text = "Generate Code";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.btnStart_Clicked);
            // 
            // lblDBInstance
            // 
            this.lblDBInstance.AutoSize = true;
            this.lblDBInstance.BackColor = System.Drawing.Color.Transparent;
            this.lblDBInstance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBInstance.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblDBInstance.Location = new System.Drawing.Point(368, 21);
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
            this.lblSchema.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblSchema.Location = new System.Drawing.Point(30, 50);
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
            this.lblPassword.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblPassword.Location = new System.Drawing.Point(360, 77);
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
            this.lblLogin.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblLogin.Location = new System.Drawing.Point(47, 77);
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
            this.lblOutputPath.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblOutputPath.Location = new System.Drawing.Point(1, 21);
            this.lblOutputPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOutputPath.Name = "lblOutputPath";
            this.lblOutputPath.Size = new System.Drawing.Size(120, 16);
            this.lblOutputPath.TabIndex = 0;
            this.lblOutputPath.Text = "* Output Path";
            this.lblOutputPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblOutputPath, "The project path in which C# files will be generated.");
            // 
            // txtSchema
            // 
            this.txtSchema.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSchema.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSchema.Location = new System.Drawing.Point(111, 46);
            this.txtSchema.Margin = new System.Windows.Forms.Padding(4);
            this.txtSchema.Name = "txtSchema";
            this.txtSchema.Size = new System.Drawing.Size(232, 23);
            this.txtSchema.TabIndex = 3;
            this.txtSchema.TextChanged += new System.EventHandler(this.txtSchema_TextChanged);
            this.txtSchema.Leave += new System.EventHandler(this.txtSchema_Leave);
            // 
            // txtLogin
            // 
            this.txtLogin.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogin.Location = new System.Drawing.Point(112, 73);
            this.txtLogin.Margin = new System.Windows.Forms.Padding(4);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(232, 23);
            this.txtLogin.TabIndex = 5;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(455, 73);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(223, 23);
            this.txtPassword.TabIndex = 6;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutputPath.Location = new System.Drawing.Point(124, 16);
            this.txtOutputPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(613, 23);
            this.txtOutputPath.TabIndex = 0;
            // 
            // cbPartialPOCOs
            // 
            this.cbPartialPOCOs.AutoSize = true;
            this.cbPartialPOCOs.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbPartialPOCOs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPartialPOCOs.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbPartialPOCOs.Location = new System.Drawing.Point(460, 98);
            this.cbPartialPOCOs.Margin = new System.Windows.Forms.Padding(4);
            this.cbPartialPOCOs.Name = "cbPartialPOCOs";
            this.cbPartialPOCOs.Size = new System.Drawing.Size(18, 17);
            this.cbPartialPOCOs.TabIndex = 8;
            this.cbPartialPOCOs.UseVisualStyleBackColor = true;
            this.cbPartialPOCOs.CheckedChanged += new System.EventHandler(this.cbPartialPOCOs_CheckedChanged);
            // 
            // cbGeneratePackage
            // 
            this.cbGeneratePackage.AutoSize = true;
            this.cbGeneratePackage.BackColor = System.Drawing.Color.Transparent;
            this.cbGeneratePackage.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGeneratePackage.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.cbGeneratePackage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGeneratePackage.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbGeneratePackage.Location = new System.Drawing.Point(33, 70);
            this.cbGeneratePackage.Margin = new System.Windows.Forms.Padding(4);
            this.cbGeneratePackage.Name = "cbGeneratePackage";
            this.cbGeneratePackage.Size = new System.Drawing.Size(162, 21);
            this.cbGeneratePackage.TabIndex = 2;
            this.cbGeneratePackage.Text = "Package Adapters";
            this.cbGeneratePackage.UseVisualStyleBackColor = false;
            this.cbGeneratePackage.CheckedChanged += new System.EventHandler(this.cbGeneratePackage_CheckedChanged);
            // 
            // cbGenerateProcedure
            // 
            this.cbGenerateProcedure.AutoSize = true;
            this.cbGenerateProcedure.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGenerateProcedure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGenerateProcedure.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbGenerateProcedure.Location = new System.Drawing.Point(17, 780);
            this.cbGenerateProcedure.Margin = new System.Windows.Forms.Padding(4);
            this.cbGenerateProcedure.Name = "cbGenerateProcedure";
            this.cbGenerateProcedure.Size = new System.Drawing.Size(175, 21);
            this.cbGenerateProcedure.TabIndex = 4;
            this.cbGenerateProcedure.Text = "Procedures Adapter";
            this.cbGenerateProcedure.UseVisualStyleBackColor = true;
            // 
            // cbGenerateFunction
            // 
            this.cbGenerateFunction.AutoSize = true;
            this.cbGenerateFunction.BackColor = System.Drawing.Color.Transparent;
            this.cbGenerateFunction.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGenerateFunction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGenerateFunction.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbGenerateFunction.Location = new System.Drawing.Point(29, 807);
            this.cbGenerateFunction.Margin = new System.Windows.Forms.Padding(4);
            this.cbGenerateFunction.Name = "cbGenerateFunction";
            this.cbGenerateFunction.Size = new System.Drawing.Size(162, 21);
            this.cbGenerateFunction.TabIndex = 5;
            this.cbGenerateFunction.Text = "Functions Adapter";
            this.cbGenerateFunction.UseVisualStyleBackColor = false;
            // 
            // gbCodeToGenerate
            // 
            this.gbCodeToGenerate.BackColor = System.Drawing.Color.Transparent;
            this.gbCodeToGenerate.Controls.Add(this.lblCSharpVersion);
            this.gbCodeToGenerate.Controls.Add(this.cmbCSharpVersion);
            this.gbCodeToGenerate.Controls.Add(this.txtProcedureNamespace);
            this.gbCodeToGenerate.Controls.Add(this.txtBaseConnectionClassFunction);
            this.gbCodeToGenerate.Controls.Add(this.txtBaseConnectionClassProcedure);
            this.gbCodeToGenerate.Controls.Add(this.txtFunctionNamespace);
            this.gbCodeToGenerate.Controls.Add(this.txtDataContractNamespace);
            this.gbCodeToGenerate.Controls.Add(this.lblDataContractNamespace);
            this.gbCodeToGenerate.Controls.Add(this.cbDataContractView);
            this.gbCodeToGenerate.Controls.Add(this.cbDataContractTable);
            this.gbCodeToGenerate.Controls.Add(this.cbDataContractObjectType);
            this.gbCodeToGenerate.Controls.Add(this.cbDataContractPackageRecord);
            this.gbCodeToGenerate.Controls.Add(this.cbXmlElementView);
            this.gbCodeToGenerate.Controls.Add(this.cbXmlElementTable);
            this.gbCodeToGenerate.Controls.Add(this.cbXmlElementPackageRecord);
            this.gbCodeToGenerate.Controls.Add(this.cbXmlElementObjectType);
            this.gbCodeToGenerate.Controls.Add(this.btnSelectPath);
            this.gbCodeToGenerate.Controls.Add(this.label2);
            this.gbCodeToGenerate.Controls.Add(this.txtOutputPath);
            this.gbCodeToGenerate.Controls.Add(this.lblOutputPath);
            this.gbCodeToGenerate.Controls.Add(this.label1);
            this.gbCodeToGenerate.Controls.Add(this.lblBaseNamespace);
            this.gbCodeToGenerate.Controls.Add(this.txtBaseNamespace);
            this.gbCodeToGenerate.Controls.Add(this.label9);
            this.gbCodeToGenerate.Controls.Add(this.txtBaseViewClass);
            this.gbCodeToGenerate.Controls.Add(this.txtBaseTableClass);
            this.gbCodeToGenerate.Controls.Add(this.txtBaseObjectTypeClass);
            this.gbCodeToGenerate.Controls.Add(this.cbGenerateBaseDtoClasses);
            this.gbCodeToGenerate.Controls.Add(this.cbGenerateBaseAdapterClass);
            this.gbCodeToGenerate.Controls.Add(this.txtBasePackageClass);
            this.gbCodeToGenerate.Controls.Add(this.txtBaseRecordTypeClass);
            this.gbCodeToGenerate.Controls.Add(this.cbPartialObjectTypes);
            this.gbCodeToGenerate.Controls.Add(this.cbPartialViews);
            this.gbCodeToGenerate.Controls.Add(this.cbPartialTables);
            this.gbCodeToGenerate.Controls.Add(this.lblBaseClassName);
            this.gbCodeToGenerate.Controls.Add(this.lblPartial);
            this.gbCodeToGenerate.Controls.Add(this.lblSerializable);
            this.gbCodeToGenerate.Controls.Add(this.txtViewNamespace);
            this.gbCodeToGenerate.Controls.Add(this.txtTableNamespace);
            this.gbCodeToGenerate.Controls.Add(this.cbPartialPOCOs);
            this.gbCodeToGenerate.Controls.Add(this.cbPartialPackageClasses);
            this.gbCodeToGenerate.Controls.Add(this.txtObjectTypeNamespace);
            this.gbCodeToGenerate.Controls.Add(this.txtPackageNamespace);
            this.gbCodeToGenerate.Controls.Add(this.txtRecordTypeNamespace);
            this.gbCodeToGenerate.Controls.Add(this.lblFacadeNamespace);
            this.gbCodeToGenerate.Controls.Add(this.cbIncludeFilterPrefixInNaming);
            this.gbCodeToGenerate.Controls.Add(this.cbGenerateFunction);
            this.gbCodeToGenerate.Controls.Add(this.cbGenerateProcedure);
            this.gbCodeToGenerate.Controls.Add(this.cbDeployResources);
            this.gbCodeToGenerate.Controls.Add(this.cbSerializableViews);
            this.gbCodeToGenerate.Controls.Add(this.cbSerializableTables);
            this.gbCodeToGenerate.Controls.Add(this.cbSerializableObjectTypes);
            this.gbCodeToGenerate.Controls.Add(this.cbGenerateView);
            this.gbCodeToGenerate.Controls.Add(this.cbGenerateRecordType);
            this.gbCodeToGenerate.Controls.Add(this.cbGenerateTable);
            this.gbCodeToGenerate.Controls.Add(this.cbSerializablePOCOs);
            this.gbCodeToGenerate.Controls.Add(this.cbGenerateObjectType);
            this.gbCodeToGenerate.Controls.Add(this.cbGeneratePackage);
            this.gbCodeToGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbCodeToGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbCodeToGenerate.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.gbCodeToGenerate.Location = new System.Drawing.Point(16, 114);
            this.gbCodeToGenerate.Margin = new System.Windows.Forms.Padding(4);
            this.gbCodeToGenerate.Name = "gbCodeToGenerate";
            this.gbCodeToGenerate.Padding = new System.Windows.Forms.Padding(4);
            this.gbCodeToGenerate.Size = new System.Drawing.Size(804, 304);
            this.gbCodeToGenerate.TabIndex = 2;
            this.gbCodeToGenerate.TabStop = false;
            this.gbCodeToGenerate.Text = "Code to Generate";
            // 
            // lblCSharpVersion
            // 
            this.lblCSharpVersion.AutoSize = true;
            this.lblCSharpVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSharpVersion.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblCSharpVersion.Location = new System.Drawing.Point(124, 226);
            this.lblCSharpVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCSharpVersion.Name = "lblCSharpVersion";
            this.lblCSharpVersion.Size = new System.Drawing.Size(87, 17);
            this.lblCSharpVersion.TabIndex = 39;
            this.lblCSharpVersion.Text = "C# Version";
            this.lblCSharpVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCSharpVersion
            // 
            this.cmbCSharpVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCSharpVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCSharpVersion.FormattingEnabled = true;
            this.cmbCSharpVersion.Location = new System.Drawing.Point(217, 221);
            this.cmbCSharpVersion.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCSharpVersion.Name = "cmbCSharpVersion";
            this.cmbCSharpVersion.Size = new System.Drawing.Size(64, 25);
            this.cmbCSharpVersion.TabIndex = 35;
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
            // txtBaseConnectionClassFunction
            // 
            this.txtBaseConnectionClassFunction.Enabled = false;
            this.txtBaseConnectionClassFunction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBaseConnectionClassFunction.Location = new System.Drawing.Point(639, 577);
            this.txtBaseConnectionClassFunction.Margin = new System.Windows.Forms.Padding(4);
            this.txtBaseConnectionClassFunction.Name = "txtBaseConnectionClassFunction";
            this.txtBaseConnectionClassFunction.Size = new System.Drawing.Size(219, 23);
            this.txtBaseConnectionClassFunction.TabIndex = 45;
            // 
            // txtBaseConnectionClassProcedure
            // 
            this.txtBaseConnectionClassProcedure.Enabled = false;
            this.txtBaseConnectionClassProcedure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBaseConnectionClassProcedure.Location = new System.Drawing.Point(639, 551);
            this.txtBaseConnectionClassProcedure.Margin = new System.Windows.Forms.Padding(4);
            this.txtBaseConnectionClassProcedure.Name = "txtBaseConnectionClassProcedure";
            this.txtBaseConnectionClassProcedure.Size = new System.Drawing.Size(219, 23);
            this.txtBaseConnectionClassProcedure.TabIndex = 44;
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
            // txtDataContractNamespace
            // 
            this.txtDataContractNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataContractNamespace.Location = new System.Drawing.Point(217, 273);
            this.txtDataContractNamespace.Margin = new System.Windows.Forms.Padding(4);
            this.txtDataContractNamespace.Name = "txtDataContractNamespace";
            this.txtDataContractNamespace.Size = new System.Drawing.Size(261, 23);
            this.txtDataContractNamespace.TabIndex = 39;
            // 
            // lblDataContractNamespace
            // 
            this.lblDataContractNamespace.AutoSize = true;
            this.lblDataContractNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataContractNamespace.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblDataContractNamespace.Location = new System.Drawing.Point(17, 278);
            this.lblDataContractNamespace.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDataContractNamespace.Name = "lblDataContractNamespace";
            this.lblDataContractNamespace.Size = new System.Drawing.Size(192, 17);
            this.lblDataContractNamespace.TabIndex = 63;
            this.lblDataContractNamespace.Text = "DataContract Namespace";
            this.lblDataContractNamespace.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbDataContractView
            // 
            this.cbDataContractView.AutoSize = true;
            this.cbDataContractView.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbDataContractView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDataContractView.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbDataContractView.Location = new System.Drawing.Point(547, 175);
            this.cbDataContractView.Margin = new System.Windows.Forms.Padding(4);
            this.cbDataContractView.Name = "cbDataContractView";
            this.cbDataContractView.Size = new System.Drawing.Size(18, 17);
            this.cbDataContractView.TabIndex = 31;
            this.cbDataContractView.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbDataContractView.UseVisualStyleBackColor = true;
            // 
            // cbDataContractTable
            // 
            this.cbDataContractTable.AutoSize = true;
            this.cbDataContractTable.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbDataContractTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDataContractTable.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbDataContractTable.Location = new System.Drawing.Point(547, 149);
            this.cbDataContractTable.Margin = new System.Windows.Forms.Padding(4);
            this.cbDataContractTable.Name = "cbDataContractTable";
            this.cbDataContractTable.Size = new System.Drawing.Size(18, 17);
            this.cbDataContractTable.TabIndex = 24;
            this.cbDataContractTable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbDataContractTable.UseVisualStyleBackColor = true;
            // 
            // cbDataContractObjectType
            // 
            this.cbDataContractObjectType.AutoSize = true;
            this.cbDataContractObjectType.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbDataContractObjectType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDataContractObjectType.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbDataContractObjectType.Location = new System.Drawing.Point(547, 123);
            this.cbDataContractObjectType.Margin = new System.Windows.Forms.Padding(4);
            this.cbDataContractObjectType.Name = "cbDataContractObjectType";
            this.cbDataContractObjectType.Size = new System.Drawing.Size(18, 17);
            this.cbDataContractObjectType.TabIndex = 17;
            this.cbDataContractObjectType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbDataContractObjectType.UseVisualStyleBackColor = true;
            // 
            // cbDataContractPackageRecord
            // 
            this.cbDataContractPackageRecord.AutoSize = true;
            this.cbDataContractPackageRecord.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbDataContractPackageRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDataContractPackageRecord.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbDataContractPackageRecord.Location = new System.Drawing.Point(547, 98);
            this.cbDataContractPackageRecord.Margin = new System.Windows.Forms.Padding(4);
            this.cbDataContractPackageRecord.Name = "cbDataContractPackageRecord";
            this.cbDataContractPackageRecord.Size = new System.Drawing.Size(18, 17);
            this.cbDataContractPackageRecord.TabIndex = 10;
            this.cbDataContractPackageRecord.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbDataContractPackageRecord.UseVisualStyleBackColor = true;
            this.cbDataContractPackageRecord.CheckedChanged += new System.EventHandler(this.cbDataContractPackageRecord_CheckedChanged);
            // 
            // cbXmlElementView
            // 
            this.cbXmlElementView.AutoSize = true;
            this.cbXmlElementView.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbXmlElementView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbXmlElementView.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbXmlElementView.Location = new System.Drawing.Point(591, 175);
            this.cbXmlElementView.Margin = new System.Windows.Forms.Padding(4);
            this.cbXmlElementView.Name = "cbXmlElementView";
            this.cbXmlElementView.Size = new System.Drawing.Size(18, 17);
            this.cbXmlElementView.TabIndex = 32;
            this.cbXmlElementView.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbXmlElementView.UseVisualStyleBackColor = true;
            this.cbXmlElementView.CheckedChanged += new System.EventHandler(this.cbXmlElementView_CheckedChanged);
            // 
            // cbXmlElementTable
            // 
            this.cbXmlElementTable.AutoSize = true;
            this.cbXmlElementTable.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbXmlElementTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbXmlElementTable.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbXmlElementTable.Location = new System.Drawing.Point(591, 149);
            this.cbXmlElementTable.Margin = new System.Windows.Forms.Padding(4);
            this.cbXmlElementTable.Name = "cbXmlElementTable";
            this.cbXmlElementTable.Size = new System.Drawing.Size(18, 17);
            this.cbXmlElementTable.TabIndex = 25;
            this.cbXmlElementTable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbXmlElementTable.UseVisualStyleBackColor = true;
            this.cbXmlElementTable.CheckedChanged += new System.EventHandler(this.cbXmlElementTable_CheckedChanged);
            // 
            // cbXmlElementPackageRecord
            // 
            this.cbXmlElementPackageRecord.AutoSize = true;
            this.cbXmlElementPackageRecord.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbXmlElementPackageRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbXmlElementPackageRecord.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbXmlElementPackageRecord.Location = new System.Drawing.Point(591, 98);
            this.cbXmlElementPackageRecord.Margin = new System.Windows.Forms.Padding(4);
            this.cbXmlElementPackageRecord.Name = "cbXmlElementPackageRecord";
            this.cbXmlElementPackageRecord.Size = new System.Drawing.Size(18, 17);
            this.cbXmlElementPackageRecord.TabIndex = 11;
            this.cbXmlElementPackageRecord.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbXmlElementPackageRecord.UseVisualStyleBackColor = true;
            this.cbXmlElementPackageRecord.CheckedChanged += new System.EventHandler(this.cbXmlElementPackageRecord_CheckedChanged);
            // 
            // cbXmlElementObjectType
            // 
            this.cbXmlElementObjectType.AutoSize = true;
            this.cbXmlElementObjectType.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbXmlElementObjectType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbXmlElementObjectType.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbXmlElementObjectType.Location = new System.Drawing.Point(591, 123);
            this.cbXmlElementObjectType.Margin = new System.Windows.Forms.Padding(4);
            this.cbXmlElementObjectType.Name = "cbXmlElementObjectType";
            this.cbXmlElementObjectType.Size = new System.Drawing.Size(18, 17);
            this.cbXmlElementObjectType.TabIndex = 18;
            this.cbXmlElementObjectType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbXmlElementObjectType.UseVisualStyleBackColor = true;
            this.cbXmlElementObjectType.CheckedChanged += new System.EventHandler(this.cbXmlElementObjectType_CheckedChanged);
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Location = new System.Drawing.Point(747, 16);
            this.btnSelectPath.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(35, 25);
            this.btnSelectPath.TabIndex = 1;
            this.btnSelectPath.Text = "...";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(537, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 56;
            this.label2.Text = "DC?";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.label2, "Generate DataContract attribute and DataMember attributes with Order and IsRequir" +
        "ed parameters.");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(580, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 17);
            this.label1.TabIndex = 55;
            this.label1.Text = "XE?";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.label1, "Generate XmlElementAttribute with Order and IsNullable(true) parameters.");
            // 
            // lblBaseNamespace
            // 
            this.lblBaseNamespace.AutoSize = true;
            this.lblBaseNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBaseNamespace.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblBaseNamespace.Location = new System.Drawing.Point(68, 252);
            this.lblBaseNamespace.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBaseNamespace.Name = "lblBaseNamespace";
            this.lblBaseNamespace.Size = new System.Drawing.Size(144, 17);
            this.lblBaseNamespace.TabIndex = 52;
            this.lblBaseNamespace.Text = "* Base Namespace";
            this.lblBaseNamespace.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBaseNamespace
            // 
            this.txtBaseNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBaseNamespace.Location = new System.Drawing.Point(217, 247);
            this.txtBaseNamespace.Margin = new System.Windows.Forms.Padding(4);
            this.txtBaseNamespace.Name = "txtBaseNamespace";
            this.txtBaseNamespace.Size = new System.Drawing.Size(261, 23);
            this.txtBaseNamespace.TabIndex = 37;
            this.txtBaseNamespace.TextChanged += new System.EventHandler(this.txtBaseNamespace_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label9.Location = new System.Drawing.Point(167, 48);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 17);
            this.label9.TabIndex = 50;
            this.label9.Text = "Gen?";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.label9, "Generate code.");
            // 
            // txtBaseViewClass
            // 
            this.txtBaseViewClass.Enabled = false;
            this.txtBaseViewClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBaseViewClass.Location = new System.Drawing.Point(625, 171);
            this.txtBaseViewClass.Margin = new System.Windows.Forms.Padding(4);
            this.txtBaseViewClass.Name = "txtBaseViewClass";
            this.txtBaseViewClass.Size = new System.Drawing.Size(164, 23);
            this.txtBaseViewClass.TabIndex = 33;
            // 
            // txtBaseTableClass
            // 
            this.txtBaseTableClass.Enabled = false;
            this.txtBaseTableClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBaseTableClass.Location = new System.Drawing.Point(625, 145);
            this.txtBaseTableClass.Margin = new System.Windows.Forms.Padding(4);
            this.txtBaseTableClass.Name = "txtBaseTableClass";
            this.txtBaseTableClass.Size = new System.Drawing.Size(164, 23);
            this.txtBaseTableClass.TabIndex = 26;
            // 
            // txtBaseObjectTypeClass
            // 
            this.txtBaseObjectTypeClass.Enabled = false;
            this.txtBaseObjectTypeClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBaseObjectTypeClass.Location = new System.Drawing.Point(625, 119);
            this.txtBaseObjectTypeClass.Margin = new System.Windows.Forms.Padding(4);
            this.txtBaseObjectTypeClass.Name = "txtBaseObjectTypeClass";
            this.txtBaseObjectTypeClass.Size = new System.Drawing.Size(164, 23);
            this.txtBaseObjectTypeClass.TabIndex = 19;
            // 
            // cbGenerateBaseDtoClasses
            // 
            this.cbGenerateBaseDtoClasses.AutoSize = true;
            this.cbGenerateBaseDtoClasses.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGenerateBaseDtoClasses.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGenerateBaseDtoClasses.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbGenerateBaseDtoClasses.Location = new System.Drawing.Point(565, 250);
            this.cbGenerateBaseDtoClasses.Margin = new System.Windows.Forms.Padding(4);
            this.cbGenerateBaseDtoClasses.Name = "cbGenerateBaseDtoClasses";
            this.cbGenerateBaseDtoClasses.Size = new System.Drawing.Size(176, 21);
            this.cbGenerateBaseDtoClasses.TabIndex = 38;
            this.cbGenerateBaseDtoClasses.Text = "Deploy Base DTOs?";
            this.cbGenerateBaseDtoClasses.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.cbGenerateBaseDtoClasses, "Ancestor classes for package record type, object type, table and view DTOs. Only " +
        "needs to be deployed once.");
            this.cbGenerateBaseDtoClasses.UseVisualStyleBackColor = true;
            // 
            // cbGenerateBaseAdapterClass
            // 
            this.cbGenerateBaseAdapterClass.AutoSize = true;
            this.cbGenerateBaseAdapterClass.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGenerateBaseAdapterClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGenerateBaseAdapterClass.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbGenerateBaseAdapterClass.Location = new System.Drawing.Point(549, 224);
            this.cbGenerateBaseAdapterClass.Margin = new System.Windows.Forms.Padding(4);
            this.cbGenerateBaseAdapterClass.Name = "cbGenerateBaseAdapterClass";
            this.cbGenerateBaseAdapterClass.Size = new System.Drawing.Size(192, 21);
            this.cbGenerateBaseAdapterClass.TabIndex = 36;
            this.cbGenerateBaseAdapterClass.Text = "Deploy Base Adapter?";
            this.cbGenerateBaseAdapterClass.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.cbGenerateBaseAdapterClass, "Ancestor class for all package adapters. Only needs to be deployed once.");
            this.cbGenerateBaseAdapterClass.UseVisualStyleBackColor = true;
            this.cbGenerateBaseAdapterClass.CheckedChanged += new System.EventHandler(this.cbGenerateBaseAdapterClass_CheckedChanged);
            // 
            // txtBasePackageClass
            // 
            this.txtBasePackageClass.Enabled = false;
            this.txtBasePackageClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBasePackageClass.Location = new System.Drawing.Point(625, 68);
            this.txtBasePackageClass.Margin = new System.Windows.Forms.Padding(4);
            this.txtBasePackageClass.Name = "txtBasePackageClass";
            this.txtBasePackageClass.Size = new System.Drawing.Size(164, 23);
            this.txtBasePackageClass.TabIndex = 5;
            this.txtBasePackageClass.TextChanged += new System.EventHandler(this.txtBaseConnectionClass_TextChanged);
            // 
            // txtBaseRecordTypeClass
            // 
            this.txtBaseRecordTypeClass.Enabled = false;
            this.txtBaseRecordTypeClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBaseRecordTypeClass.Location = new System.Drawing.Point(625, 94);
            this.txtBaseRecordTypeClass.Margin = new System.Windows.Forms.Padding(4);
            this.txtBaseRecordTypeClass.Name = "txtBaseRecordTypeClass";
            this.txtBaseRecordTypeClass.Size = new System.Drawing.Size(164, 23);
            this.txtBaseRecordTypeClass.TabIndex = 12;
            // 
            // cbPartialObjectTypes
            // 
            this.cbPartialObjectTypes.AutoSize = true;
            this.cbPartialObjectTypes.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbPartialObjectTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPartialObjectTypes.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbPartialObjectTypes.Location = new System.Drawing.Point(460, 123);
            this.cbPartialObjectTypes.Margin = new System.Windows.Forms.Padding(4);
            this.cbPartialObjectTypes.Name = "cbPartialObjectTypes";
            this.cbPartialObjectTypes.Size = new System.Drawing.Size(18, 17);
            this.cbPartialObjectTypes.TabIndex = 15;
            this.cbPartialObjectTypes.UseVisualStyleBackColor = true;
            // 
            // cbPartialViews
            // 
            this.cbPartialViews.AutoSize = true;
            this.cbPartialViews.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbPartialViews.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPartialViews.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbPartialViews.Location = new System.Drawing.Point(460, 175);
            this.cbPartialViews.Margin = new System.Windows.Forms.Padding(4);
            this.cbPartialViews.Name = "cbPartialViews";
            this.cbPartialViews.Size = new System.Drawing.Size(18, 17);
            this.cbPartialViews.TabIndex = 29;
            this.cbPartialViews.UseVisualStyleBackColor = true;
            // 
            // cbPartialTables
            // 
            this.cbPartialTables.AutoSize = true;
            this.cbPartialTables.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbPartialTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPartialTables.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbPartialTables.Location = new System.Drawing.Point(460, 149);
            this.cbPartialTables.Margin = new System.Windows.Forms.Padding(4);
            this.cbPartialTables.Name = "cbPartialTables";
            this.cbPartialTables.Size = new System.Drawing.Size(18, 17);
            this.cbPartialTables.TabIndex = 22;
            this.cbPartialTables.UseVisualStyleBackColor = true;
            // 
            // lblBaseClassName
            // 
            this.lblBaseClassName.AutoSize = true;
            this.lblBaseClassName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBaseClassName.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblBaseClassName.Location = new System.Drawing.Point(647, 48);
            this.lblBaseClassName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBaseClassName.Name = "lblBaseClassName";
            this.lblBaseClassName.Size = new System.Drawing.Size(116, 17);
            this.lblBaseClassName.TabIndex = 20;
            this.lblBaseClassName.Text = "Ancestor Class";
            this.lblBaseClassName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblBaseClassName, "Each base class will be inherited in generated code regardless of whether the cla" +
        "ss is generated.");
            // 
            // lblPartial
            // 
            this.lblPartial.AutoSize = true;
            this.lblPartial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartial.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblPartial.Location = new System.Drawing.Point(448, 48);
            this.lblPartial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPartial.Name = "lblPartial";
            this.lblPartial.Size = new System.Drawing.Size(42, 17);
            this.lblPartial.TabIndex = 38;
            this.lblPartial.Text = "Prtl?";
            this.lblPartial.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblPartial, "Make classes partial.");
            // 
            // lblSerializable
            // 
            this.lblSerializable.AutoSize = true;
            this.lblSerializable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerializable.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblSerializable.Location = new System.Drawing.Point(491, 48);
            this.lblSerializable.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSerializable.Name = "lblSerializable";
            this.lblSerializable.Size = new System.Drawing.Size(45, 17);
            this.lblSerializable.TabIndex = 37;
            this.lblSerializable.Text = "Srlz?";
            this.lblSerializable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblSerializable, "Add Serializable attribute to classes.");
            // 
            // txtViewNamespace
            // 
            this.txtViewNamespace.Enabled = false;
            this.txtViewNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViewNamespace.Location = new System.Drawing.Point(209, 171);
            this.txtViewNamespace.Margin = new System.Windows.Forms.Padding(4);
            this.txtViewNamespace.Name = "txtViewNamespace";
            this.txtViewNamespace.Size = new System.Drawing.Size(235, 23);
            this.txtViewNamespace.TabIndex = 28;
            // 
            // txtTableNamespace
            // 
            this.txtTableNamespace.Enabled = false;
            this.txtTableNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTableNamespace.Location = new System.Drawing.Point(209, 145);
            this.txtTableNamespace.Margin = new System.Windows.Forms.Padding(4);
            this.txtTableNamespace.Name = "txtTableNamespace";
            this.txtTableNamespace.Size = new System.Drawing.Size(235, 23);
            this.txtTableNamespace.TabIndex = 21;
            // 
            // cbPartialPackageClasses
            // 
            this.cbPartialPackageClasses.AutoSize = true;
            this.cbPartialPackageClasses.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbPartialPackageClasses.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPartialPackageClasses.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbPartialPackageClasses.Location = new System.Drawing.Point(460, 71);
            this.cbPartialPackageClasses.Margin = new System.Windows.Forms.Padding(4);
            this.cbPartialPackageClasses.Name = "cbPartialPackageClasses";
            this.cbPartialPackageClasses.Size = new System.Drawing.Size(18, 17);
            this.cbPartialPackageClasses.TabIndex = 4;
            this.cbPartialPackageClasses.UseVisualStyleBackColor = true;
            // 
            // txtObjectTypeNamespace
            // 
            this.txtObjectTypeNamespace.Enabled = false;
            this.txtObjectTypeNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObjectTypeNamespace.Location = new System.Drawing.Point(209, 119);
            this.txtObjectTypeNamespace.Margin = new System.Windows.Forms.Padding(4);
            this.txtObjectTypeNamespace.Name = "txtObjectTypeNamespace";
            this.txtObjectTypeNamespace.Size = new System.Drawing.Size(235, 23);
            this.txtObjectTypeNamespace.TabIndex = 14;
            // 
            // txtPackageNamespace
            // 
            this.txtPackageNamespace.Enabled = false;
            this.txtPackageNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPackageNamespace.Location = new System.Drawing.Point(209, 68);
            this.txtPackageNamespace.Margin = new System.Windows.Forms.Padding(4);
            this.txtPackageNamespace.Name = "txtPackageNamespace";
            this.txtPackageNamespace.Size = new System.Drawing.Size(235, 23);
            this.txtPackageNamespace.TabIndex = 3;
            this.txtPackageNamespace.TextChanged += new System.EventHandler(this.txtPackageNamespace_TextChanged);
            // 
            // txtRecordTypeNamespace
            // 
            this.txtRecordTypeNamespace.Enabled = false;
            this.txtRecordTypeNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecordTypeNamespace.Location = new System.Drawing.Point(209, 94);
            this.txtRecordTypeNamespace.Margin = new System.Windows.Forms.Padding(4);
            this.txtRecordTypeNamespace.Name = "txtRecordTypeNamespace";
            this.txtRecordTypeNamespace.Size = new System.Drawing.Size(235, 23);
            this.txtRecordTypeNamespace.TabIndex = 7;
            // 
            // lblFacadeNamespace
            // 
            this.lblFacadeNamespace.AutoSize = true;
            this.lblFacadeNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacadeNamespace.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblFacadeNamespace.Location = new System.Drawing.Point(271, 48);
            this.lblFacadeNamespace.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFacadeNamespace.Name = "lblFacadeNamespace";
            this.lblFacadeNamespace.Size = new System.Drawing.Size(92, 17);
            this.lblFacadeNamespace.TabIndex = 19;
            this.lblFacadeNamespace.Text = "Namespace";
            this.lblFacadeNamespace.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbIncludeFilterPrefixInNaming
            // 
            this.cbIncludeFilterPrefixInNaming.AutoSize = true;
            this.cbIncludeFilterPrefixInNaming.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbIncludeFilterPrefixInNaming.Checked = true;
            this.cbIncludeFilterPrefixInNaming.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIncludeFilterPrefixInNaming.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIncludeFilterPrefixInNaming.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbIncludeFilterPrefixInNaming.Location = new System.Drawing.Point(346, 199);
            this.cbIncludeFilterPrefixInNaming.Margin = new System.Windows.Forms.Padding(4);
            this.cbIncludeFilterPrefixInNaming.Name = "cbIncludeFilterPrefixInNaming";
            this.cbIncludeFilterPrefixInNaming.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbIncludeFilterPrefixInNaming.Size = new System.Drawing.Size(395, 21);
            this.cbIncludeFilterPrefixInNaming.TabIndex = 34;
            this.cbIncludeFilterPrefixInNaming.Text = "Include Filter Prefix in Namespace and File Name?";
            this.cbIncludeFilterPrefixInNaming.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.cbIncludeFilterPrefixInNaming, "Incorporate the Filter Prefix value into namespaces and generated file names. Thi" +
        "s is \r\nuseful in cases where packages of multiple projects exists in the same sc" +
        "hema (e.g., APPS).");
            this.cbIncludeFilterPrefixInNaming.UseVisualStyleBackColor = true;
            // 
            // cbDeployResources
            // 
            this.cbDeployResources.AutoSize = true;
            this.cbDeployResources.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbDeployResources.Checked = true;
            this.cbDeployResources.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDeployResources.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDeployResources.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbDeployResources.Location = new System.Drawing.Point(487, 274);
            this.cbDeployResources.Margin = new System.Windows.Forms.Padding(4);
            this.cbDeployResources.Name = "cbDeployResources";
            this.cbDeployResources.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbDeployResources.Size = new System.Drawing.Size(253, 21);
            this.cbDeployResources.TabIndex = 40;
            this.cbDeployResources.Text = "Deploy/Update Utility Classes?";
            this.cbDeployResources.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.cbDeployResources, "These files only need to be deployed once for each version of generator.");
            this.cbDeployResources.UseVisualStyleBackColor = true;
            this.cbDeployResources.CheckedChanged += new System.EventHandler(this.cbDeployResources_CheckedChanged);
            // 
            // cbSerializableViews
            // 
            this.cbSerializableViews.AutoSize = true;
            this.cbSerializableViews.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSerializableViews.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSerializableViews.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbSerializableViews.Location = new System.Drawing.Point(504, 175);
            this.cbSerializableViews.Margin = new System.Windows.Forms.Padding(4);
            this.cbSerializableViews.Name = "cbSerializableViews";
            this.cbSerializableViews.Size = new System.Drawing.Size(18, 17);
            this.cbSerializableViews.TabIndex = 30;
            this.cbSerializableViews.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSerializableViews.UseVisualStyleBackColor = true;
            // 
            // cbSerializableTables
            // 
            this.cbSerializableTables.AutoSize = true;
            this.cbSerializableTables.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSerializableTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSerializableTables.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbSerializableTables.Location = new System.Drawing.Point(504, 149);
            this.cbSerializableTables.Margin = new System.Windows.Forms.Padding(4);
            this.cbSerializableTables.Name = "cbSerializableTables";
            this.cbSerializableTables.Size = new System.Drawing.Size(18, 17);
            this.cbSerializableTables.TabIndex = 23;
            this.cbSerializableTables.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSerializableTables.UseVisualStyleBackColor = true;
            // 
            // cbSerializableObjectTypes
            // 
            this.cbSerializableObjectTypes.AutoSize = true;
            this.cbSerializableObjectTypes.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSerializableObjectTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSerializableObjectTypes.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbSerializableObjectTypes.Location = new System.Drawing.Point(504, 123);
            this.cbSerializableObjectTypes.Margin = new System.Windows.Forms.Padding(4);
            this.cbSerializableObjectTypes.Name = "cbSerializableObjectTypes";
            this.cbSerializableObjectTypes.Size = new System.Drawing.Size(18, 17);
            this.cbSerializableObjectTypes.TabIndex = 16;
            this.cbSerializableObjectTypes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSerializableObjectTypes.UseVisualStyleBackColor = true;
            // 
            // cbGenerateView
            // 
            this.cbGenerateView.AutoSize = true;
            this.cbGenerateView.BackColor = System.Drawing.Color.Transparent;
            this.cbGenerateView.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGenerateView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGenerateView.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbGenerateView.Location = new System.Drawing.Point(85, 173);
            this.cbGenerateView.Margin = new System.Windows.Forms.Padding(4);
            this.cbGenerateView.Name = "cbGenerateView";
            this.cbGenerateView.Size = new System.Drawing.Size(109, 21);
            this.cbGenerateView.TabIndex = 27;
            this.cbGenerateView.Text = "View DTOs";
            this.cbGenerateView.UseVisualStyleBackColor = false;
            this.cbGenerateView.CheckedChanged += new System.EventHandler(this.cbGenerateView_CheckedChanged);
            // 
            // cbGenerateRecordType
            // 
            this.cbGenerateRecordType.AutoSize = true;
            this.cbGenerateRecordType.BackColor = System.Drawing.Color.Transparent;
            this.cbGenerateRecordType.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGenerateRecordType.Enabled = false;
            this.cbGenerateRecordType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGenerateRecordType.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbGenerateRecordType.Location = new System.Drawing.Point(26, 96);
            this.cbGenerateRecordType.Margin = new System.Windows.Forms.Padding(4);
            this.cbGenerateRecordType.Name = "cbGenerateRecordType";
            this.cbGenerateRecordType.Size = new System.Drawing.Size(169, 21);
            this.cbGenerateRecordType.TabIndex = 64;
            this.cbGenerateRecordType.Text = "Record Type DTOs";
            this.cbGenerateRecordType.UseVisualStyleBackColor = false;
            // 
            // cbGenerateTable
            // 
            this.cbGenerateTable.AutoSize = true;
            this.cbGenerateTable.BackColor = System.Drawing.Color.Transparent;
            this.cbGenerateTable.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGenerateTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGenerateTable.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbGenerateTable.Location = new System.Drawing.Point(77, 148);
            this.cbGenerateTable.Margin = new System.Windows.Forms.Padding(4);
            this.cbGenerateTable.Name = "cbGenerateTable";
            this.cbGenerateTable.Size = new System.Drawing.Size(117, 21);
            this.cbGenerateTable.TabIndex = 20;
            this.cbGenerateTable.Text = "Table DTOs";
            this.cbGenerateTable.UseVisualStyleBackColor = false;
            this.cbGenerateTable.CheckedChanged += new System.EventHandler(this.cbGenerateTable_CheckedChanged);
            // 
            // cbSerializablePOCOs
            // 
            this.cbSerializablePOCOs.AutoSize = true;
            this.cbSerializablePOCOs.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSerializablePOCOs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSerializablePOCOs.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbSerializablePOCOs.Location = new System.Drawing.Point(504, 98);
            this.cbSerializablePOCOs.Margin = new System.Windows.Forms.Padding(4);
            this.cbSerializablePOCOs.Name = "cbSerializablePOCOs";
            this.cbSerializablePOCOs.Size = new System.Drawing.Size(18, 17);
            this.cbSerializablePOCOs.TabIndex = 9;
            this.cbSerializablePOCOs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSerializablePOCOs.UseVisualStyleBackColor = true;
            this.cbSerializablePOCOs.CheckedChanged += new System.EventHandler(this.cbSerializablePOCOs_CheckedChanged);
            // 
            // cbGenerateObjectType
            // 
            this.cbGenerateObjectType.AutoSize = true;
            this.cbGenerateObjectType.BackColor = System.Drawing.Color.Transparent;
            this.cbGenerateObjectType.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGenerateObjectType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGenerateObjectType.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbGenerateObjectType.Location = new System.Drawing.Point(30, 122);
            this.cbGenerateObjectType.Margin = new System.Windows.Forms.Padding(4);
            this.cbGenerateObjectType.Name = "cbGenerateObjectType";
            this.cbGenerateObjectType.Size = new System.Drawing.Size(164, 21);
            this.cbGenerateObjectType.TabIndex = 13;
            this.cbGenerateObjectType.Text = "Object Type DTOs";
            this.cbGenerateObjectType.UseVisualStyleBackColor = false;
            this.cbGenerateObjectType.CheckedChanged += new System.EventHandler(this.cbGenerateObjectType_CheckedChanged);
            // 
            // txtMaxReturnArgStringSize
            // 
            this.txtMaxReturnArgStringSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxReturnArgStringSize.Location = new System.Drawing.Point(458, 127);
            this.txtMaxReturnArgStringSize.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaxReturnArgStringSize.Name = "txtMaxReturnArgStringSize";
            this.txtMaxReturnArgStringSize.Size = new System.Drawing.Size(67, 23);
            this.txtMaxReturnArgStringSize.TabIndex = 7;
            this.txtMaxReturnArgStringSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblMaxReturnArgStringSize
            // 
            this.lblMaxReturnArgStringSize.AutoSize = true;
            this.lblMaxReturnArgStringSize.BackColor = System.Drawing.Color.Transparent;
            this.lblMaxReturnArgStringSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxReturnArgStringSize.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblMaxReturnArgStringSize.Location = new System.Drawing.Point(160, 128);
            this.lblMaxReturnArgStringSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaxReturnArgStringSize.Name = "lblMaxReturnArgStringSize";
            this.lblMaxReturnArgStringSize.Size = new System.Drawing.Size(292, 17);
            this.lblMaxReturnArgStringSize.TabIndex = 4;
            this.lblMaxReturnArgStringSize.Text = "* Max Length of VARCHAR2 Return/Arg";
            this.lblMaxReturnArgStringSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblMaxReturnArgStringSize.Click += new System.EventHandler(this.lblMaxReturnArgStringSize_Click);
            // 
            // txtMaxAssocArraySize
            // 
            this.txtMaxAssocArraySize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxAssocArraySize.Location = new System.Drawing.Point(458, 151);
            this.txtMaxAssocArraySize.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaxAssocArraySize.Name = "txtMaxAssocArraySize";
            this.txtMaxAssocArraySize.Size = new System.Drawing.Size(67, 23);
            this.txtMaxAssocArraySize.TabIndex = 8;
            this.txtMaxAssocArraySize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblMaxAssocArraySize
            // 
            this.lblMaxAssocArraySize.AutoSize = true;
            this.lblMaxAssocArraySize.BackColor = System.Drawing.Color.Transparent;
            this.lblMaxAssocArraySize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxAssocArraySize.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblMaxAssocArraySize.Location = new System.Drawing.Point(135, 152);
            this.lblMaxAssocArraySize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaxAssocArraySize.Name = "lblMaxAssocArraySize";
            this.lblMaxAssocArraySize.Size = new System.Drawing.Size(317, 17);
            this.lblMaxAssocArraySize.TabIndex = 3;
            this.lblMaxAssocArraySize.Text = "* Max Size of Associative Array Return/Arg";
            this.lblMaxAssocArraySize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblFilter.Location = new System.Drawing.Point(357, 50);
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
            this.txtFilter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilter.Location = new System.Drawing.Point(455, 46);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(4);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(223, 23);
            this.txtFilter.TabIndex = 4;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // cbConvertOracleNumberToIntegerIfColumnNameIsId
            // 
            this.cbConvertOracleNumberToIntegerIfColumnNameIsId.BackColor = System.Drawing.Color.Transparent;
            this.cbConvertOracleNumberToIntegerIfColumnNameIsId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbConvertOracleNumberToIntegerIfColumnNameIsId.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbConvertOracleNumberToIntegerIfColumnNameIsId.Location = new System.Drawing.Point(215, 69);
            this.cbConvertOracleNumberToIntegerIfColumnNameIsId.Margin = new System.Windows.Forms.Padding(4);
            this.cbConvertOracleNumberToIntegerIfColumnNameIsId.Name = "cbConvertOracleNumberToIntegerIfColumnNameIsId";
            this.cbConvertOracleNumberToIntegerIfColumnNameIsId.Size = new System.Drawing.Size(277, 26);
            this.cbConvertOracleNumberToIntegerIfColumnNameIsId.TabIndex = 2;
            this.cbConvertOracleNumberToIntegerIfColumnNameIsId.Text = "Map \"ID\" NUMBER  as INT?";
            this.toolTip1.SetToolTip(this.cbConvertOracleNumberToIntegerIfColumnNameIsId, "If column, attribute, or parameter type is NUMBER with no precision/scale, \r\nand " +
        "its name is \"ID\" or ends with \"_ID\", then treat as an INTEGER.\r\n");
            this.cbConvertOracleNumberToIntegerIfColumnNameIsId.UseVisualStyleBackColor = false;
            // 
            // gbDatabase
            // 
            this.gbDatabase.BackColor = System.Drawing.Color.Transparent;
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
            this.gbDatabase.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.gbDatabase.Location = new System.Drawing.Point(16, 5);
            this.gbDatabase.Margin = new System.Windows.Forms.Padding(4);
            this.gbDatabase.Name = "gbDatabase";
            this.gbDatabase.Padding = new System.Windows.Forms.Padding(4);
            this.gbDatabase.Size = new System.Drawing.Size(688, 103);
            this.gbDatabase.TabIndex = 0;
            this.gbDatabase.TabStop = false;
            this.gbDatabase.Text = "Oracle Database";
            // 
            // cmbDBInstance
            // 
            this.cmbDBInstance.BackColor = System.Drawing.SystemColors.Window;
            this.cmbDBInstance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDBInstance.FormattingEnabled = true;
            this.cmbDBInstance.Location = new System.Drawing.Point(455, 16);
            this.cmbDBInstance.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDBInstance.Name = "cmbDBInstance";
            this.cmbDBInstance.Size = new System.Drawing.Size(223, 25);
            this.cmbDBInstance.TabIndex = 2;
            this.cmbDBInstance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDBInstance_KeyPress);
            // 
            // cmbClientHome
            // 
            this.cmbClientHome.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClientHome.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbClientHome.FormattingEnabled = true;
            this.cmbClientHome.Location = new System.Drawing.Point(111, 16);
            this.cmbClientHome.Margin = new System.Windows.Forms.Padding(4);
            this.cmbClientHome.Name = "cmbClientHome";
            this.cmbClientHome.Size = new System.Drawing.Size(232, 25);
            this.cmbClientHome.TabIndex = 1;
            this.cmbClientHome.SelectedIndexChanged += new System.EventHandler(this.cmbClientHome_SelectedIndexChanged);
            // 
            // lblClientHome
            // 
            this.lblClientHome.AutoSize = true;
            this.lblClientHome.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClientHome.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblClientHome.Location = new System.Drawing.Point(11, 21);
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
            // cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema
            // 
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.AutoSize = true;
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Checked = true;
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Location = new System.Drawing.Point(23, 16);
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Margin = new System.Windows.Forms.Padding(4);
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Name = "cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema";
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Size = new System.Drawing.Size(451, 21);
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.TabIndex = 0;
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.Text = "Duplicate Referenced Record Types Outside Filter Prefix?";
            this.toolTip1.SetToolTip(this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema, resources.GetString("cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.ToolTip"));
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.UseVisualStyleBackColor = true;
            this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema.CheckedChanged += new System.EventHandler(this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema_CheckedChanged);
            // 
            // lblCSharpTypeUsedForOracleNumber
            // 
            this.lblCSharpTypeUsedForOracleNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSharpTypeUsedForOracleNumber.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblCSharpTypeUsedForOracleNumber.Location = new System.Drawing.Point(3, 47);
            this.lblCSharpTypeUsedForOracleNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCSharpTypeUsedForOracleNumber.Name = "lblCSharpTypeUsedForOracleNumber";
            this.lblCSharpTypeUsedForOracleNumber.Size = new System.Drawing.Size(208, 16);
            this.lblCSharpTypeUsedForOracleNumber.TabIndex = 58;
            this.lblCSharpTypeUsedForOracleNumber.Text = "NUMBER && NUMBER(p,s) ";
            this.lblCSharpTypeUsedForOracleNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblCSharpTypeUsedForOracleNumber, "An Oracle NUMBER or a NUMBER with a non-zero precision and scale.");
            // 
            // lblCSharpTypeUsedForOracleInteger
            // 
            this.lblCSharpTypeUsedForOracleInteger.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSharpTypeUsedForOracleInteger.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblCSharpTypeUsedForOracleInteger.Location = new System.Drawing.Point(3, 21);
            this.lblCSharpTypeUsedForOracleInteger.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCSharpTypeUsedForOracleInteger.Name = "lblCSharpTypeUsedForOracleInteger";
            this.lblCSharpTypeUsedForOracleInteger.Size = new System.Drawing.Size(208, 16);
            this.lblCSharpTypeUsedForOracleInteger.TabIndex = 55;
            this.lblCSharpTypeUsedForOracleInteger.Text = "INTEGER && NUMBER(p>9)";
            this.lblCSharpTypeUsedForOracleInteger.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblCSharpTypeUsedForOracleInteger, "An Oracle INTEGER, INTEGER equivalent, or any NUMBER with a precision > 9 and no " +
        "scale.");
            // 
            // cbExcludeObjectNamesWithSpecificChars
            // 
            this.cbExcludeObjectNamesWithSpecificChars.AutoSize = true;
            this.cbExcludeObjectNamesWithSpecificChars.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbExcludeObjectNamesWithSpecificChars.Checked = true;
            this.cbExcludeObjectNamesWithSpecificChars.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbExcludeObjectNamesWithSpecificChars.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbExcludeObjectNamesWithSpecificChars.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbExcludeObjectNamesWithSpecificChars.Location = new System.Drawing.Point(90, 38);
            this.cbExcludeObjectNamesWithSpecificChars.Margin = new System.Windows.Forms.Padding(4);
            this.cbExcludeObjectNamesWithSpecificChars.Name = "cbExcludeObjectNamesWithSpecificChars";
            this.cbExcludeObjectNamesWithSpecificChars.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbExcludeObjectNamesWithSpecificChars.Size = new System.Drawing.Size(384, 21);
            this.cbExcludeObjectNamesWithSpecificChars.TabIndex = 1;
            this.cbExcludeObjectNamesWithSpecificChars.Text = "Exclude Object Names With Specific Characters?";
            this.toolTip1.SetToolTip(this.cbExcludeObjectNamesWithSpecificChars, "Packages, object types, tables, and views with given characters in the name will " +
        "be excluded from generation.");
            this.cbExcludeObjectNamesWithSpecificChars.UseVisualStyleBackColor = true;
            this.cbExcludeObjectNamesWithSpecificChars.CheckedChanged += new System.EventHandler(this.cbExcludeObjectNamesWithSpecificChars_CheckedChanged);
            // 
            // cbGeneratedDynamicMethodForTypedCursor
            // 
            this.cbGeneratedDynamicMethodForTypedCursor.AutoSize = true;
            this.cbGeneratedDynamicMethodForTypedCursor.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGeneratedDynamicMethodForTypedCursor.Checked = true;
            this.cbGeneratedDynamicMethodForTypedCursor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGeneratedDynamicMethodForTypedCursor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGeneratedDynamicMethodForTypedCursor.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbGeneratedDynamicMethodForTypedCursor.Location = new System.Drawing.Point(59, 60);
            this.cbGeneratedDynamicMethodForTypedCursor.Margin = new System.Windows.Forms.Padding(4);
            this.cbGeneratedDynamicMethodForTypedCursor.Name = "cbGeneratedDynamicMethodForTypedCursor";
            this.cbGeneratedDynamicMethodForTypedCursor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbGeneratedDynamicMethodForTypedCursor.Size = new System.Drawing.Size(415, 21);
            this.cbGeneratedDynamicMethodForTypedCursor.TabIndex = 3;
            this.cbGeneratedDynamicMethodForTypedCursor.Text = "Generate Mapping Driven Method For Typed Cursor?";
            this.toolTip1.SetToolTip(this.cbGeneratedDynamicMethodForTypedCursor, resources.GetString("cbGeneratedDynamicMethodForTypedCursor.ToolTip"));
            this.cbGeneratedDynamicMethodForTypedCursor.UseVisualStyleBackColor = true;
            this.cbGeneratedDynamicMethodForTypedCursor.CheckedChanged += new System.EventHandler(this.cbGeneratedDynamicMethodForTypedCursor_CheckedChanged);
            // 
            // cbUseAutoImplementedProperties
            // 
            this.cbUseAutoImplementedProperties.AutoSize = true;
            this.cbUseAutoImplementedProperties.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbUseAutoImplementedProperties.Checked = true;
            this.cbUseAutoImplementedProperties.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUseAutoImplementedProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUseAutoImplementedProperties.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbUseAutoImplementedProperties.Location = new System.Drawing.Point(122, 82);
            this.cbUseAutoImplementedProperties.Margin = new System.Windows.Forms.Padding(4);
            this.cbUseAutoImplementedProperties.Name = "cbUseAutoImplementedProperties";
            this.cbUseAutoImplementedProperties.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbUseAutoImplementedProperties.Size = new System.Drawing.Size(353, 21);
            this.cbUseAutoImplementedProperties.TabIndex = 4;
            this.cbUseAutoImplementedProperties.Text = "Use Auto-Implemented Properties for DTOs?";
            this.toolTip1.SetToolTip(this.cbUseAutoImplementedProperties, "Generate auto-implemented properties for DTO classes. Otherwise, properties will " +
        "wrap protected fields.");
            this.cbUseAutoImplementedProperties.UseVisualStyleBackColor = true;
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveSettings.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnSaveSettings.Location = new System.Drawing.Point(341, 18);
            this.btnSaveSettings.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(125, 28);
            this.btnSaveSettings.TabIndex = 1;
            this.btnSaveSettings.Text = "Save Current";
            this.toolTip1.SetToolTip(this.btnSaveSettings, "Save current settings to config file in File Source");
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveCurrentSettings_Click);
            // 
            // lblCSharpTypeUsedForOracleTimestamp
            // 
            this.lblCSharpTypeUsedForOracleTimestamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSharpTypeUsedForOracleTimestamp.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblCSharpTypeUsedForOracleTimestamp.Location = new System.Drawing.Point(3, 124);
            this.lblCSharpTypeUsedForOracleTimestamp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCSharpTypeUsedForOracleTimestamp.Name = "lblCSharpTypeUsedForOracleTimestamp";
            this.lblCSharpTypeUsedForOracleTimestamp.Size = new System.Drawing.Size(208, 16);
            this.lblCSharpTypeUsedForOracleTimestamp.TabIndex = 61;
            this.lblCSharpTypeUsedForOracleTimestamp.Text = "TIMESTAMP";
            this.lblCSharpTypeUsedForOracleTimestamp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblCSharpTypeUsedForOracleTimestamp, "An Oracle TIMESTAMP");
            // 
            // lblCSharpUsedForOracleIntervalDayToSecond
            // 
            this.lblCSharpUsedForOracleIntervalDayToSecond.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSharpUsedForOracleIntervalDayToSecond.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblCSharpUsedForOracleIntervalDayToSecond.Location = new System.Drawing.Point(20, 204);
            this.lblCSharpUsedForOracleIntervalDayToSecond.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCSharpUsedForOracleIntervalDayToSecond.Name = "lblCSharpUsedForOracleIntervalDayToSecond";
            this.lblCSharpUsedForOracleIntervalDayToSecond.Size = new System.Drawing.Size(191, 16);
            this.lblCSharpUsedForOracleIntervalDayToSecond.TabIndex = 60;
            this.lblCSharpUsedForOracleIntervalDayToSecond.Text = "INTERVAL DAY TO SEC";
            this.lblCSharpUsedForOracleIntervalDayToSecond.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblCSharpUsedForOracleIntervalDayToSecond, "An Oracle INTERVAL DAY TO SECOND");
            this.lblCSharpUsedForOracleIntervalDayToSecond.Visible = false;
            // 
            // lblCSharpTypeUsedForOracleDate
            // 
            this.lblCSharpTypeUsedForOracleDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSharpTypeUsedForOracleDate.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblCSharpTypeUsedForOracleDate.Location = new System.Drawing.Point(3, 97);
            this.lblCSharpTypeUsedForOracleDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCSharpTypeUsedForOracleDate.Name = "lblCSharpTypeUsedForOracleDate";
            this.lblCSharpTypeUsedForOracleDate.Size = new System.Drawing.Size(208, 16);
            this.lblCSharpTypeUsedForOracleDate.TabIndex = 59;
            this.lblCSharpTypeUsedForOracleDate.Text = "DATE";
            this.lblCSharpTypeUsedForOracleDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblCSharpTypeUsedForOracleDate, "An Oracle DATE");
            // 
            // lblLocalVariableNameSuffix
            // 
            this.lblLocalVariableNameSuffix.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalVariableNameSuffix.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblLocalVariableNameSuffix.Location = new System.Drawing.Point(95, 103);
            this.lblLocalVariableNameSuffix.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLocalVariableNameSuffix.Name = "lblLocalVariableNameSuffix";
            this.lblLocalVariableNameSuffix.Size = new System.Drawing.Size(357, 21);
            this.lblLocalVariableNameSuffix.TabIndex = 5;
            this.lblLocalVariableNameSuffix.Text = "* Prefix For Generated Local Variable Names";
            this.lblLocalVariableNameSuffix.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblLocalVariableNameSuffix, "Prefix that will be used for all local variable names in generated code.");
            // 
            // lblSettingsFile
            // 
            this.lblSettingsFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSettingsFile.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblSettingsFile.Location = new System.Drawing.Point(9, 22);
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
            this.lblCSharpTypeUsedForOracleBlob.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblCSharpTypeUsedForOracleBlob.Location = new System.Drawing.Point(3, 150);
            this.lblCSharpTypeUsedForOracleBlob.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCSharpTypeUsedForOracleBlob.Name = "lblCSharpTypeUsedForOracleBlob";
            this.lblCSharpTypeUsedForOracleBlob.Size = new System.Drawing.Size(208, 16);
            this.lblCSharpTypeUsedForOracleBlob.TabIndex = 62;
            this.lblCSharpTypeUsedForOracleBlob.Text = "BLOB";
            this.lblCSharpTypeUsedForOracleBlob.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblCSharpTypeUsedForOracleBlob, "An Oracle BLOB");
            // 
            // lblCSharpTypeUsedForOracleClob
            // 
            this.lblCSharpTypeUsedForOracleClob.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSharpTypeUsedForOracleClob.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblCSharpTypeUsedForOracleClob.Location = new System.Drawing.Point(3, 177);
            this.lblCSharpTypeUsedForOracleClob.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCSharpTypeUsedForOracleClob.Name = "lblCSharpTypeUsedForOracleClob";
            this.lblCSharpTypeUsedForOracleClob.Size = new System.Drawing.Size(208, 16);
            this.lblCSharpTypeUsedForOracleClob.TabIndex = 64;
            this.lblCSharpTypeUsedForOracleClob.Text = "CLOB && NCLOB";
            this.lblCSharpTypeUsedForOracleClob.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.lblCSharpTypeUsedForOracleClob, "An Oracle CLOB or NCLOB");
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.lblSettingsFile);
            this.gbSettings.Controls.Add(this.cmbSettingsFile);
            this.gbSettings.Controls.Add(this.btnSaveSettings);
            this.gbSettings.Controls.Add(this.btnRestoreDefaults);
            this.gbSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSettings.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.gbSettings.Location = new System.Drawing.Point(560, 606);
            this.gbSettings.Margin = new System.Windows.Forms.Padding(4);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Padding = new System.Windows.Forms.Padding(4);
            this.gbSettings.Size = new System.Drawing.Size(633, 52);
            this.gbSettings.TabIndex = 5;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Settings";
            // 
            // cmbSettingsFile
            // 
            this.cmbSettingsFile.BackColor = System.Drawing.SystemColors.Window;
            this.cmbSettingsFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSettingsFile.FormattingEnabled = true;
            this.cmbSettingsFile.Location = new System.Drawing.Point(108, 19);
            this.cmbSettingsFile.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSettingsFile.Name = "cmbSettingsFile";
            this.cmbSettingsFile.Size = new System.Drawing.Size(227, 25);
            this.cmbSettingsFile.TabIndex = 0;
            this.cmbSettingsFile.SelectedIndexChanged += new System.EventHandler(this.cmbSettingsFile_SelectedIndexChanged);
            // 
            // btnRestoreDefaults
            // 
            this.btnRestoreDefaults.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestoreDefaults.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnRestoreDefaults.Location = new System.Drawing.Point(472, 18);
            this.btnRestoreDefaults.Margin = new System.Windows.Forms.Padding(4);
            this.btnRestoreDefaults.Name = "btnRestoreDefaults";
            this.btnRestoreDefaults.Size = new System.Drawing.Size(153, 28);
            this.btnRestoreDefaults.TabIndex = 2;
            this.btnRestoreDefaults.Text = "Restore Defaults";
            this.btnRestoreDefaults.UseVisualStyleBackColor = true;
            this.btnRestoreDefaults.Click += new System.EventHandler(this.btnRestoreDefaults_Click);
            // 
            // gbOracleToCSharpCustomTranslation
            // 
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
            this.gbOracleToCSharpCustomTranslation.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.gbOracleToCSharpCustomTranslation.Location = new System.Drawing.Point(16, 423);
            this.gbOracleToCSharpCustomTranslation.Margin = new System.Windows.Forms.Padding(4);
            this.gbOracleToCSharpCustomTranslation.Name = "gbOracleToCSharpCustomTranslation";
            this.gbOracleToCSharpCustomTranslation.Padding = new System.Windows.Forms.Padding(4);
            this.gbOracleToCSharpCustomTranslation.Size = new System.Drawing.Size(537, 235);
            this.gbOracleToCSharpCustomTranslation.TabIndex = 3;
            this.gbOracleToCSharpCustomTranslation.TabStop = false;
            this.gbOracleToCSharpCustomTranslation.Text = "Oracle to C# Custom Translation";
            // 
            // cmbCSharpTypeUsedForOracleClob
            // 
            this.cmbCSharpTypeUsedForOracleClob.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCSharpTypeUsedForOracleClob.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCSharpTypeUsedForOracleClob.FormattingEnabled = true;
            this.cmbCSharpTypeUsedForOracleClob.Location = new System.Drawing.Point(214, 173);
            this.cmbCSharpTypeUsedForOracleClob.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCSharpTypeUsedForOracleClob.Name = "cmbCSharpTypeUsedForOracleClob";
            this.cmbCSharpTypeUsedForOracleClob.Size = new System.Drawing.Size(315, 25);
            this.cmbCSharpTypeUsedForOracleClob.TabIndex = 6;
            // 
            // cmbCSharpTypeUsedForOracleBlob
            // 
            this.cmbCSharpTypeUsedForOracleBlob.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCSharpTypeUsedForOracleBlob.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCSharpTypeUsedForOracleBlob.FormattingEnabled = true;
            this.cmbCSharpTypeUsedForOracleBlob.Location = new System.Drawing.Point(214, 146);
            this.cmbCSharpTypeUsedForOracleBlob.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCSharpTypeUsedForOracleBlob.Name = "cmbCSharpTypeUsedForOracleBlob";
            this.cmbCSharpTypeUsedForOracleBlob.Size = new System.Drawing.Size(315, 25);
            this.cmbCSharpTypeUsedForOracleBlob.TabIndex = 5;
            // 
            // cmbCSharpTypeUsedForOracleIntervalDayToSecond
            // 
            this.cmbCSharpTypeUsedForOracleIntervalDayToSecond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCSharpTypeUsedForOracleIntervalDayToSecond.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCSharpTypeUsedForOracleIntervalDayToSecond.FormattingEnabled = true;
            this.cmbCSharpTypeUsedForOracleIntervalDayToSecond.Location = new System.Drawing.Point(214, 201);
            this.cmbCSharpTypeUsedForOracleIntervalDayToSecond.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCSharpTypeUsedForOracleIntervalDayToSecond.Name = "cmbCSharpTypeUsedForOracleIntervalDayToSecond";
            this.cmbCSharpTypeUsedForOracleIntervalDayToSecond.Size = new System.Drawing.Size(315, 25);
            this.cmbCSharpTypeUsedForOracleIntervalDayToSecond.TabIndex = 7;
            this.cmbCSharpTypeUsedForOracleIntervalDayToSecond.Visible = false;
            // 
            // cmbCSharpTypeUsedForOracleTimestamp
            // 
            this.cmbCSharpTypeUsedForOracleTimestamp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCSharpTypeUsedForOracleTimestamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCSharpTypeUsedForOracleTimestamp.FormattingEnabled = true;
            this.cmbCSharpTypeUsedForOracleTimestamp.Location = new System.Drawing.Point(214, 119);
            this.cmbCSharpTypeUsedForOracleTimestamp.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCSharpTypeUsedForOracleTimestamp.Name = "cmbCSharpTypeUsedForOracleTimestamp";
            this.cmbCSharpTypeUsedForOracleTimestamp.Size = new System.Drawing.Size(315, 25);
            this.cmbCSharpTypeUsedForOracleTimestamp.TabIndex = 4;
            // 
            // cmbCSharpTypeUsedForOracleDate
            // 
            this.cmbCSharpTypeUsedForOracleDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCSharpTypeUsedForOracleDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCSharpTypeUsedForOracleDate.FormattingEnabled = true;
            this.cmbCSharpTypeUsedForOracleDate.Location = new System.Drawing.Point(214, 92);
            this.cmbCSharpTypeUsedForOracleDate.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCSharpTypeUsedForOracleDate.Name = "cmbCSharpTypeUsedForOracleDate";
            this.cmbCSharpTypeUsedForOracleDate.Size = new System.Drawing.Size(315, 25);
            this.cmbCSharpTypeUsedForOracleDate.TabIndex = 3;
            // 
            // cmbCSharpTypeUsedForOracleNumber
            // 
            this.cmbCSharpTypeUsedForOracleNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCSharpTypeUsedForOracleNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCSharpTypeUsedForOracleNumber.FormattingEnabled = true;
            this.cmbCSharpTypeUsedForOracleNumber.Location = new System.Drawing.Point(214, 42);
            this.cmbCSharpTypeUsedForOracleNumber.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCSharpTypeUsedForOracleNumber.Name = "cmbCSharpTypeUsedForOracleNumber";
            this.cmbCSharpTypeUsedForOracleNumber.Size = new System.Drawing.Size(315, 25);
            this.cmbCSharpTypeUsedForOracleNumber.TabIndex = 1;
            // 
            // cmbCSharpTypeUsedForOracleInteger
            // 
            this.cmbCSharpTypeUsedForOracleInteger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCSharpTypeUsedForOracleInteger.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCSharpTypeUsedForOracleInteger.FormattingEnabled = true;
            this.cmbCSharpTypeUsedForOracleInteger.Location = new System.Drawing.Point(214, 16);
            this.cmbCSharpTypeUsedForOracleInteger.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCSharpTypeUsedForOracleInteger.Name = "cmbCSharpTypeUsedForOracleInteger";
            this.cmbCSharpTypeUsedForOracleInteger.Size = new System.Drawing.Size(315, 25);
            this.cmbCSharpTypeUsedForOracleInteger.TabIndex = 0;
            // 
            // gbAdvancedProcOptions
            // 
            this.gbAdvancedProcOptions.BackColor = System.Drawing.Color.Transparent;
            this.gbAdvancedProcOptions.Controls.Add(this.lblLocalVariableNameSuffix);
            this.gbAdvancedProcOptions.Controls.Add(this.txtLocalVariableNameSuffix);
            this.gbAdvancedProcOptions.Controls.Add(this.cbUseAutoImplementedProperties);
            this.gbAdvancedProcOptions.Controls.Add(this.cbGeneratedDynamicMethodForTypedCursor);
            this.gbAdvancedProcOptions.Controls.Add(this.txtExcludeChars);
            this.gbAdvancedProcOptions.Controls.Add(this.cbExcludeObjectNamesWithSpecificChars);
            this.gbAdvancedProcOptions.Controls.Add(this.cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema);
            this.gbAdvancedProcOptions.Controls.Add(this.lblMaxAssocArraySize);
            this.gbAdvancedProcOptions.Controls.Add(this.txtMaxAssocArraySize);
            this.gbAdvancedProcOptions.Controls.Add(this.lblMaxReturnArgStringSize);
            this.gbAdvancedProcOptions.Controls.Add(this.txtMaxReturnArgStringSize);
            this.gbAdvancedProcOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbAdvancedProcOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAdvancedProcOptions.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.gbAdvancedProcOptions.Location = new System.Drawing.Point(560, 423);
            this.gbAdvancedProcOptions.Margin = new System.Windows.Forms.Padding(4);
            this.gbAdvancedProcOptions.Name = "gbAdvancedProcOptions";
            this.gbAdvancedProcOptions.Padding = new System.Windows.Forms.Padding(4);
            this.gbAdvancedProcOptions.Size = new System.Drawing.Size(633, 181);
            this.gbAdvancedProcOptions.TabIndex = 4;
            this.gbAdvancedProcOptions.TabStop = false;
            this.gbAdvancedProcOptions.Text = "Advanced Options";
            // 
            // txtLocalVariableNameSuffix
            // 
            this.txtLocalVariableNameSuffix.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocalVariableNameSuffix.Location = new System.Drawing.Point(458, 103);
            this.txtLocalVariableNameSuffix.Margin = new System.Windows.Forms.Padding(4);
            this.txtLocalVariableNameSuffix.Name = "txtLocalVariableNameSuffix";
            this.txtLocalVariableNameSuffix.Size = new System.Drawing.Size(99, 23);
            this.txtLocalVariableNameSuffix.TabIndex = 6;
            this.txtLocalVariableNameSuffix.TextChanged += new System.EventHandler(this.txtLocalVariableNameSuffix_TextChanged);
            // 
            // txtExcludeChars
            // 
            this.txtExcludeChars.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExcludeChars.Location = new System.Drawing.Point(483, 35);
            this.txtExcludeChars.Margin = new System.Windows.Forms.Padding(4);
            this.txtExcludeChars.Name = "txtExcludeChars";
            this.txtExcludeChars.Size = new System.Drawing.Size(72, 23);
            this.txtExcludeChars.TabIndex = 2;
            // 
            // FormMain
            // 
            this.AcceptButton = this.BtnStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1209, 668);
            this.Controls.Add(this.gbOracleToCSharpCustomTranslation);
            this.Controls.Add(this.gbSettings);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.gbDatabase);
            this.Controls.Add(this.ListViewMessage);
            this.Controls.Add(this.gbCodeToGenerate);
            this.Controls.Add(this.gbAdvancedProcOptions);
            this.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FormMain";
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
        private System.Windows.Forms.CheckBox cbPartialPOCOs;
        private System.Windows.Forms.CheckBox cbGeneratePackage;
        private System.Windows.Forms.CheckBox cbGenerateProcedure;
        private System.Windows.Forms.CheckBox cbGenerateFunction;
        private System.Windows.Forms.GroupBox gbCodeToGenerate;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.CheckBox cbSerializablePOCOs;
        private System.Windows.Forms.CheckBox cbPartialPackageClasses;
        private System.Windows.Forms.TextBox txtPackageNamespace;
        private System.Windows.Forms.Label lblFacadeNamespace;
        private System.Windows.Forms.TextBox txtRecordTypeNamespace;
        private System.Windows.Forms.GroupBox gbDatabase;
        private System.Windows.Forms.TextBox txtProcedureNamespace;
        private System.Windows.Forms.TextBox txtFunctionNamespace;
        private System.Windows.Forms.Label lblClientHome;
        private System.Windows.Forms.ComboBox cmbClientHome;
        private System.Windows.Forms.ComboBox cmbDBInstance;
        private System.Windows.Forms.CheckBox cbGenerateRecordType;
        private System.Windows.Forms.Label lblBaseClassName;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox txtObjectTypeNamespace;
        private System.Windows.Forms.CheckBox cbGenerateObjectType;
        private System.Windows.Forms.CheckBox cbSerializableObjectTypes;
        private System.Windows.Forms.CheckBox cbGenerateTable;
        private System.Windows.Forms.CheckBox cbGenerateView;
        private System.Windows.Forms.TextBox txtViewNamespace;
        private System.Windows.Forms.TextBox txtTableNamespace;
        private System.Windows.Forms.Label lblPartial;
        private System.Windows.Forms.Label lblSerializable;
        private System.Windows.Forms.CheckBox cbPartialViews;
        private System.Windows.Forms.CheckBox cbPartialTables;
        private System.Windows.Forms.CheckBox cbSerializableTables;
        private System.Windows.Forms.CheckBox cbSerializableViews;
        private System.Windows.Forms.CheckBox cbPartialObjectTypes;
        private System.Windows.Forms.TextBox txtBaseRecordTypeClass;
        private System.Windows.Forms.TextBox txtBasePackageClass;
        private System.Windows.Forms.CheckBox cbGenerateBaseDtoClasses;
        private System.Windows.Forms.CheckBox cbGenerateBaseAdapterClass;
        private System.Windows.Forms.TextBox txtBaseViewClass;
        private System.Windows.Forms.TextBox txtBaseTableClass;
        private System.Windows.Forms.TextBox txtBaseObjectTypeClass;
        private System.Windows.Forms.TextBox txtBaseConnectionClassFunction;
        private System.Windows.Forms.TextBox txtBaseConnectionClassProcedure;
        private System.Windows.Forms.CheckBox cbDeployResources;
        private System.Windows.Forms.CheckBox cbConvertOracleNumberToIntegerIfColumnNameIsId;
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
        private System.Windows.Forms.ComboBox cmbCSharpTypeUsedForOracleInteger;
        private System.Windows.Forms.ComboBox cmbCSharpTypeUsedForOracleNumber;
        private System.Windows.Forms.ComboBox cmbCSharpTypeUsedForOracleIntervalDayToSecond;
        private System.Windows.Forms.ComboBox cmbCSharpTypeUsedForOracleTimestamp;
        private System.Windows.Forms.ComboBox cmbCSharpTypeUsedForOracleDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMaxReturnArgStringSize;
        private System.Windows.Forms.Label lblMaxReturnArgStringSize;
        private System.Windows.Forms.TextBox txtMaxAssocArraySize;
        private System.Windows.Forms.Label lblMaxAssocArraySize;
        private System.Windows.Forms.GroupBox gbAdvancedProcOptions;
        private System.Windows.Forms.Button btnRestoreDefaults;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.CheckBox cbDuplicatePackageRecordOriginatingOutsideFilterAndSchema;
        private System.Windows.Forms.CheckBox cbXmlElementPackageRecord;
        private System.Windows.Forms.CheckBox cbXmlElementView;
        private System.Windows.Forms.CheckBox cbXmlElementTable;
        private System.Windows.Forms.CheckBox cbXmlElementObjectType;
        private System.Windows.Forms.CheckBox cbDataContractView;
        private System.Windows.Forms.CheckBox cbDataContractTable;
        private System.Windows.Forms.CheckBox cbDataContractObjectType;
        private System.Windows.Forms.CheckBox cbDataContractPackageRecord;
        private System.Windows.Forms.TextBox txtDataContractNamespace;
        private System.Windows.Forms.Label lblDataContractNamespace;
        private System.Windows.Forms.TextBox txtExcludeChars;
        private System.Windows.Forms.CheckBox cbExcludeObjectNamesWithSpecificChars;
        private System.Windows.Forms.CheckBox cbGeneratedDynamicMethodForTypedCursor;
        private System.Windows.Forms.CheckBox cbUseAutoImplementedProperties;
        private System.Windows.Forms.Label lblSettingsFile;
        private System.Windows.Forms.ComboBox cmbSettingsFile;
        private System.Windows.Forms.Label lblLocalVariableNameSuffix;
        private System.Windows.Forms.TextBox txtLocalVariableNameSuffix;
        private System.Windows.Forms.ComboBox cmbCSharpVersion;
        private System.Windows.Forms.Label lblCSharpVersion;
        private System.Windows.Forms.CheckBox cbIncludeFilterPrefixInNaming;
        private System.Windows.Forms.Label lblCSharpTypeUsedForOracleBlob;
        private System.Windows.Forms.ComboBox cmbCSharpTypeUsedForOracleBlob;
        private System.Windows.Forms.Label lblCSharpTypeUsedForOracleClob;
        private System.Windows.Forms.ComboBox cmbCSharpTypeUsedForOracleClob;
    }
}

