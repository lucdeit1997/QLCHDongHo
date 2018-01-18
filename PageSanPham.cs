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
    
    public partial class PageSanPham : UserControl
    {

        bool flag = false;
        private static PageSanPham INSTANCE;
        public static PageSanPham getINSTANCE
        {
            get
            {
                if (INSTANCE == null)
                {
                    INSTANCE = new PageSanPham();

                }
                return INSTANCE;
            }
        }

        public PageSanPham()
        {
            InitializeComponent();
        }

        private void PageSanPham_Load(object sender, EventArgs e)
        {
            showSanPham();
            dgvSanPham.RowTemplate.Height = 25;
            binding();
            showCBMaSP();
            setButton(true, true, true, false, false);
        }
        public void binding()
        {
            txtMaSP.DataBindings.Clear();
            txtMaSP.DataBindings.Add("text",dgvSanPham.DataSource,"MaSP");

            txtTenSP.DataBindings.Clear();
            txtTenSP.DataBindings.Add("text",dgvSanPham.DataSource,"TenSP");


            //int giaBan = Convert.ToInt32(txtGiaBan.Text);
            //txtGiaBan.Text = giaBan.ToString("N0");

            txtGiaBan.DataBindings.Clear();
            txtGiaBan.DataBindings.Add("text",dgvSanPham.DataSource,"GiaBan");
            //int giaBan = Convert.ToInt32(txtGiaBan.Text);
            //txtGiaBan.Text = giaBan.ToString("N0");
            //MessageBox.Show(txtGiaBan.Text);

            txtDonViTinh.DataBindings.Clear();
            txtDonViTinh.DataBindings.Add("text", dgvSanPham.DataSource, "DVT");

        }

        public void showSanPham()
        {
            DataTable tb = SanPhamBUS.getINSTANCE.DSSanPham();
            dgvSanPham.DataSource = tb;
            binding();
            

        }

        private void insertSanPham()
        {
            if (!txtMaSP.Text.Equals("") && !txtTenSP.Text.Equals("") && !txtGiaBan.Text.Equals("") && !txtDonViTinh.Text.Equals(""))
            {
                if (SanPhamBUS.getINSTANCE.testCount(txtMaSP.Text))
                {
                    XtraMessageBox.Show("Mã Sản Phẩm Bị Trùng", "Vui Lòng Nhập Lại", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string maSP = txtMaSP.Text;
                    string tenSP = txtTenSP.Text;
                    int giaBan = int.Parse(txtGiaBan.Text);
                    string dVT = txtDonViTinh.Text;
                    if (SanPhamBUS.getINSTANCE.insertSanPhamBUS(new SanPham(maSP, tenSP, giaBan, dVT)))
                    {

                        showSanPham();
                        XtraMessageBox.Show("Thêm Thành Công", "Chúc Mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    } 
                }
            }
            else 
            {
                XtraMessageBox.Show("Vui lòng nhập đủ thông tin", "Chúc Mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);                
            }
        }
      
        public void setNull()
        {
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            txtGiaBan.Text = "";
            txtDonViTinh.Text = "";
            txtMaSP.Focus();
        }

        public void setButton(bool them , bool xoa, bool sua, bool luu, bool huy)
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
            setButton(true,false,false,true,true);
            flag = true;
            
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            if (flag)
            {
                insertSanPham();
                showCBMaSP();
            }
            else
            {
                suaSanPham();

            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
                xoaSP();
                showSanPham();
                HoaDonBUS.getINSTANCE.getData();
        }

        public void xoaSP()
        {
            if (SanPhamBUS.getINSTANCE.testCount(txtMaSP.Text))
            {
                
                if (XtraMessageBox.Show("Bạn có muốn xóa Mã SP: '" + txtMaSP.Text + "' Không?", "Thông Báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (HoaDonBUS.getINSTANCE.countMaSPHD(txtMaSP.Text))
                    {
                        HoaDonBUS.getINSTANCE.deleteHoaDonMaSPBUS(txtMaSP.Text);
                    }
                    SanPhamBUS.getINSTANCE.deleteSanPhamBUS(txtMaSP.Text);
                    XtraMessageBox.Show("Xóa Thành Công");
                    showCBMaSP();

                }
                
            }
            else
            {
                XtraMessageBox.Show("Mã sản phẩm cần xóa không tồn tại");
            }
        }
        
        private void btnHuy_Click(object sender, EventArgs e)
        {
            setButton(true,true,true,false,false);
            showSanPham();            
            binding();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            setButton(false,false,true,true,true);
            flag = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {

        }

        public void suaSanPham()
        {
            if(!txtMaSP.Text.Equals("") && !txtTenSP.Text.Equals("") && !txtGiaBan.Text.Equals("") && !txtDonViTinh.Text.Equals(""))
            {
                 if (!txtMaSP.Text.Equals(""))
                {
                    bool demo = SanPhamBUS.getINSTANCE.testCount(txtMaSP.Text);
                    if (!demo)
                    {
                        XtraMessageBox.Show("Mã sản phẩm không tồn tại");

                        return;
                    }
                    else
                    {
                        SanPhamBUS.getINSTANCE.updateSanPham(txtMaSP.Text, txtTenSP.Text, txtGiaBan.Text, txtDonViTinh.Text);
                        showSanPham();
                    }
                }
                else
                {
                    XtraMessageBox.Show("Vui lòng nhập mã cần sữa");
                }
            
            }
            else
            {
                XtraMessageBox.Show("Vui lòng nhập đủ thông tin");
            }
           
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            timKiemSp();
            setButton(true, true, true, false, true);
          
        }

        public void timKiemSp()
        {
            DataTable tb =  SanPhamBUS.getINSTANCE.searchSanPham(cbbSearchMaSP.Text);
            dgvSanPham.DataSource = tb;
            binding();
        }

        private void btnHuyTimKiem_Click(object sender, EventArgs e)
        {
            showSanPham();
        }

        private void showCBMaSP()
        {
            DataTable tb = SanPhamBUS.getINSTANCE.DSSanPham();
            cbbSearchMaSP.DataSource = tb;
            cbbSearchMaSP.DisplayMember = "maSP";
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            SanPhamReport sp = new SanPhamReport();
            sp.ShowPreviewDialog();
        }
    }
}
