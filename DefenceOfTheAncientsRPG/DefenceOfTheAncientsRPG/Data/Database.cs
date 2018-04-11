using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DefenceOfTheAncientsRPG.Data
{
    public class Database
    {
        private static readonly string connectionString = @"Data Source = (localdb)\ProjectsV13;Initial Catalog = Dota2RPG - database; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /// <summary>
        /// Creates a new database connection and directly opens it. The caller
        /// is resposible for properly closing the connection.
        /// </summary>
        public static SqlConnection Connection
        {
            get
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
        }

    }
}
