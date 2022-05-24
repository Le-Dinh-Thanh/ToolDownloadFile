using DevExpress.XtraEditors;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using System.Xml.Linq;
using toolVanDao.Config;
using toolVanDao.Data;

namespace toolVanDao.Services
{
    public class MinvoiceService
    {
        public static bool ck = false;

        public static WebClient SetupWebClient()
        {
            var userName = BaseConfig.UsernameLoginWeb;
            var passWord = BaseConfig.PasswordLoginWeb;
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
            var connectionString = BaseConfig.ConnectionString;
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
                var dataTableInvoice = DataContext.GetDataTableTest(sqlConnectionMisa, BaseConfig.TableInvocie, where);
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
                                var url = BaseConfig.UrlSave;
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
                                            /*  DataContext.UpdateMisa(sqlConnectionMisa, refId, BaseConfig.TableInvocie, jToken);
                                              if (BaseConfig.Version.Equals("2017"))
                                              {
                                                  DataContext.UpdateMisaVoucher(sqlConnectionMisa, refId, BaseConfig.TableVoucher, BaseConfig.TableVoucherDetail, jToken);
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
            catch (Exception e)
            {
                XtraMessageBox.Show("Lấy dữ liệu thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static bool CheckInvoiceInMinvoice(string invoiceAuthId, SqlConnection sqlConnection)
        {

            var webClient = SetupWebClient();
            var url = BaseConfig.UrlGetInvoiceByInvoiceAuthId + invoiceAuthId;
            var result = webClient.DownloadString(url);
            JObject jObject = JObject.Parse(result);
            if (jObject.ContainsKey("inv_InvoiceAuth_id"))
            {
                ck = true;
                string shd = (string)jObject["inv_invoiceNumber"];
                // XtraMessageBox.Show($"Hóa đơn số {shd} đã tồn tại trên hệ thống Minvoice! Bạn vui lòng chọn chức năng phù hợp!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // DataContext.UpdateMisa(sqlConnection, invoiceAuthId, BaseConfig.TableInvocie, jObject);
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
                    var dataTableInvoice = DataContext.GetDataTableTest(sqlConnectionMisa, BaseConfig.TableInvocie, where);
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
                            var url = BaseConfig.UrlSave;
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
                                        DataContext.UpdateMisa(sqlConnectionMisa, refId, BaseConfig.TableInvocie, jToken);
                                        if (BaseConfig.Version == "2017")
                                        {
                                            DataContext.UpdateMisaVoucher(sqlConnectionMisa, refId, BaseConfig.TableVoucher, BaseConfig.TableVoucherDetail, jToken);
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
        public static void DownloadXML(List<string> sobaomat, string txtPath)
        {
            try
            {
                foreach (var sbm in sobaomat)
                {
                    ///////////////////////////////



                    var webClient = new WebClient
                    {
                        Encoding = Encoding.UTF8
                    };
                    webClient.Headers.Add("Content-Type", "application/json; charset=utf-8");
                    JObject json = new JObject
                    {
                        {"masothue",BaseConfig.MST},
                        {"sobaomat",sbm.ToString() }

                    };

                    var url = BaseConfig.UrlDownloadXML;
                    //var ketqua1 = webClient.UploadString(url, json.ToString());
                    byte[] byteArray = Encoding.ASCII.GetBytes(json.ToString());

                    byte[] ketqua = webClient.UploadData(url, byteArray);
                    Encoding.ASCII.GetString(ketqua);

                    string filenameEncoding = webClient.ResponseHeaders["Content-Disposition"].ToString();
                    string fileName = filenameEncoding.Substring(filenameEncoding.IndexOf("=") + 1);

                    System.IO.File.WriteAllBytes(txtPath + "\\" + fileName, ketqua);

                }
            }
            catch (Exception e)
            {
                ck = true;
                XtraMessageBox.Show(e.ToString(), "Lỗi kết nối!", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                
            }
            ///////////////////////////////////////////////////////
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri(url);

            //// Add an Accept header for JSON format.
            //client.DefaultRequestHeaders.Accept.Add(
            //new MediaTypeWithQualityHeaderValue("application/json"));
            //var content = new FormUrlEncodedContent(new[]
            //{
            //new KeyValuePair<string, string>("masothue", BaseConfig.MST),
            //new KeyValuePair<string, string>("sobaomat", sbm.ToString())
            // });
            //// List data response.
            //HttpResponseMessage response = client.PostAsync( "",content).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            //if (response.IsSuccessStatusCode)
            //{
            //   string fileName = response.Content.Headers.ContentDisposition.FileName;




            //    response.Content = new StreamContent(new FileStream(txtPath+fileName, FileMode.Open, FileAccess.Read));
            //    response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            //    //response.Content.Headers.ContentDisposition.FileName = fileName;
            //    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
            //    // Parse the response body.
            //    //var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
            //    //foreach (var d in dataObjects)
            //    //{
            //    //    Console.WriteLine("{0}", d.Name);
            //    //}
            //}
            //else
            //{
            //    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            //}

            ////Make any other calls using HttpClient here.

            ////Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            //client.Dispose();

            //if (String.IsNullOrEmpty(id))
            //    return Request.CreateResponse(HttpStatusCode.BadRequest);

            //string fileName1;
            //string localFilePath;
            //int fileSize;

            ////localFilePath = getFileFromID(result, out fileName1, out fileSize);
            //fileName1 = response.Content.Headers.ContentDisposition.FileName;
            ////HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            //response.Content = new StreamContent(new FileStream(txtPath+fileName1, FileMode.Open, FileAccess.ReadWrite));
            //response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");

            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");



            //byte[] byteArray = Encoding.ASCII.GetBytes(invoice);



            //byte[] ketqua = client2.UploadData("http://0103407189.minvoice.com.vn/api/Invoice/InChuyenDoi", byteArray);
            //Encoding.ASCII.GetString(ketqua);

            //System.IO.File.WriteAllBytes(@"D:\hello.pdf", ketqua);
            ////FileStream oFS = File.OpenRead(@"top-secret.pdf");
            ////oFS.Read(ketqua, 0, Convert.ToInt32(oFS.Length));


            //FileStream oFS = File.OpenRead(@"D:\hello.pdf");
            //byte[] baPDF = new byte[oFS.Length];

            //oFS.Read(ketqua, 0, Convert.ToInt32(oFS.Length));

            //pdfViewer1.LoadDocument(oFS);


            //JObject jObject = JObject.Parse(result);
            //if (jObject.ContainsKey("inv_InvoiceAuth_id"))
            //{

            //}



        }
        public static void DownloadPDF( string txtPath, List<string> listid, List<InvoiceObject> invoiceObjects)
        {
            try
            {
                foreach (var id in invoiceObjects)
                {



                    var webClient = SetupWebClient();
                    var url = BaseConfig.UrlDownloadPDF + id.InvInvoiceAuthId;

                    byte[] ketqua = webClient.DownloadData(url);

                    //var url = BaseConfig.UrlDownloadXML;
                    ////var ketqua1 = webClient.UploadString(url, json.ToString());
                    //byte[] byteArray = Encoding.ASCII.GetBytes(json.ToString());

                    //byte[] ketqua = webClient.UploadData(url, byteArray);
                    //Encoding.ASCII.GetString(ketqua);

                    //string filenameEncoding = webClient.ResponseHeaders["Content-Disposition"].ToString();
                    //string fileName = filenameEncoding.Substring(filenameEncoding.IndexOf("=") + 1);
                    string fileName = "sohd_" + id.InvoiceNumber + "_" + id.ngay_hd.ToString("yyyy-MM-dd") + "_" + id.KyHieu.Trim().Replace("/","-") + ".pdf";

                    System.IO.File.WriteAllBytes(txtPath + "\\" + fileName, ketqua);

                }
            }
            catch (Exception e)
            {
                ck = true;
                XtraMessageBox.Show(e.ToString(), "Lỗi kết nối!", MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            }
          


        }
        public static void DownloadXML78( string txtPath, List<string> listid ,List<InvoiceObject> invoiceObjects78)
        {
            try
            {
                foreach (var id78 in invoiceObjects78)
                {
                    
                   
                  
                    var webClient = SetupWebClient();
                    var url = BaseConfig.UrlDownloadXml78 + id78.hoadon68id;

                    var ketqua = webClient.DownloadString(url);
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

                }
            }
            catch (Exception e)
            {
                ck = true;
                XtraMessageBox.Show(e.ToString(), "Lỗi kết nối!", MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            }
           



        }
        public static void Downloadpdf78(string txtPath, List<string> listid, List<InvoiceObject> invoiceObjects78)
        {
            try
            {
                foreach (var id78 in invoiceObjects78)
                {



                    var webClient = SetupWebClient();
                    var url = BaseConfig.UrlDownloadpdf78 + id78.hoadon68id;

                    byte[] ketqua = webClient.DownloadData(url);
                     //Encoding.ASCII.GetString(ketqua);
                    //JObject pdf = JObject.Parse(ketqua.ToString());
                    //XDocument pdfraw = null;


                    //var body = @"";
                    //request.AddParameter("application/json", body, ParameterType.RequestBody);
                    //IRestResponse response = client.Execute(request);
                    //Console.WriteLine(response.Content);
                    //if (xml["code"].ToString() == "00")
                    //{
                    //    JObject xmlJobject = JObject.Parse(xml["data"].ToString());
                    //    xmlraw = XDocument.Parse(System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(xmlJobject["data"].ToString())));

                    //}



                    //string theDate = id78.ngay_hd.ToString("yyyy-MM-dd");
                    // DateTime dt = id78.ngay_hd.Date;

                    //string filenameEncoding = webClient.ResponseHeaders["Content-Disposition"].ToString();
                    //string fileName = filenameEncoding.Substring(filenameEncoding.IndexOf("=") + 1);
                    string fileName = "sohd_" + id78.shdon + "_" + id78.ngay_hd.ToString("yyyy-MM-dd") + "_" + id78.KyHieu + ".pdf";

                    System.IO.File.WriteAllBytes(txtPath + "\\" + fileName, ketqua);

                }
            }
            catch (Exception e)
            {
                ck = true;
                XtraMessageBox.Show(e.ToString(), "Lỗi kết nối!", MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            }




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
                string sqlcmd1 = $"SELECT a.*, b.ID_HD FROM {BaseConfig.TableInvocie} a LEFT JOIN B3009CT b ON a.Stt = b.Stt WHERE b.ID_HD IN({invInvoiceAuthId})";
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

                            var url = BaseConfig.UrlSave;
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
                                        // DataContext.UpdateMisa(sqlConnectionMisa, refId, BaseConfig.TableInvocie, jToken);
                                        //      if (BaseConfig.Version == "2017")
                                        //   {
                                        //         DataContext.UpdateMisaVoucher(sqlConnectionMisa, refId, BaseConfig.TableVoucher, BaseConfig.TableVoucherDetail, jToken);
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
    }
}
