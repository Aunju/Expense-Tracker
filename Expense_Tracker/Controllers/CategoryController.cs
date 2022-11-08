using Expense_Tracker.Models;
using Expense_Tracker.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Expense_Tracker.Models.ContextClass;

namespace Expense_Tracker.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ExpenseDbContext db;
       
        public CategoryController(ExpenseDbContext db)
        {
            this.db = db;
            
        }
        public IActionResult Index()
        {
            return View(db.Expense_Categories.ToList());
        }
        [Authorize]
        public IActionResult Create()
        {

            return View();

        }
        public IActionResult Edit(int id)
        {
            var p = db.Expense_Categories.Where(x => x.Id == id).FirstOrDefault();
            return View(p);
        }
        [HttpPost]
        public IActionResult CreatorEdit(Expense_Category c)
        {
            if (c.Id > 0)
            {
                db.Expense_Categories.Update(c);
                db.SaveChanges();
            }
            else
            {
                db.Expense_Categories.Add(c);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult Delete(int id)
        {
            var p = db.Expense_Categories.Where(x => x.Id == id).FirstOrDefault();
            db.Remove(p);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}
