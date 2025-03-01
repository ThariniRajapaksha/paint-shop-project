using Guna.UI2.WinForms;
using Microsoft.Data.SqlClient;
using System;
using System.Windows.Forms;

namespace Tharini_Rajapaksha_KADSE202F_006
{
    public partial class CustomerDetails : Form
    {
        string connectionString = @"Data Source=LAPTOP-E4VLV2RS\SQLEXPRESS;Initial Catalog=BrushHour;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;";
        public string ClientUsername { get; set; }
        public CustomerDetails()
        {
            InitializeComponent();
        }



        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string name = guna2TextBox1.Text;
            string address = guna2TextBox2.Text;
            string contact = guna2TextBox3.Text;
            string email = guna2TextBox4.Text;
            string country = guna2TextBox5.Text;
            string city = guna2TextBox6.Text;
            string zipCode = guna2TextBox7.Text;
            string username = label9.Text;

            // Validate that all required text boxes are filled
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(contact) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(country) ||
                string.IsNullOrWhiteSpace(city) ||
                string.IsNullOrWhiteSpace(zipCode))
            {
                MessageBox.Show("Please fill in all fields before submitting.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method if validation fails
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO CustomerDetails (Name, Address, Contact, Email, Country, City, ZipCode, ClientUsername) " +
                               "VALUES (@Name, @Address, @Contact, @Email, @Country, @City, @ZipCode, @Username)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Contact", contact);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Country", country);
                cmd.Parameters.AddWithValue("@City", city);
                cmd.Parameters.AddWithValue("@ZipCode", zipCode);
                cmd.Parameters.AddWithValue("@Username", username);

                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Shipping details saved successfully!");

                var newForm = new PayType(); // Assuming you have a PayType form to open
                newForm.Show();
                this.Hide();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Clear();
            guna2TextBox2.Clear();
            guna2TextBox3.Clear();
            guna2TextBox4.Clear();
            guna2TextBox5.Clear();
            guna2TextBox6.Clear();
            guna2TextBox7.Clear();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void CustomerDetails_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ClientUsername))
            {
                label9.Text = ClientUsername;
            }
            else
            {
                label9.Text = Properties.Settings.Default.LoggedInUsername;
            }
        }
    }
}
