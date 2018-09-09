using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceManagement.Models;

namespace AttendanceManagement.ViewModel
{
    public class AttendanceViewModel
    {
        public List<Student> Students { get; set; }
        public string TeacherId { get; set; }
        public System.DateTime Date { get; set; }
        public int Slot { get; set; }
    }
}