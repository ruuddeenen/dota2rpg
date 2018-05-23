using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefenceOfTheAncientsRPG.Models
{
    public class BlockedUserInfo
    {
        public string UserId { get; set; }
        public string Message { get; set; }
        public string AdminId { get; set; }
        public DateTime Since { get; set; }
        public DateTime Until { get; set; }

        public BlockedUserInfo(string message, string userId, string adminId)
        {
            UserId = userId;
            Message = message;
            AdminId = adminId;
            Since = DateTime.Now;
        }

        public BlockedUserInfo(string message, string userId, string adminId, DateTime until)
        {
            UserId = userId;
            Message = message;
            AdminId = adminId;
            Since = DateTime.Now;
            Until = until;
        }

        /// <summary>
        /// Used for retrieving BlockedUserInfo from database.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="userId"></param>
        /// <param name="adminId"></param>
        /// <param name="since"></param>
        /// <param name="until"></param>
        public BlockedUserInfo(string message, string userId, string adminId, DateTime since, DateTime until)
        {
            UserId = userId;
            Message = message;
            AdminId = adminId;
            Since = since;
            Until = until;
        }
    }
}
