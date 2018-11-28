using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.X509.Qualified;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.Security;
using uFR;
using System.Linq;

namespace uFRSigner
{
    public partial class frmCSR : Form
    {
        static frmCSR mInstance = null;
        List<DerObjectIdentifier> mSubjectItemsOIDs = new List<DerObjectIdentifier>();
        List<string> mSubjectItems = new List<string>();
        List<int> mExtItemsIdxs = new List<int>();
        List<DerObjectIdentifier> mExtItemsOIDs = new List<DerObjectIdentifier>();
        List<X509Extension> mExtItems = new List<X509Extension>();
        AsymmetricKeyParameter mPublicKey = null;
        AsymmetricKeyParameter mPrivateKey = null;
        string mCipherName;
        string mUserPin;
        bool mUserPinRenewed;
        bool mUserPinLoggedIn = false;
        bool mCangedNotSaved = false;
        bool mParametersAreSet = false;
        byte mKeyIndex = 0;

        static public frmCSR getInstance()
        {
            if (mInstance == null)
                mInstance = new frmCSR();

            return mInstance;
        }

        public frmCSR()
        {
            InitializeComponent();
        }

        public void setParameters(AsymmetricKeyParameter public_key, AsymmetricKeyParameter private_key, byte key_idx, string cipher_name,
                                  bool user_pin_loged_in, string user_pin)
        {
            if (public_key != null)
                mPublicKey = public_key;
            else
                mPublicKey = new RsaKeyParameters(false, BigInteger.One, BigInteger.One); // DUMMY RSA public key - for templates

            if (private_key != null)
                mPrivateKey = private_key;
            else
                mPrivateKey = null;

            mUserPinRenewed = false;
            mUserPinLoggedIn = user_pin_loged_in;
            mUserPin = user_pin;

            mKeyIndex = key_idx;
            mCipherName = cipher_name;
            mParametersAreSet = true;

            if (mCipherName.Equals("ECDSA") && !mPublicKey.GetType().ToString().Equals("Org.BouncyCastle.Crypto.Parameters.RsaKeyParameters"))
            {
                lbEcdsaCurve.Text = "using " + Org.BouncyCastle.Asn1.Sec.SecNamedCurves.GetName(((ECPublicKeyParameters)mPublicKey).PublicKeyParamSet);
            }
            else
            {
                lbEcdsaCurve.Text = "";
            }
        }

        private void frmCSR_Load(object sender, EventArgs e)
        {
            mCangedNotSaved = false;
            cbDigest.SelectedIndex = 2;
            cbRdn.SelectedIndex = 0;
            cbExt.SelectedIndex = 0;
            lstDn.SelectedItem = null;
            lstExt.SelectedItem = null;
        }

        private void frmCSR_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (!mParametersAreSet)
                {
                    MessageBox.Show("Dialog parameters not initialized", "Parameters Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                }
                else
                {
                    cbDigest.SelectedIndex = 2;
                    if (mCipherName.Equals("RSA"))
                        cbCipher.SelectedIndex = 0;
                    else
                    {
                        cbCipher.SelectedIndex = 1;
                    }

                    cbKeyIndex.SelectedIndex = mKeyIndex;
                }
            }
            else
            {
                mPublicKey = null;
                mPrivateKey = null;
                mCipherName = null;
                mKeyIndex = 0;
                mParametersAreSet = false;
            }
        }

        private void btnSaveCsr_Click(object sender, EventArgs e)
        {
            saveAs(true);
            return;
            /*
            Pkcs10CertificationRequest csr;

            DerSequence av = new DerSequence();
            av.AddObject(new QCStatement(EtsiQCObjectIdentifiers.IdEtsiQcsQcCompliance));
            av.AddObject(new QCStatement(EtsiQCObjectIdentifiers.IdEtsiQcsQcSscd));

            var extensions = new Dictionary<DerObjectIdentifier, X509Extension>()
            {
                {X509Extensions.BasicConstraints, new X509Extension(true, new DerOctetString(new BasicConstraints(false)))},
                {X509Extensions.KeyUsage, new X509Extension(true, new DerOctetString(new KeyUsage(0x807F)))},
                {X509Extensions.ExtendedKeyUsage, new X509Extension(false, new DerOctetString(new ExtendedKeyUsage(KeyPurposeID.IdKPServerAuth, KeyPurposeID.IdKPClientAuth)))},
                {X509Extensions.QCStatements, new X509Extension(false, new DerOctetString(av))},
            };

            var values = new Dictionary<DerObjectIdentifier, string> {
                {X509Name.CN, "Employee"}, //domain name inside the quotes
                {X509Name.OU, "Team"},
                {X509Name.O, "Company"}, //Organisation's Legal name inside the quotes
                {X509Name.L, "City"},
                {X509Name.ST, "State"},
                {X509Name.C, "RS"},
            };

            /*
            var subjectAlternateNames = new GeneralName[] { };
            if (values[X509Name.CN].StartsWith("www."))
                values[X509Name.CN] = values[X509Name.CN].Substring(4);
            if (!values[X509Name.CN].StartsWith("*.") && subjectAlternateNames.Length == 0)
                subjectAlternateNames = new GeneralName[] { new GeneralName(GeneralName.DnsName, $"www.{values[X509Name.CN]}") };

            if (subjectAlternateNames.Length > 0)
                extensions.Add(X509Extensions.SubjectAlternativeName, new X509Extension(false, new DerOctetString(new GeneralNames(subjectAlternateNames))));
            */

            /*
            GeneralNames subjectAltName = new GeneralNames(new GeneralName(GeneralName.Rfc822Name, "example@example.org"));
            extensions.Add(X509Extensions.SubjectAlternativeName, new X509Extension(false, new DerOctetString(subjectAltName)));
            */

            //var subject = new X509Name(values.Keys.Reverse().ToList(), values);

            //if (ecMode)
            //{
            //signatureFactory = new Asn1SignatureFactory("SHA256withECDSA", pair.Private);

            //extensions.Add(X509Extensions.SubjectKeyIdentifier, new X509Extension(false, new DerOctetString(new SubjectKeyIdentifierStructure(pair.Public))));
            //csr = new Pkcs10CertificationRequest(signatureFactory, subject, pair.Public, new DerSet(new AttributePkcs(PkcsObjectIdentifiers.Pkcs9AtExtensionRequest, new DerSet(new X509Extensions(extensions)))), pair.Private);
            //}
            //else
            //{
            //signatureFactory = new Asn1SignatureFactory("SHA256withRSA", pair.Private);

            /*
            extensions.Add(X509Extensions.SubjectKeyIdentifier, new X509Extension(false, new DerOctetString(new SubjectKeyIdentifierStructure(pair.Public))));
            csr = new Pkcs10CertificationRequest(signatureFactory, subject, pair.Public, new DerSet(new AttributePkcs(PkcsObjectIdentifiers.Pkcs9AtExtensionRequest, new DerSet(new X509Extensions(extensions)))), pair.Private);
            */

            //}

            /*
            mCangedNotSaved = true;

            string signatureAlgorithm = cbDigest.Text.Replace("-", "") + "with" + mCipherName;

            X509Name subject = new X509Name(mSubjectItemsOIDs, mSubjectItems);
            */

            /*
            Attribute attribute = ;
            DerSet attributes = new DerSet(new Attribute(PkcsObjectIdentifiers.Pkcs9AtExtensionRequest, new DerSet(extensions)));
            */

            /*
            Pkcs10CertificationRequestDelaySigned csr_ds =
                new Pkcs10CertificationRequestDelaySigned(signatureAlgorithm, subject, mPublicKey,
                    new DerSet(new AttributePkcs(PkcsObjectIdentifiers.Pkcs9AtExtensionRequest, new DerSet(new X509Extensions(extensions)))));
            byte[] dataToSign = csr_ds.GetDataToSign();
            using (var fs = new FileStream("TbsCsr.bin", FileMode.Create, FileAccess.Write))
            {
                fs.Write(dataToSign, 0, dataToSign.Length);
            }

            getX509ExtensionsFromCsr(csr_ds);
            //csr_ds.SignRequest//*/
        }

