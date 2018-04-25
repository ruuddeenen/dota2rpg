using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Data;
using DefenceOfTheAncientsRPG.Models;
using System.Data.SqlClient;

namespace DefenceOfTheAncientsRPG.Data
{
    public class ApplicationUserSQLContext : IApplicationUserContext
    {

        public List<ApplicationUser> GetAllUsers()
        {
            List<ApplicationUser> result = new List<ApplicationUser>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM ApplicationUsers";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(CreateApplicationUserFromReader(reader));
                            }
                        }
                    }
                }
            }
            return result;
        }

        public ApplicationUser GetUserById(string id)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = string.Format("SELECT * FROM ApplicationUsers WHERE Id = '{0}'", id);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return CreateApplicationUserFromReader(reader);
                        }
                    }
                }
            }
            return null;
        }

        public ApplicationUser GetUserByUsername(string username)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = string.Format("SELECT * FROM ApplicationUsers WHERE Username = '{0}'", username);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return CreateApplicationUserFromReader(reader);
                        }
                    }
                }
            }
            return null;
        }

        public bool Insert(ApplicationUser user)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = string.Format("INSERT INTO ApplicationUsers (Id, Username, PasswordHash, FirstName, LastName, CreatedOn)" +
                    " VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                    user.Id, user.Username, user.Password, user.FirstName, user.LastName, user.CreatedOn.ToString("yyyyMMdd"));
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

        public bool Edit(ApplicationUser user)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = string.Format("UPDATE ApplicationUsers SET Email = {0}, FirstName = {1}, LastName = {2} WHERE Id = {3}",
                    user.Email, user.FirstName, user.LastName, user.Id);
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

        public bool ChangePassword(ApplicationUser user)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = string.Format("UPDATE ApplicationUsers SET PasswordHash = '{0}' WHERE Id = '{1}'",
                   user.Password, user.Id);
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

        /// <summary>
        /// Helper function to construct a Student instance from a DataReader.
        /// Expects that read() has already been called.
        /// </summary>
        /// <param name="reader">The DataReader to read from.</param>
        /// <returns>A new Student instance based on the read data.</returns>
        private ApplicationUser CreateApplicationUserFromReader(SqlDataReader reader)
        {
            return new ApplicationUser
            {
                Id = Convert.ToString(reader["Id"]),
                Username = Convert.ToString(reader["Username"]),
                Password = Convert.ToString(reader["PasswordHash"]),
                FirstName = Convert.ToString(reader["FirstName"]),
                LastName = Convert.ToString(reader["LastName"]),
                CreatedOn = Convert.ToDateTime(reader["CreatedOn"])
            };
        }


        public bool IsBlocked(ApplicationUser user)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = string.Format("SELECT * FROM BlockedUsers WHERE UserId = '{0}'", user.Id);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return true;
                        }
                        else return false;
                    }
                }
            }
        }

        public bool Login(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
