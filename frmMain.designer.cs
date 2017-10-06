namespace EcdsaTest
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabRSAKeys = new System.Windows.Forms.TabPage();
            this.gbRSAPub = new System.Windows.Forms.GroupBox();
            this.lbRSAPubExp = new System.Windows.Forms.Label();
            this.tbRSAPubExp = new System.Windows.Forms.TextBox();
            this.gbRSAModulus = new System.Windows.Forms.GroupBox();
            this.lbRSAModulus = new System.Windows.Forms.Label();
            this.tbRSAModulus = new System.Windows.Forms.TextBox();
            this.gbRSAPriv = new System.Windows.Forms.GroupBox();
            this.lbRSAPrivP = new System.Windows.Forms.Label();
            this.lbRSAPrivQ = new System.Windows.Forms.Label();
            this.lbRSAPrivPQ = new System.Windows.Forms.Label();
            this.lbRSAPrivDP1 = new System.Windows.Forms.Label();
            this.tbRSAPrivDQ1 = new System.Windows.Forms.TextBox();
            this.tbRSAPrivDP1 = new System.Windows.Forms.TextBox();
            this.tbRSAPrivPQ = new System.Windows.Forms.TextBox();
            this.tbRSAPrivQ = new System.Windows.Forms.TextBox();
            this.lbRSAPrivDQ1 = new System.Windows.Forms.Label();
            this.lbRSAPrivExp = new System.Windows.Forms.Label();
            this.tbRSAPrivExp = new System.Windows.Forms.TextBox();
            this.tbRSAPrivP = new System.Windows.Forms.TextBox();
            this.gpPrivateKeyMode = new System.Windows.Forms.GroupBox();
            this.rbCRT = new System.Windows.Forms.RadioButton();
            this.rbModExp = new System.Windows.Forms.RadioButton();
            this.gbRSACommands = new System.Windows.Forms.GroupBox();
            this.btnRestoreRSAPriv = new System.Windows.Forms.Button();
            this.btnBackupRSAPriv = new System.Windows.Forms.Button();
            this.btnSaveRSAPub = new System.Windows.Forms.Button();
            this.btnStoreRSAPriv = new System.Windows.Forms.Button();
            this.btnMkRSAKey = new System.Windows.Forms.Button();
            this.cbRSAKeyLength = new System.Windows.Forms.ComboBox();
            this.cbRSAKeyIndex = new System.Windows.Forms.ComboBox();
            this.lbRSAKeyLength = new System.Windows.Forms.Label();
            this.lbRSAKeyIndex = new System.Windows.Forms.Label();
            this.btnRSAImportP12 = new System.Windows.Forms.Button();
            this.tabECKeys = new System.Windows.Forms.TabPage();
            this.gbECPubKey = new System.Windows.Forms.GroupBox();
            this.lbECPubKey = new System.Windows.Forms.Label();
            this.tbECPubKey = new System.Windows.Forms.TextBox();
            this.gbECPrivKey = new System.Windows.Forms.GroupBox();
            this.lbECPrivKey = new System.Windows.Forms.Label();
            this.tbECPrivKey = new System.Windows.Forms.TextBox();
            this.gbECDomainParameters = new System.Windows.Forms.GroupBox();
            this.lbECParamK = new System.Windows.Forms.Label();
            this.tbECParamK = new System.Windows.Forms.TextBox();
            this.lbECParamR = new System.Windows.Forms.Label();
            this.tbECParamR = new System.Windows.Forms.TextBox();
            this.lbECParamG = new System.Windows.Forms.Label();
            this.tbECParamG = new System.Windows.Forms.TextBox();
            this.lbECParamB = new System.Windows.Forms.Label();
            this.tbECParamB = new System.Windows.Forms.TextBox();
            this.ltbECParamA = new System.Windows.Forms.Label();
            this.tbECParamA = new System.Windows.Forms.TextBox();
            this.gbECReductionPolynomial = new System.Windows.Forms.GroupBox();
            this.rtbECReductionPolynomial = new System.Windows.Forms.RichTextBox();
            this.lbECParamE3 = new System.Windows.Forms.Label();
            this.tbECParamE3 = new System.Windows.Forms.TextBox();
            this.lbECParamE2 = new System.Windows.Forms.Label();
            this.tbECParamE2 = new System.Windows.Forms.TextBox();
            this.lbECParamE1 = new System.Windows.Forms.Label();
            this.tbECParamE1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbECFieldPrime = new System.Windows.Forms.RadioButton();
            this.rbECFieldBinary = new System.Windows.Forms.RadioButton();
            this.lbECParamPrime = new System.Windows.Forms.Label();
            this.tbECParamPrime = new System.Windows.Forms.TextBox();
            this.gbECCommands = new System.Windows.Forms.GroupBox();
            this.btnRestoreECPriv = new System.Windows.Forms.Button();
            this.btnBackupECPriv = new System.Windows.Forms.Button();
            this.cbECName = new System.Windows.Forms.ComboBox();
            this.lbECName = new System.Windows.Forms.Label();
            this.btnSaveECPub = new System.Windows.Forms.Button();
            this.btnStoreECPriv = new System.Windows.Forms.Button();
            this.btnMkECKey = new System.Windows.Forms.Button();
            this.cbECKeyLength = new System.Windows.Forms.ComboBox();
            this.cbECKeyIndex = new System.Windows.Forms.ComboBox();
            this.lbECKeyLength = new System.Windows.Forms.Label();
            this.lbECKeyIndex = new System.Windows.Forms.Label();
            this.btnECImportP12 = new System.Windows.Forms.Button();
            this.tabSignature = new System.Windows.Forms.TabPage();
            this.gbSignatureHash = new System.Windows.Forms.GroupBox();
            this.btnHashStoreToBin = new System.Windows.Forms.Button();
            this.btnHash = new System.Windows.Forms.Button();
            this.cbHashAlg = new System.Windows.Forms.ComboBox();
            this.lbHashAlg = new System.Windows.Forms.Label();
            this.rbHashBase64 = new System.Windows.Forms.RadioButton();
            this.rbHashHex = new System.Windows.Forms.RadioButton();
            this.lbHash = new System.Windows.Forms.Label();
            this.tbHash = new System.Windows.Forms.TextBox();
            this.gbSignature = new System.Windows.Forms.GroupBox();
            this.pbSigning = new System.Windows.Forms.ProgressBar();
            this.btnSignature = new System.Windows.Forms.Button();
            this.btnSignatureStoreToBin = new System.Windows.Forms.Button();
            this.rbSignatureBase64 = new System.Windows.Forms.RadioButton();
            this.rbSignatureHex = new System.Windows.Forms.RadioButton();
            this.lbSignature = new System.Windows.Forms.Label();
            this.tbSignature = new System.Windows.Forms.TextBox();
            this.gbMessage = new System.Windows.Forms.GroupBox();
            this.btnSaveMessageToBin = new System.Windows.Forms.Button();
            this.rbMessageFromFile = new System.Windows.Forms.RadioButton();
            this.rbMessageBase64 = new System.Windows.Forms.RadioButton();
            this.rbMessageHex = new System.Windows.Forms.RadioButton();
            this.rbMessageAscii = new System.Windows.Forms.RadioButton();
            this.lbMessage = new System.Windows.Forms.Label();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.gbSignatureParameters = new System.Windows.Forms.GroupBox();
            this.cbCipher = new System.Windows.Forms.ComboBox();
            this.lbCipher = new System.Windows.Forms.Label();
            this.cbDigest = new System.Windows.Forms.ComboBox();
            this.lbDigest = new System.Windows.Forms.Label();
            this.cbSignatureKeyIndex = new System.Windows.Forms.ComboBox();
            this.lbSignatureKeyIndex = new System.Windows.Forms.Label();
            this.llbDLogicURL = new System.Windows.Forms.LinkLabel();
            this.tabControl.SuspendLayout();
            this.tabRSAKeys.SuspendLayout();
            this.gbRSAPub.SuspendLayout();
            this.gbRSAModulus.SuspendLayout();
            this.gbRSAPriv.SuspendLayout();
            this.gpPrivateKeyMode.SuspendLayout();
            this.gbRSACommands.SuspendLayout();
            this.tabECKeys.SuspendLayout();
            this.gbECPubKey.SuspendLayout();
            this.gbECPrivKey.SuspendLayout();
            this.gbECDomainParameters.SuspendLayout();
            this.gbECReductionPolynomial.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbECCommands.SuspendLayout();
            this.tabSignature.SuspendLayout();
            this.gbSignatureHash.SuspendLayout();
            this.gbSignature.SuspendLayout();
            this.gbMessage.SuspendLayout();
            this.gbSignatureParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabRSAKeys);
            this.tabControl.Controls.Add(this.tabECKeys);
            this.tabControl.Controls.Add(this.tabSignature);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1142, 600);
            this.tabControl.TabIndex = 0;
            // 
            // tabRSAKeys
            // 
            this.tabRSAKeys.BackColor = System.Drawing.SystemColors.Control;
            this.tabRSAKeys.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabRSAKeys.Controls.Add(this.gbRSAPub);
            this.tabRSAKeys.Controls.Add(this.gbRSAModulus);
            this.tabRSAKeys.Controls.Add(this.gbRSAPriv);
            this.tabRSAKeys.Controls.Add(this.gbRSACommands);
            this.tabRSAKeys.Location = new System.Drawing.Point(4, 22);
            this.tabRSAKeys.Margin = new System.Windows.Forms.Padding(0);
            this.tabRSAKeys.Name = "tabRSAKeys";
            this.tabRSAKeys.Padding = new System.Windows.Forms.Padding(3);
            this.tabRSAKeys.Size = new System.Drawing.Size(1134, 574);
            this.tabRSAKeys.TabIndex = 0;
            this.tabRSAKeys.Text = "RSA Keys";
            // 
            // gbRSAPub
            // 
            this.gbRSAPub.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRSAPub.Controls.Add(this.lbRSAPubExp);
            this.gbRSAPub.Controls.Add(this.tbRSAPubExp);
            this.gbRSAPub.Location = new System.Drawing.Point(8, 381);
            this.gbRSAPub.Margin = new System.Windows.Forms.Padding(8);
            this.gbRSAPub.Name = "gbRSAPub";
            this.gbRSAPub.Size = new System.Drawing.Size(1116, 60);
            this.gbRSAPub.TabIndex = 3;
            this.gbRSAPub.TabStop = false;
            this.gbRSAPub.Text = "Public Key";
            // 
            // lbRSAPubExp
            // 
            this.lbRSAPubExp.AutoSize = true;
            this.lbRSAPubExp.Location = new System.Drawing.Point(212, 25);
            this.lbRSAPubExp.Margin = new System.Windows.Forms.Padding(3);
            this.lbRSAPubExp.Name = "lbRSAPubExp";
            this.lbRSAPubExp.Size = new System.Drawing.Size(17, 13);
            this.lbRSAPubExp.TabIndex = 0;
            this.lbRSAPubExp.Text = "E:";
            // 
            // tbRSAPubExp
            // 
            this.tbRSAPubExp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRSAPubExp.BackColor = System.Drawing.SystemColors.Window;
            this.tbRSAPubExp.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbRSAPubExp.Location = new System.Drawing.Point(233, 21);
            this.tbRSAPubExp.Margin = new System.Windows.Forms.Padding(10);
            this.tbRSAPubExp.Name = "tbRSAPubExp";
            this.tbRSAPubExp.Size = new System.Drawing.Size(870, 22);
            this.tbRSAPubExp.TabIndex = 1;
            // 
            // gbRSAModulus
            // 
            this.gbRSAModulus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRSAModulus.Controls.Add(this.lbRSAModulus);
            this.gbRSAModulus.Controls.Add(this.tbRSAModulus);
            this.gbRSAModulus.Location = new System.Drawing.Point(8, 82);
            this.gbRSAModulus.Margin = new System.Windows.Forms.Padding(8);
            this.gbRSAModulus.Name = "gbRSAModulus";
            this.gbRSAModulus.Size = new System.Drawing.Size(1116, 63);
            this.gbRSAModulus.TabIndex = 1;
            this.gbRSAModulus.TabStop = false;
            this.gbRSAModulus.Text = "Key Modulus";
            // 
            // lbRSAModulus
            // 
            this.lbRSAModulus.AutoSize = true;
            this.lbRSAModulus.Location = new System.Drawing.Point(6, 30);
            this.lbRSAModulus.Margin = new System.Windows.Forms.Padding(3);
            this.lbRSAModulus.Name = "lbRSAModulus";
            this.lbRSAModulus.Size = new System.Drawing.Size(18, 13);
            this.lbRSAModulus.TabIndex = 0;
            this.lbRSAModulus.Text = "N:";
            // 
            // tbRSAModulus
            // 
            this.tbRSAModulus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRSAModulus.BackColor = System.Drawing.SystemColors.Window;
            this.tbRSAModulus.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbRSAModulus.Location = new System.Drawing.Point(30, 26);
            this.tbRSAModulus.Margin = new System.Windows.Forms.Padding(10);
            this.tbRSAModulus.Name = "tbRSAModulus";
            this.tbRSAModulus.Size = new System.Drawing.Size(1073, 22);
            this.tbRSAModulus.TabIndex = 1;
            // 
            // gbRSAPriv
            // 
            this.gbRSAPriv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRSAPriv.Controls.Add(this.lbRSAPrivP);
            this.gbRSAPriv.Controls.Add(this.lbRSAPrivQ);
            this.gbRSAPriv.Controls.Add(this.lbRSAPrivPQ);
            this.gbRSAPriv.Controls.Add(this.lbRSAPrivDP1);
            this.gbRSAPriv.Controls.Add(this.tbRSAPrivDQ1);
            this.gbRSAPriv.Controls.Add(this.tbRSAPrivDP1);
            this.gbRSAPriv.Controls.Add(this.tbRSAPrivPQ);
            this.gbRSAPriv.Controls.Add(this.tbRSAPrivQ);
            this.gbRSAPriv.Controls.Add(this.lbRSAPrivDQ1);
            this.gbRSAPriv.Controls.Add(this.lbRSAPrivExp);
            this.gbRSAPriv.Controls.Add(this.tbRSAPrivExp);
            this.gbRSAPriv.Controls.Add(this.tbRSAPrivP);
            this.gbRSAPriv.Controls.Add(this.gpPrivateKeyMode);
            this.gbRSAPriv.Location = new System.Drawing.Point(8, 153);
            this.gbRSAPriv.Margin = new System.Windows.Forms.Padding(0);
            this.gbRSAPriv.Name = "gbRSAPriv";
            this.gbRSAPriv.Size = new System.Drawing.Size(1116, 220);
            this.gbRSAPriv.TabIndex = 2;
            this.gbRSAPriv.TabStop = false;
            // 
            // lbRSAPrivP
            // 
            this.lbRSAPrivP.AutoSize = true;
            this.lbRSAPrivP.Location = new System.Drawing.Point(213, 25);
            this.lbRSAPrivP.Name = "lbRSAPrivP";
            this.lbRSAPrivP.Size = new System.Drawing.Size(17, 13);
            this.lbRSAPrivP.TabIndex = 1;
            this.lbRSAPrivP.Text = "P:";
            // 
            // lbRSAPrivQ
            // 
            this.lbRSAPrivQ.AutoSize = true;
            this.lbRSAPrivQ.Location = new System.Drawing.Point(211, 57);
            this.lbRSAPrivQ.Name = "lbRSAPrivQ";
            this.lbRSAPrivQ.Size = new System.Drawing.Size(18, 13);
            this.lbRSAPrivQ.TabIndex = 3;
            this.lbRSAPrivQ.Text = "Q:";
            // 
            // lbRSAPrivPQ
            // 
            this.lbRSAPrivPQ.AutoSize = true;
            this.lbRSAPrivPQ.Location = new System.Drawing.Point(205, 89);
            this.lbRSAPrivPQ.Name = "lbRSAPrivPQ";
            this.lbRSAPrivPQ.Size = new System.Drawing.Size(25, 13);
            this.lbRSAPrivPQ.TabIndex = 5;
            this.lbRSAPrivPQ.Text = "PQ:";
            // 
            // lbRSAPrivDP1
            // 
            this.lbRSAPrivDP1.AutoSize = true;
            this.lbRSAPrivDP1.Location = new System.Drawing.Point(198, 121);
            this.lbRSAPrivDP1.Name = "lbRSAPrivDP1";
            this.lbRSAPrivDP1.Size = new System.Drawing.Size(31, 13);
            this.lbRSAPrivDP1.TabIndex = 7;
            this.lbRSAPrivDP1.Text = "DP1:";
            // 
            // tbRSAPrivDQ1
            // 
            this.tbRSAPrivDQ1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRSAPrivDQ1.BackColor = System.Drawing.SystemColors.Window;
            this.tbRSAPrivDQ1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbRSAPrivDQ1.Location = new System.Drawing.Point(233, 149);
            this.tbRSAPrivDQ1.Margin = new System.Windows.Forms.Padding(0);
            this.tbRSAPrivDQ1.Name = "tbRSAPrivDQ1";
            this.tbRSAPrivDQ1.Size = new System.Drawing.Size(870, 22);
            this.tbRSAPrivDQ1.TabIndex = 10;
            // 
            // tbRSAPrivDP1
            // 
            this.tbRSAPrivDP1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRSAPrivDP1.BackColor = System.Drawing.SystemColors.Window;
            this.tbRSAPrivDP1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbRSAPrivDP1.Location = new System.Drawing.Point(233, 117);
            this.tbRSAPrivDP1.Margin = new System.Windows.Forms.Padding(10);
            this.tbRSAPrivDP1.Name = "tbRSAPrivDP1";
            this.tbRSAPrivDP1.Size = new System.Drawing.Size(870, 22);
            this.tbRSAPrivDP1.TabIndex = 8;
            // 
            // tbRSAPrivPQ
            // 
            this.tbRSAPrivPQ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRSAPrivPQ.BackColor = System.Drawing.SystemColors.Window;
            this.tbRSAPrivPQ.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbRSAPrivPQ.Location = new System.Drawing.Point(233, 85);
            this.tbRSAPrivPQ.Margin = new System.Windows.Forms.Padding(0);
            this.tbRSAPrivPQ.Name = "tbRSAPrivPQ";
            this.tbRSAPrivPQ.Size = new System.Drawing.Size(870, 22);
            this.tbRSAPrivPQ.TabIndex = 6;
            // 
            // tbRSAPrivQ
            // 
            this.tbRSAPrivQ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRSAPrivQ.BackColor = System.Drawing.SystemColors.Window;
            this.tbRSAPrivQ.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbRSAPrivQ.Location = new System.Drawing.Point(233, 53);
            this.tbRSAPrivQ.Margin = new System.Windows.Forms.Padding(10);
            this.tbRSAPrivQ.Name = "tbRSAPrivQ";
            this.tbRSAPrivQ.Size = new System.Drawing.Size(870, 22);
            this.tbRSAPrivQ.TabIndex = 4;
            // 
            // lbRSAPrivDQ1
            // 
            this.lbRSAPrivDQ1.AutoSize = true;
            this.lbRSAPrivDQ1.Location = new System.Drawing.Point(198, 153);
            this.lbRSAPrivDQ1.Name = "lbRSAPrivDQ1";
            this.lbRSAPrivDQ1.Size = new System.Drawing.Size(32, 13);
            this.lbRSAPrivDQ1.TabIndex = 9;
            this.lbRSAPrivDQ1.Text = "DQ1:";
            // 
            // lbRSAPrivExp
            // 
            this.lbRSAPrivExp.AutoSize = true;
            this.lbRSAPrivExp.Enabled = false;
            this.lbRSAPrivExp.Location = new System.Drawing.Point(7, 187);
            this.lbRSAPrivExp.Margin = new System.Windows.Forms.Padding(3);
            this.lbRSAPrivExp.Name = "lbRSAPrivExp";
            this.lbRSAPrivExp.Size = new System.Drawing.Size(18, 13);
            this.lbRSAPrivExp.TabIndex = 11;
            this.lbRSAPrivExp.Text = "D:";
            // 
            // tbRSAPrivExp
            // 
            this.tbRSAPrivExp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRSAPrivExp.BackColor = System.Drawing.SystemColors.Window;
            this.tbRSAPrivExp.Enabled = false;
            this.tbRSAPrivExp.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbRSAPrivExp.Location = new System.Drawing.Point(31, 183);
            this.tbRSAPrivExp.Name = "tbRSAPrivExp";
            this.tbRSAPrivExp.Size = new System.Drawing.Size(1072, 22);
            this.tbRSAPrivExp.TabIndex = 12;
            // 
            // tbRSAPrivP
            // 
            this.tbRSAPrivP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRSAPrivP.BackColor = System.Drawing.SystemColors.Window;
            this.tbRSAPrivP.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbRSAPrivP.Location = new System.Drawing.Point(233, 21);
            this.tbRSAPrivP.Margin = new System.Windows.Forms.Padding(0);
            this.tbRSAPrivP.Name = "tbRSAPrivP";
            this.tbRSAPrivP.Size = new System.Drawing.Size(870, 22);
            this.tbRSAPrivP.TabIndex = 2;
            // 
            // gpPrivateKeyMode
            // 
            this.gpPrivateKeyMode.Controls.Add(this.rbCRT);
            this.gpPrivateKeyMode.Controls.Add(this.rbModExp);
            this.gpPrivateKeyMode.Location = new System.Drawing.Point(10, 14);
            this.gpPrivateKeyMode.Name = "gpPrivateKeyMode";
            this.gpPrivateKeyMode.Size = new System.Drawing.Size(180, 88);
            this.gpPrivateKeyMode.TabIndex = 0;
            this.gpPrivateKeyMode.TabStop = false;
            this.gpPrivateKeyMode.Text = "Private key mode:";
            // 
            // rbCRT
            // 
            this.rbCRT.AutoSize = true;
            this.rbCRT.Checked = true;
            this.rbCRT.Location = new System.Drawing.Point(12, 26);
            this.rbCRT.Margin = new System.Windows.Forms.Padding(10);
            this.rbCRT.Name = "rbCRT";
            this.rbCRT.Size = new System.Drawing.Size(162, 17);
            this.rbCRT.TabIndex = 0;
            this.rbCRT.TabStop = true;
            this.rbCRT.Text = "Chinese Remainder Theorem";
            this.rbCRT.UseVisualStyleBackColor = true;
            this.rbCRT.CheckedChanged += new System.EventHandler(this.rbCRT_CheckedChanged);
            // 
            // rbModExp
            // 
            this.rbModExp.AutoSize = true;
            this.rbModExp.Location = new System.Drawing.Point(12, 53);
            this.rbModExp.Margin = new System.Windows.Forms.Padding(0);
            this.rbModExp.Name = "rbModExp";
            this.rbModExp.Size = new System.Drawing.Size(115, 17);
            this.rbModExp.TabIndex = 1;
            this.rbModExp.Text = "Modulus/Exponent";
            this.rbModExp.UseVisualStyleBackColor = true;
            // 
            // gbRSACommands
            // 
            this.gbRSACommands.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRSACommands.Controls.Add(this.btnRestoreRSAPriv);
            this.gbRSACommands.Controls.Add(this.btnBackupRSAPriv);
            this.gbRSACommands.Controls.Add(this.btnSaveRSAPub);
            this.gbRSACommands.Controls.Add(this.btnStoreRSAPriv);
            this.gbRSACommands.Controls.Add(this.btnMkRSAKey);
            this.gbRSACommands.Controls.Add(this.cbRSAKeyLength);
            this.gbRSACommands.Controls.Add(this.cbRSAKeyIndex);
            this.gbRSACommands.Controls.Add(this.lbRSAKeyLength);
            this.gbRSACommands.Controls.Add(this.lbRSAKeyIndex);
            this.gbRSACommands.Controls.Add(this.btnRSAImportP12);
            this.gbRSACommands.Location = new System.Drawing.Point(8, 0);
            this.gbRSACommands.Margin = new System.Windows.Forms.Padding(8);
            this.gbRSACommands.Name = "gbRSACommands";
            this.gbRSACommands.Size = new System.Drawing.Size(1116, 74);
            this.gbRSACommands.TabIndex = 0;
            this.gbRSACommands.TabStop = false;
            // 
            // btnRestoreRSAPriv
            // 
            this.btnRestoreRSAPriv.Location = new System.Drawing.Point(573, 43);
            this.btnRestoreRSAPriv.Margin = new System.Windows.Forms.Padding(0);
            this.btnRestoreRSAPriv.Name = "btnRestoreRSAPriv";
            this.btnRestoreRSAPriv.Size = new System.Drawing.Size(160, 21);
            this.btnRestoreRSAPriv.TabIndex = 9;
            this.btnRestoreRSAPriv.Text = "Restore private key from pem";
            this.btnRestoreRSAPriv.UseVisualStyleBackColor = true;
            this.btnRestoreRSAPriv.Click += new System.EventHandler(this.btnRestoreRSAPriv_Click);
            // 
            // btnBackupRSAPriv
            // 
            this.btnBackupRSAPriv.BackColor = System.Drawing.SystemColors.Control;
            this.btnBackupRSAPriv.Location = new System.Drawing.Point(573, 16);
            this.btnBackupRSAPriv.Margin = new System.Windows.Forms.Padding(0);
            this.btnBackupRSAPriv.Name = "btnBackupRSAPriv";
            this.btnBackupRSAPriv.Size = new System.Drawing.Size(160, 21);
            this.btnBackupRSAPriv.TabIndex = 8;
            this.btnBackupRSAPriv.Text = "Backup private key to pem";
            this.btnBackupRSAPriv.UseVisualStyleBackColor = true;
            this.btnBackupRSAPriv.Click += new System.EventHandler(this.btnBackupRSAPriv_Click);
            // 
            // btnSaveRSAPub
            // 
            this.btnSaveRSAPub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveRSAPub.Location = new System.Drawing.Point(943, 42);
            this.btnSaveRSAPub.Margin = new System.Windows.Forms.Padding(10);
            this.btnSaveRSAPub.Name = "btnSaveRSAPub";
            this.btnSaveRSAPub.Size = new System.Drawing.Size(160, 21);
            this.btnSaveRSAPub.TabIndex = 7;
            this.btnSaveRSAPub.Text = "Save RSA public key to pem";
            this.btnSaveRSAPub.UseVisualStyleBackColor = true;
            this.btnSaveRSAPub.Click += new System.EventHandler(this.btnSaveRSAPub_Click);
            // 
            // btnStoreRSAPriv
            // 
            this.btnStoreRSAPriv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStoreRSAPriv.BackColor = System.Drawing.Color.Azure;
            this.btnStoreRSAPriv.Location = new System.Drawing.Point(943, 15);
            this.btnStoreRSAPriv.Margin = new System.Windows.Forms.Padding(10);
            this.btnStoreRSAPriv.Name = "btnStoreRSAPriv";
            this.btnStoreRSAPriv.Size = new System.Drawing.Size(160, 21);
            this.btnStoreRSAPriv.TabIndex = 6;
            this.btnStoreRSAPriv.Text = "Store RSA private key to card";
            this.btnStoreRSAPriv.UseVisualStyleBackColor = false;
            this.btnStoreRSAPriv.Click += new System.EventHandler(this.btnStoreRSAPriv_Click);
            // 
            // btnMkRSAKey
            // 
            this.btnMkRSAKey.Location = new System.Drawing.Point(403, 15);
            this.btnMkRSAKey.Margin = new System.Windows.Forms.Padding(10);
            this.btnMkRSAKey.Name = "btnMkRSAKey";
            this.btnMkRSAKey.Size = new System.Drawing.Size(160, 48);
            this.btnMkRSAKey.TabIndex = 1;
            this.btnMkRSAKey.Text = "Generate RSA key pair (off-card)";
            this.btnMkRSAKey.UseVisualStyleBackColor = true;
            this.btnMkRSAKey.Click += new System.EventHandler(this.btnMkRSAKey_Click);
            // 
            // cbRSAKeyLength
            // 
            this.cbRSAKeyLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRSAKeyLength.FormattingEnabled = true;
            this.cbRSAKeyLength.Items.AddRange(new object[] {
            "512",
            "736",
            "768",
            "896",
            "1024",
            "1280",
            "1536",
            "1984",
            "2048",
            "4096"});
            this.cbRSAKeyLength.Location = new System.Drawing.Point(99, 16);
            this.cbRSAKeyLength.Name = "cbRSAKeyLength";
            this.cbRSAKeyLength.Size = new System.Drawing.Size(121, 21);
            this.cbRSAKeyLength.TabIndex = 3;
            // 
            // cbRSAKeyIndex
            // 
            this.cbRSAKeyIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRSAKeyIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRSAKeyIndex.FormattingEnabled = true;
            this.cbRSAKeyIndex.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.cbRSAKeyIndex.Location = new System.Drawing.Point(880, 16);
            this.cbRSAKeyIndex.Name = "cbRSAKeyIndex";
            this.cbRSAKeyIndex.Size = new System.Drawing.Size(50, 21);
            this.cbRSAKeyIndex.TabIndex = 5;
            // 
            // lbRSAKeyLength
            // 
            this.lbRSAKeyLength.AutoSize = true;
            this.lbRSAKeyLength.Location = new System.Drawing.Point(8, 19);
            this.lbRSAKeyLength.Name = "lbRSAKeyLength";
            this.lbRSAKeyLength.Size = new System.Drawing.Size(85, 13);
            this.lbRSAKeyLength.TabIndex = 2;
            this.lbRSAKeyLength.Text = "Key length [bits]:";
            // 
            // lbRSAKeyIndex
            // 
            this.lbRSAKeyIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbRSAKeyIndex.AutoSize = true;
            this.lbRSAKeyIndex.Location = new System.Drawing.Point(777, 19);
            this.lbRSAKeyIndex.Name = "lbRSAKeyIndex";
            this.lbRSAKeyIndex.Size = new System.Drawing.Size(97, 13);
            this.lbRSAKeyIndex.TabIndex = 4;
            this.lbRSAKeyIndex.Text = "Key index (in card):";
            // 
            // btnRSAImportP12
            // 
            this.btnRSAImportP12.Location = new System.Drawing.Point(233, 15);
            this.btnRSAImportP12.Margin = new System.Windows.Forms.Padding(10);
            this.btnRSAImportP12.Name = "btnRSAImportP12";
            this.btnRSAImportP12.Size = new System.Drawing.Size(160, 48);
            this.btnRSAImportP12.TabIndex = 0;
            this.btnRSAImportP12.Text = "Import from PKCS#12 file    (.p12 ;  .pfx)";
            this.btnRSAImportP12.UseVisualStyleBackColor = true;
            this.btnRSAImportP12.Click += new System.EventHandler(this.btnRSAImportP12_Click);
            // 
            // tabECKeys
            // 
            this.tabECKeys.BackColor = System.Drawing.SystemColors.Control;
            this.tabECKeys.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabECKeys.Controls.Add(this.gbECPubKey);
            this.tabECKeys.Controls.Add(this.gbECPrivKey);
            this.tabECKeys.Controls.Add(this.gbECDomainParameters);
            this.tabECKeys.Controls.Add(this.gbECCommands);
            this.tabECKeys.Location = new System.Drawing.Point(4, 22);
            this.tabECKeys.Name = "tabECKeys";
            this.tabECKeys.Padding = new System.Windows.Forms.Padding(3);
            this.tabECKeys.Size = new System.Drawing.Size(1134, 574);
            this.tabECKeys.TabIndex = 1;
            this.tabECKeys.Text = "EC Keys";
            // 
            // gbECPubKey
            // 
            this.gbECPubKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbECPubKey.Controls.Add(this.lbECPubKey);
            this.gbECPubKey.Controls.Add(this.tbECPubKey);
            this.gbECPubKey.Location = new System.Drawing.Point(8, 443);
            this.gbECPubKey.Margin = new System.Windows.Forms.Padding(0);
            this.gbECPubKey.Name = "gbECPubKey";
            this.gbECPubKey.Size = new System.Drawing.Size(1116, 104);
            this.gbECPubKey.TabIndex = 6;
            this.gbECPubKey.TabStop = false;
            this.gbECPubKey.Text = "EC public key:";
            // 
            // lbECPubKey
            // 
            this.lbECPubKey.AutoSize = true;
            this.lbECPubKey.Location = new System.Drawing.Point(8, 48);
            this.lbECPubKey.Margin = new System.Windows.Forms.Padding(3);
            this.lbECPubKey.Name = "lbECPubKey";
            this.lbECPubKey.Size = new System.Drawing.Size(39, 13);
            this.lbECPubKey.TabIndex = 17;
            this.lbECPubKey.Text = "W(uc):";
            // 
            // tbECPubKey
            // 
            this.tbECPubKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbECPubKey.BackColor = System.Drawing.SystemColors.Window;
            this.tbECPubKey.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbECPubKey.Location = new System.Drawing.Point(53, 22);
            this.tbECPubKey.Multiline = true;
            this.tbECPubKey.Name = "tbECPubKey";
            this.tbECPubKey.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.tbECPubKey.Size = new System.Drawing.Size(1049, 65);
            this.tbECPubKey.TabIndex = 18;
            // 
            // gbECPrivKey
            // 
            this.gbECPrivKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbECPrivKey.Controls.Add(this.lbECPrivKey);
            this.gbECPrivKey.Controls.Add(this.tbECPrivKey);
            this.gbECPrivKey.Location = new System.Drawing.Point(8, 373);
            this.gbECPrivKey.Margin = new System.Windows.Forms.Padding(8);
            this.gbECPrivKey.Name = "gbECPrivKey";
            this.gbECPrivKey.Size = new System.Drawing.Size(1116, 62);
            this.gbECPrivKey.TabIndex = 5;
            this.gbECPrivKey.TabStop = false;
            this.gbECPrivKey.Text = "EC private key:";
            // 
            // lbECPrivKey
            // 
            this.lbECPrivKey.AutoSize = true;
            this.lbECPrivKey.Location = new System.Drawing.Point(9, 25);
            this.lbECPrivKey.Margin = new System.Windows.Forms.Padding(3);
            this.lbECPrivKey.Name = "lbECPrivKey";
            this.lbECPrivKey.Size = new System.Drawing.Size(17, 13);
            this.lbECPrivKey.TabIndex = 0;
            this.lbECPrivKey.Text = "S:";
            // 
            // tbECPrivKey
            // 
            this.tbECPrivKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbECPrivKey.BackColor = System.Drawing.SystemColors.Window;
            this.tbECPrivKey.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbECPrivKey.Location = new System.Drawing.Point(32, 21);
            this.tbECPrivKey.Name = "tbECPrivKey";
            this.tbECPrivKey.Size = new System.Drawing.Size(1071, 22);
            this.tbECPrivKey.TabIndex = 1;
            // 
            // gbECDomainParameters
            // 
            this.gbECDomainParameters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbECDomainParameters.Controls.Add(this.lbECParamK);
            this.gbECDomainParameters.Controls.Add(this.tbECParamK);
            this.gbECDomainParameters.Controls.Add(this.lbECParamR);
            this.gbECDomainParameters.Controls.Add(this.tbECParamR);
            this.gbECDomainParameters.Controls.Add(this.lbECParamG);
            this.gbECDomainParameters.Controls.Add(this.tbECParamG);
            this.gbECDomainParameters.Controls.Add(this.lbECParamB);
            this.gbECDomainParameters.Controls.Add(this.tbECParamB);
            this.gbECDomainParameters.Controls.Add(this.ltbECParamA);
            this.gbECDomainParameters.Controls.Add(this.tbECParamA);
            this.gbECDomainParameters.Controls.Add(this.gbECReductionPolynomial);
            this.gbECDomainParameters.Controls.Add(this.groupBox1);
            this.gbECDomainParameters.Controls.Add(this.lbECParamPrime);
            this.gbECDomainParameters.Controls.Add(this.tbECParamPrime);
            this.gbECDomainParameters.Location = new System.Drawing.Point(8, 82);
            this.gbECDomainParameters.Margin = new System.Windows.Forms.Padding(0);
            this.gbECDomainParameters.Name = "gbECDomainParameters";
            this.gbECDomainParameters.Size = new System.Drawing.Size(1116, 283);
            this.gbECDomainParameters.TabIndex = 4;
            this.gbECDomainParameters.TabStop = false;
            this.gbECDomainParameters.Text = "EC domain parameters:";
            // 
            // lbECParamK
            // 
            this.lbECParamK.AutoSize = true;
            this.lbECParamK.Location = new System.Drawing.Point(260, 239);
            this.lbECParamK.Margin = new System.Windows.Forms.Padding(3);
            this.lbECParamK.Name = "lbECParamK";
            this.lbECParamK.Size = new System.Drawing.Size(17, 13);
            this.lbECParamK.TabIndex = 19;
            this.lbECParamK.Text = "K:";
            // 
            // tbECParamK
            // 
            this.tbECParamK.BackColor = System.Drawing.SystemColors.Window;
            this.tbECParamK.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbECParamK.Location = new System.Drawing.Point(284, 235);
            this.tbECParamK.Margin = new System.Windows.Forms.Padding(0);
            this.tbECParamK.Name = "tbECParamK";
            this.tbECParamK.ReadOnly = true;
            this.tbECParamK.Size = new System.Drawing.Size(139, 22);
            this.tbECParamK.TabIndex = 20;
            // 
            // lbECParamR
            // 
            this.lbECParamR.AutoSize = true;
            this.lbECParamR.Location = new System.Drawing.Point(260, 207);
            this.lbECParamR.Margin = new System.Windows.Forms.Padding(3);
            this.lbECParamR.Name = "lbECParamR";
            this.lbECParamR.Size = new System.Drawing.Size(18, 13);
            this.lbECParamR.TabIndex = 17;
            this.lbECParamR.Text = "R:";
            // 
            // tbECParamR
            // 
            this.tbECParamR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbECParamR.BackColor = System.Drawing.SystemColors.Window;
            this.tbECParamR.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbECParamR.Location = new System.Drawing.Point(284, 203);
            this.tbECParamR.Margin = new System.Windows.Forms.Padding(10);
            this.tbECParamR.Name = "tbECParamR";
            this.tbECParamR.ReadOnly = true;
            this.tbECParamR.Size = new System.Drawing.Size(819, 22);
            this.tbECParamR.TabIndex = 18;
            // 
            // lbECParamG
            // 
            this.lbECParamG.AutoSize = true;
            this.lbECParamG.Location = new System.Drawing.Point(242, 152);
            this.lbECParamG.Margin = new System.Windows.Forms.Padding(3);
            this.lbECParamG.Name = "lbECParamG";
            this.lbECParamG.Size = new System.Drawing.Size(36, 13);
            this.lbECParamG.TabIndex = 15;
            this.lbECParamG.Text = "G(uc):";
            // 
            // tbECParamG
            // 
            this.tbECParamG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbECParamG.BackColor = System.Drawing.SystemColors.Window;
            this.tbECParamG.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbECParamG.Location = new System.Drawing.Point(284, 126);
            this.tbECParamG.Margin = new System.Windows.Forms.Padding(0);
            this.tbECParamG.Multiline = true;
            this.tbECParamG.Name = "tbECParamG";
            this.tbECParamG.ReadOnly = true;
            this.tbECParamG.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.tbECParamG.Size = new System.Drawing.Size(819, 65);
            this.tbECParamG.TabIndex = 16;
            // 
            // lbECParamB
            // 
            this.lbECParamB.AutoSize = true;
            this.lbECParamB.Location = new System.Drawing.Point(212, 94);
            this.lbECParamB.Margin = new System.Windows.Forms.Padding(3);
            this.lbECParamB.Name = "lbECParamB";
            this.lbECParamB.Size = new System.Drawing.Size(16, 13);
            this.lbECParamB.TabIndex = 13;
            this.lbECParamB.Text = "b:";
            // 
            // tbECParamB
            // 
            this.tbECParamB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbECParamB.BackColor = System.Drawing.SystemColors.Window;
            this.tbECParamB.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbECParamB.Location = new System.Drawing.Point(233, 90);
            this.tbECParamB.Margin = new System.Windows.Forms.Padding(14);
            this.tbECParamB.Name = "tbECParamB";
            this.tbECParamB.ReadOnly = true;
            this.tbECParamB.Size = new System.Drawing.Size(870, 22);
            this.tbECParamB.TabIndex = 14;
            // 
            // ltbECParamA
            // 
            this.ltbECParamA.AutoSize = true;
            this.ltbECParamA.Location = new System.Drawing.Point(212, 64);
            this.ltbECParamA.Margin = new System.Windows.Forms.Padding(3);
            this.ltbECParamA.Name = "ltbECParamA";
            this.ltbECParamA.Size = new System.Drawing.Size(16, 13);
            this.ltbECParamA.TabIndex = 11;
            this.ltbECParamA.Text = "a:";
            // 
            // tbECParamA
            // 
            this.tbECParamA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbECParamA.BackColor = System.Drawing.SystemColors.Window;
            this.tbECParamA.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbECParamA.Location = new System.Drawing.Point(233, 60);
            this.tbECParamA.Margin = new System.Windows.Forms.Padding(10);
            this.tbECParamA.Name = "tbECParamA";
            this.tbECParamA.ReadOnly = true;
            this.tbECParamA.Size = new System.Drawing.Size(870, 22);
            this.tbECParamA.TabIndex = 12;
            // 
            // gbECReductionPolynomial
            // 
            this.gbECReductionPolynomial.Controls.Add(this.rtbECReductionPolynomial);
            this.gbECReductionPolynomial.Controls.Add(this.lbECParamE3);
            this.gbECReductionPolynomial.Controls.Add(this.tbECParamE3);
            this.gbECReductionPolynomial.Controls.Add(this.lbECParamE2);
            this.gbECReductionPolynomial.Controls.Add(this.tbECParamE2);
            this.gbECReductionPolynomial.Controls.Add(this.lbECParamE1);
            this.gbECReductionPolynomial.Controls.Add(this.tbECParamE1);
            this.gbECReductionPolynomial.Enabled = false;
            this.gbECReductionPolynomial.Location = new System.Drawing.Point(11, 120);
            this.gbECReductionPolynomial.Margin = new System.Windows.Forms.Padding(8);
            this.gbECReductionPolynomial.Name = "gbECReductionPolynomial";
            this.gbECReductionPolynomial.Size = new System.Drawing.Size(220, 151);
            this.gbECReductionPolynomial.TabIndex = 10;
            this.gbECReductionPolynomial.TabStop = false;
            this.gbECReductionPolynomial.Text = "Reduction polynomial:";
            // 
            // rtbECReductionPolynomial
            // 
            this.rtbECReductionPolynomial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbECReductionPolynomial.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rtbECReductionPolynomial.Location = new System.Drawing.Point(12, 22);
            this.rtbECReductionPolynomial.Multiline = false;
            this.rtbECReductionPolynomial.Name = "rtbECReductionPolynomial";
            this.rtbECReductionPolynomial.ReadOnly = true;
            this.rtbECReductionPolynomial.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtbECReductionPolynomial.Size = new System.Drawing.Size(202, 29);
            this.rtbECReductionPolynomial.TabIndex = 17;
            this.rtbECReductionPolynomial.Text = " f(x) = ";
            // 
            // lbECParamE3
            // 
            this.lbECParamE3.AutoSize = true;
            this.lbECParamE3.Location = new System.Drawing.Point(12, 119);
            this.lbECParamE3.Margin = new System.Windows.Forms.Padding(3);
            this.lbECParamE3.Name = "lbECParamE3";
            this.lbECParamE3.Size = new System.Drawing.Size(22, 13);
            this.lbECParamE3.TabIndex = 15;
            this.lbECParamE3.Text = "e3:";
            // 
            // tbECParamE3
            // 
            this.tbECParamE3.BackColor = System.Drawing.SystemColors.Window;
            this.tbECParamE3.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbECParamE3.Location = new System.Drawing.Point(41, 115);
            this.tbECParamE3.Margin = new System.Windows.Forms.Padding(0);
            this.tbECParamE3.Name = "tbECParamE3";
            this.tbECParamE3.ReadOnly = true;
            this.tbECParamE3.Size = new System.Drawing.Size(139, 22);
            this.tbECParamE3.TabIndex = 16;
            // 
            // lbECParamE2
            // 
            this.lbECParamE2.AutoSize = true;
            this.lbECParamE2.Location = new System.Drawing.Point(12, 87);
            this.lbECParamE2.Margin = new System.Windows.Forms.Padding(3);
            this.lbECParamE2.Name = "lbECParamE2";
            this.lbECParamE2.Size = new System.Drawing.Size(22, 13);
            this.lbECParamE2.TabIndex = 13;
            this.lbECParamE2.Text = "e2:";
            // 
            // tbECParamE2
            // 
            this.tbECParamE2.BackColor = System.Drawing.SystemColors.Window;
            this.tbECParamE2.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbECParamE2.Location = new System.Drawing.Point(41, 83);
            this.tbECParamE2.Margin = new System.Windows.Forms.Padding(10);
            this.tbECParamE2.Name = "tbECParamE2";
            this.tbECParamE2.ReadOnly = true;
            this.tbECParamE2.Size = new System.Drawing.Size(139, 22);
            this.tbECParamE2.TabIndex = 14;
            // 
            // lbECParamE1
            // 
            this.lbECParamE1.AutoSize = true;
            this.lbECParamE1.Location = new System.Drawing.Point(12, 55);
            this.lbECParamE1.Margin = new System.Windows.Forms.Padding(3);
            this.lbECParamE1.Name = "lbECParamE1";
            this.lbECParamE1.Size = new System.Drawing.Size(22, 13);
            this.lbECParamE1.TabIndex = 11;
            this.lbECParamE1.Text = "e1:";
            // 
            // tbECParamE1
            // 
            this.tbECParamE1.BackColor = System.Drawing.SystemColors.Window;
            this.tbECParamE1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbECParamE1.Location = new System.Drawing.Point(41, 51);
            this.tbECParamE1.Margin = new System.Windows.Forms.Padding(10);
            this.tbECParamE1.Name = "tbECParamE1";
            this.tbECParamE1.ReadOnly = true;
            this.tbECParamE1.Size = new System.Drawing.Size(139, 22);
            this.tbECParamE1.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbECFieldPrime);
            this.groupBox1.Controls.Add(this.rbECFieldBinary);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(11, 24);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 88);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "EC over field:";
            // 
            // rbECFieldPrime
            // 
            this.rbECFieldPrime.AutoSize = true;
            this.rbECFieldPrime.Checked = true;
            this.rbECFieldPrime.Location = new System.Drawing.Point(12, 26);
            this.rbECFieldPrime.Margin = new System.Windows.Forms.Padding(10);
            this.rbECFieldPrime.Name = "rbECFieldPrime";
            this.rbECFieldPrime.Size = new System.Drawing.Size(102, 17);
            this.rbECFieldPrime.TabIndex = 0;
            this.rbECFieldPrime.TabStop = true;
            this.rbECFieldPrime.Text = "Prime field { Fp }";
            this.rbECFieldPrime.UseVisualStyleBackColor = true;
            this.rbECFieldPrime.CheckedChanged += new System.EventHandler(this.rbECFieldPrime_CheckedChanged);
            // 
            // rbECFieldBinary
            // 
            this.rbECFieldBinary.AutoSize = true;
            this.rbECFieldBinary.Location = new System.Drawing.Point(12, 53);
            this.rbECFieldBinary.Margin = new System.Windows.Forms.Padding(0);
            this.rbECFieldBinary.Name = "rbECFieldBinary";
            this.rbECFieldBinary.Size = new System.Drawing.Size(113, 17);
            this.rbECFieldBinary.TabIndex = 1;
            this.rbECFieldBinary.Text = "Binary field { F2m }";
            this.rbECFieldBinary.UseVisualStyleBackColor = true;
            this.rbECFieldBinary.CheckedChanged += new System.EventHandler(this.rbECFieldBinary_CheckedChanged);
            // 
            // lbECParamPrime
            // 
            this.lbECParamPrime.AutoSize = true;
            this.lbECParamPrime.Location = new System.Drawing.Point(212, 34);
            this.lbECParamPrime.Margin = new System.Windows.Forms.Padding(3);
            this.lbECParamPrime.Name = "lbECParamPrime";
            this.lbECParamPrime.Size = new System.Drawing.Size(16, 13);
            this.lbECParamPrime.TabIndex = 0;
            this.lbECParamPrime.Text = "p:";
            // 
            // tbECParamPrime
            // 
            this.tbECParamPrime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbECParamPrime.BackColor = System.Drawing.SystemColors.Window;
            this.tbECParamPrime.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbECParamPrime.Location = new System.Drawing.Point(233, 30);
            this.tbECParamPrime.Margin = new System.Windows.Forms.Padding(10);
            this.tbECParamPrime.Name = "tbECParamPrime";
            this.tbECParamPrime.ReadOnly = true;
            this.tbECParamPrime.Size = new System.Drawing.Size(870, 22);
            this.tbECParamPrime.TabIndex = 1;
            // 
            // gbECCommands
            // 
            this.gbECCommands.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbECCommands.Controls.Add(this.btnRestoreECPriv);
            this.gbECCommands.Controls.Add(this.btnBackupECPriv);
            this.gbECCommands.Controls.Add(this.cbECName);
            this.gbECCommands.Controls.Add(this.lbECName);
            this.gbECCommands.Controls.Add(this.btnSaveECPub);
            this.gbECCommands.Controls.Add(this.btnStoreECPriv);
            this.gbECCommands.Controls.Add(this.btnMkECKey);
            this.gbECCommands.Controls.Add(this.cbECKeyLength);
            this.gbECCommands.Controls.Add(this.cbECKeyIndex);
            this.gbECCommands.Controls.Add(this.lbECKeyLength);
            this.gbECCommands.Controls.Add(this.lbECKeyIndex);
            this.gbECCommands.Controls.Add(this.btnECImportP12);
            this.gbECCommands.Location = new System.Drawing.Point(8, 0);
            this.gbECCommands.Margin = new System.Windows.Forms.Padding(8);
            this.gbECCommands.Name = "gbECCommands";
            this.gbECCommands.Size = new System.Drawing.Size(1116, 74);
            this.gbECCommands.TabIndex = 1;
            this.gbECCommands.TabStop = false;
            // 
            // btnRestoreECPriv
            // 
            this.btnRestoreECPriv.Location = new System.Drawing.Point(573, 43);
            this.btnRestoreECPriv.Margin = new System.Windows.Forms.Padding(0);
            this.btnRestoreECPriv.Name = "btnRestoreECPriv";
            this.btnRestoreECPriv.Size = new System.Drawing.Size(160, 21);
            this.btnRestoreECPriv.TabIndex = 11;
            this.btnRestoreECPriv.Text = "Restore private key from pem";
            this.btnRestoreECPriv.UseVisualStyleBackColor = true;
            this.btnRestoreECPriv.Click += new System.EventHandler(this.btnRestoreECPriv_Click);
            // 
            // btnBackupECPriv
            // 
            this.btnBackupECPriv.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnBackupECPriv.Location = new System.Drawing.Point(573, 16);
            this.btnBackupECPriv.Margin = new System.Windows.Forms.Padding(0);
            this.btnBackupECPriv.Name = "btnBackupECPriv";
            this.btnBackupECPriv.Size = new System.Drawing.Size(160, 21);
            this.btnBackupECPriv.TabIndex = 10;
            this.btnBackupECPriv.Text = "Backup private key to pem";
            this.btnBackupECPriv.UseVisualStyleBackColor = true;
            this.btnBackupECPriv.Click += new System.EventHandler(this.btnBackupECPriv_Click);
            // 
            // cbECName
            // 
            this.cbECName.DropDownHeight = 432;
            this.cbECName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbECName.FormattingEnabled = true;
            this.cbECName.IntegralHeight = false;
            this.cbECName.Items.AddRange(new object[] {
            "secp112r1",
            "secp112r2",
            "secp128r1",
            "secp128r2",
            "secp160k1",
            "secp160r1",
            "secp160r2",
            "secp192k1",
            "secp192r1",
            "secp224k1",
            "secp224r1",
            "secp256k1",
            "secp256r1",
            "secp384r1",
            "secp521r1",
            "sect113r1",
            "sect113r2",
            "sect131r1",
            "sect131r2",
            "sect163k1",
            "sect163r1",
            "sect163r2",
            "sect193r1",
            "sect193r2",
            "sect233k1",
            "sect233r1",
            "sect239k1",
            "sect283k1",
            "sect283r1",
            "sect409k1",
            "sect409r1",
            "sect571k1",
            "sect571r1"});
            this.cbECName.Location = new System.Drawing.Point(99, 16);
            this.cbECName.Margin = new System.Windows.Forms.Padding(2);
            this.cbECName.Name = "cbECName";
            this.cbECName.Size = new System.Drawing.Size(122, 21);
            this.cbECName.TabIndex = 9;
            this.cbECName.SelectedIndexChanged += new System.EventHandler(this.cbECName_SelectedIndexChanged);
            // 
            // lbECName
            // 
            this.lbECName.AutoSize = true;
            this.lbECName.Location = new System.Drawing.Point(8, 19);
            this.lbECName.Name = "lbECName";
            this.lbECName.Size = new System.Drawing.Size(67, 13);
            this.lbECName.TabIndex = 8;
            this.lbECName.Text = "Curve name:";
            // 
            // btnSaveECPub
            // 
            this.btnSaveECPub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveECPub.Location = new System.Drawing.Point(943, 42);
            this.btnSaveECPub.Margin = new System.Windows.Forms.Padding(10);
            this.btnSaveECPub.Name = "btnSaveECPub";
            this.btnSaveECPub.Size = new System.Drawing.Size(160, 21);
            this.btnSaveECPub.TabIndex = 7;
            this.btnSaveECPub.Text = "Save EC public key to pem";
            this.btnSaveECPub.UseVisualStyleBackColor = true;
            this.btnSaveECPub.Click += new System.EventHandler(this.btnSaveECPub_Click);
            // 
            // btnStoreECPriv
            // 
            this.btnStoreECPriv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStoreECPriv.BackColor = System.Drawing.Color.Azure;
            this.btnStoreECPriv.Location = new System.Drawing.Point(943, 15);
            this.btnStoreECPriv.Margin = new System.Windows.Forms.Padding(10);
            this.btnStoreECPriv.Name = "btnStoreECPriv";
            this.btnStoreECPriv.Size = new System.Drawing.Size(160, 21);
            this.btnStoreECPriv.TabIndex = 6;
            this.btnStoreECPriv.Text = "Store EC private key to card";
            this.btnStoreECPriv.UseVisualStyleBackColor = false;
            this.btnStoreECPriv.Click += new System.EventHandler(this.btnStoreECPriv_Click);
            // 
            // btnMkECKey
            // 
            this.btnMkECKey.Location = new System.Drawing.Point(403, 15);
            this.btnMkECKey.Margin = new System.Windows.Forms.Padding(10);
            this.btnMkECKey.Name = "btnMkECKey";
            this.btnMkECKey.Size = new System.Drawing.Size(160, 48);
            this.btnMkECKey.TabIndex = 1;
            this.btnMkECKey.Text = "Generate EC key pair (off-card)";
            this.btnMkECKey.UseVisualStyleBackColor = true;
            this.btnMkECKey.Click += new System.EventHandler(this.btnMkECKey_Click);
            // 
            // cbECKeyLength
            // 
            this.cbECKeyLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbECKeyLength.FormattingEnabled = true;
            this.cbECKeyLength.Items.AddRange(new object[] {
            "112",
            "113",
            "128",
            "131",
            "160",
            "163",
            "192",
            "193",
            "224",
            "233",
            "239",
            "256",
            "283",
            "384",
            "409",
            "521",
            "571"});
            this.cbECKeyLength.Location = new System.Drawing.Point(99, 42);
            this.cbECKeyLength.Name = "cbECKeyLength";
            this.cbECKeyLength.Size = new System.Drawing.Size(122, 21);
            this.cbECKeyLength.TabIndex = 3;
            this.cbECKeyLength.SelectedIndexChanged += new System.EventHandler(this.cbECKeyLength_SelectedIndexChanged);
            // 
            // cbECKeyIndex
            // 
            this.cbECKeyIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbECKeyIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbECKeyIndex.FormattingEnabled = true;
            this.cbECKeyIndex.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.cbECKeyIndex.Location = new System.Drawing.Point(880, 16);
            this.cbECKeyIndex.Name = "cbECKeyIndex";
            this.cbECKeyIndex.Size = new System.Drawing.Size(50, 21);
            this.cbECKeyIndex.TabIndex = 5;
            // 
            // lbECKeyLength
            // 
            this.lbECKeyLength.AutoSize = true;
            this.lbECKeyLength.Location = new System.Drawing.Point(8, 45);
            this.lbECKeyLength.Name = "lbECKeyLength";
            this.lbECKeyLength.Size = new System.Drawing.Size(85, 13);
            this.lbECKeyLength.TabIndex = 2;
            this.lbECKeyLength.Text = "Key length [bits]:";
            // 
            // lbECKeyIndex
            // 
            this.lbECKeyIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbECKeyIndex.AutoSize = true;
            this.lbECKeyIndex.Location = new System.Drawing.Point(777, 19);
            this.lbECKeyIndex.Name = "lbECKeyIndex";
            this.lbECKeyIndex.Size = new System.Drawing.Size(97, 13);
            this.lbECKeyIndex.TabIndex = 4;
            this.lbECKeyIndex.Text = "Key index (in card):";
            // 
            // btnECImportP12
            // 
            this.btnECImportP12.Location = new System.Drawing.Point(233, 15);
            this.btnECImportP12.Margin = new System.Windows.Forms.Padding(10);
            this.btnECImportP12.Name = "btnECImportP12";
            this.btnECImportP12.Size = new System.Drawing.Size(160, 48);
            this.btnECImportP12.TabIndex = 0;
            this.btnECImportP12.Text = "Import from PKCS#12 file    (.p12 ;  .pfx)";
            this.btnECImportP12.UseVisualStyleBackColor = true;
            this.btnECImportP12.Click += new System.EventHandler(this.btnECImportP12_Click);
            // 
            // tabSignature
            // 
            this.tabSignature.BackColor = System.Drawing.SystemColors.Control;
            this.tabSignature.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabSignature.Controls.Add(this.gbSignatureHash);
            this.tabSignature.Controls.Add(this.gbSignature);
            this.tabSignature.Controls.Add(this.gbMessage);
            this.tabSignature.Controls.Add(this.gbSignatureParameters);
            this.tabSignature.Location = new System.Drawing.Point(4, 22);
            this.tabSignature.Name = "tabSignature";
            this.tabSignature.Size = new System.Drawing.Size(1134, 574);
            this.tabSignature.TabIndex = 3;
            this.tabSignature.Text = "Signature";
            // 
            // gbSignatureHash
            // 
            this.gbSignatureHash.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSignatureHash.Controls.Add(this.btnHashStoreToBin);
            this.gbSignatureHash.Controls.Add(this.btnHash);
            this.gbSignatureHash.Controls.Add(this.cbHashAlg);
            this.gbSignatureHash.Controls.Add(this.lbHashAlg);
            this.gbSignatureHash.Controls.Add(this.rbHashBase64);
            this.gbSignatureHash.Controls.Add(this.rbHashHex);
            this.gbSignatureHash.Controls.Add(this.lbHash);
            this.gbSignatureHash.Controls.Add(this.tbHash);
            this.gbSignatureHash.Location = new System.Drawing.Point(8, 361);
            this.gbSignatureHash.Margin = new System.Windows.Forms.Padding(0);
            this.gbSignatureHash.Name = "gbSignatureHash";
            this.gbSignatureHash.Size = new System.Drawing.Size(1116, 85);
            this.gbSignatureHash.TabIndex = 25;
            this.gbSignatureHash.TabStop = false;
            this.gbSignatureHash.Text = "Signature Hash (optional):";
            // 
            // btnHashStoreToBin
            // 
            this.btnHashStoreToBin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHashStoreToBin.Enabled = false;
            this.btnHashStoreToBin.Location = new System.Drawing.Point(942, 18);
            this.btnHashStoreToBin.Margin = new System.Windows.Forms.Padding(0);
            this.btnHashStoreToBin.Name = "btnHashStoreToBin";
            this.btnHashStoreToBin.Size = new System.Drawing.Size(160, 21);
            this.btnHashStoreToBin.TabIndex = 27;
            this.btnHashStoreToBin.Text = "Save hash to file";
            this.btnHashStoreToBin.UseVisualStyleBackColor = true;
            this.btnHashStoreToBin.Click += new System.EventHandler(this.btnHashStoreToBin_Click);
            // 
            // btnHash
            // 
            this.btnHash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHash.BackColor = System.Drawing.Color.LightBlue;
            this.btnHash.Location = new System.Drawing.Point(772, 19);
            this.btnHash.Margin = new System.Windows.Forms.Padding(0);
            this.btnHash.Name = "btnHash";
            this.btnHash.Size = new System.Drawing.Size(160, 21);
            this.btnHash.TabIndex = 26;
            this.btnHash.Text = "Calculate hash (optional)";
            this.btnHash.UseVisualStyleBackColor = false;
            this.btnHash.Click += new System.EventHandler(this.btnHash_Click);
            // 
            // cbHashAlg
            // 
            this.cbHashAlg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHashAlg.FormattingEnabled = true;
            this.cbHashAlg.Items.AddRange(new object[] {
            "MD5",
            "SHA-1",
            "SHA-256",
            "SHA-384",
            "SHA-512"});
            this.cbHashAlg.Location = new System.Drawing.Point(301, 19);
            this.cbHashAlg.Name = "cbHashAlg";
            this.cbHashAlg.Size = new System.Drawing.Size(80, 21);
            this.cbHashAlg.TabIndex = 24;
            this.cbHashAlg.SelectedIndexChanged += new System.EventHandler(this.cbHashAlg_SelectedIndexChanged);
            // 
            // lbHashAlg
            // 
            this.lbHashAlg.AutoSize = true;
            this.lbHashAlg.Location = new System.Drawing.Point(215, 22);
            this.lbHashAlg.Name = "lbHashAlg";
            this.lbHashAlg.Size = new System.Drawing.Size(80, 13);
            this.lbHashAlg.TabIndex = 23;
            this.lbHashAlg.Text = "Hash algorithm:";
            // 
            // rbHashBase64
            // 
            this.rbHashBase64.AutoSize = true;
            this.rbHashBase64.Location = new System.Drawing.Point(122, 24);
            this.rbHashBase64.Name = "rbHashBase64";
            this.rbHashBase64.Size = new System.Drawing.Size(61, 17);
            this.rbHashBase64.TabIndex = 22;
            this.rbHashBase64.TabStop = true;
            this.rbHashBase64.Text = "Base64";
            this.rbHashBase64.UseVisualStyleBackColor = true;
            this.rbHashBase64.Click += new System.EventHandler(this.tbHashRadixChanged);
            // 
            // rbHashHex
            // 
            this.rbHashHex.AutoSize = true;
            this.rbHashHex.Checked = true;
            this.rbHashHex.Location = new System.Drawing.Point(69, 24);
            this.rbHashHex.Name = "rbHashHex";
            this.rbHashHex.Size = new System.Drawing.Size(47, 17);
            this.rbHashHex.TabIndex = 21;
            this.rbHashHex.TabStop = true;
            this.rbHashHex.Text = "HEX";
            this.rbHashHex.UseVisualStyleBackColor = true;
            this.rbHashHex.Click += new System.EventHandler(this.tbHashRadixChanged);
            // 
            // lbHash
            // 
            this.lbHash.AutoSize = true;
            this.lbHash.Location = new System.Drawing.Point(8, 51);
            this.lbHash.Margin = new System.Windows.Forms.Padding(3);
            this.lbHash.Name = "lbHash";
            this.lbHash.Size = new System.Drawing.Size(35, 13);
            this.lbHash.TabIndex = 17;
            this.lbHash.Text = "Hash:";
            // 
            // tbHash
            // 
            this.tbHash.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHash.BackColor = System.Drawing.SystemColors.Window;
            this.tbHash.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbHash.Location = new System.Drawing.Point(69, 47);
            this.tbHash.Name = "tbHash";
            this.tbHash.ReadOnly = true;
            this.tbHash.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.tbHash.Size = new System.Drawing.Size(1033, 22);
            this.tbHash.TabIndex = 18;
            // 
            // gbSignature
            // 
            this.gbSignature.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSignature.Controls.Add(this.pbSigning);
            this.gbSignature.Controls.Add(this.btnSignature);
            this.gbSignature.Controls.Add(this.btnSignatureStoreToBin);
            this.gbSignature.Controls.Add(this.rbSignatureBase64);
            this.gbSignature.Controls.Add(this.rbSignatureHex);
            this.gbSignature.Controls.Add(this.lbSignature);
            this.gbSignature.Controls.Add(this.tbSignature);
            this.gbSignature.Location = new System.Drawing.Point(8, 217);
            this.gbSignature.Margin = new System.Windows.Forms.Padding(8);
            this.gbSignature.Name = "gbSignature";
            this.gbSignature.Size = new System.Drawing.Size(1116, 136);
            this.gbSignature.TabIndex = 24;
            this.gbSignature.TabStop = false;
            this.gbSignature.Text = "Signature:";
            // 
            // pbSigning
            // 
            this.pbSigning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSigning.Location = new System.Drawing.Point(69, 118);
            this.pbSigning.Name = "pbSigning";
            this.pbSigning.Size = new System.Drawing.Size(1033, 10);
            this.pbSigning.Step = 1;
            this.pbSigning.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbSigning.TabIndex = 32;
            // 
            // btnSignature
            // 
            this.btnSignature.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSignature.BackColor = System.Drawing.Color.Azure;
            this.btnSignature.Location = new System.Drawing.Point(772, 19);
            this.btnSignature.Margin = new System.Windows.Forms.Padding(0);
            this.btnSignature.Name = "btnSignature";
            this.btnSignature.Size = new System.Drawing.Size(160, 21);
            this.btnSignature.TabIndex = 28;
            this.btnSignature.Text = "Get signature";
            this.btnSignature.UseVisualStyleBackColor = false;
            this.btnSignature.Click += new System.EventHandler(this.btnSignature_Click);
            // 
            // btnSignatureStoreToBin
            // 
            this.btnSignatureStoreToBin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSignatureStoreToBin.Enabled = false;
            this.btnSignatureStoreToBin.Location = new System.Drawing.Point(942, 19);
            this.btnSignatureStoreToBin.Margin = new System.Windows.Forms.Padding(10);
            this.btnSignatureStoreToBin.Name = "btnSignatureStoreToBin";
            this.btnSignatureStoreToBin.Size = new System.Drawing.Size(160, 21);
            this.btnSignatureStoreToBin.TabIndex = 27;
            this.btnSignatureStoreToBin.Text = "Save signature to binary file";
            this.btnSignatureStoreToBin.UseVisualStyleBackColor = true;
            this.btnSignatureStoreToBin.Click += new System.EventHandler(this.btnSignatureStoreToBin_Click);
            // 
            // rbSignatureBase64
            // 
            this.rbSignatureBase64.AutoSize = true;
            this.rbSignatureBase64.Location = new System.Drawing.Point(122, 24);
            this.rbSignatureBase64.Name = "rbSignatureBase64";
            this.rbSignatureBase64.Size = new System.Drawing.Size(61, 17);
            this.rbSignatureBase64.TabIndex = 22;
            this.rbSignatureBase64.TabStop = true;
            this.rbSignatureBase64.Text = "Base64";
            this.rbSignatureBase64.UseVisualStyleBackColor = true;
            this.rbSignatureBase64.Click += new System.EventHandler(this.tbSignatureRadixChanged);
            // 
            // rbSignatureHex
            // 
            this.rbSignatureHex.AutoSize = true;
            this.rbSignatureHex.Checked = true;
            this.rbSignatureHex.Location = new System.Drawing.Point(69, 24);
            this.rbSignatureHex.Name = "rbSignatureHex";
            this.rbSignatureHex.Size = new System.Drawing.Size(47, 17);
            this.rbSignatureHex.TabIndex = 21;
            this.rbSignatureHex.TabStop = true;
            this.rbSignatureHex.Text = "HEX";
            this.rbSignatureHex.UseVisualStyleBackColor = true;
            this.rbSignatureHex.Click += new System.EventHandler(this.tbSignatureRadixChanged);
            // 
            // lbSignature
            // 
            this.lbSignature.AutoSize = true;
            this.lbSignature.Location = new System.Drawing.Point(8, 73);
            this.lbSignature.Margin = new System.Windows.Forms.Padding(3);
            this.lbSignature.Name = "lbSignature";
            this.lbSignature.Size = new System.Drawing.Size(55, 13);
            this.lbSignature.TabIndex = 17;
            this.lbSignature.Text = "Signature:";
            // 
            // tbSignature
            // 
            this.tbSignature.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSignature.BackColor = System.Drawing.SystemColors.Window;
            this.tbSignature.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbSignature.Location = new System.Drawing.Point(69, 47);
            this.tbSignature.Multiline = true;
            this.tbSignature.Name = "tbSignature";
            this.tbSignature.ReadOnly = true;
            this.tbSignature.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.tbSignature.Size = new System.Drawing.Size(1033, 65);
            this.tbSignature.TabIndex = 18;
            // 
            // gbMessage
            // 
            this.gbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMessage.Controls.Add(this.btnSaveMessageToBin);
            this.gbMessage.Controls.Add(this.rbMessageFromFile);
            this.gbMessage.Controls.Add(this.rbMessageBase64);
            this.gbMessage.Controls.Add(this.rbMessageHex);
            this.gbMessage.Controls.Add(this.rbMessageAscii);
            this.gbMessage.Controls.Add(this.lbMessage);
            this.gbMessage.Controls.Add(this.tbMessage);
            this.gbMessage.Location = new System.Drawing.Point(8, 82);
            this.gbMessage.Margin = new System.Windows.Forms.Padding(0);
            this.gbMessage.Name = "gbMessage";
            this.gbMessage.Size = new System.Drawing.Size(1116, 127);
            this.gbMessage.TabIndex = 7;
            this.gbMessage.TabStop = false;
            this.gbMessage.Text = "Plain text (input message to sign):";
            // 
            // btnSaveMessageToBin
            // 
            this.btnSaveMessageToBin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveMessageToBin.Location = new System.Drawing.Point(942, 20);
            this.btnSaveMessageToBin.Margin = new System.Windows.Forms.Padding(10);
            this.btnSaveMessageToBin.Name = "btnSaveMessageToBin";
            this.btnSaveMessageToBin.Size = new System.Drawing.Size(160, 21);
            this.btnSaveMessageToBin.TabIndex = 30;
            this.btnSaveMessageToBin.Text = "Save message to binary file";
            this.btnSaveMessageToBin.UseVisualStyleBackColor = true;
            this.btnSaveMessageToBin.Click += new System.EventHandler(this.btnSaveMessageToBin_Click);
            // 
            // rbMessageFromFile
            // 
            this.rbMessageFromFile.AutoSize = true;
            this.rbMessageFromFile.Location = new System.Drawing.Point(211, 24);
            this.rbMessageFromFile.Name = "rbMessageFromFile";
            this.rbMessageFromFile.Size = new System.Drawing.Size(344, 17);
            this.rbMessageFromFile.TabIndex = 23;
            this.rbMessageFromFile.TabStop = true;
            this.rbMessageFromFile.Text = "Set file to sign (ATTENTION: it will remove any message text below)";
            this.rbMessageFromFile.UseVisualStyleBackColor = true;
            this.rbMessageFromFile.Click += new System.EventHandler(this.tbMessageRadixChanged);
            // 
            // rbMessageBase64
            // 
            this.rbMessageBase64.AutoSize = true;
            this.rbMessageBase64.Location = new System.Drawing.Point(86, 24);
            this.rbMessageBase64.Name = "rbMessageBase64";
            this.rbMessageBase64.Size = new System.Drawing.Size(61, 17);
            this.rbMessageBase64.TabIndex = 22;
            this.rbMessageBase64.TabStop = true;
            this.rbMessageBase64.Text = "Base64";
            this.rbMessageBase64.UseVisualStyleBackColor = true;
            this.rbMessageBase64.Click += new System.EventHandler(this.tbMessageRadixChanged);
            // 
            // rbMessageHex
            // 
            this.rbMessageHex.AutoSize = true;
            this.rbMessageHex.Checked = true;
            this.rbMessageHex.Location = new System.Drawing.Point(33, 24);
            this.rbMessageHex.Name = "rbMessageHex";
            this.rbMessageHex.Size = new System.Drawing.Size(47, 17);
            this.rbMessageHex.TabIndex = 21;
            this.rbMessageHex.TabStop = true;
            this.rbMessageHex.Text = "HEX";
            this.rbMessageHex.UseVisualStyleBackColor = true;
            this.rbMessageHex.Click += new System.EventHandler(this.tbMessageRadixChanged);
            // 
            // rbMessageAscii
            // 
            this.rbMessageAscii.AutoSize = true;
            this.rbMessageAscii.Location = new System.Drawing.Point(153, 24);
            this.rbMessageAscii.Name = "rbMessageAscii";
            this.rbMessageAscii.Size = new System.Drawing.Size(52, 17);
            this.rbMessageAscii.TabIndex = 20;
            this.rbMessageAscii.Text = "ASCII";
            this.rbMessageAscii.UseVisualStyleBackColor = true;
            this.rbMessageAscii.Click += new System.EventHandler(this.tbMessageRadixChanged);
            // 
            // lbMessage
            // 
            this.lbMessage.AutoSize = true;
            this.lbMessage.Location = new System.Drawing.Point(8, 73);
            this.lbMessage.Margin = new System.Windows.Forms.Padding(3);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(19, 13);
            this.lbMessage.TabIndex = 17;
            this.lbMessage.Text = "M:";
            // 
            // tbMessage
            // 
            this.tbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMessage.BackColor = System.Drawing.SystemColors.Window;
            this.tbMessage.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbMessage.Location = new System.Drawing.Point(33, 47);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.tbMessage.Size = new System.Drawing.Size(1069, 65);
            this.tbMessage.TabIndex = 18;
            // 
            // gbSignatureParameters
            // 
            this.gbSignatureParameters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSignatureParameters.Controls.Add(this.cbCipher);
            this.gbSignatureParameters.Controls.Add(this.lbCipher);
            this.gbSignatureParameters.Controls.Add(this.cbDigest);
            this.gbSignatureParameters.Controls.Add(this.lbDigest);
            this.gbSignatureParameters.Controls.Add(this.cbSignatureKeyIndex);
            this.gbSignatureParameters.Controls.Add(this.lbSignatureKeyIndex);
            this.gbSignatureParameters.Location = new System.Drawing.Point(8, 0);
            this.gbSignatureParameters.Margin = new System.Windows.Forms.Padding(8);
            this.gbSignatureParameters.Name = "gbSignatureParameters";
            this.gbSignatureParameters.Size = new System.Drawing.Size(1116, 74);
            this.gbSignatureParameters.TabIndex = 2;
            this.gbSignatureParameters.TabStop = false;
            // 
            // cbCipher
            // 
            this.cbCipher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCipher.FormattingEnabled = true;
            this.cbCipher.Items.AddRange(new object[] {
            "RSA",
            "ECDSA"});
            this.cbCipher.Location = new System.Drawing.Point(99, 43);
            this.cbCipher.Name = "cbCipher";
            this.cbCipher.Size = new System.Drawing.Size(124, 21);
            this.cbCipher.TabIndex = 9;
            // 
            // lbCipher
            // 
            this.lbCipher.AutoSize = true;
            this.lbCipher.Location = new System.Drawing.Point(8, 46);
            this.lbCipher.Name = "lbCipher";
            this.lbCipher.Size = new System.Drawing.Size(85, 13);
            this.lbCipher.TabIndex = 8;
            this.lbCipher.Text = "Cipher algorithm:";
            // 
            // cbDigest
            // 
            this.cbDigest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDigest.FormattingEnabled = true;
            this.cbDigest.Items.AddRange(new object[] {
            "SHA-1",
            "SHA-224",
            "SHA-256",
            "SHA-384",
            "SHA-512"});
            this.cbDigest.Location = new System.Drawing.Point(143, 16);
            this.cbDigest.Name = "cbDigest";
            this.cbDigest.Size = new System.Drawing.Size(80, 21);
            this.cbDigest.TabIndex = 9;
            // 
            // lbDigest
            // 
            this.lbDigest.AutoSize = true;
            this.lbDigest.Location = new System.Drawing.Point(8, 19);
            this.lbDigest.Name = "lbDigest";
            this.lbDigest.Size = new System.Drawing.Size(129, 13);
            this.lbDigest.TabIndex = 8;
            this.lbDigest.Text = "Message digest algorithm:";
            // 
            // cbSignatureKeyIndex
            // 
            this.cbSignatureKeyIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSignatureKeyIndex.FormattingEnabled = true;
            this.cbSignatureKeyIndex.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.cbSignatureKeyIndex.Location = new System.Drawing.Point(351, 16);
            this.cbSignatureKeyIndex.Name = "cbSignatureKeyIndex";
            this.cbSignatureKeyIndex.Size = new System.Drawing.Size(50, 21);
            this.cbSignatureKeyIndex.TabIndex = 5;
            // 
            // lbSignatureKeyIndex
            // 
            this.lbSignatureKeyIndex.AutoSize = true;
            this.lbSignatureKeyIndex.Location = new System.Drawing.Point(248, 19);
            this.lbSignatureKeyIndex.Name = "lbSignatureKeyIndex";
            this.lbSignatureKeyIndex.Size = new System.Drawing.Size(97, 13);
            this.lbSignatureKeyIndex.TabIndex = 4;
            this.lbSignatureKeyIndex.Text = "Key index (in card):";
            // 
            // llbDLogicURL
            // 
            this.llbDLogicURL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llbDLogicURL.Location = new System.Drawing.Point(690, 7);
            this.llbDLogicURL.Name = "llbDLogicURL";
            this.llbDLogicURL.Size = new System.Drawing.Size(460, 24);
            this.llbDLogicURL.TabIndex = 1;
            this.llbDLogicURL.TabStop = true;
            this.llbDLogicURL.Text = "http://www.d-logic.net/nfc-rfid-reader-sdk/";
            this.llbDLogicURL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.llbDLogicURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbDLogicURL_LinkClicked);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 623);
            this.Controls.Add(this.llbDLogicURL);
            this.Controls.Add(this.tabControl);
            this.MinimumSize = new System.Drawing.Size(1180, 646);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "uFR Signer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tabControl.ResumeLayout(false);
            this.tabRSAKeys.ResumeLayout(false);
            this.gbRSAPub.ResumeLayout(false);
            this.gbRSAPub.PerformLayout();
            this.gbRSAModulus.ResumeLayout(false);
            this.gbRSAModulus.PerformLayout();
            this.gbRSAPriv.ResumeLayout(false);
            this.gbRSAPriv.PerformLayout();
            this.gpPrivateKeyMode.ResumeLayout(false);
            this.gpPrivateKeyMode.PerformLayout();
            this.gbRSACommands.ResumeLayout(false);
            this.gbRSACommands.PerformLayout();
            this.tabECKeys.ResumeLayout(false);
            this.gbECPubKey.ResumeLayout(false);
            this.gbECPubKey.PerformLayout();
            this.gbECPrivKey.ResumeLayout(false);
            this.gbECPrivKey.PerformLayout();
            this.gbECDomainParameters.ResumeLayout(false);
            this.gbECDomainParameters.PerformLayout();
            this.gbECReductionPolynomial.ResumeLayout(false);
            this.gbECReductionPolynomial.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbECCommands.ResumeLayout(false);
            this.gbECCommands.PerformLayout();
            this.tabSignature.ResumeLayout(false);
            this.gbSignatureHash.ResumeLayout(false);
            this.gbSignatureHash.PerformLayout();
            this.gbSignature.ResumeLayout(false);
            this.gbSignature.PerformLayout();
            this.gbMessage.ResumeLayout(false);
            this.gbMessage.PerformLayout();
            this.gbSignatureParameters.ResumeLayout(false);
            this.gbSignatureParameters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabRSAKeys;
        private System.Windows.Forms.TabPage tabECKeys;
        private System.Windows.Forms.TabPage tabSignature;
        private System.Windows.Forms.Label lbRSAKeyLength;
        private System.Windows.Forms.LinkLabel llbDLogicURL;
        private System.Windows.Forms.ComboBox cbRSAKeyLength;
        private System.Windows.Forms.ComboBox cbRSAKeyIndex;
        private System.Windows.Forms.Label lbRSAKeyIndex;
        private System.Windows.Forms.GroupBox gbRSACommands;
        private System.Windows.Forms.Button btnStoreRSAPriv;
        private System.Windows.Forms.Button btnMkRSAKey;
        private System.Windows.Forms.Button btnRSAImportP12;
        private System.Windows.Forms.Button btnSaveRSAPub;
        private System.Windows.Forms.GroupBox gbRSAPriv;
        private System.Windows.Forms.TextBox tbRSAPrivPQ;
        private System.Windows.Forms.TextBox tbRSAPrivQ;
        private System.Windows.Forms.Label lbRSAPrivDQ1;
        private System.Windows.Forms.Label lbRSAPrivExp;
        private System.Windows.Forms.TextBox tbRSAPrivExp;
        private System.Windows.Forms.TextBox tbRSAPrivP;
        private System.Windows.Forms.GroupBox gpPrivateKeyMode;
        private System.Windows.Forms.RadioButton rbCRT;
        private System.Windows.Forms.RadioButton rbModExp;
        private System.Windows.Forms.Label lbRSAPrivP;
        private System.Windows.Forms.Label lbRSAPrivQ;
        private System.Windows.Forms.Label lbRSAPrivPQ;
        private System.Windows.Forms.Label lbRSAPrivDP1;
        private System.Windows.Forms.TextBox tbRSAPrivDQ1;
        private System.Windows.Forms.TextBox tbRSAPrivDP1;
        private System.Windows.Forms.GroupBox gbRSAModulus;
        private System.Windows.Forms.Label lbRSAModulus;
        private System.Windows.Forms.TextBox tbRSAModulus;
        private System.Windows.Forms.GroupBox gbRSAPub;
        private System.Windows.Forms.Label lbRSAPubExp;
        private System.Windows.Forms.TextBox tbRSAPubExp;
        private System.Windows.Forms.GroupBox gbECCommands;
        private System.Windows.Forms.Button btnSaveECPub;
        private System.Windows.Forms.Button btnStoreECPriv;
        private System.Windows.Forms.Button btnMkECKey;
        private System.Windows.Forms.ComboBox cbECKeyLength;
        private System.Windows.Forms.ComboBox cbECKeyIndex;
        private System.Windows.Forms.Label lbECKeyLength;
        private System.Windows.Forms.Label lbECKeyIndex;
        private System.Windows.Forms.Button btnECImportP12;
        private System.Windows.Forms.ComboBox cbECName;
        private System.Windows.Forms.Label lbECName;
        private System.Windows.Forms.GroupBox gbECDomainParameters;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbECFieldPrime;
        private System.Windows.Forms.RadioButton rbECFieldBinary;
        private System.Windows.Forms.Label lbECParamPrime;
        private System.Windows.Forms.TextBox tbECParamPrime;
        private System.Windows.Forms.GroupBox gbECReductionPolynomial;
        private System.Windows.Forms.RichTextBox rtbECReductionPolynomial;
        private System.Windows.Forms.Label lbECParamE3;
        private System.Windows.Forms.TextBox tbECParamE3;
        private System.Windows.Forms.Label lbECParamE2;
        private System.Windows.Forms.TextBox tbECParamE2;
        private System.Windows.Forms.Label lbECParamE1;
        private System.Windows.Forms.TextBox tbECParamE1;
        private System.Windows.Forms.Label lbECParamK;
        private System.Windows.Forms.TextBox tbECParamK;
        private System.Windows.Forms.Label lbECParamR;
        private System.Windows.Forms.TextBox tbECParamR;
        private System.Windows.Forms.Label lbECParamG;
        private System.Windows.Forms.TextBox tbECParamG;
        private System.Windows.Forms.Label lbECParamB;
        private System.Windows.Forms.TextBox tbECParamB;
        private System.Windows.Forms.Label ltbECParamA;
        private System.Windows.Forms.TextBox tbECParamA;
        private System.Windows.Forms.GroupBox gbECPubKey;
        private System.Windows.Forms.Label lbECPubKey;
        private System.Windows.Forms.TextBox tbECPubKey;
        private System.Windows.Forms.GroupBox gbECPrivKey;
        private System.Windows.Forms.Label lbECPrivKey;
        private System.Windows.Forms.TextBox tbECPrivKey;
        private System.Windows.Forms.GroupBox gbSignatureParameters;
        private System.Windows.Forms.ComboBox cbSignatureKeyIndex;
        private System.Windows.Forms.Label lbSignatureKeyIndex;
        private System.Windows.Forms.ComboBox cbDigest;
        private System.Windows.Forms.Label lbDigest;
        private System.Windows.Forms.ComboBox cbCipher;
        private System.Windows.Forms.Label lbCipher;
        private System.Windows.Forms.GroupBox gbMessage;
        private System.Windows.Forms.RadioButton rbMessageFromFile;
        private System.Windows.Forms.RadioButton rbMessageBase64;
        private System.Windows.Forms.RadioButton rbMessageHex;
        private System.Windows.Forms.RadioButton rbMessageAscii;
        private System.Windows.Forms.Label lbMessage;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.GroupBox gbSignature;
        private System.Windows.Forms.RadioButton rbSignatureBase64;
        private System.Windows.Forms.RadioButton rbSignatureHex;
        private System.Windows.Forms.Label lbSignature;
        private System.Windows.Forms.TextBox tbSignature;
        private System.Windows.Forms.GroupBox gbSignatureHash;
        private System.Windows.Forms.ComboBox cbHashAlg;
        private System.Windows.Forms.Label lbHashAlg;
        private System.Windows.Forms.RadioButton rbHashBase64;
        private System.Windows.Forms.RadioButton rbHashHex;
        private System.Windows.Forms.Label lbHash;
        private System.Windows.Forms.TextBox tbHash;
        private System.Windows.Forms.Button btnHash;
        private System.Windows.Forms.Button btnSignature;
        private System.Windows.Forms.Button btnSignatureStoreToBin;
        private System.Windows.Forms.Button btnHashStoreToBin;
        private System.Windows.Forms.Button btnSaveMessageToBin;
        private System.Windows.Forms.Button btnRestoreRSAPriv;
        private System.Windows.Forms.Button btnBackupRSAPriv;
        private System.Windows.Forms.Button btnRestoreECPriv;
        private System.Windows.Forms.Button btnBackupECPriv;
        private System.Windows.Forms.ProgressBar pbSigning;
    }
}

