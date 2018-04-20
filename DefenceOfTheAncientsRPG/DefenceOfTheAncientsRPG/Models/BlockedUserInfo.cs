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
        public bool Block { get; set; }

        public BlockedUserInfo()
        {

        }
    }
}
