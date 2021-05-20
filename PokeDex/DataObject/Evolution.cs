/// <summary>
/// Ryan Taylor
/// Created: 2020/12/10
/// 
/// this class is used to create a evolution object.
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
    public class Evolution
    {
        [Required]
        [Display (Name = "Pokemon Being Evolved")]
        public string Reactant { get; set; }
        [Required]
        [Display(Name = "Evolution Condition")]
        public string EvolutionCondition { get; set; }
        [Required]
        [Display(Name = "Evolves Into")]
        public string EvolvesInto { get; set; }
    }
}
