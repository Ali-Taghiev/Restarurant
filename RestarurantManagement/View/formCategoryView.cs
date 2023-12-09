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
    public partial class formCategoryView : SampleView
    {
        public formCategoryView()
        {
            InitializeComponent();
        }

        public void GetData()
        {
            string query = "select * from category where catName like '%"+ txtboxSearch.Text +"%'";
            ListBox lb  = new ListBox();
            lb.Items.Add(dgvid); 
            lb.Items.Add(dgvName);
            MainClass.LoadData(query, guna2DataGridView1,lb);
        }
        public override void btnAdd_Click(object sender, EventArgs e)
        {
            formCategoryAdd form = new formCategoryAdd();
            form.ShowDialog();
            GetData();

        }

        public override void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void formCategoryView_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                formCategoryAdd form = new formCategoryAdd();
                form.id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                form.txtName.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvName"].Value);
                form.ShowDialog();
                GetData();
            }
            if(guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                int id= Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                string query = "delete from category where catID=" + id + "";
                
                Hashtable ht = new Hashtable(); 
                MainClass.SQL(query,ht);
                MessageBox.Show("Deleted Succesfully..");
                GetData();

            }
        }
    }
}
