using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CYCMSchool.Models.ViewModels
{
    public class TutorsIndexData
    {
        public IEnumerable<Tutor> Tutors { get; set; }
        public IEnumerable<Lesson> Lessons { get; set; }
        
    }
}
