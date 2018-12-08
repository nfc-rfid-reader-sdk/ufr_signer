using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Nist;
using uFR;
using System.Threading;

namespace uFRSigner
{
    public partial class frmMain : Form
    {
        frmCSR frmCsr;
        const UInt32 MIN_UFR_LIB_VERSION = 0x04040005; // bytes from left to right: MSB=MajorVer, MidSB_H=MinorVer, MidSB_L=0, LSB=BuildNum
        const UInt32 MIN_UFR_FW_VERSION = 0x05000007; // bytes from left to right: MSB=MajorVer, MidSB_H=MinorVer, MidSB_L=0, LSB=BuildNum
        string uFR_NotOpenedMessage = "uFR reader not opened.\r\nYou can't work with DL Signer cards.";
        string mCertPassword = "";
        private bool mSOPinLoggedIn = false;
        private bool mUserPinLoggedIn = false;

        private bool uFR_Opened = false;
        private bool uFR_Selected = false;

        ECPrivateKeyParameters mPrivateKey;
        X9ECParameters mECCurve;

        public frmMain()
        {
            InitializeComponent();
        }

        private void uFrOpen()
        {
            DL_STATUS status;
            bool tryDefaultDllPath = false;
            UInt32 version = 0;
            byte version_major;
            byte version_minor;
            UInt16 lib_build;
            byte fw_build;

#if WIN64
            string DllPath = @"..\..\..\lib\windows\x86_64"; // for x64 target
#else
            string DllPath = @"..\..\..\lib\windows\x86"; // for x86 target
#endif
            string path = Directory.GetCurrentDirectory();
            string assemblyProbeDirectory = DllPath;
            Directory.SetCurrentDirectory(assemblyProbeDirectory);
            try
            {
                version = uFCoder.GetDllVersion();
            }
            catch (System.DllNotFoundException)
            {
                tryDefaultDllPath = true;
            }
            catch (System.BadImageFormatException)
            {
                tryDefaultDllPath = true;
            }
            Directory.SetCurrentDirectory(path);
            if (tryDefaultDllPath)
            {
                try
                {
                    version = uFCoder.GetDllVersion();
                }
                catch (Exception)
                {
                    throw new Exception("Can't find "
                        + uFCoder.DLL_NAME
                        + ".\r\nYou will not be able to work with DL Signer cards.");
                }
            }

            // Check lib version:
            version_major = (byte)version;
            version_minor = (byte)(version >> 8);
            lib_build = (ushort)(version >> 16);

            version = ((UInt32)version_major << 24) | ((UInt32)version_minor << 16) | (UInt32)lib_build;
            if (version < MIN_UFR_LIB_VERSION)
            {
                uFR_NotOpenedMessage = "Wrong uFCoder library version.\r\n"
                    + "You can't work with DL Signer cards.\r\n\r\nUse uFCoder library "
                    + (MIN_UFR_LIB_VERSION >> 24) + "." + ((MIN_UFR_LIB_VERSION >> 16) & 0xFF)
                    + "." + (MIN_UFR_LIB_VERSION & 0xFFFF) + " or higher.";
                throw new Exception("Wrong uFCoder library version.\r\n"
                    + "You can't work with DL Signer cards.\r\n\r\nUse uFCoder library "
                    + (MIN_UFR_LIB_VERSION >> 24) + "." + ((MIN_UFR_LIB_VERSION >> 16) & 0xFF)
                    + "." + (MIN_UFR_LIB_VERSION & 0xFFFF) + " or higher.");
            }

            status = uFCoder.ReaderOpen();
            if (status != DL_STATUS.UFR_OK)
            {
                uFR_NotOpenedMessage = "uFR reader not opened.\r\nYou can't work with DL Signer cards."
                    + "\r\n\r\nTry to connect uFR reader and restart application.";
                throw new Exception("Can't open uFR reader.\r\nYou will not be able to work with DL Signer cards.");
            }

            // Check firmware version:
            status = uFCoder.GetReaderFirmwareVersion(out version_major, out version_minor);
            if (status != DL_STATUS.UFR_OK)
            {
                uFCoder.ReaderClose();
                throw new Exception("Can't open uFR reader.\r\nYou will not be able to work with DL Signer cards.");
            }
            status = uFCoder.GetBuildNumber(out fw_build);
            if (status != DL_STATUS.UFR_OK)
            {
                uFCoder.ReaderClose();
                throw new Exception("Can't open uFR reader.\r\nYou will not be able to work with DL Signer cards.");
            }

            version = ((UInt32)version_major << 24) | ((UInt32)version_minor << 16) | (UInt32)fw_build;
            if (version < MIN_UFR_FW_VERSION)
            {
                uFCoder.ReaderClose();
                uFR_NotOpenedMessage = "Wrong uFR firmware version.\r\n"
                    + "You can't work with DL Signer cards.\r\n\r\nPlease update firmware to "
                    + (MIN_UFR_FW_VERSION >> 24) + "." + ((MIN_UFR_FW_VERSION >> 16) & 0xFF)
                    + "." + (MIN_UFR_FW_VERSION & 0xFFFF) + " or higher.";
                throw new Exception("Wrong uFR firmware version.\r\n"
                    + "You will not be able to work with DL Signer cards.\r\n\r\nPlease update firmware to "
                    + (MIN_UFR_FW_VERSION >> 24) + "." + ((MIN_UFR_FW_VERSION >> 16) & 0xFF)
                    + "." + (MIN_UFR_FW_VERSION & 0xFFFF) + " or higher and restart application.");
            }

            uFR_Opened = true;
        }

        private void objIndexComboSetItems(int itemsCount)
        {
            cbObjIndex.Items.Clear();
            for (int i = 0; i < itemsCount; i++)
                cbObjIndex.Items.Add(i.ToString());
            cbObjIndex.SelectedIndex = 0;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
#if HIDE_KEY_TOOLS
            tabControl.TabPages.Remove(tabHidden);
            gbRSAPriv.Visible = gbECPrivKey.Visible = false;
            tbRSAModulus.ReadOnly = true;
            tbRSAPubExp.ReadOnly = true;
            tbECPubKey.ReadOnly = true;
#endif
#if !USING_PIN
            tabControl.TabPages.Remove(tabPinCodes);
#endif
            Text = Text + " v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            frmCsr = frmCSR.getInstance();

            cbRSAKeyIndex.SelectedIndex = 0;
            cbRSAKeyLength.SelectedIndex = 8;

            cbECName.SelectedIndex = 0;
            cbECKeyLength.SelectedIndex = 0;
            cbECKeyIndex.SelectedIndex = 0;

            cbObjType.SelectedIndex = 0;
            cbObjIndex.SelectedIndex = 0;

            cbDigest.SelectedIndex = 3;
            cbCipher.SelectedIndex = 0;
            cbSignatureKeyIndex.SelectedIndex = 0;
            cbHashAlg.SelectedIndex = 0;

            lbCertUsageType.Text = "";

            try
            {
                uFrOpen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (uFR_Selected)
            {
                uFCoder.s_block_deselect(100);
            }
            if (uFR_Opened)
            {
                uFCoder.ReaderClose();
            }
        }

        private void llbDLogicURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.d-logic.net/nfc-rfid-reader-sdk/");
        }

        private byte[] zeroPadArray(byte[] theArray, byte paddingVal, Int32 paddingSize)
        {
            byte[] newArray = new byte[theArray.Length + paddingSize];
            for (UInt32 i = 0; i < paddingSize; i++)
            {
                newArray[i] = paddingVal;
            }
            theArray.CopyTo(newArray, paddingSize);
            return newArray;
        }

        //======================================================================================================================
        // RSA Keys page:
        //======================================================================================================================
        private void btnRSAImportP12_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "PKCS#12 files (*.p12;*.pfx)|*.p12;*.pfx|All files (*.*)|*.*";
            //dialog.InitialDirectory = @"C:\";
            dialog.Title = "Please select the cert file";

            Asn1InputStream decoder = null;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                frmPassword dlgPasswd = new frmPassword();
                if (dlgPasswd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Load your certificate from p12 file
                        X509Certificate2 cert = new X509Certificate2(dialog.FileName, dlgPasswd.password, 
                            X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);
                        var priv_key = cert.PrivateKey as RSACryptoServiceProvider;
                        RSAParameters rsa_params = priv_key.ExportParameters(true);

                        Int32 key_len = cbRSAKeyLength.FindStringExact(priv_key.KeySize.ToString());
                        if (key_len < 0)
                        {
                            MessageBox.Show("Unsupported key length of " + priv_key.KeySize + "bytes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        cbRSAKeyLength.SelectedIndex = key_len;
                        key_len = priv_key.KeySize;

                        tbRSAModulus.Text = BitConverter.ToString(rsa_params.Modulus).Replace("-", "");
                        tbRSAPrivP.Text = BitConverter.ToString(rsa_params.P).Replace("-", "");
                        tbRSAPrivQ.Text = BitConverter.ToString(rsa_params.Q).Replace("-", "");
                        tbRSAPrivPQ.Text = BitConverter.ToString(rsa_params.InverseQ).Replace("-", "");
                        tbRSAPrivDP1.Text = BitConverter.ToString(rsa_params.DP).Replace("-", "");
                        tbRSAPrivDQ1.Text = BitConverter.ToString(rsa_params.DQ).Replace("-", "");
                        tbRSAPrivExp.Text = BitConverter.ToString(rsa_params.D).Replace("-", "");

                        tbRSAPubExp.Text = BitConverter.ToString(rsa_params.Exponent).Replace("-", "");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (decoder != null)
                            decoder.Close();
                    }
                }
            }
        }

        private void btnMkRSAKey_Click(object sender, EventArgs e)
        {
            byte[] modulus;
            byte[] exponent;
            byte[] barr;
            Int32 key_len = Convert.ToInt32(cbRSAKeyLength.Text);
            tbRSAModulus.Text = "";
            tbRSAPrivExp.Text = "";
            tbRSAPrivP.Text = "";
            tbRSAPrivQ.Text = "";
            tbRSAPrivPQ.Text = "";
            tbRSAPrivDP1.Text = "";
            tbRSAPrivDQ1.Text = "";
            tbRSAPubExp.Text = "";
            Refresh();

            Cursor.Current = Cursors.WaitCursor;

            RsaKeyPairGenerator gen = new RsaKeyPairGenerator();
            KeyGenerationParameters genParams = new KeyGenerationParameters(new SecureRandom(), key_len);
            gen.Init(genParams);

            AsymmetricCipherKeyPair KeyPair = gen.GenerateKeyPair();

            modulus = ((RsaPrivateCrtKeyParameters)KeyPair.Private).Modulus.ToByteArray();
            exponent = ((RsaKeyParameters)KeyPair.Public).Exponent.ToByteArray();
            // Remove leading zeros:
            while (modulus[0] == 0 && modulus.Length > key_len / 8)
            {
                modulus = modulus.Skip(1).ToArray();
            }
            if (key_len / 8 > modulus.Length)
                modulus = zeroPadArray(modulus, 0, key_len / 8 - modulus.Length);

            while (exponent[0] == 0 && exponent.Length > 1)
            {
                exponent = exponent.Skip(1).ToArray();
            }

            tbRSAModulus.Text = BitConverter.ToString(modulus).Replace("-", "");
            tbRSAPubExp.Text = BitConverter.ToString(exponent).Replace("-", "");

            barr = ((RsaPrivateCrtKeyParameters)KeyPair.Private).Exponent.ToByteArray();
            while (barr[0] == 0 && barr.Length > key_len / 16)
            {
                barr = barr.Skip(1).ToArray();
            }
            if (key_len / 16 > barr.Length)
                barr = zeroPadArray(barr, 0, key_len / 16 - barr.Length);
            tbRSAPrivExp.Text = BitConverter.ToString(barr).Replace("-", "");
            barr = ((RsaPrivateCrtKeyParameters)KeyPair.Private).P.ToByteArray();
            while (barr[0] == 0 && barr.Length > key_len / 16)
            {
                barr = barr.Skip(1).ToArray();
            }
            if (key_len / 16 > barr.Length)
                barr = zeroPadArray(barr, 0, key_len / 16 - barr.Length);
            tbRSAPrivP.Text = BitConverter.ToString(barr).Replace("-", "");
            barr = ((RsaPrivateCrtKeyParameters)KeyPair.Private).Q.ToByteArray();
            while (barr[0] == 0 && barr.Length > key_len / 16)
            {
                barr = barr.Skip(1).ToArray();
            }
            tbRSAPrivQ.Text = BitConverter.ToString(barr).Replace("-", "");
            barr = ((RsaPrivateCrtKeyParameters)KeyPair.Private).QInv.ToByteArray();
            while (barr[0] == 0 && barr.Length > key_len / 16)
            {
                barr = barr.Skip(1).ToArray();
            }
            if (key_len / 16 > barr.Length)
                barr = zeroPadArray(barr, 0, key_len / 16 - barr.Length);
            tbRSAPrivPQ.Text = BitConverter.ToString(barr).Replace("-", "");
            barr = ((RsaPrivateCrtKeyParameters)KeyPair.Private).DP.ToByteArray();
            while (barr[0] == 0 && barr.Length > key_len / 16)
            {
                barr = barr.Skip(1).ToArray();
            }
            if (key_len / 16 > barr.Length)
                barr = zeroPadArray(barr, 0, key_len / 16 - barr.Length);
            tbRSAPrivDP1.Text = BitConverter.ToString(barr).Replace("-", "");
            barr = ((RsaPrivateCrtKeyParameters)KeyPair.Private).DQ.ToByteArray();
            while (barr[0] == 0 && barr.Length > key_len / 16)
            {
                barr = barr.Skip(1).ToArray();
            }
            if (key_len / 16 > barr.Length)
                barr = zeroPadArray(barr, 0, key_len / 16 - barr.Length);
            tbRSAPrivDQ1.Text = BitConverter.ToString(barr).Replace("-", "");

            Cursor.Current = Cursors.Default;
        }

        private void btnBackupRSAPriv_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog;

