using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO;
namespace QuanLyCuaHang
{
    public partial class FrmMain : DevExpress.XtraEditors.XtraForm
    {
        int xHeThong = 1;
        int xQuanLy = 1;
        private static int PanelSlide_width_max = 200;
        private static int PanelSlide_width_min = 55;


        NhanVien nhanVien = null;
        bool Ischeck = false;
       

        public FrmMain()
        {
            InitializeComponent();
        }

        public FrmMain(NhanVien nv): this()
        {
            this.nhanVien = nv;
         
        }
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn thoát không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (pnlSlide.Width == PanelSlide_width_max)
            {
                animLoGoLarge.HideSync(LogoLarge);
                pnlSlide.Visible = false;
                pnlSlide.Width = PanelSlide_width_min;
                animSlide.ShowSync(pnlSlide);
                animLoGoSmall.ShowSync(LogoSmall);
                pnlLogo.Height = 90;
                animSubMenu.HideSync(pnlSubQuanLy);
                animSubMenu.HideSync(pnlSubHeThong);
            }
            else
            {
                pnlLogo.Height = 172;                
                animLoGoSmall.HideSync(LogoSmall);
                pnlSlide.Visible = false;
                pnlSlide.Width = PanelSlide_width_max;
                animSlide.ShowSync(pnlSlide);
                animLoGoLarge.ShowSync(LogoLarge);
               
            }

            
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            lblDongHo.Text = DateTime.Now.ToLongTimeString();
            lblHeader.Left = -lblHeader.Width;

            lblNgay.Text = DateTime.Now.ToLongDateString();
            PnlMainBringToFront(PageMain.getINTANCE);

            
        }

        private void btnHeThong_Click(object sender, EventArgs e)
        {
            //thayDoiSubPanle(true);
            if (xHeThong == 1)
            {
                
                animSubMenu.HideSync(pnlSubQuanLy);
                xQuanLy = 1;


                xHeThong = 2; 
                animSubMenu.ShowSync(pnlSubHeThong);
                

            }
            else
            {
                xHeThong = 1;
                animSubMenu.HideSync(pnlSubHeThong);
            }


        }

        private void btnQuanLy_Click(object sender, EventArgs e)
        {
            if (xQuanLy == 1)
            {
                
                animSubMenu.HideSync(pnlSubHeThong);
                xHeThong = 1;

                //

                xQuanLy = 2;
                animSubMenu.ShowSync(pnlSubQuanLy);
                
                
            }
            else
            {
                xQuanLy = 1;
                animSubMenu.HideSync(pnlSubQuanLy);
            }
        }

        private void btnThongKeBaoCao_Click(object sender, EventArgs e)
        {
            xHeThong = 1;
            animSubMenu.HideSync(pnlSubHeThong);

            xQuanLy = 1;
            animSubMenu.HideSync(pnlSubQuanLy);


            PageReport pageRebort = new PageReport();
            PnlMainBringToFront(pageRebort);
        }

   

        public void PnlMainBringToFront(Control control) 
        {
            if (!pnlMain.Controls.Contains(control))//chứa page ko
            {
                pnlMain.Controls.Add(control);
                control.Dock = DockStyle.Fill;
                control.BringToFront();
            }
            else
            {
                control.BringToFront();
            } 
        }

        private void btnThongTinTK_Click(object sender, EventArgs e)
        {
            //PnlMainBringToFront(PageThongTinTaiKhoan.getINSTANCE);

            PageThongTinTaiKhoan pageTK = new PageThongTinTaiKhoan(nhanVien);
            PnlMainBringToFront(pageTK);
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            //PnlMainBringToFront(PageThayDoiMatKhau.getINTANCE);

            PageThayDoiMatKhau pageDMK = new PageThayDoiMatKhau(nhanVien);
            PnlMainBringToFront(pageDMK);
        }

        private void btnQLNhanVien_Click(object sender, EventArgs e)
        {
            PageNhanVien pageNV = new PageNhanVien();
            PnlMainBringToFront(pageNV);
        }




        private void btnQlKachHang_Click(object sender, EventArgs e)
        {
            PageKhachHang pageKH = new PageKhachHang();
            PnlMainBringToFront(pageKH);

        }

        private void btnQuanLySanPham_Click(object sender, EventArgs e)
        {
            PageSanPham pageSP = new PageSanPham();
            PnlMainBringToFront(pageSP);
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            
            if(XtraMessageBox.Show("Bạn có muốn đăng xuất không?","ĐĂNG XUẤT",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
                FrmDangNhap frmDangNhap = new FrmDangNhap();
                frmDangNhap.ShowDialog(); 
                Application.ExitThread();
            }
          
        }

        private void btnHoaDonBH_Click(object sender, EventArgs e)
        {
            PageHoaDonBanHang page = new PageHoaDonBanHang();
            PnlMainBringToFront(page);
        }
        int i = 8;
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDongHo.Text = DateTime.Now.ToLongTimeString();
            lblNgay.Text = DateTime.Now.ToLongDateString();


            lblHeader.Left += i;

            if (lblHeader.Left >= this.Width)
            {
                lblHeader.Left = -lblHeader.Width;
            }
                
        }
        int kt = 1;
        private void lblHeader_Click(object sender, EventArgs e)
        {
            if (kt == 1)
            {
                kt = 2;
                timer1.Stop();
                lblHeader.Left = (this.Width / 2 + lblHeader.Width / 2) / 2;
            }
            else
            {
                kt = 1;
                timer1.Start();
            }
        }

        private void LogoLarge_Click(object sender, EventArgs e)
        {
            PnlMainBringToFront(PageMain.getINTANCE);
        }

        private void LogoSmall_Click(object sender, EventArgs e)
        {
            PnlMainBringToFront(PageMain.getINTANCE);
        }

      

     //  ==================
    }
}
