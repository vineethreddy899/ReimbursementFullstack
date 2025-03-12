using Reimbursement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reimbursement.Application.ServiceInterface
{
    public interface IReimbursementService
    {
        public Task<ReimbursementModel> GetId(Guid Id);

        public Task<List<ReimbursementModel>> GetAllReimbursements();
        
        public Task<List<ReimbursementModel>> GetAllApproved(string mail, string type);
        
        public Task<List<ReimbursementModel>> GetAllPending(string mail, string type);
        
        public Task<List<ReimbursementModel>> GetAllDeclined(string mail, string type);
        
        public Task<string> GetCreatorId(Guid Id);

        public Task<List<ReimbursementModel>> MyReimbursements(string creatorId);
        
        public Task<bool> Insert(ReimbursementModel entity);
        
        public Task<bool> Remove(ReimbursementModel entity);
        
        public Task<bool> Update(ReimbursementModel entity);
    }
}
