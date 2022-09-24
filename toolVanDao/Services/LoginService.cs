using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net;
using toolVanDao.Config;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace toolVanDao.Services
{
    public class LoginService
    {
        private static JObject Login(string username, string password)
        {
            var client = new WebClient
            {
                Encoding = Encoding.UTF8
            };
            client.Headers.Add("Content-Type", "application/json; charset=utf-8");
            JObject json = new JObject
            {
                {"username",username },
                {"password",password },
                {"ma_dvcs","VP" }
            };

            var urlLogin = Properties.Settings.Default.UrlLogin;
            var token = client.UploadString(urlLogin, json.ToString());
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }
            return JObject.Parse(token);
        }

        public static void CreateAuthorization(WebClient webClient, string username, string pass)
        {
            try
            {
                var tokenJson = Login(username, pass);
                if (tokenJson != null)
                {
                    var authorization = "Bear " + tokenJson["token"] + ";VP";
                    webClient.Headers[HttpRequestHeader.Authorization] = authorization;
                }

            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
