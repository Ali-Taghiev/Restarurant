using RestarurantManagement.View;
using System;
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

        // For accessing formMain
        static formMain _obj;

        public static formMain Instance
        {
            get
            {
                if (_obj == null)
                {
                    _obj = new formMain();
                }
                return _obj;
            }
        }

        // Method to add controls to the CenterPanel
        public void AddControls(Form f)
        {
            // Clear existing controls in the CenterPanel
            CenterPanel.Controls.Clear();

            // Set the docking behavior of the provided form to fill the CenterPanel
            f.Dock = DockStyle.Fill;

            // Indicate that the provided form is not a top-level form
            f.TopLevel = false;

            // Add the provided form to the CenterPanel's controls collection
            CenterPanel.Controls.Add(f);

            // Show the form
            f.Show();
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            // Exit the application when the Exit button is clicked
            Application.Exit();
        }

        

        private void formMain_Load(object sender, EventArgs e)
        {
            // Set the label text to the currently logged-in user
            lblUser.Text = MainClass.USER;
            _obj = this;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            // Load FormHome into the CenterPanel
            AddControls(new FormHome());
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            // Load formCategoryView into the CenterPanel
            AddControls(new formCategoryView());
        }

        private void btnTables_Click(object sender, EventArgs e)
        {
            // Load formTableView into the CenterPanel
            AddControls(new formTableView());
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            // Load formStaffView into the CenterPanel
            AddControls(new formStaffView());
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            // Load formProductView into the CenterPanel
            AddControls(new formProductView());
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            // Show formPOS as a standalone form
            formPOS form = new formPOS();
            form.Show();
        }

        private void btnKitchen_Click(object sender, EventArgs e)
        {
            // Load formKitchenView into the CenterPanel
            AddControls(new formKitchenView());
        }
    }
}
