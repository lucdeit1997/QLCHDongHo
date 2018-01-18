using System;
using DTO;
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
using QuanLyCuaHang.Report;
using DevExpress.XtraReports.UI;
namespace QuanLyCuaHang
{
    public partial class PageHoaDonBanHang : UserControl
    {
        bool flag = false;
        private static PageHoaDonBanHang INSTANCE;
        public static  PageHoaDonBanHang getINSTANCE
        {
            get
            {
                if (INSTANCE == null)
                    INSTANCE = new PageHoaDonBanHang();
                return INSTANCE;
            }
            
        }
        public PageHoaDonBanHang()
        {
            InitializeComponent();
        }

        private void setButton(bool them, bool xoa, bool sua, bool luu, bool huy)
        {
            btnThem.Enabled = them;
            btnXoa.Enabled = xoa;
            btnSua.Enabled = sua;
            btnLuu.Enabled = luu;
            btnHuy.Enabled = huy;
        }

        public void setNull()
        {
            txtMaHD.Text = "";
            cbMaSP.Text = "";
            cbbMaNV.Text = "";
            cbbMaKH.Text = "";
            txtMaHD.Focus();
        }

        public void setEnable(bool val)
        {
            txtMaHD.Enabled = val;
            cbbMaKH.Enabled = val;
            cbbMaNV.Enabled = val;
            cbMaSP.Enabled = val;
            nudSoLuong.Enabled = val;
        }
    

        public void binding()
        {
            txtMaHD.DataBindings.Clear();
            txtMaHD.DataBindings.Add("text",dgvCTHD.DataSource,"maHD");

            dtpNgayLap.DataBindings.Clear();
            dtpNgayLap.DataBindings.Add("value",dgvCTHD.DataSource,"ngayLap");

            cbMaSP.DataBindings.Clear();
            cbMaSP.DataBindings.Add("text", dgvCTHD.DataSource,"MaSP");

            cbbMaNV.DataBindings.Clear();
            cbbMaNV.DataBindings.Add("text",dgvCTHD.DataSource,"maNV");

            cbbMaKH.DataBindings.Clear();
            cbbMaKH.DataBindings.Add("text",dgvCTHD.DataSource,"maKH");

            nudSoLuong.DataBindings.Clear();
            nudSoLuong.DataBindings.Add("value",dgvCTHD.DataSource,"soLuong");
        }
        private void PageHoaDonBanHang_Load(object sender, EventArgs e)
        {
            getData();
            getCBMaNV();
            getCBMaSP();
            getCBMaKH();
            binding();
            setButton(true,true,true,false,false);
            cbbSearch.ResetText();
        }

        private void getData()
        {
            DataTable tb = HoaDonBUS.getINSTANCE.getData();
            dgvCTHD.DataSource = tb;
            binding();
            showCBSearch();

        }

