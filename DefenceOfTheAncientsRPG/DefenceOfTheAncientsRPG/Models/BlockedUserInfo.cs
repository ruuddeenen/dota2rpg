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
        public DateTime Until { get; set; }

        public BlockedUserInfo(string message, string userId, string adminId)
        {
            UserId = userId;
            Message = message;
            AdminId = adminId;
        }

        public BlockedUserInfo(string message, string userId, string adminId, DateTime until)
        {
            UserId = userId;
            Message = message;
            AdminId = adminId;
            Until = until;
        }
    }
}
