using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOTNETQRCode
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        public const int WM_CLOSE = 0x10;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //picturebox_qrcode.SizeMode = StretchImage;
            btn_openqrcodeurl.Enabled = false;
        }

        private void btn_generateqrcode_Click(object sender, EventArgs e)
        {
            if (txtbox_text.Text == "")
            {
                MessageBox.Show("文本框不能为空！");
                txtbox_text.Focus();
            }
            else
            {
                picturebox_qrcode.Image = ZxingCode.GenerateQRCode(txtbox_text.Text, 300, 300);
            }
        }

        private void btn_saveqrcode_Click(object sender, EventArgs e)
        {
            if (picturebox_qrcode.Image == null)
            {
                MessageBox.Show("图像为空不能保存！");
                btn_generateqrcode.Focus();
            }
            else
            {
                txtbox_qrcodeurl.Text = "";
                Tuple<bool, string> tup = QRCodeSaveToFile(picturebox_qrcode);
                if (tup.Item1 == true)
                {
                    MessageBox.Show("保存成功！");
                    txtbox_qrcodeurl.Text = tup.Item2;
                    btn_openqrcodeurl.Focus();
                }
                else
                {
                    MessageBox.Show("保存失败！");
                }
            }
        }

        private Tuple<bool, string> QRCodeSaveToFile(PictureBox pb)
        {
            Tuple<bool, string> tup;

            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "QRCode";
                sfd.Filter = ".png|*.png|.jpg|*.jpg";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Bitmap bit = new Bitmap(pb.Width, pb.Height);
                    pb.DrawToBitmap(bit, pb.ClientRectangle);
                    bit.Save(sfd.FileName);
                    string path = sfd.FileName.ToString();
                    tup = new Tuple<bool, string>(true, path);
                    return tup;
                }
                else
                {
                    tup = new Tuple<bool, string>(false, "");
                    return tup;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btn_openqrcodeurl_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            //psi.Arguments = "/e,/select," + FilePath;
            psi.Arguments = "/e,/select," + txtbox_qrcodeurl.Text;
            System.Diagnostics.Process.Start(psi);
        }

        private void txtbox_text_MouseMove(object sender, MouseEventArgs e)
        {
            //txtbox_text.SelectAll();
            txtbox_text.Focus();
        }

        private void txtbox_qrcodeurl_MouseMove(object sender, MouseEventArgs e)
        {
            //txtbox_qrcodeurl.SelectAll();
            txtbox_qrcodeurl.Focus();
        }

        private void btn_decodingqrcode_Click(object sender, EventArgs e)
        {
            Tuple<bool, string, string> tup = ZxingCode.DecodeQRCode(txtbox_qrcodeurl.Text);
            if (tup.Item1 == true)
            {
                txtbox_text.Text = tup.Item2;
            }
            else
            {
                MessageBox.Show(tup.Item2);
                txtbox_qrcodeurl.SelectAll();
                txtbox_qrcodeurl.Focus();
            }
        }

        private void txtbox_qrcodeurl_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtbox_qrcodeurl.Text))
            {
                btn_openqrcodeurl.Enabled = false;
            }
            else
            {
                btn_openqrcodeurl.Enabled = true;
            }
        }

        private void txtbox_text_MouseClick(object sender, MouseEventArgs e)
        {
            txtbox_text.SelectAll();
            txtbox_text.Focus();
        }

        private void txtbox_qrcodeurl_MouseClick(object sender, MouseEventArgs e)
        {
            txtbox_qrcodeurl.SelectAll();
            txtbox_qrcodeurl.Focus();
        }

        private void btn_scanscreenqrcode_Click(object sender, EventArgs e)
        {
            if (picturebox_qrcode.Image!=null)
            {
                Reset();
                MessageBox.Show("请再次点击扫描屏幕二维码按钮！");
            }
            else
            {
                Tuple<bool, string> tup = ZxingCode.ScanScreenQRCode();
                if (tup.Item1 == true)
                {
                    txtbox_text.Text = tup.Item2;
                    picturebox_qrcode.Image = ZxingCode.GenerateQRCode(tup.Item2, 300, 300);
                }
                else
                {
                    MessageBox.Show(tup.Item2);
                }
            }

            /*Reset();
            MessageBox.Show("请再次点击扫描屏幕二维码按钮！", "MessageBox");
            KillMessageBox();
            Tuple<bool, string> tup = ZxingCode.ScanScreenQRCode();
            if (tup.Item1 == true)
            {
                txtbox_text.Text = tup.Item2;
                picturebox_qrcode.Image = ZxingCode.GenerateQRCode(tup.Item2, 300, 300);
            }
            else
            {
                MessageBox.Show(tup.Item2);
            }*/
        }

        private void KillMessageBox()
        {
            //按照MessageBox的标题，找到MessageBox的窗口 
            IntPtr ptr = FindWindow(null, "MessageBox");
            if (ptr != IntPtr.Zero)
            {
                //找到则关闭MessageBox窗口 
                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
        }

        void Reset()
        {
            txtbox_text.Text = "";
            txtbox_qrcodeurl.Text = "";
            picturebox_qrcode.Image = null;
            txtbox_text.Focus();
            txtbox_qrcodeurl.Focus();
            txtbox_text.Focus();
        }

        private void picturebox_qrcode_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }
}