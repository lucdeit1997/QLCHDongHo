using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;

namespace QuanLyCuaHang
{
    public partial class PageThongTinTaiKhoan : UserControl
    {
        NhanVien nhanVien = null;
        private static PageThongTinTaiKhoan INSTANCE;//thực thể

        public static PageThongTinTaiKhoan getINSTANCE
        {
            get 
            {
                if (INSTANCE == null) 
                {
                    INSTANCE = new PageThongTinTaiKhoan();
                }
                return INSTANCE;
            }
        }

        public PageThongTinTaiKhoan()
        {
            InitializeComponent();
        }

        public PageThongTinTaiKhoan(NhanVien nhanVien) : this()
        {
            this.nhanVien = nhanVien;
        }

        private void PageThongTinTaiKhoan_Load(object sender, EventArgs e)
        {
            txtTaiKhoan.Text = nhanVien.taiKhoan;
            txtMatKhau.Text = nhanVien.matKhau;
            txtTenNV.Text = nhanVien.tenNV;
            txtMaNV.Text = nhanVien.maNV;
            txtNgaySinh.Text = nhanVien.ngaySinh;
            txtSoDT.Text = nhanVien.sDT;
            txtDiaChi.Text = nhanVien.diaChi;
        }

 
    }
}
