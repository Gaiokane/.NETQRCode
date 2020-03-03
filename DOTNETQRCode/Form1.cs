using Microsoft.Win32;
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

            //MessageBox.Show(IsAutoRun().ToString());
            if (IsAutoRun() == true)
            {
                开机自启动ToolStripMenuItem.Checked = true;
            }
            else
            {
                开机自启动ToolStripMenuItem.Checked = false;
            }
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

            this.ShowInTaskbar = true;

            this.TopMost = true;//置顶显示
            this.TopMost = false;//取消置顶显示，不然一直会置顶
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

                this.ShowInTaskbar = false;
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;

                this.ShowInTaskbar = true;
            }
            else
            {
                this.TopMost = true;//置顶显示
                this.TopMost = false;//取消置顶显示，不然一直会置顶
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

        /// <summary>
        /// 修改程序在注册表中的键值
        /// </summary>
        /// <param name="isAuto">true:开机启动,false:不开机自启</param>
        /// 该程序的启动项设置到HKEY_Current_User 下，推荐。如果想改在HKEY_LOCAL_MACHINE，只需将CurrentUser改为LocalMachine
        /// Rkey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
        public static bool AutoStart(bool isAuto)
        {
            try
            {
                if (isAuto == true)
                {
                    //RegistryKey R_local = Registry.LocalMachine;
                    RegistryKey R_local = Registry.CurrentUser;
                    RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    R_run.SetValue("QRCodeWidget", Application.ExecutablePath);
                    R_run.Close();
                    R_local.Close();
                    return true;
                }
                else
                {
                    //RegistryKey R_local = Registry.LocalMachine;
                    RegistryKey R_local = Registry.CurrentUser;
                    RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    R_run.DeleteValue("QRCodeWidget", false);
                    R_run.Close();
                    R_local.Close();
                    return true;
                }

                //GlobalVariant.Instance.UserConfig.AutoStart = isAuto;
            }
            catch (Exception)
            {
                //MessageBoxDlg dlg = new MessageBoxDlg();
                //dlg.InitialData("您需要管理员权限修改", "提示", MessageBoxButtons.OK, MessageBoxDlgIcon.Error);
                //dlg.ShowDialog();
                MessageBox.Show("您需要管理员权限修改", "提示");
                return false;
            }
        }

        /// <summary>
        /// 判断是否开机启动
        /// </summary>
        /// <returns></returns>
        public static bool IsAutoRun()
        {
            try
            {
                RegistryKey R_local = Registry.CurrentUser;
                RegistryKey software = R_local.OpenSubKey(@"SOFTWARE");
                RegistryKey run = R_local.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\");
                object key = run.GetValue("QRCodeWidget");
                software.Close();
                run.Close();
                if (null == key || !Application.ExecutablePath.Equals(key.ToString()))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private void 开机自启动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (开机自启动ToolStripMenuItem.Checked == false)
            {
                if (AutoStart(true) == true)
                {
                    开机自启动ToolStripMenuItem.Checked = true;
                }
            }
            else
            {
                if (AutoStart(false) == true)
                {
                    开机自启动ToolStripMenuItem.Checked = false;
                }
            }
        }
    }
}