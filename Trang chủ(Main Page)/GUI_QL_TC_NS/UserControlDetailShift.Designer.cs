namespace Trang_chu_Main_Page_.GUI_QL_TC_NS
{
    partial class UserControlDetailShift
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
            this.lb_nameShift = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lb_timeDetail = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_nameShift
            // 
            this.lb_nameShift.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_nameShift.BackColor = System.Drawing.Color.Transparent;
            this.lb_nameShift.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_nameShift.Location = new System.Drawing.Point(44, 40);
            this.lb_nameShift.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.lb_nameShift.Name = "lb_nameShift";
            this.lb_nameShift.Size = new System.Drawing.Size(102, 20);
            this.lb_nameShift.TabIndex = 0;
            this.lb_nameShift.Text = "CA THƯỜNG";
            this.lb_nameShift.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lb_timeDetail);
            this.panel1.Controls.Add(this.lb_nameShift);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(195, 195);
            this.panel1.TabIndex = 1;
            // 
            // lb_timeDetail
            // 
            this.lb_timeDetail.BackColor = System.Drawing.Color.Transparent;
            this.lb_timeDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_timeDetail.Location = new System.Drawing.Point(55, 66);
            this.lb_timeDetail.Name = "lb_timeDetail";
            this.lb_timeDetail.Size = new System.Drawing.Size(75, 18);
            this.lb_timeDetail.TabIndex = 1;
            this.lb_timeDetail.Text = "16:30 - 21:00";
            this.lb_timeDetail.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UserControlDetailShift
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UserControlDetailShift";
            this.Size = new System.Drawing.Size(195, 195);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lb_nameShift;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lb_timeDetail;
    }
}
