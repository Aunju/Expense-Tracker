using Expense_Tracker.Models;
using Microsoft.AspNetCore.Identity;
using System.Data.Entity;
using static Expense_Tracker.Models.ContextClass;

namespace Expense_Tracker.Repository
{
    public class AccountRepo:IRepo
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountRepo(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;

        }

        public async Task<IdentityResult> CreateAsync(SignUp usermodel)
        {
            var user = new ApplicationUser()
            {
                FirstName = usermodel.FirstName,
                LastName = usermodel.LastName,
                Email = usermodel.Email,
                UserName = usermodel.Email
            };
            var result = await userManager.CreateAsync(user, usermodel.Password);
            return result;
        }

        public async Task<SignInResult> PasswordSignInAsync(Login login)
        {
            var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, false);
            return result;
        }

        public async Task SignoutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
