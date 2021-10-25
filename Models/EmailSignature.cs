using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CYCMSchool.Models
{
    public class EmailSignature
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Email Signature")]
        public string Signature { get; set; }

        public virtual ICollection<Letter> Letters { get; set; }
    }
}
