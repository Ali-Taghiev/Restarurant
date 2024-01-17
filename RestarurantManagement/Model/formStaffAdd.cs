using System;
using System.Collections;
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
    public partial class formStaffAdd : SampleAdd
    {
        public formStaffAdd()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        public int id = 0;
       
       
        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtPhone.Text) && cmboxRole.SelectedItem != null)
            {
                string query = "";

                if (id == 0)
                {
                    query = "insert into staff Values(@Name,@Phone,@Role)";

                }
                else
                {
                    query = "update staff Set sName =@Name,sPhone=@Phone,sRole=@Role where staffId=@id";

                }

                Hashtable ht = new Hashtable();
                ht.Add("@id", id);
                ht.Add("@Name", txtName.Text);
                ht.Add("@Phone", txtPhone.Text);
                ht.Add("@Role", cmboxRole.Text);

                if (MainClass.SQL(query, ht) > 0)
                {
                    guna2MessageDialog1.Show("Added Succesfully...");
                    id = 0;
                    txtName.Text = "";
                    txtPhone.Text = "";
                    cmboxRole.SelectedIndex = -1;
                    txtName.Focus();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
