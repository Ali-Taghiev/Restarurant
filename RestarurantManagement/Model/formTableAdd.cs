using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestarurantManagement.Model
{
    public partial class formTableAdd : SampleAdd
    {
        public formTableAdd()
        {
            InitializeComponent();
        }

        public int id = 0;

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                string query = "";

                if (id == 0)
                {
                    query = "insert into tables Values(@Name)";
                }
                else
                {
                    query = "update tables Set tname = @Name where tid = @id";
                }

                Hashtable ht = new Hashtable();
                ht.Add("@id", id);
                ht.Add("@Name", txtName.Text);

                if (MainClass.SQL(query, ht) > 0)
                {
                    guna2MessageDialog1.Show("Table added/updated successfully.");
                    id = 0;
                    txtName.Text = "";
                    txtName.Focus();
                }
                else
                {
                    // Handle the case where the SQL query execution fails
                    guna2MessageDialog1.Show("Failed to add/update table. Please try again.");
                }
            }
            else
            {
                // Handle the case where the table name is not provided
                guna2MessageDialog1.Show("Please enter the table name.");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
