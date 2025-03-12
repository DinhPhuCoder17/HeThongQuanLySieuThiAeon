namespace Trang_chu_Main_Page_.DSHangHoa_Dat
{
    partial class HangHoaNCC
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
            this.guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.imgImage = new Guna.UI2.WinForms.Guna2PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2CustomGradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgImage)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2CustomGradientPanel1
            // 
            this.guna2CustomGradientPanel1.Controls.Add(this.label3);
            this.guna2CustomGradientPanel1.Controls.Add(this.imgImage);
            this.guna2CustomGradientPanel1.Controls.Add(this.label4);
            this.guna2CustomGradientPanel1.Controls.Add(this.label1);
            this.guna2CustomGradientPanel1.Location = new System.Drawing.Point(0, 3);
            this.guna2CustomGradientPanel1.Name = "guna2CustomGradientPanel1";
            this.guna2CustomGradientPanel1.Size = new System.Drawing.Size(243, 324);
            this.guna2CustomGradientPanel1.TabIndex = 0;
            this.guna2CustomGradientPanel1.Click += new System.EventHandler(this.guna2PictureBox1_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(-3, 237);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 40);
            this.label3.TabIndex = 8;
            this.label3.Text = "Tên nhà cung cấp:";
            this.label3.Click += new System.EventHandler(this.guna2PictureBox1_Click);
            // 
            // imgImage
            // 
            this.imgImage.ImageRotate = 0F;
            this.imgImage.Location = new System.Drawing.Point(4, 0);
            this.imgImage.Name = "imgImage";
            this.imgImage.Size = new System.Drawing.Size(242, 152);
            this.imgImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgImage.TabIndex = 7;
            this.imgImage.TabStop = false;
            this.imgImage.Click += new System.EventHandler(this.guna2PictureBox1_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.OrangeRed;
            this.label4.Location = new System.Drawing.Point(3, 277);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 47);
            this.label4.TabIndex = 6;
            this.label4.Text = "Giá tiền:";
            this.label4.Click += new System.EventHandler(this.guna2PictureBox1_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 173);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 64);
            this.label1.TabIndex = 5;
            this.label1.Text = "Tên hàng hóa: ";
            this.label1.Click += new System.EventHandler(this.guna2PictureBox1_Click);
            // 
            // HangHoaNCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.guna2CustomGradientPanel1);
            this.Name = "HangHoaNCC";
            this.Size = new System.Drawing.Size(246, 330);
            this.guna2CustomGradientPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel1;
        public System.Windows.Forms.Label label3;
        public Guna.UI2.WinForms.Guna2PictureBox imgImage;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label1;
    }
}
