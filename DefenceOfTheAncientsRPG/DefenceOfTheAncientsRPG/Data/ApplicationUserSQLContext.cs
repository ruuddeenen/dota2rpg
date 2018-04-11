﻿using System;
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
        public List<ApplicationUser> GetAllAdmins()
        {
            List<ApplicationUser> result = new List<ApplicationUser>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM ApplicationUsers WHERE Admin = true";
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

        public List<ApplicationUser> GetAllUsers()
        {
            List<ApplicationUser> result = new List<ApplicationUser>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM ApplicationUsers WHERE Admin = false";
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
                string query = string.Format("SELECT * FROM ApplicationUsers WHERE Id = {0}", id);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
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
            }
            return null;
        }

        public bool Insert(ApplicationUser user)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = "INSERT INTO ApplicationUsers (Id, Username, PasswordHash, FirstName, LastName, CreatedOn, Active, Admin" +
                    "VALUES (:id, :username, :passwordHash, :firstName, :lastName, :createdOn, :active, :admin";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", user.ID);
                    command.Parameters.AddWithValue("username", user.Username);
                    command.Parameters.AddWithValue("passwordHash", user.PasswordHash);
                    command.Parameters.AddWithValue("firstName", user.FirstName);
                    command.Parameters.AddWithValue("lastName", user.LastName);
                    command.Parameters.AddWithValue("createdOn", user.CreatedOn);
                    command.Parameters.AddWithValue("active", user.Active);
                    command.Parameters.AddWithValue("admin", user.Admin);
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
                ID = Convert.ToString(reader["Id"]),
                Username = Convert.ToString(reader["Username"]),
                PasswordHash = Convert.ToString(reader["PasswordHash"]),
                FirstName = Convert.ToString(reader["FirstName"]),
                LastName = Convert.ToString(reader["LastName"]),
                CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
                Active = Convert.ToBoolean(reader["Active"]),
                Admin = Convert.ToBoolean(reader["Admin"])
            };
        }

    }
}
