using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarurantManagement
{
    internal class MainClass
    {

        public static readonly string con_string = "Data Source =LAPTOP-44; Initial Catalog=Restaurant;Integrated Security=true;TrustServerCertificate=true;";
        public  static SqlConnection con = new SqlConnection(con_string);

        public static bool isValidUser(string user,string pass)
        {
            bool isValid = false;
            string query = "select * from users where username=@user and upass=@pass";
            SqlCommand cmd = new SqlCommand(query,con);
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@pass", pass);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if(dt.Rows.Count > 0)
            {
                isValid=true;
            }
            



            return isValid;
        }
    
    
    }
}
