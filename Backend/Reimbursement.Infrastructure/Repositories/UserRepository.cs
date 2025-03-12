using Microsoft.AspNetCore.Identity;
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
    public class UserRepository : IUserRepository
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly ILog _logger;

        public UserRepository(UserManager<User> userManager,
               SignInManager<User> signInManager,
               RoleManager<IdentityRole> roleManager,
               ApplicationDbContext context,
               ILog logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _logger = logger;
        }


        public async Task<bool> CheckUserRoleIsIn(User user, string role)
        {
            try
            {
                return await _userManager.IsInRoleAsync(user, role);
            }
            catch (Exception exception)
            {
                _logger.LogException(exception.Message);
                return false;
            }
        }

        public async Task<IdentityResult> CreateUser(SignUp signUp)
        {
            try
            {
                var user = new User()
                {

                    Name = signUp.Name,
                    Bank = signUp.Bank,
                    PanCardNumber = signUp.PanCardNumber,
                    BankAccountNumber = signUp.BankAccountNumber,
                    Email = signUp.Email,
                    UserName = signUp.Email,
                    isApprover = signUp.isApprover,

                };
                var result = await _userManager.CreateAsync(user, signUp.Password);
                return result;
            }
            catch (Exception exception)
            {
                _logger.LogException(exception.Message);
                return new IdentityResult();
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                return _context.Users.Select(elem => elem).ToList();
            }
            catch (Exception exception)
            {
                _logger.LogException(exception.Message);
                return new List<User>();
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                return await _userManager.FindByEmailAsync(email);
            }
            catch (Exception exception)
            {
                _logger.LogException(exception.Message);
                return new User();
            }
        }

        public async Task<User> GetUserById(string Id)
        {
            try
            {
                return await _userManager.FindByIdAsync(Id);
            }
            catch (Exception exception)
            {
                _logger.LogException(exception.Message);
                return new User();
            }
        }

        public async Task<SignInResult> PasswordSignIn(SignIn signIn)
        {
            try
            {
                return await _signInManager.PasswordSignInAsync(signIn.Email, signIn.Password, false, true);
            }
            catch (Exception exception)
            {
                _logger.LogException(exception.Message);
                return new SignInResult();
            }
        }

        public async Task SignOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception exception)
            {
                _logger.LogException(exception.Message);
            }
        }
    }
}
