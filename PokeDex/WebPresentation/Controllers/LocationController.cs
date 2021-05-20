/// <summary>
/// Ryan Taylor
/// Created: 2021/04/28
/// 
/// this controles all of the crud funtions for the location object 
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
    public class LocationController : Controller
    {
        private PokemonLocationManager _locationManager = new PokemonLocationManager();

        //-----------------------------------Viewing Location------------------------------------

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/28
        /// 
        /// method used to get a list of all Locations.
        /// </summary>
        // GET: Location
        public ActionResult AllLocations()
        {
            return View(_locationManager.RetrieveAllLocation());
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/05/02
        /// 
        /// method used to show the details of a location.
        /// </summary>
        /// <param name="locationName">the name of the pokemon</param>
        public ActionResult Details(string locationName)
        {
            List<Location> locations = _locationManager.RetrieveAllLocation();
            List<Location> oneLocation = 
                locations.Where(l => l.LocationName == locationName).ToList();
            Location location = oneLocation[0];
            if (location == null)
            {
                return RedirectToAction("Error", "Home", new
                {
                    errorMessage =
                    "A pokemon with the name of " + location.LocationName
                    + " could not be found."
                });
            }
            List<PokemonLocation> pokemonLocations =
                _locationManager.RetrievePokemonLocationByLocationName(location.LocationName);
            ViewBag.PokemonLocations = pokemonLocations;
            return View(location);
        }

        //-----------------------------------Creating Location-----------------------------------

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/05/02
        /// 
        /// method used to set up a location creation form.
        /// </summary>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/05/02
        /// 
        /// method used to use the information from the form to create a location.
        /// </summary>
        /// <param name="location">the location object of the newly created location</param>
        [HttpPost]
        public ActionResult CreateLocation(Location location)
        {
            if (!location.LocationName.isValidLocationName())
            {
                string error = "Invalid location name";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            if (location.Description.Trim() == null || location.Description.Trim() == "")
            {
                string error = "location description can not be empty";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            try
            {
                _locationManager.AddNewLocation(location);
                return RedirectToAction("AllLocations", "Location");
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        //-----------------------------------Editing Location------------------------------------


        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/20
        /// 
        /// method used to setup an edit view for editing pokemon.
        /// </summary>
        /// <param name="locationName">the name of the location being edited</param>
        public ActionResult Edit(string locationName)
        {
            List<Location> locations = _locationManager.RetrieveAllLocation();
            List<Location> oneLocation =
                locations.Where(l => l.LocationName == locationName).ToList();
            Location location = oneLocation[0];
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/20
        /// 
        /// method used to setup an edit view for editing pokemon.
        /// </summary>
        /// <param name="updatedLocation">the new location object 
        /// replacing the old location object</param>
        [HttpPost]
        public ActionResult EditLocation(Location updatedLocation)
        {
            List<Location> locations = _locationManager.RetrieveAllLocation();
            List<Location> oneLocation =
                locations.Where(l => l.LocationName == updatedLocation.LocationName).ToList();
            Location outdatedLocation = oneLocation[0];
            if (outdatedLocation == null)
            {
                string error = "Loaction not found.";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            if (updatedLocation.Description.Trim() == null || updatedLocation.Description.Trim() == "")
            {
                string error = "location description can not be empty";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            try
            {
                _locationManager.EditLocation(outdatedLocation, updatedLocation);
                return RedirectToAction("AllLocations", "Location");
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        //-----------------------------------Deleting Location-----------------------------------

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/05/03
        /// 
        /// method used to set up a form to see if the user wants to delete the pokemon.
        /// </summary>
        /// <param name="locationName">the name of the location being deleted</param>
        // GET: Pokemon
        public ActionResult TryLocationDelete(string locationName)
        {
            List<Location> locations = _locationManager.RetrieveAllLocation();
            List<Location> oneLocation =
                locations.Where(l => l.LocationName == locationName).ToList();
            Location location = oneLocation[0];
            return View(location);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/05/03
        /// 
        /// method used to delete a pokemon .
        /// </summary>
        /// <param name="locationName">the name of the location being deleted</param>
        /// <param name="delete">the users choice of deleting the pokemon</param>
        [HttpPost]
        public ActionResult Delete(bool delete, string locationName)
        {
            if (delete)
            {
                try
                {
                    List<Location> locations = _locationManager.RetrieveAllLocation();
                    List<Location> oneLocation =
                        locations.Where(l => l.LocationName == locationName).ToList();
                    Location location = oneLocation[0];
                    List<PokemonLocation> pokemonLocations =
                        _locationManager.RetrievePokemonLocationByLocationName(location.LocationName);
                    foreach (var pokemonLocation in pokemonLocations)
                    {
                        try
                        {
                            _locationManager.RemovePokemonLocation(
                                pokemonLocation.LocationName, pokemonLocation.PokemonName,
                                pokemonLocation.LevelFound, pokemonLocation.GameName);
                        }
                        catch (Exception ex)
                        {
                            string error = ex.Message;
                            return RedirectToAction("Error", "Home",
                                new { errorMessage = error });
                        }
                    }
                    _locationManager.RemoveLocationByName(location.LocationName);
                    return RedirectToAction("AllLocations", "Location");
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    return RedirectToAction("Error", "Home", new { errorMessage = error });
                }
            }
            else
            {
                return RedirectToAction("AllLocations", "Location");
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/28
        /// 
        /// method used to clear the location manager.
        /// </summary>
        /// <param name="disposing">true or false to clear the pokemon manager</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _locationManager = null;
            }
            base.Dispose(disposing);
        }
    }
}