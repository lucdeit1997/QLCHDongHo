using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DevExpress.XtraEditors;
using DTO;
using DevExpress.XtraReports.UI;
using QuanLyCuaHang.Report;
namespace QuanLyCuaHang
{

    public partial class PageNhanVien : UserControl
    {
        bool them = false;
        private static PageNhanVien INSTANCE;
        public static PageNhanVien getINTANCE
        {
            get
            {
                if (INSTANCE == null)
                {
                    INSTANCE = new PageNhanVien();
                }
                return INSTANCE;
            }
        }
        public PageNhanVien()
        {
            InitializeComponent();
        }

        private void PageNhanVien_Load(object sender, EventArgs e)
        {
            hienThiDSNV();
            showCBSearch();
            binding();
            setButton(true, true, true, false, false);

        }

        public void binding()
        {
            txtMaNV.DataBindings.Clear(); 
            txtMaNV.DataBindings.Add("text", dgvNhanVien.DataSource, "MaNV");

            txtTenNV.DataBindings.Clear();
            txtTenNV.DataBindings.Add("text", dgvNhanVien.DataSource, "TenNV");

            txtTaiKhoan.DataBindings.Clear();
            txtTaiKhoan.DataBindings.Add("text", dgvNhanVien.DataSource, "TAIKHOAN");

            txtMatKhau.DataBindings.Clear();
            txtMatKhau.DataBindings.Add("text", dgvNhanVien.DataSource, "MATKHAU");

            dtpNgaySinh.DataBindings.Clear();
            dtpNgaySinh.DataBindings.Add("Value", dgvNhanVien.DataSource, "NgaySinh");

            txtSDT.DataBindings.Clear();
            txtSDT.DataBindings.Add("text", dgvNhanVien.DataSource, "SDT");

            txtDiaChi.DataBindings.Clear();
            txtDiaChi.DataBindings.Add("text", dgvNhanVien.DataSource, "DiaChi");

        }

        public void hienThiDSNV()
        {
            dgvNhanVien.RowTemplate.Height = 25;
            DataTable tb = NhanVienBUS.getINSTANCE.DSNhanVien();
            dgvNhanVien.DataSource = tb;
            binding();
        }

        private void setButton(bool them,bool xoa,bool sua, bool luu, bool huy)
        {
            btnThem.Enabled = them;
            btnXoa.Enabled = xoa;
            btnSua.Enabled = sua;
            btnLuu.Enabled = luu;
            btnHuy.Enabled = huy;
        }
        private void setTexBox()
        {
            txtMaNV.Focus();
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtTaiKhoan.Text = "";
            txtMatKhau.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
           
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            setButton(true,false,false,true,true);
            setTexBox();
            them = true;
        }

