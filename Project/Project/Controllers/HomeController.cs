using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;


namespace Project.Controllers
{
    public class HomeController : Controller
    { 
  
      ProjectEntities1 entity = new ProjectEntities1();

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
            bool isValidUser = entity.Users.Any(u => u.username ==
            user.username && u.u_password == user.u_password);

            if (isValidUser)
            {
                    Session["username"] = user.username;             
                System.Web.Security.FormsAuthentication.SetAuthCookie(user.username, false);
                    //return View();
                    return RedirectToAction("UserLogin", "Home");
                }
        }
        ModelState.AddModelError("", "Invalid Username or Password");
            return View();
    }
    public ActionResult LoginAdmin()
    {
        return View();
    }

    [HttpPost]
    public ActionResult LoginAdmin(Admin admin)
    {
        if (ModelState.IsValid)
        {
            bool isValidUser = entity.Admins.Any(u => u.admin_fullname ==
            admin.admin_fullname && u.admin_password == admin.admin_password);

            if (isValidUser)
            {   
                
                System.Web.Security.FormsAuthentication.SetAuthCookie(admin.admin_fullname, false);             
                return RedirectToAction("AdminLogin", "Home");
            }
        }
        ModelState.AddModelError("", "Invalid Username or Password");
        return View();
    }
        public ActionResult UserLogin()
        {        
            return View();
        }

        public ActionResult AdminLogin()
        {
            return View();
            
        }
        [HttpPost]
        public ActionResult AdminSport()
        {
           // TempData["Message"] = "You clicked Sports!";
            return RedirectToAction("AdminSportTournament");
        }
        public ActionResult AdminSportTournament()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminSportTournament(Sport_tournamentlist add)
        {
            
            if (ModelState.IsValid)
            {
                entity.Sport_tournamentlist.Add(add);
                entity.SaveChanges();
                return RedirectToAction("AdminLogin", "Home");
            }
            ModelState.AddModelError("", "Invalid input");
            return View();

        }
        [HttpPost]
        public ActionResult AdminCultural()
        {
            //TempData["Message"] = "You clicked Culturals!";
            return RedirectToAction("AdminCulturalEvent");
        }
        public ActionResult AdminCulturalEvent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminCulturalEvent(Cultural_eventlist add)
        {
                      
            if (ModelState.IsValid)
            {
                entity.Cultural_eventlist.Add(add);
                entity.SaveChanges();
                return RedirectToAction("AdminLogin", "Home");
            }
            ModelState.AddModelError("", "Invalid input");
            return View();

        }

       


        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("SessionExpiry");
        }
        public ActionResult SessionExpiry()
        {
            return View();
        }
    }
}
