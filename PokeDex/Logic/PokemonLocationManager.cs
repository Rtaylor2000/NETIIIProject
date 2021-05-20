using DataAccess;
using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class PokemonLocationManager : IPokemonLocationManager
    {
        private IPokemonLocationAccessor _pokemonLocationAccessor;
        public PokemonLocationManager() 
        {
            _pokemonLocationAccessor = new PokemonLocationAccessor();
        }
        public PokemonLocationManager(IPokemonLocationAccessor pokemonLocationAccessor) 
        {
            _pokemonLocationAccessor = pokemonLocationAccessor;
        }

        public bool AddNewLocation(Location location)
        {
            bool result = false;

            int newLocation = 0;

            try
            {
                newLocation = _pokemonLocationAccessor.InsertNewLocation(location);
                if (newLocation == 0) 
                {
                    throw new ApplicationException("New location was not added.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Add Location Failed", ex);
            }

            return result;
        }

        public bool AddNewPokemonLocation(PokemonLocation pokemonLocation)
        {
            bool result = false;

            int newPokemonLocation = 0;

            try
            {
                newPokemonLocation = 
                    _pokemonLocationAccessor.InsertNewPokemonLocation(pokemonLocation);
                if (newPokemonLocation == 0)
                {
                    throw new ApplicationException("New pokemon location was not added.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Add Pokemon Location Failed", ex);
            }

            return result;
        }

        public bool EditLocation(Location oldLocation, Location newLocation)
        {
            bool result = false;

            try
            {
                result = (1 == _pokemonLocationAccessor.UpdateLocation(oldLocation, 
                    newLocation));
                if (result == false) 
                {
                    throw new ApplicationException("Location data not changed.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Update Failed.", ex);
            }

            return result;
        }

        public bool EditPokemonLocation(PokemonLocation oldPokemonLocation, 
            PokemonLocation newPokemonLocation)
        {
            bool result = false;

            try
            {
                result = (1 == _pokemonLocationAccessor.UpdatePokemonLocation(
                    oldPokemonLocation, newPokemonLocation));
                if (result == false)
                {
                    throw new ApplicationException("Pokemon location data not changed.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Update Failed.", ex);
            }

            return result;
        }

        public bool RemoveLocationByName(string locationName)
        {
            bool result = false;

            try
            {
                result = (1 == _pokemonLocationAccessor.DeleteLocationByName(locationName));
                if (result == false) 
                {
                    throw new ApplicationException("Location data was not deleted.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Deletion Failed", ex);
            }

            return result;
        }

        public bool RemovePokemonLocation(string locationName, string pokemonName, 
            string levelFound, string gameName)
        {
            bool result = false;

            try
            {
                result = (1 == _pokemonLocationAccessor.DeletePokemonLocation(locationName, 
                    pokemonName, levelFound, gameName));
                if (result == false)
                {
                    throw new ApplicationException("Pokemon location data was not deleted.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Deletion Failed", ex);
            }

            return result;
        }

        public List<Location> RetrieveAllLocation()
        {
            List<Location> locations = null;

            try
            {
                locations = _pokemonLocationAccessor.SelectAllLocation();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Location list not available.", ex);
            }

            return locations;
        }

        public List<PokemonLocation> RetrievePokemonLocationByLocationName(string locationName)
        {
            List<PokemonLocation> pokemonLocations = null;

            try
            {
                pokemonLocations = 
                    _pokemonLocationAccessor.SelectPokemonLocationByLocationName(locationName);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Pokemon location list not available.", ex);
            }

            return pokemonLocations;
        }

        public List<PokemonLocation> RetrievePokemonLocationByPokemon(string pokemonName)
        {
            List<PokemonLocation> pokemonLocations = null;

            try
            {
                pokemonLocations =
                    _pokemonLocationAccessor.SelectPokemonLocationByPokemon(pokemonName);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Pokemon location list not available.", ex);
            }

            return pokemonLocations;
        }
    }
}
