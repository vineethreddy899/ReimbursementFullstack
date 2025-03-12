using Reimbursement.Application.ServiceInterface;
using Reimbursement.Domain.Entities;
using Reimbursement.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reimbursement.Application.Services
{
    public class ReimbursementService : IReimbursementService
    {
        private readonly IReimbursementRepository _reimbursementRepository;
        public ReimbursementService(IReimbursementRepository reimbursementRepository)
        {
            _reimbursementRepository = reimbursementRepository;
        }

        public async Task<List<ReimbursementModel>> GetAllApproved(string mail, string type)
        {
            return await _reimbursementRepository.GetAllApproved(mail, type);
        }

        public async Task<List<ReimbursementModel>> GetAllDeclined(string mail, string type)
        {
            return await _reimbursementRepository.GetAllDeclined(mail, type);
        }

        public async Task<List<ReimbursementModel>> GetAllPending(string mail, string type)
        {
            return await _reimbursementRepository.GetAllPending(mail, type);
        }

        public async Task<List<ReimbursementModel>> GetAllReimbursements()
        {
            return await _reimbursementRepository.GetAllReimbursements();
        }

        public async Task<string> GetCreatorId(Guid Id)
        {
            return await _reimbursementRepository.GetCreatorId(Id);
        }

        public async Task<ReimbursementModel> GetId(Guid Id)
        {
            return await _reimbursementRepository.GetId(Id);
        }

        public async Task<bool> Insert(ReimbursementModel entity)
        {

            return await _reimbursementRepository.Insert(entity);
        }

        public async Task<List<ReimbursementModel>> MyReimbursements(string creatorId)
        {
            return await _reimbursementRepository.MyReimbursements(creatorId);
        }

        public async Task<bool> Remove(ReimbursementModel entity)
        {
            return await _reimbursementRepository.Remove(entity);
        }

        public async Task<bool> Update(ReimbursementModel entity)
        {
            return await _reimbursementRepository.Update(entity);
        }
    }
}
