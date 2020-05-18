using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GuidMethodWorking.Models;
using Microsoft.AspNet.Identity;

namespace GuidMethodWorking.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private string userId;
        // GET: Employees
        public ActionResult Index()
        {
            userId = GetCurrentUserId();
            // this is to restrict users to theres only.
            var employees = db.Employees.Include(e => e.User).Where(x => x.UserId == userId);
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            userId = GetCurrentUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.FirstOrDefault(x=>x.EmployeeId == id && x.UserId == userId);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            userId = GetCurrentUserId();
           // ViewBag.UserId = userId;
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,Name,Email,PhoneNumber,Address,UserId")] Employee employee)
        {
            userId = GetCurrentUserId();
            employee.UserId = userId;
            ModelState.Clear();
            TryValidateModel(employee);
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.UserId = userId;
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            userId = GetCurrentUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.FirstOrDefault(x => x.EmployeeId == id && x.UserId == userId);
            if (employee == null)
            {
                return HttpNotFound();
            }

           // ViewBag.UserId = userId;
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,Name,Email,PhoneNumber,Address,UserId")] Employee employee)
        {
            userId = GetCurrentUserId();

            var existing = db.Employees.FirstOrDefault(x => x.EmployeeId == employee.EmployeeId && x.UserId == userId);
            if (existing == null) return HttpNotFound();
            employee.UserId = userId;
            ModelState.Clear();
            TryValidateModel(employee);
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           // ViewBag.UserId = userId;
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            userId = GetCurrentUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var existing = db.Employees.FirstOrDefault(x => x.EmployeeId == id && x.UserId == userId);
            if (existing == null) return HttpNotFound();
            return View(existing);

            //Employee employee = db.Employees.FirstOrDefault(x => x.EmployeeId == id && x.UserId == userId);
            //if (employee == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            userId = GetCurrentUserId();
            
            Employee employee = db.Employees.FirstOrDefault(x => x.EmployeeId == id && x.UserId == userId);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            userId = GetCurrentUserId();
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        protected string GetCurrentUserId()
        {
            return User.Identity.GetUserId();
        }

    }
}
