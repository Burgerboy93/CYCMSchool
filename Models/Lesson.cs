using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CYCMSchool.Models
{
    public class Lesson
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Lesson Date")]
        public DateTime LessonDate { get; set; }

        [Required]
        [ForeignKey("Student")]
        [Display(Name = "Student Name")]
        public int StudentID { get; set; }

        [Required]
        [ForeignKey("Tutor")]
        [Display(Name = "Tutor Name")]
        public int TutorID { get; set; }

        [Required]
        [ForeignKey("Instrument")]
        [Display(Name = "Instrument Name")]
        public int InstrumentID { get; set; }

        [Required]
        [ForeignKey("Duration")]
        [Display(Name = "Duration Time")]
        public int DurationID { get; set; }

        [Required]
        [ForeignKey("Term")]
        [Display(Name = "Term Number")]
        public int TermID { get; set; }
        public bool Paid { get; set; }

        public virtual Student Student { get; set; }

        public virtual Tutor Tutor { get; set; }

        public virtual Instrument Instrument { get; set; }

        public virtual Duration Duration { get; set; }

        public virtual Term Term { get; set; }

        public virtual ICollection<Letter> Letters { get; set; }
    }
}
