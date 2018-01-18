using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using QuanLyCuaHang.Report;

namespace QuanLyCuaHang
{
    public partial class PageReport : UserControl
    {
        public PageReport()
        {
            InitializeComponent();
        }

        private static PageReport INSTANCE;
        public static PageReport getINTANCE
        {
            get
            {
                if (INSTANCE == null)
                {
                    INSTANCE = new PageReport();
                }
                return INSTANCE;
            }
        }

        private void PageReport_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            KhachHangReport kh = new KhachHangReport();
            kh.ShowPreviewDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NhanVienReport nv = new NhanVienReport();
            nv.ShowPreviewDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SanPhamReport sp = new SanPhamReport();
            sp.ShowPreviewDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HoaDonReport sp = new HoaDonReport();
            sp.ShowPreviewDialog();
        }


    }
}
