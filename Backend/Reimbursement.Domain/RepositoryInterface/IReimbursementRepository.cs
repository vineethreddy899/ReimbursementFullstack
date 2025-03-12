using Reimbursement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reimbursement.Domain.RepositoryInterface
{
    public interface IReimbursementRepository
    {
        Task<ReimbursementModel> GetId(Guid Id);

        Task<List<ReimbursementModel>> GetAllReimbursements();

        Task<List<ReimbursementModel>> GetAllApproved(string mail, string type);

        Task<List<ReimbursementModel>> GetAllPending(string mail, string type);
        
        Task<List<ReimbursementModel>> GetAllDeclined(string mail, string type);

        Task<string> GetCreatorId(Guid Id);

        Task<List<ReimbursementModel>> MyReimbursements(string creatorId);
        
        Task<bool> Insert(ReimbursementModel entity);
        
        Task<bool> Remove(ReimbursementModel entity);
        
        Task<bool> Update(ReimbursementModel entity);
    }
}
