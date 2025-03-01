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

namespace Tharini_Rajapaksha_KADSE202F_006
{
    public partial class Cart : Form
    {
        private FlowLayoutPanel flowPanel;
        string connectionString = @"Data Source=LAPTOP-E4VLV2RS\SQLEXPRESS;Initial Catalog=BrushHour;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;";
        public string ClientUsername { get; set; }

        public Cart()
        {
            InitializeComponent();

            // Set up the FlowLayoutPanel to flow vertically
            this.flowPanel = new FlowLayoutPanel();
            this.flowPanel.Name = "flowPanel";
            this.flowPanel.Size = new Size(800, 400);  // Adjust size if needed
            this.flowPanel.AutoScroll = true;  // Enable scrolling
            this.flowPanel.FlowDirection = FlowDirection.TopDown; // Set to vertical flow
            this.flowPanel.WrapContents = false; // Prevents wrapping to the next column
            this.Controls.Add(this.flowPanel);  // Add it to the form's controls collection

            // Center the flowPanel vertically in the form
            CenterFlowPanel();

            // Load the cart data after initializing the flowPanel
            LoadCartData();

            // Create and add the Processed to Pay button
            RoundedButton btnProcessedToPay = new RoundedButton
            {
                Text = "Processed to Pay",
                Location = new Point(this.ClientSize.Width - 300, this.ClientSize.Height - 100), // Position on the right side
                Size = new Size(200, 50),
                BackColor = ColorTranslator.FromHtml("#003060"),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 12, FontStyle.Bold), // Set font to bold
                BorderRadius = 40 // You can set your desired radius here
            };

            btnProcessedToPay.Click += BtnProcessedToPay_Click; // Attach click event handler
            this.Controls.Add(btnProcessedToPay); // Add the button to the form
        }

        private void CenterFlowPanel()
        {
            // Calculate the new Y position to center the flowPanel vertically with an offset
            int offset = 50; // Adjust this value to move the panel lower
            int newY = (this.ClientSize.Height - flowPanel.Height) / 2 + offset; // Centering and offsetting
            flowPanel.Location = new Point(50, newY); // Keep the X position fixed (50)
        }

        private void LoadCartData()
        {
            string usernameToUse = !string.IsNullOrEmpty(ClientUsername) ? ClientUsername : Properties.Settings.Default.LoggedInUsername;

            if (string.IsNullOrEmpty(usernameToUse))
            {
                MessageBox.Show("Username is not set. Cannot load the cart data.");
                return;
            }
            ClientUsername = usernameToUse;
            label2.Text = usernameToUse;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Combine queries for OrderPaints, OrderCleaners, and OrderTools
                string query = @"
                    SELECT 'Paints' AS Category, OrderID, Name, QTY, Price FROM OrderPaints WHERE ClientUsername = @Username
                    UNION ALL
                    SELECT 'Cleaners' AS Category, OrderID, Name, QTY, Price FROM OrderCleaners WHERE ClientUsername = @Username
                    UNION ALL
                    SELECT 'Tools' AS Category, OrderID, Name, QTY, Price FROM OrderTools WHERE ClientUsername = @Username";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Username", usernameToUse);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                flowPanel.Controls.Clear(); // Clear previous cart items