        private string der2pem(byte[] DerByteArray, string HeaderFooter)
        {
            const int MAX_LINE_LEN = 64;

            string str_out = "-----BEGIN " + HeaderFooter + "-----\r\n";

#pragma warning disable 0162 // Disable "Unreachable code" warning
            if (MAX_LINE_LEN == 76)
                str_out += Convert.ToBase64String(DerByteArray, Base64FormattingOptions.InsertLineBreaks);
            else
            {
                int i = 0;
                string str_tbs = Convert.ToBase64String(DerByteArray);

                while (i < str_tbs.Length)
                {
                    int chunk = (str_tbs.Length - i) < MAX_LINE_LEN ? str_tbs.Length - i : MAX_LINE_LEN;
                    str_out += str_tbs.Substring(i, chunk) + "\r\n";
                    i += chunk;
                }
            }
#pragma warning restore 0162 // Restore "Unreachable code" warning

            str_out += "-----END " + HeaderFooter + "-----\r\n";

            return str_out;
        }

        private bool saveAs(bool Signed)
        {
            SaveFileDialog dialog;

            try
            {
                if (mSubjectItemsOIDs.Count == 0)
                {
                    if (Signed)
                    {
                        MessageBox.Show("Distinguished name can't be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else
                    {
                        MessageBox.Show("Warning!\r\nDistinguished name shouldn't be empty."
                            + "\r\nThis is OK only if you save \"TBS CSR\" template for future use.",
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        if (mExtItemsOIDs.Count == 0)
                            return false;
                    }
                }

                dialog = new SaveFileDialog();
                dialog.Filter = "CSR files (*.pem)|*.pem|All files (*.*)|*.*";
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    X509Name subject = new X509Name(mSubjectItemsOIDs, mSubjectItems);
                    string signatureAlgorithm = cbDigest.Text.Replace("-", "") + "with" + mCipherName;

                    DerSet attributes = new DerSet();
                    if (mExtItemsOIDs.Count > 0)
                    {
                        attributes.AddObject(new AttributePkcs(PkcsObjectIdentifiers.Pkcs9AtExtensionRequest, new DerSet(
                            new X509Extensions(mExtItemsOIDs, mExtItems)))); // mExtItemsOIDs.Zip(mExtItems, (k, v) => new { Key = k, Value = v })
                                                                             //              .ToDictionary(x => x.Key, x => x.Value)
                    }

                    AsymmetricKeyParameter local_pub_key;

                    if (Signed)
                        local_pub_key = mPublicKey;
                    else
                        local_pub_key = new RsaKeyParameters(false, BigInteger.One, BigInteger.One); // DUMMY RSA public key - for templates

                    Pkcs10CertificationRequestDelaySigned csr_ds =
                        new Pkcs10CertificationRequestDelaySigned(signatureAlgorithm, subject, local_pub_key, attributes);

                    byte[] dataToSign = csr_ds.GetDataToSign();
                    byte[] toSave;
                    string header_footer;

                    if (Signed)
                    {
                        /*/
                        ISigner Signer;
                        Signer = SignerUtilities.GetSigner(signatureAlgorithm);
                        Signer.Init(true, mPrivateKey);
                        Signer.BlockUpdate(dataToSign, 0, dataToSign.Length);

                        byte[] SignedData = Signer.GenerateSignature();
                        //*/

                        byte[] SignedData = uFRSigner(dataToSign); // Rest of the input parameters needed (here accessible directly from UI):
                                                                   //      - digest_alg, signature_cipher, card_key_index
                        csr_ds.SignRequest(SignedData);

                        toSave = csr_ds.GetDerEncoded();
                        header_footer = "CERTIFICATE REQUEST";
                    }
                    else
                    {
                        toSave = dataToSign;
                        header_footer = "TBS CERTIFICATE REQUEST";
                    }

                    var file_extension = Path.GetExtension(dialog.FileName);
                    if (file_extension.Equals(".pem"))
                    {
                        var textWriter = new StreamWriter(dialog.FileName);

                        textWriter.Write(der2pem(toSave, header_footer));
                        textWriter.Flush();
                        textWriter.Close();
                    }
                    else
                    {
                        using (var fs = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write))
                        {
                            fs.Write(toSave, 0, toSave.Length);
                            fs.Flush();
                            fs.Close();
                        }
                    }
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private byte[] uFRSigner(byte[] tbs)
        {
            byte key_index = (byte)cbKeyIndex.SelectedIndex;
            byte jc_signer_cipher = (byte)cbCipher.SelectedIndex;
            byte jc_signer_digest = 0;
            byte jc_signer_padding = 0;
            byte[] signature = null;
            byte[] dataToSign = null;
            DL_STATUS status;
            bool uFR_Selected = false;
            UInt16 key_size_bits;

            byte[] hash = DigestUtilities.CalculateDigest(cbDigest.Text, tbs);

            try
            {
                byte[] aid = Hex.Decode(uFCoder.JCDL_AID);
                byte[] selection_respone = new byte[16];

                status = uFCoder.SetISO14443_4_Mode();
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));
                else
                    uFR_Selected = true;

                status = uFCoder.JCAppSelectByAid(aid, (byte)aid.Length, selection_respone);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));

#if USING_PIN
                bool open_pin_dialog = true;
                if (mUserPinLoggedIn)
                {
                    status = uFCoder.JCAppLogin(false, mUserPin);
                    if (status != DL_STATUS.UFR_OK)
                    {
                        if (((int)status & 0xFFFFC0) == 0x0A63C0)
                        {
                            mUserPinLoggedIn = false;
                            open_pin_dialog = true;
                            MessageBox.Show("Wrong user PIN code. Tries remaining: " + ((int)status & 0x3F),
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                            throw new Exception(string.Format("Card error code: 0x{0:X}", status));
                    }
                }
                if (open_pin_dialog)
                {
                    frmUserPin dlg = new frmUserPin();
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        mUserPinRenewed = true;
                        mUserPinLoggedIn = true;
                        mUserPin = dlg.getUserPin();
                    }
                    else
                    {
                        mUserPinRenewed = false;
                        mUserPinLoggedIn = false;
                        throw new Exception("CSR not signed and saved because you haven't successfully logged in using PIN code.");
                    }
                }
#endif

                Cursor.Current = Cursors.WaitCursor;

                if (cbCipher.Text == "RSA")
                {
                    jc_signer_digest = (byte)uFR.JCDL_SIGNER_DIGESTS.ALG_NULL;
                    jc_signer_padding = (byte)JCDL_SIGNER_PADDINGS.PAD_PKCS1;

                    DerObjectIdentifier oid = null;
                    switch (cbDigest.Text)
                    {
                        case "SHA-1":
                            oid = OiwObjectIdentifiers.IdSha1;
                            break;
                        case "SHA-224":
                            oid = NistObjectIdentifiers.IdSha224;
                            break;
                        case "SHA-256":
                            oid = NistObjectIdentifiers.IdSha256;
                            break;
                        case "SHA-384":
                            oid = NistObjectIdentifiers.IdSha384;
                            break;
                        case "SHA-512":
                            oid = NistObjectIdentifiers.IdSha512;
                            break;
                    }
                    DigestInfo dInfo = new DigestInfo(new AlgorithmIdentifier(oid, DerNull.Instance), hash);
                    dataToSign = dInfo.GetDerEncoded();
                }
                else // ECDSA
                {
                    switch (cbDigest.Text)
                    {
                        case "SHA-1":
                            jc_signer_digest = (byte)uFR.JCDL_SIGNER_DIGESTS.ALG_SHA;
                            break;
                        case "SHA-224":
                            jc_signer_digest = (byte)uFR.JCDL_SIGNER_DIGESTS.ALG_SHA_224;
                            break;
                        case "SHA-256":
                            jc_signer_digest = (byte)uFR.JCDL_SIGNER_DIGESTS.ALG_SHA_256;
                            break;
                        case "SHA-384":
                            jc_signer_digest = (byte)uFR.JCDL_SIGNER_DIGESTS.ALG_SHA_384;
                            break;
                        case "SHA-512":
                            jc_signer_digest = (byte)uFR.JCDL_SIGNER_DIGESTS.ALG_SHA_512;
                            break;
                    }

                    // For ECDSA, first we need key length in bits:
                    UInt16 key_designator;
                    status = uFCoder.JCAppGetEcKeySizeBits(key_index, out key_size_bits, out key_designator);
                    if (status != DL_STATUS.UFR_OK)
                        throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                    // ECDSA hash alignment before signing:
                    dataToSign = Enumerable.Repeat((byte)0, (key_size_bits + 7) / 8).ToArray();
                    if (dataToSign.Length > hash.Length)
                        //Array.Copy(hash, 0, to_be_signed, to_be_signed.Length - hash.Length, hash.Length);
                        dataToSign = hash; // Can be done on J3H145 because supporting Cipher.PAD_NULL with Signature.SIG_CIPHER_ECDSA 
                    else // in case of (to_be_signed.Length <= hash.Length)
                    {
                        Array.Copy(hash, 0, dataToSign, 0, dataToSign.Length);
                        if ((key_size_bits % 8) != 0)
                        {
                            byte prev_byte = 0;
                            byte shift_by = (byte)(key_size_bits % 8);

                            for (int i = 0; i < dataToSign.Length; i++)
                            {
                                byte temp = dataToSign[i];
                                dataToSign[i] >>= 8 - shift_by;
                                dataToSign[i] |= prev_byte;
                                prev_byte = temp <<= shift_by;
                            }
                        }
                    }
                }

                if (dataToSign.Length > uFCoder.SIG_MAX_PLAIN_DATA_LEN)
                {
                    int chunk_len, src_pos, rest_of_data;
                    byte[] chunk = new byte[uFCoder.SIG_MAX_PLAIN_DATA_LEN];

                    rest_of_data = dataToSign.Length - (int)uFCoder.SIG_MAX_PLAIN_DATA_LEN;
                    src_pos = (int)uFCoder.SIG_MAX_PLAIN_DATA_LEN;
                    chunk_len = (int)uFCoder.SIG_MAX_PLAIN_DATA_LEN;
                    Array.Copy(dataToSign, 0, chunk, 0, chunk_len);

                    status = uFCoder.JCAppSignatureBegin(jc_signer_cipher, jc_signer_digest, jc_signer_padding,
                                                        key_index, chunk, (UInt16)chunk_len,
                                                        null, 0);
                    if (status != DL_STATUS.UFR_OK)
                        throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                    while (rest_of_data > 0)
                    {
                        chunk_len = rest_of_data > uFCoder.SIG_MAX_PLAIN_DATA_LEN ? (int)uFCoder.SIG_MAX_PLAIN_DATA_LEN : rest_of_data;
                        Array.Copy(dataToSign, src_pos, chunk, 0, chunk_len);

                        status = uFCoder.JCAppSignatureUpdate(chunk, (UInt16)chunk_len);
                        if (status != DL_STATUS.UFR_OK)
                            throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                        src_pos += chunk_len;
                        rest_of_data -= chunk_len;
                    }

                    status = uFCoder.JCAppSignatureEnd(out signature);
                    if (status != DL_STATUS.UFR_OK)
                        throw new Exception(string.Format("Card error code: 0x{0:X}", status));
                }
                else
                {
                    status = uFCoder.JCAppGenerateSignature(jc_signer_cipher, jc_signer_digest, jc_signer_padding,
                                                            key_index,
                                                            dataToSign, (UInt16)dataToSign.Length,
                                                            out signature,
                                                            null, 0);
                    if (status != DL_STATUS.UFR_OK)
                        throw new Exception(string.Format("Card error code: 0x{0:X}", status));
                }

                if (cbCipher.Text == "ECDSA")
                {
                    // In case of ECDSA signature, last 2 bytes are ushort value representing the key_size in bits
                    int len = signature.Length;
                    key_size_bits = (UInt16)(((UInt16)signature[len - 2] << 8) | signature[len - 1]);
                    int key_size_bytes = (key_size_bits + 7) / 8;
                    byte[] der_sig = new byte[len - 2];
                    Array.Copy(signature, der_sig, len - 2);

                    signature = DLogicAsn1Tools.fixEccSignatureSequence(der_sig);
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;

                if (uFR_Selected)
                {
                    uFCoder.s_block_deselect(100);
                    uFR_Selected = false;
                }
            }

            return signature;
        }

        private void clearEntries()
        {
            cbRdn.SelectedIndex = 0;
            ebRdn.Text = "";
            lstDn.Items.Clear();
            mSubjectItemsOIDs.Clear();
            mSubjectItems.Clear();

            cbExt.SelectedIndex = 0;
            ebExt.Text = "";
            mExtItemsIdxs.Clear();
            lstExt.Items.Clear();

            // mExtDict.Clear();
            mExtItemsIdxs.Clear();
            mExtItemsOIDs.Clear();
            mExtItems.Clear();
        }

        private void btnClearEntries_Click(object sender, EventArgs e)
        {
            if (mCangedNotSaved)
            {
                DialogResult dialogResult = MessageBox.Show("Some changes have not been saved. Do you want to save them?",
                    "Do you want to save changes?", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                {
                    if (!saveAs(false))
                        return;
                    clearEntries();
                    mCangedNotSaved = false;
                }
                else if (dialogResult == DialogResult.No)
                {
                    clearEntries();
                    mCangedNotSaved = false;
                }
                else if (dialogResult == DialogResult.Cancel)
                    return;
            }
            else if (mSubjectItemsOIDs.Count != 0 || mExtItemsOIDs.Count != 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete CSR entries?", "Are you sure?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                    clearEntries();
            }
        }

        private void lstDn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDn.SelectedItem != null)
            {
                cbRdn.Text = derOID2RDName(mSubjectItemsOIDs[lstDn.SelectedIndex]);
                ebRdn.Text = mSubjectItems[lstDn.SelectedIndex];
            }
        }

        private void btnPutRdn_Click(object sender, EventArgs e)
        {
            if (ebRdn.Text[0] == '#')
            {
                MessageBox.Show("'#' character is forbiden at the begining of the RDN string", "Wrong input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (lstDn.SelectedItem == null)
            {
                mSubjectItemsOIDs.Add(getDerOIDByAttrName(cbRdn.Text));
                mSubjectItems.Add(ebRdn.Text);
                lstDn.Items.Add(cbRdn.Text + " = " + ebRdn.Text);
            } 
            else
            {
                mSubjectItemsOIDs.Insert(lstDn.SelectedIndex, getDerOIDByAttrName(cbRdn.Text));
                mSubjectItems.Insert(lstDn.SelectedIndex, ebRdn.Text);
                lstDn.Items.Insert(lstDn.SelectedIndex, cbRdn.Text + " = " + ebRdn.Text);
            }

            mCangedNotSaved = true;
        }

        private void btnRemoveRdn_Click(object sender, EventArgs e)
        {
            if (lstDn.SelectedItem != null)
            {
                mSubjectItemsOIDs.RemoveAt(lstDn.SelectedIndex);
                mSubjectItems.RemoveAt(lstDn.SelectedIndex);
                lstDn.Items.RemoveAt(lstDn.SelectedIndex);

                mCangedNotSaved = true;
            }
        }

        private void btnUpRdn_Click(object sender, EventArgs e)
        {
            if (lstDn.SelectedItem != null)
            {
                if (lstDn.SelectedIndex > 0)
                {
                    var temp = lstDn.Items[lstDn.SelectedIndex - 1];
                    lstDn.Items[lstDn.SelectedIndex - 1] = lstDn.Items[lstDn.SelectedIndex];
                    lstDn.Items[lstDn.SelectedIndex] = temp;

                    var temp1 = mSubjectItemsOIDs[lstDn.SelectedIndex - 1];
                    mSubjectItemsOIDs[lstDn.SelectedIndex - 1] = mSubjectItemsOIDs[lstDn.SelectedIndex];
                    mSubjectItemsOIDs[lstDn.SelectedIndex] = temp1;

                    var temp2 = mSubjectItems[lstDn.SelectedIndex - 1];
                    mSubjectItems[lstDn.SelectedIndex - 1] = mSubjectItems[lstDn.SelectedIndex];
                    mSubjectItems[lstDn.SelectedIndex] = temp2;

                    lstDn.SelectedIndex--;
                }

                mCangedNotSaved = true;
            }
        }

        private void btnDnRdn_Click(object sender, EventArgs e)
        {
            if (lstDn.SelectedItem != null)
            {
                if (lstDn.SelectedIndex < lstDn.Items.Count - 1)
                {
                    var temp = lstDn.Items[lstDn.SelectedIndex + 1];
                    lstDn.Items[lstDn.SelectedIndex + 1] = lstDn.Items[lstDn.SelectedIndex];
                    lstDn.Items[lstDn.SelectedIndex] = temp;

                    var temp1 = mSubjectItemsOIDs[lstDn.SelectedIndex + 1];
                    mSubjectItemsOIDs[lstDn.SelectedIndex + 1] = mSubjectItemsOIDs[lstDn.SelectedIndex];
                    mSubjectItemsOIDs[lstDn.SelectedIndex] = temp1;

                    var temp2 = mSubjectItems[lstDn.SelectedIndex + 1];
                    mSubjectItems[lstDn.SelectedIndex + 1] = mSubjectItems[lstDn.SelectedIndex];
                    mSubjectItems[lstDn.SelectedIndex] = temp2;

                    lstDn.SelectedIndex++;
                }

                mCangedNotSaved = true;
            }
        }

        private void btnDeselect_Click(object sender, EventArgs e)
        {
            lstDn.SelectedItem = null;
        }

        private bool isEMailAddrValid(string email_addr)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email_addr);
                if (addr.Address != ebExt.Text)
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("Wrong e-mail address format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool addExtSubjectAltName()
        {
            if (!isEMailAddrValid(ebExt.Text))
                return false;

            /* When work with Dictionary:
            var extensions = new Dictionary<DerObjectIdentifier, X509Extension>()
            GeneralNames subjectAltName = new GeneralNames(new GeneralName(GeneralName.Rfc822Name, ebExt.Text));
            extensions.Add(X509Extensions.SubjectAlternativeName, new X509Extension(false, new DerOctetString(subjectAltName)));
            */
            mExtItemsOIDs.Add(X509Extensions.SubjectAlternativeName);
            mExtItems.Add(new X509Extension(chkCritical.Checked,
                new DerOctetString(new GeneralNames(new GeneralName(GeneralName.Rfc822Name, ebExt.Text)))));

            lstExt.Items.Add(cbExt.Text + ": " + ebExt.Text);
            return true;
        }

        private void replaceExtSubjectAltName(int idx)
        {
            if (!isEMailAddrValid(ebExt.Text))
                return;

            int tmp = mExtItemsIdxs[idx];
            mExtItemsIdxs.RemoveAt(idx);
            mExtItemsOIDs.RemoveAt(idx);
            mExtItems.RemoveAt(idx);
            lstExt.Items.RemoveAt(idx);

            mExtItemsIdxs.Insert(idx, tmp);
            mExtItemsOIDs.Insert(idx, X509Extensions.SubjectAlternativeName);
            mExtItems.Insert(idx, new X509Extension(chkCritical.Checked, new DerOctetString(new GeneralNames(new GeneralName(GeneralName.Rfc822Name, ebExt.Text)))));
            lstExt.Items.Insert(idx, cbExt.Text + ": " + ebExt.Text);

            lstExt.SelectedIndex = idx;
        }

        private bool addKeyUsage()
        {
            /* When work with Dictionary:
            var extensions = new Dictionary<DerObjectIdentifier, X509Extension>()
            extensions.Add(X509Extensions.KeyUsage, new X509Extension(chkCritical.Checked, 
                    new DerOctetString(new KeyUsage(KeyUsage.DigitalSignature | KeyUsage.KeyEncipherment | KeyUsage.DataEncipherment | KeyUsage.NonRepudiation)));
            // OR:
            var extensions = new Dictionary<DerObjectIdentifier, X509Extension>()
            {
                {X509Extensions.KeyUsage, new X509Extension(chkCritical.Checked, 
                    new DerOctetString(new KeyUsage(KeyUsage.DigitalSignature | KeyUsage.KeyEncipherment | KeyUsage.DataEncipherment | KeyUsage.NonRepudiation)))},
            };
            */
            frmKeyUsage dlgKeyUsage = new frmKeyUsage();
            if (dlgKeyUsage.ShowDialog() == DialogResult.OK && dlgKeyUsage.getKeyUsage() != null)
            {
                mExtItemsOIDs.Add(X509Extensions.KeyUsage);
                mExtItems.Add(new X509Extension(chkCritical.Checked, new DerOctetString(dlgKeyUsage.getKeyUsage())));
                lstExt.Items.Add(cbExt.Text + ": " + dlgKeyUsage.getKeyUsageDescription());
                return true;
            }
            return false;
        }

        private void replaceKeyUsage(int idx)
        {
            frmKeyUsage dlgKeyUsage = new frmKeyUsage();
            if (dlgKeyUsage.ShowDialog() == DialogResult.OK && dlgKeyUsage.getKeyUsage() != null)
            {
                int tmp = mExtItemsIdxs[idx];
                mExtItemsIdxs.RemoveAt(idx);
                mExtItemsOIDs.RemoveAt(idx);
                mExtItems.RemoveAt(idx);
                lstExt.Items.RemoveAt(idx);

                mExtItemsIdxs.Insert(idx, tmp);
                mExtItemsOIDs.Insert(idx, X509Extensions.KeyUsage);
                mExtItems.Insert(idx, new X509Extension(chkCritical.Checked, new DerOctetString(dlgKeyUsage.getKeyUsage())));
                lstExt.Items.Insert(idx, cbExt.Text + ": " + dlgKeyUsage.getKeyUsageDescription());

                lstExt.SelectedIndex = idx;
            }
        }

        private bool addExtendedKeyUsage()
        {
            /* When work with Dictionary:
            var extensions = new Dictionary<DerObjectIdentifier, X509Extension>()
            {
                {X509Extensions.ExtendedKeyUsage, new X509Extension(chkCritical.Checked, 
                    new DerOctetString(new ExtendedKeyUsage(KeyPurposeID.IdKPServerAuth, KeyPurposeID.IdKPClientAuth)))},
            };
            */
            frmExtKeyUsage dlgExtKeyUsage = new frmExtKeyUsage();
            if (dlgExtKeyUsage.ShowDialog() == DialogResult.OK)
            {
                if (dlgExtKeyUsage.getExtKeyUsage() != null)
                {
                    mExtItemsOIDs.Add(X509Extensions.ExtendedKeyUsage);
                    mExtItems.Add(new X509Extension(chkCritical.Checked, new DerOctetString(dlgExtKeyUsage.getExtKeyUsage())));
                    lstExt.Items.Add(cbExt.Text + ": " + dlgExtKeyUsage.getExtKeyUsageDescription());
                    return true;
                }
            }
            return false;
        }

        private void replaceExtendedKeyUsage(int idx)
        {
            frmExtKeyUsage dlgExtKeyUsage = new frmExtKeyUsage();
            if (dlgExtKeyUsage.ShowDialog() == DialogResult.OK && dlgExtKeyUsage.getExtKeyUsage() != null)
            {
                int tmp = mExtItemsIdxs[idx];
                mExtItemsIdxs.RemoveAt(idx);
                mExtItemsOIDs.RemoveAt(idx);
                mExtItems.RemoveAt(idx);
                lstExt.Items.RemoveAt(idx);

                mExtItemsIdxs.Insert(idx, tmp);
                mExtItemsOIDs.Insert(idx, X509Extensions.ExtendedKeyUsage);
                mExtItems.Insert(idx, new X509Extension(chkCritical.Checked, new DerOctetString(dlgExtKeyUsage.getExtKeyUsage())));
                lstExt.Items.Insert(idx, cbExt.Text + ": " + dlgExtKeyUsage.getExtKeyUsageDescription());

                lstExt.SelectedIndex = idx;
            }
        }

        private bool addExtQCStatements()
        {
            /* When work with Dictionary:
            DerSequence qc_statements = new DerSequence();
            qc_statements.AddObject(new QCStatement(EtsiQCObjectIdentifiers.IdEtsiQcsQcCompliance));
            qc_statements.AddObject(new QCStatement(EtsiQCObjectIdentifiers.IdEtsiQcsQcSscd));
            extensions.Add(X509Extensions.QCStatements, new X509Extension(false, new DerOctetString(qc_statements)));
            */

            frmQCStatements dlgQcStatements = new frmQCStatements();
            if (dlgQcStatements.ShowDialog() == DialogResult.OK && dlgQcStatements.getQcStatements() != null)
            {
                mExtItemsOIDs.Add(X509Extensions.QCStatements);
                mExtItems.Add(new X509Extension(chkCritical.Checked, new DerOctetString(dlgQcStatements.getQcStatements())));
                lstExt.Items.Add(cbExt.Text + ": " + dlgQcStatements.getQcStatementsDescription());
                return true;
            }
            return false;
        }

        private void replaceExtQCStatements(int idx)
        {
            frmQCStatements dlgQcStatements = new frmQCStatements();
            if (dlgQcStatements.ShowDialog() == DialogResult.OK && dlgQcStatements.getQcStatements() != null)
            {
                int tmp = mExtItemsIdxs[idx];
                mExtItemsIdxs.RemoveAt(idx);
                mExtItemsOIDs.RemoveAt(idx);
                mExtItems.RemoveAt(idx);
                lstExt.Items.RemoveAt(idx);

                mExtItemsIdxs.Insert(idx, tmp);
                mExtItemsOIDs.Insert(idx, X509Extensions.QCStatements);
                mExtItems.Insert(idx, new X509Extension(chkCritical.Checked, new DerOctetString(dlgQcStatements.getQcStatements())));
                lstExt.Items.Insert(idx, cbExt.Text + ": " + dlgQcStatements.getQcStatementsDescription());

                lstExt.SelectedIndex = idx;
            }
        }

        private void btnPutExt_Click(object sender, EventArgs e)
        {
            if (mExtItemsIdxs.Contains(cbExt.SelectedIndex))
                return;

            switch (cbExt.SelectedIndex)
            {
                case 0: // "subjectAltName(EmailAddr) [RFC 822 Name]"
                    if (!addExtSubjectAltName())
                        return;
                    break;
                case 1: // "KeyUsage"
                    if (!addKeyUsage())
                        return;
                    break;
                case 2: // "ExtendedKeyUsage"
                    if (!addExtendedKeyUsage())
                        return;
                    break;
                case 3: // "QCStatements"
                    if (!addExtQCStatements())
                        return;
                    break;
                default:
                    return;
            }
            mExtItemsIdxs.Add(cbExt.SelectedIndex);
        }

        private void btnReplaceExtension_Click(object sender, EventArgs e)
        {
            if (!mExtItemsIdxs.Contains(cbExt.SelectedIndex))
                return;
            if (lstExt.SelectedItem == null)
                return;

            switch (cbExt.SelectedIndex)
            {
                case 0: // "subjectAltName :: EmailAddr (RFC 822 Name) :"
                    replaceExtSubjectAltName(lstExt.SelectedIndex);
                    break;
                case 1: // "KeyUsage ::"
                    replaceKeyUsage(lstExt.SelectedIndex);
                    break;
                case 2: // "ExtendedKeyUsage ::"
                    replaceExtendedKeyUsage(lstExt.SelectedIndex);
                    break;
                case 3: // "QCStatements ::"
                    replaceExtQCStatements(lstExt.SelectedIndex);
                    break;
                default:
                    return;
            }

            if (lstExt.SelectedItem == null)
                chkCritical.Checked = false;
        }

        private void btnRemoveExt_Click(object sender, EventArgs e)
        {
            if (lstExt.SelectedItem != null)
            {
                mExtItemsIdxs.RemoveAt(lstExt.SelectedIndex);
                mExtItemsOIDs.RemoveAt(lstExt.SelectedIndex);
                mExtItems.RemoveAt(lstExt.SelectedIndex);
                lstExt.Items.RemoveAt(lstExt.SelectedIndex);

                chkCritical.Checked = false;
            }
        }

        private void lstExt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstExt.SelectedItem != null)
            {
                cbExt.SelectedIndex = mExtItemsIdxs[lstExt.SelectedIndex];
                chkCritical.Checked = mExtItems[lstExt.SelectedIndex].critical;
                if (cbExt.SelectedIndex == 0)
                {
                    string s;
                    parseAltNameEmail((DerOctetString)mExtItems[lstExt.SelectedIndex].Value, out s);
                    ebExt.Text = s;
                }
            }
        }

        private void cbExt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbExt.SelectedIndex == 0)
                ebExt.Enabled = true;
            else
            {
                ebExt.Enabled = false;
                ebExt.Text = "";
            }
            if (mExtItemsIdxs.Contains(cbExt.SelectedIndex))
                lstExt.SelectedIndex = mExtItemsIdxs.IndexOf(cbExt.SelectedIndex);
            else
            {
                lstExt.SelectedItem = null;
                chkCritical.Checked = cbExt.SelectedIndex == 1; // Uncheck except for "KeyUsage ::" (idx = 1)
            }
        }

