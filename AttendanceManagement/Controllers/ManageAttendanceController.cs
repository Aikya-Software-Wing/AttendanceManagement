using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttendanceManagement.Models;
using System.Data.Entity;

namespace AttendanceManagement.Controllers
{
    public class ManageAttendanceController : Controller
    {
        private AttendanceManagementDBEntities db = new AttendanceManagementDBEntities();

        // GET: ManageAttendance/Index
        public ActionResult Index()
        {

            //    List<Department> list = db.Departments.ToList();
            //   ViewBag.DepartmentList = new SelectList(list, "Department_DID", "Department_Name"); ;

          

            ViewBag.Department_DID = new SelectList(db.Departments, "DID", "Name");
            ViewBag.Section = new SelectList(db.Students, "USN", "Section");
            ViewBag.Semester = new SelectList(db.Students, "USN", "Sem");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
          public ActionResult Index(Student student)
          {
           return RedirectToAction("AddAttendance", "ManageAttendance", new { branch = student.Department_DID, Semester = student.Sem, section = student.Section});           
        } 

        public ActionResult AddAttendance(string branch, int Semester, string section)
        {
            var getStudent = db.Students.Find(branch);
            var students = db.Students.Where(s => s.Department_DID == (branch)).ToList();
            return View(students);
        }

        public ActionResult SaveAttendance([Bind(Include = "Student_USN,Teacher_TID,Subject_SubCode,Date,Slot,Status")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Attendances.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           return View();
        }

    }
}