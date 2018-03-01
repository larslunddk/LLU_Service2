using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace LLU_Service2
{
    public static class Library
    {
        
        public static string ValidateSQLContent()
        {
            string ret = "";
            string sqlConnectionString = "Server= localhost; Database= LLUTEST; Integrated Security = SSPI; ";
            SqlConnection cnn = new SqlConnection(sqlConnectionString);
            SqlCommand sqlCommand;
            SqlDataReader sqlDatareader;
            try
            {
                cnn.Open();
                string sql = "select count(*) from CU8";
                sqlCommand = new SqlCommand(sql, cnn);
                sqlDatareader = sqlCommand.ExecuteReader();
                while (sqlDatareader.Read())
                {
                    string CountStr = sqlDatareader.GetValue(0).ToString();
                    Int64 CountInt = sqlDatareader.GetInt64(0);
                    Int64 CountExpected = 136276;
                    if (CountInt!= CountExpected)
                    {
                        writeErrorLog(string.Format("Records {0} --> not equal to {1} (SQLConnectionstring:{2})",CountInt, CountExpected, sqlConnectionString));
                        ret = string.Format("Error counting  Records {0} --> not equal to {1} (SQLConnectionstring:{2})", CountInt, CountExpected, sqlConnectionString);
                    }
                    else
                    {
                        ret = string.Format("All good (SQLConnectionstring:{0})", sqlConnectionString);
                    }
                }
                sqlDatareader.Close();
                sqlCommand.Dispose();
                
                cnn.Close();
            }
            catch (Exception ex)
            {
                writeErrorLog("Can not open connection ! (Connectionstring:" + sqlConnectionString+")");
            }

            return ret;
        }
        public static DateTime ReadEventlog(string[] args, DateTime LastRead)
        {
            DateTime ret = DateTime.Now;
            EventLog[] eventLogs = EventLog.GetEventLogs();
            foreach (EventLog e in eventLogs)
            {
                foreach (EventLogEntry Entry in e.Entries)
                {
                    writeErrorLog(Entry.TimeWritten + "; " + Entry.Source + "; " + Entry.Message);
                    if (Entry.TimeWritten > LastRead)
                    {
                        break;
                    }
                    ret = Entry.TimeWritten;
                }
            }

            return ret;
        }
        public static void writeErrorLog(string message)
        {
            StreamWriter sw;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Logfile.txt", true);
                //sw.WriteLine(DateTime.Now.ToString() + ": " + ex.Source.ToString().Trim() + "; " + ex.Message.ToString().Trim());
                sw.WriteLine(DateTime.Now.ToString() + ": " + message);
                sw.Flush();
                sw.Close();
            }
            catch { }
        }
    }
}
