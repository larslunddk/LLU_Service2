using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLU_Service2
{
    class LibrarySQL_WorkInProgress
    {
        public SqlConnection LocalExecuteSQL_OpenConnection(string sqlConnectionString, string sql)
        {
            SqlConnection cnn = new SqlConnection(sqlConnectionString);
            try
            {
                cnn.Open();
            }
            catch (Exception ex)
            {
                //$"Can (most likely) not open connection ! (Connectionstring: {sqlConnectionString})";
            }
            return cnn;
        }
        public static SqlConnection NOTFINISHED_LocalExecuteSQL_Execute1Line(SqlConnection cnn, string sql)
        {
            string[] ret = new string[10000];
            string rowline = null;
            SqlCommand sqlCommand;
            SqlDataReader sqlDatareader;

            try
            {
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
                        for (col = 0; col <= sqlDatareader.FieldCount; col++)
                        {
                            line[col] = sqlDatareader.GetValue(col).ToString();
                            rowline += sqlDatareader.GetValue(col).ToString() + ";";
                        }
                        rowline = rowline.Remove(rowline.Length, 1);
                    }
                }
                ret[row] = rowline;
                //Console.WriteLine = string.Format("SQL: {0}", rowline);

                sqlDatareader.Close();
                sqlCommand.Dispose();
            }
            catch (Exception ex)
            {
                //$"Can (most likely) not open connection ! (Connectionstring: {sqlConnectionString})";
            }
            return cnn;
        }
    }
}
