using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CYCMSchool.Models.ViewModels
{
    public class AssignedLessonsData
    {
        public int LessonID { get; set; }
        public DateTime LessonDate { get; set; }
        public bool Selected { get; set; }
    }
}
