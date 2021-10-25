using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CYCMSchool.Models
{
    public enum Family
    {
        Woodwind =1,
        Brass = 2,
        Percussion = 3,
        String = 4,
        Piano = 5
    }
    public class Instrument
    {

        public int Id { get; set; }

        [Required]
        [Display(Name = "Instrument Name")]
        public string InstrumentName { get; set; }

        [Display(Name = "Instrument Family")]
        public Family InstrumentFamily { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }


    }
}
