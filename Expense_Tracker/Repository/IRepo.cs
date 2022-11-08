using Expense_Tracker.Models;
using Microsoft.AspNetCore.Identity;

namespace Expense_Tracker.Repository
{
    public interface IRepo
    {
        Task<IdentityResult> CreateAsync(SignUp usermodel);
        Task<SignInResult> PasswordSignInAsync(Login login);
        Task SignoutAsync();
    }
}
