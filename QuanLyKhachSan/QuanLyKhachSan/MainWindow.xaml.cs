using QuanLyKhachSan.ViewModel;
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

namespace QuanLyKhachSan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public object TableNhanVien { get; }
        private Connection cn = new Connection();
        public string tentaikhoan { set; get; }
        public MainWindow()
        {
            InitializeComponent();
            hienthi();
            hienThiSLPhongHienTai("Trống");
            hienThiSLPhongHienTai("Đã đặt");
        }
        public void hienthi()
        {
            string query = "select MaPhong,TenPhong,LoaiPhong,GiaPhong,TinhTrang from [HotelManagement].[dbo].[Phong]";
            var con = Connection.Instance;
            SqlCommand command = new SqlCommand(query);
            command.Connection = con;
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);
            TableMain.ItemsSource = ds.Tables[0].DefaultView;
        }
        public void hienThiSLPhongHienTai(string tinhtrang)
        {
            tinhtrang = (tinhtrang != "Trống") ? "Đã đặt" : tinhtrang;
            SqlDataReader rd = cn.GetValueDatabase("select count(*) as 'tongsl' from Phong where TinhTrang = N'" + tinhtrang + "'").ExecuteReader();
            int sl = 0 ;
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    sl = rd.GetInt32(0);
                }
            }
            if (tinhtrang == "Trống")
            {
                phongtrong.Text = sl + "";
            }
            else
            {
                phongdadat.Text = sl + "";
            }
            cn.Close();
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Button_DichVu(object sender, RoutedEventArgs e)
        {
            var _DichVu = new DichVu();
            _DichVu.ShowDialog();
        }

        private void Button_NhanVien(object sender, RoutedEventArgs e)
        {
            NhanVien NhanVien = new NhanVien();
            NhanVien.ShowDialog();
        }

        private void Button_DatPhong(object sender, RoutedEventArgs e)
        {
            DatPhong dp = new DatPhong();
            dp.tentaikhoan = this.tentaikhoan;
            dp.ShowDialog();
            
        }

        private void Button_TraPhong(object sender, RoutedEventArgs e)
        {
            TraPhong tp = new TraPhong(this);
            tp.ShowDialog();
        }

        private void Button_HoaDon(object sender, RoutedEventArgs e)
        {
            HoaDon hd = new HoaDon();
            hd.ShowDialog();
        }

        private void Button_NguoiDung(object sender, RoutedEventArgs e)
        {
            NguoiDung nd = new NguoiDung(this.tentaikhoan);
            nd.ShowDialog();
        }

        private void Button_KhachHang(object sender, RoutedEventArgs e)
        {
            KhachHang kh = new KhachHang();
            kh.ShowDialog();
        }

        private void Button_DangXuat(object sender, RoutedEventArgs e)
        {
            var lg = new LoginWindow();
            lg.Show();
            mainWindow.Hide();
        }
    }
}
