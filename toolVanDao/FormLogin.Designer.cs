namespace toolVanDao
{
    partial class FormLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtMST = new DevExpress.XtraEditors.TextEdit();
            this.btn_DN = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtMK = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtTK = new DevExpress.XtraEditors.TextEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMST.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMK.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTK.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.txtMST);
            this.groupBox1.Controls.Add(this.btn_DN);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.txtMK);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.txtTK);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBox1.Location = new System.Drawing.Point(57, 87);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(325, 162);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Đăng nhập";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(44, 28);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(72, 17);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Mã số thuế:";
            this.labelControl3.Click += new System.EventHandler(this.labelControl3_Click);
            // 
            // txtMST
            // 
            this.txtMST.Location = new System.Drawing.Point(133, 25);
            this.txtMST.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMST.Name = "txtMST";
            this.txtMST.Size = new System.Drawing.Size(157, 22);
            this.txtMST.TabIndex = 0;
            // 
            // btn_DN
            // 
            this.btn_DN.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btn_DN.Appearance.Options.UseForeColor = true;
            this.btn_DN.Location = new System.Drawing.Point(121, 121);
            this.btn_DN.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_DN.Name = "btn_DN";
            this.btn_DN.Size = new System.Drawing.Size(87, 33);
            this.btn_DN.TabIndex = 3;
            this.btn_DN.Text = "Đăng nhập";
            this.btn_DN.Click += new System.EventHandler(this.btn_DN_Click);
            this.btn_DN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btn_DN_KeyDown);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(45, 85);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(61, 17);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Mật khẩu:";
            // 
            // txtMK
            // 
            this.txtMK.Location = new System.Drawing.Point(133, 82);
            this.txtMK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMK.Name = "txtMK";
            this.txtMK.Properties.UseSystemPasswordChar = true;
            this.txtMK.Size = new System.Drawing.Size(157, 22);
            this.txtMK.TabIndex = 2;
            this.txtMK.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMK_KeyDown);
            this.txtMK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMK_KeyPress);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(44, 57);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(64, 17);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Tài khoản:";
            // 
            // txtTK
            // 
            this.txtTK.Location = new System.Drawing.Point(133, 54);
            this.txtTK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTK.Name = "txtTK";
            this.txtTK.Size = new System.Drawing.Size(157, 22);
            this.txtTK.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.Location = new System.Drawing.Point(90, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(262, 71);
            this.panel1.TabIndex = 1;
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 258);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormLogin_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormLogin_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMST.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMK.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTK.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton btn_DN;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtMK;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtTK;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtMST;
        private System.Windows.Forms.Panel panel1;
    }
}