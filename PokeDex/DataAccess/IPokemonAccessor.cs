using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IPokemonAccessor
    {
        int DeleteEvolution(string reactant, string evolutionCondition, string evolvesInto);
        int DeletePokemon(string pokemonName);
        int InsertNewEvolution(Evolution evolution);
        int InsertNewPokemon(Pokemon pokemon);
        List<Pokemon> SelectAllPokemon();
        List<Evolution> SelectEvolutionByEvolvesInto(string evolvesInto);
        List<Evolution> SelectEvolutionByReactant(string reactant);
        Pokemon SelectPokemonByName(string pokemonName);
        Pokemon SelectPokmeonByNumber(int pokedexNumber);
        List<Pokemon> SelectPokemonByType(string type);
        int UpdatePokemon(Pokemon oldPokemon, Pokemon newPokemon);
        int UpdateEvolution(Evolution oldEvolution, Evolution newEvolution);
        
    }
}
