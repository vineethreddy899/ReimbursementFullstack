using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reimbursement.Domain.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string PanCardNumber { get; set; }

        [Required]
        public string Bank { get; set; }

        [Required]
        public string BankAccountNumber { get; set; }

        [Required]
        public bool isApprover { get; set; } = false;
    }
}
