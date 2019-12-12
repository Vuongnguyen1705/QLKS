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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
 
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Thoat(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void Button_Login(object sender, RoutedEventArgs e)
        {
            if (TryLogin(TenDangNhap.Text, MatKhau.Password))
            {
                var _nameMainWindow = new MainWindow();
                _nameMainWindow.Show();
                _nameMainWindow.tentaikhoan = TenDangNhap.Text;
                string s = "select * " +
                    "from TaiKhoan where TenTaiKhoan = N'"+ TenDangNhap.Text + "' and Quyen != N'Quản Trị'";
                Connection cn = new Connection();
                SqlDataReader rd = cn.GetValueDatabase(s).ExecuteReader();
                if (rd.HasRows)
                {
                    _nameMainWindow.kh.IsEnabled = false;
                    _nameMainWindow.nv.IsEnabled = false;
                    _nameMainWindow.dv.IsEnabled = false;
                    _nameMainWindow.nd.IsEnabled = false;
                }
                cn.Close();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai thông tin");
            }
        }

        private bool TryLogin(string Username, string Password )
        {
            string query = @"Select * from [TaiKhoan] where [TenTaiKhoan] = '" + Username + "' And [MatKhau] = '" + Password + "'";
            var _ = Connection.DoQuery(query);
            if (_.Rows.Count == 0)
                return false;
            var _username = _.Rows[0]["TenTaiKhoan"];

            return true;
        }
    }
}
