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
using toolVanDao.Config;
using Newtonsoft.Json.Linq;

namespace toolVanDao
{
   
    public partial class FormGetXML : DevExpress.XtraEditors.XtraForm
    {
        public string mst;
        public string tk;
        public string mk;
        public string id;
        public string token;

        public FormGetXML()
        {
            InitializeComponent();

        }
       
     
        
        public FormGetXML(string txtmst,string txtTK,string txtMK)
        {
            InitializeComponent();
            this.mst = txtmst;
            this.tk = txtTK;
            this.mk = txtMK;


        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void btn_get_Click(object sender, EventArgs e)
        {
            try
            {


                if (check32.Checked == true && check78.Checked == false)
                {
                    // lấy thông tin thông báo phát hành
                    LoadMauHoaDon(mst);
                }
                if (check78.Checked == true && check32.Checked == false)
                {
                    // lấy thông tin thông báo phát hành
                    LoadMauHoaDon(mst);
                }
                else if (check78.Checked == false && check32.Checked == false)
                {
                    XtraMessageBox.Show("chưa tick chọn loại hóa đơn (HĐ32, HĐ78)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString(), "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void FormGetXML_Load(object sender, EventArgs e)
        {
            if (check32.Checked == true && check78.Checked == false)
            {
                txtmauso.Text = Properties.Settings.Default.mau_so.ToString();
                txtkyhieu.Text = Properties.Settings.Default.ky_hieu.ToString();
                txtPath.Text = Properties.Settings.Default.Folder.ToString();
            }
            if (check78.Checked == true && check32.Checked == false)
            {
               
                txtkyhieu.Text = Properties.Settings.Default.ky_hieu.ToString();
                txtPath.Text = Properties.Settings.Default.Folder.ToString();
            }
          
        }


        private void LoadMauHoaDon(string mst)
        {
            //TT32
            if (check32.Checked == true && check78.Checked == false)
            {
                try
                {

                    List<MauHoaDon> mauHoaDons = CommonService.GetInfoInvoice(mst);
                    searchLookUpEdit1.Properties.DataSource = mauHoaDons;
                    searchLookUpEdit1.Properties.DisplayMember = "mau_so";
                    searchLookUpEdit1.Properties.DisplayMember = "ky_hieu";
                    searchLookUpEdit1.Properties.DisplayMember = "ngay_bd_sd";
                    searchLookUpEdit1.Properties.ValueMember = "id";
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //TT78
            if (check78.Checked == true && check32.Checked == false)
            {
                try
                {

                    List<MauHoaDon78> mauHoaDons78 = CommonService.GetInforhoadon78(mst);
                    searchLookUpEdit1.Properties.DataSource = mauHoaDons78;
                    searchLookUpEdit1.Properties.DisplayMember = "ky_hieu";
                    searchLookUpEdit1.Properties.DisplayMember = "ngay_bd_sd";
                    searchLookUpEdit1.Properties.ValueMember = "id";
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
           
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (check32.Checked == true && check78.Checked == false)
            {
                var mauHoaDon = searchLookUpEdit1View.GetFocusedRow() as MauHoaDon;
                if (mauHoaDon != null)
                {
                    txtkyhieu.Text = mauHoaDon.ky_hieu;
                    txtmauso.Text = mauHoaDon.mau_so;
                    CommonService.UpdateSettingAppConfig(CommonConstants.mau_so, mauHoaDon.mau_so);
                    CommonService.UpdateSettingAppConfig(CommonConstants.ky_hieu, mauHoaDon.ky_hieu);
                    CommonService.UpdateSettingAppConfig(CommonConstants.InvoiceCodeId, mauHoaDon.id);
                }
            }
            if (check78.Checked == true && check32.Checked == false)
            {
                var mauHoaDon = searchLookUpEdit1View.GetFocusedRow() as MauHoaDon78;
                if (mauHoaDon != null)
                {
                    txtkyhieu.Text = mauHoaDon.ky_hieu;
                    CommonService.UpdateSettingAppConfig(CommonConstants.ky_hieu, mauHoaDon.ky_hieu);
                    CommonService.UpdateSettingAppConfig(CommonConstants.hoadon68_id, mauHoaDon.id);
                }
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

        private void btn_loaddata_Click(object sender, EventArgs e)
        {
            try
            {
                notifyIcon1.Icon = SystemIcons.Exclamation;
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(5000, "Thông Báo", "Đang tải Dữ liệu!Vui lòng chờ!", ToolTipIcon.Info);
                var webClient = MinvoiceService.SetupWebClient();
                var url = Properties.Settings.Default.UrlSBM; //k dùng baseconfig vì k load kịp change from settings
                                                              //  var command = BaseConfig.Command;
                var tuNgay = !string.IsNullOrEmpty(txtTuNgay.Text) ? txtTuNgay.DateTime.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");
                var denNgay = !string.IsNullOrEmpty(txtDenNgay.Text) ? txtDenNgay.DateTime.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");


                //TT32
                if (check32.Checked == true && check78.Checked == false)
                {

                    List<InvoiceObject> invoiceObjects = new List<InvoiceObject>();
                    var json = "{\"tu_ngay\":\"" + tuNgay + "\" , \"den_ngay\":\"" + denNgay + "\",\"mau_so\":\"" + txtmauso.Text + "\", \"ky_hieu\":\"" + txtkyhieu.Text + "\"}";
                    try
                    {
                        var rs = webClient.UploadString(url, json);
                        var result = JObject.Parse(rs);
                        if (result["error"] == null)
                        {
                            var resultarr = JArray.Parse(result["data"].ToString());
                            if (resultarr.Count > 0)
                            {
                                foreach (var jToken in resultarr)
                                {
                                    InvoiceObject invoiceObject = new InvoiceObject
                                    {
                                        InvoiceNumber = jToken["inv_invoiceNumber"].ToString(),
                                        KyHieu = jToken["inv_invoiceSeries"].ToString(),
                                        MauSo = jToken["mau_hd"].ToString(),
                                        Selected = false,
                                        ngay_hd = Convert.ToDateTime(jToken["inv_invoiceIssuedDate"]),
                                        SBM = jToken["sobaomat"].ToString(),
                                        InvInvoiceAuthId = jToken["inv_InvoiceAuth_id"].ToString()

                                    };
                                    invoiceObject.Value = $"{invoiceObject.MauSo} - {invoiceObject.KyHieu.Trim()} - {invoiceObject.InvoiceNumber} - {invoiceObject.ngay_hd} - {invoiceObject.SBM}";
                                    string trang_thai = jToken["trang_thai"].ToString();
                                    // Check có số bảo mật và phải là hóa đơn đã ký
                                    if (!string.IsNullOrWhiteSpace(invoiceObject.SBM.ToString()) && trang_thai == "Đã ký")
                                    {
                                        invoiceObjects.Add(invoiceObject);
                                    }

                                }
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show(result["error"].ToString(), "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        invoiceObjects.Sort((x, y) => x.InvoiceNumber.CompareTo(y.InvoiceNumber));

                        checkedListBoxControl1.DataSource = invoiceObjects;
                        checkedListBoxControl1.DisplayMember = "Value";
                        checkedListBoxControl1.ValueMember = "SBM";
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                //TT78
                if (check78.Checked == true && check32.Checked == false)
                {
                    List<InvoiceObject> invoiceObjects78 = new List<InvoiceObject>();
                    var json = "{\"tuNgay\":\"" + tuNgay + "\" , \"denNgay\":\"" + denNgay + "\", \"khieu\":\"" + txtkyhieu.Text + "\"}";

                    var rs = webClient.UploadString(url, json);
                    var result = JObject.Parse(rs);
                    if (result["error"] == null)
                    {
                        var resultarr = JArray.Parse(result["data"].ToString());
                        if (resultarr.Count > 0)
                        {
                            foreach (var jToken in resultarr)
                            {
                                InvoiceObject invoiceObject78 = new InvoiceObject
                                {
                                    shdon = jToken["shdon"].ToString(),
                                    KyHieu = jToken["khieu"].ToString(),
                                    Selected = false,
                                    ngay_hd = Convert.ToDateTime(jToken["tdlap"]),
                                    SBM = jToken["sbmat"].ToString(),
                                    hoadon68id = jToken["hoadon68_id"].ToString()

                                };
                                invoiceObject78.Value = $" {invoiceObject78.KyHieu.Trim()}  - {invoiceObject78.shdon} - {invoiceObject78.ngay_hd} - {invoiceObject78.SBM}";
                                string trang_thai = jToken["tthai"].ToString();
                                // Check có số bảo mật và phải là hóa đơn đã ký
                                if (!string.IsNullOrWhiteSpace(invoiceObject78.SBM.ToString()) && trang_thai == "Đã ký" || trang_thai == "Đã gửi")
                                {
                                    invoiceObjects78.Add(invoiceObject78);
                                }

                            }
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show(result["error"].ToString(), "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    invoiceObjects78.Sort((x, y) => int.Parse(x.shdon).CompareTo(int.Parse(y.shdon)));

                    checkedListBoxControl1.DataSource = invoiceObjects78;
                    checkedListBoxControl1.DisplayMember = "Value";
                    checkedListBoxControl1.ValueMember = "SBM";


                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString(), "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Btn_Download_PDF_Click(object sender, EventArgs e) //TẢI PDF HÀNG LOẠT
        {
            try
            {


                //kiểm tra đường dẫn file
                if (!string.IsNullOrWhiteSpace(txtPath.Text))
                {
                    //TT32
                    if (check32.Checked == true && check78.Checked == false)
                    {
                        string invoiceNumber = "";
                        List<string> sobaomat = new List<string>();
                        List<InvoiceObject> invoiceObjects = new List<InvoiceObject>();
                        List<string> listid = new List<string>();
                        foreach (var selectedItem in checkedListBoxControl1.CheckedItems)
                        {
                            invoiceNumber += $"'{(selectedItem as InvoiceObject)?.InvoiceNumber}' ,";
                            listid.Add((selectedItem as InvoiceObject)?.hoadon68id);
                            sobaomat.Add((selectedItem as InvoiceObject)?.SBM);
                            var invoiceObject = selectedItem as InvoiceObject;
                            invoiceObjects.Add(invoiceObject);

                        }

                        if (!string.IsNullOrEmpty(invoiceNumber))
                        {
                            invoiceNumber = invoiceNumber.Substring(0, invoiceNumber.Length - 1);

                            //MinvoiceService.UpdateInvoice(invoiceNumber);
                            //notifyIcon1.Icon = SystemIcons.Application;
                            notifyIcon1.Icon = SystemIcons.Exclamation;
                            notifyIcon1.Visible = true;
                            notifyIcon1.ShowBalloonTip(5000, "Thông Báo", "Đang tải XML hóa đơn!Vui lòng chờ!", ToolTipIcon.Info);

                            MinvoiceService.DownloadPDF(txtPath.Text, listid, invoiceObjects);
                            if (MinvoiceService.ck == false)
                            {
                                XtraMessageBox.Show("Tải File hoàn tất", "Thông Báo!", MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                            }

                        }
                        else
                        {
                            XtraMessageBox.Show("Chưa chọn hóa đơn muốn tải FILE XML", "Thông Báo", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                    //TT78
                    if (check78.Checked == true && check32.Checked == false)
                    {
                        string shdon78 = "";

                        List<string> sobaomat = new List<string>();
                        List<InvoiceObject> invoiceObjects78 = new List<InvoiceObject>();
                        List<string> listid = new List<string>();
                        foreach (var selectedItem in checkedListBoxControl1.CheckedItems)
                        {

                            shdon78 += $"'{(selectedItem as InvoiceObject)?.shdon}' ,";

                            listid.Add((selectedItem as InvoiceObject)?.hoadon68id);
                            sobaomat.Add((selectedItem as InvoiceObject)?.SBM);
                            var invoiceObject = selectedItem as InvoiceObject;
                            invoiceObjects78.Add(invoiceObject);

                        }

                        if (!string.IsNullOrEmpty(shdon78))
                        {
                            shdon78 = shdon78.Substring(0, shdon78.Length - 1);

                            //MinvoiceService.UpdateInvoice(invoiceNumber);
                            //notifyIcon1.Icon = SystemIcons.Application;
                            notifyIcon1.Icon = SystemIcons.Exclamation;
                            notifyIcon1.Visible = true;
                            notifyIcon1.ShowBalloonTip(5000, "Thông Báo", "Đang tải PDF hóa đơn!Vui lòng chờ!", ToolTipIcon.Info);

                            MinvoiceService.Downloadpdf78(txtPath.Text, listid, invoiceObjects78);

                            if (MinvoiceService.ck == false)
                            {
                                XtraMessageBox.Show("Tải File PDF hoàn tất", "Thông Báo!", MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                            }

                        }
                        else
                        {
                            XtraMessageBox.Show("Chưa chọn hóa đơn muốn tải FILE PDF", "Thông Báo", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }

                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa chọn vị trí lưu file!", "Thông Báo", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString(), "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_download_Click(object sender, EventArgs e) //TẢI XML HÀNG LOẠT
        {
            try
            {


                //kiểm tra đường dẫn file
                if (!string.IsNullOrWhiteSpace(txtPath.Text))
                {
                    //TT32
                    if (check32.Checked == true && check78.Checked == false)
                    {
                        string invoiceNumber = "";
                        List<string> sobaomat = new List<string>();
                        List<InvoiceObject> invoiceObjects = new List<InvoiceObject>();
                        List<string> listid = new List<string>();
                        foreach (var selectedItem in checkedListBoxControl1.CheckedItems)
                        {

                            invoiceNumber += $"'{(selectedItem as InvoiceObject)?.InvoiceNumber}' ,";
                            listid.Add((selectedItem as InvoiceObject)?.hoadon68id);
                            sobaomat.Add((selectedItem as InvoiceObject)?.SBM);
                            var invoiceObject = selectedItem as InvoiceObject;
                            invoiceObjects.Add(invoiceObject);

                        }

                        if (!string.IsNullOrEmpty(invoiceNumber))
                        {
                            invoiceNumber = invoiceNumber.Substring(0, invoiceNumber.Length - 1);

                            //MinvoiceService.UpdateInvoice(invoiceNumber);
                            //notifyIcon1.Icon = SystemIcons.Application;
                            notifyIcon1.Icon = SystemIcons.Exclamation;
                            notifyIcon1.Visible = true;
                            notifyIcon1.ShowBalloonTip(5000, "Thông Báo", "Đang tải XML hóa đơn!Vui lòng chờ!", ToolTipIcon.Info);
                            MinvoiceService.DownloadXML(sobaomat, txtPath.Text);
                            if (MinvoiceService.ck == false)
                            {
                                XtraMessageBox.Show("Tải File hoàn tất", "Thông Báo!", MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                            }

                        }
                        else
                        {
                            XtraMessageBox.Show("Chưa chọn hóa đơn muốn tải FILE XML", "Thông Báo", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                    //TT78
                    if (check78.Checked == true && check32.Checked == false)
                    {
                        string shdon78 = "";

                        List<string> sobaomat = new List<string>();
                        List<InvoiceObject> invoiceObjects78 = new List<InvoiceObject>();
                        List<string> listid = new List<string>();
                        foreach (var selectedItem in checkedListBoxControl1.CheckedItems)
                        {

                            shdon78 += $"'{(selectedItem as InvoiceObject)?.shdon}' ,";

                            listid.Add((selectedItem as InvoiceObject)?.hoadon68id);
                            sobaomat.Add((selectedItem as InvoiceObject)?.SBM);
                            var invoiceObject = selectedItem as InvoiceObject;
                            invoiceObjects78.Add(invoiceObject);

                        }

                        if (!string.IsNullOrEmpty(shdon78))
                        {
                            shdon78 = shdon78.Substring(0, shdon78.Length - 1);

                            //MinvoiceService.UpdateInvoice(invoiceNumber);
                            //notifyIcon1.Icon = SystemIcons.Application;
                            notifyIcon1.Icon = SystemIcons.Exclamation;
                            notifyIcon1.Visible = true;
                            notifyIcon1.ShowBalloonTip(5000, "Thông Báo", "Đang tải XML hóa đơn!Vui lòng chờ!", ToolTipIcon.Info);
                            MinvoiceService.DownloadXML78(txtPath.Text, listid, invoiceObjects78);
                            if (MinvoiceService.ck == false)
                            {
                                XtraMessageBox.Show("Tải File hoàn tất", "Thông Báo!", MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                            }

                        }
                        else
                        {
                            XtraMessageBox.Show("Chưa chọn hóa đơn muốn tải FILE XML", "Thông Báo", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }

                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa chọn vị trí lưu file!", "Thông Báo", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString(), "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Path_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            //folderBrowserDialog1.in = @"C:\";
            //folderBrowserDialog1.Title = "Save text Files";
            //folderBrowserDialog1.CheckFileExists = true;
            //folderBrowserDialog1.CheckPathExists = true;
            //folderBrowserDialog1.DefaultExt = "txt";
            //folderBrowserDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            //folderBrowserDialog1.FilterIndex = 2;
            //folderBrowserDialog1.RestoreDirectory = true;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = folderBrowserDialog1.SelectedPath;
                CommonService.UpdateSettingAppConfig(CommonConstants.Folder, txtPath.Text);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void check32_CheckedChanged(object sender, EventArgs e)
        {
            if (check32.Checked == true)
            {
                check78.Checked = false;
            }
            
            if (check32.Checked == true && check78.Checked==false)
            {
                CommonService.UpdateSettingAppConfig(CommonConstants.UsernameLoginWeb, tk);
                CommonService.UpdateSettingAppConfig(CommonConstants.PasswordLoginWeb, mk);
                CommonService.UpdateSettingAppConfig(CommonConstants.MST, mst);
                string UrlRef = $@"https://{mst}.minvoice.com.vn/api/System/GetDataReferencesByRefId?refId=RF00187";
                string UrlLogin = $@"https://{mst}.minvoice.com.vn/api/Account/Login";
                string UrlSBM = $@"https://{mst}.minvoice.com.vn/api/InvoiceAPISSE/GetInvoiceFromDateToDate";
                string UrlDownloadXML = "https://tracuuhoadon.minvoice.com.vn/Tracuu2/ExportZipFileXML";
                string UrlDownloadPDF = $@"https://{mst}.minvoice.com.vn/api/Invoice/Preview?id=";
                CommonService.UpdateSettingAppConfig(CommonConstants.UrlDownloadXML, UrlDownloadXML);
                CommonService.UpdateSettingAppConfig(CommonConstants.UrlDownloadPDF, UrlDownloadPDF);
                CommonService.UpdateSettingAppConfig(CommonConstants.UrlRef, UrlRef);
                CommonService.UpdateSettingAppConfig(CommonConstants.UrlSBM, UrlSBM);
                CommonService.UpdateSettingAppConfig(CommonConstants.UrlLogin, UrlLogin);
                //XtraMessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

               /// this.Hide();

               // FormGetXML frm = new FormGetXML(mst);

               // frm.ShowDialog();
               // this.Close();
            }

        }

        private void check78_CheckedChanged(object sender, EventArgs e)
        {
            if (check78.Checked == true)
            {
                check32.Checked = false;
            }

            if (check78.Checked==true && check32.Checked == false)
            {
                CommonService.UpdateSettingAppConfig(CommonConstants.UsernameLoginWeb, tk);
                CommonService.UpdateSettingAppConfig(CommonConstants.PasswordLoginWeb, mk);
                CommonService.UpdateSettingAppConfig(CommonConstants.MST, mst);
                string UrlRef78 = $@"https://{mst}.minvoice.com.vn/api/Invoice68/GetTypeInvoiceSeries";//ký hiệu hóa đơn
          
                string UrlLogin = $@"https://{mst}.minvoice.com.vn/api/Account/Login";//login 
                string UrlSBM = $@"https://{mst}.minvoice.com.vn/api/InvoiceApi78/GetInvoices";//lấy thông tin hd
           
                string UrlDownloadXml78 = $@"https://{mst}.minvoice.com.vn/api/InvoiceApi78/ExportXml?id=";//lấy xml thông qua id hd
                string UrlDownloadpdf78 = $@"https://{mst}.minvoice.com.vn/api/InvoiceApi78/PrintInvoice?id=";//lấy xml thông qua id hd
                //string UrlDownloadXml78mcqt = $@"https://{mst}.minvoice.com.vn/api/Pattern/ViewsLogTD?mtdiep=V010602649528A262CD37EF48E68563BABFCD1C7085";//lấy xml thông qua id thông điệp

                CommonService.UpdateSettingAppConfig(CommonConstants.UrlDownloadXml78, UrlDownloadXml78);
                CommonService.UpdateSettingAppConfig(CommonConstants.UrlRef78, UrlRef78);
                CommonService.UpdateSettingAppConfig(CommonConstants.UrlSBM, UrlSBM);
                CommonService.UpdateSettingAppConfig(CommonConstants.UrlLogin, UrlLogin);
                CommonService.UpdateSettingAppConfig(CommonConstants.UrlDownloadpdf78, UrlDownloadpdf78);
                ////XtraMessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ///this.Hide();

                //FormGetXML frm = new FormGetXML(mst);

                //frm.ShowDialog();
                //this.Close();
            }
        }

       
    }
}