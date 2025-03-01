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
    public partial class AdminLogin : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-E4VLV2RS\SQLEXPRESS;Initial Catalog=BrushHour;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;");
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var newForm = new UserLogin();
            newForm.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var newForm = new AdminRegister();
            newForm.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2TextBox1.Text != "" && guna2TextBox2.Text != "")
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO AdminLogin VALUES(@AdminID, @Passwords)", conn);
                    command.Parameters.AddWithValue("@AdminID", guna2TextBox1.Text);
                    command.Parameters.AddWithValue("@Passwords", guna2TextBox2.Text);
                    command.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Login Is Sucessfull");
                    var newForm = new AdminPanel();
                    newForm.Show();
                    this.Hide();
                    guna2TextBox1.Text = "";
                    guna2TextBox2.Text = "";
                }
                else
                {
                    MessageBox.Show("Fill In the Blanks...!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
