using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLU_Service2
{
    public class LibrarySQL
    {
        public static string ShowActiveStatements_sql()
        {
            string sqlCommand = @"SELECT sqltext.TEXT,
req.session_id,
req.status,
req.command,
req.cpu_time,
req.total_elapsed_time
FROM sys.dm_exec_requests req
CROSS APPLY sys.dm_exec_sql_text(sql_handle) AS sqltext 
";

  /*          string sqlCommand = @"SELECT [spid] = session_Id
	                , recid
	                , [blockedBy] = blocking_session_id 
	                , [database] = DB_NAME(sp.dbid)
	                , [user] = nt_username
	                , [status] = er.status
	                , [wait] = wait_type
	                , [current stmt] = 
		                SUBSTRING(
                            qt.text,
                            er.statement_start_offset/2,
                            (CASE
                                WHEN er.statement_end_offset = -1 THEN DATALENGTH(qt.text)
                                ELSE er.statement_end_offset
                            END - er.statement_start_offset)/2)
	                ,[current batch] = qt.text
	                , reads
	                , logical_reads
	                , cpu
	                , [time elapsed(ms)] = DATEDIFF(mi, start_time, getdate())
	                , program = program_name
	                , hostname
	                --, nt_domain
	                , start_time
	                , qt.objectid
                FROM sys.dm_exec_requests er
                INNER JOIN sys.sysprocesses sp ON er.session_id = sp.spid
                CROSS APPLY sys.dm_exec_sql_text(er.sql_handle)as qt
                WHERE session_Id > 50              -- Ignore system spids.
                AND session_Id NOT IN (@@SPID)     -- Ignore this current statement.
                ORDER BY 1, 2
                GO";
                */
                return sqlCommand;            
        }
        public static void ShowActiveStatements_exec()
        {
            string[] header = { "spid", "ecid", "BlockedBy", "Database", "user", "status", "wait", "currentStatement", "currentBatch", "reads", "Logical reads", "CPU", "Time elapsed (ms)", "Program", "Hostname", "Start time", "ObjectId" };
            
            string[] ret = LocalExecuteSQL_Single(ConnectionStrFromAppConfig(), ShowActiveStatements_sql());
            foreach (string line in ret)
            {
                if (line != null)
                {
                    Library.WriteErrorLog(line);
                }
            }

        }



        public static string[] LocalExecuteSQL_Single(string sqlConnectionString, string sql)
        {
            string[] ret = new string[1000];
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
                if(sqlDatareader.HasRows)
                { 
                    while (sqlDatareader.Read())
                    {
                        string[] line = new string[100];

                        rowline = "";
                        int col;
                        for (col = 0; col < sqlDatareader.FieldCount; col++)
                        {
                            String FieldHeader = sqlDatareader.GetName(col);
                            line[col] = sqlDatareader.GetValue(col).ToString();
                            //rowline += FieldHeader+":"+sqlDatareader.GetValue(col).ToString() + ";";
                            rowline += sqlDatareader.GetValue(col).ToString() + ";";
                        }
                        if (rowline.Substring(rowline.Length) == ";")
                        {
                            rowline = rowline.Remove(rowline.Length - 1, 1);
                        }
                    }
                }

                ret[row] = rowline;
                //Console.WriteLine = string.Format("SQL: {0}", rowline);

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
        public static String ConnectionStrFromAppConfig()
        {
            Boolean _IntegratedSecurity = false;

            string _IntegratedSecurityString = ConfigurationManager.AppSettings["IntegratedSecurity"];
            Boolean.TryParse(_IntegratedSecurityString, out _IntegratedSecurity);

            var cb = new SqlConnectionStringBuilder
            {
                DataSource = ConfigurationManager.AppSettings["Datasource"],
                InitialCatalog = ConfigurationManager.AppSettings["InitialCatalog"]
            };

            if (_IntegratedSecurity)
            {
                cb.IntegratedSecurity = _IntegratedSecurity;
            }
            else
            {
                cb.UserID = ConfigurationManager.AppSettings["UserId"];
                cb.Password = ConfigurationManager.AppSettings["Password"];
            }
            return cb.ConnectionString;

        }
        public static void ExecSQLfromAppConfig()
        {
            string sql = ConfigurationManager.AppSettings["Sql"];
            string sqlHeader = ConfigurationManager.AppSettings["SqlHeader"];
            
            string[] ret = LocalExecuteSQL_Single(ConnectionStrFromAppConfig(), sql);
            foreach (string line in ret)
            {
                Boolean sqlLogged = true;
                if (line != null)
                {
                    //if (line.Contains(sql))
                    //    sqlLogged = false;
                    if (sqlLogged)
                    {
                        Library.WriteErrorLog(Library.trimString(line), sqlHeader);
                    }
                }
            }

        }

        public static void SQL_CheckLocks()
        {
            string sqlConnectionString = "Server=localhost; Database=axdb_LLU; User Id=LLU_LocalDBAccess; Password=zzz";
            string sql;
            string[] ret;

            sql = @"DECLARE @Table TABLE(
                            SPID INT,
                            Status VARCHAR(MAX),
                            LOGIN VARCHAR(MAX),
                            HostName VARCHAR(MAX),
                            BlkBy VARCHAR(MAX),
                            DBName VARCHAR(MAX),
                            Command VARCHAR(MAX),
                            CPUTime INT,
                            DiskIO INT,
                            LastBatch VARCHAR(MAX),
                            ProgramName VARCHAR(MAX),
                            SPID_1 INT,
                            REQUESTID INT
                            )";
            /*ret = LocalExecuteSQL_Execute1Line(new LocalExecuteSQL_OpenConnection(sqlConnectionString), sql);

            sql = @"INSERT INTO @Table EXEC sp_who2";
            ret = LocalExecuteSQL(sqlConnectionString, sql);

            sql = @"SELECT * FROM @Table WHERE REPLACE(BlkBy, ' ', '')<>'.'";
            ret = LocalExecuteSQL(sqlConnectionString, sql);
            */
        }
    }
}
