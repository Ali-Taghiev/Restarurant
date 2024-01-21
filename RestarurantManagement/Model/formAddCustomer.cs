using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestarurantManagement.Model
{
    public partial class formAddCustomer : SampleAdd
    {
        // Public variables for storing order type, main ID, and driver ID
        public string OrderType = "";
        public int MainId = 0;
        public int driverId = 0;

        public formAddCustomer()
        {
            InitializeComponent();
        }

        private void formAddCustomer_Load(object sender, EventArgs e)
        {
            // Check if the order type is "Take Away" and adjust UI accordingly
            if (OrderType == "Take Away")
            {
                lblDriver.Visible = false;
                cmboxDriver.Visible = false;
            }

            // Load drivers into the ComboBox from the database
            string query = "select staffID 'id', sName 'name' from staff where sRole='Driver'";
            MainClass.ComboBoxFill(query, cmboxDriver);

            // If there is a selected driver (MainId > 0), set the ComboBox value to the driverId
            if (MainId > 0)
            {
                cmboxDriver.SelectedValue = driverId;
            }
        }

        private void cmboxDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update driverId when the selected index of the ComboBox changes
            driverId = Convert.ToInt32(cmboxDriver.SelectedValue);
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            // Add customer functionality - to be implemented
            // You can add code here to handle the addition of a customer
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Close the form when the Exit button is clicked
            this.Close();
        }
    }
}
