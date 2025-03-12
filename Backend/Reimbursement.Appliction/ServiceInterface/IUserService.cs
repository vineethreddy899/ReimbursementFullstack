using Microsoft.AspNetCore.Identity;
using Reimbursement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reimbursement.Application.ServiceInterface
{
    public interface IUserService
    {
        public Task<IdentityResult> SignUp(SignUp signUp);

        public Task<SignInResult> SignIn(SignIn signIn);

        public Task LogOut();

        public Task<User> GetUserByEmail(string email);

        public Task<User> GetUserById(string Id);

        Task<bool> IsUserRoleIn(User user, string role);
    }
}
