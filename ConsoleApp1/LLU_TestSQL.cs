﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class LLU_TestSQL
    {
        static void Main()
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
                    if (CountInt != CountExpected)
                    {
                        
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
                ret = string.Format("Can not open connection ! (Connectionstring: {0})", sqlConnectionString);
            }
            Console.WriteLine(ret);
            Console.ReadLine();

        }
    }
}