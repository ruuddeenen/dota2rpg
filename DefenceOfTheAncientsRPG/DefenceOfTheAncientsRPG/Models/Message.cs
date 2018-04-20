using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefenceOfTheAncientsRPG.Models
{
    public class Message
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string AdminId { get; set; }
        public string Content { get; set; }
        public DateTime SendOn { get; set; }

        public Message(string userid, string adminid, string content)
        {
            Id = Guid.NewGuid().ToString();
            UserId = userid;
            AdminId = adminid;
            Content = content;
            SendOn = DateTime.Now;
        }
    }
}
