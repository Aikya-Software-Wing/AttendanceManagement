using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceManagement.ViewModel
{
    public class AttendanceViewModel
    {
      
        public string Student_USN { get; set; }
        public string Teacher_TID { get; set; }
        public string Subject_SubCode { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int Slot { get; set; }
        public char Status { get; set; }

      
        public List<string> Departments { get; set; }
        public List<string> Subjects { get; set; }
        public List<string> Teachers { get; set; }



            
     
}
}