        private void btnSaveTbsCsr_Click(object sender, EventArgs e)
        {
            saveAs(false);
        }

        Pkcs10CertificationRequestDelaySigned getX509ExtensionsFromCsr(Pkcs10CertificationRequestDelaySigned csr)
        {
            CertificationRequestInfo certificationRequestInfo = csr.GetCertificationRequestInfo();

            Asn1Set attributesAsn1Set = certificationRequestInfo.Attributes;

            // The `Extension Request` attribute is contained within an ASN.1 Set,
            // usually as the first element.
            X509Extensions certificateRequestExtensions = null;

            // There should be only only one attribute in the set. (that is, only
            // the `Extension Request`, but loop through to find it properly)
            AttributePkcs pkcsAttr = (AttributePkcs)attributesAsn1Set[0];

            if (pkcsAttr.AttrType.Equals(PkcsObjectIdentifiers.Pkcs9AtExtensionRequest))
            {
                Asn1InputStream astr = new Asn1InputStream(pkcsAttr.AttrValues.GetDerEncoded());

                Asn1Set set = (Asn1Set)astr.ReadObject();
                astr = new Asn1InputStream(set[0].GetDerEncoded());
                Asn1Sequence seq = (Asn1Sequence)astr.ReadObject();
                for (int i = 0; i < seq.Count; i++)
                {
                    Asn1Sequence k = (Asn1Sequence)seq[i];
                    if (k[0].ToString().Equals(X509Extensions.SubjectAlternativeName.Id))
                    {
                        Asn1OctetString os = (Asn1OctetString)k[1];
                    }
                }
            }
            /*
            if (attribute.getAttrType().equals(PKCSObjectIdentifiers.pkcs_9_at_extensionRequest ))
            {
                // The `Extension Request` attribute is present.
                final ASN1Set attributeValues = attribute.getAttrValues();

                // The X509Extensions are contained as a value of the ASN.1 Set.
                // Assume that it is the first value of the set.
                if (attributeValues.size() >= 1)
                {
                    certificateRequestExtensions = new X509Extensions( (ASN1Sequence) attributeValues
                        .getObjectAt( 0 ) );

                    // No need to search any more.
                    break;
                }
            }
            */
            return csr;
        }

