using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Internal;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Tharini_Rajapaksha_KADSE202F_006
{
    public partial class CustomerOrders : Form
    {
        string connectionString = @"Data Source=LAPTOP-E4VLV2RS\SQLEXPRESS;Initial Catalog=BrushHour;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;";
        Guna.UI2.WinForms.Guna2DataGridView guna2DataGridView1;
        public CustomerOrders()
        {
            InitializeComponent();

            // Initialize and style Guna2DataGridView
            guna2DataGridView1 = new Guna.UI2.WinForms.Guna2DataGridView();
            guna2DataGridView1.Location = new System.Drawing.Point(50, 200);  // Moved down by increasing Y value
            guna2DataGridView1.Size = new System.Drawing.Size(800, 400);
            guna2DataGridView1.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;
            guna2DataGridView1.ColumnHeadersHeight = 40;
            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;  // Automatically adjust columns to fit content

            // Add Guna2DataGridView to the form
            this.Controls.Add(guna2DataGridView1);

            // Increase font size for header columns
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);  // Font size for header

            // Increase font size for other rows
            guna2DataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 12F);  // Font size for data rows

            // Add Guna2DataGridView to the form
            this.Controls.Add(guna2DataGridView1);

            // Load data into DataGridView
            LoadData();
        }
        private void LoadData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT SummaryID, ClientUsername, OrderDetails, GrandTotal, OrderDate FROM OrderSummary", con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    guna2DataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var newForm = new AdminPanel();
            newForm.Show();
            this.Hide();
        }
    }
}