                foreach (DataRow row in dt.Rows)
                {
                    // Create a rounded panel for each item
                    RoundedPanel itemPanel = new RoundedPanel();
                    itemPanel.Size = new Size(760, 60); // Adjust size if needed
                    itemPanel.BackColor = Color.WhiteSmoke; // Set panel background color
                    itemPanel.BorderStyle = BorderStyle.FixedSingle; // Optional border
                    itemPanel.Margin = new Padding(5); // Space between items

                    // Add name label
                    Label lblName = new Label();
                    lblName.Text = "Name: " + row["Name"].ToString();
                    lblName.Location = new Point(10, 10);
                    lblName.AutoSize = true;

                    // Add quantity label
                    Label lblQty = new Label();
                    lblQty.Text = "Quantity: " + row["QTY"].ToString();
                    lblQty.Location = new Point(200, 10);
                    lblQty.AutoSize = true;

                    // Add price label
                    Label lblPrice = new Label();
                    lblPrice.Text = "Price: Rs." + row["Price"].ToString();
                    lblPrice.Location = new Point(350, 10);
                    lblPrice.AutoSize = true;

                    // Add category label (optional)
                    Label lblCategory = new Label();
                    lblCategory.Text = "Category: " + row["Category"].ToString();
                    lblCategory.Location = new Point(500, 10);
                    lblCategory.AutoSize = true;

                    // Remove button
                    RoundedButton btnRemove = new RoundedButton();
                    btnRemove.Text = "Remove";
                    btnRemove.Size = new Size(100, 35); // Larger button size
                    btnRemove.BackColor = ColorTranslator.FromHtml("#07BB9C");
                    btnRemove.ForeColor = Color.White; // Button text color
                    btnRemove.FlatStyle = FlatStyle.Flat; // Remove border
                    btnRemove.Font = new Font("Arial", 10, FontStyle.Bold); // Set font to bold
                    btnRemove.FlatAppearance.BorderSize = 0; // Remove border size
                    btnRemove.Location = new Point(650, 5);
                    btnRemove.BorderRadius = 25;

                    btnRemove.Click += (sender, e) =>
                    {
                        int orderId = Convert.ToInt32(row["OrderID"]);
                        string category = row["Category"].ToString();
                        LoadCartData();
                    };

                    // Add all controls to the item panel
                    itemPanel.Controls.Add(lblName);
                    itemPanel.Controls.Add(lblQty);
                    itemPanel.Controls.Add(lblPrice);
                    itemPanel.Controls.Add(lblCategory);
                    itemPanel.Controls.Add(btnRemove);

                    // Add the item panel to the FlowLayoutPanel
                    flowPanel.Controls.Add(itemPanel);
                }

