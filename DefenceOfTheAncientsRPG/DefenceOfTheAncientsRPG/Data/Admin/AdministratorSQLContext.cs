using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Data;
using DefenceOfTheAncientsRPG.Models;
using System.Data.SqlClient;


namespace DefenceOfTheAncientsRPG.Data
{
    public class AdministratorSQLContext : IAdministratorContext
    {
        public List<Administrator> GetAllAdmins()
        {
            List<Administrator> result = new List<Administrator>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM Administrators";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(CreateAdministratorFromReader(reader));
                        }
                    }
                }
            }
            return result;
        }

        public bool BlockUser(ApplicationUser user, string message)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = string.Format("UPDATE ApplicationUsers SET Active = 'False' WHERE Id = '{0}'", user.ID);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
        }

        public bool UnblockUser(ApplicationUser user, string message)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = string.Format("UPDATE ApplicationUsers SET Active = 'True' WHERE Id = '{0}'", user.ID);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
        }

        public bool Insert(Administrator admin)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = string.Format("INSERT INTO Administrators (Id, Username, PasswordHash, FirstName, LastName, DateOfBirth)" +
                    " VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                    admin.ID, admin.Username, admin.PasswordHash, admin.FirstName, admin.LastName, admin.DateOfBirth.ToString("yyyyMMdd"));
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                        return true;

                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
        }

        private Administrator CreateAdministratorFromReader(SqlDataReader reader)
        {
            return new Administrator
            {
                ID = Convert.ToString(reader["Id"]),
                Username = Convert.ToString(reader["Username"]),
                PasswordHash = Convert.ToString(reader["PasswordHash"]),
                FirstName = Convert.ToString(reader["FirstName"]),
                LastName = Convert.ToString(reader["LastName"]),
                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"])
            };
        }

        public Administrator GetAdminById(string id)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = string.Format("SELECT * FROM Administrators WHERE Id = '{0}'", id);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return CreateAdministratorFromReader(reader);
                        }
                    }
                }
            }
            return null;
        }

        public Administrator GetAdminByUsername(string username)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = string.Format("SELECT * FROM Administrators WHERE Username = '{0}'", username);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return CreateAdministratorFromReader(reader);
                        }
                    }
                }
            }
            return null;
        }
    }
}
