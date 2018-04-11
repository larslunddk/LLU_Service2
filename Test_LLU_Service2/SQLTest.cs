using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_LLU_Service2
{
    class SQLTest
    {
        public static string[] LocalExecuteSQL_Single(string sqlConnectionString, string sql)
        {
            string[] ret = new string[10000];
            string rowline = null;
            SqlConnection cnn = new SqlConnection(sqlConnectionString);
            SqlCommand sqlCommand;
            SqlDataReader sqlDatareader;
            try
            {
                cnn.Open();
                sqlCommand = new SqlCommand(sql, cnn);
                sqlDatareader = sqlCommand.ExecuteReader();
                int row = 0;
                if (sqlDatareader.HasRows)
                {
                    while (sqlDatareader.Read())
                    {
                        string[] line = new string[100];

                        rowline = "";
                        int col;
                        for (col = 0; col < sqlDatareader.FieldCount; col++)
                        {
                            line[col] = sqlDatareader.GetValue(col).ToString();
                            rowline += sqlDatareader.GetValue(col).ToString() + ";";
                        }
                        rowline = rowline.Remove(rowline.Length-1, 1);
                    }
                }
                ret[row] = rowline;
                sqlDatareader.Close();
                sqlCommand.Dispose();

                cnn.Close();
            }
            catch (Exception ex)
            {
                ret = null;
                ret[0] = $"Can (most likely) not open connection ! (Connectionstring: {sqlConnectionString})";
                return ret;
            }
            return ret;
        }
    }
}
