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
    public partial class ContactUs : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-E4VLV2RS\SQLEXPRESS;Initial Catalog=BrushHour;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;");
        public ContactUs()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var newForm = new UserHomePage();
            newForm.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text != "" && guna2TextBox2.Text != "" && guna2TextBox3.Text != "" && richTextBox1.Text != "")
            {
                conn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO ContactUs VALUES(@Name, @Email, @Address, @Message)", conn);
                command.Parameters.AddWithValue("@Name", guna2TextBox1.Text);
                command.Parameters.AddWithValue("@Email", guna2TextBox2.Text);
                command.Parameters.AddWithValue("@Address", guna2TextBox3.Text);
                command.Parameters.AddWithValue("@Message", richTextBox1.Text);
                command.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Message Sent");
                var newForm = new UserHomePage();
                newForm.Show();
                this.Hide();
                guna2TextBox1.Text = "";
                guna2TextBox2.Text = "";
                guna2TextBox3.Text = "";
                richTextBox1.Text = "";
            }
            else
            {
                MessageBox.Show("Fill In the Blanks...!");
            }
        }
    }
}
