using DevExpress.XtraEditors;
using Newtonsoft.Json.Linq;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using System.Xml.Linq;
using toolVanDao.Config;
using toolVanDao.Data;
using toolVanDao.Forms;

namespace toolVanDao.Services
{
    public class MinvoiceService
    {
        public static bool ck = false;
        public static CancellationTokenSource tokenSource = null;

        // Define delegate
        public delegate void PassControl(object sender);

        // Create instance (null)
        public static PassControl passControl;


        public static WebClient SetupWebClient()
        {
            var userName = Properties.Settings.Default.UsernameLoginWeb;
            var passWord = Properties.Settings.Default.PasswordLoginWeb;
            var webClient = new WebClient
            {
                Encoding = Encoding.UTF8
            };
            webClient.Headers.Add("Content-Type", "application/json; charset=utf-8");

            LoginService.CreateAuthorization(webClient, userName, passWord);
            return webClient;
        }


        public static SqlConnection GetSqlConnectionMisaTest()
        {
            var connectionString = Properties.Settings.Default.ConnectionString;
            // Log.Debug(connectionString);
            var sqlConnection = new SqlConnection(connectionString);
            return sqlConnection;
        }

        public static void GetDataFromMisaToMinvoiceTest(int choose = 1)
        {
            try
            {
                var sqlConnectionMisa = GetSqlConnectionMisaTest();
                sqlConnectionMisa.Open();
                var where = $"WHERE So_Ct >=2700 ";
                var dataTableInvoice = DataContext.GetDataTableTest(sqlConnectionMisa, Properties.Settings.Default.TableInvocie, where);
                int i = 0;

                List<String> listrow = new List<String>();
                if (dataTableInvoice.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTableInvoice.Rows)
                    {
                        int c = 0, c1 = 0, c2 = 0, c3 = 0, c4 = 0;
                        if (i != 0)
                        {
                            c = listrow.ToArray().Where(s => s.ToString() == row["So_Ct"].ToString()).Count();
                            c1 = listrow.ToArray().Where(s => s.ToString() == row["So_Seri"].ToString()).Count();
                            c2 = listrow.ToArray().Where(s => s.ToString() == row["Ngay_Ct"].ToString()).Count();
                            c3 = listrow.ToArray().Where(s => s.ToString() == row["Ong_Ba"].ToString()).Count();
                            c4 = listrow.ToArray().Where(s => s.ToString() == row["Ma_So_Thue"].ToString()).Count();
                        }


                        if (c != 0 && c1 != 0 && c2 != 0 && c3 != 0 && c4 != 0)
                        {
                            continue;
                        }
                        else
                        {
                            listrow.Add(row["So_Ct"].ToString());
                            listrow.Add(row["So_Seri"].ToString());
                            listrow.Add(row["Ngay_Ct"].ToString());
                            listrow.Add(row["Ong_Ba"].ToString());
                            listrow.Add(row["Ma_So_Thue"].ToString());
                            i++;
                            //  var refId = row["RefID"].ToString();
                            var refId = "70A14F56-EBF9-4484-8D49-1A915E9A19D6";
                            if (!CheckInvoiceInMinvoice(refId, sqlConnectionMisa))
                            {
                                var jObjectData = JsonConvert.ConvertData(sqlConnectionMisa, row);
                                var jArrayData = new JArray { jObjectData };
                                var jObjectMainData = new JObject
                            {
                                {"windowid", "WIN00187"},
                                {"editmode", 1},
                                {"data", jArrayData}
                            };

                                var dataRequest = jObjectMainData.ToString();
                                //Log.Debug(dataRequest);
                                var url = Properties.Settings.Default.UrlSave;
                                using (var scope = new TransactionScope())
                                {
                                    try
                                    {
                                        var webClient = SetupWebClient();
                                        var result = webClient.UploadString(url, dataRequest);
                                        var resultResponse = JObject.Parse(result);
                                        if (resultResponse.ContainsKey("ok") && resultResponse.ContainsKey("data"))
                                        {
                                            var jToken = resultResponse["data"];
                                            /*  DataContext.UpdateMisa(sqlConnectionMisa, refId, Properties.Settings.Default.TableInvocie, jToken);
                                              if (Properties.Settings.Default.Version.Equals("2017"))
                                              {
                                                  DataContext.UpdateMisaVoucher(sqlConnectionMisa, refId, Properties.Settings.Default.TableVoucher, Properties.Settings.Default.TableVoucherDetail, jToken);
                                              }*/
                                            scope.Complete();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        XtraMessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                    }
                }
                if (choose == 1)
                {
                    XtraMessageBox.Show("Lấy dữ liệu thành công", "Thông Báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lấy dữ liệu thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static bool CheckInvoiceInMinvoice(string invoiceAuthId, SqlConnection sqlConnection)
        {

            var webClient = SetupWebClient();
            var url = Properties.Settings.Default.UrlGetInvoiceByInvoiceAuthId + invoiceAuthId;
            var result = webClient.DownloadString(url);
            JObject jObject = JObject.Parse(result);
            if (jObject.ContainsKey("inv_InvoiceAuth_id"))
            {
                ck = true;
                string shd = (string)jObject["inv_invoiceNumber"];
                // XtraMessageBox.Show($"Hóa đơn số {shd} đã tồn tại trên hệ thống Minvoice! Bạn vui lòng chọn chức năng phù hợp!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // DataContext.UpdateMisa(sqlConnection, invoiceAuthId, Properties.Settings.Default.TableInvocie, jObject);
                return true;
            }
            return false;
        }

        /*    public static void UpdateInvoice(string invoiceNumber)
            {
                try
                {
                    var sqlConnectionMisa = GetSqlConnectionMisaTest();
                    sqlConnectionMisa.Open();
                    var where = $"WHERE InvNo IN ({invoiceNumber})";
                    var dataTableInvoice = DataContext.GetDataTableTest(sqlConnectionMisa, Properties.Settings.Default.TableInvocie, where);
                    if (dataTableInvoice.Rows.Count > 0)
                    {
                        foreach (DataRow row in dataTableInvoice.Rows)
                        {
                            var refId = row["RefID"].ToString();
                            var invNo = row["InvNo"].ToString();

                            var jObjectData = JsonConvert.ConvertData(sqlConnectionMisa, row);
                            var jArrayData = new JArray { jObjectData };
                            var jObjectMainData = new JObject
                                {
                                    {"windowid", "WIN00187"},
                                    {"editmode", 2},
                                    {"data", jArrayData}
                                };

                            var dataRequest = jObjectMainData.ToString();
                            var url = Properties.Settings.Default.UrlSave;
                            using (var scope = new TransactionScope())
                            {
                                try
                                {
                                    var webClient = SetupWebClient();
                                    var result = webClient.UploadString(url, dataRequest);
                                    var resultResponse = JObject.Parse(result);
                                    if (resultResponse.ContainsKey("ok") && resultResponse.ContainsKey("data"))
                                    {
                                        var jToken = resultResponse["data"];
                                        DataContext.UpdateMisa(sqlConnectionMisa, refId, Properties.Settings.Default.TableInvocie, jToken);
                                        if (Properties.Settings.Default.Version == "2017")
                                        {
                                            DataContext.UpdateMisaVoucher(sqlConnectionMisa, refId, Properties.Settings.Default.TableVoucher, Properties.Settings.Default.TableVoucherDetail, jToken);
                                        }
                                        scope.Complete();
                                        XtraMessageBox.Show($"Cập nhật hóa đơn {invNo} thành công", "Thông Báo", MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                                    }
                                }
                                catch (Exception ex)
                                {
                                }
                            }

                        }
                    }
                }
                catch (Exception e)
                {
                    XtraMessageBox.Show(e.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }*/
        public static async Task DownloadXMLAsync(List<string> sobaomat, string txtPath)
        {
            try
            {
                int i = 0;
                foreach (var sbm in sobaomat)
                {
                    ///////////////////////////////
                    CancellationToken tokenAsync = tokenSource.Token;
                    if (tokenAsync.IsCancellationRequested)
                    {
                        break;
                    }

                    i++;
                    var webClient = new WebClient
                    {
                        Encoding = Encoding.UTF8
                    };
                    webClient.Headers.Add("Content-Type", "application/json; charset=utf-8");
                    JObject json = new JObject
                    {
                        {"masothue",Properties.Settings.Default.MST},
                        {"sobaomat",sbm.ToString() }

                    };

                    var url = Properties.Settings.Default.UrlDownloadXML;
                    //var ketqua1 = webClient.UploadString(url, json.ToString());
                    byte[] byteArray = Encoding.ASCII.GetBytes(json.ToString());

                    byte[] ketqua = await webClient.UploadDataTaskAsync(url, byteArray);
                    Encoding.ASCII.GetString(ketqua);

                    string filenameEncoding = webClient.ResponseHeaders["Content-Disposition"].ToString();
                    string fileName = filenameEncoding.Substring(filenameEncoding.IndexOf("=") + 1);

                    System.IO.File.WriteAllBytes(txtPath + "\\" + fileName, ketqua);
                    FormGetXML.frmget.lblDatai.Text = i.ToString();
                }
            }
            catch (Exception e)
            {
                ck = true;
                XtraMessageBox.Show(e.ToString(), "Lỗi kết nối!", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                
            }
           



        }
        public static async Task DownloadPDFAsync( string txtPath, List<string> listid, List<InvoiceObject> invoiceObjects)
        {
            try
            {
                int i = 0;
                var webClient = SetupWebClient();
                foreach (var id in invoiceObjects)
                {

                    CancellationToken tokenAsync = tokenSource.Token;
                    if (tokenAsync.IsCancellationRequested)
                    {
                        break;
                    }
                    i++;
                    if (i % 2000 == 0)
                    {
                        webClient = SetupWebClient();
                    }

                    var url = Properties.Settings.Default.UrlDownloadPDF + id.InvInvoiceAuthId;

                    byte[] ketqua = await webClient.DownloadDataTaskAsync(url);

                    //var url = Properties.Settings.Default.UrlDownloadXML;
                    ////var ketqua1 = webClient.UploadString(url, json.ToString());
                    //byte[] byteArray = Encoding.ASCII.GetBytes(json.ToString());

                    //byte[] ketqua = webClient.UploadData(url, byteArray);
                    //Encoding.ASCII.GetString(ketqua);

                    //string filenameEncoding = webClient.ResponseHeaders["Content-Disposition"].ToString();
                    //string fileName = filenameEncoding.Substring(filenameEncoding.IndexOf("=") + 1);
                    string fileName = "sohd_" + id.InvoiceNumber + "_" + id.ngay_hd.ToString("yyyy-MM-dd") + "_" + id.KyHieu.Trim().Replace("/", "-") + ".pdf";

                    System.IO.File.WriteAllBytes(txtPath + "\\" + fileName, ketqua);
                    FormGetXML.frmget.lblDatai.Text = i.ToString();
                }
            }
            catch (Exception e)
            {
                ck = true;
                XtraMessageBox.Show(e.ToString(), "Lỗi kết nối!", MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            }



        }
        public static async Task DownloadXML78Async( string txtPath, List<string> listid ,List<InvoiceObject> invoiceObjects78)
        {
            try
            {
                int i = 0;
                var webClient = SetupWebClient();
                foreach (var id78 in invoiceObjects78)
                {
                    CancellationToken tokenAsync = tokenSource.Token;
                    if (tokenAsync.IsCancellationRequested)
                    {
                        break;
                    }
                    i++;
                    if (i % 2000 == 0)
                    {
                        webClient = SetupWebClient();
                    }



                    var url = Properties.Settings.Default.UrlDownloadXml78 + id78.hoadon68id;

                    var ketqua = await  webClient.DownloadStringTaskAsync(new Uri(url));
                    // Encoding.ASCII.GetString(ketqua);
                    JObject xml = JObject.Parse(ketqua.ToString());
                    XDocument xmlraw=null;



                        if (xml["code"].ToString() == "00")
                        {
                            JObject xmlJobject = JObject.Parse(xml["data"].ToString());
                             xmlraw = XDocument.Parse(System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(xmlJobject["data"].ToString())));
                             
                        }



                    //string theDate = id78.ngay_hd.ToString("yyyy-MM-dd");
                   // DateTime dt = id78.ngay_hd.Date;

                    //string filenameEncoding = webClient.ResponseHeaders.ToString();
                    string fileName = "sohd_"+id78.shdon+"_"+ id78.ngay_hd.ToString("yyyy-MM-dd") + "_" + id78.KyHieu+".xml";

                    System.IO.File.WriteAllBytes(txtPath + "\\" + fileName, Encoding.UTF8.GetBytes(xmlraw.ToString()));
                    FormGetXML.frmget.lblDatai.Text = i.ToString();
                }
            }
            catch (Exception e)
            {
                ck = true;
                XtraMessageBox.Show(e.ToString(), "Lỗi kết nối!", MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            }
           



        }
        public static async Task<string> Downloadpdf78(string txtPath, List<string> listid, List<InvoiceObject> invoiceObjects78)
        {
            try
            {      
                await DownloadPDF78Async(invoiceObjects78, txtPath);
              
            }
            catch (Exception e)
            {
                ck = true;
                XtraMessageBox.Show(e.ToString(), "Lỗi kết nối!", MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            }
            return "ok";



        }
        public static void UpdateInvoice(List<InvoiceObject> invoiceObjects)
        {
            try
            {

                string invInvoiceAuthId = "";
                foreach (var invoiceObject in invoiceObjects)
                {
                    invInvoiceAuthId += $"'{invoiceObject.InvInvoiceAuthId}' ,";
                }

                invInvoiceAuthId = invInvoiceAuthId.Substring(0, invInvoiceAuthId.Length - 1);
                var sqlConnectionMisa = GetSqlConnectionMisaTest();
                sqlConnectionMisa.Open();
                string sqlcmd1 = $"SELECT a.*, b.ID_HD FROM {Properties.Settings.Default.TableInvocie} a LEFT JOIN B3009CT b ON a.Stt = b.Stt WHERE b.ID_HD IN({invInvoiceAuthId})";
                int i = 0;

                List<String> listrow = new List<String>();

                var dataTableInvoice = DataContext.GetDataTableTest(sqlConnectionMisa, sqlcmd1);


                if (dataTableInvoice.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTableInvoice.Rows)
                    {
                        var refId = row["ID_HD"].ToString().Trim();

                        var invoiceObject = invoiceObjects.Single(x => x.InvInvoiceAuthId == refId);
                        var invNo = invoiceObject.InvoiceNumber;

                        int c = 0;
                        if (i != 0)
                        {
                            c = listrow.ToArray().Where(s => s.ToString() == row["ID_HD"].ToString()).Count();

                        }


                        if (c != 0)
                        {
                            continue;
                        }
                        else
                        {

                            listrow.Add(row["ID_HD"].ToString());
                            i++;
                            var jObjectData = JsonConvert.ConvertDataUpdate(sqlConnectionMisa, row, invNo);
                            var jArrayData = new JArray { jObjectData };
                            var jObjectMainData = new JObject
                            {
                                {"windowid", "WIN00187"},
                                {"editmode", 2},
                                {"data", jArrayData}
                            };

                            var dataRequest = jObjectMainData.ToString();

                            // Log.Debug(dataRequest);

                            var url = Properties.Settings.Default.UrlSave;
                            using (var scope = new TransactionScope())
                            {
                                try
                                {
                                    var webClient = SetupWebClient();
                                    var result = webClient.UploadString(url, dataRequest);
                                    var resultResponse = JObject.Parse(result);
                                    if (resultResponse.ContainsKey("ok") && resultResponse.ContainsKey("data"))
                                    {
                                        var jToken = resultResponse["data"];
                                        // DataContext.UpdateMisa(sqlConnectionMisa, refId, Properties.Settings.Default.TableInvocie, jToken);
                                        //      if (Properties.Settings.Default.Version == "2017")
                                        //   {
                                        //         DataContext.UpdateMisaVoucher(sqlConnectionMisa, refId, Properties.Settings.Default.TableVoucher, Properties.Settings.Default.TableVoucherDetail, jToken);
                                        //    }
                                        scope.Complete();
                                        XtraMessageBox.Show($"Cập nhật hóa đơn {invNo} thành công", "Thông Báo", MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);
                                        string error = (string)jObject["error"];
                                        if (error != "")
                                            MessageBox.Show($"Cập nhật hóa đơn {invNo} thất bại! Do { error} ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    XtraMessageBox.Show($"Lỗi kết quả trả về số hđ {invNo} ", "lỗi", MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                XtraMessageBox.Show(e.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static async Task DownloadPDF78Async(List<InvoiceObject> invoiceObjects78, string txtPath)
        {
            int i = 0;
            var webClient = SetupWebClient();
            foreach (var id78 in invoiceObjects78)
            {
                CancellationToken tokenAsync = tokenSource.Token;
                if (tokenAsync.IsCancellationRequested)
                {
                    break;
                }


                i++;
                if (i % 2000 == 0)
                {
                    webClient = SetupWebClient();
                }
                var url = Properties.Settings.Default.UrlDownloadpdf78 + id78.hoadon68id;

                byte[] ketqua = await webClient.DownloadDataTaskAsync(new Uri(url));

                string fileName = "sohd_" + id78.shdon + "_" + id78.ngay_hd.ToString("dd-MM-yyyy") + "_" + id78.KyHieu + ".pdf";

                System.IO.File.WriteAllBytes(txtPath + "\\" + fileName, ketqua);

                FormGetXML.frmget.lblDatai.Text = i.ToString();
                //FormGetXML frm = (FormGetXML)Owner; ;
                //frm.LabelText = i.ToString();
            }
        }

        private static byte[] CombineTwoByteArrays(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }

        // Hàm kiểm tra xem mảng byte có phải pdf hay không
        public static bool IsPDFFile(byte[] data)
        {
            if (data != null && data.Length > 4 &&
                data[0] == 0x25 && // %
                data[1] == 0x50 && // P
                data[2] == 0x44 && // D
                data[3] == 0x46 && // F
                data[4] == 0x2D)
            { // -

                // version 1.3 file terminator
                if (data[5] == 0x31 && data[6] == 0x2E && data[7] == 0x33 &&
                        data[data.Length - 7] == 0x25 && // %
                        data[data.Length - 6] == 0x25 && // %
                        data[data.Length - 5] == 0x45 && // E
                        data[data.Length - 4] == 0x4F && // O
                        data[data.Length - 3] == 0x46 && // F
                        data[data.Length - 2] == 0x20 && // SPACE
                        data[data.Length - 1] == 0x0A)
                { // EOL
                    return true;
                }

                // version 1.3 file terminator
                if (data[5] == 0x31 && data[6] == 0x2E && data[7] == 0x34 &&
                        data[data.Length - 6] == 0x25 && // %
                        data[data.Length - 5] == 0x25 && // %
                        data[data.Length - 4] == 0x45 && // E
                        data[data.Length - 3] == 0x4F && // O
                        data[data.Length - 2] == 0x46 && // F
                        data[data.Length - 1] == 0x0A)
                { // EOL
                    return true;
                }
            }
            return false;
        }



                            /*              Xem in hóa đơn 78               */


                         /*         Gộp các file pdf hóa đơn thông tư 32 tải về
                                    Hiển thị pdf đã gộp lên cho người dùng xem         */
        public static async Task ShowPrintPDFAsync(string txtPath, List<string> listID, List<InvoiceObject> listInvoiceObject)
        {

            // Khai báo tên file gộp hóa đơn tải về
            // Khai báo biến i để kiểm tra số hóa đơn tải về nếu quá 2000 lần thì sẽ phải setup lại WebClient
            string fileName = "HoaDonTT32.pdf";
            int i = 0;

            // Tạo danh sách kiểu byte[] chứa các hóa đơn tải về từ API tải PDF
            List<byte[]> danhSachHoaDon = new List<byte[]>();

            try
            {

                // Khai báo webClient có thông tin về token và header để chuẩn bị nhận data từ API
                // Tạo vòng lặp để lấy dữ liệu từ API trả về mảng byte 
                WebClient webClient = SetupWebClient();
                foreach(var id in listInvoiceObject)
                {
                    // Khai báo biến hoaDon kiểu byte[] để nhận dữ liệu của từng file PDF nhận về
                    byte[] hoaDon;


                    // Nhận token từ Task xem hóa đơn tổng hợp
                    // Kiểm tra nếu nhấn nút dừng thì sẽ dừng Task xem in hóa đơn đang chạy bất đồng bộ
                    CancellationToken tokenAsync = tokenSource.Token;
                    if (tokenAsync.IsCancellationRequested)
                    {
                        break;
                    }

                    var url =  Properties.Settings.Default.UrlDownloadpdf78 + id.hoadon68id;

                    // Kiểm tra số lần lấy dữ liệu từ API
                    i++;
                    if(i % 2000 == 0)
                    {
                        webClient = SetupWebClient();
                    }

                    // Nhận dữ liệu file PDF trả về mảng byte từ API
                    hoaDon = await webClient.DownloadDataTaskAsync(new Uri(url));
                    danhSachHoaDon.Add(hoaDon);
                }


                // Sử dụng thư viện PDFSharp để gộp các mảng byte PDF đã tải về được
                // Lưu file vào địa chỉ cho trước
                using(var ms = new MemoryStream())
                {
                    using(var resultPDF = new PdfDocument(ms))
                    {
                        foreach(var hoadonPDF in danhSachHoaDon)
                        {
                            using(var src = new MemoryStream(hoadonPDF)) 
                            {
                                using(var srcPDF = PdfReader.Open(src, PdfDocumentOpenMode.Import))
                                {
                                    FormGetXML.frmget.lblDatai.Text = i.ToString();
                                    for(var count = 0; count < srcPDF.PageCount; count++)
                                    {
                                        resultPDF.AddPage(srcPDF.Pages[count]);
                                    }
                                } 
                            }
                        }
                        resultPDF.Save(ms);
                        System.IO.File.WriteAllBytes(txtPath + "\\" + fileName, ms.ToArray());
                    }
                }


                // Hiển thị xem in cho người dùng xem
                using (FormPDF form = new FormPDF(txtPath + "\\" + fileName))
                {
                    form.ShowDialog();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
                            /*          Kết thúc xem in theo hóa đơn 32             */




                                /*          Xem in theo hóa đơn 78          */
        public static async Task ShowPrintPDF78Async(string txtPath, List<string> listID, List<InvoiceObject> listInvoiceObject)
        {
            // Khai báo tên file gộp hóa đơn tải về
            // Khai báo biến i để kiểm tra số hóa đơn tải về nếu quá 2000 lần thì sẽ phải setup lại WebClient
            string fileName = "HoaDonTT32.pdf";
            int i = 0;

            // Tạo danh sách kiểu byte[] chứa các hóa đơn tải về từ API tải PDF
            List<byte[]> danhSachHoaDon = new List<byte[]>();

            try
            {

                // Khai báo webClient có thông tin về token và header để chuẩn bị nhận data từ API
                // Tạo vòng lặp để lấy dữ liệu từ API trả về mảng byte 
                WebClient webClient = SetupWebClient();
                foreach (var id in listInvoiceObject)
                {
                    // Khai báo biến hoaDon kiểu byte[] để nhận dữ liệu của từng file PDF nhận về
                    byte[] hoaDon;

                    // Nhận token từ Task xem hóa đơn tổng hợp
                    // Kiểm tra nếu nhấn nút dừng thì sẽ dừng Task xem in hóa đơn đang chạy bất đồng bộ
                    CancellationToken tokenAsync = tokenSource.Token;
                    if (tokenAsync.IsCancellationRequested)
                    {
                        break;
                    }

                    var url = Properties.Settings.Default.UrlDownloadpdf78 + id.hoadon68id;

                    // Kiểm tra số lần lấy dữ liệu từ API
                    i++;
                    if (i % 2000 == 0)
                    {
                        webClient = SetupWebClient();
                    }

                    // Nhận dữ liệu file PDF trả về mảng byte từ API
                    hoaDon = await webClient.DownloadDataTaskAsync(url);
                    danhSachHoaDon.Add(hoaDon);
                }
                webClient.Dispose();


                // Sử dụng thư viện PDFSharp để gộp các mảng byte PDF đã tải về được
                // Lưu file vào địa chỉ cho trước
                using (var ms = new MemoryStream())
                {
                    using (var resultPDF = new PdfDocument(ms))
                    {
                        foreach (var hoadonPDF in danhSachHoaDon)
                        {
                            using (var src = new MemoryStream(hoadonPDF))
                            {
                                using (var srcPDF = PdfReader.Open(src, PdfDocumentOpenMode.Import))
                                {
                                    // Nén file lại để giảm kích thước của file PDF
                                    srcPDF.Options.FlateEncodeMode = PdfFlateEncodeMode.BestCompression;
                                    srcPDF.Options.UseFlateDecoderForJpegImages = PdfUseFlateDecoderForJpegImages.Automatic;
                                    srcPDF.Options.NoCompression = false;
                                    srcPDF.Options.CompressContentStreams = true;


                                    FormGetXML.frmget.lblDatai.Text = i.ToString();

                                    for (var count = 0; count < srcPDF.PageCount; count++)
                                    {
                                        resultPDF.AddPage(srcPDF.Pages[count]);
                                    }
                                }
                            }
                        }
                        resultPDF.Save(ms);
                        System.IO.File.WriteAllBytes(txtPath + "\\" + fileName, ms.ToArray());
                    }
                }
                danhSachHoaDon = null;

                // Sau khi tải xong thì dừng đồng hồ đếm
                // Gán giá trị đồng hồ đếm cho label để hiển thị cho khách hàng xem thời gian
                FormGetXML.frmget._stopwatch.Stop();
                FormGetXML.frmget.UpdateDisplay();
                FormGetXML.frmget._labelUpdateTimer.Stop();


                // Hiển thị xem in cho người dùng xem
                using (FormPDF form = new FormPDF(txtPath + "\\" + fileName))
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
                            /*          Kết thúc xem in theo hóa đơn 78            */
    }
}
