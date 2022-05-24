using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toolVanDao.Config
{
    public class BaseConfig
    {
        public static bool ReturnInfoCustomer = false;


        public static string TableInvocie = Properties.Settings.Default.TableInvocie.Trim();
        public static string ConnectionString = Properties.Settings.Default.ConnectionString.Trim();
        public static string UsernameLoginWeb = Properties.Settings.Default.UsernameLoginWeb.Trim();

        public static string PasswordLoginWeb = Properties.Settings.Default.PasswordLoginWeb.Trim();

        public static string UrlLogin = Properties.Settings.Default.UrlLogin.Trim();
        public static string UrlSave = Properties.Settings.Default.UrlSave.Trim();


        public static string UrlGetInvoiceByInvoiceAuthId = Properties.Settings.Default.UrlGetInvoiceByInvoiceAuthId.Trim();

        public static string KyHieu = Properties.Settings.Default.KyHieu.Trim();
        public static string MauSo = Properties.Settings.Default.MauSo.Trim();

        public static string ky_hieu = Properties.Settings.Default.ky_hieu.Trim();
        public static string mau_so = Properties.Settings.Default.mau_so.Trim();

        public static string Command = Properties.Settings.Default.Command.Trim();
        public static string MST = Properties.Settings.Default.MST.Trim();
        public static string UrlRef = Properties.Settings.Default.UrlRef.Trim();
        public static string UrlRef78 = Properties.Settings.Default.UrlRef78.Trim();
        public static string UrlSBM = Properties.Settings.Default.UrlSBM.Trim();
        public static string UrlDownloadXML = Properties.Settings.Default.UrlDownloadXML.Trim();
        public static string UrlDownloadPDF = Properties.Settings.Default.UrlDownloadPDF.Trim();
        public static string UrlDownloadXml78 = Properties.Settings.Default.UrlDownloadXml78.Trim();
        public static string UrlDownloadpdf78 = Properties.Settings.Default.UrlDownloadpdf78.Trim();

        public static string UrlCommand = Properties.Settings.Default.UrlCommand.Trim();

        public static string svname = Properties.Settings.Default.svname.Trim();
        public static int authen = Properties.Settings.Default.authen;
        public static string txtusername = Properties.Settings.Default.txtusername.Trim();
        public static string txtpassword = Properties.Settings.Default.txtpassword.Trim();
    }
}
