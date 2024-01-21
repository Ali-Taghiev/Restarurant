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
            // Exit the application when the Exit button is clicked
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Check if the entered username and password are valid
            if (MainClass.isValidUser(txtboxUsername.Text, txtboxPassword.Text) == false)
            {
                // Show a message if the username or password is invalid
                guna2MessageDialog1.Show("Invalid Username or Password");
                return;
            }
            else
            {
                // If the username and password are valid, hide the login form and show the main form
                this.Hide();
                formMain frm = new formMain();
                frm.Show();
            }
        }
    }
}
