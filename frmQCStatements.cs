using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509.Qualified;
using System;
using System.Windows.Forms;

namespace uFRSigner
{
    public partial class frmQCStatements : Form
    {
        private DerSequence mQcStatements = null;
        private string mQcStatementsDescription = "";

        public frmQCStatements()
        {
            InitializeComponent();
        }

        public DerSequence getQcStatements()
        {
            return mQcStatements;
        }

        public string getQcStatementsDescription()
        {
            return mQcStatementsDescription;
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            chkEtsiQcsQcCompliance.Checked = true;
            chkEtsiQcsLimitValue.Checked = false;
            chkEtsiQcsRetentionPeriod.Checked = false;
            chkEtsiQcsQcSscd.Checked = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (chkEtsiQcsQcCompliance.Checked || chkEtsiQcsLimitValue.Checked
                || chkEtsiQcsRetentionPeriod.Checked || chkEtsiQcsQcSscd.Checked)
            {
                mQcStatements = new DerSequence();
                if (chkEtsiQcsQcCompliance.Checked)
                {
                    mQcStatements.AddObject(new QCStatement(EtsiQCObjectIdentifiers.IdEtsiQcsQcCompliance));
                    mQcStatementsDescription += "QcCompliance";
                }
                if (chkEtsiQcsLimitValue.Checked)
                {
                    mQcStatements.AddObject(new QCStatement(EtsiQCObjectIdentifiers.IdEtsiQcsLimitValue));
                    if (mQcStatementsDescription != "")
                        mQcStatementsDescription += ", ";
                    mQcStatementsDescription += "LimitValue";
                }
                if (chkEtsiQcsRetentionPeriod.Checked)
                {
                    mQcStatements.AddObject(new QCStatement(EtsiQCObjectIdentifiers.IdEtsiQcsRetentionPeriod));
                    if (mQcStatementsDescription != "")
                        mQcStatementsDescription += ", ";
                    mQcStatementsDescription += "RetentionPeriod";
                }
                if (chkEtsiQcsQcSscd.Checked)
                {
                    mQcStatements.AddObject(new QCStatement(EtsiQCObjectIdentifiers.IdEtsiQcsQcSscd));
                    if (mQcStatementsDescription != "")
                        mQcStatementsDescription += ", ";
                    mQcStatementsDescription += "QcSscd";
                }
            }
        }
    }
}
