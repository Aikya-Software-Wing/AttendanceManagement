using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AttendanceManagement.Models;

namespace AttendanceManagement.Controllers
{
    public class AttendancesController : Controller
    {
        private AttendanceManagementDBEntities db = new AttendanceManagementDBEntities();

        // GET: Attendances
        public ActionResult Index()
        {
            var attendances = db.Attendances.Include(a => a.Student).Include(a => a.Subject).Include(a => a.Teacher);
            return View(attendances.ToList());
        }

        // GET: Attendances/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // GET: Attendances/Create
        public ActionResult Create()
        {
            ViewBag.Student_USN = new SelectList(db.Students, "USN", "Name");
            ViewBag.Subject_SubCode = new SelectList(db.Subjects, "SubCode", "Name");
            ViewBag.Teacher_TID = new SelectList(db.Teachers, "TID", "Name");
            return View();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Student_USN,Teacher_TID,Subject_SubCode,Date,Slot,Status")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Attendances.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Student_USN = new SelectList(db.Students, "USN", "Name", attendance.Student_USN);
            ViewBag.Subject_SubCode = new SelectList(db.Subjects, "SubCode", "Name", attendance.Subject_SubCode);
            ViewBag.Teacher_TID = new SelectList(db.Teachers, "TID", "Name", attendance.Teacher_TID);
            return View(attendance);
        }

        // GET: Attendances/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.Student_USN = new SelectList(db.Students, "USN", "Name", attendance.Student_USN);
            ViewBag.Subject_SubCode = new SelectList(db.Subjects, "SubCode", "Name", attendance.Subject_SubCode);
            ViewBag.Teacher_TID = new SelectList(db.Teachers, "TID", "Name", attendance.Teacher_TID);
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Student_USN,Teacher_TID,Subject_SubCode,Date,Slot,Status")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Student_USN = new SelectList(db.Students, "USN", "Name", attendance.Student_USN);
            ViewBag.Subject_SubCode = new SelectList(db.Subjects, "SubCode", "Name", attendance.Subject_SubCode);
            ViewBag.Teacher_TID = new SelectList(db.Teachers, "TID", "Name", attendance.Teacher_TID);
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Attendance attendance = db.Attendances.Find(id);
            db.Attendances.Remove(attendance);
            db.SaveChanges();
            return RedirectToAction("Index");
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
