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
    /// Interaction logic for NguoiDung.xaml
    /// </summary>
    public partial class NguoiDung : Window
    {
        private Connection cn = new Connection();
        private DataRowView[] drv;
        private SqlCommand command;
        public string tentaikhoan { set; get; }
        public NguoiDung(string tentaikhoan)
        {
            InitializeComponent();
            this.tentaikhoan = tentaikhoan;
            ChangeListView();
        }
        private void ChangeListView()
        {
            string query = "select MaTaiKhoan,MaNV,TenTaiKhoan,MatKhau,Quyen,TinhTrang from taikhoan";
            query += " where TenTaiKhoan = N'"+this.tentaikhoan+"' or  Quyen != N'Quản Trị'";
            cn.Close();
            SqlCommand command = cn.GetValueDatabase(query);
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);
            TableNguoiDung.ItemsSource = ds.Tables[0].DefaultView;
            cn.Close();
        }
        private DataRowView[] getDataRowViewRightNow()
        {
            ListView lv = TableNguoiDung as ListView;
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

        public object TableMain { get; }

        private void TableNguoiDung_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            drv = getDataRowViewRightNow();
            if (drv != null)
            {
                DataRow @d = drv[drv.Length - 1].Row;
                string tentk = (string) @d.ItemArray.GetValue(2);
                string matkhau = (string) @d.ItemArray.GetValue(3);
                string quyen = (string) @d.ItemArray.GetValue(4);
                string tinhtrang = (string) @d.ItemArray.GetValue(5);
                int manv = (int)@d.ItemArray.GetValue(1);
                this.tentk.Text = tentk+"";
                this.matkhau.Text = matkhau;
                this.quyen.Text = quyen;
                this.tinhtrang.Text = tinhtrang;
                this.manhanvien.Text = manv+"";
            }
        }
        private bool kiemTraNullRong(string titol,string main)
        {
            if (main == "" || main == null)
            {
                MessageBox.Show(titol);
                return true;
            }
            return false;
        }

        private bool kiemTraThemSua()
        {
            if (kiemTraNullRong("XIN NHẬP TÊN TÀI KHOẢN!!", tentk.Text))
            {
                tentk.Focus();
                return false;
            }
            if (kiemTraNullRong("XIN NHẬP MẬT KHẨU TÀI KHOẢN!!", matkhau.Text))
            {
                matkhau.Focus();
                return false;
            }
            if (kiemTraNullRong("XIN NHẬP QUYỀN TÀI KHOẢN!!", quyen.Text))
            {
                quyen.Focus();
                return false;
            }
            if (kiemTraNullRong("XIN NHẬP TÌNH TRẠNG TÀI KHOẢN!!", tinhtrang.Text))
            {
                tinhtrang.Focus();
                return false;
            }
            if (kiemTraNullRong("XIN NHẬP MÃ NHÂN VIÊN!!", manhanvien.Text))
            {
                manhanvien.Focus();
                return false;
            }
            try
            {
                double db = Double.Parse(this.manhanvien.Text);
            }
            catch (Exception)
            {
                manhanvien.Focus();
                return false;
            }
            cn.Close();
            SqlDataReader rd = cn.GetValueDatabase("select * from NhanVien where MaNV=N'"+ manhanvien.Text + "'").ExecuteReader();
            if (!rd.HasRows)
            {
                MessageBox.Show("KHÔNG TỒN TẠI MÃ NHÂN VIÊN !!!");
                manhanvien.Focus();
                return false;
            }
            cn.Close();
            return true;
        }
        private void Button_Them(object sender, MouseButtonEventArgs e)
        {
            if (kiemTraThemSua())
            {
                cn.Close();
                SqlDataReader rd = cn.GetValueDatabase("SELECT *" +
                                                        " FROM TaiKhoan" +
                                                        " WHERE TenTaiKhoan=N'" + this.tentk.Text + "'").ExecuteReader();
                if (rd.HasRows)
                {
                    MessageBox.Show("ĐÃ TỒN TẠI TÊN TÀI KHOẢN!!");
                    return;
                }
                cn.Close();
                if (cn.ChangeDatabase("insert into TaiKhoan(MaNV,TenTaiKhoan,MatKhau,Quyen,TinhTrang) " +
                    "values (" + this.manhanvien.Text + ",N'" + this.tentk.Text + "',N'" + this.matkhau.Text + "',N'" + this.quyen.Text + "',N'" + this.tinhtrang.Text + "')") > 0)
                {
                    MessageBox.Show("THÊM TÀI KHOẢN THÀNH CÔNG!!");
                    ChangeListView();
                    return;
                }
                MessageBox.Show("THÊM TÀI KHOẢN KHÔNG THÀNH CÔNG!!");
            }
        }

        private void Button_Xoa(object sender, MouseButtonEventArgs e)
        {
            drv = getDataRowViewRightNow();
            if (drv != null)
            {
                int[] ma = new int[drv.Length];
                for (int i = 0; i < drv.Length; i++)
                {
                    ma[i] = (int)drv[i].Row.ItemArray.GetValue(0);
                    String quyen = (string) drv[i].Row.ItemArray.GetValue(4);
                    if (quyen == "Quản Trị")
                    {
                        MessageBox.Show("BẠN KHÔNG THỂ XÓA CHÍNH BẠN!!");
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
                        cn.ChangeDatabase("DELETE TaiKhoan WHERE MaTaiKhoan=" + so);
                    }
                    MessageBox.Show("BẠN ĐÃ XÓA DỊCH VỤ THÀNH CÔNG!!");
                    this.tentk.Text = "";
                    this.quyen.Text = "";
                    this.tinhtrang.Text = "";
                    this.matkhau.Text = "";
                    this.manhanvien.Text = "";
                    ChangeListView();
                }
                return;
            }
            MessageBox.Show("XIN BẠN HÃY CHỌN TÀI KHOẢN CẦN XÓA!!");
        }

        private void Button_Sua(object sender, MouseButtonEventArgs e)
        {
            drv = getDataRowViewRightNow();
            if (drv != null)
            {
                if (drv.Length > 1)
                {
                    MessageBox.Show("CHỈ ĐƯỢC CHỌN 1 TÀI KHOẢN ĐỂ SỬA!!!");
                    return;
                }
                if (kiemTraThemSua())
                {
                    DataRow d = drv[0].Row;
                    int matk = (int)d.ItemArray.GetValue(0);
                    string tentk = (string)d.ItemArray.GetValue(2);
                    MessageBoxResult rs = MessageBox.Show("BẠN CÓ THẬT SỰ MUỐN SỬA " + tentk + "?",
                            "Question",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Warning
                        );
                    if (rs.ToString() == "Yes")
                    {
                        string query = "update TaiKhoan" +
                                        " SET TenTaiKhoan = N'" + this.tentk.Text + "'," +
                                            " Quyen=N'" + this.quyen.Text + "', " +
                                            " TinhTrang=N'"+ this.tinhtrang.Text + "'," +
                                            " MatKhau=N'" + this.matkhau.Text + "'," +
                                            " MaNV=N'" + this.manhanvien.Text + "'" +
                                        " WHERE MaTaiKhoan=" + matk;
                        if (cn.ChangeDatabase(query) > 0)
                        {
                            MessageBox.Show("BẠN ĐÃ SỬA TÀI KHOẢN THÀNH CÔNG THÀNH CÔNG!!");
                            this.tentk.Text = "";
                            this.quyen.Text = "";
                            this.tinhtrang.Text = "";
                            this.matkhau.Text = "";
                            this.manhanvien.Text = "";
                            ChangeListView();
                            return;
                        }
                        MessageBox.Show("false");
                    }
                    return;
                }
                return;
            }
            MessageBox.Show("XIN BẠN HÃY CHỌN TÀI KHOẢN CẦN SỬA!!");
        }

    }
}
