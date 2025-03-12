using Microsoft.AspNetCore.Identity;
using Reimbursement.Domain.Context;
using Reimbursement.Domain.Entities;
using Reimbursement.Domain.RepositoryInterface;
using Reimbursement.Infrastructure.Repositories;
using Reimbursement.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reimbursement.Infrastructure.UOW
{
    public class UnitOfWork :IDisposable,IUnitOfWork
    {
        public IReimbursementRepository Reimbursements { get; private set; }
        public IUserRepository Users { get; private set; }

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILog _logger = Log.GetInstance;

        public UnitOfWork(ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            SignInManager<User> signInManager
            )
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;

            Users = new UserRepository(_userManager, _signInManager, _roleManager, _context, _logger);
            Reimbursements = new ReimbursementRepository(_context, _logger);
        }

        public async Task<bool> CompleteAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
