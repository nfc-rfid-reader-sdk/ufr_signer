using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Windows.Forms;

namespace uFRSigner
{
    public partial class frmExtKeyUsage : Form
    {
        private DerSequence mExtKeyUsage = null;
        private string mExtKeyUsageDescription = "";

        public frmExtKeyUsage()
        {
            InitializeComponent();
        }

        public ExtendedKeyUsage getExtKeyUsage()
        {
            return new ExtendedKeyUsage(mExtKeyUsage);
        }

        public string getExtKeyUsageDescription()
        {
            return mExtKeyUsageDescription;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            bool any_of_the_idkp_checked = false;

            foreach (Control ctrl in gbIdKP.Controls)
            {
                if (ctrl.GetType().ToString().Equals("System.Windows.Forms.CheckBox") && ((CheckBox)ctrl).Checked == true)
                {
                    any_of_the_idkp_checked = true;
                    break;
                }
            }

            if (any_of_the_idkp_checked)
            {
                mExtKeyUsage = new DerSequence();
                if (chkAnyExtendedKeyUsage.Checked)
                {
                    mExtKeyUsage.AddObject(KeyPurposeID.AnyExtendedKeyUsage);
                    mExtKeyUsageDescription += "anyExtendedKeyUsage";
                }
                if (chkServerAuth.Checked)
                {
                    mExtKeyUsage.AddObject(KeyPurposeID.IdKPServerAuth);
                    if (mExtKeyUsageDescription != "")
                        mExtKeyUsageDescription += ", ";
                    mExtKeyUsageDescription += "serverAuth";
                }
                if (chkClientAuth.Checked)
                {
                    mExtKeyUsage.AddObject(KeyPurposeID.IdKPClientAuth);
                    if (mExtKeyUsageDescription != "")
                        mExtKeyUsageDescription += ", ";
                    mExtKeyUsageDescription += "clientAuth";
                }
                if (chkCodeSigning.Checked)
                {
                    mExtKeyUsage.AddObject(KeyPurposeID.IdKPCodeSigning);
                    if (mExtKeyUsageDescription != "")
                        mExtKeyUsageDescription += ", ";
                    mExtKeyUsageDescription += "codeSigning";
                }
                if (chkEmailProtection.Checked)
                {
                    mExtKeyUsage.AddObject(KeyPurposeID.IdKPEmailProtection);
                    if (mExtKeyUsageDescription != "")
                        mExtKeyUsageDescription += ", ";
                    mExtKeyUsageDescription += "emailProtection";
                }
                if (chkIpsecEndSystem.Checked)
                {
                    mExtKeyUsage.AddObject(KeyPurposeID.IdKPIpsecEndSystem);
                    if (mExtKeyUsageDescription != "")
                        mExtKeyUsageDescription += ", ";
                    mExtKeyUsageDescription += "ipsecEndSystem";
                }
                if (chkIpsecTunnel.Checked)
                {
                    mExtKeyUsage.AddObject(KeyPurposeID.IdKPIpsecTunnel);
                    if (mExtKeyUsageDescription != "")
                        mExtKeyUsageDescription += ", ";
                    mExtKeyUsageDescription += "ipsecTunnel";
                }
                if (chkIpsecUser.Checked)
                {
                    mExtKeyUsage.AddObject(KeyPurposeID.IdKPIpsecUser);
                    if (mExtKeyUsageDescription != "")
                        mExtKeyUsageDescription += ", ";
                    mExtKeyUsageDescription += "ipsecUser";
                }
                if (chkTimeStamping.Checked)
                {
                    mExtKeyUsage.AddObject(KeyPurposeID.IdKPTimeStamping);
                    if (mExtKeyUsageDescription != "")
                        mExtKeyUsageDescription += ", ";
                    mExtKeyUsageDescription += "timeStamping";
                }
                if (chkOcspSigning.Checked)
                {
                    mExtKeyUsage.AddObject(KeyPurposeID.IdKPOcspSigning);
                    if (mExtKeyUsageDescription != "")
                        mExtKeyUsageDescription += ", ";
                    mExtKeyUsageDescription += "ocspSigning";
                }
                if (chkSmartCardLogon.Checked)
                {
                    mExtKeyUsage.AddObject(KeyPurposeID.IdKPSmartCardLogon);
                    if (mExtKeyUsageDescription != "")
                        mExtKeyUsageDescription += ", ";
                    mExtKeyUsageDescription += "smartCardLogon";
                }
                if (chkMacAddress.Checked)
                {
                    mExtKeyUsage.AddObject(KeyPurposeID.IdKPMacAddress);
                    if (mExtKeyUsageDescription != "")
                        mExtKeyUsageDescription += ", ";
                    mExtKeyUsageDescription += "macAddress";
                }
            }
        }
    }
}
