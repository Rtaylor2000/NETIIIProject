/// <summary>
/// Ryan Taylor
/// Created: 2020/12/10
/// 
/// this class is used to create a pokemon location object all the locations containing pokemon.
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
    public class PokemonLocation
    {
        [Required]
        [Display(Name = "Location Name")]
        public string LocationName { get; set; }
        [Required]
        [Display(Name = "Pokémon Name")]
        public string PokemonName { get; set; }
        [Required]
        [Display(Name = "Game Name")]
        public string GameName { get; set; }
        [Required]
        [Display(Name = "How Found")]
        public string HowFound { get; set; }
        [Required]
        [Display(Name = "Level Found")]
        public string LevelFound { get; set; }
        [Required]
        [Display(Name = "Species Encounter Rate")]
        public string SpeciesEncounterRate { get; set; }
    }
}
