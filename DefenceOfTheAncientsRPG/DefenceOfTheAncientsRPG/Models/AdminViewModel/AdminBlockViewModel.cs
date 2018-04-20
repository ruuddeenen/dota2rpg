using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DefenceOfTheAncientsRPG.Models.AdminViewModel
{
    public class AdminBlockViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string AdminId { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public bool Block { get; set; }

        [Required]
        public DateTime From { get; set; }
        
        public DateTime Until { get; set; }

        public AdminBlockViewModel()
        {
            From = DateTime.Now;
        }
    }
}
