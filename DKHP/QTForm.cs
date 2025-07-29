using DKHP.data;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DKHP
{
    public partial class QTForm : Form
    {
        private Admin _ad;
        private FushiEntities db = new FushiEntities();

        public QTForm(Admin ad)
        {
            InitializeComponent();
            _ad = ad;
            LoadSinhVien();
            LoadGiangVien();
            LoadMonHoc();
            LoadLopHoc();
            LoadLopHoc_DK();
            InitDoiMatKhauPanel();

            DGVSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGVGiangVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGVMonHoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGVLopHoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGVLopHoc_DK.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGV_DangKy.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            DGVSinhVien.SelectionChanged += DGVSinhVien_SelectionChanged;
            DGVGiangVien.SelectionChanged += DGVGiangVien_SelectionChanged;
            DGVMonHoc.SelectionChanged += DGVMonHoc_SelectionChanged;
            DGVLopHoc.SelectionChanged += DGVLopHoc_SelectionChanged;
            DGVLopHoc_DK.SelectionChanged += DGVLopHoc_DK_SelectionChanged;
            DGV_DangKy.SelectionChanged += DGV_DangKy_SelectionChanged;

            DGVSinhVien.MultiSelect = false;
            DGVGiangVien.MultiSelect = false;
            DGVMonHoc.MultiSelect = false;
            DGVLopHoc.MultiSelect = false;
            DGVLopHoc_DK.MultiSelect = false;
            DGV_DangKy.MultiSelect = false;

        }

        // ===================== SINH VIÊN =====================
        private void LoadSinhVien()
        {
            var dsSV = db.SinhViens
                .Select(sv => new
                {
                    sv.MaSV,
                    sv.HoTen,
                    sv.NgaySinh,
                    sv.NamHoc,
                    sv.NamKetThuc,
                    sv.DiaChi,
                    sv.Khoa,
                    sv.Nganh,
                    sv.MatKhau
                })
                .ToList();
            DGVSinhVien.DataSource = dsSV;
        }

        private void ThemSV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxTMaSV.Text) || string.IsNullOrWhiteSpace(TxTHoTenSV.Text) || string.IsNullOrWhiteSpace(TxTMatKhauSV.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã SV, Họ tên, Mật khẩu.");
                return;
            }

            if (db.SinhViens.Any(svCheck => svCheck.MaSV == TxTMaSV.Text))
            {
                MessageBox.Show("Mã sinh viên đã tồn tại.");
                return;
            }

            var svMoi = new SinhVien
            {
                MaSV = TxTMaSV.Text.Trim(),
                HoTen = TxTHoTenSV.Text.Trim(),
                NgaySinh = DTPNgaySinhSV.Value,
                DiaChi = TxTDiaChiSV.Text.Trim(),
                Khoa = TxTKhoaSV.Text.Trim(),
                Nganh = TxTNganhSV.Text.Trim(),
                MatKhau = TxTMatKhauSV.Text.Trim()
            };
            db.SinhViens.Add(svMoi);
            db.SaveChanges();
            LoadSinhVien();
            MessageBox.Show("Thêm sinh viên thành công!");
        }

        private void SuaSV_Click(object sender, EventArgs e)
        {
            var row = DGVSinhVien.CurrentRow;
            if (row == null || row.Cells["MaSV"].Value == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên để sửa.");
                return;
            }
            string maSV = row.Cells["MaSV"].Value.ToString();
            var svSua = db.SinhViens.FirstOrDefault(x => x.MaSV == maSV);
            if (svSua == null) return;

            svSua.HoTen = TxTHoTenSV.Text.Trim();
            svSua.NgaySinh = DTPNgaySinhSV.Value;
            svSua.DiaChi = TxTDiaChiSV.Text.Trim();
            svSua.Khoa = TxTKhoaSV.Text.Trim();
            svSua.Nganh = TxTNganhSV.Text.Trim();
            if (!string.IsNullOrWhiteSpace(TxTMatKhauSV.Text))
                svSua.MatKhau = TxTMatKhauSV.Text.Trim();

            db.SaveChanges();
            LoadSinhVien();
            MessageBox.Show("Sửa sinh viên thành công!");
        }

        private void XoaSV_Click(object sender, EventArgs e)
        {
            var row = DGVSinhVien.CurrentRow;
            if (row == null || row.Cells["MaSV"].Value == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên để xóa.");
                return;
            }
            string maSV = row.Cells["MaSV"].Value.ToString();

            if (db.DangKies.Any(dk => dk.MaSV == maSV))
            {
                MessageBox.Show(
                    "Không thể xóa sinh viên này vì đang có đăng ký môn học liên quan.\n" +
                    "- Hãy xóa hoặc chuyển các đăng ký môn học liên quan trước khi xóa sinh viên.\n" +
                    "- Nếu muốn xóa nhanh, vào tab Quản lý đăng ký để thao tác."
                );
                return;
            }

            var svXoa = db.SinhViens.FirstOrDefault(x => x.MaSV == maSV);
            if (svXoa == null) return;

            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    db.SinhViens.Remove(svXoa);
                    db.SaveChanges();
                    LoadSinhVien();
                    MessageBox.Show("Xóa sinh viên thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Có lỗi khi xóa sinh viên. Vui lòng kiểm tra lại dữ liệu.\n" +
                        "- Đảm bảo không còn đăng ký môn học nào liên quan đến sinh viên này.\n" +
                        "Chi tiết: " + ex.Message
                    );
                }
            }
        }

        private void DGVSinhVien_SelectionChanged(object sender, EventArgs e)
        {
            if (DGVSinhVien.CurrentRow != null && DGVSinhVien.CurrentRow.Cells["MaSV"].Value != null)
            {
                TxTMaSV.Text = DGVSinhVien.CurrentRow.Cells["MaSV"].Value.ToString();
                TxTHoTenSV.Text = DGVSinhVien.CurrentRow.Cells["HoTen"].Value?.ToString();
                DTPNgaySinhSV.Value = DGVSinhVien.CurrentRow.Cells["NgaySinh"].Value != null ?
                    Convert.ToDateTime(DGVSinhVien.CurrentRow.Cells["NgaySinh"].Value) : DateTime.Now;
                TxTDiaChiSV.Text = DGVSinhVien.CurrentRow.Cells["DiaChi"].Value?.ToString();
                TxTKhoaSV.Text = DGVSinhVien.CurrentRow.Cells["Khoa"].Value?.ToString();
                TxTNganhSV.Text = DGVSinhVien.CurrentRow.Cells["Nganh"].Value?.ToString();
                TxTMatKhauSV.Text = ""; // Không hiển thị mật khẩu
            }
        }

        // ===================== GIẢNG VIÊN =====================
        private void LoadGiangVien()
        {
            var dsGV = db.GiangViens
                .Select(gv => new
                {
                    gv.MaGV,
                    gv.HoTen,
                    gv.Khoa,
                    gv.NgaySinh,
                    gv.MatKhau
                })
                .ToList();
            DGVGiangVien.DataSource = dsGV;
        }

        private void ThemGV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxTMaGV.Text) || string.IsNullOrWhiteSpace(TxTHoTenGV.Text) || string.IsNullOrWhiteSpace(TxTMatKhauGV.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã GV, Họ tên, Mật khẩu.");
                return;
            }

            if (db.GiangViens.Any(gvCheck => gvCheck.MaGV == TxTMaGV.Text))
            {
                MessageBox.Show("Mã giảng viên đã tồn tại.");
                return;
            }

            var gvMoi = new GiangVien
            {
                MaGV = TxTMaGV.Text.Trim(),
                HoTen = TxTHoTenGV.Text.Trim(),
                Khoa = TxTKhoaGV.Text.Trim(),
                NgaySinh = DTPNgaySinhGV.Value,
                MatKhau = TxTMatKhauGV.Text.Trim()
            };
            db.GiangViens.Add(gvMoi);
            db.SaveChanges();
            LoadGiangVien();
            MessageBox.Show("Thêm giảng viên thành công!");
        }

        private void SuaGV_Click(object sender, EventArgs e)
        {
            var row = DGVGiangVien.CurrentRow;
            if (row == null || row.Cells["MaGV"].Value == null)
            {
                MessageBox.Show("Vui lòng chọn giảng viên để sửa.");
                return;
            }
            string maGV = row.Cells["MaGV"].Value.ToString();
            var gvSua = db.GiangViens.FirstOrDefault(x => x.MaGV == maGV);
            if (gvSua == null) return;

            gvSua.HoTen = TxTHoTenGV.Text.Trim();
            gvSua.Khoa = TxTKhoaGV.Text.Trim();
            gvSua.NgaySinh = DTPNgaySinhGV.Value;
            if (!string.IsNullOrWhiteSpace(TxTMatKhauGV.Text))
                gvSua.MatKhau = TxTMatKhauGV.Text.Trim();

            try
            {
                db.SaveChanges();
                LoadGiangVien();
                MessageBox.Show("Sửa giảng viên thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi khi sửa giảng viên. Vui lòng kiểm tra lại dữ liệu nhập vào.\n" +
                    "Chi tiết: " + ex.Message
                );
            }
        }

        private void XoaGV_Click(object sender, EventArgs e)
        {
            var row = DGVGiangVien.CurrentRow;
            if (row == null || row.Cells["MaGV"].Value == null)
            {
                MessageBox.Show("Vui lòng chọn giảng viên để xóa.");
                return;
            }
            string maGV = row.Cells["MaGV"].Value.ToString();

            if (db.LopHocs.Any(lh => lh.MaGV == maGV))
            {
                MessageBox.Show(
                    "Không thể xóa giảng viên này vì đang có lớp học sử dụng.\n" +
                    "- Hãy xóa hoặc chuyển các lớp học liên quan sang giảng viên khác trước khi xóa.\n" +
                    "- Nếu muốn xóa nhanh, vào tab Quản lý lớp học để thao tác."
                );
                return;
            }

            try
            {
                var gvXoa = db.GiangViens.FirstOrDefault(x => x.MaGV == maGV);
                if (gvXoa == null) return;

                db.GiangViens.Remove(gvXoa);
                db.SaveChanges();
                LoadGiangVien();
                MessageBox.Show("Xóa giảng viên thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi xóa giảng viên. Vui lòng kiểm tra lại dữ liệu.\nChi tiết: " + ex.Message);
            }
        }

        private void DGVGiangVien_SelectionChanged(object sender, EventArgs e)
        {
            if (DGVGiangVien.CurrentRow != null && DGVGiangVien.CurrentRow.Cells["MaGV"].Value != null)
            {
                TxTMaGV.Text = DGVGiangVien.CurrentRow.Cells["MaGV"].Value.ToString();
                TxTHoTenGV.Text = DGVGiangVien.CurrentRow.Cells["HoTen"].Value?.ToString();
                TxTKhoaGV.Text = DGVGiangVien.CurrentRow.Cells["Khoa"].Value?.ToString();
                DTPNgaySinhGV.Value = DGVGiangVien.CurrentRow.Cells["NgaySinh"].Value != null ?
                    Convert.ToDateTime(DGVGiangVien.CurrentRow.Cells["NgaySinh"].Value) : DateTime.Now;
                TxTMatKhauGV.Text = ""; // Không hiển thị mật khẩu
            }
        }

        // ===================== MÔN HỌC =====================
        private void LoadMonHoc()
        {
            var dsMon = db.MonHocs
                .Select(m => new
                {
                    m.MaMon,
                    m.TenMon,
                    m.SoTinChi,
                    m.MonTienQuyet
                })
                .ToList();
            DGVMonHoc.DataSource = dsMon;
        }

        private void ThemMon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxTMaMon.Text) || string.IsNullOrWhiteSpace(TxTTenMon.Text) || string.IsNullOrWhiteSpace(TxTSoTinChi.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã môn, Tên môn, Số tín chỉ.");
                return;
            }

            if (db.MonHocs.Any(m => m.MaMon == TxTMaMon.Text))
            {
                MessageBox.Show("Mã môn đã tồn tại. Vui lòng nhập mã môn khác.");
                return;
            }

            string monTienQuyet = string.IsNullOrWhiteSpace(TxTMonTienQuyet.Text) ? null : TxTMonTienQuyet.Text.Trim();
            if (!string.IsNullOrEmpty(monTienQuyet) && !db.MonHocs.Any(m => m.MaMon == monTienQuyet))
            {
                MessageBox.Show(
                    "Mã môn tiên quyết không tồn tại.\n" +
                    "- Nếu môn học này không có môn tiên quyết, hãy để trống ô này.\n" +
                    "- Nếu có, hãy chắc chắn mã môn tiên quyết đã tồn tại trong danh sách môn học."
                );
                return;
            }

            int soTinChi;
            if (!int.TryParse(TxTSoTinChi.Text, out soTinChi))
            {
                MessageBox.Show("Số tín chỉ phải là số nguyên. Vui lòng nhập lại, ví dụ: 2, 3, 4...");
                return;
            }

            try
            {
                var mon = new MonHoc
                {
                    MaMon = TxTMaMon.Text.Trim(),
                    TenMon = TxTTenMon.Text.Trim(),
                    SoTinChi = soTinChi,
                    MonTienQuyet = monTienQuyet
                };
                db.MonHocs.Add(mon);
                db.SaveChanges();
                LoadMonHoc();
                MessageBox.Show("Thêm môn học thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi khi thêm môn học. Vui lòng kiểm tra lại dữ liệu nhập vào.\n" +
                    "- Đảm bảo mã môn không trùng, số tín chỉ là số nguyên, mã môn tiên quyết hợp lệ.\n" +
                    "Chi tiết: " + ex.Message
                );
            }
        }

        private void SuaMon_Click(object sender, EventArgs e)
        {
            var row = DGVMonHoc.CurrentRow;
            if (row == null || row.Cells["MaMon"].Value == null)
            {
                MessageBox.Show("Vui lòng chọn môn học để sửa.");
                return;
            }
            string maMon = row.Cells["MaMon"].Value.ToString();

            string monTienQuyet = string.IsNullOrWhiteSpace(TxTMonTienQuyet.Text) ? null : TxTMonTienQuyet.Text.Trim();
            if (!string.IsNullOrEmpty(monTienQuyet) && !db.MonHocs.Any(m => m.MaMon == monTienQuyet))
            {
                MessageBox.Show(
                    "Mã môn tiên quyết không tồn tại.\n" +
                    "- Nếu môn học này không có môn tiên quyết, hãy để trống ô này.\n" +
                    "- Nếu có, hãy chắc chắn mã môn tiên quyết đã tồn tại trong danh sách môn học."
                );
                return;
            }

            int soTinChi;
            if (!int.TryParse(TxTSoTinChi.Text, out soTinChi))
            {
                MessageBox.Show("Số tín chỉ phải là số nguyên. Vui lòng nhập lại, ví dụ: 2, 3, 4...");
                return;
            }

            try
            {
                var mon = db.MonHocs.FirstOrDefault(m => m.MaMon == maMon);
                if (mon == null) return;

                mon.TenMon = TxTTenMon.Text.Trim();
                mon.SoTinChi = soTinChi;
                mon.MonTienQuyet = monTienQuyet;

                db.SaveChanges();
                LoadMonHoc();
                MessageBox.Show("Sửa môn học thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi khi sửa môn học. Vui lòng kiểm tra lại dữ liệu nhập vào.\n" +
                    "- Đảm bảo số tín chỉ là số nguyên, mã môn tiên quyết hợp lệ.\n" +
                    "Chi tiết: " + ex.Message
                );
            }
        }

        private void XoaMon_Click(object sender, EventArgs e)
        {
            var row = DGVMonHoc.CurrentRow;
            if (row == null || row.Cells["MaMon"].Value == null)
            {
                MessageBox.Show("Vui lòng chọn môn học để xóa.");
                return;
            }
            string maMon = row.Cells["MaMon"].Value.ToString();

            if (db.LopHocs.Any(lh => lh.MaMon == maMon))
            {
                MessageBox.Show(
                    "Không thể xóa môn học này vì đang có lớp học sử dụng.\n" +
                    "- Hãy xóa hoặc chuyển các lớp học liên quan sang môn học khác trước khi xóa.\n" +
                    "- Nếu muốn xóa nhanh, vào tab Quản lý lớp học để thao tác."
                );
                return;
            }
            if (db.MonHocs.Any(m => m.MonTienQuyet == maMon))
            {
                MessageBox.Show(
                    "Không thể xóa môn học này vì đang là môn tiên quyết của môn học khác.\n" +
                    "- Hãy sửa hoặc xóa các môn học liên quan trước khi xóa môn này."
                );
                return;
            }

            try
            {
                var mon = db.MonHocs.FirstOrDefault(m => m.MaMon == maMon);
                if (mon == null) return;

                db.MonHocs.Remove(mon);
                db.SaveChanges();
                LoadMonHoc();
                MessageBox.Show("Xóa môn học thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi khi xóa môn học. Vui lòng kiểm tra lại dữ liệu.\n" +
                    "- Đảm bảo không còn lớp học hoặc môn học nào liên quan đến môn này.\n" +
                    "Chi tiết: " + ex.Message
                );
            }
        }

        private void DGVMonHoc_SelectionChanged(object sender, EventArgs e)
        {
            if (DGVMonHoc.CurrentRow != null && DGVMonHoc.CurrentRow.Cells["MaMon"].Value != null)
            {
                TxTMaMon.Text = DGVMonHoc.CurrentRow.Cells["MaMon"].Value.ToString();
                TxTTenMon.Text = DGVMonHoc.CurrentRow.Cells["TenMon"].Value?.ToString();
                TxTSoTinChi.Text = DGVMonHoc.CurrentRow.Cells["SoTinChi"].Value?.ToString();
                TxTMonTienQuyet.Text = DGVMonHoc.CurrentRow.Cells["MonTienQuyet"].Value?.ToString();
            }
        }

        // ===================== LỚP HỌC =====================
        private void LoadLopHoc()
        {
            var dsLop = db.LopHocs
                .Select(lh => new
                {
                    lh.MaLop,
                    lh.MaMon,
                    TenMon = lh.MonHoc.TenMon,
                    lh.MaGV,
                    TenGV = lh.GiangVien.HoTen,
                    lh.LichHoc,
                    lh.SiSoToiDa
                })
                .ToList();
            DGVLopHoc.DataSource = dsLop;
        }

        private void ThemLop_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxTMaLop.Text) ||
                string.IsNullOrWhiteSpace(TxTMaMonLop.Text) ||
                string.IsNullOrWhiteSpace(TxTMaGVLop.Text) ||
                string.IsNullOrWhiteSpace(TxTLichHoc.Text) ||
                string.IsNullOrWhiteSpace(TxTSiSoToiDa.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã lớp, Mã môn, Mã giảng viên, Lịch học, Sĩ số tối đa.");
                return;
            }

            if (db.LopHocs.Any(lh => lh.MaLop == TxTMaLop.Text))
            {
                MessageBox.Show("Mã lớp đã tồn tại. Vui lòng nhập mã lớp khác.");
                return;
            }

            if (!db.MonHocs.Any(m => m.MaMon == TxTMaMonLop.Text.Trim()))
            {
                MessageBox.Show("Mã môn không tồn tại. Vui lòng nhập đúng mã môn.");
                return;
            }

            if (!db.GiangViens.Any(gv => gv.MaGV == TxTMaGVLop.Text.Trim()))
            {
                MessageBox.Show("Mã giảng viên không tồn tại. Vui lòng nhập đúng mã giảng viên.");
                return;
            }

            int siSo;
            if (!int.TryParse(TxTSiSoToiDa.Text, out siSo))
            {
                MessageBox.Show("Sĩ số tối đa phải là số nguyên. Vui lòng nhập lại.");
                return;
            }

            try
            {
                var lop = new LopHoc
                {
                    MaLop = TxTMaLop.Text.Trim(),
                    MaMon = TxTMaMonLop.Text.Trim(),
                    MaGV = TxTMaGVLop.Text.Trim(),
                    LichHoc = TxTLichHoc.Text.Trim(),
                    SiSoToiDa = siSo
                };
                db.LopHocs.Add(lop);
                db.SaveChanges();
                LoadLopHoc();
                MessageBox.Show("Thêm lớp học thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi thêm lớp học. Vui lòng kiểm tra lại dữ liệu.\nChi tiết: " + ex.Message);
            }
        }

        private void SuaLop_Click(object sender, EventArgs e)
        {
            var row = DGVLopHoc.CurrentRow;
            if (row == null || row.Cells["MaLop"].Value == null)
            {
                MessageBox.Show("Vui lòng chọn lớp học để sửa.");
                return;
            }
            string maLop = row.Cells["MaLop"].Value.ToString();
            var lop = db.LopHocs.FirstOrDefault(lh => lh.MaLop == maLop);
            if (lop == null) return;

            if (!db.MonHocs.Any(m => m.MaMon == TxTMaMonLop.Text.Trim()))
            {
                MessageBox.Show("Mã môn không tồn tại. Vui lòng nhập đúng mã môn.");
                return;
            }

            if (!db.GiangViens.Any(gv => gv.MaGV == TxTMaGVLop.Text.Trim()))
            {
                MessageBox.Show("Mã giảng viên không tồn tại. Vui lòng nhập đúng mã giảng viên.");
                return;
            }

            int siSo;
            if (!int.TryParse(TxTSiSoToiDa.Text, out siSo))
            {
                MessageBox.Show("Sĩ số tối đa phải là số nguyên. Vui lòng nhập lại.");
                return;
            }

            try
            {
                lop.MaMon = TxTMaMonLop.Text.Trim();
                lop.MaGV = TxTMaGVLop.Text.Trim();
                lop.LichHoc = TxTLichHoc.Text.Trim();
                lop.SiSoToiDa = siSo;

                db.SaveChanges();
                LoadLopHoc();
                MessageBox.Show("Sửa lớp học thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi sửa lớp học. Vui lòng kiểm tra lại dữ liệu.\nChi tiết: " + ex.Message);
            }
        }

        private void XoaLop_Click(object sender, EventArgs e)
        {
            var row = DGVLopHoc.CurrentRow;
            if (row == null || row.Cells["MaLop"].Value == null)
            {
                MessageBox.Show("Vui lòng chọn lớp học để xóa.");
                return;
            }
            string maLop = row.Cells["MaLop"].Value.ToString();

            if (db.DangKies.Any(dk => dk.MaLop == maLop))
            {
                MessageBox.Show(
                    "Không thể xóa lớp học này vì đang có sinh viên đăng ký.\n" +
                    "- Hãy xóa hoặc chuyển các đăng ký liên quan trước khi xóa lớp học.\n" +
                    "- Nếu muốn xóa nhanh, vào tab Quản lý đăng ký để thao tác."
                );
                return;
            }

            try
            {
                var lop = db.LopHocs.FirstOrDefault(lh => lh.MaLop == maLop);
                if (lop == null) return;

                db.LopHocs.Remove(lop);
                db.SaveChanges();
                LoadLopHoc();
                MessageBox.Show("Xóa lớp học thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi khi xóa lớp học. Vui lòng kiểm tra lại dữ liệu.\n" +
                    "- Đảm bảo không còn đăng ký nào liên quan đến lớp học này.\n" +
                    "Chi tiết: " + ex.Message
                );
            }
        }

        private void DGVLopHoc_SelectionChanged(object sender, EventArgs e)
        {
            if (DGVLopHoc.CurrentRow != null && DGVLopHoc.CurrentRow.Cells["MaLop"].Value != null)
            {
                TxTMaLop.Text = DGVLopHoc.CurrentRow.Cells["MaLop"].Value.ToString();
                TxTMaMonLop.Text = DGVLopHoc.CurrentRow.Cells["MaMon"].Value?.ToString();
                TxTMaGVLop.Text = DGVLopHoc.CurrentRow.Cells["MaGV"].Value?.ToString();
                TxTLichHoc.Text = DGVLopHoc.CurrentRow.Cells["LichHoc"].Value?.ToString();
                TxTSiSoToiDa.Text = DGVLopHoc.CurrentRow.Cells["SiSoToiDa"].Value?.ToString();
            }
        }

        // ===================== ĐĂNG KÝ =====================
        private void LoadLopHoc_DK()
        {
            var dsLop = db.LopHocs
                .Select(lh => new
                {
                    lh.MaLop,
                    lh.MaMon,
                    TenMon = lh.MonHoc.TenMon,
                    lh.MaGV,
                    TenGV = lh.GiangVien.HoTen,
                    lh.LichHoc,
                    lh.SiSoToiDa
                })
                .ToList();
            DGVLopHoc_DK.DataSource = dsLop;
        }

        private void LoadDangKyTheoLop(string maLop)
        {
            var dsDK = db.DangKies
                .Where(dk => dk.MaLop == maLop)
                .Select(dk => new
                {
                    dk.MaSV,
                    HoTen = dk.SinhVien.HoTen,
                    dk.NgayDangKy
                })
                .ToList();
            DGV_DangKy.DataSource = dsDK;
        }

        private void DGVLopHoc_DK_SelectionChanged(object sender, EventArgs e)
        {
            if (DGVLopHoc_DK.CurrentRow != null && DGVLopHoc_DK.CurrentRow.Cells["MaLop"].Value != null)
            {
                string maLop = DGVLopHoc_DK.CurrentRow.Cells["MaLop"].Value.ToString();
                LoadDangKyTheoLop(maLop);
                TxTMaLop_DK.Text = maLop;
            }
        }

        private void DGV_DangKy_SelectionChanged(object sender, EventArgs e)
        {
            if (DGV_DangKy.CurrentRow != null && DGV_DangKy.CurrentRow.Cells["MaSV"].Value != null)
            {
                TxTMaSV_DK.Text = DGV_DangKy.CurrentRow.Cells["MaSV"].Value.ToString();
            }
        }

        private void ThemDangKy_Click(object sender, EventArgs e)
        {
            string maSV = TxTMaSV_DK.Text.Trim();
            string maLop = TxTMaLop_DK.Text.Trim();

            if (string.IsNullOrWhiteSpace(maSV) || string.IsNullOrWhiteSpace(maLop))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã sinh viên và Mã lớp.");
                return;
            }

            if (!db.SinhViens.Any(sv => sv.MaSV == maSV))
            {
                MessageBox.Show("Mã sinh viên không tồn tại.");
                return;
            }

            if (!db.LopHocs.Any(lh => lh.MaLop == maLop))
            {
                MessageBox.Show("Mã lớp không tồn tại.");
                return;
            }

            if (db.DangKies.Any(dk => dk.MaSV == maSV && dk.MaLop == maLop))
            {
                MessageBox.Show("Sinh viên này đã đăng ký lớp học này.");
                return;
            }

            try
            {
                var dk = new DangKy
                {
                    MaSV = maSV,
                    MaLop = maLop,
                    NgayDangKy = DateTime.Now
                };
                db.DangKies.Add(dk);
                db.SaveChanges();
                LoadDangKyTheoLop(maLop);
                MessageBox.Show("Đăng ký thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi đăng ký. Vui lòng kiểm tra lại dữ liệu.\nChi tiết: " + ex.Message);
            }
        }

        private void XoaDangKy_Click(object sender, EventArgs e)
        {
            if (DGV_DangKy.CurrentRow == null || DGV_DangKy.CurrentRow.Cells["MaSV"].Value == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên để xóa đăng ký.");
                return;
            }
            string maSV = DGV_DangKy.CurrentRow.Cells["MaSV"].Value.ToString();
            string maLop = TxTMaLop_DK.Text.Trim();

            var dkXoa = db.DangKies.FirstOrDefault(dk => dk.MaSV == maSV && dk.MaLop == maLop);
            if (dkXoa == null) return;

            if (MessageBox.Show("Bạn có chắc muốn xóa đăng ký này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    db.DangKies.Remove(dkXoa);
                    db.SaveChanges();
                    LoadDangKyTheoLop(maLop);
                    MessageBox.Show("Xóa đăng ký thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi khi xóa đăng ký. Vui lòng kiểm tra lại dữ liệu.\nChi tiết: " + ex.Message);
                }
            }
        }

        // ===================== ĐĂNG XUẤT =====================
        private void DangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            var loginForm = new LoginForm();
            loginForm.Show();
        }

        // ===================== TÌM KIẾM =====================

        // Tìm kiếm Sinh viên
        private void TimKiemSV_Click(object sender, EventArgs e)
        {
            string keyword = TXTTimKiemSV.Text.Trim().ToLower();
            var dsSV = db.SinhViens
                .Where(sv =>
                    sv.MaSV.ToLower().Contains(keyword) ||
                    sv.HoTen.ToLower().Contains(keyword) ||
                    sv.Khoa.ToLower().Contains(keyword) ||
                    sv.Nganh.ToLower().Contains(keyword)
                )
                .Select(sv => new
                {
                    sv.MaSV,
                    sv.HoTen,
                    sv.NgaySinh,
                    sv.NamHoc,
                    sv.NamKetThuc,
                    sv.DiaChi,
                    sv.Khoa,
                    sv.Nganh,
                    sv.MatKhau
                })
                .ToList();
            DGVSinhVien.DataSource = dsSV;
        }

        // Tìm kiếm Giảng viên
        private void TimKiemGV_Click(object sender, EventArgs e)
        {
            string keyword = TXTTimKiemGV.Text.Trim().ToLower();
            var dsGV = db.GiangViens
                .Where(gv =>
                    gv.MaGV.ToLower().Contains(keyword) ||
                    gv.HoTen.ToLower().Contains(keyword) ||
                    gv.Khoa.ToLower().Contains(keyword)
                )
                .Select(gv => new
                {
                    gv.MaGV,
                    gv.HoTen,
                    gv.Khoa,
                    gv.NgaySinh,
                    gv.MatKhau
                })
                .ToList();
            DGVGiangVien.DataSource = dsGV;
        }

        // Tìm kiếm Môn học
        private void TimKiemMon_Click(object sender, EventArgs e)
        {
            string keyword = TXTTimKiemMon.Text.Trim().ToLower();
            var dsMon = db.MonHocs
                .Where(m =>
                    m.MaMon.ToLower().Contains(keyword) ||
                    m.TenMon.ToLower().Contains(keyword)
                )
                .Select(m => new
                {
                    m.MaMon,
                    m.TenMon,
                    m.SoTinChi,
                    m.MonTienQuyet
                })
                .ToList();
            DGVMonHoc.DataSource = dsMon;
        }

        // Tìm kiếm Lớp học
        private void TimKiemLop_Click(object sender, EventArgs e)
        {
            string keyword = TXTTimKiemLop.Text.Trim().ToLower();
            var dsLop = db.LopHocs
                .Where(lh =>
                    lh.MaLop.ToLower().Contains(keyword) ||
                    lh.MaMon.ToLower().Contains(keyword) ||
                    lh.MaGV.ToLower().Contains(keyword) ||
                    lh.MonHoc.TenMon.ToLower().Contains(keyword) ||
                    lh.GiangVien.HoTen.ToLower().Contains(keyword)
                )
                .Select(lh => new
                {
                    lh.MaLop,
                    lh.MaMon,
                    TenMon = lh.MonHoc.TenMon,
                    lh.MaGV,
                    TenGV = lh.GiangVien.HoTen,
                    lh.LichHoc,
                    lh.SiSoToiDa
                })
                .ToList();
            DGVLopHoc.DataSource = dsLop;
        }

        // Tìm kiếm Đăng ký

        // Tìm kiếm lớp trong DGVLopHoc_DK
        private void TimKiemLop_DK_Click(object sender, EventArgs e)
        {
            string keyword = TXTTimKiemLop_DK.Text.Trim().ToLower();
            var dsLop = db.LopHocs
                .Where(lh =>
                    lh.MaLop.ToLower().Contains(keyword) ||
                    lh.MonHoc.TenMon.ToLower().Contains(keyword) ||
                    lh.GiangVien.HoTen.ToLower().Contains(keyword) // Thêm điều kiện này
                )
                .Select(lh => new
                {
                    lh.MaLop,
                    lh.MaMon,
                    TenMon = lh.MonHoc.TenMon,
                    lh.MaGV,
                    TenGV = lh.GiangVien.HoTen,
                    lh.LichHoc,
                    lh.SiSoToiDa
                })
                .ToList();
            DGVLopHoc_DK.DataSource = dsLop;
        }

        // Tìm kiếm sinh viên đã đăng ký lớp được chọn ở DGVLopHoc_DK
        private void TimKiemSV_DK_Click(object sender, EventArgs e)
        {
            // Lấy mã lớp đang chọn ở DGVLopHoc_DK
            if (DGVLopHoc_DK.CurrentRow == null || DGVLopHoc_DK.CurrentRow.Cells["MaLop"].Value == null)
            {
                MessageBox.Show("Vui lòng chọn một lớp học để tìm kiếm sinh viên đăng ký.");
                return;
            }
            string maLop = DGVLopHoc_DK.CurrentRow.Cells["MaLop"].Value.ToString();
            string keyword = TXTTimKiemSV_DK.Text.Trim().ToLower();

            var dsDK = db.DangKies
                .Where(dk => dk.MaLop == maLop &&
                    (dk.MaSV.ToLower().Contains(keyword) ||
                     dk.SinhVien.HoTen.ToLower().Contains(keyword))
                )
                .Select(dk => new
                {
                    dk.MaSV,
                    HoTen = dk.SinhVien.HoTen,
                    dk.MaLop,
                    TenMon = dk.LopHoc.MonHoc.TenMon,
                    dk.NgayDangKy
                })
                .ToList();

            DGV_DangKy.DataSource = dsDK;
        }

        // ===================== ĐỔI MẬT KHẨU =====================

        // Các biến cho panel đổi mật khẩu
        private Panel pnlDoiMatKhau;
        private TextBox txtMatKhauCu;
        private TextBox txtMatKhauMoi;
        private TextBox txtNhapLaiMatKhauMoi;
        private Button btnXacNhanDoiMatKhau;
        private Button btnHuyDoiMatKhau;

        // Gọi hàm này trong constructor sau InitializeComponent();
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

        // Gọi hàm này khi nhấn button DoiMatKhau
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

            var admin = db.Admins.FirstOrDefault(a => a.MaAdmin == _ad.MaAdmin);
            if (admin == null)
            {
                MessageBox.Show("Không tìm thấy tài khoản.");
                return;
            }
            if (admin.MatKhau != mkCu)
            {
                MessageBox.Show("Mật khẩu cũ không đúng.");
                return;
            }

            admin.MatKhau = mkMoi;
            db.SaveChanges();
            _ad.MatKhau = mkMoi;
            MessageBox.Show("Đổi mật khẩu thành công!");
            pnlDoiMatKhau.Visible = false;
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox8_Enter(object sender, EventArgs e)
        {

        }

        private void TxTMatKhauGV_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox13_Enter(object sender, EventArgs e)
        {

        }
    }
}