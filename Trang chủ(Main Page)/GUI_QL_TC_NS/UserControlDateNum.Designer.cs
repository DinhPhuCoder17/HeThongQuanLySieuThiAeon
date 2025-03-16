namespace Trang_chu_Main_Page_.GUI_QL_TC_NS
{
    partial class UserControlDateNum
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lb_dateNumber = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_dateNumber
            // 
            this.lb_dateNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lb_dateNumber.BackColor = System.Drawing.Color.Transparent;
            this.lb_dateNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_dateNumber.Location = new System.Drawing.Point(46, 0);
            this.lb_dateNumber.Margin = new System.Windows.Forms.Padding(0);
            this.lb_dateNumber.Name = "lb_dateNumber";
            this.lb_dateNumber.Size = new System.Drawing.Size(85, 22);
            this.lb_dateNumber.TabIndex = 0;
            this.lb_dateNumber.Text = "22/1/2025";
            this.lb_dateNumber.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lb_dateNumber);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(204, 32);
            this.panel1.TabIndex = 1;
            // 
            // UserControlDateNum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.Name = "UserControlDateNum";
            this.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.Size = new System.Drawing.Size(208, 32);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lb_dateNumber;
        private System.Windows.Forms.Panel panel1;
    }
}
