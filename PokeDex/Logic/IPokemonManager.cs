using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IPokemonManager
    {
        bool RemoveEvolution(string reactant, string evolutionCondition, string evolvesInto);
        bool RemovePokemon(string pokemonName);
        bool AddNewEvolution(Evolution evolution);
        bool AddNewPokemon(Pokemon pokemon);
        List<Pokemon> RetrieveAllPokemon();
        List<Evolution> RetrieveEvolutionByEvolvesInto(string evolvesInto);
        List<Evolution> RetrieveEvolutionByReactant(string reactant);
        Pokemon RetrievePokemonByName(string pokemonName);
        Pokemon RetrievePokmeonByNumber(int pokedexNumber);
        List<Pokemon> RetrievePokemonByType(string type);
        bool EditPokemon(Pokemon oldPokemon, Pokemon newPokemon);
        bool EditEvolution(Evolution oldEvolution, Evolution newEvolution);
    }
}
