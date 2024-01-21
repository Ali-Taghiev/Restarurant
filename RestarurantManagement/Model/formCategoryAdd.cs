using System;
using System.Collections;
using System.Windows.Forms;

namespace RestarurantManagement.Model
{
    public partial class formCategoryAdd : SampleAdd
    {
        public formCategoryAdd()
        {
            InitializeComponent();
        }

        private void formCategoryAdd_Load(object sender, EventArgs e)
        {
            // Code for form load event (if needed)
        }

        public int id = 0;

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            // Check if the category name is not empty
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                string query = "";

                // Determine if it's an insert or update query based on id
                if (id == 0)
                {
                    query = "insert into category Values(@Name)";
                }
                else
                {
                    query = "update category Set catName = @Name where catID = @id";
                }

                // Create a Hashtable for parameter values
                Hashtable ht = new Hashtable();
                ht.Add("@id", id);
                ht.Add("@Name", txtName.Text);

                // Execute the SQL query and check if the operation was successful
                if (MainClass.SQL(query, ht) > 0)
                {
                    // Show success message and reset fields
                    guna2MessageDialog1.Show("Added Successfully...");
                    id = 0;
                    txtName.Focus();
                }
            }
            else
            {
                // If category name is empty, focus on the text box
                txtName.Focus();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Close the form when the Exit button is clicked
            this.Close();
        }
    }
}
