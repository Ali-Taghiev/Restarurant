using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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

            ProductsPanel.Controls.Clear();
            LoadProducts();
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

        private void AddItems(int id,string name ,string cat ,string price,Image pImage)
        {
            var w = new ucProduct()
            {
                pName = name,
                pCategory = cat,
                pPrice = price,
                pImage = pImage,
                id = Convert.ToInt32(id)
            };
            ProductsPanel.Controls.Add(w);
            w.onSelect += (ss, ee) =>
            {
                var wdg = (ucProduct)ss;

                foreach (DataGridViewRow item in guna2DataGridView1.Rows)
                {
                    // Check if the current product in guna2DataGridView1 is the same as the selected product (wdg)
                    // If true, increment the quantity and update the total amount

                    if (Convert.ToInt32(item.Cells["dgvid"].Value) == wdg.id)
                    {
                        item.Cells["dgvQty"].Value = int.Parse(item.Cells["dgvQty"].ToString() + 1);

                        item.Cells["dgvAmount"].Value = 
                        int.Parse(item.Cells["dgvQty"].ToString()) *
                            double.Parse(item.Cells["dgvPrice"].ToString());
                    }

                    //This line add new product

                    guna2DataGridView1.Rows.Add(new object[] {0,wdg.id,wdg.pName,1,wdg.pPrice,wdg.pPrice});
                }
            };

        }

        private void LoadProducts()
        {
            string query = "select * from products  inner join category  on catID = CategoryID";

            SqlCommand cmd = new SqlCommand(query, MainClass.con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            foreach (DataRow item in dt.Rows)
            {
                Byte[] imageArray = (byte[])item["pImage"];
                byte[] imagebytearray = imageArray;

                AddItems(Convert.ToInt32(item["productId"].ToString()), item["pName"].ToString(), item["catName"].ToString(),
                    item["pPrice"].ToString(),Image.FromStream(new MemoryStream(imageArray)));
            }

        }

        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            foreach (var item in ProductsPanel.Controls)
            {

                var p = (ucProduct)item;
                p.Visible = p.pName.ToLower().Contains(txtboxSearch.Text.Trim().ToLower());
            }
        }
    }
}
