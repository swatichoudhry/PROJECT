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
            return View();
        }
      
        [HttpPost]
        public ActionResult AdminCultural()
        {
            //TempData["Message"] = "You clicked Culturals!";
            return View();
        }
       
        public ActionResult Fixtures()
        {
            ViewBag.Sport = new SelectList(entity.Sport_tournamentlist, "sports_evid", "sport_name");

            return View();
        }

        public ActionResult TeamList(int Sid)
        {
            int finalcount = entity.Teams.Count(d => d.sp_id == Sid);
            ViewBag.Message = finalcount;


            int[,] rounda = GenerateRoundRobin(finalcount);
            ViewBag.RobinFix = rounda;

            return PartialView("TeamList");
        }
        private int[,] GenerateRoundRobin(int num_teams)
        {
            if (num_teams % 2 == 0)
                return GenerateRoundRobinEven(num_teams);
            else
                return GenerateRoundRobinOdd(num_teams);
        }
        private const int BYE = -1;

        private int[,] GenerateRoundRobinOdd(int num_teams)
        {
            int n2 = (int)((num_teams - 1) / 2);
            int[,] results = new int[num_teams, num_teams];


            int[] teams = new int[num_teams];
            for (int i = 0; i < num_teams; i++) teams[i] = i;


            for (int round = 0; round < num_teams; round++)
            {
                for (int i = 0; i < n2; i++)
                {
                    int team1 = teams[n2 - i];
                    int team2 = teams[n2 + i + 1];
                    results[team1, round] = team2;
                    results[team2, round] = team1;
                }

                // Set the team with the bye.
                results[teams[0], round] = BYE;

                // Rotate the array.
                RotateArray(teams);
            }

            return results;
        }

        // Rotate the entries one position.
        private void RotateArray(int[] teams)
        {
            int tmp = teams[teams.Length - 1];
            Array.Copy(teams, 0, teams, 1, teams.Length - 1);
            teams[0] = tmp;
        }

        private int[,] GenerateRoundRobinEven(int num_teams)
        {
            // Generate the result for one fewer teams.
            int[,] results = GenerateRoundRobinOdd(num_teams - 1);

            // Copy the results into a bigger array,
            // replacing the byes with the extra team.
            int[,] results2 = new int[num_teams, num_teams - 1];
            for (int team = 0; team < num_teams - 1; team++)
            {
                for (int round = 0; round < num_teams - 1; round++)
                {
                    if (results[team, round] == BYE)
                    {
                        // Change the bye to the new team.
                        results2[team, round] = num_teams - 1;
                        results2[num_teams - 1, round] = team;
                    }
                    else
                    {
                        results2[team, round] = results[team, round];
                    }
                }
            }

            return results2;
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
