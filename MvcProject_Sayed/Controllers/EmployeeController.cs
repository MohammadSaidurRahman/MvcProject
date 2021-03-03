using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;
using System.IO;
using MvcProject_Sayed.Models;

namespace MvcProject_Sayed.Controllers
{
    [RoutePrefix("Information")]
    public class EmployeeController : Controller
    {
         [Route("List")]
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAll()
        {
            return View(GetAllEmployee());
        }

        IEnumerable<Emloyee> GetAllEmployee()
        {
            using (MvcProject_SayedEntities db = new MvcProject_SayedEntities())
            {
                return db.Emloyees.ToList<Emloyee>();
            }

        }

        public ActionResult AddOrEdit(int id = 0)
        {
            Emloyee emp = new Emloyee();
            if (id != 0)
            {
                using (MvcProject_SayedEntities db = new MvcProject_SayedEntities())
                {
                    emp = db.Emloyees.Where(x => x.EmloyeeID == id).FirstOrDefault<Emloyee>();
                }
            }
            return View(emp);
        }

        [HttpPost]
        public ActionResult AddOrEdit(Emloyee emp)
        {
            try
            {
                if (emp.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(emp.ImageUpload.FileName);
                    string extension = Path.GetExtension(emp.ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    emp.ImagePath = "~/AppFiles/Images/" + fileName;
                    emp.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
                }
                using (MvcProject_SayedEntities db = new MvcProject_SayedEntities())
                {
                    if (emp.EmloyeeID == 0)
                    {
                        db.Emloyees.Add(emp);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllEmployee()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (MvcProject_SayedEntities db = new MvcProject_SayedEntities())
                {
                    Emloyee emp = db.Emloyees.Where(x => x.EmloyeeID == id).FirstOrDefault<Emloyee>();
                    db.Emloyees.Remove(emp);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllEmployee()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}