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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Tharini_Rajapaksha_KADSE202F_006
{
    public partial class UserLogin : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-E4VLV2RS\SQLEXPRESS;Initial Catalog=BrushHour;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;");
        public UserLogin()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(guna2TextBox1.Text) && !string.IsNullOrWhiteSpace(guna2TextBox2.Text))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM UserRegistration WHERE Username = @Username AND Password = @Password", conn);
                    command.Parameters.AddWithValue("@Username", guna2TextBox1.Text);
                    command.Parameters.AddWithValue("@Password", guna2TextBox2.Text);

                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        Properties.Settings.Default.LoggedInUsername = guna2TextBox1.Text;
                        Properties.Settings.Default.Save();

                        MessageBox.Show("Login is successful!");

                        var newForm = new UserHomePage();
                        newForm.ClientUsername = guna2TextBox1.Text;
                        newForm.Show();
                        this.Hide();

                        guna2TextBox1.Text = "";
                        guna2TextBox2.Text = "";
                    }
                    else 
                    {
                        MessageBox.Show("Invalid username or password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    conn.Close();
                }
                else
                {
                    MessageBox.Show("Fill in the blanks!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var newForm = new UserRegister();
            newForm.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var newForm = new AdminLogin();
            newForm.Show();
            this.Hide();
        }
    }
}
