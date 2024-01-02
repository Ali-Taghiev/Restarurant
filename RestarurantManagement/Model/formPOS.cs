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
        public int MainId = 0;
        public string OrderType;
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


                    //Event for clicking ,When we click particular category it shows its products
                    btn.Click += new EventHandler(btn_Click);

                }





            }

        }

        private void btn_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button btn = (Guna.UI2.WinForms.Guna2Button)sender;

            if(btn.Text=="All Categories")
            {
                txtboxSearch.Text = "1";
                txtboxSearch.Text = "";
                return;
            }
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
                // Cast the sender to ucProduct to access its properties
                var selectedProduct = (ucProduct)sender;

                // Iterate through the rows in guna2DataGridView1
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
            // Iterate through each control in ProductsPanel
            foreach (var item in ProductsPanel.Controls)
            {
                // Cast the control to ucProduct
                var productControl = (ucProduct)item;

                // Set visibility based on whether the product name contains the search text
                productControl.Visible = productControl.pName.ToLower().Contains(txtboxSearch.Text.Trim().ToLower());
            }
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //For Serial No in DataGridView


            int count = 0;

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        private void GetTotalAmount()
        {
            double total = 0;

            lblTotal.Text = "";

            foreach (DataGridViewRow item in guna2DataGridView1.Rows)
            {
                object cellValue = item.Cells["dgvAmount"].Value;

                if (cellValue != null)
                {
                    total += double.Parse(cellValue.ToString());
                }
            }

            lblTotal.Text = total.ToString("N2");
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTotal.Text = "0.00";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            guna2DataGridView1.Rows.Clear();
            MainId = 0;
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            OrderType = "Delivery";
        }

        private void btnTakeAway_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            OrderType = "Take Away";
        }

        private void btnDinIn_Click(object sender, EventArgs e)
        {
            OrderType = "Din In";
            formTableSelection form = new formTableSelection();

            MainClass.BlurBackground(form);

            if (form.TableName != "")
            {
                lblTable.Text = form.TableName;
                lblTable.Visible = true;
            }
            else
            {
                lblTable.Text = "";
                lblTable.Visible = false;
            }
            formWaiterSelection form2 = new formWaiterSelection();

            MainClass.BlurBackground(form2);

            if (form2.WaiterName != "")
            {
                lblWaiter.Text = form2.WaiterName;
                lblWaiter.Visible = true;
            }
            else
            {
                lblWaiter.Text = "";
                lblTable.Visible = false;
            }
        }

        private void btnKOT_Click(object sender, EventArgs e)
        {
            //Save Data in database

            string query1 = ""; //Main Table
            string query2 = ""; //Detail Table

            int detailId = 0;

            if(MainId == 0)//Insert
            {
                query1 = "INSERT INTO tblMain (aDate, aTime, TableName, WaiterName, status, orderType, total, received, change) VALUES (@aDate, @aTime, @TableName, @WaiterName, @status, @orderType, @total, @received, @change); SELECT SCOPE_IDENTITY()";

            }
            else //Update
            {
                query1 = "Update tblMain Set status=@status,total=@total,received=@received,change=@change where MainId=@ID";

            }

           
            
            SqlCommand cmd = new SqlCommand(query1,MainClass.con);

            cmd.Parameters.AddWithValue("@ID", MainId);
            cmd.Parameters.AddWithValue("@aDate", Convert.ToDateTime(DateTime.Now.Date));
            cmd.Parameters.AddWithValue("@aTime", DateTime.Now.ToShortTimeString());
            cmd.Parameters.AddWithValue("@TableName", lblTable.Text);
            cmd.Parameters.AddWithValue("@WaiterName", lblWaiter.Text);
            cmd.Parameters.AddWithValue("@status", "Pending");
            cmd.Parameters.AddWithValue("@orderType", OrderType);
            cmd.Parameters.AddWithValue("@total", Convert.ToDouble(lblTotal.Text)); //as we onyl saving data for kitvhen value will update when  payment received
            cmd.Parameters.AddWithValue("@received", Convert.ToDouble(0));
            cmd.Parameters.AddWithValue("@change", Convert.ToDouble(0));
                
            if(MainClass.con.State == ConnectionState.Closed) { MainClass.con.Open(); }

            if (MainId == 0) { MainId=Convert.ToInt32(cmd.ExecuteScalar()); } else { cmd.ExecuteNonQuery(); }

            if (MainClass.con.State == ConnectionState.Open) { MainClass.con.Close(); }


            foreach (DataGridViewRow row  in guna2DataGridView1.Rows)
            {
                detailId = Convert.ToInt32(row.Cells["dgvid"].Value);
                if (detailId == 0)
                {
                    query2 = "INSERT INTO tblDetails (MainId, proID, qty, price, amount) VALUES (@MainId, @proID, @qty, @price, @amount)";

                }
                else
                {
                    query2 = "Update tblDetails Set proID=@proID,qty=@qty,price=@price,amount=@amount where DetailID=@ID";
                }

                SqlCommand cmd2 = new SqlCommand(query2, MainClass.con);
                cmd2.Parameters.AddWithValue("@ID", detailId);
                cmd2.Parameters.AddWithValue("@MainID", MainId);
                cmd2.Parameters.AddWithValue("@proID", Convert.ToInt32(row.Cells["dgvproID"].Value));
                cmd2.Parameters.AddWithValue("@qty", Convert.ToInt32(row.Cells["dgvQty"].Value));
                cmd2.Parameters.AddWithValue("@price", Convert.ToDouble(row.Cells["dgvPrice"].Value));
                cmd2.Parameters.AddWithValue("@amount", Convert.ToDouble(row.Cells["dgvAmount"].Value));



                if (MainClass.con.State == ConnectionState.Closed) { MainClass.con.Open(); }

                 cmd2.ExecuteNonQuery(); 

                if (MainClass.con.State == ConnectionState.Open) { MainClass.con.Close(); }

                
            }

            guna2MessageDialog1.Show("Saved Succesfully..");

            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTotal.Text = "0.00";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            guna2DataGridView1.Rows.Clear();
            MainId = 0;
            detailId = 0;

        }
        public int id = 0;
        private void btnBill_Click(object sender, EventArgs e)
        {
            formBillList form = new formBillList();
            MainClass.BlurBackground(form);

            if (form.MainID > 0)
            {
                id = form.MainID;
                LoadEntries();
            }
        }
        private void LoadEntries()
        {
            string query = @"Select * from tblMain m inner join tblDetails d on m.MainID=d.MainID inner join products p on p.productId=d.proID where m.MainID= "+id+" ";
            
            SqlCommand cmd = new SqlCommand(query,MainClass.con);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            if(dt.Rows[0]["orderType"].ToString()=="Delivery") 
            {
            btnDelivery.Checked = true;
                lblWaiter.Visible = false;
                lblTable.Visible=false;
            }
            else if (dt.Rows[0]["orderType"].ToString() == "Take away")
            {
                btnTakeAway.Checked = true;
                lblWaiter.Visible = false;
                lblTable.Visible = false;
            }
            else
            {
                btnDinIn.Checked = true;
                lblWaiter.Visible = true;
                lblTable.Visible = true;
            }



            guna2DataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                lblTable.Text = item["TableName"].ToString();
                lblWaiter.Text = item["WaiterName"].ToString();

                string detailid = item["DetailID"].ToString();
                string proName = item["pName"].ToString();
                string proid = item["proID"].ToString();
                string qty = item["qty"].ToString();
                string price = item["price"].ToString();
                string amount = item["amount"].ToString();

                object[] obj = { 0,detailid, proid,proName, qty, price, amount };
                guna2DataGridView1.Rows.Add(obj);   

            }
            GetTotalAmount();
                
        }

        private void CheckOut_Click(object sender, EventArgs e)
        {

            formCheckOut form = new formCheckOut();
            form.MainID = id;
            form.amt = Convert.ToDouble(lblTotal.Text);
            MainClass.BlurBackground(form);

            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTotal.Text = "0.00";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            guna2DataGridView1.Rows.Clear();
            MainId = 0;
            
        }
    }
}
