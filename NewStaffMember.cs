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
    public partial class NewStaffMember : Form
    {
        string connectionString = @"Data Source=LAPTOP-E4VLV2RS\SQLEXPRESS;Initial Catalog=BrushHour;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;";
        public NewStaffMember()
        {
            InitializeComponent();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Clear();
            guna2TextBox2.Clear();
            guna2TextBox3.Clear();
            guna2TextBox4.Clear();
            guna2TextBox5.Clear();
            guna2TextBox6.Clear();
            guna2ComboBox1.SelectedIndex = -1;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string name = guna2TextBox1.Text;
            string contact = guna2TextBox2.Text;
            string email = guna2TextBox3.Text;
            string address = guna2TextBox4.Text;
            string zipCode = guna2TextBox5.Text;
            string country = guna2TextBox6.Text;
            string position = guna2ComboBox1.Text;

            // Validate that all required text boxes are filled
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(contact) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(zipCode) ||
                string.IsNullOrWhiteSpace(country) ||
                string.IsNullOrWhiteSpace(position))
            {
                MessageBox.Show("Please fill in all fields before submitting.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method if validation fails
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Staff (FullName, Contact, Email, Address, ZipCode, Country, Position) " +
                               "VALUES (@Name, @Contact, @Email, @Address, @ZipCode, @Country, @Position)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Contact", contact);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@ZipCode", zipCode);
                cmd.Parameters.AddWithValue("@Country", country);
                cmd.Parameters.AddWithValue("@Position", position);

                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("New Staff Member Registered successfully!");

                var newForm = new AdminPanel();
                newForm.Show();
                this.Hide();
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
