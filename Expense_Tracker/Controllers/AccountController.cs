using Expense_Tracker.Models;
using Expense_Tracker.Repository;
using Microsoft.AspNetCore.Mvc;
using static Expense_Tracker.Models.ContextClass;

namespace Expense_Tracker.Controllers
{
    public class AccountController : Controller
    {
        private readonly ExpenseDbContext db;
        private readonly IRepo repo;
        public AccountController(ExpenseDbContext db, IRepo repo)
        {
            this.db = db;
            this.repo = repo;
        }

        [Route("signup")]
        public IActionResult signup()
        {
            return View();
        }

        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> signup(SignUp signup)
        {
            if (ModelState.IsValid)
            {
                var result = await repo.CreateAsync(signup);
                if (result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                ModelState.Clear();
            }
            return RedirectToAction("login", "Account");
        }
        [Route("login")]
        public IActionResult login()
        {
            return View();
        }
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> login(Login login)
        {
            if (ModelState.IsValid)
            {
                var result = await repo.PasswordSignInAsync(login);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to login");
                }
                ModelState.Clear();
            }

            return View(login);
        }

        public async Task<IActionResult> Logout()
        {
            await repo.SignoutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
