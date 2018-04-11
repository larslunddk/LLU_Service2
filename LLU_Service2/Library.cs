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
        
        public static DateTime ReadEventlog(DateTime LastRead)
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
        
        public static string trimString(string txt)
        {
            txt = System.Text.RegularExpressions.Regex.Replace(txt, @"\r\n+", " ");
            while (txt.IndexOf("  ", 0) > 0)
            {
                txt = System.Text.RegularExpressions.Regex.Replace(txt, "  ", " ");
            }
            return txt;

        }
        public static void WriteErrorLog(string message, string header = "")
        {
            StreamWriter sw;
            try
            {
                string filename = AppDomain.CurrentDomain.BaseDirectory + String.Format("Logfile_{0:yyyyMMdd_HH}.txt", DateTime.Now);
                if (!File.Exists(filename) && header!="")
                {
                    sw = new StreamWriter(filename, true);
                    sw.WriteLine(header);
                    sw.WriteLine(DateTime.Now.ToString() + ": " + message);
                }
                else
                {
                    sw = new StreamWriter(filename, true);
                    sw.WriteLine(DateTime.Now.ToString() + ": " + message);
                }
                sw.Flush();
                sw.Close();
            }
            catch { }
        }
    }
}
