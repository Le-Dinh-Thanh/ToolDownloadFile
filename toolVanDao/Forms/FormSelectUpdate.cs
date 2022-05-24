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
using toolVanDao.Data;
using toolVanDao.Services;

namespace toolVanDao.Forms
{
    public partial class FormSelectUpdate : DevExpress.XtraEditors.XtraForm
    {
        public FormSelectUpdate()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            notifyIcon1.ShowBalloonTip(5000, "Thông Báo", "Đang cập nhật hóa đơn!Vui lòng chờ!", ToolTipIcon.Info);
            string invoiceNumber = "";
            List<InvoiceObject> invoiceObjects = new List<InvoiceObject>();

            foreach (var selectedItem in checkedListBoxControl1.CheckedItems)
            {
                invoiceNumber += $"'{(selectedItem as InvoiceObject)?.InvoiceNumber}' ,";

                var invoiceObject = selectedItem as InvoiceObject;
                invoiceObjects.Add(invoiceObject);

            }

            if (!string.IsNullOrEmpty(invoiceNumber))
            {
                invoiceNumber = invoiceNumber.Substring(0, invoiceNumber.Length - 1);
                //MinvoiceService.UpdateInvoice(invoiceNumber);
                MinvoiceService.UpdateInvoice(invoiceObjects);
            }
            else
            {
                XtraMessageBox.Show("Chưa chọn hóa đơn muốn cập nhật", "Thông Báo", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            var checkEdit = sender as CheckEdit;
            if (checkEdit != null && checkEdit.Checked)
            {
                checkedListBoxControl1.CheckAll();
            }
            else
            {
                checkedListBoxControl1.UnCheckAll();
            }
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            var tuNgay = !string.IsNullOrEmpty(dteTuNgay.Text) ? dteTuNgay.DateTime.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");
            var denNgay = !string.IsNullOrEmpty(dteDenNgay.Text) ? dteDenNgay.DateTime.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");
            checkedListBoxControl1.DataSource = CommonService.GetInvoiceObjects(tuNgay, denNgay);
            checkedListBoxControl1.DisplayMember = "Value";
            checkedListBoxControl1.ValueMember = "InvoiceNumber";
        }
    }
}