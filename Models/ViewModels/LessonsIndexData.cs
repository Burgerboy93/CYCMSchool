using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CYCMSchool.Models.ViewModels
{
    public class LessonsIndexData
    {
        public IEnumerable<Lesson> Lessons { get; set; }
        public IEnumerable<Letter> Letters { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }
}
