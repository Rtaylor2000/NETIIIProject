/// <summary>
/// Ryan Taylor
/// Created: 2021/05/09
/// 
/// this controles all of the crud funtions for the pokemon object 
/// </summary>

using Logic;
using Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPresentation.Controllers
{
    public class TypeEvaluationController : Controller
    {
        PokemonManager _pokemonManager = new PokemonManager();
        // GET: TypeEvaluation
        public ActionResult TypeEvaluationSetUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TypeEvaluation(string AttackTypeOne, string AttackTypeTwo,
            string DeffenceTypeOne, string DeffenceTypeTwo)
        {
            if (!AttackTypeOne.isValidTypeOne())
            {
                string error = "Invalid Type One.";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            if (!AttackTypeTwo.isValidTypeTwo())
            {
                string error = "Invalid Type Two.(None is valid)";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            if (!DeffenceTypeOne.isValidTypeOne())
            {
                string error = "Invalid Type One.";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            if (!DeffenceTypeTwo.isValidTypeTwo())
            {
                string error = "Invalid Type Two.(None is valid)";
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            List<int> attackOneStats = _pokemonManager.RetrieveTypeStats(AttackTypeOne);
            List<int> attackTwoStats = new List<int>();
            if (AttackTypeTwo != null || AttackTypeTwo != "NONE" || AttackTypeTwo != AttackTypeOne)
            {
                attackTwoStats = _pokemonManager.RetrieveTypeStats(AttackTypeTwo);
            }


            return View();
        }
    }
}