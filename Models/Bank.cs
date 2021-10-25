using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CYCMSchool.Models
{
    public class Bank
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [Required]
        [RegularExpression(@"^\d{3}-?\d{3}$", ErrorMessage ="Please use a valid BSB")]
        public string BSB { get; set; }

        [Required]
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Required]
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        public virtual ICollection<Letter> Letters { get; set; }
    }
}
