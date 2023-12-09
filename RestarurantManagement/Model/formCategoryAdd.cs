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
    public partial class formCategoryAdd : SampleAdd
    {
        public formCategoryAdd()
        {
            InitializeComponent();
        }

        private void formCategoryAdd_Load(object sender, EventArgs e)
        {

        }

        public override void btnClose_Click_1(object sender, EventArgs e)
        {

        }

        public int id = 0;
        public override void btnSave_Click_1(object sender, EventArgs e)
        {
            string query = "";

            if(id == 0)
            {
                query = "insert into category Values(@Name)";

            }
            else
            {
                query = "update category Set catName =@Name where catID=@id";

            }

            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);

            if (MainClass.SQL(query, ht) > 0)
            {
                MessageBox.Show("Added Succesfully...");
                id = 0;
                txtName.Focus();
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
