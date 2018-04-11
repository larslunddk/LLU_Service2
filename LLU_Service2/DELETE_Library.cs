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
        
        public static DateTime ReadEventlog(string[] args, DateTime LastRead)
        {
            DateTime ret = DateTime.Now;
            EventLog[] eventLogs = EventLog.GetEventLogs();
            foreach (EventLog e in eventLogs)
            {
                foreach (EventLogEntry Entry in e.Entries)
                {
                    WriteErrorLog(Entry.TimeWritten + "; " + Entry.Source + "; " + Entry.Message);
                    if (Entry.TimeWritten > LastRead)
                    {
                        break;
                    }
                    ret = Entry.TimeWritten;
                }
            }

            return ret;
        }
        public static void WriteErrorLog(string message)
        {
            StreamWriter sw;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + String.Format("\\Logfile_{0:yyyyMMdd_HH}.txt",DateTime.Now), true);
                //sw.WriteLine(DateTime.Now.ToString() + ": " + ex.Source.ToString().Trim() + "; " + ex.Message.ToString().Trim());
                sw.WriteLine(DateTime.Now.ToString() + ": " + message);
                sw.Flush();
                sw.Close();
            }
            catch { }
        }
    }
}