        public void insertHoaDon()
        {
            if (!txtMaHD.Text.Equals(""))
            {
                if (HoaDonBUS.getINSTANCE.countMaHD(txtMaHD.Text))
                {
                    XtraMessageBox.Show("Mã hóa đơn tồn tại");
                }
                else
                {
                    string maHD = txtMaHD.Text;
                    string ngayLap = dtpNgayLap.Value.ToString();

                    string maSP = cbMaSP.Text;
                    string maNV = cbbMaNV.Text;
                    string maKH = cbbMaKH.Text;
                    int soLuong = (int)nudSoLuong.Value;
                    HoaDon hoaDon = new HoaDon(maHD, ngayLap, maSP, maNV, maKH, soLuong);

                    if (HoaDonBUS.getINSTANCE.insertHoaHon(hoaDon))
                    {
                        XtraMessageBox.Show("Thêm Thành Công");
                        getData();
            
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Vui lòng nhập mã Hóa Đơn cần Thêm");
            }
        }

        public void getCBMaNV()
        {
            DataTable tb = NhanVienBUS.getINSTANCE.DSNhanVien();
            cbbMaNV.DataSource = tb;
            cbbMaNV.DisplayMember = "maNV";
            cbbMaNV.ValueMember = "tenNV";
        }
        public void getCBMaSP()
        {
            DataTable tb = SanPhamBUS.getINSTANCE.DSSanPham();
            cbMaSP.DataSource = tb;
            cbMaSP.DisplayMember = "maSP";
            cbMaSP.ValueMember = "tenSP";
        }
        public void getCBMaKH()
        {
            DataTable tb = KhachHangBUS.getINSTANCE.DSKhachHang();
            cbbMaKH.DataSource = tb;
            cbbMaKH.DisplayMember = "maKH";
            cbbMaKH.ValueMember = "tenKH";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            setNull();
            setEnable(true);
            setButton(true,false,false,true,true);
            flag = true;
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            HoaDonBUS.getINSTANCE.deleteHoaDonBUS(txtMaHD.Text);
            XtraMessageBox.Show("Xóa Thành công");
            getData();
            binding();
        }

        public void updateHoaDon()
        {
            if (HoaDonBUS.getINSTANCE.countMaHD(txtMaHD.Text))
            {
                string maHD = txtMaHD.Text;
                string ngayLap = dtpNgayLap.Value.ToString();

                string maSP = cbMaSP.Text;
                string maNV = cbbMaNV.Text;
                string maKH = cbbMaKH.Text;
                int soLuong = (int)nudSoLuong.Value;
                HoaDon hoaDon = new HoaDon(maHD, ngayLap, maSP, maNV, maKH, soLuong);

                if (HoaDonBUS.getINSTANCE.updateHoaDonBUS(hoaDon))
                {
                    XtraMessageBox.Show("Sữa thành công", "Thông Báo");
                    getData();
                }
            }
            else
            {
                XtraMessageBox.Show("Mã hóa đơn cần sữa không tồn tại","Thông Báo");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            setEnable(true);
            setButton(false,false,true,true,true);
            flag = false;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            setEnable(false);
            setButton(true, true, true, false, false);
            getData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (flag == true)
            {
                insertHoaDon();
            }
            else
            {
                updateHoaDon();
            }
        }

        public void showCBSearch()
        {
            DataTable tb = HoaDonBUS.getINSTANCE.getData();
            cbbSearch.DataSource = tb;
            cbbSearch.DisplayMember = "maHD";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            setButton(true,true,true,false,true);
            DataTable tb = HoaDonBUS.getINSTANCE.searchHDBUS(cbbSearch.Text);
            dgvCTHD.DataSource = tb;
            binding();
        }

        //private void cbbMaKH_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    //txtTenKH.Text = cbbMaKH.SelectedValue.ToString();
        //}

        private void cbbMaNV_SelectedValueChanged(object sender, EventArgs e)
        {
            txtTenNV.Text = cbbMaNV.SelectedValue.ToString();
        }

        //private void cbMaSP_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    //txtTenSP.Text = cbMaSP.SelectedValue.ToString();
        //}

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cbbMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataRow item in KhachHangBUS.getINSTANCE.DSKhachHang().Rows)
            {
                if (item["maKH"].ToString().Equals(cbbMaKH.Text.ToString()))
                {
                    txtSDT.Text = item["sDT"].ToString();
                    txtTenKH.Text = item["tenKH"].ToString();
                    txtDiaChi.Text = item["diaChi"].ToString();

                }
            }
        }

        decimal giaban;

        private void cbMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataRow item in SanPhamBUS.getINSTANCE.DSSanPham().Rows)
            {
                if (item["maSP"].ToString().Equals(cbMaSP.Text.ToString()))
                {
                    txtTenSP.Text = item["tenSP"].ToString();
                    giaban = (int)item["giaBan"];
                    txtGiaBan.Text = giaban.ToString("N0");
                }
            }
        }
        private void txtGiaBan_OnValueChanged(object sender, EventArgs e)
        {
            giaban = Convert.ToDecimal(txtGiaBan.Text);
            decimal Tongtien = (int)(giaban * nudSoLuong.Value);
            txtTongTien.Text = Tongtien.ToString("N0") + " VND";   
        }



        private void txtGiaBan_OnValueChanged_1(object sender, EventArgs e)
        {
            giaban = Convert.ToDecimal(txtGiaBan.Text);
            decimal Tongtien = (int)(giaban * nudSoLuong.Value);
            txtTongTien.Text = Tongtien.ToString("N0") + " VND";   
        }

        private void nudSoLuong_ValueChanged(object sender, EventArgs e)
        {
            decimal Tongtien = (int)(giaban * nudSoLuong.Value);
            txtTongTien.Text = Tongtien.ToString("N0") + " VND";   
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            TongHoaDonReport hd = new TongHoaDonReport();
            hd.ShowPreviewDialog();
        }
    }
}
