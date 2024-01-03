using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RestarurantManagement.Model
{
    public partial class formProductAdd : SampleAdd
    {
        public formProductAdd()
        {
            InitializeComponent();
        }
        public int id = 0;
        public int cID = 0;

        //For Image
        Byte[] imageByteArr;
        private void formProductAdd_Load(object sender, EventArgs e)
        {
            string query = "select catID 'id' ,catName 'name' from category ";
            MainClass.ComboBoxFill(query, cmboxCategory);

            if (cID > 0)
            {
                cmboxCategory.SelectedValue = cID;
            }
        }
        public override void btnSave_Click_1(object sender, EventArgs e)
        {
            

        }



        private void btnClose_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }
        string filePath;
        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter="Images(.png, .jpg)|* .png; *.jpg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                filePath = fd.FileName;
                txtImage.Image= new Bitmap(filePath);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string query = "";

            if (id == 0)
            {
                query = "insert into products Values(@Name,@Price,@Cat,@Image)";

            }
            else
            {
                query = "update products Set pName =@Name,pPrice=@Price,CategoryID=@Cat,pImage=@Image where productId=@id";

            }

            //For Image
            Image temp = new Bitmap(txtImage.Image);
            MemoryStream ms = new MemoryStream();
            temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            imageByteArr = ms.ToArray();
            //
            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);
            ht.Add("@Price", txtPrice.Text);
            ht.Add("@Cat", Convert.ToInt32(cmboxCategory.SelectedValue));
            ht.Add("@Image", imageByteArr);
            if (MainClass.SQL(query, ht) > 0)
            {
                guna2MessageDialog1.Show("Added Successfully...");
                id = 0;
                txtName.Text = "";
                txtPrice.Text = "";

                // Clear the selection in the ComboBox
                cmboxCategory.SelectedIndex = -1;

                txtImage.Image = RestarurantManagement.Properties.Resources.features1;
                txtName.Focus();
            }

        }
    }
}
