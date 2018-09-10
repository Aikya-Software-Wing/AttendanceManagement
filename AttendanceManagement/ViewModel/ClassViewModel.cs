using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceManagement.ViewModel
{
    public class ClassViewModel
    {
        public string Department_DID { get; set; }
        public int Sem { get; set; }
        public string Section { get; set; }
        public string Slot { get; set; }
        public System.DateTime Date { get; set; }
    }
}