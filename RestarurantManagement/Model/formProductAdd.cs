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

        // For Image
        Byte[] imageByteArr;

        private void formProductAdd_Load(object sender, EventArgs e)
        {
            // Load categories into the ComboBox
            string query = "select catID 'id' ,catName 'name' from category ";
            MainClass.ComboBoxFill(query, cmboxCategory);

            // If a category ID is provided, set it in the ComboBox
            if (cID > 0)
            {
                cmboxCategory.SelectedValue = cID;
            }
        }

        string filePath;

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            // Open a file dialog to select an image file
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Images(.png, .jpg)|*.png; *.jpg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                filePath = fd.FileName;
                txtImage.Image = new Bitmap(filePath);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            // Check if the necessary fields are not empty
            if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtPrice.Text) && cmboxCategory.SelectedItem != null)
            {
                string query = "";

                // Construct the SQL query based on whether it's an insert or update
                if (id == 0)
                {
                    query = "insert into products Values(@Name,@Price,@Cat,@Image)";
                }
                else
                {
                    query = "update products Set pName =@Name,pPrice=@Price,CategoryID=@Cat,pImage=@Image where productId=@id";
                }

                // Convert the image to a byte array
                Image temp = new Bitmap(txtImage.Image);
                MemoryStream ms = new MemoryStream();
                temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                imageByteArr = ms.ToArray();

                // Set up the parameters for the SQL query
                Hashtable ht = new Hashtable();
                ht.Add("@id", id);
                ht.Add("@Name", txtName.Text);
                ht.Add("@Price", txtPrice.Text);
                ht.Add("@Cat", Convert.ToInt32(cmboxCategory.SelectedValue));
                ht.Add("@Image", imageByteArr);

                // Execute the SQL query
                if (MainClass.SQL(query, ht) > 0)
                {
                    guna2MessageDialog1.Show("Added Successfully...");
                    id = 0;
                    txtName.Text = "";
                    txtPrice.Text = "";

                    // Clear the selection in the ComboBox
                    cmboxCategory.SelectedIndex = -1;

                    // Set the default image
                    txtImage.Image = RestarurantManagement.Properties.Resources.features1;
                    txtName.Focus();
                }
            }
            else
            {
                txtName.Focus();
            }
        }
    }
}
