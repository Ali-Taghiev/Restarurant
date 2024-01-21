using System;
using System.Collections;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace RestarurantManagement.Model
{
    public partial class formPOS : Form
    {
        // Properties to store POS data
        public int MainId = 0;
        public int driverId = 0;
        public string OrderType = "";
        public string customerName = "";
        public string customerPhone = "";

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
            // Set up POS form
            guna2DataGridView1.BorderStyle = BorderStyle.FixedSingle;
            AddCategory();
            ProductsPanel.Controls.Clear();
            LoadProducts();
        }

        private void AddCategory()
        {
            // Populate categories in the POS form

            string query = "select * from category";
            SqlCommand cmd = new SqlCommand(query, MainClass.con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            CategoryPanel.Controls.Clear();

            if (dt.Rows.Count > 0)
            {
                int buttonHeight = 40;
                int spacing = 5;
                int yPos = 45;

                foreach (DataRow row in dt.Rows)
                {
                    // Create category buttons dynamically
                    Guna.UI2.WinForms.Guna2Button btn = new Guna.UI2.WinForms.Guna2Button();
                    btn.FillColor = Color.FromArgb(50, 55, 89);
                    btn.Size = new Size(130, buttonHeight);
                    btn.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
                    btn.Text = row["catName"].ToString();
                    btn.Location = new Point(0, yPos);
                    CategoryPanel.Controls.Add(btn);

                    // Event for clicking on a category to filter products
                    btn.Click += new EventHandler(btn_Click);
                    yPos += buttonHeight + spacing;
                }
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            // Handle category button click event
            Guna.UI2.WinForms.Guna2Button btn = (Guna.UI2.WinForms.Guna2Button)sender;

            if (btn.Text == "All Categories")
            {
                txtboxSearch.Text = "1";
                txtboxSearch.Text = "";
                return;
            }

            // Filter products based on the selected category
            foreach (var item in ProductsPanel.Controls)
            {
                var productControl = (ucProduct)item;
                productControl.Visible = productControl.pCategory.ToLower().Contains(btn.Text.ToLower());
            }
        }

        private void AddItems(int id, string proID, string name, string cat, string price, Image pImage)
        {
            // Create a new ucProduct control with the provided information
            var productControl = new ucProduct()
            {
                pName = name,
                pCategory = cat,
                pPrice = price,
                pImage = pImage,
                id = Convert.ToInt32(proID)
            };

            // Add the product control to the ProductsPanel
            ProductsPanel.Controls.Add(productControl);

            // Attach an event handler to the onSelect event of the product control
            productControl.onSelect += (sender, eventArgs) =>
            {
                // Handle product selection
                var selectedProduct = (ucProduct)sender;

                foreach (DataGridViewRow item in guna2DataGridView1.Rows)
                {
                    // Check if the product is already in the DataGridView
                    if (Convert.ToInt32(item.Cells["dgvproID"].Value) == selectedProduct.id)
                    {
                        // Increment the quantity of the existing product
                        item.Cells["dgvQty"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) + 1;
                        // Update the total amount for the existing product
                        item.Cells["dgvAmount"].Value =
                            Convert.ToString(int.Parse(item.Cells["dgvQty"].Value.ToString()) *
                            double.Parse(item.Cells["dgvPrice"].Value.ToString()));
                        return; // Exit the method if the product is found in the DataGridView
                    }
                }

                // If the product is not in the DataGridView, add a new row for the product
                guna2DataGridView1.Rows.Add(new object[] { 0, 0, selectedProduct.id, selectedProduct.pName, 1, selectedProduct.pPrice, selectedProduct.pPrice });

                GetTotalAmount();
            };
        }

        private void LoadProducts()
        {
            // Load products into the POS form

            string query = "select * from products inner join category on catID = CategoryID";
            SqlCommand cmd = new SqlCommand(query, MainClass.con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            foreach (DataRow item in dt.Rows)
            {
                // Retrieve image data from the database
                byte[] imageArray = (byte[])item["pImage"];

                // Convert the byte array to an Image
                Image productImage = Image.FromStream(new MemoryStream(imageArray));

                // Call the AddItems method to add the product to the ProductsPanel
                AddItems(0, item["productId"].ToString(), item["pName"].ToString(), item["catName"].ToString(),
                         item["pPrice"].ToString(), productImage);
            }
        }

        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            // Filter products based on the search text
            foreach (var item in ProductsPanel.Controls)
            {
                var productControl = (ucProduct)item;
                productControl.Visible = productControl.pName.ToLower().Contains(txtboxSearch.Text.Trim().ToLower());
            }
        }

        private void guna2DataGridView1_CellFormatting(object sender
