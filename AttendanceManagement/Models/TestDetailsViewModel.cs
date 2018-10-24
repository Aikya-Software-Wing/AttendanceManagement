using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AttendanceManagement.Models
{
    public class TestDetailsViewModel
    {
        public List<string> SubCode { get; set; }
        public int InternalNumber { get; set; }
        public List<string> QuestionNumber { get; set; }
        public List<int> Marks { get; set; }
        public List<int> CO { get; set; }
    }
}