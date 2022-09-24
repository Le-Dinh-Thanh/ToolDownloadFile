using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using toolVanDao.Services;
//using ClosedXML.Excel;
//using Syncfusion.XlsIO;

namespace toolVanDao
{
    public partial class Form2 : Form
    {
        private string _connectionString = "";
        private string _connectionString1 = "";
        private SqlConnection _sqlConnection;
        public SqlConnection sqlConnection;
        public string ten;
        public int i = 0;
        List<KH> lstKQ;
        public bool CheckConnectionString = false;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        public class KH
        {
            public string CKS { get; set; }
            public string MST { get; set; }

            public DateTime ngayhethan { get; set; }
        }
        public class ServerName
        {
            public string Name { get; set; }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            List<string> listBox1 = new List<string> {"103.145.62.137,38394","123.31.12.39,6601","123.31.12.39,6600","103.75.184.248,8807","103.75.184.248,8812","103.57.221.159,1002","103.75.184.248,8809","123.31.12.39,1111","123.31.17.91,8804","103.75.184.248,8810","123.31.17.91,8802","123.31.12.39,6604","103.145.62.137,38569","123.31.12.39,38565","103.75.184.248,8811","123.31.12.39,6607","123.31.12.39,6605","103.145.62.137,38383","103.145.62.137,38393","123.31.17.91,8806","123.31.12.39,9901","103.57.221.159,1003","103.145.62.137,38392" ,"123.31.17.91,8813", "123.31.17.91,8801", "103.145.62.137,38390", "103.75.184.248,8808", "103.145.62.137,38565", "123.31.12.39,38562", "123.31.17.91,8814", "103.57.221.159,1001", "123.31.12.39,1433", "123.31.12.39,38563", "123.31.12.39,6608", "123.31.12.39,9900", "103.145.62.137,38391", "123.31.17.91,8817", "123.31.12.39,38564", "123.31.12.39,6606", "123.31.12.39,6609", "103.145.62.137,38389", "123.31.12.39,6611", "123.31.17.91,8816", "123.31.12.39,6610", "103.57.221.159,1000", "123.31.17.91,8803", "103.145.62.137,38565", "123.31.17.91,8800", "123.31.12.39,6602", "123.31.17.91,8818", "103.145.62.137,38395", "123.31.12.39,6603", "123.31.17.91,1111", "123.31.17.91,8815", "123.31.17.91,8805", "103.145.62.137,38568", "103.145.62.137,38396" };
            //_connectionString = $"Server={serverName};"; _connectionString += $"User Id={userName}; Password={passWord}; ";
            foreach (string s in listBox1)
            {
                try
                {
                    ten = s.ToString();
                    Console.WriteLine(s.ToString());
                    _connectionString = $"Server={s.ToString()}; User Id=sa; Password = 123@Hoadon!@#; ";
                    // _connectionString = $"Server={s.ToString()}; Database={database}; User Id=sa; Password = 123@Hoadon!@#; ";
                    _sqlConnection = new SqlConnection(_connectionString);
                    _sqlConnection.Open();

                    string command = "SELECT name from sys.databases";
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command, _sqlConnection);
                    DataTable table = new DataTable();
                    sqlDataAdapter.Fill(table);

                    List<ServerName> lstServerName = GetListDatabaseName(table);
                    lstKQ = new List<KH>();
                    foreach (ServerName name in lstServerName)
                    {

                        try
                        {


                            _connectionString1 = $"Server={s.ToString()}; Database={name.Name.ToString()}; User Id=sa; Password = 123@Hoadon!@#; ";
                            sqlConnection = new SqlConnection(_connectionString1);
                            sqlConnection.Open();
                            string sqlcmd = $" SELECT * FROM chukyso";
                            var TableInvoiceDate = Services.DataContext.GetDataTableTest(sqlConnection, sqlcmd);

                            if (TableInvoiceDate.Rows.Count > 0)
                            {
                                foreach (DataRow row in TableInvoiceDate.Rows)
                                {
                                    if (row["ngayketthuc"].ToString() != "")
                                    {


                                        if ((DateTime.Parse(row["ngayketthuc"].ToString()) - DateTime.Now).TotalDays <= 30 && (DateTime.Parse(row["ngayketthuc"].ToString()) - DateTime.Now).TotalDays >= 0)
                                        {
                                            lstKQ.Add(new KH
                                            {
                                                CKS = row["subjectname"].ToString(),
                                                MST = name.Name.ToString().Replace("HDT_", ""),
                                                ngayhethan = DateTime.Parse(row["ngayketthuc"].ToString())
                                            }

                                               );
                                        }
                                    }
                                }
                                //lokDatabaseName.Properties.DataSource = lstServerName;
                                //lokDatabaseName.Properties.DisplayMember = "Name";
                                //lokDatabaseName.Properties.ValueMember = "Name";
                            }
                            sqlConnection.Close();
                        }
                        catch (Exception ex)
                        {
                            sqlConnection.Close();
                            // MessageBox.Show(ex.ToString(),"Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }

                    }

                    //var workbook = new XLWorkbook();
                    //workbook.AddWorksheet("sheetName");
                    //var ws = workbook.Worksheet("sheetName");
                    ////Recorrer el objecto
                    //int row1 = 2;
                    //ws.Cells("A" + "1").Value = "CKS";
                    //ws.Cells("B" + "1").Value = "MST";
                    //ws.Cells("C" + "1").Value = "Ngày hết hạn";
                    //foreach (var c in lstKQ)
                    //{

                    //    //Escribrie en Excel en cada celda
                    //    ws.Cell("A" + row1.ToString()).Value = c.CKS;
                    //    ws.Cell("B" + row1.ToString()).Value = c.MST;
                    //    ws.Cell("C" + row1.ToString()).Value = c.ngayhethan;

                    //    row1++;

                    //}

                    string k = $"HetHan_CKS" + i.ToString() + ".xlsx";
                    //Guardar Excel 
                    //Ruta = Nombre_Proyecto\bin\Debug
                    //workbook.SaveAs(@"C:\Users\Quang Huy\Desktop\hETHAN_cks\" + k);
                    i++;
                    _sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    _sqlConnection.Close();
                    if (ex.ToString().Contains("Login failed"))
                    {
                        Console.WriteLine("Không kết nối bằng user sa được!! {0}", ten.ToString());
                    }
                    //OnQueryContinueDrag;
                }
            }




        }
        private List<ServerName> GetListDatabaseName(DataTable table)
        {
            List<ServerName> lstServerName = new List<ServerName>();
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    lstServerName.Add(new ServerName
                    {
                        Name = row["name"].ToString()

                    });
                }
            }
            return lstServerName;
        }

    }
}
