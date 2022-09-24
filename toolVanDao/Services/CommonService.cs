using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using toolVanDao.Data;
using toolVanDao.Config;
using Newtonsoft.Json.Linq;


namespace toolVanDao.Services
{
    public class CommonService
    {
        public static void UpdateSettingAppConfig(string key, string value)
        {
            switch (key)
            {
                
                case "mau_so":
                    Properties.Settings.Default.mau_so = value; break;
                case "ky_hieu":
                    Properties.Settings.Default.ky_hieu = value; break;
                case "InvoiceCodeId":
                    Properties.Settings.Default.InvoiceCodeId = value; break;
                case "UsernameLoginWeb":
                    Properties.Settings.Default.UsernameLoginWeb = value; break;
                case "PasswordLoginWeb":
                    Properties.Settings.Default.PasswordLoginWeb = value; break;
                case "MST":
                    Properties.Settings.Default.MST = value; break;
                case "UrlSBM":
                    Properties.Settings.Default.UrlSBM = value; break;
                case "UrlRef":
                    Properties.Settings.Default.UrlRef = value; break;
                case "UrlLogin":
                    Properties.Settings.Default.UrlLogin = value; break;
                case "Folder":
                    Properties.Settings.Default.Folder = value; break;
                case "UrlDownloadXML":
                    Properties.Settings.Default.UrlDownloadXML = value; break;
                case "UrlDownloadPDF":
                    Properties.Settings.Default.UrlDownloadPDF = value; break;
                ///78
                case "UrlRef78":
                    Properties.Settings.Default.UrlRef78 = value; break;
                case "UrlDownloadXml78":
                    Properties.Settings.Default.UrlDownloadXml78 = value; break;
                
                case "UrlDownloadpdf78":
                    Properties.Settings.Default.UrlDownloadpdf78 = value; break;
            }

            Properties.Settings.Default.Save();
        }

        public static List<MauHoaDon> GetInfoInvoice(string mst)
        {
            List<MauHoaDon> mauHoaDons = new List<MauHoaDon>();
            var webClient = MinvoiceService.SetupWebClient();
            var url = Properties.Settings.Default.UrlRef;

            try
            {
                var result = webClient.DownloadString(url);
                if (!string.IsNullOrEmpty(result))
                {
                    JArray jArray = JArray.Parse(result);
                    if (jArray.Count > 0)
                    {
                        foreach (var jToken in jArray)
                        {
                            MauHoaDon mauHoaDon = new MauHoaDon
                            {
                                mau_so = jToken["mau_so"].ToString(),
                                ky_hieu = jToken["ky_hieu"].ToString(), 
                                ngay_bd_sd = Convert.ToDateTime(jToken["ngay_bd_sd"]),
                                id = jToken["id"].ToString()
                              
                            };
                            mauHoaDons.Add(mauHoaDon);
                        }
                    }
                }

                return mauHoaDons;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static List<MauHoaDon78> GetInforhoadon78(string mst)
        {

            List<MauHoaDon78> mauHoaDons78 = new List<MauHoaDon78>();
            var webClient = MinvoiceService.SetupWebClient();
            var url = Properties.Settings.Default.UrlRef78;

            try
            {
                var result = webClient.DownloadString(url);
                if (!string.IsNullOrEmpty(result))
                {
                    JObject JObject = JObject.Parse(result);
                    var Data = JObject["data"].ToString();
                    JArray jArray = JArray.Parse(Data);
                    if (jArray.Count > 0)
                    {
                        foreach (var jToken in jArray)
                        {
                            MauHoaDon78 mauHoaDon78 = new MauHoaDon78
                            {
                                ky_hieu = jToken["khhdon"].ToString(),
                                ngay_bd_sd = Convert.ToDateTime(string.IsNullOrEmpty(jToken["date_new"].ToString()) ? DateTime.Now : jToken["date_new"]),
                                id = jToken["id"].ToString()

                            };
                            mauHoaDons78.Add(mauHoaDon78);
                        }
                    }
                }

                return mauHoaDons78;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static List<InvoiceObject> GetInvoiceObjects(string tuNgay, string denNgay)
        {
            var webClient = MinvoiceService.SetupWebClient();
            var url = BaseConfig.UrlCommand;
            var command = BaseConfig.Command;
            var json = "{\"command\":\"" + command + "\" , parameter:{\"tu_ngay\":\"" + tuNgay + "\",\"den_ngay\":\"" + denNgay + "\"}}";

            List<InvoiceObject> invoiceObjects = new List<InvoiceObject>();
            try
            {
                var rs = webClient.UploadString(url, json);
                var result = JArray.Parse(rs);
                if (result.Count > 0)
                {
                    foreach (var jToken in result)
                    {
                        InvoiceObject invoiceObject = new InvoiceObject
                        {
                            InvoiceNumber = jToken["inv_invoiceNumber"].ToString(),
                            KyHieu = jToken["inv_invoiceSeries"].ToString(),
                            MauSo = jToken["mau_hd"].ToString(),
                            Selected = false,
                            InvInvoiceAuthId = jToken["inv_InvoiceAuth_id"].ToString()
                        };
                        invoiceObject.Value =
                            $"{invoiceObject.MauSo} - {invoiceObject.KyHieu} - {invoiceObject.InvoiceNumber} - {invoiceObject.InvInvoiceAuthId} ";
                        invoiceObjects.Add(invoiceObject);
                    }
                }

                return invoiceObjects;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static List<InvoiceObject> GethoadonObjects78(string tuNgay, string denNgay)
        {
            var webClient = MinvoiceService.SetupWebClient();
            var url = BaseConfig.UrlCommand;
            var command = BaseConfig.Command;
            var json = "{\"command\":\"" + command + "\" , parameter:{\"tu_ngay\":\"" + tuNgay + "\",\"den_ngay\":\"" + denNgay + "\"}}";

            List<InvoiceObject> invoiceObjects = new List<InvoiceObject>();
            try
            {
                var rs = webClient.UploadString(url, json);
                var result = JArray.Parse(rs);
                if (result.Count > 0)
                {
                    foreach (var jToken in result)
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
                        invoiceObjects.Add(invoiceObject78);
                    }
                    
                }

                return invoiceObjects;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static string ConvertNumber2(string value)
        {
            string[] a = new string[] { };
            if (value.Contains("."))
            {
                a = value.Split('.');
            }

            if (value.Contains(","))
            {
                a = value.Split(',');
            }

            if (a.Length > 1)
            {
                var phanThapPhan = GetMinOfNumber(int.Parse(a[1]));
                var phanNguyen = a[0];

                var result = phanThapPhan > 0 ? $"{phanNguyen}.{phanThapPhan}" : phanNguyen;
                return result;
            }

            return value;
        }

        private static int GetMinOfNumber(int number)
        {
            if (number <= 0) return 0;
            while (number % 10 == 0)
            {
                number = number / 10;

            }

            return number;
        }
    }
}
