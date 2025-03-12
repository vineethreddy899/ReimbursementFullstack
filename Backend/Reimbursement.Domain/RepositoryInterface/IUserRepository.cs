using Microsoft.AspNetCore.Identity;
using Reimbursement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reimbursement.Domain.RepositoryInterface
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);

        Task<User> GetUserById(string Id);

        Task<IdentityResult> CreateUser(SignUp signUp);

        Task<SignInResult> PasswordSignIn(SignIn signIn);

        Task SignOut();

        Task<List<User>> GetAllUsers();

        Task<bool> CheckUserRoleIsIn(User user, string role);
    }
}
