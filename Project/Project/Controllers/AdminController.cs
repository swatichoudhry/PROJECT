using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        ProjectEntities1 entity = new ProjectEntities1();
        // GET: Admin
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

        public ActionResult CreateTeam()
        {
            ViewBag.Sport = new SelectList(entity.Sport_tournamentlist, "sports_evid", "sport_name");

            return View();
        }
        public ActionResult _TotTeam(int Sid)
        {
            int a = entity.Sports_reg.Count(d => d.sport_id == Sid);
            int count_teams = 0;
            if (Sid == 1 || Sid == 2)
            {
                count_teams = a / 11;
            }
            else if (Sid == 3)
            {
                count_teams = a / 4;
            }
            Team team = new Team();
            for (int i = 1; i <= count_teams; i++)
            {
                int count = entity.Teams.Count(d => d.sp_id == Sid);
                team.t_no = count + 1;
                team.sp_id = Sid;
                entity.Teams.Add(team);
                entity.SaveChanges();
            }
            int finalcount = entity.Teams.Count(d => d.sp_id == Sid);
            ViewBag.Message = finalcount;
            return PartialView("_TotTeam");
        }

        public List<Event> GetEventList()
        {
            ProjectEntities1 entity = new ProjectEntities1();
            List<Event> events = entity.Events.ToList();
            return events;
        }


        public ActionResult AdminViewPortal()
        {
            ProjectEntities1 entity = new ProjectEntities1();
            ViewBag.EventList = new SelectList(GetEventList(), "event_id", "event_name");
            return View();
        }

        public ActionResult GetSportsList(int SLid)
        {
            ProjectEntities1 entity = new ProjectEntities1();

        
            if (SLid == 1)
            {
                ViewBag.Sportslist = new SelectList(entity.Sport_tournamentlist, "sports_evid", "sport_name");
                ViewBag.Message = "sport";
            }
            else
            {
                ViewBag.Sportslist = new SelectList(entity.Cultural_eventlist, "cul_evid", "event_name");
                ViewBag.Message = "event";
            }
            return PartialView("DisplaySportsList");
        }

        public ActionResult ListofParticipants(int sportsid)
        {
            ProjectEntities1 entity = new ProjectEntities1();

            List<Sports_reg> userids = entity.Sports_reg.Where(x => x.sport_id == sportsid).ToList();
            List<string> usernamelist = new List<string>();
            List<User> users = new List<User>();
            foreach (var item in userids)
            {
                users.Add(entity.Users.FirstOrDefault(x => x.uid == item.userid));
            }
            foreach (var item in users)
            {
                usernamelist.Add(item.username);
                Console.WriteLine(item.username);
            }

            ViewBag.listof = usernamelist;
            return PartialView("ListofParticipants");

        }
        public ActionResult ListofParticipants11(int event_id)
        {
            ProjectEntities1 entity = new ProjectEntities1();

            List<Cultural_reg> culuserids = entity.Cultural_reg.Where(x => x.eventid == event_id).ToList();
            List<string> culusernamelist = new List<string>();
            List<User> culusers = new List<User>();
            foreach (var item in culuserids)
            {
                culusers.Add(entity.Users.FirstOrDefault(x => x.uid == item.userid));

            }
            foreach (var item in culusers)
            {
                culusernamelist.Add(item.username);
                Console.WriteLine(item.username);
            }
            ViewBag.listof1 = culusernamelist;

            return PartialView("ListofParticipants11");
        }
    }
}