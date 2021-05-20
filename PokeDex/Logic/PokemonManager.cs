/// <summary>
/// Ryan Taylor
/// Created: 2020/12/10
/// 
/// this class file is used to check crud functions related to the pokemon object.
/// </summary>
/// 
using DataAccess;
using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class PokemonManager : IPokemonManager
    {
        private IPokemonAccessor _pokemonAccessor;
        public PokemonManager()
        {
            _pokemonAccessor = new PokemonAccessor();
        }

        public PokemonManager(IPokemonAccessor pokemonAccessor)
        {
            _pokemonAccessor = pokemonAccessor;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to create an evolution object.
        /// </summary>
        /// <param name="evolution">the new evolution object to be added into the </param>
        /// <exception cref="SQLException">Insert Fails 
        /// (Add evolution Failed New evolution was not added)</exception>
        /// <returns>true or false if the addition worked</returns>
        public bool AddNewEvolution(Evolution evolution)
        {
            bool result = false;
            int newEvolution = 0;

            try
            {
                newEvolution = _pokemonAccessor.InsertNewEvolution(evolution);
                if (newEvolution == 0) 
                {
                    throw new ApplicationException("New evolution was not added.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Add Evolution Failed.", ex);
            }

            return result;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to create a pokemon object.
        /// </summary>
        /// <param name="pokemon">the new pokemon object to be added into the </param>
        /// <exception cref="SQLException">Insert Fails 
        /// (Add pokemon Failed new pokemon was not added.)</exception>
        /// <returns>true or false if the addition worked</returns>
        public bool AddNewPokemon(Pokemon pokemon)
        {
            bool result = false;
            int newPokemon = 0;

            try
            {
                newPokemon = _pokemonAccessor.InsertNewPokemon(pokemon);
                if (newPokemon == 0)
                {
                    throw new ApplicationException("New pokemon was not added.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Add Pokemon Failed.", ex);
            }

            return result;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to edit an evolution object.
        /// </summary>
        /// <param name="oldEvolution">the original evolution object</param>
        /// <param name="newEvolution">the new evolution object to relpace the old one</param>
        /// <exception cref="SQLException">Insert Fails 
        /// (Update Failed. Evolution data not changed.)</exception>
        /// <returns>true or false if the edit worked</returns>
        public bool EditEvolution(Evolution oldEvolution, Evolution newEvolution)
        {
            bool result = false;

            try
            {
                result = (1 == _pokemonAccessor.UpdateEvolution(oldEvolution, newEvolution));
                if (result == false) 
                {
                    throw new ApplicationException("Evolution data not changed.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Update Failed.", ex);
            }

            return result;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to edit a pokemon object.
        /// </summary>
        /// <param name="oldPokemon">the original pokemon object</param>
        /// <param name="newPokemon">the new pokemon object to relpace the old one</param>
        /// <exception cref="SQLException">Insert Fails 
        /// (Update Failed. Pokemon data not changed.)</exception>
        /// <returns>true or false if the edit worked</returns>
        public bool EditPokemon(Pokemon oldPokemon, Pokemon newPokemon)
        {
            bool result = false;

            try
            {
                result = (1 == _pokemonAccessor.UpdatePokemon(oldPokemon, newPokemon));
                if (result == false)
                {
                    throw new ApplicationException("Pokemon data not changed.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Update Failed.", ex);
            }

            return result;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to remove an evolution object.
        /// </summary>
        /// <param name="reactant">the name of the pokemon be evolved</param>
        /// <param name="evolutionCondition">how the pokemon evolves</param>
        /// <param name="evolvesInto">the name of the pokemon it evolves into</param>
        /// <exception cref="SQLException">Insert Fails 
        /// (Deletion Failed. Evolution data was not deleted.)</exception>
        /// <returns>true or false if the removel worked</returns>
        public bool RemoveEvolution(string reactant, string evolutionCondition, string evolvesInto)
        {
            bool result = false;

            try
            {
                result = (1 == _pokemonAccessor.DeleteEvolution(reactant, evolutionCondition, evolvesInto));
                if (result == false)
                {
                    throw new ApplicationException("Evolution data was not deleted.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Deletion Failed.", ex);
            }

            return result;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to remove a pokemon object.
        /// </summary>
        /// <param name="pokemonName">the name of the pokemon to be removed</param>
        /// <exception cref="SQLException">Insert Fails 
        /// (Deletion Failed. Pokemon data was not deleted.)</exception>
        /// <returns>true or false if the removel worked</returns>
        public bool RemovePokemon(string pokemonName)
        {
            bool result = false;

            try
            {
                result = (1 == _pokemonAccessor.DeletePokemon(pokemonName));
                if (result == false)
                {
                    throw new ApplicationException("Pokemon data was not deleted.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Deletion Failed.", ex);
            }

            return result;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to retrive all the pokemon objects in the database.
        /// </summary>
        /// <exception cref="SQLException">Select Fails 
        /// (Pokemon list not available.)</exception>
        /// <returns>a list of pokemon</returns>
        public List<Pokemon> RetrieveAllPokemon()
        {
            List<Pokemon> pokemon = null;
            try
            {
                pokemon = _pokemonAccessor.SelectAllPokemon();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Pokemon list not available.", ex);
            }
            return pokemon;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to retrive all the evolution objects in the database 
        /// based on the name of the pokemon being evolved into.
        /// </summary>
        /// <param name="evolvesInto">the name of the pokemon being evolved into</param>
        /// <exception cref="SQLException">Select Fails 
        /// (Evolutions list not available.)</exception>
        /// <returns>a list of evolutions</returns>
        public List<Evolution> RetrieveEvolutionByEvolvesInto(string evolvesInto)
        {
            List<Evolution> evolutions = null;
            try
            {
                evolutions = _pokemonAccessor.SelectEvolutionByEvolvesInto(evolvesInto);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Evolutions list not available.", ex);
            }
            return evolutions;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to retrive all the evolution objects in the database 
        /// based on the name of the pokemon being evolved.
        /// </summary>
        /// <param name="reactant">the name of the pokemon being evolved</param>
        /// <exception cref="SQLException">Select Fails 
        /// (Evolutions list not available.)</exception>
        /// <returns>a list of evolutions</returns>
        public List<Evolution> RetrieveEvolutionByReactant(string reactant)
        {
            List<Evolution> evolutions = null;
            try
            {
                evolutions = _pokemonAccessor.SelectEvolutionByReactant(reactant);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Evolutions list not available.", ex);
            }
            return evolutions;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to retrive a pokemon objects by its name.
        /// </summary>
        /// <param name="pokemonName">the name of the pokemon</param>
        /// <exception cref="SQLException">Select Fails 
        /// (Pokemon not found.)</exception>
        /// <returns>a pokemon object</returns>
        public Pokemon RetrievePokemonByName(string pokemonName)
        {
            Pokemon pokemon = null;
            try
            {
                pokemon = _pokemonAccessor.SelectPokemonByName(pokemonName);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Pokemon not found.", ex);
            }
            return pokemon;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to retrive a pokemon objects by its type.
        /// </summary>
        /// <param name="type">the elemental type of a pokemon</param>
        /// <exception cref="SQLException">Select Fails 
        /// (Pokemon list not available.)</exception>
        /// <returns>a list of pokemon</returns>
        public List<Pokemon> RetrievePokemonByType(string type)
        {
            List<Pokemon> pokemon = null;
            try
            {
                pokemon = _pokemonAccessor.SelectPokemonByType(type);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Pokemon list not available.", ex);
            }
            return pokemon;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/12/10
        /// 
        /// this method is used to retrive apokemon objects by its pokedex number.
        /// </summary>
        /// <param name="pokedexNumber">the pokedex number of a pokemon</param>
        /// <exception cref="SQLException">Select Fails 
        /// (Pokemon not found.)</exception>
        /// <returns>a pokemon object</returns>
        public Pokemon RetrievePokmeonByNumber(int pokedexNumber)
        {
            Pokemon pokemon = null;
            try
            {
                pokemon = _pokemonAccessor.SelectPokmeonByNumber(pokedexNumber);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Pokemon not found.", ex);
            }
            return pokemon;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2020/05/09
        /// 
        /// this method is used to retrive the stats of a pokemon type.
        /// </summary>
        /// <param name="type">the pokemon type we need the stats on</param>
        /// <exception>returns a null list</exception>
        /// <returns>a list of numbers for that types stats</returns>
        public List<int> RetrieveTypeStats(string type) 
        {
            List<int> typeStats = new List<int>();
            switch (type)
            {
                case "NORMAL":
                    typeStats = new List<int> { 1, 1, 1, 1, 1, -1, 1, 0, 1, 1, 1, 1, 1, 1, 1};
                    break;
                case "FIGHTING":
                    typeStats = new List<int> { 2, 1, -1, -1, 1, 2, -1, 0, 1, 1, 1, 1, -1, 2, 1};
                    break;
                case "FLYING":
                    typeStats = new List<int> { 1, 2, 1, 1, 1, -1, 2, 1, 1, 1, 2, -1, 1, 1, 1};
                    break;
                case "POISON":
                    typeStats = new List<int> { 1, 1, 1, -1, -1, -1, 2, -1, 1, 1, 2, 1, 1, 1, 1};
                    break;
                case "GROUND":
                    typeStats = new List<int> { 1, 1, 0, 2, 1, 2, -1, 1, 2, 1, -1, 2, 1, 1, 1};
                    break;
                case "ROCK":
                    typeStats = new List<int> { 1, -1, 2, 1, -1, 1, 2, 1, 2, 1, 1, 1, 1, 2, 1};
                    break;
                case "BUG":
                    typeStats = new List<int> { 1, -1, -1, 2, 1, 1, 1, -1, -1, 1, 2, 1, 2, 1, 1};
                    break;
                case "GHOST":
                    typeStats = new List<int> { 0, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 0, 1, 1};
                    break;
                case "FIRE":
                    typeStats = new List<int> { 1, 1, 1, 1, 1, -1, 2, 1, -1, -1, 2, 1, 1, 2, -1};
                    break;
                case "WATER":
                    typeStats = new List<int> { 1, 1, 1, 1, 2, 2, 1, 1, 2, -1, -1, 1, 1, 1, -1};
                    break;
                case "GRASS":
                    typeStats = new List<int> { 1, 1, -1, -1, 2, 2, -1, 1, -1, 2, -1, 1, 1, 1, -1};
                    break;
                case "ELECTRIC":
                    typeStats = new List<int> { 1, 1, 2, 1, 0, 1, 1, 1, 1, 2, -1, -1, 1, 1, -1};
                    break;
                case "PSYCHIC":
                    typeStats = new List<int> { 1, 2, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, -1, 1, 1};
                    break;
                case "ICE":
                    typeStats = new List<int> { 1, 1, 2, 1, 2, 1, 1, 1, 1, -1, 2, 1, 1, -1, 2};
                    break;
                case "DRAGON":
                    typeStats = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2};
                    break;
                default:
                    break;
            }
            return typeStats;
        }
    }
}
