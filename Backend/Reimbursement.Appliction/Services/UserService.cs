using Microsoft.AspNetCore.Identity;
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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task<User> GetUserById(string Id)
        {
            return await _userRepository.GetUserById(Id);
        }

        public async Task<bool> IsUserRoleIn(User user, string role)
        {
            return await _userRepository.CheckUserRoleIsIn(user, role);
        }

        public async Task LogOut()
        {
             await _userRepository.SignOut();
        }

        public async Task<SignInResult> SignIn(SignIn signIn)
        {
            return await _userRepository.PasswordSignIn(signIn);
        }

        public async Task<IdentityResult> SignUp(SignUp signUp)
        {
            return await _userRepository.CreateUser(signUp);
        }
    }
}
