using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Data;
using DefenceOfTheAncientsRPG.Models;
using System.Data.SqlClient;
using DefenceOfTheAncientsRPG.Exceptions;
using System.Data;

namespace DefenceOfTheAncientsRPG.Data
{
    public class ApplicationUserSQLContext : IApplicationUserContext
    {

        public List<ApplicationUser> GetAllUsers()
        {
            List<ApplicationUser> result = new List<ApplicationUser>();
            using (SqlConnection connection = Database.Connection)
            {
                using (SqlCommand command = new SqlCommand("dbo.spGetUsers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(CreateApplicationUserFromReader(reader));
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
                using (SqlCommand command = new SqlCommand("dbo.spGetUserById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@id", id));
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
                using (SqlCommand command = new SqlCommand("dbo.spGetUserByUsername", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@username", username));
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
                using (SqlCommand command = new SqlCommand("spInsertUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@id", user.Id));
                    command.Parameters.Add(new SqlParameter("@username", user.Username));
                    command.Parameters.Add(new SqlParameter("@password", user.Password));
                    command.Parameters.Add(new SqlParameter("@firstname", user.FirstName));
                    command.Parameters.Add(new SqlParameter("@lastname", user.LastName));
                    command.Parameters.Add(new SqlParameter("@email", user.Email));

                    return true;
                }
            }
        }

        public bool Edit(ApplicationUser user)
        {
            using (SqlConnection connection = Database.Connection)
            {
                using (SqlCommand command = new SqlCommand("dbo.spUpdateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@id", user.Id));
                    command.Parameters.Add(new SqlParameter("@firstname", user.FirstName));
                    command.Parameters.Add(new SqlParameter("@lastname", user.LastName));

                    if (command.ExecuteNonQuery() > 0) return true;
                    else return false;
                }
            }
        }

        public bool ChangePassword(string userid, string newpassword)
        {
            using (SqlConnection connection = Database.Connection)
            {
                using (SqlCommand command = new SqlCommand("dbo.spUpdatePasswordHash", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@id", userid));
                    command.Parameters.Add(new SqlParameter("@password", newpassword));
                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }


        private ApplicationUser CreateApplicationUserFromReader(SqlDataReader reader)
        {
            return new ApplicationUser
            {
                Id = Convert.ToString(reader["Id"]),
                Username = Convert.ToString(reader["Username"]),
                Password = Convert.ToString(reader["PasswordHash"]),
                FirstName = Convert.ToString(reader["FirstName"]),
                LastName = Convert.ToString(reader["LastName"]),
                CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
                Email = Convert.ToString(reader["Email"])
            };
        }


        public bool IsBlocked(ApplicationUser user)
        {
            using (SqlConnection connection = Database.Connection)
            {
                using (SqlCommand command = new SqlCommand("dbo.spGetBlockedUserByUserId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@userid", user.Id));

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


        public bool BlockUser(BlockedUserInfo info)
        {
            using (SqlConnection connection = Database.Connection)
            {
                /*
                string query = string.Format("INSERT INTO BlockedUsers (UserId, Message, Until, ByAdminId)" +
                    " VALUES ('{0}', '{1}','{2}','{3}')",
                    info.UserId, info.Message, info.Until.ToString("yyyyMMdd"), info.AdminId);*/
                using (SqlCommand command = new SqlCommand("dbo.spInsertBlockedUser", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@userid", info.UserId));
                        command.Parameters.Add(new SqlParameter("@message", info.Message));
                        command.Parameters.Add(new SqlParameter("@adminid", info.AdminId));
                        command.Parameters.Add(new SqlParameter("@until", info.Until));
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }

        public bool Unblock(string userId)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = string.Format("DELETE FROM BlockedUsers WHERE UserId = '{0}'", userId);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            throw new Exception();
        }

        public List<BlockedUserInfo> GetAllBlockedUsersInfo()
        {
            List<BlockedUserInfo> result = new List<BlockedUserInfo>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM BlockedUsers";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // result.Add(CreateBlockedUserInfoFromReader(reader));
                        }
                    }
                }
            }
            return result;
        }

        private BlockedUserInfo CreateBlockedUserInfoFromReader(SqlDataReader reader)
        {
            return new BlockedUserInfo
            (
                Convert.ToString(reader["Message"]),
                Convert.ToString(reader["UserId"]),
                Convert.ToString(reader["ByAdminId"]),
                Convert.ToDateTime(reader["Until"])
            );
        }

        public int RemoveEntriesFromBlockedUsers(List<BlockedUserInfo> entriesToBeRemoved)
        {
            System.Text.StringBuilder query = new System.Text.StringBuilder();
            query.Append("DELETE FROM BlockedUsers WHERE UserId IN ");

            query.Append("(");
            int count = 0;
            foreach (BlockedUserInfo bui in entriesToBeRemoved)
            {
                if (count > 0) query.Append(",");
                query
                    .Append("(")
                    .Append(bui.UserId)
                    .Append(")");

                count++;
            }
            query.Append(")");

            using (SqlConnection connection = Database.Connection)
            {
                using (SqlCommand command = new SqlCommand(query.ToString(), connection))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}
