using Reimbursement.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reimbursement.Infrastructure.UOW
{
    public interface IUnitOfWork
    {
        IReimbursementRepository Reimbursements { get; }
        Task<bool> CompleteAsync();
    }
}
