namespace Trang_chủ_Main_Page_
{
    partial class financialManagement
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(financialManagement));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.dtg_Bill = new Guna.UI2.WinForms.Guna2DataGridView();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.dtp_Bill_Start = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dtp_Bill_End = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblCustomerList = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btn_Bill_Cancel = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btn_Bill_FilterDate = new Guna.UI2.WinForms.Guna2GradientButton();
            this.guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.txt_Bill_SearchBar = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_Bill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            resources.ApplyResources(this.guna2Panel1, "guna2Panel1");
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.Controls.Add(this.label4);
            this.guna2Panel1.Controls.Add(this.guna2CirclePictureBox1);
            this.guna2Panel1.Controls.Add(this.txt_Bill_SearchBar);
            this.guna2Panel1.Name = "guna2Panel1";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // dtg_Bill
            // 
            resources.ApplyResources(this.dtg_Bill, "dtg_Bill");
            this.dtg_Bill.AllowUserToAddRows = false;
            this.dtg_Bill.AllowUserToResizeColumns = false;
            this.dtg_Bill.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            this.dtg_Bill.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtg_Bill.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.OrangeRed;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtg_Bill.DefaultCellStyle = dataGridViewCellStyle7;
            this.dtg_Bill.EnableHeadersVisualStyles = true;
            this.dtg_Bill.GridColor = System.Drawing.Color.White;
            this.dtg_Bill.Name = "dtg_Bill";
            this.dtg_Bill.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtg_Bill.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dtg_Bill.RowHeadersVisible = false;
            this.dtg_Bill.RowTemplate.DividerHeight = 5;
            this.dtg_Bill.RowTemplate.Height = 40;
            this.dtg_Bill.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dtg_Bill.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dtg_Bill.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dtg_Bill.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dtg_Bill.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dtg_Bill.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dtg_Bill.ThemeStyle.GridColor = System.Drawing.Color.White;
            this.dtg_Bill.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.White;
            this.dtg_Bill.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dtg_Bill.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtg_Bill.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.DimGray;
            this.dtg_Bill.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtg_Bill.ThemeStyle.HeaderStyle.Height = 40;
            this.dtg_Bill.ThemeStyle.ReadOnly = true;
            this.dtg_Bill.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dtg_Bill.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dtg_Bill.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtg_Bill.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dtg_Bill.ThemeStyle.RowsStyle.Height = 40;
            this.dtg_Bill.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.OrangeRed;
            this.dtg_Bill.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dtg_Bill.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.guna2DataGridView1_CellContentClick);
            this.dtg_Bill.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtg_Bill_CellContentDoubleClick);
            this.dtg_Bill.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtg_Bill_CellDoubleClick);
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 22;
            this.guna2Elipse1.TargetControl = this.dtg_Bill;
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Name = "label11";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // dtp_Bill_Start
            // 
            resources.ApplyResources(this.dtp_Bill_Start, "dtp_Bill_Start");
            this.dtp_Bill_Start.Animated = true;
            this.dtp_Bill_Start.BackColor = System.Drawing.Color.Transparent;
            this.dtp_Bill_Start.BorderRadius = 8;
            this.dtp_Bill_Start.Checked = true;
            this.dtp_Bill_Start.FillColor = System.Drawing.Color.White;
            this.dtp_Bill_Start.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_Bill_Start.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtp_Bill_Start.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_Bill_Start.Name = "dtp_Bill_Start";
            this.dtp_Bill_Start.Value = new System.DateTime(2025, 3, 3, 14, 48, 32, 894);
            this.dtp_Bill_Start.ValueChanged += new System.EventHandler(this.guna2DateTimePicker2_ValueChanged);
            // 
            // dtp_Bill_End
            // 
            resources.ApplyResources(this.dtp_Bill_End, "dtp_Bill_End");
            this.dtp_Bill_End.Animated = true;
            this.dtp_Bill_End.BackColor = System.Drawing.Color.Transparent;
            this.dtp_Bill_End.BorderRadius = 8;
            this.dtp_Bill_End.Checked = true;
            this.dtp_Bill_End.FillColor = System.Drawing.Color.White;
            this.dtp_Bill_End.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_Bill_End.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtp_Bill_End.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_Bill_End.Name = "dtp_Bill_End";
            this.dtp_Bill_End.Value = new System.DateTime(2025, 3, 3, 14, 48, 32, 894);
            // 
            // lblCustomerList
            // 
            resources.ApplyResources(this.lblCustomerList, "lblCustomerList");
            this.lblCustomerList.BackColor = System.Drawing.Color.Transparent;
            this.lblCustomerList.Name = "lblCustomerList";
            // 
            // btn_Bill_Cancel
            // 
            resources.ApplyResources(this.btn_Bill_Cancel, "btn_Bill_Cancel");
            this.btn_Bill_Cancel.Animated = true;
            this.btn_Bill_Cancel.BackColor = System.Drawing.Color.Transparent;
            this.btn_Bill_Cancel.BorderRadius = 8;
            this.btn_Bill_Cancel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_Bill_Cancel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_Bill_Cancel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_Bill_Cancel.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_Bill_Cancel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_Bill_Cancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(124)))), ((int)(((byte)(99)))));
            this.btn_Bill_Cancel.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(78)))), ((int)(((byte)(90)))));
            this.btn_Bill_Cancel.ForeColor = System.Drawing.Color.White;
            this.btn_Bill_Cancel.Name = "btn_Bill_Cancel";
            this.btn_Bill_Cancel.Click += new System.EventHandler(this.btn_Bill_Cancel_Click);
            // 
            // btn_Bill_FilterDate
            // 
            resources.ApplyResources(this.btn_Bill_FilterDate, "btn_Bill_FilterDate");
            this.btn_Bill_FilterDate.Animated = true;
            this.btn_Bill_FilterDate.BackColor = System.Drawing.Color.Transparent;
            this.btn_Bill_FilterDate.BorderRadius = 8;
            this.btn_Bill_FilterDate.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_Bill_FilterDate.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_Bill_FilterDate.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_Bill_FilterDate.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_Bill_FilterDate.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_Bill_FilterDate.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(124)))), ((int)(((byte)(99)))));
            this.btn_Bill_FilterDate.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(78)))), ((int)(((byte)(90)))));
            this.btn_Bill_FilterDate.ForeColor = System.Drawing.Color.White;
            this.btn_Bill_FilterDate.Name = "btn_Bill_FilterDate";
            this.btn_Bill_FilterDate.Click += new System.EventHandler(this.btn_Bill_FilterDate_Click);
            // 
            // guna2CirclePictureBox1
            // 
            resources.ApplyResources(this.guna2CirclePictureBox1, "guna2CirclePictureBox1");
            this.guna2CirclePictureBox1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.guna2CirclePictureBox1.ImageRotate = 0F;
            this.guna2CirclePictureBox1.Name = "guna2CirclePictureBox1";
            this.guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CirclePictureBox1.TabStop = false;
            // 
            // txt_Bill_SearchBar
            // 
            resources.ApplyResources(this.txt_Bill_SearchBar, "txt_Bill_SearchBar");
            this.txt_Bill_SearchBar.Animated = true;
            this.txt_Bill_SearchBar.BackColor = System.Drawing.Color.White;
            this.txt_Bill_SearchBar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.txt_Bill_SearchBar.BorderRadius = 8;
            this.txt_Bill_SearchBar.BorderThickness = 0;
            this.txt_Bill_SearchBar.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_Bill_SearchBar.DefaultText = "";
            this.txt_Bill_SearchBar.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_Bill_SearchBar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_Bill_SearchBar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_Bill_SearchBar.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_Bill_SearchBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.txt_Bill_SearchBar.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_Bill_SearchBar.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_Bill_SearchBar.IconLeft = global::Trang_chu_Main_Page_.Properties.Resources.Thiết_kế_chưa_có_tên__14_;
            this.txt_Bill_SearchBar.Name = "txt_Bill_SearchBar";
            this.txt_Bill_SearchBar.PlaceholderForeColor = System.Drawing.Color.Black;
            this.txt_Bill_SearchBar.PlaceholderText = "Enter Order Code";
            this.txt_Bill_SearchBar.SelectedText = "";
            this.txt_Bill_SearchBar.TextChanged += new System.EventHandler(this.txt_Bill_SearchBar_TextChanged);
            // 
            // financialManagement
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btn_Bill_FilterDate);
            this.Controls.Add(this.btn_Bill_Cancel);
            this.Controls.Add(this.lblCustomerList);
            this.Controls.Add(this.dtp_Bill_End);
            this.Controls.Add(this.dtp_Bill_Start);
            this.Controls.Add(this.dtg_Bill);
            this.Controls.Add(this.guna2Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "financialManagement";
            this.Load += new System.EventHandler(this.financialManagement_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_Bill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2TextBox txt_Bill_SearchBar;
        private Guna.UI2.WinForms.Guna2DataGridView dtg_Bill;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private System.Windows.Forms.Label label11;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtp_Bill_Start;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtp_Bill_End;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCustomerList;
        private Guna.UI2.WinForms.Guna2GradientButton btn_Bill_Cancel;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
        private Guna.UI2.WinForms.Guna2GradientButton btn_Bill_FilterDate;
    }
}