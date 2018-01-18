using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QuanLyCuaHang
{
    public partial class PageMain : DevExpress.XtraEditors.XtraUserControl
    {
        private static PageMain INSTANCE;
        public static PageMain getINTANCE
        {
            get
            {
                if (INSTANCE == null)
                {
                    INSTANCE = new PageMain();
                }
                return INSTANCE;
            }
        }
        public PageMain()
        {
            InitializeComponent();
        }
    }
}
