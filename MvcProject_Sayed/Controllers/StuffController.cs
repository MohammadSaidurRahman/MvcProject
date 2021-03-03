using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MvcProject_Sayed.Models;

namespace MvcProject_Sayed.Controllers
{
    public class StuffController : Controller
    {
        // GET: Stuff
        private MvcProject_SayedEntities _context;
        public StuffController()
        {
            _context = new MvcProject_SayedEntities();
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult List()
        {
            return Json(_context.Stuffs.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(Stuff user)
        {
            _context.Stuffs.Add(user);
            _context.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(int ID)
        {
            return Json(_context.Stuffs.FirstOrDefault(x => x.StuffID == ID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Stuff user)
        {
            var data = _context.Stuffs.FirstOrDefault(x => x.StuffID == user.StuffID);
            if (data != null)
            {
                data.StuffName = user.StuffName;
                data.State = user.State;
                data.Country = user.Country;
                data.Age = user.Age;
                _context.SaveChanges();
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            var data = _context.Stuffs.FirstOrDefault(x => x.StuffID == ID);
            _context.Stuffs.Remove(data);
            _context.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}