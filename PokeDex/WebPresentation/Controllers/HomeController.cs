﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPresentation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/09
        ///
        /// a method for showing errors
        /// </summary>
        /// 
        ///<param name="errorMessage">The error message to be desplayed</param>
        ///<returns>a view to display an error</returns>
        public ActionResult Error(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;

            return View();
        }
    }
}