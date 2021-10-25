using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CYCMSchool.Models
{
    public class Duration
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Duration Time (Minutes)")]
        public int DurationTime { get; set; }

        [Required]
        public decimal Cost { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