        private void LoadTbsCsr(Asn1Sequence der_tbs, object sender, EventArgs e)
        {
            Asn1Sequence DestinguishedName = null;
            Asn1Sequence Extensions = null;
            Asn1TaggedObject a0 = null;

            if (der_tbs.Count == 4)
            {
                DerInteger ver = DerInteger.GetInstance(der_tbs[0]);
                DestinguishedName = Asn1Sequence.GetInstance(der_tbs[1]);
                Asn1Sequence key = Asn1Sequence.GetInstance(der_tbs[2]);
                a0 = Asn1TaggedObject.GetInstance(der_tbs[3]);
            }

            btnClearEntries_Click(sender, e);
            if (mSubjectItemsOIDs.Count != 0 || mExtItemsIdxs.Count != 0) // Check is it really cleared
                return;

            parseDestinguishedName(DestinguishedName);

            // Check extensions existence:
            Asn1Sequence csr_attr = null;
            try
            {
                if (a0.TagNo == 0)
                {
                    csr_attr = Asn1Sequence.GetInstance(a0, true);
                }
                else
                    throw new Exception();
            }
            catch { }

            if (csr_attr != null)
            {
                // Manage extensions:
                DerObjectIdentifier oid = DerObjectIdentifier.GetInstance(csr_attr[0]);
                if (oid.Id == PkcsObjectIdentifiers.Pkcs9AtExtensionRequest.Id)
                {
                    Asn1Set attr_set = Asn1Set.GetInstance(csr_attr[1]);
                    Extensions = Asn1Sequence.GetInstance(attr_set[0]);
                }

                if (Extensions != null)
                {
                    for (int i = 0; i < Extensions.Count; i++)
                    {
                        Asn1Sequence extension = Asn1Sequence.GetInstance(Extensions[i]);
                        DerOctetString ext_data;
                        bool critical;
                        if (extension.Count == 3 && DerBoolean.GetInstance((extension[1])).IsTrue)
                        {
                            critical = true;
                            ext_data = (DerOctetString)Asn1OctetString.GetInstance(extension[2]);
                        }
                        else
                        {
                            critical = false;
                            ext_data = (DerOctetString)Asn1OctetString.GetInstance(extension[1]);
                        }

                        // in call to mExtItemsIdxs.Add(x) for parameter x must be used index as defined in cbExt
                        string desc = "";
                        if (DerObjectIdentifier.GetInstance(extension[0]).Id.Equals(X509Extensions.SubjectAlternativeName.Id))
                        {
                            if (parseAltNameEmail(ext_data, out desc))
                            {
                                mExtItemsIdxs.Add(0);
                                mExtItemsOIDs.Add(X509Extensions.SubjectAlternativeName);
                                mExtItems.Add(new X509Extension(critical, ext_data));
                                lstExt.Items.Add(cbExt.Items[0] + ": " + desc);
                            }
                        }
                        else if (DerObjectIdentifier.GetInstance(extension[0]).Id.Equals(X509Extensions.KeyUsage.Id))
                        {
                            if (parseKeyUsage(ext_data, out desc))
                            {
                                mExtItemsIdxs.Add(1);
                                mExtItemsOIDs.Add(X509Extensions.KeyUsage);
                                mExtItems.Add(new X509Extension(critical, ext_data));
                                lstExt.Items.Add(cbExt.Items[1] + ": " + desc);
                            }
                        }
                        else if (DerObjectIdentifier.GetInstance(extension[0]).Id.Equals(X509Extensions.ExtendedKeyUsage.Id))
                        {
                            if (parseExtKeyUsage(ext_data, out desc))
                            {
                                mExtItemsIdxs.Add(2);
                                mExtItemsOIDs.Add(X509Extensions.ExtendedKeyUsage);
                                mExtItems.Add(new X509Extension(critical, ext_data));
                                lstExt.Items.Add(cbExt.Items[2] + ": " + desc);
                            }
                        }
                        else if (DerObjectIdentifier.GetInstance(extension[0]).Id.Equals(X509Extensions.QCStatements.Id))
                        {
                            if (parseQCStatements(ext_data, out desc))
                            {
                                mExtItemsIdxs.Add(3);
                                mExtItemsOIDs.Add(X509Extensions.QCStatements);
                                mExtItems.Add(new X509Extension(critical, ext_data));
                                lstExt.Items.Add(cbExt.Items[3] + ": " + desc);
                            }
                        }
                    }
                }
            }
        }

