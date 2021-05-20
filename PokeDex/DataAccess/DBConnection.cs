using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    internal static class DBConnection
    {
        private static string connectionString =
            @"Data Source=localHost\sqlexpress;Initial Catalog=gen_one_db;Integrated Security=True";

        public static SqlConnection GetSqlConnection()
        {
            var conn = new SqlConnection(connectionString);
            return conn;
        }
    }
}
