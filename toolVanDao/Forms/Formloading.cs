using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using toolVanDao.Services;

namespace toolVanDao.Forms
{
    public partial class Formloading : DevExpress.XtraEditors.XtraForm
    {
        public Formloading()
        {
            InitializeComponent();
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void Formloading_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private int _tick = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            _tick++;
            if (_tick == 3)
            {
                 MinvoiceService.GetDataFromMisaToMinvoiceTest();
               // Form1 frm = new Form1();
              //  frm.GetDataFromDateToDate();
                timer1.Stop();
                timer1.Dispose();
                Close();
            }
        }
    }
}