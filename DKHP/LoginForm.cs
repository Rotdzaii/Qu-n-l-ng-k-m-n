using DKHP.data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DKHP
{
    public partial class LoginForm : Form
    {
        private FushiEntities db = new FushiEntities();
        public LoginForm()
        {
            InitializeComponent();
            Mk.UseSystemPasswordChar = true;
        }

        private void buttondn_Click(object sender, EventArgs e)
        {
            string userId = Mssv.Text.Trim();
            string password = Mk.Text.Trim();

            // Kiểm tra SinhVien
            var sv = db.SinhViens.FirstOrDefault(x => x.MaSV == userId && x.MatKhau == password);
            if (sv != null)
            {
                // Mở form cho sinh viên
                HSForm f = new HSForm(sv);
                f.Show();
                this.Hide();
                return;
            }

            // Kiểm tra GiangVien
            var gv = db.GiangViens.FirstOrDefault(x => x.MaGV == userId && x.MatKhau == password);
            if (gv != null)
            {
                // Mở form cho giảng viên
                GVForm f = new GVForm(gv);
                f.Show();
                this.Hide();
                return;
            }

            // Kiểm tra Admin
            var ad = db.Admins.FirstOrDefault(x => x.MaAdmin == userId && x.MatKhau == password);
            if (ad != null)
            {
                // Mở form cho admin
                QTForm f = new QTForm(ad);
                f.Show();
                this.Hide();
                return;
            }

            MessageBox.Show("Sai mã số hoặc mật khẩu!", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Mk_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
