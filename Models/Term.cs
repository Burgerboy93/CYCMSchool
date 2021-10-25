using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CYCMSchool.Models
{
    public class Term
    {
        public int Id { get; set; }

        [Required]
        [Range(1,4)]
        [Display(Name = "Term Number")]
        public int TermNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Term Start Date")]
        public DateTime TermStart { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Term End Date")]
        public DateTime TermEnd { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
