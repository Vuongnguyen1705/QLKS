using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace QuanLyKhachSan.ViewModel
{
    class Connection
    {
        public static SqlConnection conn;

        private static string connectionString = @"Server=HUNGVUONG\SQLEXPRESS;Database=HotelManagement;Trusted_Connection=True";


        public void Close()
        {
            conn.Close();
        }
        public int ChangeDatabase(string query)
        {
            conn = Instance;
            SqlCommand cm = new SqlCommand(query, conn);
            conn.Open();
            int truefalse = cm.ExecuteNonQuery();
            conn.Close();
            return truefalse;
        }
        public SqlCommand GetValueDatabase(string query)
        {
            conn = Instance;
            conn.Open();
            SqlCommand Command = new SqlCommand(query, conn);
            return Command;
        }
        public static SqlConnection Instance
        {
            get
            {
                if (conn == null)
                {
                    conn = new SqlConnection(connectionString);
                }
                return conn;
            }
        }

        public static DataTable DoQuery(string query)
        {
            SqlDataAdapter cmd = new SqlDataAdapter(query, Instance);

            DataTable dt = new DataTable();

            cmd.Fill(dt);

            Instance.Close();

            return dt;
        }
    }
}
