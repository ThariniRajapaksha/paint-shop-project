using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tharini_Rajapaksha_KADSE202F_006
{
    public partial class ItemsOptions : Form
    {
        public ItemsOptions()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var newForm = new AddNewItems();
            newForm.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var newForm = new AdminPanel();
            newForm.Show();
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            var newForm = new EditItems();
            newForm.Show();
            this.Hide();
        }
    }
}
