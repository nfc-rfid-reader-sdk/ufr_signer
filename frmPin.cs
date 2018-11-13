using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Windows.Forms;
using uFR;

namespace uFRSigner
{
    public partial class frmUserPin : Form
    {
        private string mUserPin = "";

        public string getPin()
        {
            return mUserPin;
        }

        public frmUserPin()
        {
            InitializeComponent();
        }

        public string getUserPin()
        {
            return tbPin.Text;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
#if USING_PIN
            bool uFR_Selected = false;
            DL_STATUS status = DL_STATUS.UFR_OK;

            try
            {
                /*/ Open JCApp:
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
                //*/

                // uFR have to be in 14443_4_Mode and JCApp applet have to be selected:
                status = uFCoder.JCAppLogin(false, tbPin.Text);
                if (status != DL_STATUS.UFR_OK)
                    throw new Exception(string.Format("Card error code: 0x{0:X}", status));

                mUserPin = tbPin.Text;
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                string message = ex.Message;

                mUserPin = "";
                if (((int)status & 0x0A63C0) == 0x0A63C0)
                    message = "Wrong user PIN code. Tries remaining: " + ((int)status & 0x3F);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                /*/
                if (uFR_Selected)
                {
                    uFCoder.s_block_deselect(100);
                    uFR_Selected = false;
                }
                //*/
                Cursor.Current = Cursors.Default;
            }
#endif
        }
    }
}
