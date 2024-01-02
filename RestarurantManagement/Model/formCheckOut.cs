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
    public partial class formCheckOut : SampleAdd
    {
        public formCheckOut()
        {
            InitializeComponent();
     
        
        }

        public double amt;
        public int MainID = 0;

        private void txtPaymentRecieved_TextChanged(object sender, EventArgs e)
        {
            double amt = 0;
            double received = 0;
            double change = 0;
            
            double.TryParse(txtBillAmount.Text, out amt);
            double.TryParse(txtPaymentRecieved.Text, out received);
            change=received-amt;

            txtChange.Text=change.ToString();
        }
      
        public override void btnSave_Click_1(object sender, EventArgs e)
        {
            string query = @"Update tblMain Set total =@total, received=@rec,change=@change,status='Paid' where MainID=@id";

            Hashtable ht = new Hashtable();
            ht.Add("@total",txtBillAmount.Text);
            ht.Add("@rec",txtPaymentRecieved.Text);
            ht.Add("@change", txtChange.Text);
            ht.Add("@id", MainID);

            if(MainClass.SQL(query,ht)>0)
            {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Show("Saved Succesfully");
                this.Close();
            }

        }

        private void formCheckOut_Load(object sender, EventArgs e)
        {
            txtBillAmount.Text=amt.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