        private void btnLoadTbsCsr_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "PEM files (*.pem;*.crt;*.cer)|*.pem;*.crt;*.cer|All files (*.*)|*.*";
            //dialog.InitialDirectory = @"C:\";
            dialog.Title = "Please select the PEM file";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                TextReader fileStream = null;
                try
                {
                    // Load tbs cert. request from PEM file:
                    fileStream = System.IO.File.OpenText(dialog.FileName);
                    string b64Str = fileStream.ReadToEnd().Replace("\r\n", "")
                        .Replace("-----BEGIN TBS CERTIFICATE REQUEST-----", "").Replace("-----END TBS CERTIFICATE REQUEST-----", "");
                    Byte[] tbs = Convert.FromBase64String(b64Str);

                    Asn1InputStream input = new Asn1InputStream(new MemoryStream(tbs));
                    Asn1Sequence der_tbs = (Asn1Sequence)input.ReadObject();

                    LoadTbsCsr(der_tbs, sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (fileStream != null)
                        fileStream.Close();
                }
            }

            /* Pach to reverse TBS CSR byte array to a Pkcs10CertificationRequestDelaySigned instance:
            DerSequence seq = (DerSequence)decoder.ReadObject();
            DerSequence top = new DerSequence(seq);
            DerSequence sgnAlg = new DerSequence(PkcsObjectIdentifiers.Sha1WithRsaEncryption);
            sgnAlg.AddObject(new DerNull());
            top.AddObject(sgnAlg);
            top.AddObject(new DerBitString(0xFF));
            Pkcs10CertificationRequestDelaySigned csr_ds = new Pkcs10CertificationRequestDelaySigned(top);


            byte[] x = csr_ds.GetDataToSign();

            byte[] b = new byte[] {1,2,3,4,5,6,7,8,9,10};
            csr_ds.SignRequest(b);
            DerBitString sig = csr_ds.Signature;
            */

