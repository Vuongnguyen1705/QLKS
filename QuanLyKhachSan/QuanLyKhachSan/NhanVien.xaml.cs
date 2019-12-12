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
    /// Interaction logic for NhanVien.xaml
    /// </summary>
    public partial class NhanVien : Window
    {
        public NhanVien()
        {
            InitializeComponent();
            string query = "select MaNV, HoNV, TenNV, NgaySinh, GioiTinh ,DiaChi, Luong, ChucVu from [HotelManagement].[dbo].[NhanVien] where TenNV != N'Không'";
            var con = Connection.Instance;
            SqlCommand command = new SqlCommand(query);
            command.Connection = con;
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);
            TableNhanVien.ItemsSource = ds.Tables[0].DefaultView;
        }

        private void TableDichVu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
