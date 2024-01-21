using System;

using System.Data;
using System.Data.SqlClient;
using System.Drawing;

using System.Windows.Forms;

namespace RestarurantManagement.Model
{
    public partial class formWaiterSelection : Form
    {
        public string WaiterName;

        public formWaiterSelection()
        {
            InitializeComponent();
        }

        private void formWaiterSelection_Load(object sender, EventArgs e)
        {
            // Retrieve waiter data from the database
            string query = "select * from staff where sRole Like 'Waiter'";
            SqlCommand cmd = new SqlCommand(query, MainClass.con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            // Display buttons for each waiter
            foreach (DataRow row in dt.Rows)
            {
                Guna.UI2.WinForms.Guna2Button btn = new Guna.UI2.WinForms.Guna2Button();
                btn.Text = row["sName"].ToString();
                btn.Width = 150;
                btn.Height = 50;
                btn.FillColor = Color.FromArgb(241, 85, 126);
                btn.HoverState.FillColor = Color.FromArgb(50, 55, 89);
                btn.Click += new EventHandler(btn_Click);
                flowLayoutPanel1.Controls.Add(btn);
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            // Set WaiterName to the text of the clicked button and close the form
            WaiterName = (sender as Guna.UI2.WinForms.Guna2Button).Text.ToString();
            this.Close();
        }
    }
}
