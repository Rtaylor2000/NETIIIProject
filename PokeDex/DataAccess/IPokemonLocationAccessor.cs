using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IPokemonLocationAccessor
    {
        int DeleteLocationByName(string locationName);
        int DeletePokemonLocation(string locationName, string pokemonName, string levelFound
            , string gameName);
        int InsertNewLocation(Location location);
        int InsertNewPokemonLocation(PokemonLocation pokemonLocation);
        List<Location> SelectAllLocation();
        List<PokemonLocation> SelectPokemonLocationByLocationName(string locationName);
        List<PokemonLocation> SelectPokemonLocationByPokemon(string pokemonName);
        int UpdateLocation(Location oldLocation, Location newLocation);
        int UpdatePokemonLocation(PokemonLocation oldPokemonLocation
            , PokemonLocation newPokemonLocation);
    }
}
