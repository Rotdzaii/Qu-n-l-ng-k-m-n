using DKHP.data;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DKHP
{
    public partial class GVForm : Form
    {
        private GiangVien _gv;
        private FushiEntities db = new FushiEntities();

        // Panel và các control cho đổi mật khẩu
        private Panel pnlDoiMatKhau;
        private TextBox txtMatKhauCu;
        private TextBox txtMatKhauMoi;
        private TextBox txtNhapLaiMatKhauMoi;
        private Button btnXacNhanDoiMatKhau;
        private Button btnHuyDoiMatKhau;

        public GVForm(GiangVien gv)
        {
            InitializeComponent();
            _gv = gv;
            HienThiThongTinGiangVien();
            LoadDanhSachLopHoc();
            DGVLopHoc.SelectionChanged += DGVLopHoc_SelectionChanged;

            // Hủy chọn khi load dữ liệu
            DGVLopHoc.DataBindingComplete += (s, e) => DGVLopHoc.ClearSelection();
            DGVSinhVien.DataBindingComplete += (s, e) => DGVSinhVien.ClearSelection();

            DGVLopHoc.MultiSelect = false;
            DGVSinhVien.MultiSelect = false;

            DGVLopHoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGVSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Khởi tạo panel đổi mật khẩu
            InitDoiMatKhauPanel();
        }

        private void HienThiThongTinGiangVien()
        {
            TxTMSGV.Text = _gv.MaGV;
            TxTHoVaTen.Text = _gv.HoTen;
            TxTKhoa.Text = _gv.Khoa;
            TxTNgaySinh.Text = _gv.NgaySinh.HasValue ? _gv.NgaySinh.Value.ToString("dd/MM/yyyy") : "";
        }

        // Hiển thị danh sách lớp mà giảng viên phụ trách
        private void LoadDanhSachLopHoc()
        {
            var dsLop = db.LopHocs
                .Where(lh => lh.MaGV == _gv.MaGV)
                .Select(lh => new
                {
                    lh.MaLop,
                    lh.MaMon,
                    TenMon = lh.MonHoc.TenMon,
                    GiangVien = lh.GiangVien.HoTen,
                    lh.LichHoc,
                    lh.SiSoToiDa,
                    SoLuongDaDK = lh.DangKies.Count(),
                    SoTinChi = lh.MonHoc.SoTinChi
                })
                .ToList();

            DGVLopHoc.DataSource = dsLop;
            if (DGVLopHoc.Rows.Count > 0)
            {
                DGVLopHoc.Rows[0].Selected = true;
                string maLop = DGVLopHoc.Rows[0].Cells["MaLop"].Value.ToString();
                LoadDanhSachSinhVien(maLop);
            }
        }

        // Khi chọn lớp, hiển thị danh sách sinh viên đã đăng ký lớp đó
        private void DGVLopHoc_SelectionChanged(object sender, EventArgs e)
        {
            if (DGVLopHoc.CurrentRow != null && DGVLopHoc.CurrentRow.Cells["MaLop"].Value != null)
            {
                string maLop = DGVLopHoc.CurrentRow.Cells["MaLop"].Value.ToString();
                LoadDanhSachSinhVien(maLop);
            }
        }

        private void LoadDanhSachSinhVien(string maLop)
        {
            var dsSinhVien = (from dk in db.DangKies
                              join sv in db.SinhViens on dk.MaSV equals sv.MaSV
                              where dk.MaLop == maLop
                              select new
                              {
                                  sv.MaSV,
                                  sv.HoTen,
                                  sv.NgaySinh,
                                  sv.Khoa,
                                  sv.Nganh
                              }).ToList();

            DGVSinhVien.DataSource = dsSinhVien;
        }

        private void DangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            var loginForm = new LoginForm();
            loginForm.Show();
        }

        // ===================== ĐỔI MẬT KHẨU =====================
        // Khởi tạo panel đổi mật khẩu
        private void InitDoiMatKhauPanel()
        {
            pnlDoiMatKhau = new Panel
            {
                Width = 320,
                Height = 200,
                Left = (this.Width - 320) / 2,
                Top = (this.Height - 200) / 2,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false
            };

            Label lblCu = new Label { Text = "Mật khẩu cũ:", Left = 20, Top = 20, Width = 110 };
            txtMatKhauCu = new TextBox { Left = 140, Top = 18, Width = 150, UseSystemPasswordChar = true };

            Label lblMoi = new Label { Text = "Mật khẩu mới:", Left = 20, Top = 60, Width = 110 };
            txtMatKhauMoi = new TextBox { Left = 140, Top = 58, Width = 150, UseSystemPasswordChar = true };

            Label lblNhapLai = new Label { Text = "Nhập lại MK mới:", Left = 20, Top = 100, Width = 110 };
            txtNhapLaiMatKhauMoi = new TextBox { Left = 140, Top = 98, Width = 150, UseSystemPasswordChar = true };

            btnXacNhanDoiMatKhau = new Button { Text = "Xác nhận", Left = 50, Top = 145, Width = 100 };
            btnHuyDoiMatKhau = new Button { Text = "Hủy", Left = 170, Top = 145, Width = 100 };

            btnXacNhanDoiMatKhau.Click += BtnXacNhanDoiMatKhau_Click;
            btnHuyDoiMatKhau.Click += BtnHuyDoiMatKhau_Click;

            pnlDoiMatKhau.Controls.Add(lblCu);
            pnlDoiMatKhau.Controls.Add(txtMatKhauCu);
            pnlDoiMatKhau.Controls.Add(lblMoi);
            pnlDoiMatKhau.Controls.Add(txtMatKhauMoi);
            pnlDoiMatKhau.Controls.Add(lblNhapLai);
            pnlDoiMatKhau.Controls.Add(txtNhapLaiMatKhauMoi);
            pnlDoiMatKhau.Controls.Add(btnXacNhanDoiMatKhau);
            pnlDoiMatKhau.Controls.Add(btnHuyDoiMatKhau);

            this.Controls.Add(pnlDoiMatKhau);
        }

        // Khi nhấn nút Đổi mật khẩu
        private void DoiMatKhau_Click(object sender, EventArgs e)
        {
            txtMatKhauCu.Text = "";
            txtMatKhauMoi.Text = "";
            txtNhapLaiMatKhauMoi.Text = "";
            pnlDoiMatKhau.Visible = true;
            pnlDoiMatKhau.BringToFront();
        }

        private void BtnHuyDoiMatKhau_Click(object sender, EventArgs e)
        {
            pnlDoiMatKhau.Visible = false;
        }

        private void BtnXacNhanDoiMatKhau_Click(object sender, EventArgs e)
        {
            string mkCu = txtMatKhauCu.Text.Trim();
            string mkMoi = txtMatKhauMoi.Text.Trim();
            string mkNhapLai = txtNhapLaiMatKhauMoi.Text.Trim();

            if (string.IsNullOrEmpty(mkCu) || string.IsNullOrEmpty(mkMoi) || string.IsNullOrEmpty(mkNhapLai))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            if (mkMoi != mkNhapLai)
            {
                MessageBox.Show("Mật khẩu mới và nhập lại không khớp.");
                return;
            }

            var gv = db.GiangViens.FirstOrDefault(g => g.MaGV == _gv.MaGV);
            if (gv == null)
            {
                MessageBox.Show("Không tìm thấy tài khoản.");
                return;
            }
            if (gv.MatKhau != mkCu)
            {
                MessageBox.Show("Mật khẩu cũ không đúng.");
                return;
            }

            gv.MatKhau = mkMoi;
            db.SaveChanges();
            _gv.MatKhau = mkMoi;
            MessageBox.Show("Đổi mật khẩu thành công!");
            pnlDoiMatKhau.Visible = false;
        }

        // Tìm kiếm lớp học mà giảng viên phụ trách
        private void TimKiemLop_DK_Click(object sender, EventArgs e)
        {
            string keyword = TXTTimKiemLop_DK.Text.Trim().ToLower();
            var dsLop = db.LopHocs
                .Where(lh => lh.MaGV == _gv.MaGV &&
                    (lh.MaLop.ToLower().Contains(keyword) ||
                     lh.MonHoc.TenMon.ToLower().Contains(keyword))
                )
                .Select(lh => new
                {
                    lh.MaLop,
                    lh.MaMon,
                    TenMon = lh.MonHoc.TenMon,
                    GiangVien = lh.GiangVien.HoTen,
                    lh.LichHoc,
                    lh.SiSoToiDa,
                    SoLuongDaDK = lh.DangKies.Count(),
                    SoTinChi = lh.MonHoc.SoTinChi
                })
                .ToList();

            DGVLopHoc.DataSource = dsLop;

            // Hiển thị danh sách sinh viên của lớp đầu tiên nếu có kết quả
            if (dsLop.Count > 0)
            {
                string maLop = dsLop[0].MaLop;
                LoadDanhSachSinhVien(maLop);
                if (DGVLopHoc.Rows.Count > 0)
                    DGVLopHoc.Rows[0].Selected = true;
            }
            else
            {
                DGVSinhVien.DataSource = null;
            }
        }

        // Tìm kiếm sinh viên đã đăng ký lớp được chọn
        private void TimKiemSV_DK_Click(object sender, EventArgs e)
        {
            if (DGVLopHoc.CurrentRow == null || DGVLopHoc.CurrentRow.Cells["MaLop"].Value == null)
            {
                MessageBox.Show("Vui lòng chọn một lớp học để tìm kiếm sinh viên đăng ký.");
                return;
            }
            string maLop = DGVLopHoc.CurrentRow.Cells["MaLop"].Value.ToString();
            string keyword = TXTTimKiemSV_DK.Text.Trim().ToLower();

            var dsSinhVien = (from dk in db.DangKies
                              join sv in db.SinhViens on dk.MaSV equals sv.MaSV
                              where dk.MaLop == maLop &&
                                    (sv.MaSV.ToLower().Contains(keyword) ||
                                     sv.HoTen.ToLower().Contains(keyword) ||
                                     sv.Khoa.ToLower().Contains(keyword) ||
                                     sv.Nganh.ToLower().Contains(keyword))
                              select new
                              {
                                  sv.MaSV,
                                  sv.HoTen,
                                  sv.NgaySinh,
                                  sv.Khoa,
                                  sv.Nganh
                              }).ToList();

            DGVSinhVien.DataSource = dsSinhVien;
        }
    }
}