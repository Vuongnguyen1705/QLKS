using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using QuanLyKhachSan.ViewModel;

namespace QuanLyKhachSan
{
    /// <summary>
    /// Interaction logic for DatPhong.xaml
    /// </summary>
    public partial class DatPhong : Window
    {
        private Connection cn = new Connection();
        private DataRowView[] drv;
        private SqlCommand cm;
        private string NGAY_SINH = "";
        private string NGAY_BAT_DAU = "";
        private string NGAY_KET_THUC = "";
        private string TONG_SO_PHONG;
        private string TONG_SO_DICH_VU;
        public string tentaikhoan { set; get; }
        public DatPhong()
        {
            InitializeComponent();
            hienThiDanhSach("select TenPhong,LoaiPhong,GiaPhong from Phong where TinhTrang=N'Trống'","phong");
            hienThiDanhSach("select TenDV,GiaDV from DichVu","dichvu");
        }
        private void hienThiDanhSach(string query,string table)
        {
            cn.Close();
            SqlCommand command = cn.GetValueDatabase(query);
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);
            switch (table)
            {
                case "phong":
                    {
                        TablePhong.ItemsSource = ds.Tables[0].DefaultView;
                        break;
                    }
                case "dichvu":
                    {
                        TableDichVu.ItemsSource = ds.Tables[0].DefaultView;
                        break;
                    }
            }
            cn.Close();
        }
        private void ComboBoxItem_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            ComboBoxItem a = sender as ComboBoxItem;
            hienThiDanhSach("select TenPhong,LoaiPhong,GiaPhong from Phong where TinhTrang=N'Trống' AND LoaiPhong=N'"+(string) a.Content+"'","phong");
        }
        private DataRowView[] getDataRowViewRightNow(string doituong)
        {
            ListView lv = (doituong == "phong") ? TablePhong as ListView : TableDichVu as ListView;
            if (lv.SelectedItems.Count < 1)
            {
                return null;
            }
            DataRowView[] drv = new DataRowView[lv.SelectedItems.Count];
            for (int i = 0; i < lv.SelectedItems.Count; i++)
            {
                drv[i] = (DataRowView) lv.SelectedItems[i];
            }
            return drv;
        }
        private void DatePicker_chonNgayBatDau(object sender, MouseButtonEventArgs e)
        {
            ganThoiGian("NGAY_BAT_DAU", sender);
        }
        private void DatePicker_chonNgayKetThuc(object sender, MouseButtonEventArgs e)
        {
            ganThoiGian("NGAY_KET_THUC",sender);
        }
        private void DatePicker_chonNgaySinh(object sender, MouseButtonEventArgs e)
        {
            ganThoiGian("NGAY_SINH", sender);
        }
        private void ganThoiGian(string ngay, object sender)
        {
            DatePicker a = sender as DatePicker;
            if (a.SelectedDate != null)
            {
                DateTime b = (DateTime)a.SelectedDate;
                string[] c = b.GetDateTimeFormats();
                switch (ngay)
                {
                    case "NGAY_KET_THUC":
                        {
                            NGAY_KET_THUC = (c[0] != NGAY_KET_THUC)  ? c[0] : NGAY_KET_THUC;
                            break;

                        }
                    case "NGAY_BAT_DAU":
                        {
                            NGAY_BAT_DAU = (c[0] != NGAY_BAT_DAU) ? c[0] : NGAY_BAT_DAU;
                            break;
                        }

                    case "NGAY_SINH":
                        {
                            NGAY_SINH = (c[0] != NGAY_SINH) ? c[0] : NGAY_SINH;
                            break;
                        }
                }
            }
        }

        private bool kiemTraRongNull(string kiemtra,string title)
        {
            if (kiemtra == "" || kiemtra == null)
            {
                MessageBox.Show(title);
                return true;
            }
            return false;
        }
        private bool kiemTraRongNull(DataRowView[] kiemtra, string title)
        {
            if (kiemtra == null)
            {
                MessageBox.Show(title);
                return true;
            }
            return false;
        }
        private string tongSo(string[] St)
        {
            string chuoi = "";
            for (int i=0;i<St.Length;i++)
            {
                chuoi += St[i];
                if (i != St.Length-1)
                {
                    chuoi += "@";
                }
            }
            return chuoi;
        }
        private double tongTien()
        {
            string[] tongphong = TONG_SO_PHONG.Split("@");
            string[] tongdichvu = TONG_SO_DICH_VU.Split("@");
            double tongtien = 0;
            cn.Close();
            SqlDataReader rd;
            foreach (string s in tongphong)
            {
                rd = cn.GetValueDatabase("select GiaPhong from Phong where TenPhong = N'" + s + "'").ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        tongtien += (double)rd.GetDouble(0);
                    }
                }
            }
            cn.Close();
            foreach (string s in tongdichvu)
            {
                rd = cn.GetValueDatabase("select GiaDV from DichVu where TenDV = N'" + s + "'").ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        tongtien += (double)rd.GetDouble(0);
                    }
                }
                cn.Close();
            }
            return tongtien;
        }
        private void themKhachHang()
        {
            cn.Close();
            try
            {
                string query = "select* from KhachHang where SDT = N'" + this.sodienthoai.Text + "'";
                //MessageBox.Show(query);
                SqlDataReader rd = cn.GetValueDatabase(query).ExecuteReader();
                if (!rd.HasRows)
                {
                    query = "insert into KhachHang(HoKH,TenKH,GioiTinh,NgaySinh,QuocTich,SDT,MaPhong) " +
                        "values (N'" + this.hokhachhang.Text + "'," +
                        "N'" + this.tenkhachhang.Text + "'," +
                        "N'" + this.gioitinh.Text + "'," +
                        "'" + NGAY_SINH + "'," +
                        "N'" + this.quoctich.Text + "'," +
                        "N'" + this.sodienthoai.Text + "',3)";
                    //MessageBox.Show(query);
                    cn.Close();
                    if (cn.ChangeDatabase(query) > 0)
                    {
                        //MessageBox.Show("them kh thanh cong");
                    }
                    else
                    {
                        //MessageBox.Show("them kh khong thanh cong");
                    }
                    return;
                }
                //MessageBox.Show("ton tai kh");
                cn.Close();
            }
            catch (Exception)
            {
                //MessageBox.Show("LỖI KHÁCH HÀNG");
            }
        }
        private void themHoaDon()
        {
            
            string query = "select MaNV from TaiKhoan where TenTaiKhoan = N'" + tentaikhoan + "'";
            //MessageBox.Show(query);
            cn.Close();
            cn.Close();
            SqlDataReader rd = cn.GetValueDatabase(query).ExecuteReader();
            int manv = 0;
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    manv = (int) rd.GetInt32(0);
                }
            }
            query = "insert into HoaDon(MaNV,SoPhong,NgayDat,NgayTra, GiaHD) " +
                "values (" + manv + ",'" + TONG_SO_PHONG + "','" + NGAY_BAT_DAU + "','" + NGAY_KET_THUC + "'," + tongTien() + ")";
            //MessageBox.Show(query);
            
            cn.Close();
            cn.ChangeDatabase(query);
        }
        private void themCTHD(string sdt)
        {
            int makh = 0;
            string query = "select MaKH from KhachHang where SDT = N'" + sdt + "'";
            //MessageBox.Show(query);
            cn.Close();
            SqlDataReader rd = cn.GetValueDatabase(query).ExecuteReader();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    makh = rd.GetInt32(0);
                }
            }
            int mahd = 0;
            query = "select max(MaHD) from hoadon";
            //MessageBox.Show(query);
            cn.Close();
            rd = cn.GetValueDatabase(query).ExecuteReader();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    mahd = rd.GetInt32(0);
                }
            }            try
            {
                query = "insert into HoaDon(MaKH,MaHD,DanhSachDichVu) " +
               "values (" + makh + "," + mahd + ",'" + TONG_SO_DICH_VU + ")";
                //MessageBox.Show(query);
                cn.Close();
                cn.ChangeDatabase(query);
            }
            catch (Exception)
            {
                //MessageBox.Show("LỖI CT HÓA ĐƠN");
            }
            cn.Close();   
        }
        private void Button_Click_datPhong(object sender, RoutedEventArgs e)
        {
            string hokhachhang = this.hokhachhang.Text;
            if (kiemTraRongNull(hokhachhang, "XIN NHẬP HỌ KHÁCH HÀNG!!")) 
            { 
                this.hokhachhang.Focus();
                return;
            }
            string tenkhachhang = this.tenkhachhang.Text;
            if (kiemTraRongNull(tenkhachhang, "XIN NHẬP TÊN KHÁCH HÀNG!!")) 
            { 
                this.tenkhachhang.Focus();  
                return; 
            }
            string gioitinh = this.gioitinh.Text;
            if (kiemTraRongNull(gioitinh, "XIN NHẬP GIỚI TÍNH KHÁCH HÀNG!!")) 
            { 
                this.gioitinh.Focus();  
                return; 
            }
            if (kiemTraRongNull(NGAY_SINH, "XIN CHỌN NGÀY SINH KHÁCH HÀNG!!"))
            { 
                this.ngaysinh.Focus(); 
                return; 
            }
            string quoctich = this.quoctich.Text;
            if (kiemTraRongNull(quoctich, "XIN NHẬP QUỐC TỊCH KHÁCH HÀNG!!")) 
            { 
                this.quoctich.Focus();  
                return; 
            }
            string sdt = this.sodienthoai.Text;
            if (kiemTraRongNull(sdt, "XIN NHẬP SỐ ĐIỆN THOẠI KHÁCH HÀNG!!")) 
            { 
                this.sodienthoai.Focus();  
                return; 
            }
            if (kiemTraRongNull(NGAY_BAT_DAU, "XIN CHỌN NGÀY BẮT ĐẦU ĐẶT PHÒNG!!")) 
            { 
                this.ngaybatdau.Focus(); 
                return; 
            }
            if (kiemTraRongNull(NGAY_KET_THUC, "XIN CHỌN NGÀY KẾT THÚC ĐẶT PHÒNG!!")) 
            { 
                this.ngayketthuc.Focus(); 
                return; 
            }
            if (kiemTraRongNull(getDataRowViewRightNow("phong"), "XIN CHỌN PHÒNG!!")) 
            { 
                return; 
            }
            drv = getDataRowViewRightNow("phong");
            string[] tenphong = new string[drv.Length];
            for (int i = 0; i < drv.Length; i++)
            {
                tenphong[i] = (string) drv[i].Row.ItemArray.GetValue(0);
                           
            }
            drv = getDataRowViewRightNow("dichvu");
            string[] tendichvu = (drv != null) ? new string[drv.Length] : new string[1];
            if (drv != null)
            {
                for (int i = 0; i < drv.Length; i++) 
                {
                    tendichvu[i] = (string) drv[i].Row.ItemArray.GetValue(0);
                }
            }
            else
            {
                tendichvu[0] = "Không có dịch vụ";
            }
            try
            {
                themKhachHang();
                TONG_SO_PHONG = tongSo(tenphong);
                TONG_SO_DICH_VU = tongSo(tendichvu);
                //MessageBox.Show(TONG_SO_PHONG+" "+TONG_SO_DICH_VU);
                themHoaDon();
                themCTHD(sdt);
            }
            catch (Exception)
            {
                MessageBox.Show("ĐẶT PHÒNG THÀNH CÔNG!!");
            }
            MessageBox.Show("ĐẶT PHÒNG THÀNH CÔNG!!");
        }
    }
}
