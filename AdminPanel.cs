using Guna.UI2.WinForms;
using Microsoft.Data.SqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tharini_Rajapaksha_KADSE202F_006
{
    public partial class AdminPanel : Form
    {
        string connectionString = @"Data Source=LAPTOP-E4VLV2RS\SQLEXPRESS;Initial Catalog=BrushHour;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;";
        

        private Guna2CircleProgressBar progressTotalBills;
        private Guna2CircleProgressBar progressTotalIncome;
        private Guna2CircleProgressBar progressTotalUsers;

        private Label labelTotalBills;
        private Label labelTotalIncome;
        private Label labelTotalUsers;

        public AdminPanel()
        {
            InitializeComponent();
            InitializeDashboardElements();
        }

        private void InitializeDashboardElements()
        {
            // These methods fetch real data from your database
            int totalBills = GetTotalBills();
            decimal totalIncome = GetTotalIncome();
            int totalUsers = GetTotalUsers();
            // Initialize Circle ProgressBars
            this.progressTotalBills = new Guna2CircleProgressBar();
            this.progressTotalIncome = new Guna2CircleProgressBar();
            this.progressTotalUsers = new Guna2CircleProgressBar();

            // Initialize Labels for progress bars
            this.labelTotalBills = new Label();
            this.labelTotalIncome = new Label();
            this.labelTotalUsers = new Label();

            // Set label properties
            SetupLabel(this.labelTotalBills, $"Total Bills: {totalBills}", new Point(80, 240));    // Adjusted X position
            SetupLabel(this.labelTotalIncome, $"Total Income: Rs.{totalIncome:F2}", new Point(380, 240));  // Adjusted X position
            SetupLabel(this.labelTotalUsers, $"Total Users: {totalUsers}", new Point(680, 240));    // Adjusted X position

            // Add the labels to panel2
            panel2.Controls.Add(this.labelTotalBills);
            panel2.Controls.Add(this.labelTotalIncome);
            panel2.Controls.Add(this.labelTotalUsers);

            // Set positions inside panel2 for progress bars with appropriate spacing
            this.progressTotalBills.Location = new Point(80, 20);   // X: 80, Y: 20
            this.progressTotalIncome.Location = new Point(380, 20); // Adjust for increased size (X: 380)
            this.progressTotalUsers.Location = new Point(680, 20);  // Adjust for increased size (X: 680)

            // Add progress bars to panel2
            panel2.Controls.Add(this.progressTotalBills);
            panel2.Controls.Add(this.progressTotalIncome);
            panel2.Controls.Add(this.progressTotalUsers);
        }

        private void SetupLabel(Label label, string text, Point location)
        {
            label.AutoSize = true;
            label.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            label.ForeColor = Color.Black;
            label.Text = text;
            label.Location = location;
        }

        private void SetupCircleProgressBar(Guna2CircleProgressBar progressBar, int value, string labelText, bool isCurrency = false)
        {
            // Increase the size of the circle progress bar
            progressBar.Size = new System.Drawing.Size(250, 250);  // Adjust size if needed
            progressBar.FillColor = System.Drawing.Color.FromArgb(200, 213, 218);
            progressBar.ProgressColor = System.Drawing.Color.FromArgb(51, 102, 255);
            progressBar.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            progressBar.ShadowDecoration.Parent = progressBar;
            progressBar.Value = value;

            // Label inside progress bar (adjust location for the new size)
            Label label = new Label();
            label.AutoSize = true;
            label.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);  // Increased font size
            label.ForeColor = System.Drawing.Color.Black;
            label.Location = new System.Drawing.Point(70, 85); // Adjust the text position for the new size
            label.Text = isCurrency ? $"Rs.{value}" : value.ToString();

            // Label for the description below the progress bar
            Label descLabel = new Label();
            descLabel.AutoSize = true;
            descLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);  // Increased font size
            descLabel.ForeColor = System.Drawing.Color.Black;
            descLabel.Location = new System.Drawing.Point(60, 175); // Adjust position below the progress bar
            descLabel.Text = labelText;

            progressBar.Controls.Add(label);
            progressBar.Controls.Add(descLabel);
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            LoadStatistics();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Refresh statistics when button is clicked
            LoadStatistics();
        }

        private void LoadStatistics()
        {
            // These methods fetch real data from your database
            int totalBills = GetTotalBills();
            decimal totalIncome = GetTotalIncome();
            int totalUsers = GetTotalUsers();

            // Update existing labels
            labelTotalBills.Text = $"Total Bills: {totalBills}";
            labelTotalIncome.Text = $"Total Income: Rs.{totalIncome:F2}";
            labelTotalUsers.Text = $"Total Users: {totalUsers}";

            // Update progress bars with the fetched data
            progressTotalBills.Value = totalBills;
            progressTotalIncome.Value = (int)totalIncome;  // Assuming the CircleProgressBar accepts only integer values
            progressTotalUsers.Value = totalUsers;
        }

        private int GetTotalBills()
        {
            int count = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Bill";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    count = (int)cmd.ExecuteScalar();
                }
            }
            return count;
        }

        private decimal GetTotalIncome()
        {
            decimal totalIncome = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT SUM(GrandTotal) FROM Bill";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    totalIncome = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
            }
            return totalIncome;
        }

        private int GetTotalUsers()
        {
            int count = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM UserRegistration";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    count = (int)cmd.ExecuteScalar();
                }
            }
            return count;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var newForm = new CustomerOrders();
            newForm.Show();
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            var newForm = new StaffMembers();
            newForm.Show();
            this.Hide();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            var newForm = new StatusOrders();
            newForm.Show();
            this.Hide();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            var newForm = new BillReport();
            newForm.Show();
            this.Hide();
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            // Confirm logout with a dialog
            var result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // Close the current form
                this.Close();

            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var newForm = new AdminPanel();
            newForm.Show();
            this.Hide();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            var newForm = new NewStaffMember();
            newForm.Show();
            this.Hide();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            var newForm = new ItemsOptions();
            newForm.Show();
            this.Hide();
        }
    }
}
