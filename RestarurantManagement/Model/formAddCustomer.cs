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
        public formAddCustomer()
        {
            InitializeComponent();
        }

        public string OrderType="";
        public int MainId = 0;
        public int driverId = 0;

        private void formAddCustomer_Load(object sender, EventArgs e)
        {
            if(OrderType=="Take Away")
            {

                lblDriver.Visible = false;
                cmboxDriver.Visible = false;
            }

            string query = "select staffID 'id', sName 'name' from staff where sRole='Driver'";
            MainClass.ComboBoxFill(query, cmboxDriver);

            if (MainId > 0)
            {
                cmboxDriver.SelectedValue = driverId;
            }
        }

        private void cmboxDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            driverId = Convert.ToInt32(cmboxDriver.SelectedValue);
        }

       

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
