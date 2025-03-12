using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reimbursement.Application.DTOs
{
    public class UpdateReimbursementDTO
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }
        [Required]
        public string ReimbursementType { get; set; }
        [Required]
        public double RequestedValue { get; set; }
        [Required]
        public string Currency { get; set; }

        public string CreatorId { get; set; }

        public string ImagePath { get; set; }
    }
}
