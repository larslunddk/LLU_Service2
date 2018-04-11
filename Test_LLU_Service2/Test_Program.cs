using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using LLU_Service2;

namespace Test_LLU_Service2
{
    class Test_Program
    {
        static void Main(string[] args)
        {
            LibrarySQL.ExecSQLfromAppConfig();
            Console.WriteLine("Slut");
            Console.ReadKey();
        }
    }
}
