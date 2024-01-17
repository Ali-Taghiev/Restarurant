using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Management;
using System.Windows.Forms;

namespace RestarurantManagement
{
    public partial class formLogin : Form
    {
        public formLogin()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
             if(MainClass.isValidUser(txtboxUsername.Text, txtboxPassword.Text) == false)
            {
                guna2MessageDialog1.Show("Invalid Username or Password");
                return;
            }
            else
            {
                this.Hide();
                formMain frm =new formMain();
                frm.Show();
            } 
        }

       
    }
}
