namespace uFRSigner
{
    partial class frmExtKeyUsage
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
            this.gbIdKP = new System.Windows.Forms.GroupBox();
            this.chkMacAddress = new System.Windows.Forms.CheckBox();
            this.chkSmartCardLogon = new System.Windows.Forms.CheckBox();
            this.chkOcspSigning = new System.Windows.Forms.CheckBox();
            this.chkTimeStamping = new System.Windows.Forms.CheckBox();
            this.chkIpsecUser = new System.Windows.Forms.CheckBox();
            this.chkIpsecTunnel = new System.Windows.Forms.CheckBox();
            this.chkIpsecEndSystem = new System.Windows.Forms.CheckBox();
            this.chkEmailProtection = new System.Windows.Forms.CheckBox();
            this.chkCodeSigning = new System.Windows.Forms.CheckBox();
            this.chkClientAuth = new System.Windows.Forms.CheckBox();
            this.chkServerAuth = new System.Windows.Forms.CheckBox();
            this.chkAnyExtendedKeyUsage = new System.Windows.Forms.CheckBox();
            this.gbIdKP.SuspendLayout();
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
            // gbIdKP
            // 
            this.gbIdKP.Controls.Add(this.chkMacAddress);
            this.gbIdKP.Controls.Add(this.chkSmartCardLogon);
            this.gbIdKP.Controls.Add(this.chkOcspSigning);
            this.gbIdKP.Controls.Add(this.chkTimeStamping);
            this.gbIdKP.Controls.Add(this.chkIpsecUser);
            this.gbIdKP.Controls.Add(this.chkIpsecTunnel);
            this.gbIdKP.Controls.Add(this.chkIpsecEndSystem);
            this.gbIdKP.Controls.Add(this.chkEmailProtection);
            this.gbIdKP.Controls.Add(this.chkCodeSigning);
            this.gbIdKP.Controls.Add(this.chkClientAuth);
            this.gbIdKP.Controls.Add(this.chkServerAuth);
            this.gbIdKP.Controls.Add(this.chkAnyExtendedKeyUsage);
            this.gbIdKP.Location = new System.Drawing.Point(12, 12);
            this.gbIdKP.Name = "gbIdKP";
            this.gbIdKP.Size = new System.Drawing.Size(252, 298);
            this.gbIdKP.TabIndex = 0;
            this.gbIdKP.TabStop = false;
            this.gbIdKP.Text = "Key Purpose ID (IdKP):";
            // 
            // chkMacAddress
            // 
            this.chkMacAddress.AutoSize = true;
            this.chkMacAddress.Location = new System.Drawing.Point(6, 269);
            this.chkMacAddress.Name = "chkMacAddress";
            this.chkMacAddress.Size = new System.Drawing.Size(85, 17);
            this.chkMacAddress.TabIndex = 11;
            this.chkMacAddress.Text = "MacAddress";
            this.chkMacAddress.UseVisualStyleBackColor = true;
            // 
            // chkSmartCardLogon
            // 
            this.chkSmartCardLogon.AutoSize = true;
            this.chkSmartCardLogon.Location = new System.Drawing.Point(6, 246);
            this.chkSmartCardLogon.Name = "chkSmartCardLogon";
            this.chkSmartCardLogon.Size = new System.Drawing.Size(105, 17);
            this.chkSmartCardLogon.TabIndex = 10;
            this.chkSmartCardLogon.Text = "SmartCardLogon";
            this.chkSmartCardLogon.UseVisualStyleBackColor = true;
            // 
            // chkOcspSigning
            // 
            this.chkOcspSigning.AutoSize = true;
            this.chkOcspSigning.Location = new System.Drawing.Point(6, 223);
            this.chkOcspSigning.Name = "chkOcspSigning";
            this.chkOcspSigning.Size = new System.Drawing.Size(86, 17);
            this.chkOcspSigning.TabIndex = 9;
            this.chkOcspSigning.Text = "OcspSigning";
            this.chkOcspSigning.UseVisualStyleBackColor = true;
            // 
            // chkTimeStamping
            // 
            this.chkTimeStamping.AutoSize = true;
            this.chkTimeStamping.Location = new System.Drawing.Point(6, 203);
            this.chkTimeStamping.Name = "chkTimeStamping";
            this.chkTimeStamping.Size = new System.Drawing.Size(93, 17);
            this.chkTimeStamping.TabIndex = 8;
            this.chkTimeStamping.Text = "TimeStamping";
            this.chkTimeStamping.UseVisualStyleBackColor = true;
            // 
            // chkIpsecUser
            // 
            this.chkIpsecUser.AutoSize = true;
            this.chkIpsecUser.Location = new System.Drawing.Point(6, 180);
            this.chkIpsecUser.Name = "chkIpsecUser";
            this.chkIpsecUser.Size = new System.Drawing.Size(74, 17);
            this.chkIpsecUser.TabIndex = 7;
            this.chkIpsecUser.Text = "IpsecUser";
            this.chkIpsecUser.UseVisualStyleBackColor = true;
            // 
            // chkIpsecTunnel
            // 
            this.chkIpsecTunnel.AutoSize = true;
            this.chkIpsecTunnel.Location = new System.Drawing.Point(6, 157);
            this.chkIpsecTunnel.Name = "chkIpsecTunnel";
            this.chkIpsecTunnel.Size = new System.Drawing.Size(85, 17);
            this.chkIpsecTunnel.TabIndex = 6;
            this.chkIpsecTunnel.Text = "IpsecTunnel";
            this.chkIpsecTunnel.UseVisualStyleBackColor = true;
            // 
            // chkIpsecEndSystem
            // 
            this.chkIpsecEndSystem.AutoSize = true;
            this.chkIpsecEndSystem.Location = new System.Drawing.Point(6, 134);
            this.chkIpsecEndSystem.Name = "chkIpsecEndSystem";
            this.chkIpsecEndSystem.Size = new System.Drawing.Size(105, 17);
            this.chkIpsecEndSystem.TabIndex = 5;
            this.chkIpsecEndSystem.Text = "IpsecEndSystem";
            this.chkIpsecEndSystem.UseVisualStyleBackColor = true;
            // 
            // chkEmailProtection
            // 
            this.chkEmailProtection.AutoSize = true;
            this.chkEmailProtection.Location = new System.Drawing.Point(6, 111);
            this.chkEmailProtection.Name = "chkEmailProtection";
            this.chkEmailProtection.Size = new System.Drawing.Size(99, 17);
            this.chkEmailProtection.TabIndex = 4;
            this.chkEmailProtection.Text = "EmailProtection";
            this.chkEmailProtection.UseVisualStyleBackColor = true;
            // 
            // chkCodeSigning
            // 
            this.chkCodeSigning.AutoSize = true;
            this.chkCodeSigning.Location = new System.Drawing.Point(6, 88);
            this.chkCodeSigning.Name = "chkCodeSigning";
            this.chkCodeSigning.Size = new System.Drawing.Size(86, 17);
            this.chkCodeSigning.TabIndex = 3;
            this.chkCodeSigning.Text = "CodeSigning";
            this.chkCodeSigning.UseVisualStyleBackColor = true;
            // 
            // chkClientAuth
            // 
            this.chkClientAuth.AutoSize = true;
            this.chkClientAuth.Location = new System.Drawing.Point(6, 65);
            this.chkClientAuth.Name = "chkClientAuth";
            this.chkClientAuth.Size = new System.Drawing.Size(74, 17);
            this.chkClientAuth.TabIndex = 2;
            this.chkClientAuth.Text = "ClientAuth";
            this.chkClientAuth.UseVisualStyleBackColor = true;
            // 
            // chkServerAuth
            // 
            this.chkServerAuth.AutoSize = true;
            this.chkServerAuth.Location = new System.Drawing.Point(6, 42);
            this.chkServerAuth.Name = "chkServerAuth";
            this.chkServerAuth.Size = new System.Drawing.Size(79, 17);
            this.chkServerAuth.TabIndex = 1;
            this.chkServerAuth.Text = "ServerAuth";
            this.chkServerAuth.UseVisualStyleBackColor = true;
            // 
            // chkAnyExtendedKeyUsage
            // 
            this.chkAnyExtendedKeyUsage.AutoSize = true;
            this.chkAnyExtendedKeyUsage.Location = new System.Drawing.Point(6, 19);
            this.chkAnyExtendedKeyUsage.Name = "chkAnyExtendedKeyUsage";
            this.chkAnyExtendedKeyUsage.Size = new System.Drawing.Size(138, 17);
            this.chkAnyExtendedKeyUsage.TabIndex = 0;
            this.chkAnyExtendedKeyUsage.Text = "AnyExtendedKeyUsage";
            this.chkAnyExtendedKeyUsage.UseVisualStyleBackColor = true;
            // 
            // frmExtKeyUsage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 321);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.gbIdKP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExtKeyUsage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chose Extended Key Usage";
            this.gbIdKP.ResumeLayout(false);
            this.gbIdKP.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox gbIdKP;
        private System.Windows.Forms.CheckBox chkTimeStamping;
        private System.Windows.Forms.CheckBox chkIpsecUser;
        private System.Windows.Forms.CheckBox chkIpsecTunnel;
        private System.Windows.Forms.CheckBox chkIpsecEndSystem;
        private System.Windows.Forms.CheckBox chkEmailProtection;
        private System.Windows.Forms.CheckBox chkCodeSigning;
        private System.Windows.Forms.CheckBox chkClientAuth;
        private System.Windows.Forms.CheckBox chkServerAuth;
        private System.Windows.Forms.CheckBox chkAnyExtendedKeyUsage;
        private System.Windows.Forms.CheckBox chkSmartCardLogon;
        private System.Windows.Forms.CheckBox chkOcspSigning;
        private System.Windows.Forms.CheckBox chkMacAddress;
    }
}