using DKHP.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DKHP
{
    public partial class HSForm : Form
    {
        private FushiEntities db = new FushiEntities();
        private string _maLopCu = null;
        private SinhVien _sv;
        private List<dynamic> _dsLopMoi;

        private Panel pnlDoiMatKhau;
        private TextBox txtMatKhauCu;
        private TextBox txtMatKhauMoi;
        private TextBox txtNhapLaiMatKhauMoi;
        private Button btnXacNhanDoiMatKhau;
        private Button btnHuyDoiMatKhau;

        public HSForm(SinhVien sv)
        {
            InitializeComponent();
            _sv = sv;
            HienThiThongTinSinhVien();
            LoadLopHocChuaDangKy();
            LoadLopHocDaDangKy();
            DGVChonLopMoi.Visible = false;
            XacNhanChuyenLop.Visible = false;
            DGVMonHoc.MultiSelect = false;
            DGVMonHoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGVMonDaDangKy.MultiSelect = false;
            DGVMonDaDangKy.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGVChonLopMoi.MultiSelect = false;
            DGVChonLopMoi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGVMonHoc.DataBindingComplete += (s, e) => DGVMonHoc.ClearSelection();
            DGVMonDaDangKy.DataBindingComplete += (s, e) => DGVMonDaDangKy.ClearSelection();
            DGVChonLopMoi.DataBindingComplete += (s, e) => DGVChonLopMoi.ClearSelection();
            InitDoiMatKhauPanel();
        }

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
            var sv = db.SinhViens.FirstOrDefault(s => s.MaSV == _sv.MaSV);
            if (sv == null)
            {
                MessageBox.Show("Không tìm thấy tài khoản.");
                return;
            }
            if (sv.MatKhau != mkCu)
            {
                MessageBox.Show("Mật khẩu cũ không đúng.");
                return;
            }
            sv.MatKhau = mkMoi;
            db.SaveChanges();
            _sv.MatKhau = mkMoi;
            MessageBox.Show("Đổi mật khẩu thành công!");
            pnlDoiMatKhau.Visible = false;
        }

        private void HienThiThongTinSinhVien()
        {
            TxTMaSV.Text = _sv.MaSV;
            TxTHoTen.Text = _sv.HoTen;
            TxTNgaySinh.Text = _sv.NgaySinh.HasValue ? _sv.NgaySinh.Value.ToString("dd/MM/yyyy") : "";
            TxTNamHoc.Text = _sv.NamHoc.HasValue ? _sv.NamHoc.Value.ToString() : "";
            TxTNamKetThuc.Text = _sv.NamKetThuc.HasValue ? _sv.NamKetThuc.Value.ToString() : "";
            TxTDiaChi.Text = _sv.DiaChi;
            TxTKhoa.Text = _sv.Khoa;
            TxTNganh.Text = _sv.Nganh;
        }

        private void LoadLopHocChuaDangKy()
        {
            var lopChuaDK = db.LopHocs
                .Where(lh => !db.DangKies.Any(dk => dk.MaSV == _sv.MaSV && dk.MaLop == lh.MaLop))
                .Select(lh => new
                {
                    lh.MaLop,
                    lh.MaMon,
                    TenMon = lh.MonHoc.TenMon,
                    MonTienQuyet = lh.MonHoc.MonTienQuyet, // Thêm dòng này
                    GiangVien = lh.GiangVien.HoTen,
                    lh.LichHoc,
                    lh.SiSoToiDa,
                    SoLuongDaDK = lh.DangKies.Count(),
                    SoTinChi = lh.MonHoc.SoTinChi
                })
                .ToList();
            DGVMonHoc.DataSource = lopChuaDK;
        }

        private void LoadLopHocDaDangKy()
        {
            var lopDaDK = db.DangKies
                .Where(dk => dk.MaSV == _sv.MaSV)
                .Select(dk => new
                {
                    dk.MaDK,
                    dk.MaLop,
                    MaMon = dk.LopHoc.MaMon,
                    TenMon = dk.LopHoc.MonHoc.TenMon,
                    MonTienQuyet = dk.LopHoc.MonHoc.MonTienQuyet, // Thêm dòng này
                    GiangVien = dk.LopHoc.GiangVien.HoTen,
                    dk.LopHoc.LichHoc,
                    dk.LopHoc.SiSoToiDa,
                    SoLuongDaDK = dk.LopHoc.DangKies.Count(),
                    SoTinChi = dk.LopHoc.MonHoc.SoTinChi,
                    NgayDangKy = dk.NgayDangKy
                })
                .ToList();
            DGVMonDaDangKy.DataSource = lopDaDK;
        }

        private void DangKy_Click(object sender, EventArgs e)
        {
            if (DGVMonHoc.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn lớp học để đăng ký.");
                return;
            }
            string maLop = DGVMonHoc.SelectedRows[0].Cells["MaLop"].Value.ToString();
            var lop = db.LopHocs.FirstOrDefault(l => l.MaLop == maLop);
            if (lop == null)
            {
                MessageBox.Show("Không tìm thấy lớp học.");
                return;
            }

            // --- KIỂM TRA ĐÃ ĐĂNG KÝ MÔN NÀY CHƯA ---
            bool daDangKyMon = db.DangKies.Any(dk => dk.MaSV == _sv.MaSV && dk.LopHoc.MaMon == lop.MaMon);
            if (daDangKyMon)
            {
                MessageBox.Show("Bạn đã đăng ký một lớp của môn này rồi!");
                return;
            }
            // ----------------------------------------

            if (lop.DangKies.Count() >= lop.SiSoToiDa)
            {
                MessageBox.Show("Lớp đã đủ sĩ số.");
                return;
            }
            var mon = lop.MonHoc;
            if (!string.IsNullOrEmpty(mon.MonTienQuyet))
            {
                bool daHocTienQuyet = db.DangKies
                    .Any(dk => dk.MaSV == _sv.MaSV && dk.LopHoc.MaMon == mon.MonTienQuyet);
                if (!daHocTienQuyet)
                {
                    MessageBox.Show("Bạn chưa học môn tiên quyết: " + mon.MonTienQuyet);
                    return;
                }
            }
            int tongTinChi = db.DangKies
                .Where(dk => dk.MaSV == _sv.MaSV)
                .Sum(dk => (int?)dk.LopHoc.MonHoc.SoTinChi) ?? 0;
            if (tongTinChi + mon.SoTinChi > 18)
            {
                MessageBox.Show("Vượt quá số tín chỉ tối đa cho phép.");
                return;
            }
            DangKy dkMoi = new DangKy
            {
                MaSV = _sv.MaSV,
                MaLop = maLop,
                NgayDangKy = DateTime.Now
            };
            db.DangKies.Add(dkMoi);
            db.SaveChanges();
            MessageBox.Show("Đăng ký thành công!");
            LoadLopHocChuaDangKy();
            LoadLopHocDaDangKy();
        }

        private void Huy_Click(object sender, EventArgs e)
        {
            if (DGVMonDaDangKy.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn lớp học để hủy đăng ký.");
                return;
            }
            int maDK = Convert.ToInt32(DGVMonDaDangKy.SelectedRows[0].Cells["MaDK"].Value);
            var dk = db.DangKies.FirstOrDefault(x => x.MaDK == maDK && x.MaSV == _sv.MaSV);
            if (dk == null)
            {
                MessageBox.Show("Không tìm thấy đăng ký.");
                return;
            }
            db.DangKies.Remove(dk);
            db.SaveChanges();
            MessageBox.Show("Hủy đăng ký thành công!");
            LoadLopHocChuaDangKy();
            LoadLopHocDaDangKy();
        }

        private void ChuyenLop_Click(object sender, EventArgs e)
        {
            if (DGVMonDaDangKy.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn lớp học đã đăng ký để chuyển.");
                return;
            }
            _maLopCu = DGVMonDaDangKy.SelectedRows[0].Cells["MaLop"].Value.ToString();
            var lopCu = db.LopHocs.FirstOrDefault(l => l.MaLop == _maLopCu);
            if (lopCu == null)
            {
                MessageBox.Show("Không tìm thấy lớp học cũ.");
                return;
            }
            string maMon = lopCu.MaMon;
            _dsLopMoi = db.LopHocs
                .Where(lh => lh.MaMon == maMon && lh.MaLop != _maLopCu
                    && !db.DangKies.Any(dk => dk.MaSV == _sv.MaSV && dk.MaLop == lh.MaLop)
                    && lh.DangKies.Count() < lh.SiSoToiDa)
                .Select(lh => new
                {
                    lh.MaLop,
                    lh.MaMon,
                    TenMon = lh.MonHoc.TenMon,
                    MonTienQuyet = lh.MonHoc.MonTienQuyet, // Thêm dòng này
                    GiangVien = lh.GiangVien.HoTen,
                    lh.LichHoc,
                    lh.SiSoToiDa,
                    SoLuongDaDK = lh.DangKies.Count(),
                    SoTinChi = lh.MonHoc.SoTinChi
                })
                .ToList<dynamic>();
            if (_dsLopMoi.Count == 0)
            {
                MessageBox.Show("Không có lớp khác cùng môn để chuyển hoặc các lớp đều đã đủ sĩ số.");
                DGVChonLopMoi.Visible = false;
                XacNhanChuyenLop.Visible = false;
                return;
            }
            DGVChonLopMoi.DataSource = _dsLopMoi;
            DGVChonLopMoi.Visible = true;
            XacNhanChuyenLop.Visible = true;
        }

        private void XacNhanChuyenLop_Click(object sender, EventArgs e)
        {
            if (DGVChonLopMoi.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn lớp mới để chuyển.");
                return;
            }
            string maLopMoi = DGVChonLopMoi.SelectedRows[0].Cells["MaLop"].Value.ToString();
            if (maLopMoi == _maLopCu)
            {
                MessageBox.Show("Bạn đang ở lớp này rồi.");
                return;
            }
            var lopMoi = db.LopHocs.FirstOrDefault(l => l.MaLop == maLopMoi);
            if (lopMoi == null || lopMoi.DangKies.Count() >= lopMoi.SiSoToiDa)
            {
                MessageBox.Show("Lớp mới không hợp lệ hoặc đã đủ sĩ số.");
                return;
            }
            var dkCu = db.DangKies.FirstOrDefault(dk => dk.MaSV == _sv.MaSV && dk.MaLop == _maLopCu);
            if (dkCu != null)
                db.DangKies.Remove(dkCu);
            DangKy dkMoi = new DangKy
            {
                MaSV = _sv.MaSV,
                MaLop = maLopMoi,
                NgayDangKy = DateTime.Now
            };
            db.DangKies.Add(dkMoi);
            db.SaveChanges();
            MessageBox.Show("Chuyển lớp thành công!");
            DGVChonLopMoi.Visible = false;
            XacNhanChuyenLop.Visible = false;
            _maLopCu = null;
            LoadLopHocChuaDangKy();
            LoadLopHocDaDangKy();
        }

        private void TimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = TxTTimKiem.Text.Trim().ToLower();
            var query = db.LopHocs
                .Where(lh => !db.DangKies.Any(dk => dk.MaSV == _sv.MaSV && dk.MaLop == lh.MaLop));
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                query = query.Where(lh =>
                    lh.MaLop.ToLower().Contains(tuKhoa) ||
                    lh.MaMon.ToLower().Contains(tuKhoa) ||
                    lh.MonHoc.TenMon.ToLower().Contains(tuKhoa) ||
                    lh.GiangVien.HoTen.ToLower().Contains(tuKhoa) ||
                    lh.LichHoc.ToLower().Contains(tuKhoa) ||
                    lh.MonHoc.SoTinChi.ToString().Contains(tuKhoa)
                );
            }
            var ketQua = query.Select(lh => new
            {
                lh.MaLop,
                lh.MaMon,
                TenMon = lh.MonHoc.TenMon,
                MonTienQuyet = lh.MonHoc.MonTienQuyet, // Thêm dòng này
                GiangVien = lh.GiangVien.HoTen,
                lh.LichHoc,
                lh.SiSoToiDa,
                SoLuongDaDK = lh.DangKies.Count(),
                SoTinChi = lh.MonHoc.SoTinChi
            }).ToList();
            DGVMonHoc.DataSource = ketQua;
        }

        private void DangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            var loginForm = new LoginForm();
            loginForm.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void HSForm_Load(object sender, EventArgs e)
        {

        }
    }
}