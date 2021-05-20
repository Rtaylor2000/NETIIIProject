using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebPresentation.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/19
        /// 
        /// a method for displaying the admins interface
        /// </summary>
        // GET: Admin
        public ActionResult Index()
        {
            var userManager =
                HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var users = userManager.Users.OrderBy(n => n.Surname);

            return View(users);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/19
        /// 
        /// a method for displaying the details of a member
        /// </summary>
        /// <param name="id">the id of the member being showed</param>
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userManager =
                HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);

            if (user == null)
            {
                return HttpNotFound();
            }

            var allRoles = new string[] { "admin", "researcher", "user" };
            var roles = userManager.GetRoles(id);
            if (roles.Count() == 0) 
            {
                roles.Add("user");
            }
            var noRoles = allRoles.Except(roles);

            ViewBag.Roles = roles;
            ViewBag.NoRoles = noRoles;

            return View(user);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/04/19
        /// 
        /// a method for changing the role of a member
        /// </summary>
        /// <param name="id">the id of the member being edited</param>
        /// <param name="newRole">the name of the role being given</param>
        /// <param name="oldRole">the id of the role being changed</param>
        public ActionResult ChangeRole(string id, string newRole, string oldRole)
        {
            // identity system
            var userManager =
                HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);


            if (user.Email.Contains("admin") && oldRole == "admin")
            {
                return RedirectToAction("Details", new { Id = id });
            }

            userManager.AddToRole(id, newRole);
            userManager.RemoveFromRole(id, oldRole);

            //old system - we need the old userId
            var oldMemberManager = new Logic.MemberManager();
            var oldMember = oldMemberManager.RetrieveMemberByActive().Find(e => e.Email == user.Email);

            if (oldMember != null)
            {
                //then call the syncronize method
                oldMemberManager.EditMemberProfile(oldMember, oldMember, newRole, oldRole);
            }
            return RedirectToAction("Details", new { Id = id });
        }
    }
}