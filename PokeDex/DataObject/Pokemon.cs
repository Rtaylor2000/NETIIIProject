/// <summary>
/// Ryan Taylor
/// Created: 2020/12/10
/// 
/// this class is used to create a pokemon object.
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
    public class Pokemon
    {
        [Required]
        [Display(Name = "Pokémon Number")]
        public int PokedexNumber { get; set; }
        [Required]
        [Display(Name = "Pokémon Name")]
        public string PokemonName { get; set; }
        [Required]
        [Display(Name = "Type One")]
        public string TypeOne { get; set; }
        [Display(Name = "Type Two")]
        public string TypeTwo { get; set; }
        [Required]
        [Display(Name = "Catch Rate")]
        public int CatchRate { get; set; }
        [Required]
        [Display(Name = "Base HP")]
        public int BaseHP { get; set; }
        [Required]
        [Display(Name = "Base Attack")]
        public int BaseAttack { get; set; }
        [Required]
        [Display(Name = "Base Defense")]
        public int BaseDefense { get; set; }
        [Required]
        [Display(Name = "Base Special Attack")]
        public int BaseSpecialAttack { get; set; }
        [Required]
        [Display(Name = "Base Special Defense")]
        public int BaseSpecialDefense { get; set; }
        [Required]
        [Display(Name = "Base Speed")]
        public int BaseSpeed { get; set; }
        [Required]
        [Display(Name = "Pokémon Description")]
        public string PokemonDescription { get; set; }
    }
}
