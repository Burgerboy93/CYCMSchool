using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CYCMSchool.Models
{
    public class Tutor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Contact Email")]
        public string ContactEmail { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get 
            {
                return FirstName + " " + LastName; 
            }
        }

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
