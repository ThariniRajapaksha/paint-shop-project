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
using System.Xml;


namespace Tharini_Rajapaksha_KADSE202F_006
{
    public partial class Paints : Form
    {
        private FlowLayoutPanel flowPanel;
        string connectionString = @"Data Source=LAPTOP-E4VLV2RS\SQLEXPRESS;Initial Catalog=BrushHour;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;";


        public Paints()
        {
            InitializeComponent();
            DisplayPaints();
        }
        private void DisplayPaints()
        {
            string query = "SELECT Image, Stock, Name, Liters, Price FROM Paints";

            Panel centerPanel = new Panel();
            centerPanel.Dock = DockStyle.Fill;
            centerPanel.AutoScroll = false;
            this.Controls.Add(centerPanel);

            flowPanel = new FlowLayoutPanel();
            flowPanel.FlowDirection = FlowDirection.LeftToRight; // Change to LeftToRight for row layout
            flowPanel.WrapContents = true; // Allow wrapping to new rows
            flowPanel.AutoScroll = true;
            flowPanel.Dock = DockStyle.Bottom;
            flowPanel.Height = 450;

            centerPanel.Controls.Add(flowPanel);

            Button checkoutButton = new Button();
            checkoutButton.Text = "Add Cart";
            checkoutButton.Size = new Size(100, 30);
            checkoutButton.Location = new Point((this.ClientSize.Width - 100) / 2, flowPanel.Top - 40);
            checkoutButton.Click += guna2Button2_Click;
            this.Controls.Add(checkoutButton);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string imagePath = reader["Image"].ToString().Trim('"');
                    string stock = reader["Stock"].ToString();
                    string name = reader["Name"].ToString();
                    int liters = Convert.ToInt32(reader["Liters"]);
                    int price = Convert.ToInt32(reader["Price"]);

                    RoundedPanel itemPanel = new RoundedPanel();
                    itemPanel.CornerRadius = 20;
                    itemPanel.BorderColor = Color.Black;
                    itemPanel.BorderThickness = 2;
                    itemPanel.Size = new Size(380, 200); // Adjusted size for two columns
                    itemPanel.BackColor = Color.WhiteSmoke;
                    itemPanel.Margin = new Padding(40);

                    PictureBox paintImage = new PictureBox();
                    try
                    {
                        paintImage.Image = Image.FromFile(imagePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Image not found: {imagePath}\nError: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        paintImage.Image = Image.FromFile(@"C:\Path\To\Your\PlaceholderImage.png");
                    }

                    paintImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    paintImage.Size = new Size(100, 100);
                    paintImage.Location = new Point(10, 10);
                    itemPanel.Controls.Add(paintImage);

                    Label stockLabel = new Label();
                    stockLabel.Text = "Stock: " + stock;
                    stockLabel.Location = new Point(120, 10);
                    itemPanel.Controls.Add(stockLabel);

                    Label nameLabel = new Label();
                    nameLabel.Text = "Name: " + name;
                    nameLabel.Location = new Point(120, 30);
                    itemPanel.Controls.Add(nameLabel);

                    Label litersLabel = new Label();
                    litersLabel.Text = "Liters: " + liters;
                    litersLabel.Location = new Point(120, 50);
                    itemPanel.Controls.Add(litersLabel);

                    Label priceLabel = new Label();
                    priceLabel.Text = "Price: " + price;
                    priceLabel.Location = new Point(120, 70);
                    itemPanel.Controls.Add(priceLabel);

                    Label qtyLabel = new Label();
                    qtyLabel.Text = "Quantity:";
                    qtyLabel.Location = new Point(120, 90);
                    itemPanel.Controls.Add(qtyLabel);

                    NumericUpDown qtySelector = new NumericUpDown();
                    qtySelector.Minimum = 1;
                    qtySelector.Maximum = 100;
                    qtySelector.Location = new Point(120, 110);
                    itemPanel.Controls.Add(qtySelector);

                    flowPanel.Controls.Add(itemPanel);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (flowPanel == null || flowPanel.Controls.Count == 0)
            {
                MessageBox.Show("No items found to place an order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string clientUsername = GetLoggedInUsername();

            string insertQuery = "INSERT INTO OrderPaints (Image, Name, QTY, Liters, Price, ClientUsername) VALUES (@Image, @Name, @QTY, @Liters, @Price, @ClientUsername)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                bool orderPlaced = false;

                foreach (Control control in flowPanel.Controls)
                {
                    if (control is Panel itemPanel)
                    {
                        PictureBox paintImage = itemPanel.Controls.OfType<PictureBox>().FirstOrDefault();
                        Label nameLabel = itemPanel.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Text.StartsWith("Name:"));
                        Label litersLabel = itemPanel.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Text.StartsWith("Liters:"));
                        Label priceLabel = itemPanel.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Text.StartsWith("Price:"));
                        NumericUpDown qtySelector = itemPanel.Controls.OfType<NumericUpDown>().FirstOrDefault();

                        if (paintImage != null && nameLabel != null && litersLabel != null && priceLabel != null && qtySelector != null)
                        {
                            string image = paintImage.Text.Replace("Image: ", "");
                            string name = nameLabel.Text.Replace("Name: ", "");
                            int liters = int.Parse(litersLabel.Text.Replace("Liters: ", ""));
                            int price = int.Parse(priceLabel.Text.Replace("Price: ", ""));
                            int qty = (int)qtySelector.Value;

                            if (qty > 1)
                            {
                                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                                {
                                    command.Parameters.AddWithValue("@Image", image);
                                    command.Parameters.AddWithValue("@Name", name);
                                    command.Parameters.AddWithValue("@QTY", qty);
                                    command.Parameters.AddWithValue("@Liters", liters);
                                    command.Parameters.AddWithValue("@Price", price);
                                    command.Parameters.AddWithValue("@ClientUsername", clientUsername); 

                                    try
                                    {
                                        int rowsAffected = command.ExecuteNonQuery();
                                        if (rowsAffected > 0)
                                        {
                                            orderPlaced = true;
                                        }
                                    }
                                    catch (SqlException ex)
                                    {
                                        MessageBox.Show($"Error inserting data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                    }
                }

                if (orderPlaced)
                {
                    MessageBox.Show("Order placed successfully!");
                    var newForm = new UserHomePage();
                    newForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("No orders were placed.");
                }
            }
        }
        private string GetLoggedInUsername()
        {
            return Properties.Settings.Default.LoggedInUsername;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var newForm = new UserHomePage();
            newForm.Show();
            this.Hide();
        }
    }
}
