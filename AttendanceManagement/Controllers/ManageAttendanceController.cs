using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttendanceManagement.Models;
using AttendanceManagement.ViewModel;
using System.Data.Entity;
using System.Collections;

namespace AttendanceManagement.Controllers
{
    public class ManageAttendanceController : Controller
    {
        private AttendanceManagementDBEntities db = new AttendanceManagementDBEntities();

        // GET: ManageAttendance/Index
        public ActionResult Index()
        {

            // new SelectList(db.students, "field1", "field2")
            // field1 denotes which field from db has to be selected
            // field2 denotes which attribute corresponding to field1 has to be passed to controller URL

            ViewBag.Department_DID = new SelectList(db.Departments, "DID", "Name");
            ViewBag.Section = new SelectList(db.Students, "Section", "Section");
            ViewBag.Sem= new SelectList(db.Students, "Sem", "Sem");
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Student student)
        {
           return RedirectToAction("AddAttendance", "ManageAttendance", new { departmentID = student.Department_DID, Semester = student.Sem, section = student.Section, slot = student.Slot, date = student.Date});           
        } 

        public ActionResult AddAttendance(string departmentID, int Semester, string section, int slot, DateTime date)
        {
            AttendanceViewModel attendanceViewModel = new AttendanceViewModel
            {
                Slot = slot,
                Date = date
            };
            var students = db.Students.Where(s => s.Department_DID == (departmentID)).Where(s => s.Sem == (Semester)).Where(s => s.Section == (section)).ToList();
            attendanceViewModel.Students = students;
            var subjectCode = students[0].Subject_SubCode;
            var query = db.Teacher_Teaches_Student.Where(s => s.Subject_SubCode == subjectCode).Select(t => t.Teacher_TID).ToList();
            attendanceViewModel.TeacherId = query[0];
            return View(attendanceViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAttendance([Bind(Include = "Student_USN,Teacher_TID,Subject_Subcode,Date,Slot")] AttendanceViewModel attendanceViewModel)
        {
            Attendance attendance = new Attendance();
            attendance.Date = attendanceViewModel.Date;
            attendance.Slot = attendanceViewModel.Slot;
            if (ModelState.IsValid)
            {
                db.Attendances.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(attendance);
        }

        public ActionResult Invalid()
        {
            return View();
        }
       
    }
}