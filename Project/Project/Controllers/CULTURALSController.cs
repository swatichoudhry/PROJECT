using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class CULTURALSController : Controller
    {
        // GET: CULTURALS
        ProjectEntities1 entity = new ProjectEntities1();
        public ActionResult Culturals()
        {
            ViewBag.events = new SelectList(entity.Cultural_eventlist, "cul_evid", "event_name");
            return View();
        }
        public ActionResult addini(int id)
        {
            Cultural_reg cultural_Reg = new Cultural_reg();
            var cd = Session["username"].ToString();
            List<User> qw = entity.Users.Where(x => x.username == cd).ToList();
            var ew = qw[0].uid;

            if (entity.Cultural_reg.Any(d => d.cultural_id == id && d.userid == ew))
            {
                ViewBag.Message = "Can't Register for same sport twice";
            }
            else
            {
                cultural_Reg.cultural_id = id;
                cultural_Reg.userid = qw[0].uid;
                var av = entity.Cultural_eventlist.Find(id);
                cultural_Reg.event_name = av.event_name;
                entity.Cultural_reg.Add(cultural_Reg);
                ViewBag.Message = "Registered!";
                entity.SaveChanges();
            }

            return PartialView("addini");
        }

        public ActionResult _mypartial(int Cid)
        {

            List<Cultural_eventlist> modelll = entity.Cultural_eventlist.Where(d => d.cul_evid == Cid).ToList();
            ViewBag.ASQ = modelll;
            ViewBag.PSP = modelll[0].cul_evid;
            return PartialView("_mypartial");
        }


    }
}