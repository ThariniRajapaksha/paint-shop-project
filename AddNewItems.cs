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
    public partial class AddNewItems : Form
    {
        string connectionString = @"Data Source=LAPTOP-E4VLV2RS\SQLEXPRESS;Initial Catalog=BrushHour;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;";
        public AddNewItems()
        {
            InitializeComponent();

            guna2ComboBox1.Items.Add("Paints");
            guna2ComboBox1.Items.Add("Cleaners");
            guna2ComboBox1.Items.Add("Tools");

            // Set event handler for category selection
            guna2ComboBox1.SelectedIndexChanged += comboBoxCategory_SelectedIndexChanged;
        }
        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected category
            string selectedCategory = guna2ComboBox1.SelectedItem.ToString();

            // Show or hide the Liters field based on category
            if (selectedCategory == "Tools")
            {
                // Hide or disable Liters field for Tools
                guna2TextBox3.Visible = false;
                guna2TextBox3.Visible = false; // Assuming you have a label for Liters
            }
            else
            {
                // Show Liters field for Paints and Cleaners
                guna2TextBox3.Visible = true;
                guna2TextBox3.Visible = true;
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Clear();
            guna2TextBox2.Clear();
            guna2TextBox3.Clear();
            guna2TextBox4.Clear();
            guna2ComboBox1.SelectedIndex = -1;
            guna2ComboBox2.SelectedIndex = -1;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string selectedCategory = guna2ComboBox1.SelectedItem.ToString(); // Get selected category
            string image = guna2TextBox1.Text;
            string itemName = guna2TextBox2.Text; // Item name from a text box
            int price = int.Parse(guna2TextBox4.Text); // Price from a text box
            string stockStatus = guna2ComboBox2.Text; // Stock status from a text box

            // Determine which table to insert into
            string query = "";
            SqlCommand cmd = null;

            if (selectedCategory == "Tools")
            {
                // Insert into Tools (No Liters column)
                query = "INSERT INTO Tools (Image, Name, Price, Stock) VALUES (@Image, @Name, @Price, @Stock)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Image", image);
                cmd.Parameters.AddWithValue("@Name", itemName);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Stock", stockStatus);
            }
            else
            {
                // Insert into Paints or Cleaners (with Liters column)
                int liters = int.Parse(guna2TextBox3.Text); // Liters from a text box

                query = "INSERT INTO " + selectedCategory + " (Image, Name, Liters, Price, Stock) VALUES (@Image, @Name, @Liters, @Price, @Stock)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Image", image);
                cmd.Parameters.AddWithValue("@Name", itemName);
                cmd.Parameters.AddWithValue("@Liters", liters);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Stock", stockStatus);
            }

            // Execute the query
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                cmd.Connection = conn;
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item added successfully!");
                    var newForm = new AdminPanel();
                    newForm.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var newForm = new ItemsOptions();
            newForm.Show();
            this.Hide();
        }
    }
}
