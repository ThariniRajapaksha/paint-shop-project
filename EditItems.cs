using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Tharini_Rajapaksha_KADSE202F_006
{
    public partial class EditItems : Form
    {
        string connectionString = @"Data Source=LAPTOP-E4VLV2RS\SQLEXPRESS;Initial Catalog=BrushHour;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;";
        public EditItems()
        {
            InitializeComponent();
            LoadComboBox();
        }
        // Method to populate ComboBox with table names
        private void LoadComboBox()
        {
            guna2ComboBox1.Items.Add("Cleaners");
            guna2ComboBox1.Items.Add("Paints");
            guna2ComboBox1.Items.Add("Tools");
        }
        // Method to load data from the selected table into DataGridView
        private void LoadData(string tableName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();  // Open the connection

                    // Dynamically create the query based on the selected table
                    string query = $"SELECT * FROM {tableName}";  // Get data from the selected table
                    Console.WriteLine($"Executing Query: {query}");  // Log the query for debugging

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);  // Fill the data table with the result

                    // Bind the data to the DataGridView
                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        // Show message if no data is found
                        MessageBox.Show($"No data found in the {tableName} table.");
                    }

                    con.Close();  // Close the connection
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                MessageBox.Show($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string itemId = dataGridView1.SelectedRows[0].Cells["ItemID"].Value.ToString();
                string newName = dataGridView1.SelectedRows[0].Cells["Name"].Value.ToString();
                string newStock = dataGridView1.SelectedRows[0].Cells["Stock"].Value.ToString();
                string newLiters = dataGridView1.SelectedRows[0].Cells["Liters"].Value.ToString();
                string newPrice = dataGridView1.SelectedRows[0].Cells["Price"].Value.ToString();
                string selectedTable = guna2ComboBox1.SelectedItem.ToString();  // Get the selected table

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand($"UPDATE {selectedTable} SET Name = @Name, Stock = @Stock, Liters = @Liters, Price = @Price WHERE ItemID = @ItemID", con);
                    cmd.Parameters.AddWithValue("@ItemID", itemId);
                    cmd.Parameters.AddWithValue("@Name", newName);
                    cmd.Parameters.AddWithValue("@Stock", newStock);
                    cmd.Parameters.AddWithValue("@Liters", newLiters);
                    cmd.Parameters.AddWithValue("@Price", newPrice);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record updated successfully!");
                    LoadData(selectedTable);  // Refresh data after update
                    var newForm = new AdminPanel();
                    newForm.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Please select a row to update.");
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string itemId = dataGridView1.SelectedRows[0].Cells["ItemID"].Value.ToString();
                string selectedTable = guna2ComboBox1.SelectedItem.ToString();  // Get the selected table

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand($"DELETE FROM {selectedTable} WHERE ItemID = @ItemID", con);
                    cmd.Parameters.AddWithValue("@ItemID", itemId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record deleted successfully!");
                    LoadData(selectedTable);  // Refresh data after deletion
                    var newForm = new AdminPanel();
                    newForm.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var newForm = new AdminPanel();
            newForm.Show();
            this.Hide();
        }

        private void guna2ComboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string selectedTable = guna2ComboBox1.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedTable))
            {
                try
                {
                    LoadData(selectedTable);  // Pass the selected table to LoadData
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading data: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a valid table.");
            }
        }
    }
}
