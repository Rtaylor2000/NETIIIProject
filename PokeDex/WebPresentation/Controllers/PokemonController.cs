/// <summary>
/// Ryan Taylor
/// Created: 2021/03/20
/// 
/// this controles all of the crud funtions for the pokemon object 
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
    public class PokemonController : Controller
    {
        private PokemonManager _pokemonManager = new PokemonManager();
        private PokemonLocationManager _locationManager = new PokemonLocationManager();

        //-----------------------------------Viewing Pokemon------------------------------------

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/20
        /// 
        /// method used to get a list of all pokemon.
        /// </summary>
        // GET: Pokemon
        public ActionResult AllPokemon()
        {
            return View(_pokemonManager.RetrieveAllPokemon());
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/14
        /// 
        /// method used to find and show the details of a pokemon by their name.
        /// </summary>
        /// <param name="pokemonName">the name of the pokemon</param>
        [HttpPost]
        public ActionResult SearchByName(string pokemonName) => RedirectToAction("Details", "Pokemon", new { pokemonName = pokemonName });

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/14
        /// 
        /// method used to find and show the details of a pokemon by their dex number.
        /// </summary>
        /// <param name="pokedexNumber">the pokedex number of the pokemon</param>
        [HttpPost]
        public ActionResult SearchByNumber(int pokedexNumber) 
        {
            Pokemon p = _pokemonManager.RetrievePokmeonByNumber(pokedexNumber);
            if (p == null)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = 
                    "A pokemon with a pokedex number of "+ pokedexNumber 
                    + " could not be found." });
            }
            return RedirectToAction("Details", "Pokemon", new { pokemonName = p.PokemonName });
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/14
        /// 
        /// method used to find and show a list of pokemon of a sertian type.
        /// </summary>
        /// <param name="type">the type of pokemon you want to find</param>
        [HttpPost]
        public ActionResult SearchByType(string type) 
        {
            List<Pokemon> pokemonByType = _pokemonManager.RetrievePokemonByType(type);
            if (pokemonByType == null) 
            {
                return RedirectToAction("Error", "Home", new
                {
                    errorMessage =
                    "Pokemon with a type of " + type
                    + " could not be found."
                });
            }
            return View(pokemonByType);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/20
        /// 
        /// method used to show the details of a pokemon.
        /// </summary>
        /// <param name="pokemonName">the name of the pokemon</param>
        public ActionResult Details(string pokemonName)
        {
            Pokemon p = _pokemonManager.RetrievePokemonByName(pokemonName);
            if (p == null)
            {
                return RedirectToAction("Error", "Home", new
                {
                    errorMessage =
                    "A pokemon with the name of " + pokemonName
                    + " could not be found."
                });
            }
            List<Evolution> evolutions =
                _pokemonManager.RetrieveEvolutionByReactant(p.PokemonName);
            List<PokemonLocation> pokemonLocations =
                _locationManager.RetrievePokemonLocationByPokemon(p.PokemonName);
            ViewBag.Evolutions = evolutions;
            ViewBag.PokemonLocations = pokemonLocations;
            return View(p);
        }

        //-----------------------------------Creating Pokemon-----------------------------------

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/20
        /// 
        /// method used to set up a pokemon creation form.
        /// </summary>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/20
        /// 
        /// method used to use the information from the form to create a pokemon.
        /// </summary>
        /// <param name="pokemon">the pokemon object of the newly created pokemon</param>
        [HttpPost]
        public ActionResult CreatePokemon(Pokemon pokemon)
        {
            if (!pokemon.PokemonName.isValidPokemonName())
            {
                string error = "Invalid Pokemon Name.";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            if (!pokemon.TypeOne.isValidTypeOne())
            {
                string error = "Invalid Type One.";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            if (!pokemon.TypeTwo.isValidTypeTwo())
            {
                string error = "Invalid Type Two.(None is valid)";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            if (pokemon.PokemonDescription.Trim() == "")
            {
                string error = "The Pokemon Description can not be empty.";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            try
            {
                _pokemonManager.AddNewPokemon(pokemon);
                return RedirectToAction("AllPokemon", "Pokemon");
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        //-----------------------------------Editing Pokemon------------------------------------

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/20
        /// 
        /// method used to setup an edit view for editing pokemon.
        /// </summary>
        /// <param name="pokemonName">the name of the pokemon being edited</param>
        public ActionResult Edit(string pokemonName)
        {
            Pokemon p = _pokemonManager.RetrievePokemonByName(pokemonName);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/20
        /// 
        /// method used to setup an edit view for editing pokemon.
        /// </summary>
        /// <param name="updatedPokemon">the new pokmeon object 
        /// replacing the old pokemon object</param>
        [HttpPost]
        public ActionResult PokemonEdit(Pokemon updatedPokemon)
        {
            Pokemon outdatedPokemon = _pokemonManager.RetrievePokemonByName(
                updatedPokemon.PokemonName);
            if (outdatedPokemon == null)
            {
                return HttpNotFound();
            }
            if (!updatedPokemon.TypeOne.isValidTypeOne())
            {
                string error = "Invalid Type One.";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            if (!updatedPokemon.TypeTwo.isValidTypeTwo())
            {
                string error = "Invalid Type Two.(None is valid)";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            if (updatedPokemon.PokemonDescription.Trim() == "")
            {
                string error = "The Pokemon Description can not be empty.";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            try
            {
                _pokemonManager.EditPokemon(outdatedPokemon, updatedPokemon);
                return RedirectToAction("AllPokemon", "Pokemon" );
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        //-----------------------------------Deleting Pokemon-----------------------------------

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/20
        /// 
        /// method used to set up a form to see if the user wants to delete the pokemon.
        /// </summary>
        /// <param name="pokemonName">the name of the pokemon being deleted</param>
        // GET: Pokemon
        public ActionResult TryPokemonDelete(string pokemonName)
        {
            return View(_pokemonManager.RetrievePokemonByName(pokemonName));
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/28
        /// 
        /// method used to delete a pokemon .
        /// </summary>
        /// <param name="pokemonName">the name of the pokemon being deleted</param>
        /// <param name="delete">the users choice of deleting the pokemon</param>
        [HttpPost]
        public ActionResult Delete(bool delete, string pokemonName) 
        {
            if (delete)
            {
                try
                {
                    List<PokemonLocation> pokemonLocations =
                        _locationManager.RetrievePokemonLocationByPokemon(pokemonName);
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
                    List<Evolution> pokmeonEvolutions =
                        _pokemonManager.RetrieveEvolutionByReactant(pokemonName);
                    pokmeonEvolutions.AddRange(
                        _pokemonManager.RetrieveEvolutionByEvolvesInto(pokemonName));
                    foreach (var evolution in pokmeonEvolutions)
                    {
                        try
                        {
                            _pokemonManager.RemoveEvolution(evolution.Reactant, 
                                evolution.EvolutionCondition, evolution.EvolvesInto);
                        }
                        catch (Exception ex)
                        {
                            string error = ex.Message;
                            return RedirectToAction("Error", "Home", 
                                new { errorMessage = error });
                        }
                    }
                    _pokemonManager.RemovePokemon(pokemonName);
                    return RedirectToAction("AllPokemon", "Pokemon");
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    return RedirectToAction("Error", "Home", new { errorMessage = error });
                }
            }
            else 
            {
                return RedirectToAction("AllPokemon", "Pokemon");
            }
        }



        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/20
        /// 
        /// method used to clear the pokemon manager.
        /// </summary>
        /// <param name="disposing">true or false to clear the pokemon manager</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _pokemonManager = null;
                _locationManager = null;
            }
            base.Dispose(disposing);
        }
    }
}