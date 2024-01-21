using RestarurantManagement.Model;
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

namespace RestarurantManagement.View
{
    public partial class formStaffView : SampleView
    {
        public formStaffView()
        {
            InitializeComponent();
        }

        private void formStaffView_Load(object sender, EventArgs e)
        {
            // Load initial staff data
            GetData();
        }

        public void GetData()
        {
            // Fetch staff data from the database based on the search criteria
            string query = "select * from staff where sName like '%" + txtboxSearch.Text + "%'";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvName);
            lb.Items.Add(dgvPhone);
            lb.Items.Add(dgvRole);
            MainClass.LoadData(query, guna2DataGridView1, lb);
        }

        public override void btnAdd_Click(object sender, EventArgs e)
        {
            // Open the form for adding a new staff member
            MainClass.BlurBackground(new formStaffAdd());
            // Refresh the data after adding
            GetData();
        }

        public override void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            // Refresh staff data based on search text
            GetData();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                // Open the form for editing staff details
                formStaffAdd form = new formStaffAdd();
                form.id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                form.txtName.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvName"].Value);
                form.txtPhone.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvPhone"].Value);
                form.cmboxRole.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvRole"].Value);
                MainClass.BlurBackground(form);
                // Refresh data after editing
                GetData();
            }

            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                // Ask for confirmation before deleting staff details
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question;
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
                if (guna2MessageDialog1.Show("Are you sure to delete this Staff Details?") == DialogResult.Yes)
                {
                    // Delete the selected staff member
                    int id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                    string query = "delete from staff where staffId=" + id + "";

                    Hashtable ht = new Hashtable();
                    MainClass.SQL(query, ht);

                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;

                    guna2MessageDialog1.Show("Deleted Successfully..");
                    // Refresh data after deletion
                    GetData();
                }
            }
        }

       
    }
}
