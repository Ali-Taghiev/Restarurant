using RestarurantManagement.Model;
using System;
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
    }
}
