namespace Tharini_Rajapaksha_KADSE202F_006
{
    partial class Cart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pictureBox1 = new PictureBox();
            guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            label1 = new Label();
            labelTotal = new Label();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.Image = Properties.Resources.Brush_Hour_removebg_preview;
            pictureBox1.Location = new Point(474, -76);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(422, 260);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 20;
            pictureBox1.TabStop = false;
            // 
            // guna2Button1
            // 
            guna2Button1.Animated = true;
            guna2Button1.AutoRoundedCorners = true;
            guna2Button1.BorderColor = Color.MidnightBlue;
            guna2Button1.BorderRadius = 21;
            guna2Button1.BorderThickness = 2;
            guna2Button1.CustomizableEdges = customizableEdges1;
            guna2Button1.DisabledState.BorderColor = Color.DarkGray;
            guna2Button1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button1.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button1.FillColor = Color.Transparent;
            guna2Button1.FocusedColor = Color.DimGray;
            guna2Button1.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guna2Button1.ForeColor = Color.MidnightBlue;
            guna2Button1.Image = Properties.Resources._340;
            guna2Button1.Location = new Point(12, 24);
            guna2Button1.Name = "guna2Button1";
            guna2Button1.PressedColor = Color.DimGray;
            guna2Button1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Button1.Size = new Size(250, 44);
            guna2Button1.TabIndex = 22;
            guna2Button1.Text = "Continue Shopping";
            guna2Button1.Click += guna2Button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.DimGray;
            label1.Location = new Point(12, 113);
            label1.Name = "label1";
            label1.Size = new Size(250, 46);
            label1.TabIndex = 23;
            label1.Text = "Shopping Cart";
            // 
            // labelTotal
            // 
            labelTotal.AutoSize = true;
            labelTotal.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            labelTotal.Location = new Point(356, 609);
            labelTotal.Name = "labelTotal";
            labelTotal.Size = new Size(139, 28);
            labelTotal.TabIndex = 25;
            labelTotal.Text = "Grand Total   :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold);
            label2.ForeColor = Color.DimGray;
            label2.Location = new Point(723, 113);
            label2.Name = "label2";
            label2.Size = new Size(117, 46);
            label2.TabIndex = 27;
            label2.Text = "label2";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label3.ForeColor = Color.DimGray;
            label3.Location = new Point(356, 576);
            label3.Name = "label3";
            label3.Size = new Size(242, 28);
            label3.TabIndex = 28;
            label3.Text = "Shipping Fee : Rs.1500.00";
            // 
            // Cart
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(963, 697);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(labelTotal);
            Controls.Add(label1);
            Controls.Add(guna2Button1);
            Controls.Add(pictureBox1);
            Name = "Cart";
            Text = "Cart";
            Load += Cart_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Label label1;
        private Label labelTotal;
        private Label label2;
        private Label label3;
    }
}
