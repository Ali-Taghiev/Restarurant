using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestarurantManagement
{
    internal class MainClass
    {

        public static readonly string con_string = "Data Source =LAPTOP-44; Initial Catalog=Restaurant;Integrated Security=true;TrustServerCertificate=true;";
        public static SqlConnection con = new SqlConnection(con_string);

        public static bool isValidUser(string user, string pass)
        {
            bool isValid = false;
            string query = "select * from users where username=@user and upass=@pass";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@pass", pass);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                isValid = true;
                USER = dt.Rows[0]["uName"].ToString();

            }




            return isValid;
        }
        public static string user;
        public static string USER
        {
            get { return user; }
            private set { user = value; }
        }


        //Method for CRUD operations

        public static int SQL(string query, Hashtable ht)
        {
            int result = 0;

            try
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;

                foreach (DictionaryEntry item in ht)
                {
                    cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                }
                if (con.State == ConnectionState.Closed) { con.Open(); }
                result = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open) { con.Close(); }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                con.Close();
            }

            return result;
        }

        // For Loading Data from DATabase

        public static void LoadData(string query, DataGridView dgv, ListBox lb)
        {

            //Serial no in Datagridview

            dgv.CellFormatting += new DataGridViewCellFormattingEventHandler(dgv_CellFormatting);
            try
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);

                for (int i = 0; i < lb.Items.Count; i++)
                {
                    string columnName = ((DataGridViewColumn)lb.Items[i]).Name;
                    dgv.Columns[columnName].DataPropertyName = dt.Columns[i].ToString();
                }
                dgv.DataSource = dt;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                con.Close();
            }


        }
        private static void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            Guna.UI2.WinForms.Guna2DataGridView dgv = (Guna.UI2.WinForms.Guna2DataGridView)sender;
            int count = 0;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        //Blur background when other form opened
        public static void BlurBackground(Form Model)
        {
            Form Background = new Form();
            using (Model)
            {

                Background.StartPosition = FormStartPosition.Manual;
                Background.FormBorderStyle = FormBorderStyle.None;
                Background.Opacity = 0.5d;
                Background.BackColor = Color.Black;
                Background.Size = formMain.Instance.Size;
                Background.Location = formMain.Instance.Location;
                Background.ShowInTaskbar = false;
                Background.Show();
                Model.Owner = Background;
                Model.ShowDialog(Background);
                Background.Dispose();
            }
        }


        //Fill ComboBoxes

        public static void ComboBoxFill(string query , ComboBox cb)
        {
            SqlCommand cmd = new SqlCommand(query,con);
                cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cb.DisplayMember = "name";
            cb.ValueMember = "id";
            cb.DataSource = dt;
            cb.SelectedIndex = -1;

        }

    }
}
