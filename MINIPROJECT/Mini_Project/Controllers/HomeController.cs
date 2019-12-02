using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mini_Project.Models;

namespace Mini_Project.Controllers
{
    public class HomeController : Controller
    {
        moviesEntities entity = new moviesEntities();
         
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                bool isValidUser = entity.Users.Any(u => u.UserName.ToLower() ==
                user.UserName && u.Password == user.Password);

                if (isValidUser)
                {
                    System.Web.Security.FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToAction("", "");
                }
            }
            ModelState.AddModelError("", "Invalid Username or Password");
            return View();
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User registeruser)
        {
            if (ModelState.IsValid)
            {
                bool isValidUser = entity.Users.Any(u => u.UserName ==
                registeruser.UserName || u.EmailId == registeruser.EmailId || u.PhoneNumber == registeruser.PhoneNumber);

                if (!isValidUser)
                {
                    entity.Users.Add(registeruser);
                    entity.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
            ModelState.AddModelError("", "User already exist,Use another Username or Password or Email");
            return View();
        }


    }
}