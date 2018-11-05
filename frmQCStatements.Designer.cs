namespace uFRSigner
{
    partial class frmQCStatements
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkEtsiQcsQcSscd = new System.Windows.Forms.CheckBox();
            this.chkEtsiQcsRetentionPeriod = new System.Windows.Forms.CheckBox();
            this.chkEtsiQcsLimitValue = new System.Windows.Forms.CheckBox();
            this.chkEtsiQcsQcCompliance = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDefault = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkEtsiQcsQcSscd);
            this.groupBox1.Controls.Add(this.chkEtsiQcsRetentionPeriod);
            this.groupBox1.Controls.Add(this.chkEtsiQcsLimitValue);
            this.groupBox1.Controls.Add(this.chkEtsiQcsQcCompliance);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 114);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ETSI QC Statements:";
            // 
            // chkEtsiQcsQcSscd
            // 
            this.chkEtsiQcsQcSscd.AutoSize = true;
            this.chkEtsiQcsQcSscd.Checked = true;
            this.chkEtsiQcsQcSscd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEtsiQcsQcSscd.Location = new System.Drawing.Point(6, 88);
            this.chkEtsiQcsQcSscd.Name = "chkEtsiQcsQcSscd";
            this.chkEtsiQcsQcSscd.Size = new System.Drawing.Size(116, 17);
            this.chkEtsiQcsQcSscd.TabIndex = 3;
            this.chkEtsiQcsQcSscd.Text = "ETSI Qcs Qc Sscd";
            this.chkEtsiQcsQcSscd.UseVisualStyleBackColor = true;
            // 
            // chkEtsiQcsRetentionPeriod
            // 
            this.chkEtsiQcsRetentionPeriod.AutoSize = true;
            this.chkEtsiQcsRetentionPeriod.Location = new System.Drawing.Point(6, 65);
            this.chkEtsiQcsRetentionPeriod.Name = "chkEtsiQcsRetentionPeriod";
            this.chkEtsiQcsRetentionPeriod.Size = new System.Drawing.Size(154, 17);
            this.chkEtsiQcsRetentionPeriod.TabIndex = 2;
            this.chkEtsiQcsRetentionPeriod.Text = "ETSI Qcs Retention Period";
            this.chkEtsiQcsRetentionPeriod.UseVisualStyleBackColor = true;
            // 
            // chkEtsiQcsLimitValue
            // 
            this.chkEtsiQcsLimitValue.AutoSize = true;
            this.chkEtsiQcsLimitValue.Location = new System.Drawing.Point(6, 42);
            this.chkEtsiQcsLimitValue.Name = "chkEtsiQcsLimitValue";
            this.chkEtsiQcsLimitValue.Size = new System.Drawing.Size(126, 17);
            this.chkEtsiQcsLimitValue.TabIndex = 1;
            this.chkEtsiQcsLimitValue.Text = "ETSI Qcs Limit Value";
            this.chkEtsiQcsLimitValue.UseVisualStyleBackColor = true;
            // 
            // chkEtsiQcsQcCompliance
            // 
            this.chkEtsiQcsQcCompliance.AutoSize = true;
            this.chkEtsiQcsQcCompliance.Checked = true;
            this.chkEtsiQcsQcCompliance.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEtsiQcsQcCompliance.Location = new System.Drawing.Point(6, 19);
            this.chkEtsiQcsQcCompliance.Name = "chkEtsiQcsQcCompliance";
            this.chkEtsiQcsQcCompliance.Size = new System.Drawing.Size(147, 17);
            this.chkEtsiQcsQcCompliance.TabIndex = 0;
            this.chkEtsiQcsQcCompliance.Text = "ETSI Qcs Qc Compliance";
            this.chkEtsiQcsQcCompliance.UseVisualStyleBackColor = true;
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
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(270, 103);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(118, 23);
            this.btnDefault.TabIndex = 3;
            this.btnDefault.Text = "Default";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // frmQCStatements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 133);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQCStatements";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chose QCStatements";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkEtsiQcsQcSscd;
        private System.Windows.Forms.CheckBox chkEtsiQcsRetentionPeriod;
        private System.Windows.Forms.CheckBox chkEtsiQcsLimitValue;
        private System.Windows.Forms.CheckBox chkEtsiQcsQcCompliance;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDefault;
    }
}