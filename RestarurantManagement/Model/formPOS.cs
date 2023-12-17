using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestarurantManagement.Model
{
    public partial class formPOS : Form
    {
        public formPOS()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void formPOS_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.BorderStyle = BorderStyle.FixedSingle;
            AddCategory();
        }

        private void AddCategory()
        {
           
            

            string query = "select * from category";
            SqlCommand cmd  = new SqlCommand(query,MainClass.con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            CategoryPanel.Controls.Clear();


            if(dt.Rows.Count > 0)
            {

                int buttonHeight = 40;
                int spacing = 5;
                int yPos = 0;

                foreach (DataRow row in dt.Rows)
                {
                    Guna.UI2.WinForms.Guna2Button btn = new Guna.UI2.WinForms.Guna2Button();
                    btn.FillColor = Color.FromArgb(50, 55, 89);
                    btn.Size = new Size(130, buttonHeight);
                    btn.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
                    btn.Text = row["catName"].ToString();

                    // Set button location based on yPos
                    btn.Location = new Point(0, yPos);

                    CategoryPanel.Controls.Add(btn);

                    // Increment yPos for the next button
                    yPos += buttonHeight + spacing;

                    
                }





            }

        }
    }
}
