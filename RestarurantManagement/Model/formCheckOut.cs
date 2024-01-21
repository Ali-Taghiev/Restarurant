using System;
using System.Collections;
using System.Windows.Forms;

namespace RestarurantManagement.Model
{
    public partial class formCheckOut : SampleAdd
    {
        public formCheckOut()
        {
            InitializeComponent();
        }

        public double amt; // Variable to store the total amount
        public int MainID = 0; // Variable to store the MainID of the transaction

        private void txtPaymentRecieved_TextChanged(object sender, EventArgs e)
        {
            double amt = 0;
            double received = 0;
            double change = 0;

            // Parse the values from textboxes, calculate change, and display it
            double.TryParse(txtBillAmount.Text, out amt);
            double.TryParse(txtPaymentRecieved.Text, out received);
            change = received - amt;

            txtChange.Text = change.ToString();
        }

        private void formCheckOut_Load(object sender, EventArgs e)
        {
            // Set the bill amount textbox to the stored amount
            txtBillAmount.Text = amt.ToString();
        }

        private void btnSave2_Click(object sender, EventArgs e)
        {
            // SQL query to update transaction details in the database
            string query = @"Update tblMain Set total = @total, received = @rec, change = @change, status = 'Paid' where MainID = @id";

            // Hashtable to store parameter values for the SQL query
            Hashtable ht = new Hashtable();
            ht.Add("@total", txtBillAmount.Text);
            ht.Add("@rec", txtPaymentRecieved.Text);
            ht.Add("@change", txtChange.Text);
            ht.Add("@id", MainID);

            // Execute the SQL query and check if the operation was successful
            if (MainClass.SQL(query, ht) > 0)
            {
                // Show success message and close the form
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Show("Saved Successfully");
                this.Close();
            }
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Close the form when the Exit button is clicked
            this.Close();
        }
    }
}
