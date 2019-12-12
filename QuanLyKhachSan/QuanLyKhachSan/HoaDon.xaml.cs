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
    /// Interaction logic for HoaDon.xaml
    /// </summary>
    public partial class HoaDon : Window
    {
        private Connection cn = new Connection();
        private DataRowView[] drv;
        private SqlCommand command;
        public HoaDon()
        {
            InitializeComponent();
            ChangeListView();

        }
        private void ChangeListView()
        {
            cn.Close();
            string query = "select MaNV,MaHD,SoPhong,NgayDat,NgayTra,GiaHD from HoaDon";
            command = cn.GetValueDatabase(query);
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);
            TableHoaDon.ItemsSource = ds.Tables[0].DefaultView;
            cn.Close();
        }
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ListViewItem_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void TableHoaDon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
