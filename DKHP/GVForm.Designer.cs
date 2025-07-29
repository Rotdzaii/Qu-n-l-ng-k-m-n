namespace DKHP
{
    partial class GVForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GVForm));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.TimKiemSV_DK = new System.Windows.Forms.Button();
            this.TimKiemLop_DK = new System.Windows.Forms.Button();
            this.TXTTimKiemSV_DK = new System.Windows.Forms.TextBox();
            this.TXTTimKiemLop_DK = new System.Windows.Forms.TextBox();
            this.DGVSinhVien = new System.Windows.Forms.DataGridView();
            this.DGVLopHoc = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.TxTMSGV = new System.Windows.Forms.TextBox();
            this.TxTKhoa = new System.Windows.Forms.TextBox();
            this.TxTNgaySinh = new System.Windows.Forms.TextBox();
            this.TxTHoVaTen = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TabGV = new System.Windows.Forms.TabControl();
            this.DangXuat = new System.Windows.Forms.Button();
            this.DoiMatKhau = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVSinhVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVLopHoc)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.TabGV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.MediumTurquoise;
            this.tabPage2.Controls.Add(this.TimKiemSV_DK);
            this.tabPage2.Controls.Add(this.TimKiemLop_DK);
            this.tabPage2.Controls.Add(this.TXTTimKiemSV_DK);
            this.tabPage2.Controls.Add(this.TXTTimKiemLop_DK);
            this.tabPage2.Controls.Add(this.DGVSinhVien);
            this.tabPage2.Controls.Add(this.DGVLopHoc);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(797, 428);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Danh Sách Sinh Viên Đã Đăng Ký";
            // 
            // TimKiemSV_DK
            // 
            this.TimKiemSV_DK.Location = new System.Drawing.Point(556, 25);
            this.TimKiemSV_DK.Name = "TimKiemSV_DK";
            this.TimKiemSV_DK.Size = new System.Drawing.Size(112, 23);
            this.TimKiemSV_DK.TabIndex = 5;
            this.TimKiemSV_DK.Text = "Tìm Kiếm Sinh Viên";
            this.TimKiemSV_DK.UseVisualStyleBackColor = true;
            this.TimKiemSV_DK.Click += new System.EventHandler(this.TimKiemSV_DK_Click);
            // 
            // TimKiemLop_DK
            // 
            this.TimKiemLop_DK.Location = new System.Drawing.Point(220, 25);
            this.TimKiemLop_DK.Name = "TimKiemLop_DK";
            this.TimKiemLop_DK.Size = new System.Drawing.Size(83, 23);
            this.TimKiemLop_DK.TabIndex = 4;
            this.TimKiemLop_DK.Text = "Tìm Kiếm Lớp";
            this.TimKiemLop_DK.UseVisualStyleBackColor = true;
            this.TimKiemLop_DK.Click += new System.EventHandler(this.TimKiemLop_DK_Click);
            // 
            // TXTTimKiemSV_DK
            // 
            this.TXTTimKiemSV_DK.Location = new System.Drawing.Point(431, 27);
            this.TXTTimKiemSV_DK.Name = "TXTTimKiemSV_DK";
            this.TXTTimKiemSV_DK.Size = new System.Drawing.Size(119, 20);
            this.TXTTimKiemSV_DK.TabIndex = 3;
            // 
            // TXTTimKiemLop_DK
            // 
            this.TXTTimKiemLop_DK.Location = new System.Drawing.Point(95, 27);
            this.TXTTimKiemLop_DK.Name = "TXTTimKiemLop_DK";
            this.TXTTimKiemLop_DK.Size = new System.Drawing.Size(119, 20);
            this.TXTTimKiemLop_DK.TabIndex = 2;
            // 
            // DGVSinhVien
            // 
            this.DGVSinhVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVSinhVien.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DGVSinhVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVSinhVien.Location = new System.Drawing.Point(53, 278);
            this.DGVSinhVien.Name = "DGVSinhVien";
            this.DGVSinhVien.Size = new System.Drawing.Size(715, 127);
            this.DGVSinhVien.TabIndex = 1;
            // 
            // DGVLopHoc
            // 
            this.DGVLopHoc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVLopHoc.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DGVLopHoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVLopHoc.Location = new System.Drawing.Point(53, 79);
            this.DGVLopHoc.Name = "DGVLopHoc";
            this.DGVLopHoc.Size = new System.Drawing.Size(715, 153);
            this.DGVLopHoc.TabIndex = 0;
            this.DGVLopHoc.SelectionChanged += new System.EventHandler(this.DGVLopHoc_SelectionChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.MediumTurquoise;
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.TxTMSGV);
            this.tabPage1.Controls.Add(this.TxTKhoa);
            this.tabPage1.Controls.Add(this.TxTNgaySinh);
            this.tabPage1.Controls.Add(this.TxTHoVaTen);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(797, 428);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Thông Tin Cá Nhân";
            // 
            // TxTMSGV
            // 
            this.TxTMSGV.Location = new System.Drawing.Point(216, 176);
            this.TxTMSGV.Name = "TxTMSGV";
            this.TxTMSGV.Size = new System.Drawing.Size(119, 20);
            this.TxTMSGV.TabIndex = 0;
            // 
            // TxTKhoa
            // 
            this.TxTKhoa.Location = new System.Drawing.Point(529, 176);
            this.TxTKhoa.Name = "TxTKhoa";
            this.TxTKhoa.Size = new System.Drawing.Size(119, 20);
            this.TxTKhoa.TabIndex = 6;
            // 
            // TxTNgaySinh
            // 
            this.TxTNgaySinh.Location = new System.Drawing.Point(529, 223);
            this.TxTNgaySinh.Name = "TxTNgaySinh";
            this.TxTNgaySinh.Size = new System.Drawing.Size(119, 20);
            this.TxTNgaySinh.TabIndex = 5;
            // 
            // TxTHoVaTen
            // 
            this.TxTHoVaTen.Location = new System.Drawing.Point(216, 223);
            this.TxTHoVaTen.Name = "TxTHoVaTen";
            this.TxTHoVaTen.Size = new System.Drawing.Size(119, 20);
            this.TxTHoVaTen.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(479, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Khoa:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(98, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mã Giảng viên:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(98, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Họ Và Tên:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(447, 224);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Ngày Sinh:";
            // 
            // TabGV
            // 
            this.TabGV.Controls.Add(this.tabPage1);
            this.TabGV.Controls.Add(this.tabPage2);
            this.TabGV.Location = new System.Drawing.Point(-1, -1);
            this.TabGV.Name = "TabGV";
            this.TabGV.SelectedIndex = 0;
            this.TabGV.Size = new System.Drawing.Size(805, 454);
            this.TabGV.TabIndex = 8;
            // 
            // DangXuat
            // 
            this.DangXuat.Location = new System.Drawing.Point(726, -1);
            this.DangXuat.Name = "DangXuat";
            this.DangXuat.Size = new System.Drawing.Size(75, 23);
            this.DangXuat.TabIndex = 9;
            this.DangXuat.Text = "Đăng Xuất";
            this.DangXuat.UseVisualStyleBackColor = true;
            this.DangXuat.Click += new System.EventHandler(this.DangXuat_Click);
            // 
            // DoiMatKhau
            // 
            this.DoiMatKhau.Location = new System.Drawing.Point(640, -1);
            this.DoiMatKhau.Name = "DoiMatKhau";
            this.DoiMatKhau.Size = new System.Drawing.Size(80, 23);
            this.DoiMatKhau.TabIndex = 8;
            this.DoiMatKhau.Text = "Đổi Mật Khẩu";
            this.DoiMatKhau.UseVisualStyleBackColor = true;
            this.DoiMatKhau.Click += new System.EventHandler(this.DoiMatKhau_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(307, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 132);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(429, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(0, 0);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // GVForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DoiMatKhau);
            this.Controls.Add(this.DangXuat);
            this.Controls.Add(this.TabGV);
            this.Name = "GVForm";
            this.Text = "GVForm";
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVSinhVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVLopHoc)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.TabGV.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox TxTMSGV;
        private System.Windows.Forms.TextBox TxTKhoa;
        private System.Windows.Forms.TextBox TxTNgaySinh;
        private System.Windows.Forms.TextBox TxTHoVaTen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl TabGV;
        private System.Windows.Forms.DataGridView DGVSinhVien;
        private System.Windows.Forms.DataGridView DGVLopHoc;
        private System.Windows.Forms.Button DangXuat;
        private System.Windows.Forms.Button DoiMatKhau;
        private System.Windows.Forms.Button TimKiemSV_DK;
        private System.Windows.Forms.Button TimKiemLop_DK;
        private System.Windows.Forms.TextBox TXTTimKiemSV_DK;
        private System.Windows.Forms.TextBox TXTTimKiemLop_DK;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}