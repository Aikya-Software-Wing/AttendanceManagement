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
    public class Teacher_Teaches_StudentController : Controller
    {
        private AttendanceManagementDBEntities db = new AttendanceManagementDBEntities();

        // GET: Teacher_Teaches_Student
        public ActionResult Index()
        {
            var teacher_Teaches_Student = db.Teacher_Teaches_Student.Include(t => t.Student).Include(t => t.Subject).Include(t => t.Teacher);
            return View(teacher_Teaches_Student.ToList());
        }

        // GET: Teacher_Teaches_Student/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher_Teaches_Student teacher_Teaches_Student = db.Teacher_Teaches_Student.Find(id);
            if (teacher_Teaches_Student == null)
            {
                return HttpNotFound();
            }
            return View(teacher_Teaches_Student);
        }

        // GET: Teacher_Teaches_Student/Create
        public ActionResult Create()
        {
            ViewBag.Student_USN = new SelectList(db.Students, "USN", "Name");
            ViewBag.Subject_SubCode = new SelectList(db.Subjects, "SubCode", "Name");
            ViewBag.Teacher_TID = new SelectList(db.Teachers, "TID", "Name");
            return View();
        }

        // POST: Teacher_Teaches_Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Subject_SubCode,Student_USN,Teacher_TID")] Teacher_Teaches_Student teacher_Teaches_Student)
        {
            if (ModelState.IsValid)
            {
                db.Teacher_Teaches_Student.Add(teacher_Teaches_Student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Student_USN = new SelectList(db.Students, "USN", "Name", teacher_Teaches_Student.Student_USN);
            ViewBag.Subject_SubCode = new SelectList(db.Subjects, "SubCode", "Name", teacher_Teaches_Student.Subject_SubCode);
            ViewBag.Teacher_TID = new SelectList(db.Teachers, "TID", "Name", teacher_Teaches_Student.Teacher_TID);
            return View(teacher_Teaches_Student);
        }

        // GET: Teacher_Teaches_Student/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher_Teaches_Student teacher_Teaches_Student = db.Teacher_Teaches_Student.Find(id);
            if (teacher_Teaches_Student == null)
            {
                return HttpNotFound();
            }
            ViewBag.Student_USN = new SelectList(db.Students, "USN", "Name", teacher_Teaches_Student.Student_USN);
            ViewBag.Subject_SubCode = new SelectList(db.Subjects, "SubCode", "Name", teacher_Teaches_Student.Subject_SubCode);
            ViewBag.Teacher_TID = new SelectList(db.Teachers, "TID", "Name", teacher_Teaches_Student.Teacher_TID);
            return View(teacher_Teaches_Student);
        }

        // POST: Teacher_Teaches_Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Subject_SubCode,Student_USN,Teacher_TID")] Teacher_Teaches_Student teacher_Teaches_Student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher_Teaches_Student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Student_USN = new SelectList(db.Students, "USN", "Name", teacher_Teaches_Student.Student_USN);
            ViewBag.Subject_SubCode = new SelectList(db.Subjects, "SubCode", "Name", teacher_Teaches_Student.Subject_SubCode);
            ViewBag.Teacher_TID = new SelectList(db.Teachers, "TID", "Name", teacher_Teaches_Student.Teacher_TID);
            return View(teacher_Teaches_Student);
        }

        // GET: Teacher_Teaches_Student/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher_Teaches_Student teacher_Teaches_Student = db.Teacher_Teaches_Student.Find(id);
            if (teacher_Teaches_Student == null)
            {
                return HttpNotFound();
            }
            return View(teacher_Teaches_Student);
        }

        // POST: Teacher_Teaches_Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Teacher_Teaches_Student teacher_Teaches_Student = db.Teacher_Teaches_Student.Find(id);
            db.Teacher_Teaches_Student.Remove(teacher_Teaches_Student);
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
