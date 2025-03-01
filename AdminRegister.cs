using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Tharini_Rajapaksha_KADSE202F_006
{
    public partial class AdminRegister : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-E4VLV2RS\SQLEXPRESS;Initial Catalog=BrushHour;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;");
        public AdminRegister()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var newForm = new AdminLogin();
            newForm.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2TextBox1.Text != "" && guna2TextBox2.Text != "" && guna2TextBox3.Text != "" && guna2TextBox4.Text != "")
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO AdminRegistration VALUES(@Name, @Email, @AdminID, @Password)", conn);
                    command.Parameters.AddWithValue("@Name", guna2TextBox1.Text);
                    command.Parameters.AddWithValue("@Email", guna2TextBox2.Text);
                    command.Parameters.AddWithValue("@AdminID", guna2TextBox3.Text);
                    command.Parameters.AddWithValue("@Password", guna2TextBox4.Text);
                    command.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Registration Is Sucessfull");
                    var newForm = new AdminLogin();
                    newForm.Show();
                    this.Hide();
                    guna2TextBox1.Text = "";
                    guna2TextBox2.Text = "";
                    guna2TextBox3.Text = "";
                    guna2TextBox4.Text = "";
                }
                else
                {
                    MessageBox.Show("Fill In the Blanks...!");
                }
            }
            catch (SqlException sqlEx)
            {
                switch (sqlEx.Number)
                {
                    case 2627:  
                        MessageBox.Show("This username already exists. Please choose another.");
                        break;
                    case 547:  
                        MessageBox.Show("There is a problem with the foreign key relationships. Please ensure the data is valid.");
                        break;
                    case 2601:  
                        MessageBox.Show("Duplicate key error: This data already exists.");
                        break;
                    default:
                       
                        MessageBox.Show("A database error occurred: " + sqlEx.Message);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}
