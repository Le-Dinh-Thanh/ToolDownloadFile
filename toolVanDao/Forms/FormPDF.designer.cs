
namespace toolVanDao.Forms
{
    partial class FormPDF
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
            pdfViewer2.CloseDocument();
            pdfViewer1.Dispose();
            pdfViewer2.Dispose();
            fileStream.Dispose();
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
            this.pdfViewer1 = new DevExpress.XtraPdfViewer.PdfViewer();
            this.pdfViewer2 = new DevExpress.XtraPdfViewer.PdfViewer();
            this.SuspendLayout();
            // 
            // pdfViewer1
            // 
            this.pdfViewer1.Location = new System.Drawing.Point(0, 0);
            this.pdfViewer1.Name = "pdfViewer1";
            this.pdfViewer1.Size = new System.Drawing.Size(0, 0);
            this.pdfViewer1.TabIndex = 0;
            // 
            // pdfViewer2
            // 
            this.pdfViewer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pdfViewer2.Location = new System.Drawing.Point(-1, -1);
            this.pdfViewer2.Name = "pdfViewer2";
            this.pdfViewer2.Size = new System.Drawing.Size(1174, 473);
            this.pdfViewer2.TabIndex = 1;
            // 
            // FormPDF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1173, 472);
            this.Controls.Add(this.pdfViewer2);
            this.Controls.Add(this.pdfViewer1);
            this.Name = "FormPDF";
            this.Text = "FormPDF";
            this.Load += new System.EventHandler(this.FormPDF_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraPdfViewer.PdfViewer pdfViewer1;
        private DevExpress.XtraPdfViewer.PdfViewer pdfViewer2;
    }
}