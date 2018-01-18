using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BUS;
using DTO;
namespace QuanLyCuaHang
{
    public partial class FrmDangKi : DevExpress.XtraEditors.XtraForm
    {
        
        public FrmDangKi()
        {
            InitializeComponent();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmDangKi_Load(object sender, EventArgs e)
        {

        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {

            dangKy();
               
        }

        public void dangKy()
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
                        XtraMessageBox.Show("Tài khoản này đã tồn tại");

                    }
                    else
                    {

                        if (txtMatKhau.Text.Equals(txtXacNhanMK.Text))
                        {

                            if (txtMatKhau.Text.Length >= 5)
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
                                    XtraMessageBox.Show("Đăng Kí thành công", "Thông Báo");
                                    this.Hide();
                                    FrmDangNhap.getINSTANCE.ShowDialog();
                                    this.Close();

                                }

                            }
                            else
                            {
                                XtraMessageBox.Show("Vui lòng nhập mật khẩu lớn hơn 5 kí tự", "Thông Báo");
                            
                            }
                            
                        }
                        else
                        {
                            XtraMessageBox.Show("Vui lòng xác nhận lại mật khẩu");
                        
                        }

                       
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("vui Lòng Nhập đủ thông tin");
            }

        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        bool flag = false;
        int x, y;
        private void pnlHead_MouseDown(object sender, MouseEventArgs e)
        {
            flag = true;
            x = e.X;
            y = e.Y;
        }

        private void pnlHead_MouseUp(object sender, MouseEventArgs e)
        {
            flag = false;
        }

        private void pnlHead_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag == true)
            {
                this.SetDesktopLocation(Cursor.Position.X - x, Cursor.Position.Y - y);
            }
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

    }
}