using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using BUS;
using DTO;
namespace QuanLyCuaHang
{
    public partial class FrmDangNhap : DevExpress.XtraEditors.XtraForm
    {
        private static FrmDangNhap INSTANCE;

        public static FrmDangNhap getINSTANCE {
            get {
                if (INSTANCE == null)
                {
                    INSTANCE = new FrmDangNhap(false);
                }

                return INSTANCE;
            }
        }
        bool login = false;
        public FrmDangNhap()
        {
            Thread t = new Thread(new ThreadStart(StartForm));
            t.Start();
            Thread.Sleep(2000);
            InitializeComponent();

            //for (int i = 0; i < 1000; i++)
            //{
            //    Thread.Sleep(4);
            //}

            t.Abort();
        }

        public FrmDangNhap(bool isFALSE) {
            InitializeComponent();
        }

        //public FrmDangNhap(string userName, string passWord, bool isCheck) : this()
        //{
        //    //InitializeComponent();
        //    chkSavePassWord.Checked = isCheck;
        //    if(isCheck)
        //    {
        //        txtTaiKhoan.Text = userName;
        //        txtMatKhau.Text = passWord;
        //    }
        //}
        public void StartForm()
        {
            Application.Run(new FrmSplashScreen());

            //FrmSplashScreen.getINSTANCE.Show();
        }
       
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LlbDangKi_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmDangKi f = new FrmDangKi();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            
            DataTable tb = NhanVienBUS.getINSTANCE.connectLogin();
            DataTableReader reader = tb.CreateDataReader();

            
            while (reader.Read() == true)
            {

                if (reader.GetString(2).Equals(txtTaiKhoan.Text) && reader.GetString(3).Equals(txtMatKhau.Text))
                {
                    login = true;

                    //txtTaiKhoan.Text = "";
                    //txtMatKhau.Text = "";

                    this.Hide();

                    
                    NhanVien nhanVien = new NhanVien(
                        reader.GetString(0).ToString(), 
                        reader.GetString(1).ToString(), 
                        reader.GetString(2).ToString(), 
                        reader.GetString(3).ToString(), 
                        reader.GetDateTime(4).ToLongDateString(),
                        reader.GetString(5).ToString(), 
                        reader.GetString(6).ToString());


                    FrmMain f = new FrmMain(nhanVien);
                    //FrmMain f = new FrmMain();  
                    f.ShowDialog();
                    this.Close();
                    
                    return;
                }

            }

            if (!login) {
                
                if (txtMatKhau.Text == ""  || txtTaiKhoan.Text == "")
                {
                    
                    if (txtTaiKhoan.Text == "" && txtMatKhau.Text == "")
                    {
                        XtraMessageBox.Show("Vui lòng nhập tài khoản Và mạt khẩu");
                    }

                    else if (txtTaiKhoan.Text == "")
                    {
                        XtraMessageBox.Show("Vui Lòng Nhập tài khoản");
                    }
                    else
                    {
                        XtraMessageBox.Show("Vui Lòng Nhập mật khau");
                        
                    }
     
                }
                else
                    XtraMessageBox.Show("Sai tài khoản hoặc mật khẩu");
            }

        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        bool flag = false; int x, y;
        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            flag = true;
            x = e.X;
            y = e.Y;
        }

        private void pnlHeader_MouseUp(object sender, MouseEventArgs e)
        {
            flag = false;
        }

        private void pnlHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag == true)
            {
                this.SetDesktopLocation(Cursor.Position.X-x, Cursor.Position.Y-y);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void chkSavePassWord_OnChange(object sender, EventArgs e)
        {
            if (chkSavePassWord.Checked)
            {
                flag = true;
                txtMatKhau.isPassword = false;
            }
            else
            {
                flag = false;
                txtMatKhau.isPassword = true;
            }
        }


    }
}