using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using toolVanDao.Config;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;

namespace toolVanDao.Services
{
    public class DataContext
    {
        public static SqlConnection GetDbConnection(string dataSource, string initialCatalog, string userId,
           string password)
        {
            string connectionString =
                $@"Data Source={dataSource};Initial Catalog={initialCatalog};User ID={userId};Password={password};Connection Timeout=999; MultipleActiveResultSets=True";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            return sqlConnection;
        }
        public static DataTable GetDataTableTest(SqlConnection sqlConnection, string tableName, string where = "")
        {
            DataTable dataTable = new DataTable();
            try
            {
                string commandText = $"SELECT * FROM {tableName} {where}";
                SqlDataAdapter adapter = new SqlDataAdapter(commandText, sqlConnection);
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception e)
            {
                throw new DataException(e.ToString());
            }
        }

        public static void UpdateMisa(SqlConnection sqlConnection, string refId, string tableName, JToken jObject)
        {
            try
            {
                string commandText = $"UPDATE {tableName} SET InvSeries = '{jObject["inv_invoiceSeries"]}', InvNo = '{jObject["inv_invoiceNumber"]}', IsInvoice = 1 WHERE RefID = '{refId}'";

              //  Log.Debug(commandText);
                SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection)
                {
                    CommandType = CommandType.Text
                };
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    /*    public static void UpdateMisaVoucher(SqlConnection sqlConnection, string refId, string tableVoucherName, string tableVoucherDetailName, JToken jObject)
        {
            try
            {
                if (BaseConfig.Version.Equals("2017"))
                {
                    string commandText = $"UPDATE {tableVoucherName} SET InvSeries = '{jObject["inv_invoiceSeries"]}', InvNo = '{jObject["inv_invoiceNumber"]}' WHERE RefID IN (SELECT RefID FROM {tableVoucherDetailName} WHERE SAInvoiceRefID = '{refId}')";

                    Log.Debug(commandText);
                    SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection)
                    {
                        CommandType = CommandType.Text
                    };
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }*/

        public static DataTable GetDataTableTest(SqlConnection sqlConnection, string sql)
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlConnection);
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception e)
            {
                throw new DataException(e.ToString());
            }
        }
    }
}
