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

namespace QuanLyCuaHang
{
    public partial class FrmSplashScreen : DevExpress.XtraEditors.XtraForm
    {
        private static FrmSplashScreen INSTANCE;

        public static FrmSplashScreen getINSTANCE {
            get {
                if (INSTANCE == null)
                {
                    INSTANCE = new FrmSplashScreen();
                }

                return INSTANCE;
            }
        }

        public FrmSplashScreen()
        {
            InitializeComponent();
        }

        private void FrmSplashScreen_Load(object sender, EventArgs e)
        {

        }
    }
}