using DevExpress.XtraEditors;

namespace toolVanDao
{
    partial class FormGetXML
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

            /*pdfViewer2.CloseDocument();
            pdfViewer1.Dispose();
            pdfViewer2.Dispose();
            fileStream.Dispose();*/
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
        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGetXML));
            this.txtTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.txtDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.check78 = new System.Windows.Forms.CheckBox();
            this.check32 = new System.Windows.Forms.CheckBox();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtkyhieu = new DevExpress.XtraEditors.TextEdit();
            this.txtmauso = new DevExpress.XtraEditors.TextEdit();
            this.chkAll = new DevExpress.XtraEditors.CheckEdit();
            this.btn_loaddata = new DevExpress.XtraEditors.SimpleButton();
            this.btn_get = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.searchLookUpEdit1 = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.checkedListBoxControl1 = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.txtPath = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btn_Path = new DevExpress.XtraEditors.SimpleButton();
            this.btn_download = new DevExpress.XtraEditors.SimpleButton();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.Btn_Download_PDF = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.btn_stop = new DevExpress.XtraEditors.SimpleButton();
            this.btn_ViewPDF = new DevExpress.XtraEditors.SimpleButton();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDatai = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtkyhieu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtmauso.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTuNgay
            // 
            this.txtTuNgay.EditValue = new System.DateTime(2022, 4, 20, 0, 0, 0, 0);
            this.txtTuNgay.Location = new System.Drawing.Point(31, 68);
            this.txtTuNgay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTuNgay.Name = "txtTuNgay";
            this.txtTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtTuNgay.Size = new System.Drawing.Size(100, 22);
            this.txtTuNgay.TabIndex = 0;
            // 
            // txtDenNgay
            // 
            this.txtDenNgay.EditValue = new System.DateTime(2022, 4, 20, 0, 0, 0, 0);
            this.txtDenNgay.Location = new System.Drawing.Point(156, 68);
            this.txtDenNgay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDenNgay.Name = "txtDenNgay";
            this.txtDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDenNgay.Size = new System.Drawing.Size(100, 22);
            this.txtDenNgay.TabIndex = 1;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.groupBox1);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.txtkyhieu);
            this.groupControl1.Controls.Add(this.txtmauso);
            this.groupControl1.Controls.Add(this.chkAll);
            this.groupControl1.Controls.Add(this.btn_loaddata);
            this.groupControl1.Controls.Add(this.btn_get);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.searchLookUpEdit1);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtTuNgay);
            this.groupControl1.Controls.Add(this.txtDenNgay);
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(885, 108);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Thông tin cần lấy";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.check78);
            this.groupBox1.Controls.Add(this.check32);
            this.groupBox1.Location = new System.Drawing.Point(262, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(154, 48);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chọn loại hóa đơn";
            // 
            // check78
            // 
            this.check78.AutoSize = true;
            this.check78.Location = new System.Drawing.Point(83, 22);
            this.check78.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.check78.Name = "check78";
            this.check78.Size = new System.Drawing.Size(65, 21);
            this.check78.TabIndex = 12;
            this.check78.Text = "HD78";
            this.check78.UseVisualStyleBackColor = true;
            this.check78.CheckedChanged += new System.EventHandler(this.check78_CheckedChanged);
            // 
            // check32
            // 
            this.check32.AutoSize = true;
            this.check32.Location = new System.Drawing.Point(6, 22);
            this.check32.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.check32.Name = "check32";
            this.check32.Size = new System.Drawing.Size(65, 21);
            this.check32.TabIndex = 11;
            this.check32.Text = "HD32";
            this.check32.UseVisualStyleBackColor = true;
            this.check32.CheckedChanged += new System.EventHandler(this.check32_CheckedChanged);
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(706, 49);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(50, 17);
            this.labelControl6.TabIndex = 10;
            this.labelControl6.Text = "Ký hiệu:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(637, 49);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 17);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "Mẫu số:";
            // 
            // txtkyhieu
            // 
            this.txtkyhieu.Location = new System.Drawing.Point(706, 70);
            this.txtkyhieu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtkyhieu.Name = "txtkyhieu";
            this.txtkyhieu.Size = new System.Drawing.Size(62, 22);
            this.txtkyhieu.TabIndex = 5;
            // 
            // txtmauso
            // 
            this.txtmauso.Location = new System.Drawing.Point(637, 70);
            this.txtmauso.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtmauso.Name = "txtmauso";
            this.txtmauso.Size = new System.Drawing.Size(62, 22);
            this.txtmauso.TabIndex = 4;
            // 
            // chkAll
            // 
            this.chkAll.Location = new System.Drawing.Point(5, 70);
            this.chkAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkAll.Name = "chkAll";
            this.chkAll.Properties.Caption = "";
            this.chkAll.Size = new System.Drawing.Size(20, 24);
            this.chkAll.TabIndex = 8;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // btn_loaddata
            // 
            this.btn_loaddata.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_loaddata.Appearance.Options.UseFont = true;
            this.btn_loaddata.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_loaddata.ImageOptions.Image")));
            this.btn_loaddata.Location = new System.Drawing.Point(775, 57);
            this.btn_loaddata.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_loaddata.Name = "btn_loaddata";
            this.btn_loaddata.Size = new System.Drawing.Size(105, 47);
            this.btn_loaddata.TabIndex = 6;
            this.btn_loaddata.Text = "Tải dữ liệu";
            this.btn_loaddata.Click += new System.EventHandler(this.btn_loaddata_Click);
            // 
            // btn_get
            // 
            this.btn_get.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_get.ImageOptions.Image")));
            this.btn_get.Location = new System.Drawing.Point(422, 57);
            this.btn_get.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_get.Name = "btn_get";
            this.btn_get.Size = new System.Drawing.Size(76, 47);
            this.btn_get.TabIndex = 2;
            this.btn_get.Text = "Lấy";
            this.btn_get.Click += new System.EventHandler(this.btn_get_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(505, 46);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(79, 17);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Dải hóa đơn:";
            // 
            // searchLookUpEdit1
            // 
            this.searchLookUpEdit1.EditValue = "[Chưa chọn dải hóa đơn]";
            this.searchLookUpEdit1.Location = new System.Drawing.Point(505, 68);
            this.searchLookUpEdit1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.searchLookUpEdit1.Name = "searchLookUpEdit1";
            this.searchLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpEdit1.Properties.NullText = "[Chưa chọn dải hóa đơn]";
            this.searchLookUpEdit1.Properties.PopupView = this.searchLookUpEdit1View;
            this.searchLookUpEdit1.Size = new System.Drawing.Size(125, 22);
            this.searchLookUpEdit1.TabIndex = 3;
            this.searchLookUpEdit1.EditValueChanged += new System.EventHandler(this.searchLookUpEdit1_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(156, 46);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(65, 17);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(31, 46);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(57, 17);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Từ ngày:";
            this.labelControl1.Click += new System.EventHandler(this.labelControl1_Click);
            // 
            // checkedListBoxControl1
            // 
            this.checkedListBoxControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkedListBoxControl1.Location = new System.Drawing.Point(0, 114);
            this.checkedListBoxControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkedListBoxControl1.Name = "checkedListBoxControl1";
            this.checkedListBoxControl1.Size = new System.Drawing.Size(885, 196);
            this.checkedListBoxControl1.TabIndex = 7;
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(31, 373);
            this.txtPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(167, 22);
            this.txtPath.TabIndex = 8;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(31, 350);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(73, 17);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "Vị trí lưu file:";
            // 
            // btn_Path
            // 
            this.btn_Path.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Path.ImageOptions.Image")));
            this.btn_Path.Location = new System.Drawing.Point(207, 361);
            this.btn_Path.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Path.Name = "btn_Path";
            this.btn_Path.Size = new System.Drawing.Size(85, 47);
            this.btn_Path.TabIndex = 9;
            this.btn_Path.Text = "Chọn";
            this.btn_Path.Click += new System.EventHandler(this.btn_Path_Click);
            // 
            // btn_download
            // 
            this.btn_download.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_download.ImageOptions.Image")));
            this.btn_download.Location = new System.Drawing.Point(766, 357);
            this.btn_download.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_download.Name = "btn_download";
            this.btn_download.Size = new System.Drawing.Size(114, 47);
            this.btn_download.TabIndex = 10;
            this.btn_download.Text = "Tải File XML";
            this.btn_download.Click += new System.EventHandler(this.btn_download_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // Btn_Download_PDF
            // 
            this.Btn_Download_PDF.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Download_PDF.ImageOptions.Image")));
            this.Btn_Download_PDF.Location = new System.Drawing.Point(637, 357);
            this.Btn_Download_PDF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Download_PDF.Name = "Btn_Download_PDF";
            this.Btn_Download_PDF.Size = new System.Drawing.Size(114, 47);
            this.Btn_Download_PDF.TabIndex = 11;
            this.Btn_Download_PDF.Text = "Tải File PDF";
            this.Btn_Download_PDF.Click += new System.EventHandler(this.Btn_Download_PDF_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(387, 376);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Thời gian:";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(459, 376);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(0, 17);
            this.lblTime.TabIndex = 13;
            // 
            // btn_stop
            // 
            this.btn_stop.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_stop.ImageOptions.Image")));
            this.btn_stop.Location = new System.Drawing.Point(298, 361);
            this.btn_stop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(83, 45);
            this.btn_stop.TabIndex = 14;
            this.btn_stop.Text = "Dừng";
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_ViewPDF
            // 
            this.btn_ViewPDF.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_ViewPDF.ImageOptions.Image")));
            this.btn_ViewPDF.Location = new System.Drawing.Point(555, 357);
            this.btn_ViewPDF.Name = "btn_ViewPDF";
            this.btn_ViewPDF.Size = new System.Drawing.Size(75, 47);
            this.btn_ViewPDF.TabIndex = 15;
            this.btn_ViewPDF.Text = "In";
            this.btn_ViewPDF.Click += new System.EventHandler(this.btn_ViewPDF_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(473, 336);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(0, 17);
            this.lblTotal.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(387, 336);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "Tổng số HĐ:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // lblDatai
            // 
            this.lblDatai.AutoSize = true;
            this.lblDatai.Location = new System.Drawing.Point(624, 336);
            this.lblDatai.Name = "lblDatai";
            this.lblDatai.Size = new System.Drawing.Size(0, 17);
            this.lblDatai.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(552, 336);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "Đã tải:";
            // 
            // FormGetXML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 411);
            this.Controls.Add(this.lblDatai);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_ViewPDF);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Btn_Download_PDF);
            this.Controls.Add(this.btn_download);
            this.Controls.Add(this.btn_Path);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.checkedListBoxControl1);
            this.Controls.Add(this.groupControl1);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("FormGetXML.IconOptions.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "FormGetXML";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tải XML";
            this.Load += new System.EventHandler(this.FormGetXML_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtkyhieu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtmauso.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DateEdit txtTuNgay;
        private DateEdit txtDenNgay;
        private GroupControl groupControl1;
        private LabelControl labelControl1;
        private SimpleButton btn_loaddata;
        private SimpleButton btn_get;
        private LabelControl labelControl3;
        private SearchLookUpEdit searchLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private LabelControl labelControl2;
        private CheckedListBoxControl checkedListBoxControl1;
        private TextEdit txtPath;
        private LabelControl labelControl4;
        private SimpleButton btn_Path;
        private SimpleButton btn_download;
        private CheckEdit chkAll;
        private TextEdit txtkyhieu;
        private TextEdit txtmauso;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private LabelControl labelControl6;
        private LabelControl labelControl5;
        private System.Windows.Forms.CheckBox check78;
        private System.Windows.Forms.CheckBox check32;
        private SimpleButton Btn_Download_PDF;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTime;
        private SimpleButton btn_stop;
        private SimpleButton btn_ViewPDF;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label lblDatai;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}