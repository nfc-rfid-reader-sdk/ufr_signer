namespace uFRSigner
{
    partial class frmKeyUsage
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.gbKeyUsage = new System.Windows.Forms.GroupBox();
            this.chkDecipherOnly = new System.Windows.Forms.CheckBox();
            this.chkEncipherOnly = new System.Windows.Forms.CheckBox();
            this.chkCRLSign = new System.Windows.Forms.CheckBox();
            this.chkKeyCertSign = new System.Windows.Forms.CheckBox();
            this.chkKeyAgreement = new System.Windows.Forms.CheckBox();
            this.chkDataEncipherment = new System.Windows.Forms.CheckBox();
            this.chkKeyEncipherment = new System.Windows.Forms.CheckBox();
            this.chkNonRepudiation = new System.Windows.Forms.CheckBox();
            this.chkDigitalSignature = new System.Windows.Forms.CheckBox();
            this.gbKeyUsage.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(270, 47);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(118, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(270, 18);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(118, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // gbKeyUsage
            // 
            this.gbKeyUsage.Controls.Add(this.chkDecipherOnly);
            this.gbKeyUsage.Controls.Add(this.chkEncipherOnly);
            this.gbKeyUsage.Controls.Add(this.chkCRLSign);
            this.gbKeyUsage.Controls.Add(this.chkKeyCertSign);
            this.gbKeyUsage.Controls.Add(this.chkKeyAgreement);
            this.gbKeyUsage.Controls.Add(this.chkDataEncipherment);
            this.gbKeyUsage.Controls.Add(this.chkKeyEncipherment);
            this.gbKeyUsage.Controls.Add(this.chkNonRepudiation);
            this.gbKeyUsage.Controls.Add(this.chkDigitalSignature);
            this.gbKeyUsage.Location = new System.Drawing.Point(12, 12);
            this.gbKeyUsage.Name = "gbKeyUsage";
            this.gbKeyUsage.Size = new System.Drawing.Size(252, 231);
            this.gbKeyUsage.TabIndex = 0;
            this.gbKeyUsage.TabStop = false;
            this.gbKeyUsage.Text = "Key Usage:";
            // 
            // chkDecipherOnly
            // 
            this.chkDecipherOnly.AutoSize = true;
            this.chkDecipherOnly.Enabled = false;
            this.chkDecipherOnly.Location = new System.Drawing.Point(6, 203);
            this.chkDecipherOnly.Name = "chkDecipherOnly";
            this.chkDecipherOnly.Size = new System.Drawing.Size(91, 17);
            this.chkDecipherOnly.TabIndex = 8;
            this.chkDecipherOnly.Text = "Decipher only";
            this.chkDecipherOnly.UseVisualStyleBackColor = true;
            // 
            // chkEncipherOnly
            // 
            this.chkEncipherOnly.AutoSize = true;
            this.chkEncipherOnly.Enabled = false;
            this.chkEncipherOnly.Location = new System.Drawing.Point(6, 180);
            this.chkEncipherOnly.Name = "chkEncipherOnly";
            this.chkEncipherOnly.Size = new System.Drawing.Size(90, 17);
            this.chkEncipherOnly.TabIndex = 7;
            this.chkEncipherOnly.Text = "Encipher only";
            this.chkEncipherOnly.UseVisualStyleBackColor = true;
            // 
            // chkCRLSign
            // 
            this.chkCRLSign.AutoSize = true;
            this.chkCRLSign.Enabled = false;
            this.chkCRLSign.Location = new System.Drawing.Point(6, 157);
            this.chkCRLSign.Name = "chkCRLSign";
            this.chkCRLSign.Size = new System.Drawing.Size(83, 17);
            this.chkCRLSign.TabIndex = 6;
            this.chkCRLSign.Text = "CRL signing";
            this.chkCRLSign.UseVisualStyleBackColor = true;
            // 
            // chkKeyCertSign
            // 
            this.chkKeyCertSign.AutoSize = true;
            this.chkKeyCertSign.Enabled = false;
            this.chkKeyCertSign.Location = new System.Drawing.Point(6, 134);
            this.chkKeyCertSign.Name = "chkKeyCertSign";
            this.chkKeyCertSign.Size = new System.Drawing.Size(109, 17);
            this.chkKeyCertSign.TabIndex = 5;
            this.chkKeyCertSign.Text = "Certificate signing";
            this.chkKeyCertSign.UseVisualStyleBackColor = true;
            // 
            // chkKeyAgreement
            // 
            this.chkKeyAgreement.AutoSize = true;
            this.chkKeyAgreement.Location = new System.Drawing.Point(6, 111);
            this.chkKeyAgreement.Name = "chkKeyAgreement";
            this.chkKeyAgreement.Size = new System.Drawing.Size(97, 17);
            this.chkKeyAgreement.TabIndex = 4;
            this.chkKeyAgreement.Text = "Key agreement";
            this.chkKeyAgreement.UseVisualStyleBackColor = true;
            this.chkKeyAgreement.CheckedChanged += new System.EventHandler(this.chkKeyAgreement_CheckedChanged);
            // 
            // chkDataEncipherment
            // 
            this.chkDataEncipherment.AutoSize = true;
            this.chkDataEncipherment.Location = new System.Drawing.Point(6, 88);
            this.chkDataEncipherment.Name = "chkDataEncipherment";
            this.chkDataEncipherment.Size = new System.Drawing.Size(116, 17);
            this.chkDataEncipherment.TabIndex = 3;
            this.chkDataEncipherment.Text = "Data encipherment";
            this.chkDataEncipherment.UseVisualStyleBackColor = true;
            // 
            // chkKeyEncipherment
            // 
            this.chkKeyEncipherment.AutoSize = true;
            this.chkKeyEncipherment.Location = new System.Drawing.Point(6, 65);
            this.chkKeyEncipherment.Name = "chkKeyEncipherment";
            this.chkKeyEncipherment.Size = new System.Drawing.Size(111, 17);
            this.chkKeyEncipherment.TabIndex = 2;
            this.chkKeyEncipherment.Text = "Key encipherment";
            this.chkKeyEncipherment.UseVisualStyleBackColor = true;
            // 
            // chkNonRepudiation
            // 
            this.chkNonRepudiation.AutoSize = true;
            this.chkNonRepudiation.Location = new System.Drawing.Point(6, 42);
            this.chkNonRepudiation.Name = "chkNonRepudiation";
            this.chkNonRepudiation.Size = new System.Drawing.Size(101, 17);
            this.chkNonRepudiation.TabIndex = 1;
            this.chkNonRepudiation.Text = "Non-repudiation";
            this.chkNonRepudiation.UseVisualStyleBackColor = true;
            // 
            // chkDigitalSignature
            // 
            this.chkDigitalSignature.AutoSize = true;
            this.chkDigitalSignature.Location = new System.Drawing.Point(6, 19);
            this.chkDigitalSignature.Name = "chkDigitalSignature";
            this.chkDigitalSignature.Size = new System.Drawing.Size(101, 17);
            this.chkDigitalSignature.TabIndex = 0;
            this.chkDigitalSignature.Text = "Digital signature";
            this.chkDigitalSignature.UseVisualStyleBackColor = true;
            // 
            // frmKeyUsage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 256);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.gbKeyUsage);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmKeyUsage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chose Key Usage";
            this.gbKeyUsage.ResumeLayout(false);
            this.gbKeyUsage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox gbKeyUsage;
        private System.Windows.Forms.CheckBox chkDataEncipherment;
        private System.Windows.Forms.CheckBox chkKeyEncipherment;
        private System.Windows.Forms.CheckBox chkNonRepudiation;
        private System.Windows.Forms.CheckBox chkDigitalSignature;
        private System.Windows.Forms.CheckBox chkDecipherOnly;
        private System.Windows.Forms.CheckBox chkEncipherOnly;
        private System.Windows.Forms.CheckBox chkCRLSign;
        private System.Windows.Forms.CheckBox chkKeyCertSign;
        private System.Windows.Forms.CheckBox chkKeyAgreement;
    }
}