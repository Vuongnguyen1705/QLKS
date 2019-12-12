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
    /// Interaction logic for TraPhong.xaml
    /// </summary>
    public partial class TraPhong : Window
    {
        private Connection cn = new Connection();
        private DataRowView[] drv;
        private SqlCommand command;
        private MainWindow main;
        public TraPhong(MainWindow main)
        {
            InitializeComponent();

            ChangeListView();
            this.main = main;
        }
        private void ChangeListView()
        {
            string query = "select TenPhong,GiaPhong,DichVu.GiaDV,TenNV " +
                            "from NhanVien,Phong,DichVu " +
                            "where NhanVien.MaNV = Phong.MaNV AND" +
                                    " Phong.MaDV = DichVu.MaDV AND " +
                                    " Phong.TinhTrang != N'Trống'";
            SqlCommand command = cn.GetValueDatabase(query);
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);
            TableTraPhong.ItemsSource = ds.Tables[0].DefaultView;
            cn.Close();
        }
        private DataRowView[] getDataRowViewRightNow()
        {
            ListView lv = TableTraPhong as ListView;
            if (lv.SelectedItems.Count < 1)
            {
                return null;
            }
            DataRowView[] drv = new DataRowView[lv.SelectedItems.Count];
            for (int i = 0; i < lv.SelectedItems.Count; i++)
            {
                drv[i] = (DataRowView)lv.SelectedItems[i];
            }
            return drv;
        }

        private void Button_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            drv = getDataRowViewRightNow();
            if (drv != null)
            {
                string[] ma = new string[drv.Length];
                for (int i = 0; i < drv.Length; i++)
                {
                    ma[i] = (string)drv[i].Row.ItemArray.GetValue(0);
                }
                MessageBoxResult rs = MessageBox.Show("BẠN CÓ THẬT SỰ MUỐN TRẢ PHÒNG?",
                    "Question",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );
                if (rs.ToString() == "Yes")
                {
                    foreach (string so in ma)
                    {
                        cn.ChangeDatabase("UPDATE Phong SET TinhTrang = N'Trống' WHERE TenPhong=N'"+so+"'");
                    }
                    MessageBox.Show("BẠN ĐÃ TRẢ PHÒNG THÀNH CÔNG!!");
                    hienThiSLPhongHienTai("Trống");
                    hienThiSLPhongHienTai("Đã Đặt");
                    this.main.hienthi();
                    this.main.hienThiSLPhongHienTai("Trống");
                    this.main.hienThiSLPhongHienTai("Đã Đặt");
                    ChangeListView();
                }
                return;
            }
            MessageBox.Show("XIN BẠN HÃY PHÒNG CẦN TRẢ!");
        }
        private void hienThiSLPhongHienTai(string tinhtrang)
        {
            tinhtrang = (tinhtrang != "Trống") ? "Đã đặt" : tinhtrang;
            SqlDataReader rd = cn.GetValueDatabase("select count(*) as 'tongsl' from Phong where TinhTrang = N'" + tinhtrang + "'").ExecuteReader();
            int sl = 0;
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    sl = rd.GetInt32(0);
                }
            }
            if (tinhtrang == "Trống")
            {
                main.phongtrong.Text = sl + "";
            }
            else
            {
                main.phongdadat.Text = sl + "";
            }
            cn.Close();
        }
    }
}
