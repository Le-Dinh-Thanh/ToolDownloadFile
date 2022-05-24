using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using toolVanDao.Config;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace toolVanDao.Services
{
    public class JsonConvert
    {

        public static string a;
        public static string b;
        public static string stt;
        public static JObject ConvertMasterObject(SqlConnection sqlConnection, DataRow row)
        {

            try
            {

                var jObject = new JObject();
                Guid inv_InvoiceAuth_id = Guid.NewGuid();
                a = inv_InvoiceAuth_id.ToString();

                string sqlcomman = $" UPDATE B3009CT SET ID_HD = '{a}' WHERE Stt = '{row["Stt"].ToString()}' ";

                SqlCommand sqlCommand = new SqlCommand(sqlcomman, sqlConnection)
                {
                    CommandType = CommandType.Text
                };
                sqlCommand.ExecuteNonQuery();

                jObject.Add("inv_InvoiceAuth_id", inv_InvoiceAuth_id);
                jObject.Add("inv_invoiceType", "01GTKT");

                jObject.Add("so_benh_an", row["So_Ct"].ToString());
               // jObject.Add("inv_InvoiceCode_id", "b3c7452b-f2c4-4c33-b590-da9e0f91c743");
                jObject.Add("inv_invoiceSeries", "HN/20E");
                jObject.Add("inv_invoiceNumber", "");
                jObject.Add("inv_invoiceName", "Hóa đơn giá trị gia tăng");
                jObject.Add("inv_invoiceIssuedDate", DateTime.Parse(row["Ngay_Ct"].ToString()).ToString("yyyy-MM-dd"));
                jObject.Add("inv_currencyCode", "VND");

                jObject.Add("inv_exchangeRate", 1);

                jObject.Add("inv_adjustmentType", 1);
                jObject.Add("inv_buyerDisplayName", !string.IsNullOrEmpty(row["Ong_Ba"].ToString())
                    ? row["Ong_Ba"].ToString()
                    : null);

                jObject.Add("ma_dt", !string.IsNullOrEmpty(row["Ma_Dt"].ToString())
                     ? row["Ma_Dt"].ToString()
                    : null);
                jObject.Add("inv_buyerLegalName", !string.IsNullOrEmpty(row["Ten_Dt"].ToString())
                     ? row["Ten_Dt"].ToString()
                    : null);

                jObject.Add("inv_buyerTaxCode", !string.IsNullOrEmpty(row["Ma_So_Thue"].ToString())
                     ? row["Ma_So_Thue"].ToString()
                    : null);

                jObject.Add("inv_buyerAddressLine", !string.IsNullOrEmpty(row["Dia_Chi"].ToString())
                     ? row["Dia_Chi"].ToString()
                    : null);

                jObject.Add("inv_buyerEmail", "");
                jObject.Add("inv_buyerBankAccount", "");
                jObject.Add("inv_buyerBankName", "");
                jObject.Add("inv_paymentMethodName", "Tiền Mặt/Chuyển Khoản");

                jObject.Add("inv_sellerBankAccount", "");
                jObject.Add("inv_sellerBankName", "");
                jObject.Add("trang_thai", "Chờ ký");
                jObject.Add("nguoi_ky", "");
                jObject.Add("sobaomat", "");
                jObject.Add("trang_thai_hd", 1);
                jObject.Add("in_chuyen_doi", false);
                jObject.Add("ngay_ky", null);
                jObject.Add("nguoi_in_cdoi", "");
                jObject.Add("ngay_in_cdoi", null);
                jObject.Add("mau_hd", BaseConfig.MauSo);
                jObject.Add("ma_ct", "HDDT");

                return jObject;

            }
            catch (Exception e)
            {
                MessageBox.Show("Xảy ra lỗi ở Đầu phiếu ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception(e.Message);
            }
        }


        public static JObject ConvertDetailJObjectSaInvoiceDetail(DataRow row)
        {

            try
            {


                var vatAmount = !string.IsNullOrEmpty(row["Tien_Thue"].ToString())
                    ? double.Parse(row["Tien_Thue"].ToString())
                    : 0;

                var discountAmount = 0;

                var totalAmountWithoutVat = !string.IsNullOrEmpty(row["Tien"].ToString())
                  ? double.Parse(row["Tien"].ToString())
                  : 0;

                var totalAmount = totalAmountWithoutVat + vatAmount - discountAmount;

                var jObject = new JObject();
                jObject.Add("inv_InvoiceAuthDetail_id", Guid.NewGuid());
                jObject.Add("inv_InvoiceAuth_id", a);
                jObject.Add("stt_rec0", "");
                jObject.Add("inv_itemCode", !string.IsNullOrEmpty(row["Ma_Vt"].ToString()) ? row["Ma_Vt"].ToString() : null);

               // if (row["So_khung"].ToString() !="" || row["So_May"].ToString() !="")
              //  {
                //    jObject.Add("inv_itemName", !string.IsNullOrEmpty(row["Ten_Vt"].ToString()) ? row["Ten_Vt"].ToString() + ". SK: " + row["So_khung"].ToString() + ". SM: " + row["So_May"].ToString() : null);
               // }
               // else
              //  {
                    jObject.Add("inv_itemName", !string.IsNullOrEmpty(row["Ten_Vt"].ToString()) ? row["Ten_Vt"].ToString() : null);
              //  }
               

                jObject.Add("inv_unitCode", "");
                jObject.Add("inv_unitName", "");
                jObject.Add("inv_unitPrice", !string.IsNullOrEmpty(row["Gia"].ToString()) ? CommonService.ConvertNumber2(row["Gia"].ToString()) : null);
                jObject.Add("inv_quantity", !string.IsNullOrEmpty(row["So_Luong"].ToString()) ? CommonService.ConvertNumber2(row["So_Luong"].ToString()) : null);

                jObject.Add(
                   "inv_TotalAmountWithoutVat",
                   !string.IsNullOrEmpty(row["Tien"].ToString()) ? CommonService.ConvertNumber2(row["Tien"].ToString()) : null);

                jObject.Add(
                    "inv_vatAmount",
                    !string.IsNullOrEmpty(row["Tien_Thue"].ToString()) ? CommonService.ConvertNumber2(row["Tien_Thue"].ToString()) : null);
                jObject.Add("inv_TotalAmount", CommonService.ConvertNumber2(totalAmount.ToString()));

                jObject.Add("inv_promotion", false);
                jObject.Add("inv_discountPercentage", null);
                jObject.Add("inv_discountAmount", 0);
                jObject.Add("ma_thue",
                   !string.IsNullOrEmpty(row["Thue_gtgt"].ToString())
                       ? double.Parse(row["Thue_gtgt"].ToString()).ToString("N0")
                       : null);

                return jObject;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Xảy ra lỗi ở Tên hàng:" + row["Ten_Vt"].ToString() + "--- Thứ tự:" + row["Stt"].ToString());
                throw new Exception(ex.Message);
            }
        }

        public static JObject ConvertData(SqlConnection sqlConnection, DataRow row)
        {
            int count = 0;
            try
            {
                var inv_TotalAmount = 0.0;
                var inv_TotalAmountWithoutVat = 0.0;
                var inv_vatAmount = 0.0;
                var inv_discountAmount = 0.0;
                count++;
                count++; var masterJObject = ConvertMasterObject(sqlConnection, row);
                count++; var so_ct = row["So_Ct"].ToString();
                count++; var so_seri = row["So_Seri"].ToString();
                count++; var ngay_ct = row["Ngay_Ct"].ToString();
                count++; var mst = row["Ma_So_Thue"].ToString();
                count++; var jArrayDetail = GetJArrayDetail(sqlConnection, so_ct, so_seri, ngay_ct, mst, out inv_TotalAmount, out inv_TotalAmountWithoutVat, out inv_vatAmount, out inv_discountAmount);
                count++; masterJObject.Add("inv_TotalAmount", inv_TotalAmount);
                count++; masterJObject.Add("inv_TotalAmountWithoutVat", inv_TotalAmountWithoutVat);
                count++; masterJObject.Add("inv_vatAmount", inv_vatAmount);
                count++; masterJObject.Add("inv_discountAmount", inv_discountAmount);

                count++; var data = new JObject
                {
                    {"tab_id", "TAB00188 "},
                    {"tab_table", "inv_InvoiceAuthDetail"},
                    {"data", jArrayDetail}
                };
                count++; var details = new JArray(data);
                count++; masterJObject.Add("details", details);
                return masterJObject;
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi convert dữ liệu! Dòng" + count + "", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception(e.Message);
            }
        }


        private static JArray GetJArrayDetail(SqlConnection sqlConnection, string so_ct, string so_seri, string ngay_ct, string mst, out double inv_TotalAmount, out double inv_TotalAmountWithoutVat, out double inv_vatAmount, out double inv_discountAmount)
        {
            inv_TotalAmount = 0.0;
            inv_TotalAmountWithoutVat = 0.0;
            inv_vatAmount = 0.0;
            inv_discountAmount = 0.0;

            var jArrayDetail = new JArray();
            int count = 0;

            //string whereVoucherDetail = $"WHERE SAInvoiceRefID = '{refId}' ORDER BY SortOrder";
            //var dataTableVoucherDetail =
            //    DataContext.GetDataTableTest(sqlConnection, BaseConfig.TableVoucherDetail, whereVoucherDetail);
            try
            {


                string whereInvoiceDetail = $" WHERE So_Ct = '{so_ct}' AND So_Seri = '{so_seri}' AND Ngay_Ct = '{Convert.ToDateTime(ngay_ct).ToString("yyyy-MM-dd")}' AND Ma_So_Thue = '{mst}' ";
                string sqlSelectInvoiceDetail = $" SELECT * FROM {BaseConfig.TableInvocie}";
                sqlSelectInvoiceDetail += whereInvoiceDetail;

                var dataTableInvoiceDetail = DataContext.GetDataTableTest(sqlConnection, sqlSelectInvoiceDetail);
                if (dataTableInvoiceDetail.Rows.Count > 0)

                {

                    foreach (DataRow dataRowInvoiceDetail in dataTableInvoiceDetail.Rows)
                    {
                        count++;
                        var detailJObject = ConvertDetailJObjectSaInvoiceDetail(dataRowInvoiceDetail);



                        if (detailJObject.ContainsKey("inv_TotalAmount"))
                        {
                            if (!string.IsNullOrEmpty(detailJObject["inv_TotalAmount"].ToString()))
                            {
                                inv_TotalAmount += (double)detailJObject["inv_TotalAmount"];
                            }
                        }


                        if (detailJObject.ContainsKey("inv_TotalAmountWithoutVat"))
                        {
                            if (!string.IsNullOrEmpty(detailJObject["inv_TotalAmountWithoutVat"].ToString()))
                            {
                                inv_TotalAmountWithoutVat += (double)detailJObject["inv_TotalAmountWithoutVat"];
                            }
                        }

                        if (detailJObject.ContainsKey("inv_vatAmount"))
                        {
                            if (!string.IsNullOrEmpty(detailJObject["inv_vatAmount"].ToString()))
                            {
                                inv_vatAmount += (double)detailJObject["inv_vatAmount"];
                            }
                        }

                        if (detailJObject.ContainsKey("inv_discountAmount"))
                        {
                            if (!string.IsNullOrEmpty(detailJObject["inv_discountAmount"].ToString()))
                            {
                                inv_discountAmount += (double)detailJObject["inv_discountAmount"];
                            }
                        }


                        jArrayDetail.Add(detailJObject);
                    }
                }



                //var whereInvoiceDetail = $"WHERE RefID = '{refId}' ORDER BY SortOrder";
                //var dataTableInvoiceDetail =
                //    DataContext.GetDataTableTest(sqlConnection, BaseConfig.TableInvoiceDetail, whereInvoiceDetail);



                return jArrayDetail;
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi convert dữ liệu! Dòng" + count + "", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception(e.Message);
            }
        }
        public static JObject ConvertDataUpdate(SqlConnection sqlConnection, DataRow row, string invNo)
        {
            try
            {
                var inv_TotalAmount = 0.0;
                var inv_TotalAmountWithoutVat = 0.0;
                var inv_vatAmount = 0.0;
                var inv_discountAmount = 0.0;

                var masterJObject = ConvertMasterObjectUpdate(sqlConnection, row, invNo);
                var so_ct = row["So_Ct"].ToString();
                var so_seri = row["So_Seri"].ToString();
                var ngay_ct = row["Ngay_Ct"].ToString();
                var mst = row["Ma_So_Thue"].ToString();
                var jArrayDetail = GetJArrayDetailUpdate(sqlConnection, so_ct, so_seri, ngay_ct, mst, out inv_TotalAmount, out inv_TotalAmountWithoutVat, out inv_vatAmount, out inv_discountAmount);
                masterJObject.Add("inv_TotalAmount", inv_TotalAmount);
                masterJObject.Add("inv_TotalAmountWithoutVat", inv_TotalAmountWithoutVat);
                masterJObject.Add("inv_vatAmount", inv_vatAmount);
                masterJObject.Add("inv_discountAmount", inv_discountAmount);

                var data = new JObject
                {
                    {"tab_id", "TAB00188 "},
                    {"tab_table", "inv_InvoiceAuthDetail"},
                    {"data", jArrayDetail}
                };
                var details = new JArray(data);
                masterJObject.Add("details", details);
                return masterJObject;
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi convert dữ liệu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception(e.Message);
            }
        }
        public static JObject ConvertMasterObjectUpdate(SqlConnection sqlConnection, DataRow row, string invNo)
        {

            try
            {

                var jObject = new JObject();
                b = row["ID_HD"].ToString();
                jObject.Add("inv_InvoiceAuth_id", row["ID_HD"].ToString());
                jObject.Add("inv_invoiceType", "01GTKT");

                jObject.Add("so_benh_an", row["So_Ct"].ToString());
                //  jObject.Add("inv_InvoiceCode_id", "");
                jObject.Add("inv_invoiceSeries", "HN/20E");
                jObject.Add("inv_invoiceNumber", invNo.ToString());
                jObject.Add("inv_invoiceName", "Hóa đơn giá trị gia tăng");
                jObject.Add("inv_invoiceIssuedDate", DateTime.Parse(row["Ngay_Ct"].ToString()).ToString("yyyy-MM-dd"));
                jObject.Add("inv_currencyCode", "VND");

                jObject.Add("inv_exchangeRate", 1);

                jObject.Add("inv_adjustmentType", 1);
                jObject.Add("inv_buyerDisplayName", !string.IsNullOrEmpty(row["Ong_Ba"].ToString())
                 ? row["Ong_Ba"].ToString()
                    : null);

                jObject.Add("ma_dt", !string.IsNullOrEmpty(row["Ma_Dt"].ToString())
                     ? row["Ma_Dt"].ToString()
                    : null);
                jObject.Add("inv_buyerLegalName", !string.IsNullOrEmpty(row["Ten_Dt"].ToString())
                     ? row["Ten_Dt"].ToString()
                    : null);

                jObject.Add("inv_buyerTaxCode", !string.IsNullOrEmpty(row["Ma_So_Thue"].ToString())
                     ? row["Ma_So_Thue"].ToString()
                    : null);

                jObject.Add("inv_buyerAddressLine", !string.IsNullOrEmpty(row["Dia_Chi"].ToString())
                     ? row["Dia_Chi"].ToString()
                    : null);

                jObject.Add("inv_buyerEmail", "");
                jObject.Add("inv_buyerBankAccount", "");
                jObject.Add("inv_buyerBankName", "");
                jObject.Add("inv_paymentMethodName", "Tiền Mặt/Chuyển Khoản");

                jObject.Add("inv_sellerBankAccount", "");
                jObject.Add("inv_sellerBankName", "");
                jObject.Add("trang_thai", "Chờ ký");
                jObject.Add("nguoi_ky", "");
                jObject.Add("sobaomat", "");
                jObject.Add("trang_thai_hd", 1);
                jObject.Add("in_chuyen_doi", false);
                jObject.Add("ngay_ky", null);
                jObject.Add("nguoi_in_cdoi", "");
                jObject.Add("ngay_in_cdoi", null);
                jObject.Add("mau_hd", BaseConfig.MauSo);
                jObject.Add("ma_ct", "HDDT");

                return jObject;

            }
            catch (Exception e)
            {
                MessageBox.Show("Xảy ra lỗi ở Đầu phiếu ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception(e.Message);
            }
        }
        private static JArray GetJArrayDetailUpdate(SqlConnection sqlConnection, string so_ct, string so_seri, string ngay_ct, string mst, out double inv_TotalAmount, out double inv_TotalAmountWithoutVat, out double inv_vatAmount, out double inv_discountAmount)
        {
            inv_TotalAmount = 0.0;
            inv_TotalAmountWithoutVat = 0.0;
            inv_vatAmount = 0.0;
            inv_discountAmount = 0.0;

            var jArrayDetail = new JArray();


            //string whereVoucherDetail = $"WHERE SAInvoiceRefID = '{refId}' ORDER BY SortOrder";
            //var dataTableVoucherDetail =
            //    DataContext.GetDataTableTest(sqlConnection, BaseConfig.TableVoucherDetail, whereVoucherDetail);

            string whereInvoiceDetail = $" WHERE So_Ct = '{so_ct}' AND So_Seri = '{so_seri}' AND Ngay_Ct = '{Convert.ToDateTime(ngay_ct).ToString("yyyy-MM-dd")}' AND Ma_So_Thue = '{mst}' ";
            string sqlSelectInvoiceDetail = $" SELECT * FROM {BaseConfig.TableInvocie}";
            sqlSelectInvoiceDetail += whereInvoiceDetail;

            var dataTableInvoiceDetail = DataContext.GetDataTableTest(sqlConnection, sqlSelectInvoiceDetail);
            if (dataTableInvoiceDetail.Rows.Count > 0)
            {
                foreach (DataRow dataRowInvoiceDetail in dataTableInvoiceDetail.Rows)
                {
                    var detailJObject = ConvertDetailJObjectSaInvoiceDetailUpdate(dataRowInvoiceDetail);
                    inv_TotalAmount += (double)detailJObject["inv_TotalAmount"];
                    inv_TotalAmountWithoutVat += (double)detailJObject["inv_TotalAmountWithoutVat"];
                    inv_vatAmount += (double)detailJObject["inv_vatAmount"];
                    inv_discountAmount += (double)detailJObject["inv_discountAmount"];
                    jArrayDetail.Add(detailJObject);
                }
            }



            //var whereInvoiceDetail = $"WHERE RefID = '{refId}' ORDER BY SortOrder";
            //var dataTableInvoiceDetail =
            //    DataContext.GetDataTableTest(sqlConnection, BaseConfig.TableInvoiceDetail, whereInvoiceDetail);



            return jArrayDetail;
        }
        public static JObject ConvertDetailJObjectSaInvoiceDetailUpdate(DataRow row)
        {
            try
            {
                var vatAmount = !string.IsNullOrEmpty(row["Tien_Thue"].ToString())
                    ? double.Parse(row["Tien_Thue"].ToString())
                    : 0;

                var discountAmount = 0;

                var totalAmountWithoutVat = !string.IsNullOrEmpty(row["Tien"].ToString())
                    ? double.Parse(row["Tien"].ToString())
                    : 0;

                var totalAmount = totalAmountWithoutVat + vatAmount - discountAmount;
                var jObject = new JObject();
                //   jObject.Add("inv_InvoiceAuthDetail_id", Guid.NewGuid());
                jObject.Add("inv_InvoiceAuth_id", b);
                jObject.Add("stt_rec0", "");
                jObject.Add("inv_itemCode", !string.IsNullOrEmpty(row["Ma_Vt"].ToString()) ? row["Ma_Vt"].ToString() : null);

                //if (row["So_khung"].ToString() != "" || row["So_May"].ToString() != "")
             //   {
              //      jObject.Add("inv_itemName", !string.IsNullOrEmpty(row["Ten_Vt"].ToString()) ? row["Ten_Vt"].ToString() + ". SK: " + row["So_khung"].ToString() + ". SM: " + row["So_May"].ToString() : null);
              //  }
               // else
              //  {
                    jObject.Add("inv_itemName", !string.IsNullOrEmpty(row["Ten_Vt"].ToString()) ? row["Ten_Vt"].ToString() : null);
            //    }
            
                jObject.Add("inv_unitCode", "");
                jObject.Add("inv_unitName", "");

                jObject.Add("inv_unitPrice", !string.IsNullOrEmpty(row["Gia"].ToString()) ? CommonService.ConvertNumber2(row["Gia"].ToString()) : null);
                jObject.Add("inv_quantity", !string.IsNullOrEmpty(row["So_Luong"].ToString()) ? CommonService.ConvertNumber2(row["So_Luong"].ToString()) : null);

                jObject.Add(
                   "inv_TotalAmountWithoutVat",
                   !string.IsNullOrEmpty(row["Tien"].ToString()) ? CommonService.ConvertNumber2(row["Tien"].ToString()) : null);

                jObject.Add(
                    "inv_vatAmount",
                    !string.IsNullOrEmpty(row["Tien_Thue"].ToString()) ? CommonService.ConvertNumber2(row["Tien_Thue"].ToString()) : null);
                jObject.Add("inv_TotalAmount", CommonService.ConvertNumber2(totalAmount.ToString()));


                jObject.Add("inv_promotion", false);
                jObject.Add("inv_discountPercentage", null);
                jObject.Add("inv_discountAmount", 0);
                jObject.Add(
                   "ma_thue",
                   !string.IsNullOrEmpty(row["Thue_gtgt"].ToString())
                       ? double.Parse(row["Thue_gtgt"].ToString()).ToString("N0")
                       : null);


                return jObject;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xảy ra lỗi ở Tên hàng:" + row["Ten_Vt"].ToString() + "--- Thứ tự:" + row["Stt"].ToString());
                throw new Exception(ex.Message);
            }
        }
    }
}
