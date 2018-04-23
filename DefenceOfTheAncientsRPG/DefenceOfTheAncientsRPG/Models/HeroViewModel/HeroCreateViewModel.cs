using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DefenceOfTheAncientsRPG.Models;

namespace DefenceOfTheAncientsRPG.Models.HeroViewModel
{
    public class HeroCreateViewModel
    {
        [Required]
        [Display (Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Strength gain")]
        public float StrengthGain { get; set; }
        [Required]
        [Display(Name = "Agility gain")]
        public float AgilityGain { get; set; }
        [Required]
        [Display(Name = "Intelligence gain")]
        public float IntelligenceGain { get; set; }
        [Required]
        [Display(Name = "Main attribute")]
        public Attribute MainAttribute { get; set; }
    }
}
