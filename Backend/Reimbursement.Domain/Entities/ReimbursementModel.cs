using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reimbursement.Domain.Entities
{
    public class ReimbursementModel
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ReimbursementType { get; set; }

        public double RequestedValue { get; set; }

        public double ApprovedValue { get; set; }

        public string RequestPhase { get; set; }

        public string Currency { get; set; }

        public string CreatorId { get; set; }

        public virtual User Creator { get; set; }

        public string ApprovedBy { get; set; }

        public string InternalNotes { get; set; }

        public double ApprovedAmount { get; set; }

        public string ImagePath { get; set; }
    }
}
