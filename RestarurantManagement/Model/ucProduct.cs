using System;
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
    public partial class ucProduct : UserControl
    {
        public ucProduct()
        {
            InitializeComponent();

            // Attach the Click event handler for the entire control
            this.Click += ucProduct_Click;

            // Attach the Click event handler for each child control
            lblName.Click += ucProduct_Click;
            txtImage.Click += ucProduct_Click;
            // Add other controls as needed
        }

        public event EventHandler onSelect = null;
        public int id { get; set; }
        public string pPrice { get; set; }
        public string pCategory { get; set; }
        public string pName
        {
            get { return lblName.Text; }
            set { lblName.Text = value; }
        }
        public Image pImage
        {
            get { return txtImage.Image; }
            set { txtImage.Image = value; }
        }

        private void ucProduct_Click(object sender, EventArgs e)
        {
            // Raise the onSelect event when any part of the ucProduct is clicked
            onSelect?.Invoke(this, EventArgs.Empty);
        }
    }
}

