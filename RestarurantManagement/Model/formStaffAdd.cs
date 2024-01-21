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
    public partial class formStaffAdd : SampleAdd
    {
        public formStaffAdd()
        {
            InitializeComponent();
        }

        public int id = 0;
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtPhone.Text) && cmboxRole.SelectedItem != null)
            {
                string query = "";

                if (id == 0)
                {
                    query = "insert into staff Values(@Name, @Phone, @Role)";
                }
                else
                {
                    query = "update staff Set sName = @Name, sPhone = @Phone, sRole = @Role where staffId = @id";
                }

                Hashtable ht = new Hashtable();
                ht.Add("@id", id);
                ht.Add("@Name", txtName.Text);
                ht.Add("@Phone", txtPhone.Text);
                ht.Add("@Role", cmboxRole.Text);

                if (MainClass.SQL(query, ht) > 0)
                {
                    guna2MessageDialog1.Show("Added Successfully...");
                    id = 0;
                    txtName.Text = "";
                    txtPhone.Text = "";
                    cmboxRole.SelectedIndex = -1;
                    txtName.Focus();
                }
                else
                {
                    // Handle the case where the SQL query execution fails
                    guna2MessageDialog1.Show("Failed to add/update staff. Please try again.");
                }
            }
            else
            {
                // Handle the case where required fields are not filled
                guna2MessageDialog1.Show("Please fill in all the required fields.");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
