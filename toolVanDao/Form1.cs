using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using toolVanDao.Forms;
using toolVanDao.Services;
using toolVanDao.Config;
using DevExpress.XtraEditors;
using Newtonsoft.Json.Linq;
using System.Transactions;
using System.Data.SqlClient;

namespace toolVanDao
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        DataGridViewCheckBoxColumn dgvcCheckBox = new DataGridViewCheckBoxColumn();
        List<JObject> LISTOBJECT = new List<JObject>();
        List<InvoiceOBJ> listobj = new List<InvoiceOBJ>();
        int couttable;
        int click = 0;
        bool cx = false;
        bool ktra = false;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FormLoadData load = new FormLoadData();
            load.ShowDialog();
            if (FormLoadData.ckk == true)
            {
                CBSeries.Enabled = true;
                tuNgay.Enabled = true;
                denNgay.Enabled = true;
                btnUpdate.Enabled = true;
                simpleButton2.Enabled = true;
                //simpleButton3.Enabled = true;
                notifyIcon1.ShowBalloonTip(5000, "Thông Báo", "Đã kết nối", ToolTipIcon.Info);
            }
          //  LoadKyhieu();
           
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Formloading frmLoading = new Formloading();
            frmLoading.ShowDialog();
        }
        public void LoadKyhieu()
        {
            var sqlConnection = MinvoiceService.GetSqlConnectionMisaTest();
            sqlConnection.Open();
            string sqlcmd = $"SELECT So_Seri FROM {BaseConfig.TableInvocie}  GROUP BY So_Seri Order By So_Seri";
            
            var TableInvoiceSeries = DataContext.GetDataTableTest(sqlConnection, sqlcmd);
            try
            {
                
                CBSeries.DataSource = TableInvoiceSeries;
                CBSeries.DisplayMember = "Ký hiệu";
                CBSeries.ValueMember = "So_Seri";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
           
           // Formloading frmload = new Formloading();
           // frmload.ShowDialog();
            GetDataFromDateToDate();
        }
        public  void GetDataFromDateToDate()
        {
            int cout = 0;
            try
            {
                
                if (tuNgay.SelectedText != "" && denNgay.SelectedText != "")
                {
                    int ss = tuNgay.SelectedText.CompareTo(denNgay.SelectedText);
                    if (ss > 0)
                    {
                        XtraMessageBox.Show("'Từ ngày' không được lớn hơn 'Đến ngày'!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {

                        cout++;
                        //So_Seri = '{CBSeries.SelectedValue.ToString()}' AND
                        int choose = 1;
                        cout++; var sqlConnection = MinvoiceService.GetSqlConnectionMisaTest();
                        cout++; sqlConnection.Open();
                        cout++; string sqlcmd = $" SELECT * FROM {BaseConfig.TableInvocie} WHERE  Ngay_Ct BETWEEN '{tuNgay.DateTime.ToString("yyyy-MM-dd")}' AND '{denNgay.DateTime.ToString("yyyy-MM-dd")}'ORDER BY Ngay_Ct";


                        cout++; var TableInvoiceDate = DataContext.GetDataTableTest(sqlConnection, sqlcmd);
                        int i = 0;
                        HashSet<String> list1 = new HashSet<String>();
                        //List<String> listrow = new List<String>();
                        cout++; if (TableInvoiceDate.Rows.Count > 0)
                        {
                            notifyIcon1.ShowBalloonTip(5000, "Thông Báo", "Đang lấy dữ liệu!Vui lòng chờ!", ToolTipIcon.Info);
                            foreach (DataRow row in TableInvoiceDate.Rows)
                            {
                                cout++;
                                var RefID = "70A14F56-EBF9-4484-8D49-1A915E9A19D6";
                                int c = 0;// c1 = 0, c2 = 0, c3 = 0, c4 = 0;
                                if (i != 0)
                                {
                                    // System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
                                    cout++; // st.Start();
                                    if ( list1.Contains(row["So_Ct"].ToString()) && list1.Contains(row["So_Seri"].ToString()) && list1.Contains(row["Ngay_Ct"].ToString()) && list1.Contains(row["Ong_Ba"].ToString()) && list1.Contains(row["Ma_So_Thue"].ToString()))
                                    {
                                        cout++;
                                        c = 1;
                                    }
                                    /*   c = listrow.ToArray().Where(s => s.ToString() == row["So_Ct"].ToString()).Count();
                                       c1 = listrow.ToArray().Where(s => s.ToString() == row["So_Seri"].ToString()).Count();
                                       c2 = listrow.ToArray().Where(s => s.ToString() == row["Ngay_Ct"].ToString()).Count();
                                       c3 = listrow.ToArray().Where(s => s.ToString() == row["Ong_Ba"].ToString()).Count();
                                       c4 = listrow.ToArray().Where(s => s.ToString() == row["Ma_So_Thue"].ToString()).Count();*/
                                   // st.Stop();
                                  //  long timeslaped = st.ElapsedMilliseconds;
                                  //  double time = timeslaped / 1000;
                                   // Console.WriteLine("Thời gian thực hiện tìm kiếm là "+time+" giây");
                                }

                                if (c==1)
                                {
                                    continue;
                                }
                              /*  if (c != 0 && c1 != 0 && c2 != 0 && c3 != 0 && c4 != 0)
                                {
                                    continue;
                                }*/
                                else
                                {
                                    cout++;
                                    list1.Add(row["So_Ct"].ToString());
                                    list1.Add(row["So_Seri"].ToString());
                                    list1.Add(row["Ngay_Ct"].ToString());
                                    list1.Add(row["Ong_Ba"].ToString());
                                    list1.Add(row["Ma_So_Thue"].ToString());
                                    /*  listrow.Add(row["So_Ct"].ToString());
                                      listrow.Add(row["So_Seri"].ToString());
                                      listrow.Add(row["Ngay_Ct"].ToString());
                                      listrow.Add(row["Ong_Ba"].ToString());
                                      listrow.Add(row["Ma_So_Thue"].ToString());*/
                                    i++;
                                    cout++; string sqlcmd1 = $"SELECT ID_HD FROM B3009CT WHERE Stt = '{row["Stt"].ToString()}' AND  ID_HD <>''";
                                    DataTable tableIDHD = DataContext.GetDataTableTest(sqlConnection, sqlcmd1);
                                    cout++;
                                    //  var refId = row["RefID"].ToString();
                                    if (tableIDHD.Rows.Count>0)
                                    {
                                        cout++;
                                        foreach (DataRow r in tableIDHD.Rows)
                                        {
                                            cout++;
                                            RefID = r["ID_HD"].ToString();
                                        }
                                    }

                                    cout++;
                                    if (!MinvoiceService.CheckInvoiceInMinvoice(RefID, sqlConnection))
                                    {
                                        
                                        var jObjectData = JsonConvert.ConvertData(sqlConnection, row);
                                        
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
                                                var webClient = MinvoiceService.SetupWebClient();
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
                                                else
                                                {
                                                    string sqlcmd2 = $" UPDATE B3009CT SET ID_HD = '' WHERE Stt = '{row["Stt"].ToString()}' ";
                                                    SqlCommand sqlCommand = new SqlCommand(sqlcmd2, sqlConnection)
                                                    {
                                                        CommandType = CommandType.Text
                                                    };
                                                    sqlCommand.ExecuteNonQuery();
                                                    JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);
                                                    string error = (string)jObject["error"];
                                                    if (error != "")
                                                    {
                                                        MessageBox.Show($"Tạo hóa đơn thất bại! Do { error} ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    }
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
                        if (choose == 1 && TableInvoiceDate.Rows.Count > 0 && MinvoiceService.ck == false)
                        {
                            XtraMessageBox.Show("Lấy dữ liệu tạo hóa đơn thành công", "Thông Báo", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (ktra == false)
                            {
                                if (choose == 1 && TableInvoiceDate.Rows.Count > 0 && MinvoiceService.ck == true)
                                {
                                    XtraMessageBox.Show("1 số hóa đơn có thể bị thiếu, do đã tồn tại 1 số hóa đơn trên Minvoice! ", "Thông Báo", MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                XtraMessageBox.Show("Không có dữ liệu hóa đơn từ ngày: " + tuNgay.SelectedText.ToString() + " đến ngày: " + denNgay.SelectedText.ToString() + " ", "Thông Báo", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
                        }
                    }

                }
                else
                {
                    XtraMessageBox.Show("Bạn vui lòng chọn ngày hóa đơn đầy đủ!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                notifyIcon1.ShowBalloonTip(5000, "Thông Báo", "Tạo hóa đơn thất bại!", ToolTipIcon.Error);
                XtraMessageBox.Show("Lấy dữ liệu thất bại", "Lỗi  " + ex.ToString() + " dòng " + cout+"", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FormSelectUpdate fselect = new FormSelectUpdate();
            fselect.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //  simpleButton2.ImageOptions.Location = ImageLocation.TopCenter;
            CBSeries.Enabled = false;
            tuNgay.Enabled = false;
            denNgay.Enabled = false;
            btnUpdate.Enabled = false;
            simpleButton2.Enabled = false;
            simpleButton3.Enabled = false;




            dgvcCheckBox.Width = 40;



            dataGridView1.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.DataPropertyName =  "soct"; //cái này cần phải trùng với thuộc tính trong Entity Class
            col1.HeaderText = "Số chứng từ";
            col1.Width = 150;

            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.DataPropertyName = "ngayhd";
            col2.HeaderText = "Ngày HĐ";
            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.DataPropertyName = "tendv";
            col3.HeaderText = "Tên đơn vị";
            col3.Width = 150;

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.DataPropertyName = "makh";
            col4.HeaderText = "Mã khách hàng";
            col4.Width = 150;

            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.DataPropertyName = "tenng";
            col5.HeaderText = "Tên người mua";
            col5.Width = 150;

            DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
            col6.DataPropertyName = "mst";
            col6.HeaderText = "Mã số thuế";

            DataGridViewTextBoxColumn col7 = new DataGridViewTextBoxColumn();
            col7.DataPropertyName = "diachi";
            col7.HeaderText = "Địa chỉ";
            DataGridViewTextBoxColumn col8 = new DataGridViewTextBoxColumn();
            col8.DataPropertyName = "tongtien";
            col8.HeaderText = "Tổng Tiền";

            dataGridView1.Columns.Add(dgvcCheckBox);
            dataGridView1.Columns.Add(col1);
            dataGridView1.Columns.Add(col2);
            dataGridView1.Columns.Add(col3);
            dataGridView1.Columns.Add(col4);
            dataGridView1.Columns.Add(col5);
            dataGridView1.Columns.Add(col6);
            dataGridView1.Columns.Add(col7);
            dataGridView1.Columns.Add(col8);
        }

        private void layhd_Click(object sender, EventArgs e)
        {
            int cout = 0;
            try
            {
               
                
                if (tuNgay.SelectedText != "" && denNgay.SelectedText != "")
                {
                    int ss = tuNgay.SelectedText.CompareTo(denNgay.SelectedText);
                    if (ss > 0)
                    {
                        XtraMessageBox.Show("'Từ ngày' không được lớn hơn 'Đến ngày'!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {

                        cout++;
                        //So_Seri = '{CBSeries.SelectedValue.ToString()}' AND
                        int choose = 1;
                        cout++; var sqlConnection = MinvoiceService.GetSqlConnectionMisaTest();
                        cout++; sqlConnection.Open();
                        cout++; string sqlcmd = $" SELECT * FROM {BaseConfig.TableInvocie} WHERE  Ngay_Ct BETWEEN '{tuNgay.DateTime.ToString("yyyy-MM-dd")}' AND '{denNgay.DateTime.ToString("yyyy-MM-dd")}'ORDER BY Ngay_Ct";


                        cout++; var TableInvoiceDate = DataContext.GetDataTableTest(sqlConnection, sqlcmd);
                        int i = 0;
                        HashSet<String> list1 = new HashSet<String>();
                        //List<String> listrow = new List<String>();
                        couttable = TableInvoiceDate.Rows.Count;
                        cout++; if (TableInvoiceDate.Rows.Count > 0)
                        {
                            notifyIcon1.ShowBalloonTip(5000, "Thông Báo", "Đang lấy dữ liệu!Vui lòng chờ!", ToolTipIcon.Info);
                            foreach (DataRow row in TableInvoiceDate.Rows)
                            {
                                cout++;
                                var RefID = "70A14F56-EBF9-4484-8D49-1A915E9A19D6";
                                int c = 0;// c1 = 0, c2 = 0, c3 = 0, c4 = 0;
                                if (i != 0)
                                {
                                    // System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
                                    cout++; // st.Start();
                                    if (list1.Contains(row["So_Ct"].ToString()) && list1.Contains(row["So_Seri"].ToString()) && list1.Contains(row["Ngay_Ct"].ToString()) && list1.Contains(row["Ong_Ba"].ToString()) && list1.Contains(row["Ma_So_Thue"].ToString()))
                                    {
                                        cout++;
                                        c = 1;
                                    }
                                    /*   c = listrow.ToArray().Where(s => s.ToString() == row["So_Ct"].ToString()).Count();
                                       c1 = listrow.ToArray().Where(s => s.ToString() == row["So_Seri"].ToString()).Count();
                                       c2 = listrow.ToArray().Where(s => s.ToString() == row["Ngay_Ct"].ToString()).Count();
                                       c3 = listrow.ToArray().Where(s => s.ToString() == row["Ong_Ba"].ToString()).Count();
                                       c4 = listrow.ToArray().Where(s => s.ToString() == row["Ma_So_Thue"].ToString()).Count();*/
                                    // st.Stop();
                                    //  long timeslaped = st.ElapsedMilliseconds;
                                    //  double time = timeslaped / 1000;
                                    // Console.WriteLine("Thời gian thực hiện tìm kiếm là "+time+" giây");
                                }

                                if (c == 1)
                                {
                                    continue;
                                }
                                /*  if (c != 0 && c1 != 0 && c2 != 0 && c3 != 0 && c4 != 0)
                                  {
                                      continue;
                                  }*/
                                else
                                {
                                    cout++;
                                    list1.Add(row["So_Ct"].ToString());
                                    list1.Add(row["So_Seri"].ToString());
                                    list1.Add(row["Ngay_Ct"].ToString());
                                    list1.Add(row["Ong_Ba"].ToString());
                                    list1.Add(row["Ma_So_Thue"].ToString());
                                    /*  listrow.Add(row["So_Ct"].ToString());
                                      listrow.Add(row["So_Seri"].ToString());
                                      listrow.Add(row["Ngay_Ct"].ToString());
                                      listrow.Add(row["Ong_Ba"].ToString());
                                      listrow.Add(row["Ma_So_Thue"].ToString());*/
                                    i++;
                                    cout++; string sqlcmd1 = $"SELECT ID_HD FROM B3009CT WHERE Stt = '{row["Stt"].ToString()}' AND  ID_HD <>''";
                                    DataTable tableIDHD = DataContext.GetDataTableTest(sqlConnection, sqlcmd1);
                                    cout++;
                                    //  var refId = row["RefID"].ToString();
                                    if (tableIDHD.Rows.Count > 0)
                                    {
                                        cout++;
                                        foreach (DataRow r in tableIDHD.Rows)
                                        {
                                            cout++;
                                            RefID = r["ID_HD"].ToString();
                                        }
                                    }

                                    cout++;
                                      if (!MinvoiceService.CheckInvoiceInMinvoice(RefID, sqlConnection))
                                       {
                                   
                                        var jObjectData = JsonConvert.ConvertData(sqlConnection, row);
               
                                        var jArrayData = new JArray { jObjectData };
                                        var jObjectMainData = new JObject
                                        {
                                            {"windowid", "WIN00187"},
                                            {"editmode", 1},
                                            {"data", jArrayData}
                                        };
                                 
                                    
                                    
                                                 InvoiceOBJ obj = new InvoiceOBJ {
                                                 STT = row["Stt"].ToString(),
                                                 inv_invoiceauth_id = (string)jObjectData["inv_InvoiceAuth_id"],
                                                 soct = (string)jObjectData["so_benh_an"],
                                                 ngayhd = (string)jObjectData["inv_invoiceIssuedDate"],
                                                 tendv = (string)jObjectData["inv_buyerLegalName"],
                                                 makh = (string)jObjectData["ma_dt"],
                                                 tenng = (string)jObjectData["inv_buyerDisplayName"],
                                                 mst = (string)jObjectData["inv_buyerTaxCode"],
                                                 diachi = (string)jObjectData["inv_buyerAddressLine"],
                                                 tongtien = (string)jObjectData["inv_TotalAmount"]
                                                };

                                                listobj.Add(obj);
                                                LISTOBJECT.Add(jObjectMainData);
                                        /*
                                        var dataRequest = jObjectMainData.ToString();
                                        //Log.Debug(dataRequest);
                                        var url = BaseConfig.UrlSave;
                                        using (var scope = new TransactionScope())
                                        {
                                            try
                                            {
                                                var webClient = MinvoiceService.SetupWebClient();
                                                var result = webClient.UploadString(url, dataRequest);
                                                var resultResponse = JObject.Parse(result);
                                                if (resultResponse.ContainsKey("ok") && resultResponse.ContainsKey("data"))
                                                {
                                                    var jToken = resultResponse["data"];
                                                    //  DataContext.UpdateMisa(sqlConnectionMisa, refId, BaseConfig.TableInvocie, jToken);
                                                   //   if (BaseConfig.Version.Equals("2017"))
                                                    //  {
                                                   //       DataContext.UpdateMisaVoucher(sqlConnectionMisa, refId, BaseConfig.TableVoucher, BaseConfig.TableVoucherDetail, jToken);
                                                    //  }
                                                    scope.Complete();
                                                }
                                                else
                                                {
                                                    string sqlcmd2 = $" UPDATE B3009CT SET ID_HD = '' WHERE Stt = '{row["Stt"].ToString()}' ";
                                                    SqlCommand sqlCommand = new SqlCommand(sqlcmd2, sqlConnection)
                                                    {
                                                        CommandType = CommandType.Text
                                                    };
                                                    sqlCommand.ExecuteNonQuery();
                                                    JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);
                                                    string error = (string)jObject["error"];
                                                    if (error != "")
                                                    {
                                                        MessageBox.Show($"Tạo hóa đơn thất bại! Do { error} ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    }
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                XtraMessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }*/
                                    }

                                }
                            }
                            dataGridView1.DataSource = listobj;
                            sqlConnection.Close();
                        }
                        
                        else
                        {
                           
                                XtraMessageBox.Show("Không có dữ liệu hóa đơn từ ngày: " + tuNgay.SelectedText.ToString() + " đến ngày: " + denNgay.SelectedText.ToString() + " ", "Thông Báo", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            
                        }
                        if (ktra == false)
                        {
                            if (choose == 1 && TableInvoiceDate.Rows.Count > 0 && MinvoiceService.ck == true)
                            {
                                ktra = true;
                                XtraMessageBox.Show("1 số hóa đơn có thể bị thiếu, do đã tồn tại 1 số hóa đơn trên Minvoice! Hãy chọn update để cập nhật các hđ này! ", "Thông Báo", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                            }
                        }
                    }

                }
                else
                {
                    XtraMessageBox.Show("Bạn vui lòng chọn ngày hóa đơn đầy đủ!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
              //  notifyIcon1.ShowBalloonTip(5000, "Thông Báo", "Tạo hóa đơn thất bại!", ToolTipIcon.Error);
             //   XtraMessageBox.Show("Lấy dữ liệu thất bại", "Lỗi dòng " + cout + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ckAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i=0;i<=dataGridView1.RowCount-1;i++)
            {
                

                if (ckAll.Checked == true)
                {
                    dataGridView1.Rows[i].Cells[0].Value = true;
                }
                else
                {
                    dataGridView1.Rows[i].Cells[0].Value = false;
                }
            }
        }

 private void taohd_Click(object sender, EventArgs e)
        {
            click++;

            try
            {
                    var sqlConnection = MinvoiceService.GetSqlConnectionMisaTest();
                sqlConnection.Open();
                int choose = 1;
                notifyIcon1.ShowBalloonTip(5000, "Thông Báo", "Đang tạo hóa đơn!Vui lòng chờ!", ToolTipIcon.Info);
                for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                {

                
                    bool checkedCell = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);
                    if (checkedCell == true)
                    {

                       int dem = dataGridView1.Rows[i].Index;
                    

                        var dataRequest = LISTOBJECT[dem].ToString();
                        //Log.Debug(dataRequest);
                        var url = BaseConfig.UrlSave;
                        using (var scope = new TransactionScope())
                        {
                        
                            var webClient = MinvoiceService.SetupWebClient();
                            var result = webClient.UploadString(url, dataRequest);
                            var resultResponse = JObject.Parse(result);
                            if (resultResponse.ContainsKey("ok") && resultResponse.ContainsKey("data"))
                            {
                                string sqlcomman = $" UPDATE B3009CT SET ID_HD = '{listobj[dem].inv_invoiceauth_id}' WHERE Stt = '{listobj[dem].STT}' ";

                                SqlCommand sqlCommand = new SqlCommand(sqlcomman, sqlConnection)
                                {
                                    CommandType = CommandType.Text
                                };
                                sqlCommand.ExecuteNonQuery();
                                var jToken = resultResponse["data"];
                                //  DataContext.UpdateMisa(sqlConnectionMisa, refId, BaseConfig.TableInvocie, jToken);
                                //   if (BaseConfig.Version.Equals("2017"))
                                //  {
                                //       DataContext.UpdateMisaVoucher(sqlConnectionMisa, refId, BaseConfig.TableVoucher, BaseConfig.TableVoucherDetail, jToken);
                                //  }
                                scope.Complete();
                            }
                            else
                            {
                                if (click>=2)
                                {
                                    cx = true;
                                    JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);
                                    string error = (string)jObject["error"];
                                    if (error.Contains("inv_InvoiceAuth_id") || error.Contains("PRIMARY KEY"))
                                    {
                                        XtraMessageBox.Show("Lỗi trùng hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                    {

                                    
                                        MessageBox.Show($"Tạo hóa đơn thất bại! Do {error}! ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {


                                    cx = true;
                                    JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);
                                    string error = (string)jObject["error"];
                                    if (error.Contains("inv_InvoiceAuth_id") || error.Contains("PRIMARY KEY"))
                                    {
                                        XtraMessageBox.Show("Lỗi trùng hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                    {

                                        string sqlcmd2 = $" UPDATE B3009CT SET ID_HD = '' WHERE Stt = '{listobj[dem].STT}' ";
                                        SqlCommand sqlCommand = new SqlCommand(sqlcmd2, sqlConnection)
                                        {
                                            CommandType = CommandType.Text
                                        };
                                        sqlCommand.ExecuteNonQuery();
                                        MessageBox.Show($"Tạo hóa đơn thất bại! Do {error}! ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        
                        }
                    




                    }
                    else
                    {

                    }

                }

                if (choose == 1 && couttable > 0 && MinvoiceService.ck == false && cx ==false)
                {
                    XtraMessageBox.Show("Lấy dữ liệu hóa đơn thành công!", "Thông Báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                if (choose == 1 && couttable > 0 && MinvoiceService.ck == false && cx ==true)
                {
                    XtraMessageBox.Show("Đã xong !", "Thông Báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                notifyIcon1.ShowBalloonTip(5000, "Thông Báo", "Tạo hóa đơn thất bại!", ToolTipIcon.Error);
                XtraMessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnreset_Click(object sender, EventArgs e)
        {
           
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            tuNgay.EditValue = null;
            denNgay.EditValue = null;
            ckAll.Checked = false;
            dataGridView1.DataSource = null;
            MinvoiceService.ck = false;
            cx = false;
            click = 0;
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                simpleButton3.Enabled = true;
            }
            else
            {
                simpleButton3.Enabled = false;
            }
        }
    }
}
