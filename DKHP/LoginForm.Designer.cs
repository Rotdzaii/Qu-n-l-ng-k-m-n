namespace DKHP
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.Mssv = new System.Windows.Forms.TextBox();
            this.Mk = new System.Windows.Forms.TextBox();
            this.TenTruong = new System.Windows.Forms.Label();
            this.ThongTin = new System.Windows.Forms.Label();
            this.buttondn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Mssv
            // 
            this.Mssv.Location = new System.Drawing.Point(302, 245);
            this.Mssv.Name = "Mssv";
            this.Mssv.Size = new System.Drawing.Size(180, 20);
            this.Mssv.TabIndex = 0;
            // 
            // Mk
            // 
            this.Mk.Location = new System.Drawing.Point(302, 293);
            this.Mk.Name = "Mk";
            this.Mk.Size = new System.Drawing.Size(180, 20);
            this.Mk.TabIndex = 1;
            this.Mk.TextChanged += new System.EventHandler(this.Mk_TextChanged);
            // 
            // TenTruong
            // 
            this.TenTruong.AutoSize = true;
            this.TenTruong.Location = new System.Drawing.Point(315, 166);
            this.TenTruong.Name = "TenTruong";
            this.TenTruong.Size = new System.Drawing.Size(158, 13);
            this.TenTruong.TabIndex = 2;
            this.TenTruong.Text = "TRƯỜNG ĐẠI HỌC VĂN LANG";
            // 
            // ThongTin
            // 
            this.ThongTin.AutoSize = true;
            this.ThongTin.Location = new System.Drawing.Point(334, 204);
            this.ThongTin.Name = "ThongTin";
            this.ThongTin.Size = new System.Drawing.Size(116, 13);
            this.ThongTin.TabIndex = 3;
            this.ThongTin.Text = "Cổng thông tin đào tạo";
            // 
            // buttondn
            // 
            this.buttondn.Location = new System.Drawing.Point(302, 342);
            this.buttondn.Name = "buttondn";
            this.buttondn.Size = new System.Drawing.Size(180, 25);
            this.buttondn.TabIndex = 4;
            this.buttondn.Text = "Đăng Nhập";
            this.buttondn.UseVisualStyleBackColor = true;
            this.buttondn.Click += new System.EventHandler(this.buttondn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(318, 36);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(151, 115);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttondn);
            this.Controls.Add(this.ThongTin);
            this.Controls.Add(this.TenTruong);
            this.Controls.Add(this.Mk);
            this.Controls.Add(this.Mssv);
            this.Name = "LoginForm";
            this.Text = "Đăng Nhập";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Mssv;
        private System.Windows.Forms.TextBox Mk;
        private System.Windows.Forms.Label TenTruong;
        private System.Windows.Forms.Label ThongTin;
        private System.Windows.Forms.Button buttondn;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

