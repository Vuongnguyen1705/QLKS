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
    /// Interaction logic for DichVu.xaml
    /// </summary>
    public partial class DichVu : Window
    {
        private Connection cn = new Connection();
        private DataRowView[] drv;
        private SqlCommand command;
        public DichVu()
        {
            InitializeComponent();
            ChangeListView();
        }
        private DataRowView[] getDataRowViewRightNow()
        {
            ListView lv = TableDichVu as ListView;
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
        private void TableDichVu_MouseUp(object sender, MouseButtonEventArgs e)
        {
            drv = getDataRowViewRightNow();
            if (drv != null)
            {
                DataRow @d = drv[drv.Length-1].Row;
                string @ten = (string)@d.ItemArray.GetValue(1);
                double @gia = (double)@d.ItemArray.GetValue(2);
                txtNameDV.Text = @ten;
                txtGiaDV.Text = @gia + "";
            }
        }
        private void ChangeListView()
        {
            string query = "select MaDV,TenDV,GiaDV from [HotelManagement].[dbo].[DichVu] where GiaDV != 0";
            command = cn.GetValueDatabase(query);
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);
            TableDichVu.ItemsSource = ds.Tables[0].DefaultView;
            cn.Close();
        }
        private void xoa_Click(object sender, RoutedEventArgs e)
        {
            drv = getDataRowViewRightNow();
            if (drv != null)
            {
                int[] ma = new int[drv.Length];
                for(int i=0;i < drv.Length;i++)
                {
                    ma[i] = (int) drv[i].Row.ItemArray.GetValue(0);
                    if (ma[i] < 6)
                    {
                        MessageBox.Show("ĐÓ LÀ DỊCH VỤ CƠ BẢN, BẠN KHÔNG THỂ XÓA!!");
                        return;
                    }
                }
                MessageBoxResult rs = MessageBox.Show("BẠN CÓ THẬT SỰ MUỐN XÓA?",
                    "Question",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );
                if (rs.ToString() == "Yes")
                {
                    foreach (int so in ma)
                    {
                        cn.ChangeDatabase("DELETE dichvu WHERE madv=" + so);
                    }
                    MessageBox.Show("BẠN ĐÃ XÓA DỊCH VỤ THÀNH CÔNG!!");
                    txtGiaDV.Text = "";
                    txtNameDV.Text = "";
                    ChangeListView();
                }
                return;
            }
            MessageBox.Show("XIN BẠN HÃY CHỌN DỊCH VỤ CẦN XÓA!!");
        }
        private bool kiemTraThemSua()
        {
            string @tendv = txtNameDV.Text;
            string @giadv = txtGiaDV.Text;
            if (@tendv == "" || @tendv == null)
            {
                MessageBox.Show("XIN HÃY NHẬP TÊN DỊCH VỤ!!");
                txtNameDV.Focus();
                return false;
            }
            if (@giadv == "" || @giadv == null)
            {
                MessageBox.Show("XIN HÃY NHẬP GIÁ DỊCH VỤ!!");
                txtGiaDV.Focus();
                return false;
            }
            try
            {
                double @number = double.Parse(@giadv);
            }
            catch (Exception)
            {
                MessageBox.Show("XIN NHẬP GIÁ DỊCH VỤ LÀ SỐ!!");
                txtGiaDV.Focus();
                return false;
            }
            return true;
        }
        private void them_Click(object sender, RoutedEventArgs e)
        {
            if (kiemTraThemSua())
            {
                SqlDataReader rd = cn.GetValueDatabase("SELECT *" +
                                                        " FROM DichVu" +
                                                        " WHERE TenDV=N'" + txtNameDV.Text + "'").ExecuteReader();
                if (!rd.HasRows)
                {
                    cn.Close();
                    string @query = "INSERT INTO DichVu(TenDV,GiaDV)" +
                                    " VALUES (N'" + txtNameDV.Text + "'," + txtGiaDV.Text + ")";
                    if (cn.ChangeDatabase(@query) > 0)
                    {
                        MessageBox.Show("ĐÃ THÊM DỊCH VỤ THÀNH CÔNG!!");
                        txtGiaDV.Text = "";
                        txtNameDV.Text = "";
                        ChangeListView();
                    }
                    cn.Close();
                    return;
                }
                cn.Close();
                MessageBox.Show("TÊN LOẠI HÌNH DỊCH VỤ ĐÃ TỒN TẠI!!");
            }
        }
        private void sua_Click(object sender, RoutedEventArgs e)
        {
            drv = getDataRowViewRightNow();
            if (drv != null)
            {
                if (drv.Length > 1)
                {
                    MessageBox.Show("CHỈ ĐƯỢC CHỌN 1 DỊCH VỤ ĐỂ SỬA!!!");
                    return;
                }
                if (kiemTraThemSua())
                {
                    DataRow @d = drv[0].Row;
                    int ma = (int) d.ItemArray.GetValue(0);
                    string ten = (string) d.ItemArray.GetValue(1);
                    MessageBoxResult rs = MessageBox.Show("BẠN CÓ THẬT SỰ MUỐN SỬA "+ten+"?",
                            "Question",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Warning
                        );
                    if (rs.ToString() == "Yes")
                    {
                        string query = "update dichvu" +
                                        " SET tendv = N'" + txtNameDV.Text + "'," +
                                            " GiaDV=" + txtGiaDV.Text + 
                                        " WHERE madv=" + ma;
                        if (cn.ChangeDatabase(query) > 0)
                        {
                            MessageBox.Show("BẠN ĐÃ SỬA DỊCH VỤ THÀNH CÔNG!!");
                            txtGiaDV.Text = "";
                            txtNameDV.Text = "";
                            ChangeListView();
                            return;
                        }
                        MessageBox.Show("false");
                    }
                    return;
                }
                return;
            }
            MessageBox.Show("XIN BẠN HÃY CHỌN DỊCH VỤ CẦN SỬA!!");
        }
    }
}