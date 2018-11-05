namespace uFRSigner
{
    partial class frmCSR
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
            this.gbDn = new System.Windows.Forms.GroupBox();
            this.btnDeselect = new System.Windows.Forms.Button();
            this.ebRdn = new System.Windows.Forms.TextBox();
            this.btnDnRdn = new System.Windows.Forms.Button();
            this.btnUpRdn = new System.Windows.Forms.Button();
            this.btnRemoveRdn = new System.Windows.Forms.Button();
            this.btnPutRdn = new System.Windows.Forms.Button();
            this.lstDn = new System.Windows.Forms.ListBox();
            this.cbRdn = new System.Windows.Forms.ComboBox();
            this.gbExt = new System.Windows.Forms.GroupBox();
            this.chkCritical = new System.Windows.Forms.CheckBox();
            this.btnReplaceExtension = new System.Windows.Forms.Button();
            this.ebExt = new System.Windows.Forms.TextBox();
            this.btnRemoveExt = new System.Windows.Forms.Button();
            this.btnPutExt = new System.Windows.Forms.Button();
            this.lstExt = new System.Windows.Forms.ListBox();
            this.cbExt = new System.Windows.Forms.ComboBox();
            this.btnSignCsr = new System.Windows.Forms.Button();
            this.btnLoadCsr = new System.Windows.Forms.Button();
            this.btnSaveCsr = new System.Windows.Forms.Button();
            this.btnSaveTbsCsr = new System.Windows.Forms.Button();
            this.btnLoadTbsCsr = new System.Windows.Forms.Button();
            this.btnClearEntries = new System.Windows.Forms.Button();
            this.cbDigest = new System.Windows.Forms.ComboBox();
            this.lbDigest = new System.Windows.Forms.Label();
            this.gbDn.SuspendLayout();
            this.gbExt.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDn
            // 
            this.gbDn.Controls.Add(this.btnDeselect);
            this.gbDn.Controls.Add(this.ebRdn);
            this.gbDn.Controls.Add(this.btnDnRdn);
            this.gbDn.Controls.Add(this.btnUpRdn);
            this.gbDn.Controls.Add(this.btnRemoveRdn);
            this.gbDn.Controls.Add(this.btnPutRdn);
            this.gbDn.Controls.Add(this.lstDn);
            this.gbDn.Controls.Add(this.cbRdn);
            this.gbDn.Location = new System.Drawing.Point(9, 9);
            this.gbDn.Margin = new System.Windows.Forms.Padding(0);
            this.gbDn.Name = "gbDn";
            this.gbDn.Size = new System.Drawing.Size(648, 220);
            this.gbDn.TabIndex = 0;
            this.gbDn.TabStop = false;
            this.gbDn.Text = "Distinguished Name (DN):";
            // 
            // btnDeselect
            // 
            this.btnDeselect.Location = new System.Drawing.Point(556, 50);
            this.btnDeselect.Name = "btnDeselect";
            this.btnDeselect.Size = new System.Drawing.Size(86, 24);
            this.btnDeselect.TabIndex = 4;
            this.btnDeselect.Text = "Deselect";
            this.btnDeselect.UseVisualStyleBackColor = true;
            this.btnDeselect.Click += new System.EventHandler(this.btnDeselect_Click);
            // 
            // ebRdn
            // 
            this.ebRdn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ebRdn.BackColor = System.Drawing.SystemColors.Window;
            this.ebRdn.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ebRdn.Location = new System.Drawing.Point(232, 21);
            this.ebRdn.Margin = new System.Windows.Forms.Padding(0);
            this.ebRdn.Name = "ebRdn";
            this.ebRdn.Size = new System.Drawing.Size(317, 22);
            this.ebRdn.TabIndex = 1;
            // 
            // btnDnRdn
            // 
            this.btnDnRdn.Location = new System.Drawing.Point(556, 185);
            this.btnDnRdn.Name = "btnDnRdn";
            this.btnDnRdn.Size = new System.Drawing.Size(86, 23);
            this.btnDnRdn.TabIndex = 7;
            this.btnDnRdn.Text = "Move Down";
            this.btnDnRdn.UseVisualStyleBackColor = true;
            this.btnDnRdn.Click += new System.EventHandler(this.btnDnRdn_Click);
            // 
            // btnUpRdn
            // 
            this.btnUpRdn.Location = new System.Drawing.Point(556, 156);
            this.btnUpRdn.Name = "btnUpRdn";
            this.btnUpRdn.Size = new System.Drawing.Size(86, 23);
            this.btnUpRdn.TabIndex = 6;
            this.btnUpRdn.Text = "Move Up";
            this.btnUpRdn.UseVisualStyleBackColor = true;
            this.btnUpRdn.Click += new System.EventHandler(this.btnUpRdn_Click);
            // 
            // btnRemoveRdn
            // 
            this.btnRemoveRdn.Location = new System.Drawing.Point(556, 127);
            this.btnRemoveRdn.Name = "btnRemoveRdn";
            this.btnRemoveRdn.Size = new System.Drawing.Size(86, 23);
            this.btnRemoveRdn.TabIndex = 5;
            this.btnRemoveRdn.Text = "Remove";
            this.btnRemoveRdn.UseVisualStyleBackColor = true;
            this.btnRemoveRdn.Click += new System.EventHandler(this.btnRemoveRdn_Click);
            // 
            // btnPutRdn
            // 
            this.btnPutRdn.Location = new System.Drawing.Point(556, 21);
            this.btnPutRdn.Name = "btnPutRdn";
            this.btnPutRdn.Size = new System.Drawing.Size(86, 24);
            this.btnPutRdn.TabIndex = 3;
            this.btnPutRdn.Text = "Put";
            this.btnPutRdn.UseVisualStyleBackColor = true;
            this.btnPutRdn.Click += new System.EventHandler(this.btnPutRdn_Click);
            // 
            // lstDn
            // 
            this.lstDn.FormattingEnabled = true;
            this.lstDn.Location = new System.Drawing.Point(10, 48);
            this.lstDn.Name = "lstDn";
            this.lstDn.ScrollAlwaysVisible = true;
            this.lstDn.Size = new System.Drawing.Size(540, 160);
            this.lstDn.TabIndex = 2;
            // 
            // cbRdn
            // 
            this.cbRdn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRdn.FormattingEnabled = true;
            this.cbRdn.Items.AddRange(new object[] {
            "Common Name (CN)",
            "Organizational Unit (OU)",
            "Organization (O)",
            "Locality (L)",
            "State Or Province Name (ST)",
            "Country Code (C)",
            "Domain component (DC)",
            "Domain name qualifier (DNQ)",
            "Title (T)",
            "Street",
            "SerialNumber",
            "Surname",
            "GivenName",
            "Initials",
            "Generation",
            "UniqueIdentifier",
            "BusinessCategory",
            "PostalCode",
            "Pseudonym",
            "Email address {Deprecated} (E)"});
            this.cbRdn.Location = new System.Drawing.Point(10, 21);
            this.cbRdn.Name = "cbRdn";
            this.cbRdn.Size = new System.Drawing.Size(219, 21);
            this.cbRdn.TabIndex = 0;
            // 
            // gbExt
            // 
            this.gbExt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbExt.Controls.Add(this.chkCritical);
            this.gbExt.Controls.Add(this.btnReplaceExtension);
            this.gbExt.Controls.Add(this.ebExt);
            this.gbExt.Controls.Add(this.btnRemoveExt);
            this.gbExt.Controls.Add(this.btnPutExt);
            this.gbExt.Controls.Add(this.lstExt);
            this.gbExt.Controls.Add(this.cbExt);
            this.gbExt.Location = new System.Drawing.Point(9, 240);
            this.gbExt.Margin = new System.Windows.Forms.Padding(8);
            this.gbExt.Name = "gbExt";
            this.gbExt.Size = new System.Drawing.Size(648, 220);
            this.gbExt.TabIndex = 1;
            this.gbExt.TabStop = false;
            this.gbExt.Text = "Extensions:";
            // 
            // chkCritical
            // 
            this.chkCritical.AutoSize = true;
            this.chkCritical.Location = new System.Drawing.Point(556, 25);
            this.chkCritical.Name = "chkCritical";
            this.chkCritical.Size = new System.Drawing.Size(60, 17);
            this.chkCritical.TabIndex = 3;
            this.chkCritical.Text = "Critical ";
            this.chkCritical.UseVisualStyleBackColor = true;
            // 
            // btnReplaceExtension
            // 
            this.btnReplaceExtension.Location = new System.Drawing.Point(556, 78);
            this.btnReplaceExtension.Name = "btnReplaceExtension";
            this.btnReplaceExtension.Size = new System.Drawing.Size(86, 23);
            this.btnReplaceExtension.TabIndex = 5;
            this.btnReplaceExtension.Text = "Replace";
            this.btnReplaceExtension.UseVisualStyleBackColor = true;
            this.btnReplaceExtension.Click += new System.EventHandler(this.btnReplaceExtension_Click);
            // 
            // ebExt
            // 
            this.ebExt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ebExt.BackColor = System.Drawing.SystemColors.Window;
            this.ebExt.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ebExt.Location = new System.Drawing.Point(232, 21);
            this.ebExt.Margin = new System.Windows.Forms.Padding(0);
            this.ebExt.Name = "ebExt";
            this.ebExt.Size = new System.Drawing.Size(317, 22);
            this.ebExt.TabIndex = 1;
            // 
            // btnRemoveExt
            // 
            this.btnRemoveExt.Location = new System.Drawing.Point(556, 185);
            this.btnRemoveExt.Name = "btnRemoveExt";
            this.btnRemoveExt.Size = new System.Drawing.Size(86, 23);
            this.btnRemoveExt.TabIndex = 6;
            this.btnRemoveExt.Text = "Remove";
            this.btnRemoveExt.UseVisualStyleBackColor = true;
            this.btnRemoveExt.Click += new System.EventHandler(this.btnRemoveExt_Click);
            // 
            // btnPutExt
            // 
            this.btnPutExt.Location = new System.Drawing.Point(556, 48);
            this.btnPutExt.Name = "btnPutExt";
            this.btnPutExt.Size = new System.Drawing.Size(86, 24);
            this.btnPutExt.TabIndex = 4;
            this.btnPutExt.Text = "Put";
            this.btnPutExt.UseVisualStyleBackColor = true;
            this.btnPutExt.Click += new System.EventHandler(this.btnPutExt_Click);
            // 
            // lstExt
            // 
            this.lstExt.FormattingEnabled = true;
            this.lstExt.Location = new System.Drawing.Point(10, 48);
            this.lstExt.Name = "lstExt";
            this.lstExt.ScrollAlwaysVisible = true;
            this.lstExt.Size = new System.Drawing.Size(540, 160);
            this.lstExt.TabIndex = 2;
            this.lstExt.SelectedIndexChanged += new System.EventHandler(this.lstExt_SelectedIndexChanged);
            // 
            // cbExt
            // 
            this.cbExt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExt.FormattingEnabled = true;
            this.cbExt.Items.AddRange(new object[] {
            "subjectAltName(EmailAddr)",
            "KeyUsage",
            "ExtendedKeyUsage",
            "QCStatements"});
            this.cbExt.Location = new System.Drawing.Point(10, 21);
            this.cbExt.Name = "cbExt";
            this.cbExt.Size = new System.Drawing.Size(219, 21);
            this.cbExt.TabIndex = 0;
            this.cbExt.SelectedIndexChanged += new System.EventHandler(this.cbExt_SelectedIndexChanged);
            // 
            // btnSignCsr
            // 
            this.btnSignCsr.BackColor = System.Drawing.Color.Ivory;
            this.btnSignCsr.Location = new System.Drawing.Point(672, 290);
            this.btnSignCsr.Name = "btnSignCsr";
            this.btnSignCsr.Size = new System.Drawing.Size(108, 35);
            this.btnSignCsr.TabIndex = 7;
            this.btnSignCsr.Text = "Sign CSR";
            this.btnSignCsr.UseVisualStyleBackColor = false;
            // 
            // btnLoadCsr
            // 
            this.btnLoadCsr.Location = new System.Drawing.Point(672, 194);
            this.btnLoadCsr.Name = "btnLoadCsr";
            this.btnLoadCsr.Size = new System.Drawing.Size(108, 35);
            this.btnLoadCsr.TabIndex = 6;
            this.btnLoadCsr.Text = "Load data from CSR";
            this.btnLoadCsr.UseVisualStyleBackColor = true;
            // 
            // btnSaveCsr
            // 
            this.btnSaveCsr.BackColor = System.Drawing.Color.Azure;
            this.btnSaveCsr.Location = new System.Drawing.Point(672, 331);
            this.btnSaveCsr.Name = "btnSaveCsr";
            this.btnSaveCsr.Size = new System.Drawing.Size(108, 35);
            this.btnSaveCsr.TabIndex = 8;
            this.btnSaveCsr.Text = "Save CSR";
            this.btnSaveCsr.UseVisualStyleBackColor = false;
            this.btnSaveCsr.Click += new System.EventHandler(this.btnSaveCsr_Click);
            // 
            // btnSaveTbsCsr
            // 
            this.btnSaveTbsCsr.BackColor = System.Drawing.Color.Ivory;
            this.btnSaveTbsCsr.Location = new System.Drawing.Point(672, 112);
            this.btnSaveTbsCsr.Name = "btnSaveTbsCsr";
            this.btnSaveTbsCsr.Size = new System.Drawing.Size(108, 35);
            this.btnSaveTbsCsr.TabIndex = 4;
            this.btnSaveTbsCsr.Text = "Save TBS CSR";
            this.btnSaveTbsCsr.UseVisualStyleBackColor = false;
            this.btnSaveTbsCsr.Click += new System.EventHandler(this.btnSaveTbsCsr_Click);
            // 
            // btnLoadTbsCsr
            // 
            this.btnLoadTbsCsr.Location = new System.Drawing.Point(672, 153);
            this.btnLoadTbsCsr.Name = "btnLoadTbsCsr";
            this.btnLoadTbsCsr.Size = new System.Drawing.Size(108, 35);
            this.btnLoadTbsCsr.TabIndex = 5;
            this.btnLoadTbsCsr.Text = "Load data from TBS CSR";
            this.btnLoadTbsCsr.UseVisualStyleBackColor = true;
            this.btnLoadTbsCsr.Click += new System.EventHandler(this.btnLoadTbsCsr_Click);
            // 
            // btnClearEntries
            // 
            this.btnClearEntries.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnClearEntries.Location = new System.Drawing.Point(672, 425);
            this.btnClearEntries.Name = "btnClearEntries";
            this.btnClearEntries.Size = new System.Drawing.Size(108, 35);
            this.btnClearEntries.TabIndex = 9;
            this.btnClearEntries.Text = "Clear entries";
            this.btnClearEntries.UseVisualStyleBackColor = false;
            this.btnClearEntries.Click += new System.EventHandler(this.btnClearEntries_Click);
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
            this.cbDigest.Location = new System.Drawing.Point(697, 30);
            this.cbDigest.Name = "cbDigest";
            this.cbDigest.Size = new System.Drawing.Size(80, 21);
            this.cbDigest.TabIndex = 3;
            // 
            // lbDigest
            // 
            this.lbDigest.AutoSize = true;
            this.lbDigest.Location = new System.Drawing.Point(672, 9);
            this.lbDigest.Name = "lbDigest";
            this.lbDigest.Size = new System.Drawing.Size(108, 13);
            this.lbDigest.TabIndex = 2;
            this.lbDigest.Text = "CSR digest algorithm:";
            this.lbDigest.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // frmCSR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 471);
            this.Controls.Add(this.cbDigest);
            this.Controls.Add(this.lbDigest);
            this.Controls.Add(this.btnClearEntries);
            this.Controls.Add(this.btnSignCsr);
            this.Controls.Add(this.btnLoadCsr);
            this.Controls.Add(this.btnSaveCsr);
            this.Controls.Add(this.btnSaveTbsCsr);
            this.Controls.Add(this.btnLoadTbsCsr);
            this.Controls.Add(this.gbExt);
            this.Controls.Add(this.gbDn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmCSR";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Certificate Signing Request (CSR)";
            this.Load += new System.EventHandler(this.frmCSR_Load);
            this.VisibleChanged += new System.EventHandler(this.frmCSR_VisibleChanged);
            this.gbDn.ResumeLayout(false);
            this.gbDn.PerformLayout();
            this.gbExt.ResumeLayout(false);
            this.gbExt.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDn;
        private System.Windows.Forms.Button btnDnRdn;
        private System.Windows.Forms.Button btnUpRdn;
        private System.Windows.Forms.Button btnRemoveRdn;
        private System.Windows.Forms.Button btnPutRdn;
        private System.Windows.Forms.ListBox lstDn;
        private System.Windows.Forms.ComboBox cbRdn;
        private System.Windows.Forms.GroupBox gbExt;
        private System.Windows.Forms.TextBox ebRdn;
        private System.Windows.Forms.TextBox ebExt;
        private System.Windows.Forms.Button btnRemoveExt;
        private System.Windows.Forms.Button btnPutExt;
        private System.Windows.Forms.ListBox lstExt;
        private System.Windows.Forms.ComboBox cbExt;
        private System.Windows.Forms.Button btnSignCsr;
        private System.Windows.Forms.Button btnLoadCsr;
        private System.Windows.Forms.Button btnSaveCsr;
        private System.Windows.Forms.Button btnSaveTbsCsr;
        private System.Windows.Forms.Button btnLoadTbsCsr;
        private System.Windows.Forms.Button btnReplaceExtension;
        private System.Windows.Forms.Button btnClearEntries;
        private System.Windows.Forms.Button btnDeselect;
        private System.Windows.Forms.ComboBox cbDigest;
        private System.Windows.Forms.Label lbDigest;
        private System.Windows.Forms.CheckBox chkCritical;
    }
}