            try
            {
                dialog = new SaveFileDialog();
                dialog.Filter = "Signature binary files (*.pem)|*.pem|All files (*.*)|*.*";
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var key = new RsaPrivateCrtKeyParameters(
                        new BigInteger(1, Hex.Decode(tbRSAModulus.Text)),
                        new BigInteger(1, Hex.Decode(tbRSAPubExp.Text)),
                        new BigInteger(1, Hex.Decode(tbRSAPrivExp.Text)),
                        new BigInteger(1, Hex.Decode(tbRSAPrivP.Text)),
                        new BigInteger(1, Hex.Decode(tbRSAPrivQ.Text)),
                        new BigInteger(1, Hex.Decode(tbRSAPrivDP1.Text)),
                        new BigInteger(1, Hex.Decode(tbRSAPrivDQ1.Text)),
                        new BigInteger(1, Hex.Decode(tbRSAPrivPQ.Text))
                        );

                    var textWriter = new StreamWriter(dialog.FileName);
                    var pemWr = new PemWriter(textWriter);
                    pemWr.WriteObject(key);
                    pemWr.Writer.Flush();
                    textWriter.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestoreRSAPriv_Click(object sender, EventArgs e)
        {
            Int32 key_len, key_idx;
            byte[] modulus, d, p, q, pq, dp, dq;
            byte[] pub_exp;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "PEM files (*.pem;*.crt;*.cer)|*.pem;*.crt;*.cer|All files (*.*)|*.*";
            //dialog.InitialDirectory = @"C:\";
            dialog.Title = "Please select the PEM file";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Load your certificate from PEM file
                    TextReader fileStream = System.IO.File.OpenText(dialog.FileName);
                    PemReader pemReader = new Org.BouncyCastle.OpenSsl.PemReader(fileStream);
                    object KeyParameter = pemReader.ReadObject();

                    if (KeyParameter.GetType() == typeof(Org.BouncyCastle.X509.X509Certificate))
                    {
                        // Load your certificate:
                        X509Certificate2 certificate = new X509Certificate2(dialog.FileName);
                        KeyParameter = Org.BouncyCastle.Security.DotNetUtilities.GetRsaPublicKey(certificate.GetRSAPublicKey());
                        //certificate.Dispose(); ToDo: See if needed
                    }

                    RsaPrivateCrtKeyParameters key;

                    if (KeyParameter.GetType() == typeof(Org.BouncyCastle.Crypto.Parameters.RsaPrivateCrtKeyParameters))
                    {
                        if (!((RsaPrivateCrtKeyParameters)KeyParameter).IsPrivate)
                            throw new Exception("File doesn't contain RSA private key.");
                        key = (RsaPrivateCrtKeyParameters)KeyParameter;
                    }
                    else if (KeyParameter.GetType() == typeof(Org.BouncyCastle.Crypto.AsymmetricCipherKeyPair))
                    {
                        if (!((AsymmetricCipherKeyPair)KeyParameter).Private.IsPrivate)
                            throw new Exception("File doesn't contain RSA private key.");
                        key = (RsaPrivateCrtKeyParameters)((AsymmetricCipherKeyPair)KeyParameter).Private;
                    }
                    else if (KeyParameter.GetType() == typeof(Org.BouncyCastle.Crypto.Parameters.RsaKeyParameters))
                    {
                        if (!((RsaKeyParameters)KeyParameter).IsPrivate)
                            throw new Exception("File doesn't contain RSA private key.");
                        key = (RsaPrivateCrtKeyParameters)KeyParameter;
                    }
                    else
                        throw new Exception("File doesn't contain RSA private key.");
                    //? Org.BouncyCastle.Crypto.AsymmetricKeyParameter
                    //? Org.BouncyCastle.Crypto.Parameters.RsaBlindingParameters
                    //? Org.BouncyCastle.Crypto.Parameters.RsaKeyGenerationParameters
                    //MessageBox.Show(KeyParameter.GetType().ToString());

                    key_len = key.Modulus.BitLength;
                    modulus = key.Modulus.ToByteArray();
                    d = key.Exponent.ToByteArray();
                    p = key.P.ToByteArray();
                    q = key.Q.ToByteArray();
                    pq = key.QInv.ToByteArray();
                    dp = key.DP.ToByteArray();
                    dq = key.DQ.ToByteArray();
                    pub_exp = key.PublicExponent.ToByteArray();

                    key_idx = cbRSAKeyLength.FindStringExact(key_len.ToString());
                    if (key_idx < 0)
                    {
                        MessageBox.Show("Unsupported key length of " + key_len + "bytes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    cbRSAKeyLength.SelectedIndex = key_idx;

                    // Remove leading zeros:
                    while (modulus[0] == 0 && modulus.Length > key_len / 8)
                    {
                        modulus = modulus.Skip(1).ToArray();
                    }
                    while (d[0] == 0 && modulus.Length > key_len / 8)
                    {
                        d = d.Skip(1).ToArray();
                    }
                    while (p[0] == 0 && modulus.Length > key_len / 16)
                    {
                        p = p.Skip(1).ToArray();
                    }
                    while (q[0] == 0 && modulus.Length > key_len / 16)
                    {
                        q = q.Skip(1).ToArray();
                    }
                    while (pq[0] == 0 && modulus.Length > key_len / 16)
                    {
                        pq = pq.Skip(1).ToArray();
                    }
                    while (dp[0] == 0 && modulus.Length > key_len / 16)
                    {
                        dp = dp.Skip(1).ToArray();
                    }
                    while (dq[0] == 0 && modulus.Length > key_len / 16)
                    {
                        dq = dq.Skip(1).ToArray();
                    }
                    while (pub_exp[0] == 0 && pub_exp.Length > 0)
                    {
                        pub_exp = pub_exp.Skip(1).ToArray();
                    }
                    tbRSAModulus.Text = BitConverter.ToString(modulus).Replace("-", "");
                    tbRSAPrivP.Text = BitConverter.ToString(p).Replace("-", "");
                    tbRSAPrivQ.Text = BitConverter.ToString(q).Replace("-", "");
                    tbRSAPrivPQ.Text = BitConverter.ToString(pq).Replace("-", "");
                    tbRSAPrivDP1.Text = BitConverter.ToString(dp).Replace("-", "");
                    tbRSAPrivDQ1.Text = BitConverter.ToString(dq).Replace("-", "");
                    tbRSAPrivExp.Text = BitConverter.ToString(d).Replace("-", "");
                    tbRSAPubExp.Text = BitConverter.ToString(pub_exp).Replace("-", "");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSaveRSAPub_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog;

            try
            {
                dialog = new SaveFileDialog();
                dialog.Filter = "Signature binary files (*.pem)|*.pem|All files (*.*)|*.*";
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var key = new RsaKeyParameters(false, new BigInteger(1, Hex.Decode(tbRSAModulus.Text)), 
                        new BigInteger(1, Hex.Decode(tbRSAPubExp.Text)));

                    var textWriter = new StreamWriter(dialog.FileName);
                    var pemWr = new PemWriter(textWriter);
                    pemWr.WriteObject(key);
                    pemWr.Writer.Flush();
                    textWriter.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMkRSAKeyOnCard_Click(object sender, EventArgs e)
        {
            DL_STATUS status = DL_STATUS.UFR_OK;
            byte key_index = Convert.ToByte(cbRSAKeyIndex.Text);
            byte key_type;
            UInt16 key_size_bits = Convert.ToUInt16(cbRSAKeyLength.Text);

            try
            {
                if (!uFR_Opened)
                    throw new Exception(uFR_NotOpenedMessage);
#if USING_PIN
                if (!mSOPinLoggedIn)
                    throw new Exception("To generate key pairs, login as SO (enter SO PIN and click \"SO Login\" on \"PIN Codes\" tab)");
#endif
                Cursor.Current = Cursors.WaitCursor;

                if (rbCRT.Checked)
                {
                    key_type = (byte)JCDL_KEY_TYPES.TYPE_RSA_CRT_PRIVATE;
                }
                else
                {
                    key_type = (byte)JCDL_KEY_TYPES.TYPE_RSA_PRIVATE;
                }

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
                status = uFCoder.JCAppLogin(true, tbSOPin.Text);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));
#endif
                status = uFCoder.JCAppGenerateKeyPair(key_type, key_index, 0, key_size_bits, null, 0);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                MessageBox.Show("The key has been successfully stored.", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (((int)status & 0xFFFFC0) == 0x0A63C0)
                {
                    btnSOLogout.Enabled = false;
                    mSOPinLoggedIn = false;
                    tbSOPin.Text = "";
                    MessageBox.Show("Wrong SO PIN code. Tries remaining: " + ((int)status & 0x3F),
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (uFR_Selected)
                {
                    uFCoder.s_block_deselect(100);
                    uFR_Selected = false;
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnGetRSAPublic_Click(object sender, EventArgs e)
        {
            DL_STATUS status = DL_STATUS.UFR_OK;
            byte key_index = Convert.ToByte(cbRSAKeyIndex.Text);
            byte[] modulus, exponent;

            try
            {
                if (!uFR_Opened)
                    throw new Exception(uFR_NotOpenedMessage);
                Cursor.Current = Cursors.WaitCursor;

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

                status = uFCoder.JCAppGetRsaPublicKey(key_index, out modulus, out exponent);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                tbRSAModulus.Text = "";
                tbRSAPrivExp.Text = "";
                tbRSAPrivP.Text = "";
                tbRSAPrivQ.Text = "";
                tbRSAPrivPQ.Text = "";
                tbRSAPrivDP1.Text = "";
                tbRSAPrivDQ1.Text = "";
                tbRSAPubExp.Text = "";

                string s = (modulus.Length * 8).ToString();
                int i = cbRSAKeyLength.Items.IndexOf(s);
                if (i > -1)
                {
                    cbRSAKeyLength.SelectedItem = s;
                    tbRSAModulus.Text = Hex.ToHexString(modulus);
                    tbRSAPubExp.Text = Hex.ToHexString(exponent);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (uFR_Selected)
                {
                    uFCoder.s_block_deselect(100);
                    uFR_Selected = false;
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnStoreRSAPriv_Click(object sender, EventArgs e)
        {
            DL_STATUS status = DL_STATUS.UFR_OK;
            byte key_index = Convert.ToByte(cbRSAKeyIndex.Text);
            byte key_type;
            UInt16 key_size_bits = Convert.ToUInt16(cbRSAKeyLength.Text);
            int key_size_bytes, component_size_bytes;
            int key_offset = 0;
            byte[] key;

            try
            {
                if (!uFR_Opened)
                    throw new Exception(uFR_NotOpenedMessage);
#if USING_PIN
                if (!mSOPinLoggedIn)
                    throw new Exception("To store key pairs, login as SO (enter SO PIN and click \"SO Login\" on \"PIN Codes\" tab)");
#endif
                Cursor.Current = Cursors.WaitCursor;

                if (rbCRT.Checked)
                {
                    key_type = (byte)JCDL_KEY_TYPES.TYPE_RSA_CRT_PRIVATE;
                    component_size_bytes = key_size_bits / 16;
                    key_size_bytes = component_size_bytes * 5;
                    key = new byte[key_size_bytes];

                    if (tbRSAPrivP.Text.Length / 2 != component_size_bytes)
                        throw new Exception("Wrong size of the RSA private key parameter P");
                    Array.Copy(Hex.Decode(tbRSAPrivP.Text), 0, key, key_offset, component_size_bytes);
                    key_offset += component_size_bytes;

                    if (tbRSAPrivQ.Text.Length / 2 != component_size_bytes)
                        throw new Exception("Wrong size of the RSA private key parameter Q");
                    Array.Copy(Hex.Decode(tbRSAPrivQ.Text), 0, key, key_offset, component_size_bytes);
                    key_offset += component_size_bytes;

                    if (tbRSAPrivPQ.Text.Length / 2 != component_size_bytes)
                        throw new Exception("Wrong size of the RSA private key parameter PQ");
                    Array.Copy(Hex.Decode(tbRSAPrivPQ.Text), 0, key, key_offset, component_size_bytes);
                    key_offset += component_size_bytes;

                    if (tbRSAPrivDP1.Text.Length / 2 != component_size_bytes)
                        throw new Exception("Wrong size of the RSA private key parameter DP1");
                    Array.Copy(Hex.Decode(tbRSAPrivDP1.Text), 0, key, key_offset, component_size_bytes);
                    key_offset += component_size_bytes;

                    if (tbRSAPrivDQ1.Text.Length / 2 != component_size_bytes)
                        throw new Exception("Wrong size of the RSA private key parameter DQ1");
                    Array.Copy(Hex.Decode(tbRSAPrivDQ1.Text), 0, key, key_offset, component_size_bytes);
                }
                else
                {
                    key_type = (byte)JCDL_KEY_TYPES.TYPE_RSA_PRIVATE;
                    component_size_bytes = key_size_bits / 8;
                    key_size_bytes = component_size_bytes * 2;
                    key = new byte[key_size_bytes];

                    if (tbRSAModulus.Text.Length / 2 != component_size_bytes)
                        throw new Exception("Wrong size of the RSA private key parameter N");
                    Array.Copy(Hex.Decode(tbRSAModulus.Text), 0, key, key_offset, component_size_bytes);
                    key_offset += component_size_bytes;

                    if (tbRSAPrivExp.Text.Length / 2 != component_size_bytes)
                        throw new Exception("Wrong size of the RSA private key parameter D");
                    Array.Copy(Hex.Decode(tbRSAPrivExp.Text), 0, key, key_offset, component_size_bytes);
                }

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
                status = uFCoder.JCAppLogin(true, tbSOPin.Text);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));
#endif
                status = uFCoder.JCAppPutPrivateKey(key_type, key_index, key, key_size_bits, null, 0);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                MessageBox.Show("The key has been successfully stored.", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (((int)status & 0xFFFFC0) == 0x0A63C0)
                {
                    btnSOLogout.Enabled = false;
                    mSOPinLoggedIn = false;
                    tbSOPin.Text = "";
                    MessageBox.Show("Wrong SO PIN code. Tries remaining: " + ((int)status & 0x3F),
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (uFR_Selected)
                {
                    uFCoder.s_block_deselect(100);
                    uFR_Selected = false;
                }
                Cursor.Current = Cursors.Default;
            }
        }

        //======================================================================================================================
        // EC Keys page:
        //======================================================================================================================
        void populateECDomainParameters(string curve_name)
        {
            byte[] barr1;
            Int32 key_bytes_len;
            mECCurve = SecNamedCurves.GetByName(curve_name);

            try
            {
                key_bytes_len = (mECCurve.Curve.FieldSize + 7) / 8;

                if (mECCurve.Curve.GetType() == typeof(FpCurve))
                {
                    rbECFieldPrime.Checked = true;
                    barr1 = mECCurve.Curve.Field.Characteristic.ToByteArray();
                    while (barr1[0] == 0 && barr1.Length > key_bytes_len)
                    {
                        barr1 = barr1.Skip(1).ToArray();
                    }
                    if (key_bytes_len > barr1.Length)
                        barr1 = zeroPadArray(barr1, 0, key_bytes_len - barr1.Length);

                    /*/ debug:
                    tbECParamPrime.Text = mECCurve.Curve.Field.Characteristic.ToString();//*/
                    tbECParamPrime.Text = BitConverter.ToString(barr1).Replace("-", "");
                }
                else if (mECCurve.Curve.GetType() == typeof(F2mCurve))
                {
                    rbECFieldBinary.Checked = true;
                    populateECReductionPolynomial(!((F2mCurve)mECCurve.Curve).IsTrinomial(),
                            (ushort)mECCurve.Curve.FieldSize,
                            (ushort)((F2mCurve)mECCurve.Curve).K3,
                            (ushort)((F2mCurve)mECCurve.Curve).K2,
                            (ushort)((F2mCurve)mECCurve.Curve).K1);
                }

                barr1 = mECCurve.Curve.A.ToBigInteger().ToByteArray();
                while (barr1[0] == 0 && barr1.Length > key_bytes_len)
                {
                    barr1 = barr1.Skip(1).ToArray();
                }
                if (key_bytes_len > barr1.Length)
                    barr1 = zeroPadArray(barr1, 0, key_bytes_len - barr1.Length);
                tbECParamA.Text = BitConverter.ToString(barr1).Replace("-", "");

                barr1 = mECCurve.Curve.B.ToBigInteger().ToByteArray();
                while (barr1[0] == 0 && barr1.Length > key_bytes_len)
                {
                    barr1 = barr1.Skip(1).ToArray();
                }
                if (key_bytes_len > barr1.Length)
                    barr1 = zeroPadArray(barr1, 0, key_bytes_len - barr1.Length);
                tbECParamB.Text = BitConverter.ToString(barr1).Replace("-", "");

                barr1 = mECCurve.G.XCoord.ToBigInteger().ToByteArray();
                if (key_bytes_len > barr1.Length)
                    barr1 = zeroPadArray(barr1, 0, key_bytes_len - barr1.Length);
                while (barr1[0] == 0 && barr1.Length > key_bytes_len)
                {
                    barr1 = barr1.Skip(1).ToArray();
                }
                tbECParamG.Text = "04" + BitConverter.ToString(barr1).Replace("-", "");

                barr1 = mECCurve.G.YCoord.ToBigInteger().ToByteArray();
                while (barr1[0] == 0 && barr1.Length > key_bytes_len)
                {
                    barr1 = barr1.Skip(1).ToArray();
                }
                if (key_bytes_len > barr1.Length)
                    barr1 = zeroPadArray(barr1, 0, key_bytes_len - barr1.Length);
                tbECParamG.Text += BitConverter.ToString(barr1).Replace("-", "");

                barr1 = mECCurve.N.ToByteArray();
                while (barr1[0] == 0 && barr1.Length > key_bytes_len)
                {
                    barr1 = barr1.Skip(1).ToArray();
                }
                if (key_bytes_len > barr1.Length)
                    barr1 = zeroPadArray(barr1, 0, key_bytes_len - barr1.Length);
                tbECParamR.Text = BitConverter.ToString(barr1).Replace("-", "");

                tbECParamK.Text = mECCurve.H.ToString(); // mECCurve.Curve.Cofactor.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clearECReductionPolynomial()
        {
            rtbECReductionPolynomial.Clear();
            //richTextBox1.SelectionColor = Color.Black;
            rtbECReductionPolynomial.SelectionFont = new Font("Times New Roman", 10, FontStyle.Italic);
            rtbECReductionPolynomial.SelectedText = " f(x) = ";
        }

        private void populateECReductionPolynomial(Boolean isPentanomial, UInt16 m, UInt16 e1, UInt16 e2, UInt16 e3)
        {
            tbECParamE1.Enabled = true;

            rtbECReductionPolynomial.Clear();
            //richTextBox1.SelectionColor = Color.Black;
            rtbECReductionPolynomial.SelectionFont = new Font("Times New Roman", 10, FontStyle.Italic);
            rtbECReductionPolynomial.SelectedText = " f(x) = x";
            // superscripted text...
            rtbECReductionPolynomial.SelectionFont = new Font("Times New Roman", 7, FontStyle.Italic);
            rtbECReductionPolynomial.SelectionCharOffset = 8;
            rtbECReductionPolynomial.SelectedText = m.ToString();
            // normal text...
            rtbECReductionPolynomial.SelectionFont = new Font("Times New Roman", 10, FontStyle.Italic);
            rtbECReductionPolynomial.SelectionCharOffset = 0;
            rtbECReductionPolynomial.SelectedText = " + x";

            if (isPentanomial)
            {
                lbECParamE1.Text = "e1:";
                tbECParamE1.Text = e1.ToString();
                lbECParamE2.Enabled = true;
                lbECParamE3.Enabled = true;
                tbECParamE2.Enabled = true;
                tbECParamE3.Enabled = true;
                tbECParamE2.Text = e2.ToString();
                tbECParamE3.Text = e3.ToString();

                // superscripted text...
                rtbECReductionPolynomial.SelectionFont = new Font("Times New Roman", 7, FontStyle.Italic);
                rtbECReductionPolynomial.SelectionCharOffset = 8;
                rtbECReductionPolynomial.SelectedText = e1.ToString();
                // normal text...
                rtbECReductionPolynomial.SelectionFont = new Font("Times New Roman", 10, FontStyle.Italic);
                rtbECReductionPolynomial.SelectionCharOffset = 0;
                rtbECReductionPolynomial.SelectedText = " + x";
                // superscripted text...
                rtbECReductionPolynomial.SelectionFont = new Font("Times New Roman", 7, FontStyle.Italic);
                rtbECReductionPolynomial.SelectionCharOffset = 8;
                rtbECReductionPolynomial.SelectedText = e2.ToString();
                // normal text...
                rtbECReductionPolynomial.SelectionFont = new Font("Times New Roman", 10, FontStyle.Italic);
                rtbECReductionPolynomial.SelectionCharOffset = 0;
                rtbECReductionPolynomial.SelectedText = " + x";
                // superscripted text...
                rtbECReductionPolynomial.SelectionFont = new Font("Times New Roman", 7, FontStyle.Italic);
                rtbECReductionPolynomial.SelectionCharOffset = 8;
                rtbECReductionPolynomial.SelectedText = e3.ToString();
            }
            else
            {
                lbECParamE1.Text = "e:";
                tbECParamE1.Text = e3.ToString();
                tbECParamE2.Clear();
                tbECParamE3.Clear();
                tbECParamE2.Enabled = false;
                tbECParamE3.Enabled = false;
                lbECParamE2.Enabled = false;
                lbECParamE3.Enabled = false;

                // superscripted text...
                rtbECReductionPolynomial.SelectionFont = new Font("Times New Roman", 7, FontStyle.Italic);
                rtbECReductionPolynomial.SelectionCharOffset = 8;
                rtbECReductionPolynomial.SelectedText = e3.ToString();
            }
            // normal text...
            rtbECReductionPolynomial.SelectionFont = new Font("Times New Roman", 10, FontStyle.Italic);
            rtbECReductionPolynomial.SelectionCharOffset = 0;
            rtbECReductionPolynomial.SelectedText = " + 1";
        }

        private void rbCRT_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCRT.Checked)
            {
                lbRSAPrivP.Enabled = true;
                lbRSAPrivQ.Enabled = true;
                lbRSAPrivPQ.Enabled = true;
                lbRSAPrivDP1.Enabled = true;
                lbRSAPrivDQ1.Enabled = true;
                tbRSAPrivP.Enabled = true;
                tbRSAPrivQ.Enabled = true;
                tbRSAPrivPQ.Enabled = true;
                tbRSAPrivDP1.Enabled = true;
                tbRSAPrivDQ1.Enabled = true;

                lbRSAPrivExp.Enabled = false;
                tbRSAPrivExp.Enabled = false;
            }
            else
            {
                lbRSAPrivP.Enabled = false;
                lbRSAPrivQ.Enabled = false;
                lbRSAPrivPQ.Enabled = false;
                lbRSAPrivDP1.Enabled = false;
                lbRSAPrivDQ1.Enabled = false;
                tbRSAPrivP.Enabled = false;
                tbRSAPrivQ.Enabled = false;
                tbRSAPrivPQ.Enabled = false;
                tbRSAPrivDP1.Enabled = false;
                tbRSAPrivDQ1.Enabled = false;

                lbRSAPrivExp.Enabled = true;
                tbRSAPrivExp.Enabled = true;
            }
        }

        private void rbECFieldPrime_CheckedChanged(object sender, EventArgs e)
        {
            if (rbECFieldPrime.Checked)
            {
                tbECParamPrime.Enabled = true;
                clearECReductionPolynomial();
                tbECParamE1.Clear();
                tbECParamE2.Clear();
                tbECParamE3.Clear();
                gbECReductionPolynomial.Enabled = false;
            }
        }

        private void rbECFieldBinary_CheckedChanged(object sender, EventArgs e)
        {
            if (rbECFieldBinary.Checked)
            {
                tbECParamPrime.Enabled = false;
                tbECParamPrime.Clear();
                gbECReductionPolynomial.Enabled = true;
            }
        }

        private void btnECImportP12_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "PKCS#12 files (*.p12;*.pfx)|*.p12;*.pfx|All files (*.*)|*.*";
            //dialog.InitialDirectory = @"C:\";
            dialog.Title = "Please select the cert file";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                frmPassword dlgPasswd = new frmPassword();
                if (dlgPasswd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Load your certificate from p12 file
                        X509Certificate2 certificate = new X509Certificate2(dialog.FileName, dlgPasswd.password, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);
                        //RSACryptoServiceProvider pub_key = (RSACryptoServiceProvider)certificate.PublicKey.Key;
                        PublicKey pub_key = certificate.PublicKey;

                        //if (!certificate.HasPrivateKey)
                        //{
                        //    throw new Exception("Certificate does not contain private key.");
                        //}

                        if (pub_key.EncodedKeyValue.Oid.FriendlyName.Equals("ECC")
                            || pub_key.EncodedKeyValue.Oid.FriendlyName.Equals("ECDSA"))
                        {
                            var parser = new X509CertificateParser();
                            // Load your certificate
                            var bouncyCertificate = parser.ReadCertificate(certificate.RawData);
                            //certificate.Dispose(); ToDo: See if needed

                            string curve_name = SecNamedCurves.GetName(
                                (DerObjectIdentifier)DerObjectIdentifier.FromByteArray(
                                    bouncyCertificate.CertificateStructure.SubjectPublicKeyInfo.AlgorithmID.Parameters.GetDerEncoded()
                                    )
                                );

                            if (cbECName.Items.Contains(curve_name))
                            {
                                int index = cbECName.FindString(curve_name);
                                cbECName.SelectedIndex = index;
                            }
                            else
                                throw new Exception("Unknown ECC Parameters in certificate.");

                            tbECPubKey.Text = BitConverter.ToString(
                                bouncyCertificate.CertificateStructure.SubjectPublicKeyInfo.PublicKeyData.GetBytes()
                                ).Replace("-", "");
                        }
                        else
                        {
                            MessageBox.Show("File doesn't contain EC public key.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnMkECKey_Click(object sender, EventArgs e)
        {
            byte[] barr1, barr2;
            Int32 key_bytes_len;

            Cursor.Current = Cursors.WaitCursor;

            ECKeyPairGenerator gen = new ECKeyPairGenerator("ECDSA");

            string CurveName = cbECName.Text;
            X9ECParameters curve = SecNamedCurves.GetByName(CurveName);
            ECDomainParameters curveSpec = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H, curve.GetSeed());
            ECKeyGenerationParameters keyGenParam = new ECKeyGenerationParameters(curveSpec, new SecureRandom());
            gen.Init(keyGenParam);
            //Generation of Key Pair
            AsymmetricCipherKeyPair KeyPair = gen.GenerateKeyPair();

            key_bytes_len = (curve.Curve.FieldSize + 7) / 8;

            mPrivateKey = (ECPrivateKeyParameters)KeyPair.Private;
            barr1 = mPrivateKey.D.ToByteArray();
            while (barr1[0] == 0 && barr1.Length > key_bytes_len)
            {
                barr1 = barr1.Skip(1).ToArray();
            }
            if (key_bytes_len > barr1.Length)
                barr1 = zeroPadArray(barr1, 0, key_bytes_len - barr1.Length);
            tbECPrivKey.Text = BitConverter.ToString(barr1).Replace("-", "");

            ECPublicKeyParameters publicKeyParam = (ECPublicKeyParameters)KeyPair.Public;
            barr1 = publicKeyParam.Q.XCoord.ToBigInteger().ToByteArray();
            while (barr1[0] == 0 && barr1.Length > key_bytes_len)
            {
                barr1 = barr1.Skip(1).ToArray();
            }
            if (key_bytes_len > barr1.Length)
                barr1 = zeroPadArray(barr1, 0, key_bytes_len - barr1.Length);
            barr2 = publicKeyParam.Q.YCoord.ToBigInteger().ToByteArray();
            while (barr2[0] == 0 && barr2.Length > key_bytes_len)
            {
                barr2 = barr2.Skip(1).ToArray();
            }
            if (key_bytes_len > barr2.Length)
                barr2 = zeroPadArray(barr2, 0, key_bytes_len - barr2.Length);
            tbECPubKey.Text = "04" + BitConverter.ToString(barr1).Replace("-", "") + BitConverter.ToString(barr2).Replace("-", "");

            Cursor.Current = Cursors.Default;
        }

        private void btnBackupECPriv_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog;

            try
            {
                if (tbECPrivKey.Text.Trim().Equals(""))
                    throw new Exception("Private key can't have a zero length.");

                dialog = new SaveFileDialog();
                dialog.Filter = "Signature binary files (*.pem)|*.pem|All files (*.*)|*.*";
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    X9ECParameters curve = SecNamedCurves.GetByName(cbECName.Text);
                    ECDomainParameters curveSpec = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H, curve.GetSeed());
                    var key = new ECPrivateKeyParameters("ECDSA", new BigInteger(1, Hex.Decode(tbECPrivKey.Text)), curveSpec);

                    var textWriter = new StreamWriter(dialog.FileName);
                    var pemWr = new PemWriter(textWriter);
                    pemWr.WriteObject(key);
                    pemWr.Writer.Flush();
                    textWriter.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnRestoreECPriv_Click(object sender, EventArgs e)
        {
            byte[] barr;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "PEM files (*.pem;*.crt;*.cer)|*.pem;*.crt;*.cer|All files (*.*)|*.*";
            //dialog.InitialDirectory = @"C:\";
            dialog.Title = "Please select the PEM file";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //ToDo: Find beter strategy to open Org.BouncyCastle.X509.X509Certificate

                    // Load your certificate from PEM file
                    TextReader fileStream = System.IO.File.OpenText(dialog.FileName);
                    PemReader pemReader = new Org.BouncyCastle.OpenSsl.PemReader(fileStream);
                    object PemObj = pemReader.ReadObject();

                    if (PemObj.GetType() == typeof(Org.BouncyCastle.Crypto.AsymmetricCipherKeyPair))
                    {
                        ECPrivateKeyParameters key = (ECPrivateKeyParameters)((AsymmetricCipherKeyPair)PemObj).Private;
                        if (!key.IsPrivate)
                            throw new Exception("File doesn't contain EC public key.");

                        int idx = 0;
                        foreach (string s in cbECName.Items)
                        {
                            var EC = SecNamedCurves.GetByName(s);
                            if (EC.Curve.Equals(key.Parameters.Curve))
                                break;
                            idx++;
                        }
                        if ((idx + 1) >= cbECName.Items.Count)
                            throw new Exception("Unknown ECC Parameters in pem file.");

                        cbECName.SelectedIndex = idx;
                       
                        int key_bytes_len = (key.Parameters.Curve.FieldSize + 7) / 8;
                        barr = key.D.ToByteArray();
                        while (barr[0] == 0 && barr.Length > key_bytes_len)
                        {
                            barr = barr.Skip(1).ToArray();
                        }
                        if (key_bytes_len > barr.Length)
                            barr = zeroPadArray(barr, 0, key_bytes_len - barr.Length);
                        tbECPrivKey.Text = BitConverter.ToString(barr).Replace("-", "");

                        tbECPubKey.Text = "";
                    }
/*
                    else if (PemObj.GetType() == typeof(Org.BouncyCastle.X509.X509Certificate))
                    {
                        var parser = new X509CertificateParser();
                        // Load your certificate
                        X509Certificate2 certificate = new X509Certificate2(dialog.FileName);
                        var bouncyCertificate = parser.ReadCertificate(certificate.RawData);
                        //certificate.Dispose(); ToDo: See if needed

                        string curve_name = SecNamedCurves.GetName(
                            (DerObjectIdentifier)DerObjectIdentifier.FromByteArray(
                                bouncyCertificate.CertificateStructure.SubjectPublicKeyInfo.AlgorithmID.Parameters.GetDerEncoded()
                                )
                            );

                        if (cbECName.Items.Contains(curve_name))
                        {
                            int index = cbECName.FindString(curve_name);
                            cbECName.SelectedIndex = index;
                        }
                        else
                            throw new Exception("Unknown ECC Parameters in certificate.");

                        tbECPrivKey.Text = ???;
                    }
*/
                    else
                        throw new Exception("File doesn't contain EC public key.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSaveECPub_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog;

            try
            {
                dialog = new SaveFileDialog();
                dialog.Filter = "Signature binary files (*.pem)|*.pem|All files (*.*)|*.*";
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    X9ECParameters curve = SecNamedCurves.GetByName(cbECName.Text);
                    ECDomainParameters curveSpec = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H, curve.GetSeed());
                    var key = new ECPublicKeyParameters("ECDSA", curve.Curve.DecodePoint(Hex.Decode(tbECPubKey.Text)), curveSpec);

                    var textWriter = new StreamWriter(dialog.FileName);
                    var pemWr = new PemWriter(textWriter);
                    pemWr.WriteObject(key);
                    pemWr.Writer.Flush();
                    textWriter.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private byte GetECKeyDesignator(string ec_curve_name)
        {
            byte ret_val = 0;
            if (ec_curve_name.Substring(ec_curve_name.Length - 2).Equals("r1"))
                ret_val = 1;
            else if (ec_curve_name.Substring(ec_curve_name.Length - 2).Equals("r2"))
                ret_val = 2;
            else if (!ec_curve_name.Substring(ec_curve_name.Length - 2).Equals("k1"))
                ret_val = 3;

            return ret_val;
        }

        private void btnMkECKeyOnCard_Click(object sender, EventArgs e)
        {
            DL_STATUS status = DL_STATUS.UFR_OK;
            byte key_index = Convert.ToByte(cbECKeyIndex.Text);
            byte key_type;
            UInt16 key_size_bits = Convert.ToUInt16(cbECKeyLength.Text);
            UInt16 component_size_bytes = (UInt16) ((key_size_bits + 7) / 8);
            bool isCurveTrinomial;
            byte[] param = null;
            UInt16 param_size = 1; // for r_oversized indicator or (byte)!isCurveTrinomial at index 0
            UInt16 param_offset = 1;
            UInt16 temp;
            byte r_oversized = 0;

            try
            {
                if (!uFR_Opened)
                    throw new Exception(uFR_NotOpenedMessage);
#if USING_PIN
                if (!mSOPinLoggedIn)
                    throw new Exception("To generate key pairs, login as SO (enter SO PIN and click \"SO Login\" on \"PIN Codes\" tab)");
#endif

                if (rbECFieldPrime.Checked)
                {
                    key_type = (byte)JCDL_KEY_TYPES.TYPE_EC_FP_PRIVATE;
                    param_size += (UInt16)(component_size_bytes * 6 + 3);

                    // Code is logically deliberately dislocated here:
                    if (tbECParamR.Text.Length / 2 != component_size_bytes)
                    {
                        if (tbECParamR.Text.Length / 2 != component_size_bytes + 1)
                        {
                            throw new Exception("Wrong size of the EC curve parameter R");
                        }
                        else
                        {
                            r_oversized = 1;
                            ++param_size;
                        }
                    }
                    param = new byte[param_size];
                    param[0] = r_oversized;

                    if (tbECParamPrime.Text.Length / 2 != component_size_bytes)
                        throw new Exception("Wrong size of the EC curve parameter p");
                    Array.Copy(Hex.Decode(tbECParamPrime.Text), 0, param, param_offset, component_size_bytes);
                    param_offset += component_size_bytes;

                    if (tbECParamA.Text.Length / 2 != component_size_bytes)
                        throw new Exception("Wrong size of the EC curve parameter a");
                    Array.Copy(Hex.Decode(tbECParamA.Text), 0, param, param_offset, component_size_bytes);
                    param_offset += component_size_bytes;

                    if (tbECParamB.Text.Length / 2 != component_size_bytes)
                        throw new Exception("Wrong size of the EC curve parameter b");
                    Array.Copy(Hex.Decode(tbECParamB.Text), 0, param, param_offset, component_size_bytes);
                    param_offset += component_size_bytes;

                    if (tbECParamG.Text.Length / 2 != component_size_bytes * 2 + 1)
                        throw new Exception("Wrong size of the EC curve parameter G(uc)");
                    Array.Copy(Hex.Decode(tbECParamG.Text), 0, param, param_offset, component_size_bytes * 2 + 1);
                    param_offset += (UInt16)(component_size_bytes * 2 + 1);

                    Array.Copy(Hex.Decode(tbECParamR.Text), 0, param, param_offset, component_size_bytes + r_oversized);
                    param_offset += (UInt16)(component_size_bytes + r_oversized);

                    temp = Convert.ToUInt16(tbECParamK.Text);
                    param[param_offset++] = (byte)(temp >> 8);
                    param[param_offset] = (byte)temp;
                }
                else
                {
                    key_type = (byte)JCDL_KEY_TYPES.TYPE_EC_F2M_PRIVATE;
                    param_size += (UInt16)(component_size_bytes * 5 + 5);

                    isCurveTrinomial = ((F2mCurve)mECCurve.Curve).IsTrinomial();
                    if (!isCurveTrinomial)
                    {
                        param_size += 4;
                    }
                    param = new byte[param_size];
                    param[0] = (byte)(!isCurveTrinomial ? 1 : 0);

                    if (tbECParamA.Text.Length / 2 != component_size_bytes)
                        throw new Exception("Wrong size of the EC curve parameter a");
                    Array.Copy(Hex.Decode(tbECParamA.Text), 0, param, param_offset, component_size_bytes);
                    param_offset += component_size_bytes;

                    if (tbECParamB.Text.Length / 2 != component_size_bytes)
                        throw new Exception("Wrong size of the EC curve parameter b");
                    Array.Copy(Hex.Decode(tbECParamB.Text), 0, param, param_offset, component_size_bytes);
                    param_offset += component_size_bytes;

                    if (tbECParamG.Text.Length / 2 != component_size_bytes * 2 + 1)
                        throw new Exception("Wrong size of the EC curve parameter G(uc)");
                    Array.Copy(Hex.Decode(tbECParamG.Text), 0, param, param_offset, component_size_bytes * 2 + 1);
                    param_offset += (UInt16)(component_size_bytes * 2 + 1);

                    if (tbECParamR.Text.Length / 2 != component_size_bytes)
                        throw new Exception("Wrong size of the EC curve parameter R");
                    Array.Copy(Hex.Decode(tbECParamR.Text), 0, param, param_offset, component_size_bytes);
                    param_offset += component_size_bytes;

                    temp = Convert.ToUInt16(tbECParamE1.Text);
                    param[param_offset++] = (byte)(temp >> 8);
                    param[param_offset++] = (byte)temp;

                    if (!isCurveTrinomial)
                    {
                        temp = Convert.ToUInt16(tbECParamE2.Text);
                        param[param_offset++] = (byte)(temp >> 8);
                        param[param_offset++] = (byte)temp;

                        temp = Convert.ToUInt16(tbECParamE3.Text);
                        param[param_offset++] = (byte)(temp >> 8);
                        param[param_offset++] = (byte)temp;
                    }

                    temp = Convert.ToUInt16(tbECParamK.Text);
                    param[param_offset++] = (byte)(temp >> 8);
                    param[param_offset] = (byte)temp;
                }

                Cursor.Current = Cursors.WaitCursor;

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
                status = uFCoder.JCAppLogin(true, tbSOPin.Text);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));
#endif

                status = uFCoder.JCAppGenerateKeyPair(key_type, key_index, GetECKeyDesignator(cbECName.Text), key_size_bits, param, param_size);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                MessageBox.Show("The key has been successfully stored.", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (((int)status & 0xFFFFC0) == 0x0A63C0)
                {
                    btnSOLogout.Enabled = false;
                    mSOPinLoggedIn = false;
                    tbSOPin.Text = "";
                    MessageBox.Show("Wrong SO PIN code. Tries remaining: " + ((int)status & 0x3F), 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (uFR_Selected)
                {
                    uFCoder.s_block_deselect(100);
                    uFR_Selected = false;
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnStoreECPriv_Click(object sender, EventArgs e)
        {
            DL_STATUS status = DL_STATUS.UFR_OK;
            byte key_index = Convert.ToByte(cbECKeyIndex.Text);
            byte key_type;
            byte[] key_param = new byte[1];
            byte r_oversized = 0;
            byte key_param_len;
            UInt16 key_size_bits = Convert.ToUInt16(cbECKeyLength.Text);
            UInt16 temp;
            int component_size_bytes = (key_size_bits + 7) / 8;
            int key_size_bytes;
            int key_offset = 0;
            byte[] key;
            bool isCurveTrinomial;

            try
            {
                if (!uFR_Opened)
                    throw new Exception(uFR_NotOpenedMessage);
#if USING_PIN
                if (!mSOPinLoggedIn)
                    throw new Exception("To store key pairs, login as SO (enter SO PIN and click \"SO Login\" on \"PIN Codes\" tab)");
#endif
                Cursor.Current = Cursors.WaitCursor;

                key_param[0] = 0;
                key_param_len = 1;

                //X9ECParameters curve = SecNamedCurves.GetByName(cbECName.Text);

                if (rbECFieldPrime.Checked)
                {
                    key_type = (byte)JCDL_KEY_TYPES.TYPE_EC_FP_PRIVATE;
                    key_size_bytes = component_size_bytes * 7 + 3;
                    
                    // Code is logically deliberately dislocated here:
                    if (tbECParamR.Text.Length / 2 != component_size_bytes)
                    {
                        if (tbECParamR.Text.Length / 2 != component_size_bytes + 1)
                        {
                            throw new Exception("Wrong size of the EC curve parameter R");
                        }
                        else
                        {
                            key_param[0] = 1;
                            r_oversized = 1;
                            ++key_size_bytes;
                        }
                    }
                    key = new byte[key_size_bytes];

                    if (tbECParamPrime.Text.Length / 2 != component_size_bytes)
                        throw new Exception("Wrong size of the EC curve parameter p");
                    Array.Copy(Hex.Decode(tbECParamPrime.Text), 0, key, key_offset, component_size_bytes);
                    key_offset += component_size_bytes;

                    if (tbECParamA.Text.Length / 2 != component_size_bytes)
                        throw new Exception("Wrong size of the EC curve parameter a");
                    Array.Copy(Hex.Decode(tbECParamA.Text), 0, key, key_offset, component_size_bytes);
                    key_offset += component_size_bytes;

                    if (tbECParamB.Text.Length / 2 != component_size_bytes)
                        throw new Exception("Wrong size of the EC curve parameter b");
                    Array.Copy(Hex.Decode(tbECParamB.Text), 0, key, key_offset, component_size_bytes);
                    key_offset += component_size_bytes;

                    if (tbECParamG.Text.Length / 2 != component_size_bytes * 2 + 1)
                        throw new Exception("Wrong size of the EC curve parameter G(uc)");
                    Array.Copy(Hex.Decode(tbECParamG.Text), 0, key, key_offset, component_size_bytes * 2 + 1);
                    key_offset += component_size_bytes * 2 + 1;

                    Array.Copy(Hex.Decode(tbECParamR.Text), 0, key, key_offset, component_size_bytes + r_oversized);
                    key_offset += component_size_bytes + r_oversized;

                    if (tbECPrivKey.Text.Length / 2 != component_size_bytes)
                        throw new Exception("Wrong size of the EC private key (parameter S)");
                    Array.Copy(Hex.Decode(tbECPrivKey.Text), 0, key, key_offset, component_size_bytes);
                    key_offset += component_size_bytes;

                    temp = Convert.ToUInt16(tbECParamK.Text);
                    key[key_offset++] = (byte)(temp >> 8);
                    key[key_offset] = (byte)temp;
                }
                else
                {
                    key_type = (byte)JCDL_KEY_TYPES.TYPE_EC_F2M_PRIVATE;
                    key_size_bytes = component_size_bytes * 6 + 5;

                    isCurveTrinomial = ((F2mCurve)mECCurve.Curve).IsTrinomial();
                    if (!isCurveTrinomial)
                    {
                        key_param[0] = 1;
                        key_size_bytes += 4;
                    }
                    key = new byte[key_size_bytes];

                    if (tbECParamA.Text.Length / 2 != component_size_bytes)
                        throw new Exception("Wrong size of the EC curve parameter a");
                    Array.Copy(Hex.Decode(tbECParamA.Text), 0, key, key_offset, component_size_bytes);
                    key_offset += component_size_bytes;

                    if (tbECParamB.Text.Length / 2 != component_size_bytes)
                        throw new Exception("Wrong size of the EC curve parameter b");
                    Array.Copy(Hex.Decode(tbECParamB.Text), 0, key, key_offset, component_size_bytes);
                    key_offset += component_size_bytes;

                    if (tbECParamG.Text.Length / 2 != component_size_bytes * 2 + 1)
                        throw new Exception("Wrong size of the EC curve parameter G(uc)");
                    Array.Copy(Hex.Decode(tbECParamG.Text), 0, key, key_offset, component_size_bytes * 2 + 1);
                    key_offset += component_size_bytes * 2 + 1;

                    if (tbECParamR.Text.Length / 2 != component_size_bytes)
                        throw new Exception("Wrong size of the EC curve parameter R");
                    Array.Copy(Hex.Decode(tbECParamR.Text), 0, key, key_offset, component_size_bytes);
                    key_offset += component_size_bytes;

                    if (tbECPrivKey.Text.Length / 2 != component_size_bytes)
                        throw new Exception("Wrong size of the EC private key (parameter S)");
                    Array.Copy(Hex.Decode(tbECPrivKey.Text), 0, key, key_offset, component_size_bytes);
                    key_offset += component_size_bytes;

                    temp = Convert.ToUInt16(tbECParamE1.Text);
                    key[key_offset++] = (byte)(temp >> 8);
                    key[key_offset++] = (byte)temp;

                    if (!isCurveTrinomial)
                    {
                        temp = Convert.ToUInt16(tbECParamE2.Text);
                        key[key_offset++] = (byte)(temp >> 8);
                        key[key_offset++] = (byte)temp;

                        temp = Convert.ToUInt16(tbECParamE3.Text);
                        key[key_offset++] = (byte)(temp >> 8);
                        key[key_offset++] = (byte)temp;
                    }

                    temp = Convert.ToUInt16(tbECParamK.Text);
                    key[key_offset++] = (byte)(temp >> 8);
                    key[key_offset] = (byte)temp;
                }

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
                status = uFCoder.JCAppLogin(true, tbSOPin.Text);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));
#endif

                status = uFCoder.JCAppPutPrivateKey(key_type, key_index, key, key_size_bits, key_param, key_param_len);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                MessageBox.Show("The key has been successfully stored.", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (((int)status & 0xFFFFC0) == 0x0A63C0)
                {
                    btnSOLogout.Enabled = false;
                    mSOPinLoggedIn = false;
                    tbSOPin.Text = "";
                    MessageBox.Show("Wrong SO PIN code. Tries remaining: " + ((int)status & 0x3F),
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (uFR_Selected)
                {
                    uFCoder.s_block_deselect(100);
                    uFR_Selected = false;
                }
                Cursor.Current = Cursors.Default;
            }
        }

        //======================================================================================================================
        // Signature page:
        //======================================================================================================================
        private bool m_gate_cbECKeyLength = false;
        private bool m_gate_populateECDomainParameters = false;
        private int mECNameCurrComboIndex = 0;
        private void cbECName_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool populate_ok = true;
            if (cbECName.SelectedIndex < 0)
                populate_ok = false;

            cbECName.BackColor = SystemColors.Window;

            m_gate_cbECKeyLength = true;

            if (m_gate_populateECDomainParameters)
            {
                m_gate_populateECDomainParameters = false;
                populate_ok = false;
            }

            if (mECNameCurrComboIndex != cbECName.SelectedIndex)
            {
                mECNameCurrComboIndex = cbECName.SelectedIndex;
                if (populate_ok)
                {
                    tbECPrivKey.Text = "";
                    tbECPubKey.Text = "";
                }
            }

            if (cbECName.SelectedIndex < 15)
            {
                rbECFieldPrime.Checked = true;
            }
            else
            {
                rbECFieldBinary.Checked = true;
            }
            switch (cbECName.SelectedIndex)
            {
                case 0:
                case 1:
                    cbECKeyLength.SelectedIndex = 0;
                    break;
                case 2:
                case 3:
                    cbECKeyLength.SelectedIndex = 2;
                    break;
                case 4:
                case 5:
                case 6:
                    cbECKeyLength.SelectedIndex = 4;
                    break;
                case 7:
                case 8:
                    cbECKeyLength.SelectedIndex = 6;
                    break;
                case 9:
                case 10:
                    cbECKeyLength.SelectedIndex = 8;
                    break;
                case 11:
                case 12:
                    cbECKeyLength.SelectedIndex = 11;
                    break;
                case 13:
                    cbECKeyLength.SelectedIndex = 13;
                    break;
                case 14:
                    cbECKeyLength.SelectedIndex = 15;
                    break;
                case 15:
                case 16:
                    cbECKeyLength.SelectedIndex = 1;
                    break;
                case 17:
                case 18:
                    cbECKeyLength.SelectedIndex = 3;
                    break;
                case 19:
                case 20:
                case 21:
                    cbECKeyLength.SelectedIndex = 5;
                    break;
                case 22:
                case 23:
                    cbECKeyLength.SelectedIndex = 7;
                    break;
                case 24:
                case 25:
                    cbECKeyLength.SelectedIndex = 9;
                    break;

                case 26:
                    cbECKeyLength.SelectedIndex = 10;
                    break;
                case 27:
                case 28:
                    cbECKeyLength.SelectedIndex = 12;
                    break;
                case 29:
                case 30:
                    cbECKeyLength.SelectedIndex = 14;
                    break;
                case 31:
                case 32:
                    cbECKeyLength.SelectedIndex = 16;
                    break;
            }

            if (populate_ok)
                populateECDomainParameters(cbECName.Text);

            /*/ DEBUG:
            if (tbECParamR.Text.Length != tbECParamA.Text.Length)
            {
                tbECParamR.BackColor = Color.MistyRose;
            }
            else
            {
                tbECParamR.BackColor = SystemColors.Window;
            }//*/

            m_gate_cbECKeyLength = false;
        }

        private void cbECKeyLength_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbECKeyLength.BackColor = SystemColors.Window;

            if (m_gate_cbECKeyLength)
            {
                return;
            }

            switch (cbECKeyLength.SelectedIndex)
            {
                case 0:
                    if (cbECName.SelectedIndex != 1)
                        cbECName.SelectedIndex = 0;
                    break;
                case 1:
                    if (cbECName.SelectedIndex != 16)
                        cbECName.SelectedIndex = 15;
                    break;
                case 2:
                    if (cbECName.SelectedIndex != 3)
                        cbECName.SelectedIndex = 2;
                    break;
                case 3:
                    if (cbECName.SelectedIndex != 18)
                        cbECName.SelectedIndex = 17;
                    break;
                case 4:
                    if (cbECName.SelectedIndex != 5 && cbECName.SelectedIndex != 6)
                        cbECName.SelectedIndex = 4;
                    break;
                case 5:
                    if (cbECName.SelectedIndex != 20 && cbECName.SelectedIndex != 21)
                        cbECName.SelectedIndex = 19;
                    break;
                case 6:
                    if (cbECName.SelectedIndex != 8)
                        cbECName.SelectedIndex = 7;
                    break;
                case 7:
                    if (cbECName.SelectedIndex != 23)
                        cbECName.SelectedIndex = 22;
                    break;
                case 8:
                    if (cbECName.SelectedIndex != 10)
                        cbECName.SelectedIndex = 9;
                    break;
                case 9:
                    if (cbECName.SelectedIndex != 25)
                        cbECName.SelectedIndex = 24;
                    break;
                case 10:
                    cbECName.SelectedIndex = 26;
                    break;
                case 11:
                    if (cbECName.SelectedIndex != 12)
                        cbECName.SelectedIndex = 11;
                    break;
                case 12:
                    if (cbECName.SelectedIndex != 28)
                        cbECName.SelectedIndex = 27;
                    break;
                case 13:
                    cbECName.SelectedIndex = 13;
                    break;
                case 14:
                    if (cbECName.SelectedIndex != 30)
                        cbECName.SelectedIndex = 29;
                    break;
                case 15:
                    cbECName.SelectedIndex = 14;
                    break;
                case 16:
                    if (cbECName.SelectedIndex != 32)
                        cbECName.SelectedIndex = 31;
                    break;
            }
        }
        
        //======================================================================================================================
        // Signature page:
        //======================================================================================================================
        enum RADIX
        {
            Hex,
            Base64,
            ASCII,
            Exception_FromFile
        };
        RADIX mMessageRadix = RADIX.Hex;
        RADIX mSignatureRadix = RADIX.Hex;
        RADIX mHashRadix = RADIX.Hex;
        Stream mSignigFileStream = null;
        long mChunkedBytesToRead;
        bool isECDSACipher = false;

        private void tbMessageRadixChanged(object sender, EventArgs e)
        {
            bool showExceptionMessage = true;
            try
            {
                if (sender == rbMessageFromFile)
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Filter = "All files (*.*)|*.*";
                    //dialog.InitialDirectory = @"C:\";
                    dialog.Title = "Please select file for signing";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        tbMessage.ReadOnly = true;
                        tbMessage.BackColor = SystemColors.Info;
                        tbMessage.Text = dialog.FileName;
                        mMessageRadix = RADIX.Exception_FromFile;
                        btnSaveMessageToBin.Enabled = false;
                    }
                    else
                    {
                        showExceptionMessage = false;
                        throw new Exception("File selection canceled");
                    }
                }
                else
                {
                    switch (mMessageRadix) // from radix
                    {
                        case RADIX.Hex:
                            if (sender == rbMessageBase64)
                            {
                                tbMessage.Text = Convert.ToBase64String(Hex.Decode(tbMessage.Text));
                                mMessageRadix = RADIX.Base64;
                            }
                            else if (sender == rbMessageAscii)
                            {
                                tbMessage.Text = Encoding.ASCII.GetString(Hex.Decode(tbMessage.Text));
                                mMessageRadix = RADIX.ASCII;
                            }
                            break;
                        case RADIX.Base64:
                            if (sender == rbMessageHex)
                            {
                                tbMessage.Text = BitConverter.ToString(Convert.FromBase64String(tbMessage.Text)).Replace("-", "");
                                mMessageRadix = RADIX.Hex;
                            }
                            else if (sender == rbMessageAscii)
                            {
                                tbMessage.Text = Encoding.ASCII.GetString(Convert.FromBase64String(tbMessage.Text));
                                mMessageRadix = RADIX.ASCII;
                            }
                            break;
                        case RADIX.ASCII:
                            if (sender == rbMessageHex)
                            {
                                tbMessage.Text = BitConverter.ToString(Encoding.ASCII.GetBytes(tbMessage.Text)).Replace("-", "");
                                mMessageRadix = RADIX.Hex;
                            }
                            else if (sender == rbMessageBase64)
                            {
                                tbMessage.Text = Convert.ToBase64String(Encoding.ASCII.GetBytes(tbMessage.Text));
                                mMessageRadix = RADIX.Base64;
                            }
                            break;
                        case RADIX.Exception_FromFile:
                            tbMessage.BackColor = SystemColors.Window;
                            tbMessage.Text = "";
                            tbMessage.ReadOnly = false;
                            if (sender == rbMessageHex)
                                mMessageRadix = RADIX.Hex;
                            else if (sender == rbMessageBase64)
                                mMessageRadix = RADIX.Base64;
                            else if (sender == rbMessageAscii)
                                mMessageRadix = RADIX.ASCII;

                            btnSaveMessageToBin.Enabled = true;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                switch (mMessageRadix)
                {
                    case RADIX.Hex:
                        rbMessageHex.Checked = true;
                        break;
                    case RADIX.Base64:
                        rbMessageBase64.Checked = true;
                        break;
                    case RADIX.ASCII:
                        rbMessageAscii.Checked = true;
                        break;
                    case RADIX.Exception_FromFile:
                        rbMessageFromFile.Checked = true;
                        break;
                }
                if (showExceptionMessage)
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbSignatureRadixChanged(object sender, EventArgs e)
        {
            try
            {
                if (mSignatureRadix == RADIX.Hex && sender == rbSignatureBase64)
                {
                    tbSignature.Text = Convert.ToBase64String(Hex.Decode(tbSignature.Text));
                    mSignatureRadix = RADIX.Base64;
                }
                else if (mSignatureRadix == RADIX.Base64 && sender == rbSignatureHex)
                {
                    tbSignature.Text = BitConverter.ToString(Convert.FromBase64String(tbSignature.Text)).Replace("-", "");
                    mSignatureRadix = RADIX.Hex;
                }
            }
            catch (Exception ex)
            {
                if (mSignatureRadix == RADIX.Hex)
                    rbSignatureHex.Checked = true;
                else // case RADIX.Base64:
                    rbSignatureBase64.Checked = true;

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbHashRadixChanged(object sender, EventArgs e)
        {
            try
            {
                if (mHashRadix == RADIX.Hex && sender == rbHashBase64)
                {
                    tbHash.Text = Convert.ToBase64String(Hex.Decode(tbHash.Text));
                    mHashRadix = RADIX.Base64;
                }
                else if (mHashRadix == RADIX.Base64 && sender == rbHashHex)
                {
                    tbHash.Text = BitConverter.ToString(Convert.FromBase64String(tbHash.Text)).Replace("-", "");
                    mHashRadix = RADIX.Hex;
                }
            }
            catch (Exception ex)
            {
                if (mHashRadix == RADIX.Hex)
                    rbHashHex.Checked = true;
                else // RADIX.Base64:
                    rbHashBase64.Checked = true;

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private byte[] Hash(string filename, HashAlgorithm hash)
        {
            byte[] hashBytes;
            
            using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read,
              FileShare.Delete | FileShare.ReadWrite))
            {
                try
                {
                    hashBytes = hash.ComputeHash(fs);
                }
                finally
                {
                    hash.Clear();
                }
            }
            return hashBytes;
        }

        private byte[] Hash(byte[] plain, HashAlgorithm hash)
        {
            byte[] hashBytes;
            {
                try
                {
                    hashBytes = hash.ComputeHash(plain);
                }
                finally
                {
                    hash.Clear();
                }
            }
            return hashBytes;
        }

        private void btnHash_Click(object sender, EventArgs e)
        {
            HashAlgorithm hashAlg;
            byte[] plain;

            try
            {
                if (mSignatureRadix == RADIX.Hex)
                {
                    plain = Hex.Decode(tbSignature.Text);
                }
                else // mSignatureRadix == RADIX.Base64
                {
                    plain = Convert.FromBase64String(tbSignature.Text);
                }
                if (plain.Length == 0)
                    throw new Exception("Invalid signature.");

                switch (cbHashAlg.Text)
                {
                    case "MD5":
                        hashAlg = new MD5CryptoServiceProvider();
                        break;
                    case "SHA-1":
                        hashAlg = new SHA1CryptoServiceProvider();
                        break;
                    case "SHA-256":
                        hashAlg = new SHA256CryptoServiceProvider();
                        break;
                    case "SHA-384":
                        hashAlg = new SHA384CryptoServiceProvider();
                        break;
                    case "SHA-512":
                        hashAlg = new SHA512CryptoServiceProvider();
                        break;
                    default:
                        throw new Exception("Unknown hash algorithm");
                }

                if (mHashRadix == RADIX.Hex)
                {
                    tbHash.Text = BitConverter.ToString(Hash(plain, hashAlg)).Replace("-", "");
                }
                else // mHashRadix == RADIX.Base64
                {
                    tbHash.Text = Convert.ToBase64String(Hash(plain, hashAlg));
                }

                btnHashStoreToBin.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveMessageToBin_Click(object sender, EventArgs e)
        {
            byte[] byteArray;
            SaveFileDialog dialog;

            try
            {
                switch (mMessageRadix) // from radix
                {
                    case RADIX.Hex:
                        byteArray = Hex.Decode(tbMessage.Text);
                        break;
                    case RADIX.Base64:
                        byteArray = Convert.FromBase64String(tbMessage.Text);
                        break;
                    case RADIX.ASCII:
                        byteArray = Encoding.ASCII.GetBytes(tbMessage.Text);
                        break;
                    default:
                        throw new Exception("Unknown input data radix");
                }
                if (byteArray.Length == 0)
                    throw new Exception("There is no valid data to save.");

                dialog = new SaveFileDialog();
                dialog.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";
                //dialog.FilterIndex = 2;
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (var fs = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(byteArray, 0, byteArray.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSignatureStoreToBin_Click(object sender, EventArgs e)
        {
            byte[] byteArray;
            SaveFileDialog dialog;

            try
            {
                switch (mSignatureRadix) // from radix
                {
                    case RADIX.Hex:
                        byteArray = Hex.Decode(tbSignature.Text);
                        break;
                    case RADIX.Base64:
                        byteArray = Convert.FromBase64String(tbSignature.Text);
                        break;
                    case RADIX.ASCII:
                        byteArray = Encoding.ASCII.GetBytes(tbSignature.Text);
                        break;
                    default:
                        throw new Exception("Unknown input data radix");
                }
                if (byteArray.Length == 0)
                    throw new Exception("There is no valid data to save.");

                dialog = new SaveFileDialog();
                dialog.Filter = "Signature binary files (*.sig)|*.sig|Binary files (*.bin)|*.bin|All files (*.*)|*.*";
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (var fs = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(byteArray, 0, byteArray.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHashStoreToBin_Click(object sender, EventArgs e)
        {
            byte[] byteArray;
            string signature_extension, file_comment;

            SaveFileDialog dialog;

            try
            {
                file_comment = " hash of the signature generated by uFR Signer (http://www.d-logic.net/nfc-rfid-reader-sdk/)\r\n";
                switch (cbHashAlg.Text)
                {
                    case "MD5":
                        signature_extension = "MD5 files (*.md5)|*.md5|";
                        file_comment = "# MD5" + file_comment;
                        break;
                    case "SHA-1":
                        signature_extension = "SHA1 files (*.sha1)|*.sha1|";
                        file_comment = "# SHA1" + file_comment;
                        break;
                    case "SHA-256":
                        signature_extension = "SHA256 files (*.sha256)|*.sha256|";
                        file_comment = "# SHA256" + file_comment;
                        break;
                    case "SHA-384":
                        signature_extension = "SHA384 files (*.sha384)|*.sha384|";
                        file_comment = "# SHA384" + file_comment;
                        break;
                    case "SHA-512":
                        signature_extension = "SHA512 files (*.sha512)|*.sha512|";
                        file_comment = "# SHA512" + file_comment;
                        break;
                    default:
                        throw new Exception("Unknown hash algorithm");
                }

                switch (mHashRadix) // from radix
                {
                    case RADIX.Hex:
                        byteArray = Hex.Decode(tbHash.Text);
                        break;
                    case RADIX.Base64:
                        byteArray = Convert.FromBase64String(tbHash.Text);
                        break;
                    case RADIX.ASCII:
                        byteArray = Encoding.ASCII.GetBytes(tbHash.Text);
                        break;
                    default:
                        throw new Exception("Unknown input data radix");
                }
                if (byteArray.Length == 0)
                    throw new Exception("There is no valid data to save.");

                dialog = new SaveFileDialog();
                dialog.Filter = signature_extension + "Binary files (*.bin)|*.bin";
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (dialog.FilterIndex == 1)
                    {
                        // hash text file format:
                        File.WriteAllText(dialog.FileName, file_comment + "\r\n" 
                            + BitConverter.ToString(byteArray).Replace("-", "") 
                            + " *\r\n"); // ToDo: + mSignatureFileName between * and \r\n
                    }
                    else using (var fs = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(byteArray, 0, byteArray.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int mHashAlgCurrComboIndex = 0;

        private void cbHashAlg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mHashAlgCurrComboIndex != cbHashAlg.SelectedIndex)
            {
                mHashAlgCurrComboIndex = cbHashAlg.SelectedIndex;
                tbHash.Text = "";
            }
        }

        IDigest mLocalDigest;
        bool mLocalDigestInUse;
        byte[] mData;
        int mChunkSize;
        DerObjectIdentifier mOid;
        byte jc_signer_cipher;
        byte jc_signer_digest;
        byte jc_signer_padding;
        byte key_index;
        UInt16 key_size_bits;

        private void btnSignature_Click(object sender, EventArgs e)
        {
            DL_STATUS status = DL_STATUS.UFR_OK;
            BackgroundWorker bgw = null;
            byte[] sig;
            bool chunked = false;
            byte[] chunk;
            string localDigestMechanism = "";

            jc_signer_cipher = 0;
            jc_signer_digest = 0;
            jc_signer_padding = 0;
            key_index = 0;

            try
            {
                UseWaitCursor = true;

                mLocalDigestInUse = true;

                if (!uFR_Opened)
                    throw new Exception(uFR_NotOpenedMessage);
#if USING_PIN
                if (!mUserPinLoggedIn)
                    throw new Exception("To get signature, login as User (enter User PIN and click \"User Login\" on \"PIN Codes\" tab)");
#endif
                if (tbMessage.Text.Trim().Equals(""))
                    throw new Exception("Signing message can't have a zero length.");

                tbSignature.Text = "";
                btnSignatureStoreToBin.Enabled = false;

                key_index = Convert.ToByte(cbSignatureKeyIndex.Text);

                if (cbCipher.Text.Equals("RSA"))
                {
                    isECDSACipher = false;
                    jc_signer_cipher = (byte)JCDL_SIGNER_CIPHERS.SIG_CIPHER_RSA;
                    jc_signer_padding = (byte)JCDL_SIGNER_PADDINGS.PAD_PKCS1;
                }
                else if (cbCipher.Text.Equals("ECDSA"))
                {
                    isECDSACipher = true;
                    jc_signer_cipher = (byte)JCDL_SIGNER_CIPHERS.SIG_CIPHER_ECDSA;
                    jc_signer_padding = (byte)JCDL_SIGNER_PADDINGS.PAD_NULL;
                }

                switch (cbDigest.Text)
                {
                    case "None":
                        localDigestMechanism = null;
                        jc_signer_digest = (byte)JCDL_SIGNER_DIGESTS.ALG_NULL;
                        mOid = null;
                        break;
                    case "SHA-1":
                        localDigestMechanism = "SHA-1";
                        jc_signer_digest = (byte)JCDL_SIGNER_DIGESTS.ALG_SHA;
                        mOid = OiwObjectIdentifiers.IdSha1;
                        break;
                    case "SHA-224":
                        localDigestMechanism = "SHA-224";
                        jc_signer_digest = (byte)JCDL_SIGNER_DIGESTS.ALG_SHA_224;
                        mOid = NistObjectIdentifiers.IdSha224;
                        break;
                    case "SHA-256":
                        localDigestMechanism = "SHA-256";
                        jc_signer_digest = (byte)JCDL_SIGNER_DIGESTS.ALG_SHA_256;
                        mOid = NistObjectIdentifiers.IdSha256;
                        break;
                    case "SHA-384":
                        localDigestMechanism = "SHA-384";
                        jc_signer_digest = (byte)JCDL_SIGNER_DIGESTS.ALG_SHA_384;
                        mOid = NistObjectIdentifiers.IdSha384;
                        break;
                    case "SHA-512":
                        localDigestMechanism = "SHA-512";
                        jc_signer_digest = (byte)JCDL_SIGNER_DIGESTS.ALG_SHA_512;
                        mOid = NistObjectIdentifiers.IdSha512;
                        break;
                    default:
                        throw new Exception("Unknown digest algorithm");
                }

                if (chkDigestOffCard.Checked)
                {
                    jc_signer_digest = (byte)JCDL_SIGNER_DIGESTS.ALG_NULL;
                    mLocalDigestInUse = true;
                }
                else
                {
                    mLocalDigestInUse = false;
                }

                if (mLocalDigestInUse)
                    mLocalDigest = DigestUtilities.GetDigest(localDigestMechanism);

                switch (mMessageRadix) // from radix
                {
                    case RADIX.Hex:
                        mData = Hex.Decode(tbMessage.Text);
                        break;
                    case RADIX.Base64:
                        mData = Convert.FromBase64String(tbMessage.Text);
                        break;
                    case RADIX.ASCII:
                        mData = Encoding.ASCII.GetBytes(tbMessage.Text);
                        break;
                    case RADIX.Exception_FromFile:
                        mData = new byte[uFCoder.SIG_MAX_PLAIN_DATA_LEN];
                        if (localDigestMechanism == null)
                            throw new Exception("Can't sign a file without using digest algorithm.");
                        break;
                    default:
                        throw new Exception("Unknown input data radix");
                }
                //data.Length;

                pbSigning.Value = 0;
                pbSigning.Maximum = 10000;
                Refresh();

                if (mMessageRadix == RADIX.Exception_FromFile)
                    mSignigFileStream = File.OpenRead(tbMessage.Text);

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
                status = uFCoder.JCAppLogin(false, tbPin.Text);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));
#endif
                int max_len;
                if (mLocalDigestInUse)
                    max_len = 8192;
                else
                    max_len = (int)uFCoder.SIG_MAX_PLAIN_DATA_LEN;

                if (mSignigFileStream != null)
                {
                    chunked = (int)mSignigFileStream.Length > max_len;
                    mChunkSize = chunked ? max_len : (int)mSignigFileStream.Length;

                    chunk = new byte[mChunkSize];
                    if (mSignigFileStream.Read(chunk, 0, mChunkSize) != mChunkSize)
                        throw new Exception("Error while reading file " + tbMessage.Text);
                    mChunkedBytesToRead = (int)mSignigFileStream.Length - mChunkSize;
                }
                else
                {
                    chunked = mData.Length > max_len;
                    mChunkSize = chunked ? max_len : mData.Length;

                    chunk = new byte[mChunkSize];
                    Array.Copy(mData, 0, chunk, 0, mChunkSize);
                    mChunkedBytesToRead = mData.Length - mChunkSize;
                }

                // For ECDSA we need key length in bits:
                if (isECDSACipher)
                {
                    UInt16 key_designator;
                    status = uFCoder.JCAppGetEcKeySizeBits(key_index, out key_size_bits, out key_designator);
                    if (status != DL_STATUS.UFR_OK)
                        throw new Exception(string.Format("Card error code: 0x{0:X}", status));
                }

                if (chunked)
                {
                    if ((mSignigFileStream != null))
                        pbSigning.Maximum = (int)(mSignigFileStream.Length / max_len);
                    else
                        pbSigning.Maximum = (int)(mData.Length / max_len);

                    if (mLocalDigestInUse)
                        mLocalDigest.BlockUpdate(chunk, 0, mChunkSize);
                    else
                    {
                        status = uFCoder.JCAppSignatureBegin(jc_signer_cipher, jc_signer_digest, jc_signer_padding,
                                                         key_index, chunk, (UInt16)mChunkSize,
                                                         null, 0);
                        if (status != DL_STATUS.UFR_OK)
                            throw new Exception(string.Format("Card error code: 0x{0:X}", status));
                    }

                    bgw = new BackgroundWorker();
                    bgw.ProgressChanged += bgw_ProgressChanged;
                    bgw.DoWork += bgw_DoWork;
                    bgw.WorkerReportsProgress = true;
                    bgw.RunWorkerCompleted += bgw_WorkCompleted;
                    bgw.RunWorkerAsync();

                    // ??? pbSigning.Value = 5;

                    // Disable controls critical for execution:
                    tabControl.Enabled = false;
                    return;
                }
                else
                {
                    byte[] to_be_signed;
                    byte[] hash;

                    if (localDigestMechanism == null)
                        hash = chunk;
                    else
                    {
                        hash = new byte[mLocalDigest.GetDigestSize()];
                        mLocalDigest.BlockUpdate(chunk, 0, mChunkSize);
                        mLocalDigest.DoFinal(hash, 0);
                    }

                    if (isECDSACipher)
                    {
                        // ECDSA hash/plain_txt alignment before signing:

                        to_be_signed = Enumerable.Repeat((byte)0, (key_size_bits + 7) / 8).ToArray();
                        if (to_be_signed.Length > hash.Length)
                            //Array.Copy(hash, 0, to_be_signed, to_be_signed.Length - hash.Length, hash.Length);
                            to_be_signed = hash; // Can be done on J3H145 because supporting Cipher.PAD_NULL with Signature.SIG_CIPHER_ECDSA 
                        else // in case of (to_be_signed.Length <= hash.Length)
                        {
                            Array.Copy(hash, 0, to_be_signed, 0, to_be_signed.Length);
                            if ((key_size_bits % 8) != 0)
                            {
                                byte prev_byte = 0;
                                byte shift_by = (byte)(key_size_bits % 8);

                                for (int i = 0; i < to_be_signed.Length; i++)
                                {
                                    byte temp = to_be_signed[i];
                                    to_be_signed[i] >>= 8 - shift_by;
                                    to_be_signed[i] |= prev_byte;
                                    prev_byte = temp <<= shift_by;
                                }
                            }
                        }
                    }
                    else
                    {
                        // RSA signer preparation:
                        Org.BouncyCastle.Asn1.X509.DigestInfo dInfo =
                                new Org.BouncyCastle.Asn1.X509.DigestInfo(new Org.BouncyCastle.Asn1.X509.AlgorithmIdentifier(mOid, DerNull.Instance), hash);
                        to_be_signed = dInfo.GetDerEncoded();
                    }

// Examples (debug):
//                        hash = DigestUtilities.CalculateDigest(/*localDigestMechanism*/"SHA-256", chunk);
//                        byte[] temp = DigestUtilities.CalculateDigest(/*localDigestMechanism*/"SHA-256", chunk);

                    status = uFCoder.JCAppGenerateSignature(jc_signer_cipher, jc_signer_digest, jc_signer_padding,
                                                            key_index,
                                                            //chunk, (UInt16)mChunkSize, 
                                                            to_be_signed, (UInt16)to_be_signed.Length,
                                                            out sig, 
                                                            null, 0);
                    if (status != DL_STATUS.UFR_OK)
                        throw new Exception(string.Format("Card error code: 0x{0:X}", status));
                }

                pbSigning.Value = pbSigning.Maximum;
                // workaround for animation feature (aero theme):
                pbSigning.Value = pbSigning.Maximum - 1;
                pbSigning.Value = pbSigning.Maximum;

                tbSignature.Text = "";
                //tbSignatureRadixChanged(rbMessageHex, new EventArgs());
                rbSignatureHex.Checked = true;
                mSignatureRadix = RADIX.Hex;

                if (isECDSACipher)
                {
                    // In case of ECDSA signature, last 2 bytes are ushort value representing the key_size in bits
                    int len = sig.Length;
                    key_size_bits = (UInt16)(((UInt16)sig[len - 2] << 8) | sig[len - 1]);
                    int key_size_bytes = (key_size_bits + 7) / 8;
                    byte[] der_sig = new byte[len - 2];
                    Array.Copy(sig, der_sig, len - 2);

                    der_sig = DLogicAsn1Tools.fixEccSignatureSequence(der_sig);

                    /*/
                    // If you want to remove DER tags and lengths from signature:
                    Asn1InputStream decoder = new Asn1InputStream(new MemoryStream(der_sig));
                    DerSequence seq = (DerSequence)decoder.ReadObject();
                    DerInteger der_r = (DerInteger)seq[0];
                    DerInteger der_s = (DerInteger)seq[1];
                    byte[] r = der_r.PositiveValue.ToByteArray();
                    byte[] s = der_s.PositiveValue.ToByteArray();

                    // Fix leading zeros (padding or removing depending on mECKeyByteSize:
                    while (r[0] == 0 && r.Length > key_size_bytes)
                    {
                        r = r.Skip(1).ToArray();
                    }
                    if (key_size_bytes > r.Length)
                        r = zeroPadArray(r, 0, key_size_bytes - r.Length);

                    while (s[0] == 0 && s.Length > key_size_bytes)
                    {
                        s = s.Skip(1).ToArray();
                    }
                    if (key_size_bytes > s.Length)
                        s = zeroPadArray(s, 0, key_size_bytes - s.Length);

                    tbSignature.Text = BitConverter.ToString(r).Replace("-", "") + BitConverter.ToString(s).Replace("-", "");//*/

                    tbSignature.Text = BitConverter.ToString(der_sig).Replace("-", "");
                }
                else
                    tbSignature.Text = BitConverter.ToString(sig).Replace("-", "");

                btnSignatureStoreToBin.Enabled = true;
            }
            catch (Exception ex)
            {
                if (((int)status & 0xFFFFC0) == 0x0A63C0)
                {
                    mUserPinLoggedIn = false;
                    tbPin.Text = "";
                    btnUserLogout.Enabled = false;
                    MessageBox.Show("Wrong user PIN code. Tries remaining: " + ((int)status & 0x3F),
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (((int)status == 0x0A6984)                                   // SW_DATA_INVALID
                        && (jc_signer_digest == (byte)JCDL_SIGNER_DIGESTS.ALG_NULL))
                {
                        MessageBox.Show(ex.Message
                            + "\r\n\r\nATTENTION:"
                            + "\r\nWhen \"None\" digest algorithm is in use with RSA, this algorithm is only suitable for messages of limited length."
                            + "\r\nThe total number of input bytes processed may not be more than RSA key's modulus size in bytes substracted by 11.", 
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (bgw == null)
                {
                    if (mSignigFileStream != null)
                    {
                        mSignigFileStream.Close();
                        mSignigFileStream = null;
                    }

                    if (uFR_Selected)
                    {
                        uFCoder.s_block_deselect(100);
                        uFR_Selected = false;
                    }

                    UseWaitCursor = false;
                }
            }
#if MY_DEBUG
            // Debug:
            debugSignature();
            //------------------
#endif
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] chunk = new byte[mChunkSize];
            int chunk_len = 0;
            DL_STATUS status;

            try
            {
                do
                {
                    if (mSignigFileStream != null)
                        chunk_len = mSignigFileStream.Read(chunk, 0, mChunkSize);
                    else
                    {
                        chunk_len = mChunkedBytesToRead > mChunkSize ? mChunkSize : (int)mChunkedBytesToRead;
                        Array.Copy(mData, mData.Length - mChunkedBytesToRead, chunk, 0, chunk_len);
                    }
                    mChunkedBytesToRead -= chunk_len;

                    if (mLocalDigestInUse)
                        mLocalDigest.BlockUpdate(chunk, 0, chunk_len);
                    else
                    {
                        status = uFCoder.JCAppSignatureUpdate(chunk, (UInt16)chunk_len);
                        if (status != DL_STATUS.UFR_OK)
                            throw new Exception(string.Format("Card error code: 0x{0:X}", status));
                    }

                    // when using PerformStep() the percentProgress arg is redundant
                    ((BackgroundWorker)sender).ReportProgress(0);
                }
                while (mChunkedBytesToRead > 0);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                mSignigFileStream.Close();
                mSignigFileStream = null;
            }
        }

        void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbSigning.PerformStep();
        }

        void bgw_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DL_STATUS status;

            try
            {
                pbSigning.Value = pbSigning.Maximum;

                byte[] sig;
                if (mLocalDigestInUse)
                {
                    byte[] to_be_signed;
                    byte[] hash;

                    hash = new byte[mLocalDigest.GetDigestSize()];
                    mLocalDigest.DoFinal(hash, 0);

                    if (isECDSACipher)
                    {
                        // ECDSA hash/plain_txt alignment before signing:

                        to_be_signed = Enumerable.Repeat((byte)0, (key_size_bits + 7) / 8).ToArray();
                        if (to_be_signed.Length > hash.Length)
                            //Array.Copy(hash, 0, to_be_signed, to_be_signed.Length - hash.Length, hash.Length);
                            to_be_signed = hash; // debug
                        else // in case of (to_be_signed.Length <= hash.Length)
                        {
                            Array.Copy(hash, 0, to_be_signed, 0, to_be_signed.Length);
                            if ((key_size_bits % 8) != 0)
                            {
                                byte prev_byte = 0;
                                byte shift_by = (byte)(key_size_bits % 8);

                                for (int i = 0; i < to_be_signed.Length; i++)
                                {
                                    byte temp = to_be_signed[i];
                                    to_be_signed[i] >>= 8 - shift_by;
                                    to_be_signed[i] |= prev_byte;
                                    prev_byte = temp <<= shift_by;
                                }
                            }
                        }
                    }
                    else
                    {
                        // RSA signer preparation:
                        Org.BouncyCastle.Asn1.X509.DigestInfo dInfo =
                                new Org.BouncyCastle.Asn1.X509.DigestInfo(new Org.BouncyCastle.Asn1.X509.AlgorithmIdentifier(mOid, DerNull.Instance), hash);
                        to_be_signed = dInfo.GetDerEncoded();
                    }

                    status = uFCoder.JCAppGenerateSignature(jc_signer_cipher, jc_signer_digest, jc_signer_padding,
                                                            key_index,
                                                            to_be_signed, (UInt16)to_be_signed.Length,
                                                            out sig,
                                                            null, 0);
                    if (status != DL_STATUS.UFR_OK)
                        throw new Exception(string.Format("Card error code: 0x{0:X}", status));
                }
                else
                {
                    status = uFCoder.JCAppSignatureEnd(out sig);
                    if (status != DL_STATUS.UFR_OK)
                        throw new Exception(string.Format("Card error code: 0x{0:X}", status));
                }

                tbSignature.Text = "";
                //tbSignatureRadixChanged(rbMessageHex, new EventArgs());
                rbSignatureHex.Checked = true;
                mSignatureRadix = RADIX.Hex;

                if (isECDSACipher)
                {
                    // In case of ECDSA signature, last 2 bytes are ushort value representing the key_size in bits
                    int len = sig.Length;
                    UInt16 key_size_bits = (UInt16)(((UInt16)sig[len - 2] << 8) | sig[len - 1]);
                    int key_size_bytes = (key_size_bits + 7) / 8;
                    byte[] der_sig = new byte[len - 2];
                    Array.Copy(sig, der_sig, len - 2);

                    der_sig = DLogicAsn1Tools.fixEccSignatureSequence(der_sig);
                    tbSignature.Text = BitConverter.ToString(der_sig).Replace("-", "");
                }
                else
                    tbSignature.Text = BitConverter.ToString(sig).Replace("-", "");

                btnSignatureStoreToBin.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (uFR_Selected)
                {
                    uFCoder.s_block_deselect(100);
                    uFR_Selected = false;
                }

                if (mSignigFileStream != null)
                {
                    mSignigFileStream.Close();
                    mSignigFileStream = null;
                }

                // Enable controls critical for execution:
                tabControl.Enabled = true;
                UseWaitCursor = false;
                Cursor.Current = Cursors.Default;
            }
        }

        //----------------------------------------------------------------------

        private void btnOpenCertFile_Click(object sender, EventArgs e)
        {
            X509Certificate2 cert = null;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "PEM files (*.pem;*.crt;*.cer)|*.pem;*.crt;*.cer|PKSC#12 files (*.p12; *.pfx)|*.p12; *.pfx|All files (*.*)|*.*";
            //dialog.InitialDirectory = @"C:\";
            dialog.Title = "Please select the file containing certificate";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string file_ext = Path.GetExtension(dialog.FileName);

                    if (file_ext == ".p12" || file_ext == ".pfx")
                    {
                        frmPassword dlgPasswd = new frmPassword();
                        if (dlgPasswd.ShowDialog() == DialogResult.OK)
                        {
                            mCertPassword = dlgPasswd.password;
                            cert = new X509Certificate2(dialog.FileName, mCertPassword, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);
                        }
                    }
                    else
                    {
                        cert = new X509Certificate2(dialog.FileName);
                    }

                    tbCertFile.Text = dialog.FileName;

                    foreach (X509Extension extension in cert.Extensions)
                        if (extension.Oid.FriendlyName == "Basic Constraints")
                        {
                            X509BasicConstraintsExtension ext = (X509BasicConstraintsExtension)extension;
                            if (ext.CertificateAuthority)
                            {
                                lbCertUsageType.Text = "Selected is CA";
                                lbCertUsageType.Font = new Font(lbCertUsageType.Font, FontStyle.Bold);
                            }
                            else
                            {
                                lbCertUsageType.Text = "Selected is not CA";
                                lbCertUsageType.Font = new Font(lbCertUsageType.Font, FontStyle.Regular);
                            }
                        }

                    // Now we display certificate dialog:
                    X509Certificate2UI.DisplayCertificate(cert, this.Handle);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnShowSelectedFile_Click(object sender, EventArgs e)
        {
            X509Certificate2 cert = null;

            if (!(File.Exists(tbCertFile.Text) && Path.HasExtension(tbCertFile.Text)))
            {
                MessageBox.Show("Invalid certificate file name and / or path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string file_ext = Path.GetExtension(tbCertFile.Text);

                if (file_ext == ".p12" || file_ext == ".pfx")
                {
                    cert = new X509Certificate2(tbCertFile.Text, mCertPassword, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);
                }
                else
                {
                    cert = new X509Certificate2(tbCertFile.Text);
                }

                X509Certificate2UI.DisplayCertificate(cert, this.Handle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPutCertFromFile_Click(object sender, EventArgs e)
        {
            DL_STATUS status = DL_STATUS.UFR_OK;
            byte obj_type = (byte) cbObjType.SelectedIndex;
            byte obj_index = Convert.ToByte(cbObjIndex.Text);
            X509Certificate2 cert = null;

            if (!(File.Exists(tbCertFile.Text) && Path.HasExtension(tbCertFile.Text)))
            {
                MessageBox.Show("Invalid certificate file name and / or path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (!uFR_Opened)
                    throw new Exception(uFR_NotOpenedMessage);
#if USING_PIN
                if (!mSOPinLoggedIn)
                    throw new Exception("To store key pairs, login as SO (enter SO PIN and click \"SO Login\" on \"PIN Codes\" tab)");
#endif
                string file_ext = Path.GetExtension(tbCertFile.Text);

                if (file_ext == ".p12" || file_ext == ".pfx")
                {
                    cert = new X509Certificate2(tbCertFile.Text, mCertPassword, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);
                }
                else
                {
                    cert = new X509Certificate2(tbCertFile.Text);
                }

                // Should remove any private key from cert:
                if (cert.HasPrivateKey)
                {
                    cert = new X509Certificate2(cert.Export(X509ContentType.Cert));
                }

                byte[] raw_cert = cert.Export(X509ContentType.Cert);
                if (raw_cert == null || raw_cert.Length == 0)
                    throw new Exception("Invalid certificate");
                byte[] raw_subject = cert.SubjectName.RawData;
                if (raw_subject == null || raw_subject.Length == 0)
                    throw new Exception("Invalid certificate");
                byte[] raw_id = Encoding.ASCII.GetBytes(tbObjId.Text);
                if (raw_id.Length == 0)
                    throw new Exception("Invalid Id");

                // Open JCApp:
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
                status = uFCoder.JCAppLogin(true, tbSOPin.Text);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));
#endif

                status = uFCoder.JCAppPutObj(obj_type,  obj_index, raw_cert, (UInt16) raw_cert.Length, raw_id, (byte) raw_id.Length);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                status = uFCoder.JCAppPutObjSubject(obj_type, obj_index, raw_subject, (byte)raw_subject.Length);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                if (uFR_Selected)
                {
                    uFR_Selected = false;
                    uFCoder.s_block_deselect(100);
                }

                btnRefresh_Click(btnRefresh, new EventArgs());

                MessageBox.Show("The certificate has been successfully stored.", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (((int)status & 0xFFFFC0) == 0x0A63C0)
                {
                    btnSOLogout.Enabled = false;
                    mSOPinLoggedIn = false;
                    tbSOPin.Text = "";
                    MessageBox.Show("Wrong SO PIN code. Tries remaining: " + ((int)status & 0x3F),
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (uFR_Selected)
                {
                    uFCoder.s_block_deselect(100);
                    uFR_Selected = false;
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DL_STATUS status;
            byte obj_type, obj_index, max_index;
            ListView lstv;

            X509Certificate2 cert = null;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (!uFR_Opened)
                    throw new Exception(uFR_NotOpenedMessage);

                byte[] raw_cert;
                byte[] raw_id;
                UInt16 cert_size;

                // Open JCApp:
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

                max_index = 3;
                for (obj_type = 0; obj_type < 3; obj_type++) {

                    switch (obj_type)
                    {
                        case 0:
                            lstv = lstvRSACerts;
                            break;
                        case 1:
                            lstv = lstvECDSACerts;
                            break;
                        default:
                            lstv = lstvCACerts;
                            max_index = 12;
                            break;
                    }

                    for (obj_index = 0; obj_index < max_index; obj_index++)
                    {
                        lstv.Items[obj_index].SubItems[1].Text = "Empty";

                        status = uFCoder.JCAppGetObjId(obj_type, obj_index, out raw_id, out cert_size);
                        if (status != DL_STATUS.UFR_OK)
                        {
                            // Retry:
                            Thread.Sleep(30);
                            status = uFCoder.JCAppGetObjId(obj_type, obj_index, out raw_id, out cert_size);
                            if (status != DL_STATUS.UFR_OK)
                                continue;
                        }

                        Thread.Sleep(30);

                        raw_cert = new byte[cert_size];
                        status = uFCoder.JCAppGetObj(obj_type, obj_index, raw_cert);
                        if (status != DL_STATUS.UFR_OK)
                        {
                            // Retry:
                            Thread.Sleep(30);
                            status = uFCoder.JCAppGetObj(obj_type, obj_index, raw_cert);
                            if (status != DL_STATUS.UFR_OK)
                                continue;
                        }

                        try
                        {
                            cert = new X509Certificate2(raw_cert);
                            lstv.Items[obj_index].SubItems[1].Text = cert.Subject;
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }

                    // AutoResizeColumns:
                    lstv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    ListView.ColumnHeaderCollection ccnt = lstv.Columns;
                    int colWidth = TextRenderer.MeasureText(ccnt[1].Text, lstv.Font).Width + 10;
                    if (colWidth > ccnt[1].Width)
                    {
                        ccnt[1].Width = colWidth;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (uFR_Selected)
                {
                    uFCoder.s_block_deselect(100);
                    uFR_Selected = false;
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void cbObjType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbObjType.SelectedIndex == 2)
            {
                if (cbObjIndex.Items.Count != 12)
                {
                    objIndexComboSetItems(12);
                    cbObjIndex.SelectedIndex = 0;
                }
            }
            else if (cbObjIndex.Items.Count != 3)
            {
                objIndexComboSetItems(3);
                cbObjIndex.SelectedIndex = 0;
            }
        }

        private void btnShowCert_Click(object sender, EventArgs e)
        {
            DL_STATUS status;
            byte obj_type;
            byte obj_index;
            ListView lstv;
            X509Certificate2 cert = null;

            if (sender == btnShowRSACert)
            {
                obj_type = 0;
                lstv = lstvRSACerts;
            }
            else if (sender == btnShowECDSACert)
            {
                obj_type = 1;
                lstv = lstvECDSACerts;
            }
            else if (sender == btnShowCACert)
            {
                obj_type = 2;
                lstv = lstvCACerts;
            }
            else
                return;

            try
            {
                if (lstv.SelectedIndices.Count == 0)
                    throw new Exception("No items selected.");

                obj_index = (byte)lstv.SelectedIndices[0];

                Cursor.Current = Cursors.WaitCursor;

                if (!uFR_Opened)
                    throw new Exception(uFR_NotOpenedMessage);

                byte[] raw_cert;
                byte[] raw_id;
                UInt16 cert_size;

                // Open JCApp:
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

                status = uFCoder.JCAppGetObjId(obj_type, obj_index, out raw_id, out cert_size);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                raw_cert = new byte[cert_size];
                status = uFCoder.JCAppGetObj(obj_type, obj_index, raw_cert);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                cert = new X509Certificate2(raw_cert);
                X509Certificate2UI.DisplayCertificate(cert, this.Handle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (uFR_Selected)
                {
                    uFCoder.s_block_deselect(100);
                    uFR_Selected = false;
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnInvalidateCert_Click(object sender, EventArgs e)
        {
            DL_STATUS status;
            byte obj_type;
            byte obj_index;
            ListView lstv;

            if (sender == btnInvalidateRSACert)
            {
                obj_type = 0;
                lstv = lstvRSACerts;
            }
            else if (sender == btnInvalidateECDSACert)
            {
                obj_type = 1;
                lstv = lstvECDSACerts;
            }
            else if (sender == btnInvalidateCACert)
            {
                obj_type = 2;
                lstv = lstvCACerts;
            }
            else
                return;

            try
            {
                if (lstv.SelectedIndices.Count == 0)
                    throw new Exception("No items selected.");

                obj_index = (byte)lstv.SelectedIndices[0];

                Cursor.Current = Cursors.WaitCursor;

                if (!uFR_Opened)
                    throw new Exception(uFR_NotOpenedMessage);

                // Open JCApp:
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
                status = uFCoder.JCAppLogin(true, tbSOPin.Text);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));
#endif

                status = uFCoder.JCAppInvalidateCert(obj_type, obj_index);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                if (uFR_Selected)
                {
                    uFR_Selected = false;
                    uFCoder.s_block_deselect(100);
                }

                btnRefresh_Click(btnRefresh, new EventArgs());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (uFR_Selected)
                {
                    uFCoder.s_block_deselect(100);
                    uFR_Selected = false;
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabCardObjects)
                btnRefresh_Click(btnRefresh, new EventArgs());
            else if (e.TabPage == tabPinCodes)
                btnRefreshTriesRemaining_Click(btnRefreshTriesRemaining, new EventArgs());
        }

        private void btnRefreshTriesRemaining_Click(object sender, EventArgs e)
        {
            DL_STATUS status = DL_STATUS.UFR_OK;
            UInt16 PukSOTriesRemaining, PukTriesRemaining, PinSOTriesRemaining, PinTriesRemaining;

            lbPukSOTriesRemaining.Text = "Tries remaining: ?";
            lbPukTriesRemaining.Text = "Tries remaining: ?";
            lbPinSOTriesRemaining.Text = "Tries remaining: ?";
            lbPinTriesRemaining.Text = "Tries remaining: ?";
            this.Refresh();

            try
            {
                if (!uFR_Opened)
                    throw new Exception(uFR_NotOpenedMessage);

                // Open JCApp:
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

                status = uFCoder.JCAppGetPinTriesRemaining(DL_SECURE_CODE.SO_PUK, out PukSOTriesRemaining);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                status = uFCoder.JCAppGetPinTriesRemaining(DL_SECURE_CODE.USER_PUK, out PukTriesRemaining);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                status = uFCoder.JCAppGetPinTriesRemaining(DL_SECURE_CODE.SO_PIN, out PinSOTriesRemaining);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                status = uFCoder.JCAppGetPinTriesRemaining(DL_SECURE_CODE.USER_PIN, out PinTriesRemaining);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                lbPukSOTriesRemaining.Text = "Tries remaining: " + PukSOTriesRemaining;
                lbPukTriesRemaining.Text = "Tries remaining: " + PukTriesRemaining;
                lbPinSOTriesRemaining.Text = "Tries remaining: " + PinSOTriesRemaining;
                lbPinTriesRemaining.Text = "Tries remaining: " + PinTriesRemaining;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (uFR_Selected)
                {
                    uFCoder.s_block_deselect(100);
                    uFR_Selected = false;
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnSOLogin_Click(object sender, EventArgs e)
        {
#if USING_PIN
            UInt16 PinTriesRemaining;
            DL_STATUS status = DL_STATUS.UFR_OK;

            try
            {
                // Open JCApp:
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

                status = uFCoder.JCAppLogin(true, tbSOPin.Text);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                mSOPinLoggedIn = true;

                status = uFCoder.JCAppGetPinTriesRemaining(DL_SECURE_CODE.SO_PIN, out PinTriesRemaining);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));
                lbPinSOTriesRemaining.Text = "Tries remaining: " + PinTriesRemaining;

                btnSOLogout.Enabled = true;
                MessageBox.Show("The SO has been successfully logged in", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                mSOPinLoggedIn = false;
                btnSOLogout.Enabled = false;
                tbSOPin.Text = "";
                if (((int)status & 0xFFFFC0) == 0x0A63C0)
                    lbPinSOTriesRemaining.Text = "Tries remaining: " + ((int)status & 0x3F);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (uFR_Selected)
                {
                    uFCoder.s_block_deselect(100);
                    uFR_Selected = false;
                }
                Cursor.Current = Cursors.Default;
            }
#endif
        }

        private void btnUserLogin_Click(object sender, EventArgs e)
        {
#if USING_PIN
            UInt16 PinTriesRemaining;
            DL_STATUS status = DL_STATUS.UFR_OK;

            try
            {
                // Open JCApp:
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

                status = uFCoder.JCAppLogin(false, tbPin.Text);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                mUserPinLoggedIn = true;
                status = uFCoder.JCAppGetPinTriesRemaining(DL_SECURE_CODE.USER_PIN, out PinTriesRemaining);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));
                lbPinTriesRemaining.Text = "Tries remaining: " + PinTriesRemaining;

                btnUserLogout.Enabled = true;
                MessageBox.Show("The user has been successfully logged in", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                mUserPinLoggedIn = false;
                tbPin.Text = "";
                btnUserLogout.Enabled = false;
                if (((int)status & 0xFFFFC0) == 0x0A63C0)
                    lbPinTriesRemaining.Text = "Tries remaining: " + ((int)status & 0x3F);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (uFR_Selected)
                {
                    uFCoder.s_block_deselect(100);
                    uFR_Selected = false;
                }
                Cursor.Current = Cursors.Default;
            }
#endif
        }

        private void btnUnblockUserPin_Click(object sender, EventArgs e)
        {
            UInt16 PinTriesRemaining;
            DL_STATUS status = DL_STATUS.UFR_OK;

            try
            {
                // Open JCApp:
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

                status = uFCoder.JCAppPinUnblock(false, tbPuk.Text);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                status = uFCoder.JCAppGetPinTriesRemaining(DL_SECURE_CODE.USER_PUK, out PinTriesRemaining);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));
                lbPukTriesRemaining.Text = "Tries remaining: " + PinTriesRemaining;

                status = uFCoder.JCAppGetPinTriesRemaining(DL_SECURE_CODE.USER_PIN, out PinTriesRemaining);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));
                lbPinTriesRemaining.Text = "Tries remaining: " + PinTriesRemaining;

                MessageBox.Show("The user PIN has been successfully unblocked", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                if (((int)status & 0xFFFFC0) == 0x0A63C0)
                    lbPukTriesRemaining.Text = "Tries remaining: " + ((int)status & 0x3F);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (uFR_Selected)
                {
                    uFCoder.s_block_deselect(100);
                    uFR_Selected = false;
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnUnblockSOPin_Click(object sender, EventArgs e)
        {
            UInt16 PinTriesRemaining;
            DL_STATUS status = DL_STATUS.UFR_OK;

            try
            {
                // Open JCApp:
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

                status = uFCoder.JCAppPinUnblock(true, tbSOPuk.Text);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                status = uFCoder.JCAppGetPinTriesRemaining(DL_SECURE_CODE.SO_PUK, out PinTriesRemaining);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));
                lbPukSOTriesRemaining.Text = "Tries remaining: " + PinTriesRemaining;

                status = uFCoder.JCAppGetPinTriesRemaining(DL_SECURE_CODE.SO_PIN, out PinTriesRemaining);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));
                lbPinSOTriesRemaining.Text = "Tries remaining: " + PinTriesRemaining;

                MessageBox.Show("The SO PIN has been successfully unblocked", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (((int)status & 0xFFFFC0) == 0x0A63C0)
                    lbPukSOTriesRemaining.Text = "Tries remaining: " + ((int)status & 0x3F);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (uFR_Selected)
                {
                    uFCoder.s_block_deselect(100);
                    uFR_Selected = false;
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void tbSOPin_TextChanged(object sender, EventArgs e)
        {
            btnSOLogin.Enabled = tbSOPin.Text.Length >= uFCoder.JCAPP_MIN_PIN_LENGTH;
        }

        private void tbPin_TextChanged(object sender, EventArgs e)
        {
            btnUserLogin.Enabled = tbPin.Text.Length >= uFCoder.JCAPP_MIN_PIN_LENGTH;
        }

        private void tbSOPuk_TextChanged(object sender, EventArgs e)
        {
            btnUnblockSOPin.Enabled = tbSOPuk.Text.Length == uFCoder.JCAPP_PUK_LENGTH;
        }

        private void tbPuk_TextChanged(object sender, EventArgs e)
        {
            btnUnblockUserPin.Enabled = tbPuk.Text.Length == uFCoder.JCAPP_PUK_LENGTH;
        }

        private void tbNewSOPin_TextChanged(object sender, EventArgs e)
        {
            btnChangeSOPin.Enabled = (tbNewSOPin.Text.Length >= uFCoder.JCAPP_MIN_PIN_LENGTH) && tbNewSOPin.Text.Equals(tbNewSOPinAgain.Text);
        }

        private void tbNewPin_TextChanged(object sender, EventArgs e)
        {
            btnChangeUserPin.Enabled = (tbNewPin.Text.Length >= uFCoder.JCAPP_MIN_PIN_LENGTH) && tbNewPin.Text.Equals(tbNewPinAgain.Text);
        }

        private void tbNewSOPuk_TextChanged(object sender, EventArgs e)
        {
            btnChangeSOPuk.Enabled = (tbNewSOPuk.Text.Length == uFCoder.JCAPP_PUK_LENGTH) && tbNewSOPuk.Text.Equals(tbNewSOPukAgain.Text);
        }

        private void tbNewPuk_TextChanged(object sender, EventArgs e)
        {
            btnChangePuk.Enabled = (tbNewPuk.Text.Length == uFCoder.JCAPP_PUK_LENGTH) && tbNewPuk.Text.Equals(tbNewPukAgain.Text);
        }

        private void cbCipher_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*lbECDSASignatureAttention.Visible = cbCipher.Text.Equals("ECDSA");
            chkDigestOffCard.Checked = chkDigestOffCard.Enabled = !cbCipher.Text.Equals("ECDSA") && !cbDigest.Text.Equals("None");
            */
            chkDigestOffCard.Checked = chkDigestOffCard.Enabled = !cbDigest.Text.Equals("None");
        }

        private void cbDigest_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*if (cbCipher.Text.Equals("ECDSA") && cbDigest.Text.Equals("None"))
                chkDigestOffCard.Checked = chkDigestOffCard.Enabled = !cbCipher.Text.Equals("ECDSA") && !cbDigest.Text.Equals("None");
            else if (!cbCipher.Text.Equals("ECDSA"))
                chkDigestOffCard.Checked = chkDigestOffCard.Enabled = !cbDigest.Text.Equals("None");
            */
            chkDigestOffCard.Checked = chkDigestOffCard.Enabled = !cbDigest.Text.Equals("None");
        }

        private void btnGetECPublicKey_Click(object sender, EventArgs e)
        {
            DL_STATUS status = DL_STATUS.UFR_OK;
            byte key_index = Convert.ToByte(cbECKeyIndex.Text);
            UInt16 k, key_size_bits, key_designator;
            byte[] keyW, field, a, b, g, r;

            try
            {
                if (!uFR_Opened)
                    throw new Exception(uFR_NotOpenedMessage);
                Cursor.Current = Cursors.WaitCursor;

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

                status = uFCoder.JCAppGetEcPublicKey(key_index, out keyW, out field, out a, out b, out g, out r, out k, out key_size_bits, out key_designator);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                tbECPubKey.Text = Hex.ToHexString(keyW);
                tbECParamPrime.Text = Hex.ToHexString(field);
                tbECParamA.Text = Hex.ToHexString(a);
                tbECParamB.Text = Hex.ToHexString(b);
                tbECParamG.Text = Hex.ToHexString(g);
                tbECParamR.Text = Hex.ToHexString(r);
                tbECParamK.Text = k.ToString();

                string s = ECCurveDesignator2Name(key_designator);
                int i = cbECName.Items.IndexOf(s);
                if (i > -1)
                {
                    m_gate_populateECDomainParameters = mECNameCurrComboIndex != i;
                    cbECName.SelectedItem = s;
                }
                else
                {
                    cbECName.SelectedIndex = -1;
                    cbECKeyLength.SelectedIndex = -1;
                    cbECName.BackColor = cbECKeyLength.BackColor = Color.MistyRose;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (uFR_Selected)
                {
                    uFCoder.s_block_deselect(100);
                    uFR_Selected = false;
                }
                Cursor.Current = Cursors.Default;
            }
        }

        public DerObjectIdentifier ECCurveDesignator2Oid(UInt16 ec_key_designator)
        {
            return SecNamedCurves.GetOid(ECCurveDesignator2Name(ec_key_designator));
        }

        public string ECCurveDesignator2Name(UInt16 ec_key_designator)
        {
            string ret_val = "";

            if ((ec_key_designator & 0x20) == 0x20) // F2m krive
            {
                ret_val = "sect";
                switch (ec_key_designator & 0x0F)
                {
                    case 0:
                        ret_val += "113";
                        break;
                    case 1:
                        ret_val += "131";
                        break;
                    case 2:
                        ret_val += "163";
                        break;
                    case 3:
                        ret_val += "193";
                        break;
                    case 4:
                        ret_val += "233";
                        break;
                    case 5:
                        ret_val += "239";
                        break;
                    case 6:
                        ret_val += "283";
                        break;
                    case 7:
                        ret_val += "409";
                        break;
                    case 8:
                        ret_val += "571";
                        break;
                    default:
                        return "";
                }
            }
            else
            {
                ret_val = "secp";
                switch (ec_key_designator & 0x0F) // // Fp kriva
                {
                    case 0:
                        ret_val += "112";
                        break;
                    case 1:
                        ret_val += "128";
                        break;
                    case 2:
                        ret_val += "160";
                        break;
                    case 3:
                        ret_val += "192";
                        break;
                    case 4:
                        ret_val += "224";
                        break;
                    case 5:
                        ret_val += "256";
                        break;
                    case 6:
                        ret_val += "384";
                        break;
                    case 7:
                        ret_val += "521";
                        break;
                    default:
                        return "";
                }
            }

            switch (ec_key_designator >> 6)
            {
                case 0:
                    ret_val += "k1";
                    break;
                case 1:
                    ret_val += "r1";
                    break;
                case 2:
                    ret_val += "r2";
                    break;
                //case 3:
                //    break;
                default:
                    return "";
            }

            return ret_val;
        }

        private void btnRsaCsr_Click(object sender, EventArgs e)
        {
            try
            {
                byte key_idx = Convert.ToByte(cbRSAKeyIndex.Text);

                AsymmetricKeyParameter pub_key = null;
                if (!tbRSAPubExp.Text.Equals(""))
                    pub_key = new RsaKeyParameters(false, new BigInteger(1, Hex.Decode(tbRSAModulus.Text)),
                            new BigInteger(1, Hex.Decode(tbRSAPubExp.Text)));

                AsymmetricKeyParameter priv_key = null;
                if (!tbRSAPrivExp.Text.Equals(""))
                    priv_key = new RsaKeyParameters(true, new BigInteger(1, Hex.Decode(tbRSAModulus.Text)),
                            new BigInteger(1, Hex.Decode(tbRSAPrivExp.Text)));

                frmCsr.setParameters(pub_key, priv_key, key_idx, "RSA", mUserPinLoggedIn, tbPin.Text);
                frmCsr.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEcdsaCsr_Click(object sender, EventArgs e)
        {
            try
            {
                byte key_idx = Convert.ToByte(cbECKeyIndex.Text);

                X9ECParameters curve = SecNamedCurves.GetByName(cbECName.Text);

                AsymmetricKeyParameter pub_key = null;
                if (!tbECPubKey.Text.Equals(""))
                    pub_key = new ECPublicKeyParameters("ECDSA", curve.Curve.DecodePoint(Hex.Decode(tbECPubKey.Text)), SecNamedCurves.GetOid(cbECName.Text));
                //X9ObjectIdentifiers.Prime256v1 same as SecObjectIdentifiers.SecP256r1

                AsymmetricKeyParameter priv_key = null;
                if (!tbECPrivKey.Text.Equals(""))
                    priv_key = new ECPrivateKeyParameters("ECDSA", new BigInteger(1, Hex.Decode(tbECPrivKey.Text)), SecNamedCurves.GetOid(cbECName.Text));

                frmCsr.setParameters(pub_key, priv_key, key_idx, "ECDSA", mUserPinLoggedIn, tbPin.Text);
                frmCsr.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSOLogout_Click(object sender, EventArgs e)
        {
#if USING_PIN
            mSOPinLoggedIn = false;
            tbSOPin.Text = "";
            btnSOLogout.Enabled = false;
#endif
        }

        private void btnUserLogout_Click(object sender, EventArgs e)
        {
#if USING_PIN
            mUserPinLoggedIn = false;
            tbPin.Text = "";
            btnUserLogout.Enabled = false;
#endif
        }

        private void PinChange_Click(object sender, EventArgs e)
        {
#if USING_PIN
            DL_SECURE_CODE code_to_change = DL_SECURE_CODE.USER_PIN;
            TextBox tbNewPinSource = null;
            TextBox tbNewPinSourceAgain = null;
            Label lbTriesRemaining = null;
            string pin_name = "";
            UInt16 PinTriesRemaining;
            DL_STATUS status = DL_STATUS.UFR_OK;

            if (!mSOPinLoggedIn)
            {
                MessageBox.Show("To change User PIN code, first you need to be logged in as SO", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            switch (((Button)sender).Name)
            {
                case "btnChangeSOPin":
                    code_to_change = DL_SECURE_CODE.SO_PIN;
                    tbNewPinSource = tbNewSOPin;
                    tbNewPinSourceAgain = tbNewSOPinAgain;
                    lbTriesRemaining = lbPinSOTriesRemaining;
                    pin_name = "SO PIN";
                    break;
                case "btnChangeSOPuk":
                    code_to_change = DL_SECURE_CODE.SO_PUK;
                    tbNewPinSource = tbNewSOPuk;
                    tbNewPinSourceAgain = tbNewSOPukAgain;
                    lbTriesRemaining = lbPukSOTriesRemaining;
                    pin_name = "SO PUK";
                    break;
                case "btnChangeUserPin":
                    code_to_change = DL_SECURE_CODE.USER_PIN;
                    tbNewPinSource = tbNewPin;
                    tbNewPinSourceAgain = tbNewPinAgain;
                    lbTriesRemaining = lbPinTriesRemaining;
                    pin_name = "user PIN";
                    break; 
                case "btnChangePuk":
                    code_to_change = DL_SECURE_CODE.USER_PUK;
                    tbNewPinSource = tbNewPuk;
                    tbNewPinSourceAgain = tbNewPukAgain;
                    lbTriesRemaining = lbPukTriesRemaining;
                    pin_name = "user PUK";
                    break;
            }

            try
            {
                // Open JCApp:
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

                status = uFCoder.JCAppLogin(true, tbSOPin.Text);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                status = uFCoder.JCAppPinChange(code_to_change, tbNewPinSource.Text);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                status = uFCoder.JCAppGetPinTriesRemaining(code_to_change, out PinTriesRemaining);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));
                lbTriesRemaining.Text = "Tries remaining: " + PinTriesRemaining;

                MessageBox.Show("The " + pin_name + " has been successfully changed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (((int)status & 0xFFFFC0) == 0x0A63C0)
                    lbPinSOTriesRemaining.Text = "Tries remaining: " + ((int)status & 0x3F);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (uFR_Selected)
                {
                    uFCoder.s_block_deselect(100);
                    uFR_Selected = false;
                }
                tbNewPinSource.Text = "";
                tbNewPinSourceAgain.Text = "";
                Cursor.Current = Cursors.Default;
            }
#endif
        }

#if MY_DEBUG
        const int INPUT_FILE_BUFFER_LEN = 8192;
        int mECKeyByteSize;
        bool isECDSACipher = false;
        ISigner mSigner;
        byte[] mMessage;

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            Int64 temp = 0, percentSize;

            try
            {
                ((BackgroundWorker)sender).ReportProgress(0);

                using (Stream source = File.OpenRead(tbMessage.Text))
                {
                    percentSize = source.Length / 1000;
                    int bytesRead;
                    while ((bytesRead = source.Read(mMessage, 0, mMessage.Length)) > 0)
                    {
                        mSigner.BlockUpdate(mMessage, 0, bytesRead);
                        temp += bytesRead;
                        if (temp >= percentSize)
                        {
                            temp = 0;
                            // when using PerformStep() the percentProgress arg is redundant
                            ((BackgroundWorker)sender).ReportProgress(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbSigning.PerformStep();
        }

        void bgw_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                pbSigning.Value = pbSigning.Maximum;
                pbSigning.Value = pbSigning.Maximum - 1; // workaround for animation feature (aero theme)
                pbSigning.Value = pbSigning.Maximum;

                byte[] result = mSigner.GenerateSignature();

                tbSignature.Text = "";
                tbSignatureRadixChanged(rbMessageHex, new EventArgs());
                rbSignatureHex.Checked = true;

                if (isECDSACipher)
                {
                    Asn1InputStream decoder = new Asn1InputStream(new MemoryStream(result));
                    DerSequence seq = (DerSequence)decoder.ReadObject();
                    DerInteger der_r = (DerInteger)seq[0];
                    DerInteger der_s = (DerInteger)seq[1];
                    byte[] r = der_r.PositiveValue.ToByteArray();
                    byte[] s = der_s.PositiveValue.ToByteArray();

                    // Fix leading zeros (padding or removing depending on mECKeyByteSize:
                    while (r[0] == 0 && r.Length > mECKeyByteSize)
                    {
                        r = r.Skip(1).ToArray();
                    }
                    if (mECKeyByteSize > r.Length)
                        r = zeroPadArray(r, 0, mECKeyByteSize - r.Length);

                    while (s[0] == 0 && s.Length > mECKeyByteSize)
                    {
                        s = s.Skip(1).ToArray();
                    }
                    if (mECKeyByteSize > s.Length)
                        s = zeroPadArray(s, 0, mECKeyByteSize - s.Length);

                    tbSignature.Text = BitConverter.ToString(r).Replace("-", "") + BitConverter.ToString(s).Replace("-", "");
                }
                else
                    tbSignature.Text = BitConverter.ToString(result).Replace("-", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Enable controls critical for execution:
                tabControl.Enabled = true;
            }
        }

        private void debugSignature()
        {
            string signatureMechanism;
            AsymmetricKeyParameter key;

            try
            {
                if (cbCipher.Text.Equals("RSA"))
                {
                    isECDSACipher = false;
                    key = new RsaPrivateCrtKeyParameters(
                        new BigInteger(1, Hex.Decode(tbRSAModulus.Text)),
                        new BigInteger(1, Hex.Decode(tbRSAPubExp.Text)),
                        new BigInteger(1, Hex.Decode(tbRSAPrivExp.Text)),
                        new BigInteger(1, Hex.Decode(tbRSAPrivP.Text)),
                        new BigInteger(1, Hex.Decode(tbRSAPrivQ.Text)),
                        new BigInteger(1, Hex.Decode(tbRSAPrivDP1.Text)),
                        new BigInteger(1, Hex.Decode(tbRSAPrivDQ1.Text)),
                        new BigInteger(1, Hex.Decode(tbRSAPrivPQ.Text))
                        );

                    switch (cbDigest.Text)
                    {
                        case "SHA-1":
                            signatureMechanism = "SHA-1withRSA";
                            break;
                        case "SHA-224":
                            signatureMechanism = "SHA-224withRSA";
                            break;
                        case "SHA-256":
                            signatureMechanism = "SHA-256withRSA";
                            break;
                        case "SHA-384":
                            signatureMechanism = "SHA-384withRSA";
                            if (((RsaPrivateCrtKeyParameters)key).Modulus.BitLength < 736)
                                throw new Exception("RSA key bit length not matched with SHA-384.\r\n(use longer RSA key)");
                            break;
                        case "SHA-512":
                            if (((RsaPrivateCrtKeyParameters)key).Modulus.BitLength < 768)
                                throw new Exception("RSA key bit length not matched with SHA-512.\r\n(use longer RSA key)");
                            signatureMechanism = "SHA-512withRSA";
                            break;
                        default:
                            throw new Exception("Unknown hash algorithm");
                    }
                }
                else if (cbCipher.Text.Equals("ECDSA"))
                {
                    isECDSACipher = true;
                    switch (cbDigest.Text)
                    {
                        case "SHA-1":
                            signatureMechanism = "SHA-1withECDSA";
                            break;
                        case "SHA-224":
                            signatureMechanism = "SHA-224withECDSA";
                            break;
                        case "SHA-256":
                            signatureMechanism = "SHA-256withECDSA";
                            break;
                        case "SHA-384":
                            signatureMechanism = "SHA-384withECDSA";
                            break;
                        case "SHA-512":
                            signatureMechanism = "SHA-512withECDSA";
                            break;
                        default:
                            throw new Exception("Unknown digest algorithm");
                    }

                    X9ECParameters curve = SecNamedCurves.GetByName(cbECName.Text);
                    ECDomainParameters curveSpec = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H, curve.GetSeed());
                    key = new ECPrivateKeyParameters("ECDSA", new BigInteger(1, Hex.Decode(tbECPrivKey.Text)), curveSpec);
                    mECKeyByteSize = (curve.Curve.FieldSize + 7) / 8;
                }
                else
                    throw new Exception("Unknown cipher algorithm");

                mSigner = SignerUtilities.GetSigner(signatureMechanism);
                mSigner.Init(true, key);

                switch (mMessageRadix) // from radix
                {
                    case RADIX.Hex:
                        mMessage = Hex.Decode(tbMessage.Text);
                        break;
                    case RADIX.Base64:
                        mMessage = Convert.FromBase64String(tbMessage.Text);
                        break;
                    case RADIX.ASCII:
                        mMessage = Encoding.ASCII.GetBytes(tbMessage.Text);
                        break;
                    case RADIX.Exception_FromFile:
                        mMessage = new byte[INPUT_FILE_BUFFER_LEN];
                        break;
                    default:
                        throw new Exception("Unknown input data radix");
                }

                pbSigning.Value = 0;
                Refresh();

                if (mMessageRadix == RADIX.Exception_FromFile)
                {
                    var bgw = new BackgroundWorker();
                    bgw.ProgressChanged += bgw_ProgressChanged;
                    bgw.DoWork += bgw_DoWork;
                    bgw.WorkerReportsProgress = true;
                    bgw.RunWorkerCompleted += bgw_WorkCompleted;
                    bgw.RunWorkerAsync();

                    // Disable controls critical for execution:
                    tabControl.Enabled = false;
                    return;
                }
                else
                    mSigner.BlockUpdate(mMessage, 0, mMessage.Length);

                byte[] result = mSigner.GenerateSignature();
                pbSigning.Value = pbSigning.Maximum;
                pbSigning.Value = pbSigning.Maximum - 1; // workaround for animation feature (aero theme)
                pbSigning.Value = pbSigning.Maximum;

                tbSignature.Text = "";
                tbSignatureRadixChanged(rbMessageHex, new EventArgs());
                rbSignatureHex.Checked = true;

                if (isECDSACipher)
                {
                    Asn1InputStream decoder = new Asn1InputStream(new MemoryStream(result));
                    DerSequence seq = (DerSequence)decoder.ReadObject();
                    DerInteger der_r = (DerInteger)seq[0];
                    DerInteger der_s = (DerInteger)seq[1];
                    byte[] r = der_r.PositiveValue.ToByteArray();
                    byte[] s = der_s.PositiveValue.ToByteArray();

                    // Fix leading zeros (padding or removing depending on mECKeyByteSize:
                    while (r[0] == 0 && r.Length > mECKeyByteSize)
                    {
                        r = r.Skip(1).ToArray();
                    }
                    if (mECKeyByteSize > r.Length)
                        r = zeroPadArray(r, 0, mECKeyByteSize - r.Length);

                    while (s[0] == 0 && s.Length > mECKeyByteSize)
                    {
                        s = s.Skip(1).ToArray();
                    }
                    if (mECKeyByteSize > s.Length)
                        s = zeroPadArray(s, 0, mECKeyByteSize - s.Length);

                    tbSignature.Text = BitConverter.ToString(r).Replace("-", "") + BitConverter.ToString(s).Replace("-", "");
                }
                else
                    tbSignature.Text = BitConverter.ToString(result).Replace("-", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
#endif
    }
}
