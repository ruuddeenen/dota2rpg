using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DefenceOfTheAncientsRPG.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DefenceOfTheAncientsRPG.Models.HeroViewModel
{
    public class HeroCreateViewModel
    {
        [Required]
        [Display(Name = "Name")]
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
        public Attribute MainAttribute
        {
            get
            {
                return (Attribute)SelectedAttribute;
            }
        }

        public int SelectedAttribute { get; set; }

        public List<Attribute> Attributes; 
        public IEnumerable<SelectListItem> AttributeItems
        {
            get

            {
                try
                {
                    var allAttributes = Attributes.Select(a => new SelectListItem
                    {
                        Text = a.ToString(),
                        Value = ((int)a).ToString()

                    });
                    return DefaultAttributeItem.Concat(allAttributes);
                }
                catch
                {
                    return null;
                }
            }
        }

        public IEnumerable<SelectListItem> DefaultAttributeItem
        {
            get
            {
                return Enumerable.Repeat(new SelectListItem
                {
                    Value = "-1",
                    Text = "Select a main attribute"
                }, count: 1);
            }
        }

    }
}
