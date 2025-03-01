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
    public partial class UserHomePage : Form
    {
        public string ClientUsername { get; set; }
        public UserHomePage()
        {
            InitializeComponent();
        }

        private void UserHomePage_Load(object sender, EventArgs e)
        {
            // ComboBox properties
            guna2ComboBox1.FillColor = Color.FromArgb(0, 255, 255, 255);
            guna2ComboBox1.BorderColor = Color.FromArgb(0, 255, 255, 255);

            // Add items including placeholder
            guna2ComboBox1.Items.Add("Services"); // Add placeholder text as an item
            guna2ComboBox1.Items.AddRange(new string[] { "Paints", "Cleaners", "Tools" });

            // Set the draw mode
            guna2ComboBox1.DrawMode = DrawMode.OwnerDrawFixed;

            // Attach the DrawItem event handler
            guna2ComboBox1.DrawItem += new DrawItemEventHandler(guna2ComboBox1_DrawItem);

            // Ensure no item is selected initially
            guna2ComboBox1.SelectedIndex = 0; // Set to the placeholder item initially



            if (!string.IsNullOrEmpty(ClientUsername))
            {
                label1.Text = ClientUsername;
            }
            else
            {
                label1.Text = Properties.Settings.Default.LoggedInUsername;
            }

        }
        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ignore the placeholder
            if (guna2ComboBox1.SelectedIndex == 0) return;

            // Check if the selected item is "Paints"
            if (guna2ComboBox1.SelectedItem != null && guna2ComboBox1.SelectedItem.ToString() == "Paints")
            {
                // Open the Paints form
                var newForm = new Paints();
                newForm.Show();
                this.Hide();
            }
            // Check if the selected item is "Cleaners"
            if (guna2ComboBox1.SelectedItem != null && guna2ComboBox1.SelectedItem.ToString() == "Cleaners")
            {
                // Open the Cleaners form
                var newForm = new Cleaners();
                newForm.Show();
                this.Hide();
            }
            // Check if the selected item is "Tools"
            if (guna2ComboBox1.SelectedItem != null && guna2ComboBox1.SelectedItem.ToString() == "Tools")
            {
                // Open the Tools form
                var newForm = new Tools();
                newForm.Show();
                this.Hide();
            }
        }
        private void guna2ComboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index < 0)
                return; // No item to draw

            // Set custom font and color for the placeholder
            if (e.Index == 0) // Placeholder is the first item
            {
                Font placeholderFont = new Font("Segoe UI Black", 13.8f, FontStyle.Bold);
                Brush placeholderBrush = Brushes.Gray; // Placeholder color
                e.Graphics.DrawString(guna2ComboBox1.Items[e.Index].ToString(), placeholderFont, placeholderBrush, e.Bounds);
            }
            else
            {
                // Set custom font and color for regular items
                Font itemFont = new Font("Segoe UI", 10, FontStyle.Regular);
                Brush itemBrush = Brushes.Black; // Default color for items

                // Check if the item is selected
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    itemBrush = Brushes.Blue; // Color for the selected item
                }

                // Draw the item text
                e.Graphics.DrawString(guna2ComboBox1.Items[e.Index].ToString(), itemFont, itemBrush, e.Bounds);
            }

            // Draw the focus rectangle if needed
            e.DrawFocusRectangle();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            var newForm = new Paints();
            newForm.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var newForm = new AboutUs();
            newForm.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var newForm = new ContactUs();
            newForm.Show();
            this.Hide();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            var newForm = new Paints();
            newForm.Show();
            this.Hide();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            var newForm = new AboutUs();
            newForm.Show();
            this.Hide();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            var newForm = new ContactUs();
            newForm.Show();
            this.Hide();
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            var newForm = new Cleaners();
            newForm.Show();
            this.Hide();
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            var newForm = new Tools();
            newForm.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var newForm = new Cart();
            newForm.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var newForm = new TrackMyOrder();
            newForm.Show();
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // Confirm logout with a dialog
            var result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // Close the current form
                this.Close();

            }
        }
    }
}
