using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
                Tuple<bool, string> tup = QRCodeSaveToFile(picturebox_qrcode);
                if (tup.Item1 == true)
                {
                    MessageBox.Show("保存成功！");
                    txtbox_qrcodeurl.Text = tup.Item2;
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
    }
}