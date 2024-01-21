using System;
using System.Windows.Forms;

namespace RestarurantManagement.Model
{
    public partial class formBillList : SampleAdd
    {
        // Public variable to store the selected MainID
        public int MainID = 0;

        public formBillList()
        {
            InitializeComponent();
        }

        private void formBillList_Load(object sender, EventArgs e)
        {
            // Load data when the form is loaded
            LoadData();
        }

        private void LoadData()
        {
            // SQL query to retrieve data from the database excluding 'Pending' status
            string query = @"select MainID,TableName,WaiterName,orderType,status,total from tblMain where status <> 'Pending'";

            // ListBox to store DataGridView column names for formatting
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvTable);
            lb.Items.Add(dgvWaiter);
            lb.Items.Add(dgvOrderType);
            lb.Items.Add(dgvStatus);
            lb.Items.Add(dgvTotal);

            // Load data into the DataGridView using the MainClass utility method
            MainClass.LoadData(query, guna2DataGridView1, lb);
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Auto-increment the first column value (index 0) for better user readability
            int count = 0;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle cell click event, specifically for the 'dgvedit' column
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                // Store the selected MainID and close the form
                MainID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                this.Close();
            }
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            // Close the form when the control box is clicked
            this.Close();
        }
    }
}
