using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class SPORTSController : Controller
    {
        // GET: SPORTS
        ProjectEntities1 entity = new ProjectEntities1();
        public ActionResult Sports()
        {
            ViewBag.Sport = new SelectList(entity.Sport_tournamentlist, "sports_evid", "sport_name");

            return View();

        }
        public ActionResult _mypartial(int Cid)
        {
            
            List<Sport_tournamentlist> modelll = entity.Sport_tournamentlist.Where(d => d.sports_evid == Cid).ToList();
            ViewBag.ASQ = modelll;
            ViewBag.PSP = modelll[0].sports_evid;
            return PartialView("_mypartial");
        }
        public ActionResult addteam(int id)
        {

            Team team = new Team();

            team.sp_id = id;
            entity.Teams.Add(team);
            entity.SaveChanges();
           return RedirectToAction("Sports");
        }

        public ActionResult addini(int id)
        {
            

            Sports_reg sports_Reg = new Sports_reg();                    
            var cd = Session["username"].ToString();
            List<User> qw = entity.Users.Where(x => x.username == cd).ToList();
            var ew = qw[0].uid;
            
            if (entity.Sports_reg.Any(d => d.sport_id == id && d.userid == ew))
            {
                ViewBag.Message = "Cannot Register for same sport twice";
            }
            else
            {
                sports_Reg.sport_id = id;
                sports_Reg.userid = qw[0].uid;
                var av = entity.Sport_tournamentlist.Find(id);
                sports_Reg.sport_name = av.sport_name;
                entity.Sports_reg.Add(sports_Reg);
                ViewBag.Message = "Registered";
                entity.SaveChanges();
            }

            return View();
        }


    }
}