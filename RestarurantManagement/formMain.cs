using RestarurantManagement.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestarurantManagement.Model;

namespace RestarurantManagement
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
        }

        //For Accesing formMain

        static formMain _obj;

        public static formMain Instance
        {
            get { 
            if(_obj == null)
                {
                    _obj = new formMain();
                }
            return _obj;
            }
            
    
        }

        public  void AddControls(Form f)
        {
            CenterPanel.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            CenterPanel.Controls.Add(f);
            f.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void formMain_Load(object sender, EventArgs e)
        {
            lblUser.Text = MainClass.USER;
            _obj = this;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            AddControls(new FormHome());
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            AddControls(new formCategoryView());
        }

        private void btnTables_Click(object sender, EventArgs e)
        {
            AddControls(new formTableView());
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            AddControls(new formStaffView());
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            AddControls(new formProductView());
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            formPOS form = new formPOS();
            form.Show();
        }

        private void btnKitchen_Click(object sender, EventArgs e)
        {
            AddControls(new formKitchenView());
        }
    }
}
