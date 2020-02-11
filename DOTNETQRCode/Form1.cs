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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //picturebox_qrcode.SizeMode = StretchImage;
            btn_openqrcodeurl.Enabled = false;

            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;

            //弹气泡/通知框提示
            this.notifyIcon1.ShowBalloonTip(20, "最小化", "可右键任务栏图标进行快捷操作", ToolTipIcon.Info);
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
                //picturebox_qrcode.Image = ZxingCode.GenerateBarcode(txtbox_text.Text, 300, 300);
                picturebox_qrcode.Image = ZxingCode.GenerateQRCode(txtbox_text.Text, 300, 300);
                //picturebox_qrcode.Image = ZxingCode.GenerateQRCodeWithLOGO(txtbox_text.Text, 300, 300);
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
            Reset();

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

        void Reset()
        {
            txtbox_text.Text = "";
            txtbox_text.Refresh();
            txtbox_qrcodeurl.Text = "";
            txtbox_qrcodeurl.Refresh();
            picturebox_qrcode.Image = null;
            //强制刷新，不然要等调用该方法那块都执行完了才会刷新，MessageBox.Show()也有效
            picturebox_qrcode.Refresh();
            txtbox_text.Focus();
        }

        private void picturebox_qrcode_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void 显示主界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*if (this.Visible)
            {
                this.Hide();
            }
            else
            {
                this.Show();
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            }*/
            this.Show();
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
        }

        private void 扫描屏幕二维码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tuple<bool, string> tup = ZxingCode.ScanScreenQRCode();
            if (tup.Item1 == true)
            {
                txtbox_text.Text = tup.Item2;
                picturebox_qrcode.Image = ZxingCode.GenerateQRCode(tup.Item2, 300, 300);
                this.Show();
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            }
            else
            {
                MessageBox.Show(tup.Item2);
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            }
        }

        protected override void WndProc(ref Message msg)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;

            if (msg.Msg == WM_SYSCOMMAND && ((int)msg.WParam == SC_CLOSE))
            {
                // 点击winform右上关闭按钮 
                // 加入想要的逻辑处理
                this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                this.Hide();
                return;//阻止了窗体关闭
            }
            base.WndProc(ref msg);
        }
    }
}