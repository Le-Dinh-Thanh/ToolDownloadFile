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
using System.Net;
using Newtonsoft.Json.Linq;
using toolVanDao.Services;
using toolVanDao.Config;

namespace toolVanDao
{
    public partial class FormLogin : DevExpress.XtraEditors.XtraForm
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btn_DN_KeyDown(object sender, KeyEventArgs e)
        {
           
        }
       
        private void btn_DN_Click(object sender, EventArgs e)
        {
          
            if (!string.IsNullOrWhiteSpace(txtTK.Text) && !string.IsNullOrWhiteSpace(txtMK.Text) && !string.IsNullOrWhiteSpace(txtMST.Text))
            {
               
              var ketqua = xulydangnhap(txtTK.Text, txtMK.Text, txtMST.Text);
                if (ketqua !=null)
                {
                   var token = JObject.Parse(ketqua);

                    var loi = token["error"] ?? "";
                    
                    if (string.IsNullOrWhiteSpace(loi.ToString()))
                    {
                       
                            //CommonService.UpdateSettingAppConfig(CommonConstants.UsernameLoginWeb, txtTK.Text);
                            //CommonService.UpdateSettingAppConfig(CommonConstants.PasswordLoginWeb, txtMK.Text);
                            //CommonService.UpdateSettingAppConfig(CommonConstants.MST, txtMST.Text);
                            //string UrlRef = $@"https://{txtMST.Text}.minvoice.com.vn/api/System/GetDataReferencesByRefId?refId=RF00187";
                            //string UrlLogin = $@"https://{txtMST.Text}.minvoice.com.vn/api/Account/Login";
                            //string UrlSBM = $@"https://{txtMST.Text}.minvoice.com.vn/api/InvoiceAPISSE/GetInvoiceFromDateToDate";
                            //string UrlDownloadXML = "https://tracuuhoadon.minvoice.com.vn/Tracuu2/ExportZipFileXML";
                            //CommonService.UpdateSettingAppConfig(CommonConstants.UrlDownloadXML, UrlDownloadXML);
                            //CommonService.UpdateSettingAppConfig(CommonConstants.UrlRef, UrlRef);
                            //CommonService.UpdateSettingAppConfig(CommonConstants.UrlSBM, UrlSBM);
                            //CommonService.UpdateSettingAppConfig(CommonConstants.UrlLogin, UrlLogin);
                            XtraMessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.Hide();

                            FormGetXML frm = new FormGetXML(txtMST.Text, txtTK.Text, txtMK.Text);

                            frm.ShowDialog();
                            this.Show();
                        
                        
                           
                        
                       
                    }
                    else
                    {
                        XtraMessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Vui lòng nhập đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string xulydangnhap(string tk, string mk, string mst)
        {
            var ketqua ="";
            try
            {
                string link = $@"https://{mst}.minvoice.com.vn/api/Account/Login";
                var webClient = new WebClient
                {
                    Encoding = Encoding.UTF8
                };
                webClient.Headers.Add("Content-Type", "application/json; charset=utf-8");
                JObject json = new JObject
                {
                {"username",tk },
                {"password",mk },
                {"ma_dvcs","VP" }
                };
                 ketqua = webClient.UploadString(link, json.ToString());
                return ketqua;
            }
            catch(Exception e)
            {
                XtraMessageBox.Show(e.ToString(), "Lỗi mã số thuế không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
           

        }


        private void txtMK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                this.Close();
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(txtTK.Text) && !string.IsNullOrWhiteSpace(txtMK.Text) && !string.IsNullOrWhiteSpace(txtMST.Text))
                {
                    var ketqua = xulydangnhap(txtTK.Text, txtMK.Text, txtMST.Text);
                    if (ketqua != null)
                    {
                        var token = JObject.Parse(ketqua);
                        var loi = token["error"] ?? "";

                        if (string.IsNullOrWhiteSpace(loi.ToString()))
                        {

                            //CommonService.UpdateSettingAppConfig(CommonConstants.UsernameLoginWeb, txtTK.Text);
                            //CommonService.UpdateSettingAppConfig(CommonConstants.PasswordLoginWeb, txtMK.Text);
                            //CommonService.UpdateSettingAppConfig(CommonConstants.MST, txtMST.Text);
                            //string UrlRef = $@"https://{txtMST.Text}.minvoice.com.vn/api/System/GetDataReferencesByRefId?refId=RF00187";
                            //string UrlLogin = $@"https://{txtMST.Text}.minvoice.com.vn/api/Account/Login";
                            //string UrlSBM = $@"https://{txtMST.Text}.minvoice.com.vn/api/InvoiceAPISSE/GetInvoiceFromDateToDate";
                            //string UrlDownloadXML = "https://tracuuhoadon.minvoice.com.vn/Tracuu2/ExportZipFileXML";
                            //CommonService.UpdateSettingAppConfig(CommonConstants.UrlDownloadXML, UrlDownloadXML);
                            //CommonService.UpdateSettingAppConfig(CommonConstants.UrlRef, UrlRef);
                            //CommonService.UpdateSettingAppConfig(CommonConstants.UrlSBM, UrlSBM);
                            //CommonService.UpdateSettingAppConfig(CommonConstants.UrlLogin, UrlLogin);
                          XtraMessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.Hide();
                            FormGetXML frm = new FormGetXML(txtMST.Text, txtTK.Text, txtMK.Text);
                            frm.ShowDialog();
                            this.Show();

                        }
                        else
                        {
                            XtraMessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show("Vui lòng nhập đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FormLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void FormLogin_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtMK_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void FormLogin_Load(object sender, EventArgs e)
        {
            
        }

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}