        public void insertNhanVien()
        {
            if (!txtTaiKhoan.Text.Equals("") && !txtMatKhau.Text.Equals("") && !txtMaNV.Text.Equals("")
                      && !txtTenNV.Text.Equals("") && !dtpNgaySinh.Value.Equals("") && !txtDiaChi.Text.Equals("")
                      && !txtSDT.Text.Equals(""))
            {
                if (NhanVienBUS.getINSTANCE.testCountNhanVien(txtMaNV.Text))
                {
                    XtraMessageBox.Show("Mã nhân viên đã tồn tại");
                }
                else
                {
                    if (NhanVienBUS.getINSTANCE.testCountTaiKhoan(txtTaiKhoan.Text))
                    {
                        XtraMessageBox.Show("Tên Tài Khoản Đã Tồn tại ");
                    }
                    else
                    {
                        if (txtMatKhau.Text.Length >= 5)
                        {
                            if (txtMaNV.Text.Length > 10)
                            {
                                XtraMessageBox.Show("Vui lòng nhập tên tài khoản bé 10 kí tự", "Thông Báo");

                            }
                            else
                            {
                                string maNV = txtMaNV.Text;
                                string tenNV = txtTenNV.Text;
                                string taiKhoan = txtTaiKhoan.Text;
                                string matKhau = txtMatKhau.Text;
                                String ngaySinh = dtpNgaySinh.Value.ToString();
                                string diaChi = txtDiaChi.Text;
                                string sDT = txtSDT.Text;
                                if (NhanVienBUS.getINSTANCE.insertNhanVien(new NhanVien(maNV, tenNV, taiKhoan, matKhau, ngaySinh, diaChi, sDT)))
                                {
                                    hienThiDSNV();
                                    XtraMessageBox.Show("Thêm thành công", "Thông Báo");

                                }
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show("Vui lòng nhập mật khẩu lớn hơn 5 kí tự");
                        }
                        

                        
                    }                   
                }
            }
            else
            {
                XtraMessageBox.Show("Nhập đầy đủ thông tin để thêm ");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn xóa MaNV '" + txtMaNV.Text + "' không?", "Thông Báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                deleteNhanVien();
                hienThiDSNV();
            }
           
        }
        private void deleteNhanVien()
        {
            if (!txtMaNV.Text.Equals(""))
            {
                if (NhanVienBUS.getINSTANCE.testCountNhanVien(txtMaNV.Text))
                {
                    if (HoaDonBUS.getINSTANCE.countMaNVHD(txtMaNV.Text))
                    {
                        HoaDonBUS.getINSTANCE.deleteHoaDonMaNV(txtMaNV.Text);
                    }
                    
                    NhanVienBUS.getINSTANCE.deleteNhanVien(txtMaNV.Text);
                    XtraMessageBox.Show("Xóa thành công", "Thông Báo");                    

                }
                else
                {
                    XtraMessageBox.Show("Mã nhân viên không tồn tại", "Thông Báo");                    
                }
            }
            else
            {
                XtraMessageBox.Show("Vui lòng nhập MaNV cần xóa","Thông Báo");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            setButton(false, false,true,true,true);
            them = false;
        }

        public void updateNhanVien()
        {
            if (!txtTaiKhoan.Text.Equals("") && !txtMatKhau.Text.Equals("") && !txtMaNV.Text.Equals("")
                      && !txtTenNV.Text.Equals("") && !dtpNgaySinh.Value.Equals("") && !txtDiaChi.Text.Equals("")
                      && !txtSDT.Text.Equals(""))
            {

                if (NhanVienBUS.getINSTANCE.testCountNhanVien(txtMaNV.Text))
                {
                    if (NhanVienBUS.getINSTANCE.testCountTaiKhoan(txtTaiKhoan.Text))
                    {
                        XtraMessageBox.Show("Tên tài khoản đã tồn tại", "Thông Báo");

                    }
                    else
                    {
                        string maNV = txtMaNV.Text;
                        string tenNV = txtTenNV.Text;
                        string taiKhoan = txtTaiKhoan.Text;
                        string matKhau = txtMatKhau.Text;
                        String ngaySinh = dtpNgaySinh.Value.ToString();
                        string diaChi = txtDiaChi.Text;
                        string sDT = txtSDT.Text;

                        if(NhanVienBUS.getINSTANCE.updateNhanVien(new NhanVien(maNV,tenNV,taiKhoan,matKhau,ngaySinh,diaChi,sDT)))
                        {
                            XtraMessageBox.Show("Sữa thành công", "Thông Báo");
                            hienThiDSNV();
                        }
                       
                    }
                }
                else
                {
                    XtraMessageBox.Show("Mã nhân viên cần sữa không tồn tại", "Thông Báo");
                    
                }
            }
            else
            {
                
                XtraMessageBox.Show("Vui lòng nhập đủ thông tin cần sữa", "Thông Báo");
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            searchNhanVien();
            setButton(true, true, true, false, true);
           
        }
        public void searchNhanVien()
        {
            DataTable tb;
                tb = NhanVienBUS.getINSTANCE.searchNhanVien(cbxSearch.Text);
            dgvNhanVien.DataSource = tb;
            binding();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            setButton(true,true,true,false,false);
            hienThiDSNV();
        }
        public void showCBSearch()
        { 
            DataTable tb = NhanVienBUS.getINSTANCE.getCBMaNV();
            cbxSearch.DataSource = tb;
            cbxSearch.DisplayMember = "maNV";
            
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (them == true)
            {
                insertNhanVien();
                showCBSearch();
            }
            else
            {
                updateNhanVien();            
            }
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void pnlNhanVien_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            NhanVienReport nv = new NhanVienReport();
            nv.ShowPreviewDialog();
        }

    }
}
