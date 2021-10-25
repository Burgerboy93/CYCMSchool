using System;
using System.Collections.Generic;
using PagedList;
using System.Linq;
using System.Threading.Tasks;

namespace CYCMSchool.Models.ViewModels
{
    public class StudentIndexData
    {
        public int StudentID { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Lesson> Lessons { get; set; }
        public IEnumerable<Letter> Letters { get; set; }
    }
}
