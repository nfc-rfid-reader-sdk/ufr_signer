using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace uFRSigner
{
    public partial class frmKeyUsage : Form
    {
        private KeyUsage mKeyUsage = null;
        private string mKeyUsageDescription = "";

        public frmKeyUsage()
        {
            InitializeComponent();
        }

        private void chkKeyAgreement_CheckedChanged(object sender, EventArgs e)
        {
            chkEncipherOnly.Enabled = chkDecipherOnly.Enabled = chkKeyAgreement.Checked;

            if (!chkKeyAgreement.Checked)
                chkEncipherOnly.Checked = chkDecipherOnly.Checked = false;
        }

        public KeyUsage getKeyUsage()
        {
            return mKeyUsage;
        }

        public string getKeyUsageDescription()
        {
            return mKeyUsageDescription;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int key_usage_bit_map = 0;

            if (chkDigitalSignature.Checked || chkNonRepudiation.Checked || chkKeyEncipherment.Checked || chkDataEncipherment.Checked
                || chkKeyAgreement.Checked)
            {
                if (chkDigitalSignature.Checked)
                {
                    key_usage_bit_map |= KeyUsage.DigitalSignature;
                    mKeyUsageDescription += "digitalSignature";
                }
                if (chkNonRepudiation.Checked)
                {
                    key_usage_bit_map |= KeyUsage.NonRepudiation;
                    if (mKeyUsageDescription != "")
                        mKeyUsageDescription += ", ";
                    mKeyUsageDescription += "nonRepudiation";
                }
                if (chkKeyEncipherment.Checked)
                {
                    key_usage_bit_map |= KeyUsage.KeyEncipherment;
                    if (mKeyUsageDescription != "")
                        mKeyUsageDescription += ", ";
                    mKeyUsageDescription += "keyEncipherment";
                }
                if (chkDataEncipherment.Checked)
                {
                    key_usage_bit_map |= KeyUsage.DataEncipherment;
                    if (mKeyUsageDescription != "")
                        mKeyUsageDescription += ", ";
                    mKeyUsageDescription += "dataEncipherment";
                }
                /*
                // Only for CA root and intermediate => not for leaf certificates:
                KeyCertSign --> "keyCertSign"
                CrlSign --> "cRLSign"
                */
                if (chkKeyAgreement.Checked)
                {
                    key_usage_bit_map |= KeyUsage.KeyAgreement;
                    if (mKeyUsageDescription != "")
                        mKeyUsageDescription += ", ";
                    mKeyUsageDescription += "keyAgreement";

                    if (chkEncipherOnly.Checked)
                    {
                        key_usage_bit_map |= KeyUsage.EncipherOnly;
                        if (mKeyUsageDescription != "")
                            mKeyUsageDescription += ", ";
                        mKeyUsageDescription += "encipherOnly";
                    }
                    if (chkDecipherOnly.Checked)
                    {
                        key_usage_bit_map |= KeyUsage.DecipherOnly;
                        if (mKeyUsageDescription != "")
                            mKeyUsageDescription += ", ";
                        mKeyUsageDescription += "decipherOnly";
                    }
                }

                if (key_usage_bit_map != 0)
                    mKeyUsage = new KeyUsage(key_usage_bit_map);
            }
        }
    }
}
