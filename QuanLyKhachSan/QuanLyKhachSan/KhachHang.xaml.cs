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
    /// Interaction logic for KhachHang.xaml
    /// </summary>
    public partial class KhachHang : Window
    {
        private Connection cn = new Connection();
        private DataRowView[] drv;
        private SqlCommand command;
        public KhachHang()
        {
            InitializeComponent();
            ChangeListView();
        }
        private void ChangeListView()
        {
            string query = "select MaKH,HoKH,TenKH,NgaySinh,GioiTinh,QuocTich,SDT,MaPhong from KhachHang";
            command = cn.GetValueDatabase(query);
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);
            TableNhaKHien.ItemsSource = ds.Tables[0].DefaultView;
            cn.Close();
        }
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ListViewItem_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
