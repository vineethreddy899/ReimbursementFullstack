using Reimbursement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reimbursement.Application.ServiceInterface
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
