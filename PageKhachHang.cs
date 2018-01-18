using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BUS;
using DTO;
using DevExpress.XtraReports.UI;
using QuanLyCuaHang.Report;
namespace QuanLyCuaHang
{
    
    public partial class PageKhachHang : UserControl
    {

        bool them = false;
             
        private static PageKhachHang INSTANCE;
        public static PageKhachHang getINSTANCE
        {
            get 
            {
                if (INSTANCE == null)
                {
                    INSTANCE = new PageKhachHang();
                }
                return INSTANCE;
            }
        }
        public PageKhachHang()
        {
            InitializeComponent();
        }

        private void PageKhachHang_Load(object sender, EventArgs e)
        {
            getCBBMaKH();
            showKhachHang();
            setButton(true, true, true, false, false);
            dgvKhachHang.RowTemplate.Height = 25;
        }



        public void showKhachHang()
        {
            dgvKhachHang.ColumnHeadersHeight = 25;
            DataTable tb = KhachHangBUS.getINSTANCE.DSKhachHang();
            dgvKhachHang.DataSource = tb;
            binding();
        }


        public void binding()
        {
            txtMaKH.DataBindings.Clear();
            txtMaKH.DataBindings.Add("text",dgvKhachHang.DataSource,"MaKH");

            txtTenKH.DataBindings.Clear();
            txtTenKH.DataBindings.Add("text",dgvKhachHang.DataSource,"TenKH");

            txtDiaChi.DataBindings.Clear();
            txtDiaChi.DataBindings.Add("text",dgvKhachHang.DataSource,"DiaChi");


            txtSoDT.DataBindings.Clear();
            txtSoDT.DataBindings.Add("text",dgvKhachHang.DataSource,"SDT");
        }

        public void setNull()
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtDiaChi.Text = "";
            txtSoDT.Text = "";
        }
        private void setButton(bool them, bool xoa, bool sua, bool luu, bool huy)
        {
            btnThem.Enabled = them;
            btnXoa.Enabled = xoa;
            btnSua.Enabled = sua;
            btnLuu.Enabled = luu;
            btnHuy.Enabled = huy;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            setNull();
            setButton(true, false, false, true, true);
            txtMaKH.Focus();
            them = true;
        }

        public void setEnabled(bool val)
        {
            txtMaKH.Enabled = !val;
            txtTenKH.Enabled = val;
            txtDiaChi.Enabled = val;
            txtSoDT.Enabled = val;
            txtMaKH.Focus();
        }

        public void themKH()
        {
            if (!txtMaKH.Text.Equals("") && !txtTenKH.Text.Equals("") && !txtSoDT.Text.Equals("") && !txtDiaChi.Text.Equals(""))
            {
                if (KhachHangBUS.getINSTANCE.testCountMaKH(txtMaKH.Text))
                {
                    XtraMessageBox.Show("Mã khách hàng bị trùng", "Thông Báo");                                 
                }
                else
                {
                    string maKH = txtMaKH.Text;
                    string tenKH = txtTenKH.Text;
                    string sDT = txtSoDT.Text;
                    string diaChi = txtDiaChi.Text;
                    if (KhachHangBUS.getINSTANCE.themKhachHang(new KhachHang(maKH, tenKH, diaChi, sDT)))
                    {
                        XtraMessageBox.Show("Thêm Thành Công", "Chúc Mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        showKhachHang();
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Vui lòng nhập đủ thông tin","Thông Báo");             
            }
        }
       
           
        private void btnHuy_Click(object sender, EventArgs e)
        {
            setButton(true, true, true, false, false);
            showKhachHang(); 
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (them)
            {
                themKH();
                getCBBMaKH();
            }
            else
            {
                suaKH();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
                xoaKhachHang();
                
           
        }

        public void xoaKhachHang()
        {
            if (KhachHangBUS.getINSTANCE.testCountMaKH(txtMaKH.Text))
            {
                if (XtraMessageBox.Show("Bạn có muốn xóa KH '" + txtTenKH.Text + "' không ?", "Thông Báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {               

                    if(HoaDonBUS.getINSTANCE.countMaKHHD(txtMaKH.Text))
                    {
                        HoaDonBUS.getINSTANCE.deleteHoaDonMaKHBUS(txtMaKH.Text);
                        HoaDonBUS.getINSTANCE.getData();
                    }
                    KhachHangBUS.getINSTANCE.deleteKhachHangBUS(txtMaKH.Text);
                    XtraMessageBox.Show("Xóa thành công", "Chúc Mừng");
                    showKhachHang();
                    getCBBMaKH();
                }  
                    
            }
            else
            {
                XtraMessageBox.Show("Mã khách hàng cần xóa không tồn tại","Lỗi");
            }
        }
        public void suaKH()
        {
            if (KhachHangBUS.getINSTANCE.testCountMaKH(txtMaKH.Text))
            {
                if (XtraMessageBox.Show("Bạn có muốn sữa khách hàng có mã '" + txtMaKH.Text + "' không", "Thông Báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    string maKH = txtMaKH.Text;
                    string tenKH = txtTenKH.Text;
                    string diaChi = txtDiaChi.Text;
                    string sDT = txtSoDT.Text;
                    if (KhachHangBUS.getINSTANCE.updateKhachHang(new KhachHang(maKH, tenKH, diaChi, sDT)))
                    {
                        XtraMessageBox.Show("Sữa Thành công");
                        showKhachHang();
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Mã Khách Hàng cần sữa Không Tồn Tại","Thông Báo");                
            }
        }
     
        private void btnSua_Click(object sender, EventArgs e)
        {
            setButton(false, false, true, true, true);

            them = false;
        }


        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            setButton(true, true, true, false, true);
            DataTable tb = new DataTable();
            tb = KhachHangBUS.getINSTANCE.searchKhachHang(cbbSearch.Text);
            dgvKhachHang.DataSource = tb;
            binding();
        }

        private void btnHuyTimKiem_Click(object sender, EventArgs e)
        {
            showKhachHang();
        }
        private void getCBBMaKH()
        {
            DataTable tb = KhachHangBUS.getINSTANCE.DSKhachHang();
            cbbSearch.DataSource = tb;
            cbbSearch.DisplayMember = "maKH";
        }

        private void txtSoDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            KhachHangReport kh = new KhachHangReport();
            kh.ShowPreviewDialog();
        }

    }
}
