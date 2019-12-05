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
            ViewBag.events = new SelectList(entity.Cultural_eventlist.ToList(), "event_name", "event_name");
            //ViewBag.events = new SelectList(entity.Cultural_eventlist, "event_name", "cul_evid");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Culturals (Cultural_reg obj, FormCollection form)

        {
            var cd = Session["username"].ToString();
            List<User> qw = entity.Users.Where(x => x.username == cd).ToList();          
            obj.userid = qw[0].uid;
            string event_name = form[1].ToString();
            Cultural_reg cultural_Reg = new Cultural_reg();

            Cultural_eventlist cultural_Eventlist = entity.Cultural_eventlist.FirstOrDefault(x => x.event_name == event_name);
            obj.eventid = cultural_Eventlist.cul_evid;
            obj.event_name = cultural_Eventlist.event_name;



            if (ModelState.IsValid)
            {


                entity.Cultural_reg.Add(obj);
                entity.SaveChanges();
                return RedirectToAction("Culturals");
            }

            return View();
        }


    }
}