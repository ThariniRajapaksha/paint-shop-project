using Guna.UI2.WinForms;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace Tharini_Rajapaksha_KADSE202F_006
{
    public partial class Bill : Form
    {
        string connectionString = @"Data Source=LAPTOP-E4VLV2RS\SQLEXPRESS;Initial Catalog=BrushHour;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;";

        public Bill()
        {
            InitializeComponent();
        }

        private void Bill_Load(object sender, EventArgs e)
        {
            LoadOrderDetails();
        }

        // Method to load order details from SQL and display them in FlowLayoutPanel
        private void LoadOrderDetails()
        {
            flowLayoutPanel1.Controls.Clear(); // Clear existing controls

            // Add Invoice title
            Label lblInvoiceTitle = new Label
            {
                Text = "Bill Settlement",
                Font = new Font("Arial", 20, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.DimGray,
                Location = new Point(10, 20),
                Margin = new Padding(0, 10, 0, 10) // Add vertical margin below the title
            };
            flowLayoutPanel1.Controls.Add(lblInvoiceTitle);

            // SQL query to fetch order data from SubmittedOrder
            string query = @"
        SELECT 
            c.ClientUsername, 
            c.Name, 
            c.Address, 
            c.Contact, 
            o.OrderDetails, 
            o.GrandTotal, 
            o.OrderDate 
        FROM 
            SubmittedOrder o
        JOIN 
            CustomerDetails c ON o.ClientUsername = c.ClientUsername";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    List<(string clientUsername, string name, string address, string contact, string orderDetails, decimal grandTotal, DateTime orderDate)> orderData = new List<(string, string, string, string, string, decimal, DateTime)>();

                    // Fetching data from SubmittedOrder
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            // Store data for later insertion into Bill table
                            string clientUsername = reader["ClientUsername"].ToString();
                            string name = reader["Name"].ToString();
                            string address = reader["Address"].ToString();
                            string contact = reader["Contact"].ToString();
                            string orderDetails = reader["OrderDetails"].ToString();
                            decimal grandTotal = Convert.ToDecimal(reader["GrandTotal"]);
                            DateTime orderDate = Convert.ToDateTime(reader["OrderDate"]);

                            // Store data to insert into Bill table later
                            orderData.Add((clientUsername, name, address, contact, orderDetails, grandTotal, orderDate));

                            // Create a Panel to hold each order's details
                            RoundedPanel itemPanel = new RoundedPanel
                            {
                                Size = new Size(700, 400),  // Increased width to 700
                                Margin = new Padding(10, 10, 10, 10),
                                CornerRadius = 20,
                                BackColor = Color.FloralWhite,
                                Padding = new Padding(15), // Padding inside the panel remains the same
                                Location = new Point(0, 100)
                            };

                            // Adjust Font Size
                            Font labelFont = new Font("Arial", 14, FontStyle.Regular); // Increase font size to 14

                            // Create Labels for each data field
                            Label lblClientUsername = new Label
                            {
                                Text = "Client Username: " + clientUsername,
                                AutoSize = true,
                                Font = labelFont,
                                Location = new Point(10, 10),
                                Margin = new Padding(0, 15, 0, 0) // Add vertical spacing between labels
                            };

                            Label lblName = new Label
                            {
                                Text = "Name: " + name,
                                AutoSize = true,
                                Font = labelFont,
                                Location = new Point(10, 50), // Adjusted Location for spacing
                                Margin = new Padding(0, 15, 0, 0) // Add vertical spacing
                            };

                            Label lblAddress = new Label
                            {
                                Text = "Address: " + address,
                                AutoSize = true,
                                Font = labelFont,
                                Location = new Point(10, 90), // Adjusted Location for spacing
                                Margin = new Padding(0, 15, 0, 0)
                            };

                            Label lblContact = new Label
                            {
                                Text = "Contact: " + contact,
                                AutoSize = true,
                                Font = labelFont,
                                Location = new Point(10, 130), // Adjusted Location for spacing
                                Margin = new Padding(0, 15, 0, 0)
                            };

                            Label lblOrderDetails = new Label
                            {
                                Text = "Order Details: " + orderDetails,
                                AutoSize = true,
                                Font = labelFont,
                                Location = new Point(10, 170), // Adjusted Location for spacing
                                Margin = new Padding(0, 15, 0, 0)
                            };

                            Label lblGrandTotal = new Label
                            {
                                Text = "Grand Total: " + grandTotal.ToString("C"),
                                AutoSize = true,
                                Font = new Font("Arial", 14, FontStyle.Bold), // Increased font size and bold for total
                                Location = new Point(10, 210), // Adjusted Location for spacing
                                Margin = new Padding(0, 15, 0, 0)
                            };

                            Label lblOrderDate = new Label
                            {
                                Text = "Order Date: " + orderDate.ToString("g"),
                                AutoSize = true,
                                Font = labelFont,
                                Location = new Point(10, 250), // Adjusted Location for spacing
                                Margin = new Padding(0, 15, 0, 0)
                            };

                            // Add the labels to the panel
                            itemPanel.Controls.Add(lblClientUsername);
                            itemPanel.Controls.Add(lblName);
                            itemPanel.Controls.Add(lblAddress);
                            itemPanel.Controls.Add(lblContact);
                            itemPanel.Controls.Add(lblOrderDetails);
                            itemPanel.Controls.Add(lblGrandTotal);
                            itemPanel.Controls.Add(lblOrderDate);

                            // Add the panel to the FlowLayoutPanel
                            flowLayoutPanel1.Controls.Add(itemPanel);
                        }

                        reader.Close(); // Close the DataReader before executing any other commands
                    }

                    // Insert the data into the Bill table after the DataReader is closed
                    foreach (var order in orderData)
                    {
                        string insertBillQuery = @"
                INSERT INTO Bill (ClientUsername, Name, Address, Contact, OrderDetails, GrandTotal, OrderDate)
                VALUES (@ClientUsername, @Name, @Address, @Contact, @OrderDetails, @GrandTotal, @OrderDate)";

                        using (SqlCommand insertCmd = new SqlCommand(insertBillQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@ClientUsername", order.clientUsername);
                            insertCmd.Parameters.AddWithValue("@Name", order.name);
                            insertCmd.Parameters.AddWithValue("@Address", order.address);
                            insertCmd.Parameters.AddWithValue("@Contact", order.contact);
                            insertCmd.Parameters.AddWithValue("@OrderDetails", order.orderDetails);
                            insertCmd.Parameters.AddWithValue("@GrandTotal", order.grandTotal);
                            insertCmd.Parameters.AddWithValue("@OrderDate", order.orderDate);

                            insertCmd.ExecuteNonQuery();
                        }
                    }

                    // Delete the data from the SubmittedOrder table
                    string deleteOrderQuery = "DELETE FROM SubmittedOrder";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteOrderQuery, conn))
                    {
                        deleteCmd.ExecuteNonQuery();
                    }
                }

                // Add Thank You message at the end of FlowLayoutPanel
                Label lblThankYou = new Label
                {
                    Text = "Thank you for ordering from us!",
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    AutoSize = true,
                    ForeColor = Color.DarkGray,
                    Location = new Point(10, 10)
                };
                flowLayoutPanel1.Controls.Add(lblThankYou);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading order details: " + ex.Message);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var newForm = new UserHomePage();
            newForm.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
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
