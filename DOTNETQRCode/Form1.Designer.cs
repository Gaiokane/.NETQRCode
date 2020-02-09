namespace DOTNETQRCode
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lab_text = new System.Windows.Forms.Label();
            this.txtbox_text = new System.Windows.Forms.TextBox();
            this.txtbox_qrcodeurl = new System.Windows.Forms.TextBox();
            this.lab__qrcodeurl = new System.Windows.Forms.Label();
            this.btn_generateqrcode = new System.Windows.Forms.Button();
            this.btn_saveqrcode = new System.Windows.Forms.Button();
            this.btn_decodingqrcode = new System.Windows.Forms.Button();
            this.btn_scanscreenqrcode = new System.Windows.Forms.Button();
            this.picturebox_qrcode = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picturebox_qrcode)).BeginInit();
            this.SuspendLayout();
            // 
            // lab_text
            // 
            this.lab_text.AutoSize = true;
            this.lab_text.Location = new System.Drawing.Point(12, 15);
            this.lab_text.Name = "lab_text";
            this.lab_text.Size = new System.Drawing.Size(77, 12);
            this.lab_text.TabIndex = 0;
            this.lab_text.Text = "文      本：";
            // 
            // txtbox_text
            // 
            this.txtbox_text.Location = new System.Drawing.Point(95, 12);
            this.txtbox_text.Name = "txtbox_text";
            this.txtbox_text.Size = new System.Drawing.Size(337, 21);
            this.txtbox_text.TabIndex = 1;
            // 
            // txtbox_qrcodeurl
            // 
            this.txtbox_qrcodeurl.Location = new System.Drawing.Point(95, 39);
            this.txtbox_qrcodeurl.Name = "txtbox_qrcodeurl";
            this.txtbox_qrcodeurl.Size = new System.Drawing.Size(337, 21);
            this.txtbox_qrcodeurl.TabIndex = 3;
            // 
            // lab__qrcodeurl
            // 
            this.lab__qrcodeurl.AutoSize = true;
            this.lab__qrcodeurl.Location = new System.Drawing.Point(12, 42);
            this.lab__qrcodeurl.Name = "lab__qrcodeurl";
            this.lab__qrcodeurl.Size = new System.Drawing.Size(77, 12);
            this.lab__qrcodeurl.TabIndex = 2;
            this.lab__qrcodeurl.Text = "二维码路径：";
            // 
            // btn_generateqrcode
            // 
            this.btn_generateqrcode.Location = new System.Drawing.Point(226, 66);
            this.btn_generateqrcode.Name = "btn_generateqrcode";
            this.btn_generateqrcode.Size = new System.Drawing.Size(100, 100);
            this.btn_generateqrcode.TabIndex = 4;
            this.btn_generateqrcode.Text = "生成二维码";
            this.btn_generateqrcode.UseVisualStyleBackColor = true;
            this.btn_generateqrcode.Click += new System.EventHandler(this.btn_generateqrcode_Click);
            // 
            // btn_saveqrcode
            // 
            this.btn_saveqrcode.Location = new System.Drawing.Point(332, 66);
            this.btn_saveqrcode.Name = "btn_saveqrcode";
            this.btn_saveqrcode.Size = new System.Drawing.Size(100, 100);
            this.btn_saveqrcode.TabIndex = 5;
            this.btn_saveqrcode.Text = "保存二维码";
            this.btn_saveqrcode.UseVisualStyleBackColor = true;
            this.btn_saveqrcode.Click += new System.EventHandler(this.btn_saveqrcode_Click);
            // 
            // btn_decodingqrcode
            // 
            this.btn_decodingqrcode.Location = new System.Drawing.Point(226, 172);
            this.btn_decodingqrcode.Name = "btn_decodingqrcode";
            this.btn_decodingqrcode.Size = new System.Drawing.Size(100, 100);
            this.btn_decodingqrcode.TabIndex = 6;
            this.btn_decodingqrcode.Text = "解码";
            this.btn_decodingqrcode.UseVisualStyleBackColor = true;
            // 
            // btn_scanscreenqrcode
            // 
            this.btn_scanscreenqrcode.Location = new System.Drawing.Point(332, 172);
            this.btn_scanscreenqrcode.Name = "btn_scanscreenqrcode";
            this.btn_scanscreenqrcode.Size = new System.Drawing.Size(100, 100);
            this.btn_scanscreenqrcode.TabIndex = 7;
            this.btn_scanscreenqrcode.Text = "扫描屏幕二维码";
            this.btn_scanscreenqrcode.UseVisualStyleBackColor = true;
            // 
            // picturebox_qrcode
            // 
            this.picturebox_qrcode.Location = new System.Drawing.Point(14, 66);
            this.picturebox_qrcode.Name = "picturebox_qrcode";
            this.picturebox_qrcode.Size = new System.Drawing.Size(206, 206);
            this.picturebox_qrcode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picturebox_qrcode.TabIndex = 8;
            this.picturebox_qrcode.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 284);
            this.Controls.Add(this.picturebox_qrcode);
            this.Controls.Add(this.btn_scanscreenqrcode);
            this.Controls.Add(this.btn_decodingqrcode);
            this.Controls.Add(this.btn_saveqrcode);
            this.Controls.Add(this.btn_generateqrcode);
            this.Controls.Add(this.txtbox_qrcodeurl);
            this.Controls.Add(this.lab__qrcodeurl);
            this.Controls.Add(this.txtbox_text);
            this.Controls.Add(this.lab_text);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturebox_qrcode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lab_text;
        private System.Windows.Forms.TextBox txtbox_text;
        private System.Windows.Forms.TextBox txtbox_qrcodeurl;
        private System.Windows.Forms.Label lab__qrcodeurl;
        private System.Windows.Forms.Button btn_generateqrcode;
        private System.Windows.Forms.Button btn_saveqrcode;
        private System.Windows.Forms.Button btn_decodingqrcode;
        private System.Windows.Forms.Button btn_scanscreenqrcode;
        private System.Windows.Forms.PictureBox picturebox_qrcode;
    }
}

