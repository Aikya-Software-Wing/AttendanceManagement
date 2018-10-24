using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Runtime;
using AttendanceManagement.Models;

namespace AttendanceManagement.Controllers
{
    public class InternalsController : Controller
    {
        private AttendanceManagementDBEntities1 db = new AttendanceManagementDBEntities1();
        // GET: Internals
        public ActionResult Index()
        {
            return View();
        }

        // GET: Internals/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Internals/Create
        public ActionResult Create()
        {
            LoadViewBag();
            return View();
        }
            
        private void LoadViewBag() {
            var user = db.AspNetUsers.FirstOrDefault(u => u.Email == User.Identity.Name);
            var teacher = db.Teachers.FirstOrDefault(u => u.REFID == user.Id);
            var teacher_Teaches_Students = db.Teacher_Teaches_Student.Where(t => t.Teacher_TID == teacher.TID);
            var distinctSubjects = new HashSet<Subject>();
            foreach (var tts in teacher_Teaches_Students)
            {
                distinctSubjects.Add(tts.Subject);
            }
            SelectList subjects = new SelectList(distinctSubjects, "SubCode", "SubCode");
            ViewBag.SubCode = subjects;            
        }

        // POST: Internals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubCode,InternalNumber,QuestionNumber,Marks,CO")] TestDetailsViewModel testDetails)
        {
            var user = db.AspNetUsers.FirstOrDefault(u => u.Email == User.Identity.Name);
            var teacher = db.Teachers.FirstOrDefault(u => u.REFID == user.Id);
            var internalNumber = testDetails.InternalNumber;            
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection("Data Source=DEV\\SQLEXPRESS;Initial Catalog=AttendanceManagementDB;Integrated Security=True"))
                {
                    var subject = testDetails.SubCode[0].ToString();
                    if (subject != null)
                    {
                        var tableName = teacher.Name + "_" + subject;
                        string createtableCommandString = "IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" +tableName+"]') AND type in (N'U'))" +
                            " BEGIN CREATE TABLE [dbo].["+ tableName + "](Id int IDENTITY PRIMARY KEY, InternalNo int not null, QNo varchar(3) not null, Marks int, CO int) END";
                        SqlCommand command = new SqlCommand(createtableCommandString, connection);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        int count = testDetails.QuestionNumber.Count();
                        for (int i=0; i<count; i++) {
                            string qno = testDetails.QuestionNumber[i];
                            int marks = testDetails.Marks[i];
                            int co = testDetails.CO[i];
                            string insertCommandString = "INSERT INTO " + tableName + " (InternalNo,QNo,Marks,CO) VALUES (" + internalNumber + ",'" + qno + "'," + marks + "," + co + ")";
                            SqlCommand insertCommand = new SqlCommand(insertCommandString, connection);
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            LoadViewBag();
            return View(testDetails);
        }

        // GET: Internals/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Internals/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Internals/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Internals/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
