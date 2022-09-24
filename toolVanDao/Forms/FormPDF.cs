using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace toolVanDao.Forms
{
    public partial class FormPDF : Form
    {
        public FileStream fileStream;
        public FormPDF()
        {
            InitializeComponent();
        }
        public FormPDF(string pathFile)
        {
            InitializeComponent();
            fileStream = new FileStream(pathFile, FileMode.Open);
            pdfViewer2.LoadDocument(fileStream);
            pdfViewer2.DetachStreamAfterLoadComplete = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Application.OpenForms[0].Activated += new EventHandler(Form2_Activated);
        }

        void Form2_Activated(object sender, EventArgs e)
        {
            Console.WriteLine("Activated!");
        }

        private void FormPDF_Load(object sender, EventArgs e)
        {
            InitializeComponent();
            // pdfViewer2.LoadDocument(fileStream);

        }
    }
}
