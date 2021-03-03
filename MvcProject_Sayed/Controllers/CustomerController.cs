using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;


using MvcProject_Sayed.Models;
using MvcProject_Sayed.Models.ViewModel;

using AutoMapper;
using PagedList;
using System.Data.Entity;


namespace MvcProject_Sayed.Controllers
{
    public class CustomerController : Controller
    {
        private MvcProject_SayedEntities db = new MvcProject_SayedEntities();

        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var customers = from t in db.Customers
                             select t;
            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(t => t.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    customers = customers.OrderByDescending(t => t.Name);
                    break;
                default:
                    customers = customers.OrderBy(t => t.Name);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(customers.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var query = db.Customers.Single(t => t.CustomerId == id);
            var customer = Mapper.Map<Customer, CustomerVM>(query);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerVM customerVM)
        {
            if (ModelState.IsValid)
            {
                var customer = Mapper.Map<Customer>(customerVM);
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customerVM);
        }

        // GET: Edit
        public ActionResult Edit(int? id)
        {
            var query = db.Customers.Single(t => t.CustomerId == id);
            var customer = Mapper.Map<Customer, CustomerVM>(query);
            return View(customer);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerVM customerVM)
        {
            if (ModelState.IsValid)
            {
                var customer = Mapper.Map<Customer>(customerVM);
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerVM);
        }

        // GET: Delete

        public ActionResult Delete(int? id)
        {
            var query = db.Customers.Single(t => t.CustomerId == id);
            var customer = Mapper.Map<Customer, CustomerVM>(query);
            return View(customer);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, CustomerVM customerVM)
        {
            var query = db.Customers.Single(t => t.CustomerId == id);
            var customer = Mapper.Map<Customer, CustomerVM>(query);
            db.Customers.Remove(query);  // 
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Partial_Test()
        {
            return View(db.Customers.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}