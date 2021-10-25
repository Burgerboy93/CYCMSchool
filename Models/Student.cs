using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CYCMSchool.Models
{
    public enum Gender
    {
        Male = 1,
        Female = 2,
        NonBinary = 3
    }

    public class Student
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        public int Age
        {
            get
            {
                int age = DateTime.Now.Year - DateOfBirth.Year;

                if(DateTime.Today.Month < DateOfBirth.Month || (DateTime.Today.Month == DateOfBirth.Month && DateTime.Today.Day < DateOfBirth.Day))
                {
                    age--;
                }
                return age;    
            }
        }

        [Display(Name = "Student Gender")]
        [Range(1, int.MaxValue, ErrorMessage="Select a valid Gender")]
        public Gender StudentGender { get; set; }

        [StringLength(200)]
        [Display(Name = "Guardian's Name")]
        public string GuardianName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        [Display(Name = "Contact Email")]
        public string ContactEmail { get; set; }

        [Required, DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get 
            {
                return FirstName + " " + LastName;
            } 
        }
        public virtual ICollection<Lesson> Lessons { get; set; }

        public virtual ICollection<Letter> Letters { get; set; }
    }
}
