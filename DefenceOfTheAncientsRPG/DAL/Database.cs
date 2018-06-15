using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DefenceOfTheAncientsRPG.Data
{
    public class Database
    {
        public static string ConnectionString { get; set; }

        static Database()
        {
            ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DefenceOfTheAncientsRPG-database;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            int columns = 0;
            using (SqlConnection connection = Connection)
            {
                string query = "SELECT COUNT(*) FROM Administrators";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            columns = Convert.ToInt32(reader[0]);
                        }
                    }
                }
            }

            if (columns == 0)
            {
                CreateSuperUserAdmin();
            }
        }

        private static void CreateSuperUserAdmin()
        {
            using (SqlConnection connection = Connection)
            {
                string id = Guid.NewGuid().ToString();
                string username = "a.SuperUser";
                string passwordHash = Logic.SecurePasswordHasher.Hash("SU@123");
                string firstName = "Super";
                string lastName = "User";
                string dateOfBirth = DateTime.Now.ToString("yyyyMMdd");
                string createdOn = DateTime.Now.ToString("yyyyMMdd");
                string activated = "True";
                string query = string.Format("INSERT INTO Administrators (Id, Username, PasswordHash, FirstName, LastName, DateOfBirth, CreatedOn, Activated)" +
                    " VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')",
                    id, username, passwordHash, firstName, lastName, dateOfBirth, createdOn, activated);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new database connection and directly opens it. The caller
        /// is resposible for properly closing the connection.
        /// </summary>
        public static SqlConnection Connection
        {
            get
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();
                return connection;
            }
        }

        public static bool CleanTable(string table)
        {
            using (SqlConnection connection = Connection)
            {
                string query = string.Format("DELETE FROM {0}", table);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }

        }
    }
}