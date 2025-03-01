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
    public partial class UserRegister : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-E4VLV2RS\SQLEXPRESS;Initial Catalog=BrushHour;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;");
        public UserRegister()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var newForm = new UserLogin();
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
                    SqlCommand command = new SqlCommand("INSERT INTO UserRegistration VALUES(@Name, @Email, @Username, @Password)", conn);
                    command.Parameters.AddWithValue("@Name", guna2TextBox1.Text);
                    command.Parameters.AddWithValue("@Email", guna2TextBox2.Text);
                    command.Parameters.AddWithValue("@Username", guna2TextBox3.Text);
                    command.Parameters.AddWithValue("@Password", guna2TextBox4.Text);
                    command.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Registration Is Sucessfull");
                    var newForm = new UserLogin();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
