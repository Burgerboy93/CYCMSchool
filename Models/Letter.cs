using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CYCMSchool.Models
{
    public class Letter
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        [Required]
        [ForeignKey("Bank")]
        [Display(Name = "Account Name")]
        public int BankID { get; set; }

        [Display(Name = "Letter Reference")]
        [NotMapped]
        public string LetterRef
        {
            get
            {

               string reference = $"{CreatedDate.Year.ToString()}{Student?.LastName ?? string.Empty}{Id.ToString()}";

                return reference;
            }
        }

        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [Required]
        [ForeignKey("EmailSignature")]
        public int EmailSignatureId { get; set; }

        public int? StudentId { get; set; }

        public virtual Bank Bank { get; set; }

        public virtual EmailSignature EmailSignature { get; set; }

        public virtual Student Student { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