                CalculateTotal(); // Calculate total across all tables
            }
        }

        private void BtnProcessedToPay_Click(object sender, EventArgs e)
        {
            // Collect the remaining items and the grand total
            string orderDetails = GetOrderDetails(); // Method to get item details
            decimal grandTotal = CalculateTotalValue(); // Method to calculate the total

            if (string.IsNullOrWhiteSpace(orderDetails) || grandTotal <= 0)
            {
                MessageBox.Show("Cannot process order. Please check your cart.");
                return;
            }

            // Save to the OrderSummary table
            SaveOrderSummary(ClientUsername, orderDetails, grandTotal);

            // Save the items to MyCart table if necessary
            SaveToMyCart(ClientUsername);

            // Empty the cart by removing all items from OrderPaints, OrderCleaners, and OrderTools tables
            EmptyCart();

            // Clear the items displayed in the FlowLayoutPanel
            flowPanel.Controls.Clear();
        }
        private void EmptyCart()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Remove all items for the specific user from OrderPaints, OrderCleaners, and OrderTools
                string deletePaints = "DELETE FROM OrderPaints WHERE ClientUsername = @Username";
                string deleteCleaners = "DELETE FROM OrderCleaners WHERE ClientUsername = @Username";
                string deleteTools = "DELETE FROM OrderTools WHERE ClientUsername = @Username";

                SqlCommand cmdPaints = new SqlCommand(deletePaints, con);
                SqlCommand cmdCleaners = new SqlCommand(deleteCleaners, con);
                SqlCommand cmdTools = new SqlCommand(deleteTools, con);

                cmdPaints.Parameters.AddWithValue("@Username", ClientUsername);
                cmdCleaners.Parameters.AddWithValue("@Username", ClientUsername);
                cmdTools.Parameters.AddWithValue("@Username", ClientUsername);

                // Execute the delete commands
                cmdPaints.ExecuteNonQuery();
                cmdCleaners.ExecuteNonQuery();
                cmdTools.ExecuteNonQuery();
            }
        }

        private string GetOrderDetails()
        {
            StringBuilder detailsBuilder = new StringBuilder();

            foreach (Control control in flowPanel.Controls)
            {
                if (control is Panel itemPanel)
                {
                    Label lblName = itemPanel.Controls.OfType<Label>().FirstOrDefault(l => l.Text.StartsWith("Name:"));
                    Label lblQty = itemPanel.Controls.OfType<Label>().FirstOrDefault(l => l.Text.StartsWith("Quantity:"));
                    Label lblPrice = itemPanel.Controls.OfType<Label>().FirstOrDefault(l => l.Text.StartsWith("Price:"));

                    if (lblName != null && lblQty != null && lblPrice != null)
                    {
                        detailsBuilder.AppendLine($"{lblName.Text}, {lblQty.Text}, {lblPrice.Text}");
                    }
                }
            }

            return detailsBuilder.ToString(); // Return the concatenated details
        }

        private decimal CalculateTotalValue()
        {
            decimal total = 0;

            foreach (Control control in flowPanel.Controls)
            {
                if (control is Panel itemPanel)
                {
                    Label lblPrice = itemPanel.Controls.OfType<Label>().FirstOrDefault(l => l.Text.StartsWith("Price:"));
                    Label lblQty = itemPanel.Controls.OfType<Label>().FirstOrDefault(l => l.Text.StartsWith("Quantity:"));

                    if (lblPrice != null && lblQty != null)
                    {
                        decimal price = Convert.ToDecimal(lblPrice.Text.Replace("Price: Rs.", "").Trim());
                        int quantity = Convert.ToInt32(lblQty.Text.Replace("Quantity: ", "").Trim());
                        total += price * quantity;
                    }
                }
            }

            // Add flat fee once to the total
            decimal flatFee = 1500; // Adjust this value if needed
            total += flatFee;

            return total; // Return the calculated total
        }

        private void SaveOrderSummary(string username, string orderDetails, decimal grandTotal)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string insertQuery = "INSERT INTO OrderSummary (ClientUsername, OrderDetails, GrandTotal) VALUES (@Username, @OrderDetails, @GrandTotal)";
                    SqlCommand cmd = new SqlCommand(insertQuery, con);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@OrderDetails", orderDetails);
                    cmd.Parameters.AddWithValue("@GrandTotal", grandTotal);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Order processed successfully!");
                var newForm = new CustomerDetails();
                newForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving order: " + ex.Message);
            }
        }
        private void SaveToMyCart(string username)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                foreach (Control control in flowPanel.Controls)
                {
                    if (control is Panel itemPanel)
                    {
                        Label lblName = itemPanel.Controls.OfType<Label>().FirstOrDefault(l => l.Text.StartsWith("Name:"));
                        Label lblQty = itemPanel.Controls.OfType<Label>().FirstOrDefault(l => l.Text.StartsWith("Quantity:"));
                        Label lblPrice = itemPanel.Controls.OfType<Label>().FirstOrDefault(l => l.Text.StartsWith("Price:"));
                        // Assuming you want to add a default value for Image and Liters
                        string image = ""; // Add your logic for image if available
                        decimal liters = 0; // Adjust this value based on your logic

                        if (lblName != null && lblQty != null && lblPrice != null)
                        {
                            string insertQuery = "INSERT INTO MyCart (OrderID, Image, Name, Quantity, Liters, Price, ClientUsername, Status) " +
                                                 "VALUES (@OrderID, @Image, @Name, @Quantity, @Liters, @Price, @ClientUsername, '@Status')";

                            SqlCommand cmd = new SqlCommand(insertQuery, con);
                            cmd.Parameters.AddWithValue("@OrderID", /* Add the logic for OrderID if needed */ 0); // Update this logic if needed
                            cmd.Parameters.AddWithValue("@Image", image);
                            cmd.Parameters.AddWithValue("@Name", lblName.Text.Replace("Name: ", "").Trim());
                            cmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(lblQty.Text.Replace("Quantity: ", "").Trim()));
                            cmd.Parameters.AddWithValue("@Liters", liters);
                            cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(lblPrice.Text.Replace("Price: Rs.", "").Trim()));
                            cmd.Parameters.AddWithValue("@ClientUsername", username);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private void CalculateTotal()
        {
            decimal total = 0;

            foreach (Control control in flowPanel.Controls)
            {
                if (control is Panel itemPanel)
                {
                    // Get the price and quantity from the controls inside the panel
                    Label lblPrice = itemPanel.Controls.OfType<Label>().FirstOrDefault(l => l.Text.StartsWith("Price:"));
                    Label lblQty = itemPanel.Controls.OfType<Label>().FirstOrDefault(l => l.Text.StartsWith("Quantity:"));

                    if (lblPrice != null && lblQty != null)
                    {
                        // Extract the numeric value from the price label correctly
                        decimal price = Convert.ToDecimal(lblPrice.Text.Replace("Price: Rs.", "").Trim());
                        int quantity = Convert.ToInt32(lblQty.Text.Replace("Quantity: ", "").Trim());

                        // Multiply price by quantity and add to total
                        total += price * quantity; // Add price for this item multiplied by quantity
                    }
                }
            }

            // Add a flat fee once to the total
            decimal flatFee = 1500; // Adjust this value if needed
            total += flatFee;

            // Display the total price
            labelTotal.Text = "Grand Total: Rs." + total.ToString("F2"); // Format the total to 2 decimal places
        }

       

        private void Cart_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var newForm = new UserHomePage();
            newForm.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
