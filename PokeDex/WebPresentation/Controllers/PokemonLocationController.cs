/// <summary>
/// Ryan Taylor
/// Created: 2021/05/08
///
/// this controles all of the crud funtions for the pokemon location object 
/// </summary>

using DataObject;
using Logic;
using Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPresentation.Controllers
{
    [Authorize]
    public class PokemonLocationController : Controller
    {
        private PokemonManager _pokemonManager = new PokemonManager();
        private PokemonLocationManager _locationManager = new PokemonLocationManager();

        //-----------------------------------Viewing Pokemon Locations------------------------------------

        // GET: PokemonLocations
        public ActionResult AllPokemonLocations()
        {
            List<PokemonLocation> pokemonLocations = new List<PokemonLocation>();
            List<Pokemon> allPokemon = _pokemonManager.RetrieveAllPokemon();
            foreach (var pokemon in allPokemon)
            {
                pokemonLocations.AddRange(
                    _locationManager.RetrievePokemonLocationByPokemon(pokemon.PokemonName));
            }
            return View(pokemonLocations);
        }

        //-----------------------------------Creating Pokemon Locations-----------------------------------

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/05/08
        /// 
        /// method used to set up a pokemon location creation form.
        /// </summary>
        public ActionResult Create()
        {
            List<Pokemon> allPokemon = _pokemonManager.RetrieveAllPokemon();
            List<string> pokemonNames = new List<string>();
            foreach (var pokemon in allPokemon)
            {
                pokemonNames.Add(pokemon.PokemonName);
            }

            List<Location> allLocations = _locationManager.RetrieveAllLocation();
            List<string> locationNames = new List<string>();
            foreach (var location in allLocations)
            {
                locationNames.Add(location.LocationName);
            }

            ViewBag.PokemonNames = pokemonNames;
            ViewBag.LocationNames = locationNames;
            return View();
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/05/08
        /// 
        /// method used to use the information from the form to create a pokemon.
        /// </summary>
        /// <param name="pokemonlocation">the pokemon location object of the newly 
        /// created pokemon location</param>
        [HttpPost]
        public ActionResult CreatePokemonLocation(PokemonLocation pokemonlocation)
        {
            if (!pokemonlocation.LocationName.isValidLocationName())
            {
                string error = "Invalid Location Name.";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            if (!pokemonlocation.GameName.isValidGameName())
            {
                string error = "Invalid Location Name.";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            if (!pokemonlocation.LevelFound.isValidLevelFound())
            {
                string error = "Invalid Level Found.";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            try
            {
                _locationManager.AddNewPokemonLocation(pokemonlocation);
                return RedirectToAction("AllPokemonLocations", "PokemonLocation");
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        //-----------------------------------Editing Pokemon Locations------------------------------------

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/05/09
        /// 
        /// method used to setup an edit view for editing pokemon.
        /// </summary>
        /// <param name="pokemonName">the name of the pokemon in the pokemon location being edited</param>
        /// <param name="locationName">the name of the location in the pokemon location being edited</param>
        /// <param name="gameName">the name of the game in the pokemon location being edited</param>
        /// <param name="levelFound">the level of the pokemon in the pokemon location being edited</param>
        public ActionResult Edit(string pokemonName, string locationName, 
            string levelFound, string gameName)
        {
            List<PokemonLocation> pokemonLocations = 
                _locationManager.RetrievePokemonLocationByLocationName(locationName);
            List<PokemonLocation> onePokemonLocation = pokemonLocations.Where(p => 
            p.PokemonName == pokemonName && p.LevelFound == levelFound 
            && p.GameName == gameName).ToList();
            PokemonLocation pokemonLocation = onePokemonLocation[0];
            if (pokemonLocation == null)
            {
                return HttpNotFound();
            }
            List<Location> allLocations = _locationManager.RetrieveAllLocation();
            List<string> locationNames = new List<string>();
            foreach (var location in allLocations)
            {
                locationNames.Add(location.LocationName);
            }
            ViewBag.LocationNames = locationNames;
            return View(pokemonLocation);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/05/09
        /// 
        /// method used to setup an edit view for editing pokemon.
        /// </summary>
        /// <param name="updatedPokemonLocation">the new pokmeon location object 
        /// replacing the old pokemon location object</param>
        /// <param name="oldLevelFound">the level of the pokemon in the pokemon location being edited</param>
        /// <param name="oldLocationName">the name of the location in the pokemon location being edited</param>
        /// <param name="oldGameName">the name of the game in the pokemon location being edited</param>
        [HttpPost]
        public ActionResult EditPokemonLocation(PokemonLocation updatedPokemonLocation, string oldLocationName,
            string oldLevelFound, string oldGameName)
        {
            List<PokemonLocation> pokemonLocations =
                _locationManager.RetrievePokemonLocationByLocationName(oldLocationName);
            List<PokemonLocation> onePokemonLocation = pokemonLocations.Where(p =>
            p.PokemonName == updatedPokemonLocation.PokemonName && p.LevelFound == oldLevelFound
            && p.GameName == oldGameName).ToList();
            PokemonLocation oldPokemonLocation = onePokemonLocation[0];
            if (oldPokemonLocation == null)
            {
                return HttpNotFound();
            }
            if (!updatedPokemonLocation.LocationName.isValidLocationName())
            {
                string error = "Invalid Location Name.";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            if (!updatedPokemonLocation.GameName.isValidGameName())
            {
                string error = "Invalid Location Name.";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            if (!updatedPokemonLocation.LevelFound.isValidLevelFound())
            {
                string error = "Invalid Level Found.";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            try
            {
                _locationManager.EditPokemonLocation(oldPokemonLocation, updatedPokemonLocation);
                return RedirectToAction("AllPokemonLocations", "PokemonLocation");
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        //-----------------------------------Deleting Pokemon Locations-----------------------------------

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/05/09
        /// 
        /// method used to set up a form to see if the user wants to delete the pokemon.
        /// </summary>
        /// <param name="pokemonName">the name of the pokemon in the pokemon location being edited</param>
        /// <param name="locationName">the name of the location in the pokemon location being edited</param>
        /// <param name="gameName">the name of the game in the pokemon location being edited</param>
        /// <param name="levelFound">the level of the pokemon in the pokemon location being edited</param>
        // GET: Pokemon
        public ActionResult TryPokemonLocationDelete(string pokemonName, string locationName,
            string levelFound, string gameName)
        {
            List<PokemonLocation> pokemonLocations =
                _locationManager.RetrievePokemonLocationByLocationName(locationName);
            List<PokemonLocation> onePokemonLocation = pokemonLocations.Where(p =>
            p.PokemonName == pokemonName && p.LevelFound == levelFound
            && p.GameName == gameName).ToList();
            PokemonLocation pokemonLocation = onePokemonLocation[0];
            if (pokemonLocation == null)
            {
                return HttpNotFound();
            }
            return View(pokemonLocation);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/05/09
        /// 
        /// method used to delete a pokemon .
        /// </summary>
        /// <param name="pokemonName">the name of the pokemon in the pokemon location being edited</param>
        /// <param name="locationName">the name of the location in the pokemon location being edited</param>
        /// <param name="gameName">the name of the game in the pokemon location being edited</param>
        /// <param name="levelFound">the level of the pokemon in the pokemon location being edited</param>
        /// <param name="delete">the users choice of deleting the pokemon</param>
        [HttpPost]
        public ActionResult Delete(bool delete, string pokemonName, string locationName,
            string levelFound, string gameName)
        {
            if (delete)
            {
                try
                {
                    List<PokemonLocation> pokemonLocations =
                        _locationManager.RetrievePokemonLocationByLocationName(
                            locationName);
                    List<PokemonLocation> onePokemonLocation = pokemonLocations.Where(
                        p => p.PokemonName == pokemonName && p.LevelFound == levelFound
                    && p.GameName == gameName).ToList();
                    PokemonLocation pokemonLocation = onePokemonLocation[0];
                    _locationManager.RemovePokemonLocation(pokemonLocation.LocationName, 
                        pokemonLocation.PokemonName, pokemonLocation.LevelFound, 
                        pokemonLocation.GameName);
                    return RedirectToAction("AllPokemonLocations", "PokemonLocation");
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    return RedirectToAction("Error", "Home", new { errorMessage = error });
                }
            }
            else
            {
                return RedirectToAction("AllPokemonLocations", "PokemonLocation");
            }
        }


    }
}