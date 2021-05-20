using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IPokemonLocationManager
    {
        bool RemoveLocationByName(string locationName);
        bool RemovePokemonLocation(string locationName, string pokemonName, string levelFound
            , string gameName);
        bool AddNewLocation(Location location);
        bool AddNewPokemonLocation(PokemonLocation pokemonLocation);
        List<Location> RetrieveAllLocation();
        List<PokemonLocation> RetrievePokemonLocationByLocationName(string locaionName);
        List<PokemonLocation> RetrievePokemonLocationByPokemon(string pokemonName);
        bool EditLocation(Location oldLocation, Location newLocation);
        bool EditPokemonLocation(PokemonLocation oldPokemonLocation
            , PokemonLocation newPokemonLocation);
    }
}
