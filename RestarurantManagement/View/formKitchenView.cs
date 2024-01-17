using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestarurantManagement.View
{
    public partial class formKitchenView : Form
    {
        public formKitchenView()
        {
            InitializeComponent();
        }

        private void formKitchenView_Load(object sender, EventArgs e)
        {
            GetOrders();
        }

        private void GetOrders()
        {
            flowLayoutPanel1.Controls.Clear();

            string query1 = @"Select * from tblMain where status = 'Pending' ";
            SqlCommand cmd = new SqlCommand(query1,MainClass.con);
            DataTable dt = new DataTable(); 
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            FlowLayoutPanel p1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                p1 = new FlowLayoutPanel();
                p1.AutoSize = true;
                p1.Width = 230; 
                p1.Height = 350;
                p1.FlowDirection = FlowDirection.TopDown;
                p1.BorderStyle = BorderStyle.FixedSingle;
                p1.Margin =  new Padding(10,10, 10, 10);

                FlowLayoutPanel p2 = new FlowLayoutPanel();
                p2.BackColor = Color.FromArgb(50, 55, 89);
                p2.AutoSize = true;
                p2.Width = 230;
                p2.Height = 350;
                p2.FlowDirection = FlowDirection.TopDown;
                p2.Margin = new Padding(0,0,0,0);

                Label lb1 = new Label();
                lb1.ForeColor = Color.White;
                lb1.Margin = new Padding(10, 10, 3, 0);
                lb1.AutoSize = true;


                Label lb2 = new Label();
                lb2.ForeColor = Color.White;
                lb2.Margin = new Padding(10, 5, 3, 0);
                lb2.AutoSize = true;

                Label lb3 = new Label();
                lb3.Margin = new Padding(10, 5, 3, 0);
                lb3.AutoSize = true;
                lb3.ForeColor = Color.White;

                Label lb4 = new Label();
                lb4.Margin = new Padding(10, 5, 3, 10);
                lb4.AutoSize = true;
                lb4.ForeColor = Color.White;


                lb1.Text ="Table : " + dt.Rows[i]["TableName"].ToString();

                lb2.Text = "Waiter Name : " + dt.Rows[i]["WaiterName"].ToString();
                 
                lb3.Text = "Order Time : " + dt.Rows[i]["aTime"].ToString();

                lb4.Text = "Order Type: " + dt.Rows[i]["orderType"].ToString();

                p2.Controls.Add(lb1);
                p2.Controls.Add(lb2);
                p2.Controls.Add(lb3);
                p2.Controls.Add(lb4);

                p1.Controls.Add(p2);
               



                //Add products 

                int mid = 0;
                mid = Convert.ToInt32(dt.Rows[i]["MainID"].ToString());

                string query2 = "Select * from tblMain m  inner join tblDetails d on m.MainID=d.MainID inner join products p on p.productId=d.proID where m.MainID=" + mid + "";

                SqlCommand cmd2 = new SqlCommand(query2, MainClass.con);
                DataTable dt2 = new DataTable();
                SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                adapter2.Fill(dt2);
                for (int j = 0; j < dt2.Rows.Count; j++)
                {

                    Label lb5 = new Label();
                    lb5.Margin = new Padding(10, 5, 3, 0);
                    lb5.AutoSize = true;
                    lb5.ForeColor = Color.Black;

                    int no = j + 1;
                    lb5.Text = "" + no + " " + dt2.Rows[j]["pName"].ToString() + " " + dt2.Rows[j]["qty"].ToString();

                    p1.Controls.Add(lb5);
                    
                }

                //Button to change order status 
                Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                b.AutoRoundedCorners = true;
                b.Size = new Size(100, 35);
                b.FillColor = Color.FromArgb(241, 85, 126);
                b.Margin = new Padding(30, 5, 3, 10);
                b.Text = "Complete";
                b.Tag = dt.Rows[i]["MainID"].ToString(); //store the id

                b.Click += new EventHandler(b_Click);

                 p1.Controls.Add(b);

                flowLayoutPanel1.Controls.Add(p1);
            }     



        }

        private void b_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32((sender as Guna.UI2.WinForms.Guna2Button).Tag.ToString());

            guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question;
            guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
            if (guna2MessageDialog1.Show("Are you sure you want to complete ?") == DialogResult.Yes)
            {
                string query = @"Update tblMain Set status ='Complete' where MainID=@ID";

               Hashtable ht = new Hashtable();
                ht.Add("@ID", id);

                if (MainClass.SQL(query, ht)>0)
                {
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Show("Saved Succesfully...");
                    GetOrders();

                }
            
            }


           
        }
    }
}
