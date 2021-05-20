/// <summary>
/// Ryan Taylor
/// Created: 2020/12/10
/// 
/// this class is used to create a location object.
/// </summary>
/// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObject
{
    public class Location
    {
        [Required]
        [Display(Name = "Location Name")]
        public string LocationName { get; set; }
        [Required]
        [Display(Name = "Location Description")]
        public string Description { get; set; }
    }
}
