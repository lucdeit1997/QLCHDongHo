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
using BUS;
using DevExpress.XtraEditors;
namespace QuanLyCuaHang
{
    public partial class PageThayDoiMatKhau : UserControl
    {
        NhanVien nhanVien = null;
        private static PageThayDoiMatKhau INTANCE;
        public static PageThayDoiMatKhau getINTANCE
        {
            get
            {
                if (INTANCE == null)
                {
                    INTANCE = new PageThayDoiMatKhau();
                }
                return INTANCE;
            }
        }

        public PageThayDoiMatKhau()
        {
            InitializeComponent();
        }

        public PageThayDoiMatKhau(NhanVien nhanVien) :this()
        {
            this.nhanVien = nhanVien;   
        }
        private void btnXácNhan_Click(object sender, EventArgs e)
        {
            if (!txtMatKhau.Text.Equals("") && !txtMatKhauMoi.Text.Equals("") && !txtXacNhanMatKhau.Text.Equals(""))
            {
                if (txtMatKhau.Text.Equals(nhanVien.matKhau))
                {

                    if (txtMatKhauMoi.Text.Equals(txtXacNhanMatKhau.Text))
                    {
                        if (NhanVienBUS.getINSTANCE.updatePasswordBUS(txtTaiKhoan.Text, txtMatKhau.Text, txtMatKhauMoi.Text))
                        {
                            XtraMessageBox.Show("Đổi mật khẩu thành công","Thông Báo");
                        }

                    }
                    else
                    {
                        XtraMessageBox.Show("Vui lòng xác nhận lại mật khẩu", "Thông Báo");

                    }
                }
                else
                {
                    XtraMessageBox.Show("Sai mật khẩu", "Thông Báo");

                }

            }
            else
            {
                XtraMessageBox.Show("Vui lòng nhập đủ thông tin", "Thông Báo");
               
            }
        }

        private void PageThayDoiMatKhau_Load(object sender, EventArgs e)
        {
            txtTaiKhoan.Text = nhanVien.taiKhoan;
            txtMatKhau.Focus();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            
        }


    }
}
