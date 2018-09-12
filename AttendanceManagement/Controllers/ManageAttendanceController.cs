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
        private static AttendanceViewModel attendanceViewModel = new AttendanceViewModel();

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
        public ActionResult Index(ClassViewModel classViewModel)
        {
           return RedirectToAction("AddAttendance", "ManageAttendance", new { departmentID = classViewModel.Department_DID, Semester = classViewModel.Sem, section = classViewModel.Section, slot = classViewModel.Slot, date = classViewModel.Date});           
        } 

        [HttpGet]
        public ActionResult AddAttendance(string departmentID, int Semester, string section, string slot, DateTime date)
        {
            attendanceViewModel.Slot = slot;
            attendanceViewModel.Date = date;
            var students = db.Students.Where(s => s.Department_DID == (departmentID)).Where(s => s.Sem == (Semester)).Where(s => s.Section == (section)).ToList();
            attendanceViewModel.Students = students;
            var subjectCode = students[0].Subject_SubCode;
            var query = db.Teacher_Teaches_Student.Where(s => s.Subject_SubCode == subjectCode).Select(t => t.Teacher_TID).ToList();
            attendanceViewModel.TeacherId = query[0];
            attendanceViewModel.SubjectCode = subjectCode;
            return View(attendanceViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAttendance([Bind(Include = "IsPresent")] AttendanceViewModel model)
        {
            Attendance attendance = new Attendance();
            attendance.Date = attendanceViewModel.Date;
            attendance.Slot = attendanceViewModel.Slot;
            attendance.Subject_SubCode = attendanceViewModel.SubjectCode;
            attendance.Teacher_TID = attendanceViewModel.TeacherId;
            attendance.Teacher = db.Teachers.Find(attendance.Teacher_TID);
            attendance.Subject = db.Subjects.Find(attendance.Subject_SubCode);
            int countOfStudents = attendanceViewModel.Students.Count;
            var checkBoxes = model.IsPresent.ToList();
            var students = attendanceViewModel.Students;
            for(int i=0; i<countOfStudents; i++)
            {
                if ( checkBoxes[i] == true) {
                    attendance.Student_USN = students[i].USN;
                    attendance.Student = db.Students.Find(students[i].USN);
                    db.Attendances.Add(attendance);
                    db.SaveChanges();
                }
            }
       /*     if (ModelState.IsValid)
            {
                db.Attendances.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }*/
            return View(attendance);
        }

        public ActionResult Invalid()
        {
            return View();
        }
       
    }
}