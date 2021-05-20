/// <summary>
/// Ryan Taylor
/// Created: 2021/05/03
/// 
/// this controles all of the crud funtions for the evolution object 
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
    public class EvolutionController : Controller
    {
        private PokemonManager _pokemonManager = new PokemonManager();

        //-----------------------------------Viewing Evolutions------------------------------------

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/05/03
        /// 
        /// method used to get a list of all evolutions.
        /// </summary>
        // GET: Evolution
        public ActionResult AllEvolutions()
        {
            List<Evolution> evolutions = new List<Evolution>();
            List<Pokemon> allPokemon = _pokemonManager.RetrieveAllPokemon();
            foreach (var pokemon in allPokemon)
            {
                if (_pokemonManager.RetrieveEvolutionByReactant(pokemon.PokemonName) 
                    != null)
                {
                    evolutions.AddRange(
                        _pokemonManager.RetrieveEvolutionByReactant(
                            pokemon.PokemonName));
                }
            }
            return View(evolutions);
        }

        //-----------------------------------Creating Evolutions-----------------------------------

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/05/05
        /// 
        /// method used to set up an evolution creation form.
        /// </summary>
        public ActionResult Create()
        {
            List<Pokemon> allPokemon = _pokemonManager.RetrieveAllPokemon();
            List<string> pokemonNames = new List<string>();
            foreach (var pokemon in allPokemon)
            {
                pokemonNames.Add(pokemon.PokemonName);
            }
            ViewBag.PokemonNames = pokemonNames;
            return View();
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/05/05
        /// 
        /// method used to use the information from the form to create a pokemon.
        /// </summary>
        /// <param name="evolution">the evolution object of the newly created evolution</param>
        [HttpPost]
        public ActionResult CreateEvolution(Evolution evolution)
        {
            if (!evolution.Reactant.isValidPokemonName())
            {
                string error = "Invalid Reactant.";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            if (!evolution.EvolvesInto.isValidPokemonName())
            {
                string error = "Invalid Evolves Into.";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            try
            {
                _pokemonManager.AddNewEvolution(evolution);
                return RedirectToAction("AllEvolutions", "Evolution");
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        //-----------------------------------Editing Evolutions------------------------------------

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/05/05
        /// 
        /// method used to setup an edit view for editing pokemon.
        /// </summary>
        /// <param name="reactant">the name of the pokemon being evolved</param>
        /// <param name="condition">how the pokmeon evolves</param>
        /// <param name="evolvesInto">the pokmeon being evolved into</param>
        public ActionResult Edit(string reactant, string condition, string evolvesInto)
        {
            List<Pokemon> allPokemon = _pokemonManager.RetrieveAllPokemon();
            List<string> pokemonNames = new List<string>();
            foreach (var pokemon in allPokemon)
            {
                pokemonNames.Add(pokemon.PokemonName);
            }
            ViewBag.PokemonNames = pokemonNames;
            List<Evolution> evolutions = _pokemonManager.RetrieveEvolutionByReactant(reactant);
            if (evolutions == null)
            {
                return HttpNotFound();
            }
            List<Evolution> oneEvolution = evolutions.Where(
                e => e.EvolutionCondition == condition 
                && e.EvolvesInto == evolvesInto).ToList();
            Evolution theEvolution = oneEvolution[0];
            return View(theEvolution);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/05/05
        /// 
        /// method used to setup an edit view for editing pokemon.
        /// </summary>
        /// <param name="updatedEvolution">the new evolution object 
        /// replacing the old evolution object</param>
        /// <param name="oldConditon">the original condition of the old evolution</param>
        /// <param name="oldEvolvesInto">the original evolves into variable of the old evolution</param>
        [HttpPost]
        public ActionResult EvolutionEdit(Evolution updatedEvolution, string oldConditon, string oldEvolvesInto)
        {
            List<Evolution> evolutions = _pokemonManager.RetrieveEvolutionByReactant(updatedEvolution.Reactant);
            List<Evolution> oneEvolution = evolutions.Where(
                e => e.EvolutionCondition == oldConditon
                && e.EvolvesInto == oldEvolvesInto).ToList();
            Evolution outdatedEvolution = oneEvolution[0];
            if (outdatedEvolution == null)
            {
                return HttpNotFound();
            }
            if (!updatedEvolution.EvolvesInto.isValidPokemonName())
            {
                string error = "Invalid Evolves Into.";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            try
            {
                _pokemonManager.EditEvolution(outdatedEvolution, updatedEvolution);
                return RedirectToAction("AllEvolutions", "Evolution");
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        //-----------------------------------Deleting Evolutions-----------------------------------

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/03/20
        /// 
        /// method used to set up a form to see if the user wants to delete the pokemon.
        /// </summary>
        /// <param name="reactant">the name of the pokemon being evolved</param>
        /// <param name="condition">how the pokmeon evolves</param>
        /// <param name="evolvesInto">the pokmeon being evolved into</param>
        // GET: Pokemon
        public ActionResult TryEvolutionDelete(string reactant, string condition, string evolvesInto)
        {
            List<Evolution> evolutions = _pokemonManager.RetrieveEvolutionByReactant(reactant);
            List<Evolution> oneEvolution = evolutions.Where(
                e => e.EvolutionCondition == condition
                && e.EvolvesInto == evolvesInto).ToList();
            Evolution theEvolution = oneEvolution[0];
            return View(theEvolution);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/28
        /// 
        /// method used to delete a pokemon .
        /// </summary>
        /// <param name="reactant">the name of the pokemon being evolved</param>
        /// <param name="condition">how the pokmeon evolves</param>
        /// <param name="evolvesInto">the pokmeon being evolved into</param>
        /// <param name="delete">the users choice of deleting the evolution</param>
        [HttpPost]
        public ActionResult Delete(bool delete, string reactant, string condition, string evolvesInto)
        {
            if (delete)
            {
                try
                {
                    List<Evolution> evolutions = _pokemonManager.RetrieveEvolutionByReactant(reactant);
                    List<Evolution> oneEvolution = evolutions.Where(
                        e => e.EvolutionCondition == condition
                        && e.EvolvesInto == evolvesInto).ToList();
                    Evolution theEvolution = oneEvolution[0];

                    _pokemonManager.RemoveEvolution(theEvolution.Reactant, 
                        theEvolution.EvolutionCondition, theEvolution.EvolvesInto);
                    return RedirectToAction("AllEvolutions", "Evolution");
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    return RedirectToAction("Error", "Home", new { errorMessage = error });
                }
            }
            else
            {
                return RedirectToAction("AllEvolutions", "Evolution");
            }
        }



        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/05/05
        /// 
        /// method used to clear the pokemon manager.
        /// </summary>
        /// <param name="disposing">true or false to clear the pokemon manager</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _pokemonManager = null;
            }
            base.Dispose(disposing);
        }

    }
}