using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reimbursement.Domain.Entities
{
    public class SignUp
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [MinLength(8)]
        public string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(10), MaxLength(10)]
        public string PanCardNumber { get; set; }

        [Required]
        public string Bank { get; set; }

        [Required]
        [StringLength(12),MaxLength(12)]
        public string BankAccountNumber { get; set; }

        public bool isApprover { get; set; } = false;
    }
}