            /* Schema:
            SEQUENCE(4 elem)

                INTEGER 0                   // CSR version

                SEQUENCE(1 elem)            // Destinguished name
                    SET(x elem)
                        SEQUENCE(2 elem)
                            OBJECT IDENTIFIER 2.5.4.3 commonName(X.520 DN component)
                            UTF8Stringtest
                        SEQUENCE(2 elem)
                        …

                SEQUENCE(2 elem)            // key … RSA key folows:
                    SEQUENCE(2 elem)
                        OBJECT IDENTIFIER 1.2.840.113549.1.1.1 rsaEncryption(PKCS #1)
                        NULL
                    BIT STRING(1 elem)
                        SEQUENCE(2 elem)
                            INTEGER(key_bit_size bits) 84441253225619649293598940536…
                            INTEGER 65537

                [0] (0 elem)                // Attributes + extensions
            */
        }

        private bool parseDestinguishedName(Asn1Sequence DestinguishedName)
        {
            string value = "";

            try
            {
                if (DestinguishedName != null)
                {
                    for (int i = 0; i < DestinguishedName.Count; i++)
                    {
                        Asn1Set set = Asn1Set.GetInstance(DestinguishedName[i]);
                        Asn1Sequence seq = Asn1Sequence.GetInstance(set[0]);

                        DerObjectIdentifier oid = DerObjectIdentifier.GetInstance(seq[0]);

                        if (oid.Equals(X509Name.EmailAddress) || oid.Equals(X509Name.DC))
                        {
                            value = DerIA5String.GetInstance(seq[1]).ToString();
                        }
                        /*
                        if (oid.Equals(X509Name.DateOfBirth))
                        {
                            value = DerGeneralizedTime.GetInstance(seq[1]).ToString();
                        }
                        */
                        else if (oid.Equals(X509Name.C) || oid.Equals(X509Name.SerialNumber)
                                || oid.Equals(X509Name.DnQualifier) || oid.Equals(X509Name.TelephoneNumber))
                        {
                            value = DerPrintableString.GetInstance(seq[1]).ToString();
                        }
                        else
                            value = DerUtf8String.GetInstance(seq[1]).ToString();

                        if (oid != null && value[0] != '#')
                        {
                            mSubjectItemsOIDs.Add(oid);
                            mSubjectItems.Add(value);
                            lstDn.Items.Add(derOID2RDName(oid) + " = " + value);
                        }
                    }

                    return true;

                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        private bool parseAltNameEmail(DerOctetString data, out string description)
        {
            description = "";
            try
            {
                Asn1Sequence seq = Asn1Sequence.GetInstance(data.GetOctets());
                Asn1TaggedObject taged = Asn1TaggedObject.GetInstance(seq[0]);
                description = Encoding.ASCII.GetString(((DerOctetString)taged.GetObject()).GetOctets());
                return true;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return false;
        }

        private bool parseKeyUsage(DerOctetString data, out string description)
        {
            description = "";
            uint bit_map = 0;
            try
            {
                DerBitString bit_str = DerBitString.GetInstance(data.GetOctets());
                bit_map = (uint)bit_str.IntValue;

                if ((bit_map & KeyUsage.DigitalSignature) == KeyUsage.DigitalSignature)
                {
                    description += "digitalSignature";
                }
                if ((bit_map & KeyUsage.NonRepudiation) == KeyUsage.NonRepudiation)
                {
                    if (description != "")
                        description += ", ";
                    description += "nonRepudiation";
                }
                if ((bit_map & KeyUsage.KeyEncipherment) == KeyUsage.KeyEncipherment)
                {
                    if (description != "")
                        description += ", ";
                    description += "keyEncipherment";
                }
                if ((bit_map & KeyUsage.DataEncipherment) == KeyUsage.DataEncipherment)
                {
                    if (description != "")
                        description += ", ";
                    description += "dataEncipherment";
                }
                /*
                // Only for CA root and intermediate => not for leaf certificates:
                KeyCertSign --> "keyCertSign"
                CrlSign --> "cRLSign"
                */
                if ((bit_map & KeyUsage.KeyAgreement) == KeyUsage.KeyAgreement)
                {
                    if (description != "")
                        description += ", ";
                    description += "keyAgreement";

                    if ((bit_map & KeyUsage.EncipherOnly) == KeyUsage.EncipherOnly)
                    {
                        if (description != "")
                            description += ", ";
                        description += "encipherOnly";
                    }
                    if ((bit_map & KeyUsage.DecipherOnly) == KeyUsage.DecipherOnly)
                    {
                        if (description != "")
                            description += ", ";
                        description += "decipherOnly";
                    }
                }

                if (bit_map != 0)
                    return true;
            }
            catch { }

            return false;
        }

        private bool parseExtKeyUsage(DerOctetString data, out string description)
        {
            description = "";
            try
            {
                Asn1Sequence seq = Asn1Sequence.GetInstance(data.GetOctets());

                for (int i = 0; i < seq.Count; i++)
                {
                    DerObjectIdentifier oid = DerObjectIdentifier.GetInstance(seq[i]);

                    if (description != "")
                        description += ", ";

                    if (oid.Id.Equals(KeyPurposeID.AnyExtendedKeyUsage.Id))
                        description += "anyExtendedKeyUsage";
                    else if (oid.Id.Equals(KeyPurposeID.IdKPServerAuth.Id))
                        description += "serverAuth";
                    else if (oid.Id.Equals(KeyPurposeID.IdKPClientAuth.Id))
                        description += "clientAuth";
                    else if (oid.Id.Equals(KeyPurposeID.IdKPCodeSigning.Id))
                        description += "codeSigning";
                    else if (oid.Id.Equals(KeyPurposeID.IdKPEmailProtection.Id))
                        description += "emailProtection";
                    else if (oid.Id.Equals(KeyPurposeID.IdKPIpsecEndSystem.Id))
                        description += "ipsecEndSystem";
                    else if (oid.Id.Equals(KeyPurposeID.IdKPIpsecTunnel.Id))
                        description += "ipsecTunnel";
                    else if (oid.Id.Equals(KeyPurposeID.IdKPIpsecUser.Id))
                        description += "ipsecUser";
                    else if (oid.Id.Equals(KeyPurposeID.IdKPTimeStamping.Id))
                        description += "timeStamping";
                    else if (oid.Id.Equals(KeyPurposeID.IdKPOcspSigning.Id))
                        description += "ocspSigning";
                    else if (oid.Id.Equals(KeyPurposeID.IdKPSmartCardLogon.Id))
                        description += "smartCardLogon";
                    else if (oid.Id.Equals(KeyPurposeID.IdKPMacAddress.Id))
                        description += "macAddress";
                }

                if (!description.Equals(""))
                    return true;
            }
            catch { }

            return false;
        }

        private bool parseQCStatements(DerOctetString data, out string description)
        {
            description = "";
            try
            {
                Asn1Sequence seq_outer = Asn1Sequence.GetInstance(data.GetOctets());

                for (int i = 0; i < seq_outer.Count; i++)
                {
                    Asn1Sequence seq = Asn1Sequence.GetInstance(seq_outer[i]);
                    DerObjectIdentifier oid = DerObjectIdentifier.GetInstance(seq[0]);

                    if (description != "")
                        description += ", ";

                    if (oid.Id.Equals(EtsiQCObjectIdentifiers.IdEtsiQcsQcCompliance.Id))
                        description += "QcCompliance";
                    else if (oid.Id.Equals(EtsiQCObjectIdentifiers.IdEtsiQcsLimitValue.Id))
                        description += "LimitValue";
                    else if (oid.Id.Equals(EtsiQCObjectIdentifiers.IdEtsiQcsRetentionPeriod.Id))
                        description += "RetentionPeriod";
                    else if (oid.Id.Equals(EtsiQCObjectIdentifiers.IdEtsiQcsQcSscd.Id))
                        description += "QcSscd";
                }

                if (!description.Equals(""))
                    return true;
            }
            catch { }

            return false;
        }

        private string derOID2RDName(DerObjectIdentifier oid)
        {
            if (oid.Id.Equals(X509Name.BusinessCategory.Id))
                return "BusinessCategory";
            else if (oid.Id.Equals(X509Name.C.Id))
                return "Country Code (C)";
            else if (oid.Id.Equals(X509Name.CN.Id))
                return "Common Name (CN)";
            else if (oid.Id.Equals(X509Name.CountryOfCitizenship.Id))
                return "CountryOfCitizenship";
            else if (oid.Id.Equals(X509Name.CountryOfResidence.Id))
                return "CountryOfResidence";
            else if (oid.Id.Equals(X509Name.DateOfBirth.Id))
                return "DateOfBirth";
            else if (oid.Id.Equals(X509Name.DC.Id))
                return "Domain component (DC)";
            else if (oid.Id.Equals(X509Name.DmdName.Id))
                return "DmdName";
            else if (oid.Id.Equals(X509Name.DnQualifier.Id))
                return "Domain name qualifier (DNQ)";
            else if (oid.Id.Equals(X509Name.E.Id))
                return "Email address {Deprecated} (E)";
            else if (oid.Id.Equals(X509Name.EmailAddress.Id))
                return "EmailAddress";
            else if (oid.Id.Equals(X509Name.Gender.Id))
                return "Gender";
            else if (oid.Id.Equals(X509Name.Generation.Id))
                return "Generation";
            else if (oid.Id.Equals(X509Name.GivenName.Id))
                return "GivenName";
            else if (oid.Id.Equals(X509Name.Initials.Id))
                return "Initials";
            else if (oid.Id.Equals(X509Name.L.Id))
                return "Locality (L)";
            else if (oid.Id.Equals(X509Name.Name.Id))
                return "Name";
            else if (oid.Id.Equals(X509Name.NameAtBirth.Id))
                return "NameAtBirth";
            else if (oid.Id.Equals(X509Name.O.Id))
                return "Organization (O)";
            else if (oid.Id.Equals(X509Name.OU.Id))
                return "Organizational Unit (OU)";
            else if (oid.Id.Equals(X509Name.PlaceOfBirth.Id))
                return "PlaceOfBirth";
            else if (oid.Id.Equals(X509Name.PostalAddress.Id))
                return "PostalAddress";
            else if (oid.Id.Equals(X509Name.PostalCode.Id))
                return "PostalCode";
            else if (oid.Id.Equals(X509Name.Pseudonym.Id))
                return "Pseudonym";
            else if (oid.Id.Equals(X509Name.SerialNumber.Id))
                return "SerialNumber";
            else if (oid.Id.Equals(X509Name.ST.Id))
                return "State Or Province Name (ST)";
            else if (oid.Id.Equals(X509Name.Street.Id))
                return "Street";
            else if (oid.Id.Equals(X509Name.Surname.Id))
                return "Surname";
            else if (oid.Id.Equals(X509Name.T.Id))
                return "Title (T)";
            else if (oid.Id.Equals(X509Name.TelephoneNumber.Id))
                return "TelephoneNumber";
            else if (oid.Id.Equals(X509Name.UID.Id))
                return "UID";
            else if (oid.Id.Equals(X509Name.UniqueIdentifier.Id))
                return "UniqueIdentifier";
            else if (oid.Id.Equals(X509Name.UnstructuredAddress.Id))
                return "UnstructuredAddress";
            else if (oid.Id.Equals(X509Name.UnstructuredName.Id))
                return "UnstructuredName";
            else
                return "";
        }

        private DerObjectIdentifier getDerOIDByAttrName(string AttrName)
        {
            switch (AttrName)
            {
                case "BusinessCategory":
                    return X509Name.BusinessCategory;
                case "Country Code (C)":
                    return X509Name.C;
                case "Common Name (CN)":
                    return X509Name.CN;
                case "CountryOfCitizenship":
                    return X509Name.CountryOfCitizenship;
                case "CountryOfResidence":
                    return X509Name.CountryOfResidence;
                case "DateOfBirth":
                    return X509Name.DateOfBirth;
                case "Domain component (DC)":
                    return X509Name.DC;
                case "DmdName":
                    return X509Name.DmdName;
                case "Domain name qualifier (DNQ)":
                    return X509Name.DnQualifier;
                case "Email address {Deprecated} (E)":
                    return X509Name.E;
                case "EmailAddress":
                    return X509Name.EmailAddress;
                case "Gender":
                    return X509Name.Gender;
                case "Generation":
                    return X509Name.Generation;
                case "GivenName":
                    return X509Name.GivenName;
                case "Initials":
                    return X509Name.Initials;
                case "Locality (L)":
                    return X509Name.L;
                case "Name":
                    return X509Name.Name;
                case "NameAtBirth":
                    return X509Name.NameAtBirth;
                case "Organization (O)":
                    return X509Name.O;
                case "Organizational Unit (OU)":
                    return X509Name.OU;
                case "PlaceOfBirth":
                    return X509Name.PlaceOfBirth;
                case "PostalAddress":
                    return X509Name.PostalAddress;
                case "PostalCode":
                    return X509Name.PostalCode;
                case "Pseudonym":
                    return X509Name.Pseudonym;
                case "SerialNumber":
                    return X509Name.SerialNumber;
                case "State Or Province Name (ST)":
                    return X509Name.ST;
                case "Street":
                    return X509Name.Street;
                case "Surname":
                    return X509Name.Surname;
                case "Title (T)":
                    return X509Name.T;
                case "TelephoneNumber":
                    return X509Name.TelephoneNumber;
                case "UID":
                    return X509Name.UID;
                case "UniqueIdentifier":
                    return X509Name.UniqueIdentifier;
                case "UnstructuredAddress":
                    return X509Name.UnstructuredAddress;
                case "UnstructuredName":
                    return X509Name.UnstructuredName;
                default:
                    return null;
            }
        }

        private void btnLoadCsr_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "PEM files (*.pem;*.crt;*.cer)|*.pem;*.crt;*.cer|All files (*.*)|*.*";
            //dialog.InitialDirectory = @"C:\";
            dialog.Title = "Please select the PEM file";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                TextReader fileStream = null;
                try
                {
                    // Load tbs cert. request from PEM file:
                    fileStream = System.IO.File.OpenText(dialog.FileName);
                    string b64Str = fileStream.ReadToEnd().Replace("\r\n", "")
                        .Replace("-----BEGIN CERTIFICATE REQUEST-----", "").Replace("-----END CERTIFICATE REQUEST-----", "");
                    Byte[] tbs = Convert.FromBase64String(b64Str);

                    Asn1InputStream input = new Asn1InputStream(new MemoryStream(tbs));
                    Asn1Sequence der_tbs = (Asn1Sequence)input.ReadObject();
                    der_tbs = Asn1Sequence.GetInstance(der_tbs[0]);

                    LoadTbsCsr(der_tbs, sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (fileStream != null)
                        fileStream.Close();
                }
            }
        }
    }
}
