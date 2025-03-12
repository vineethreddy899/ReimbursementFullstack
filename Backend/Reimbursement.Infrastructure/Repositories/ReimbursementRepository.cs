using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reimbursement.Domain.Context;
using Reimbursement.Domain.Entities;
using Reimbursement.Domain.RepositoryInterface;
using Reimbursement.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reimbursement.Infrastructure.Repositories
{
    public class ReimbursementRepository : IReimbursementRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILog _logger;
        public ReimbursementRepository(ApplicationDbContext context,ILog logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<ReimbursementModel>> GetAllApproved(string mail, string type)
        {
            try
            {
                return await _context.Reimbursements.Where(e => e.RequestPhase == "Approved" && 
                (mail == null || e.Creator.Email == mail) && 
                (type == null || e.ReimbursementType == type)).OrderBy(e => e.CreatedDate)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex.Message);
                return new List<ReimbursementModel>();
            }
        }

        public async Task<List<ReimbursementModel>> GetAllDeclined(string mail, string type)
        {
            try
            {
                return await _context.Reimbursements.Where(e => e.RequestPhase == "Declined" 
                && (mail == null || e.Creator.Email == mail) 
                && (type == null || e.ReimbursementType == type)).OrderBy(e => e.CreatedDate)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex.Message);
                return new List<ReimbursementModel>();
            }
        }

        public async Task<List<ReimbursementModel>> GetAllPending(string mail, string type)
        {
            try
            { 
                return await _context.Reimbursements.Where(e => e.RequestPhase == "Pending" && 
                        (mail == null || e.Creator.Email == mail) && 
                        (type == null || e.ReimbursementType == type))
                        .OrderBy(e => e.CreatedDate)
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex.Message);
                return new List<ReimbursementModel>();
            }
        }

        public async Task<List<ReimbursementModel>> GetAllReimbursements()
        {
            try
            {
                return await _context.Reimbursements.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex.Message);
                return new List<ReimbursementModel>();
            }
        }

        public async Task<string> GetCreatorId(Guid Id)
        {
            try
            {
                if (Id == null) throw new ArgumentNullException();
                string userId = _context.Reimbursements.Where(e => e.Id == Id).Select(e => (e.CreatorId)).FirstOrDefault();
                return userId;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex.Message);
                return "";
            }
        }

        public async Task<ReimbursementModel> GetId(Guid Id)
        {
            try
            {
                if (Id == null) throw new ArgumentNullException();
                return await _context.Reimbursements.FindAsync(Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex.Message);
                return new ReimbursementModel();
            }
        }

        public async Task<bool> Insert(ReimbursementModel entity)
        {
            try
            {
                entity.Creator = await _context.Users.FindAsync(entity.CreatorId);
                await  _context.Reimbursements.AddAsync(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex.Message);
                return false;
            }
        }

        public async Task<List<ReimbursementModel>> MyReimbursements(string creatorId)
        {
            try
            {
                if (creatorId == null) throw new ArgumentNullException();
                return await _context.Reimbursements.Where(e => e.CreatorId == creatorId)
                    .OrderByDescending(e => e.CreatedDate)
                    .Select(elem => new ReimbursementModel()
                {
                    Id = elem.Id,
                    CreatedDate = elem.CreatedDate,
                    CreatorId = elem.CreatorId,
                    Currency = elem.Currency,
                    RequestedValue = elem.RequestedValue,
                    ReimbursementType = elem.ReimbursementType,
                    RequestPhase = elem.RequestPhase,
                    ApprovedValue = elem.ApprovedValue,
                    ApprovedAmount = elem.ApprovedAmount,
                    ApprovedBy = elem.ApprovedBy,
                    InternalNotes = elem.InternalNotes,
                    ImagePath = elem.ImagePath,
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex.Message);
                return new List<ReimbursementModel>();
            }
        }

        public async Task<bool> Remove(ReimbursementModel entity)
        {
            try
            {
                _context.Reimbursements.Remove(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex.Message);
                return false;
            }
        }

        public async Task<bool> Update(ReimbursementModel entity)
        {
            try
            {
                _context.Reimbursements.Update(entity);
                return await _context.SaveChangesAsync() > 0; ;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex.Message);
                return false;
            }
        }
    }
}
