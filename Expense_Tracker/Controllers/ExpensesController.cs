using Expense_Tracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using static Expense_Tracker.Models.ContextClass;

namespace Expense_Tracker.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ExpenseDbContext db;

        public ExpensesController (ExpenseDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            MultipleTable m = new MultipleTable();
            m.Expense_Categories = db.Expense_Categories.ToList();
            m.Expenses = db.Expenses.ToList();
            return View(m);
            //ViewBag.sort = usertext;

            //ViewBag.sortparam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewBag.dateSort = sortOrder == "Date" ? "date_desc" : "Date";

            //IQueryable<Expenses> expenses = db.Expenses;
            //if (!string.IsNullOrEmpty(usertext))
            //{
            //    usertext = usertext.ToLower();
            //    expenses = expenses.Where(s => s.Expense_Category.ToString().Contains(usertext)
            //                  );
            //}

            //switch (sortOrder)
            //{
            //    case "name_desc":
            //        expenses = expenses.OrderByDescending(s => s.Expense_Category);
            //        break;
            //    case "Date":
            //        expenses = expenses.OrderBy(s => s.Date);
            //        break;
            //    case "date_desc":
            //        expenses = expenses.OrderByDescending(s => s.Date);
            //        break;
            //    default:
            //        expenses = expenses.OrderBy(s => s.Expense_Category);
            //        break;
            //}
            //ViewBag.count = expenses.Count();
            //if (page <= 0) page = 1;
            //int pageSize = 10;
            //ViewBag.pSize = pageSize;

            //return View(await PaginatedList<Expenses>.CreateAsync(expenses,page,pageSize));
        }
       [Authorize]
        public IActionResult Create()
        {

            ViewBag.categories = db.Expense_Categories.ToList();
            return View();
        }
        public IActionResult Edit(int id)
        {

            var p = db.Expenses.Where(x => x.ExpenseId == id).FirstOrDefault();
            ViewBag.categories = db.Expense_Categories.ToList();
            return View(p);
        }
        [HttpPost]
        public IActionResult CreatorEdit(Expenses c)
        {
            if (c.ExpenseId > 0)
            {
                ViewBag.categories = db.Expense_Categories.ToList();
                db.Expenses.Update(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.categories = db.Expense_Categories.ToList();
                db.Expenses.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            ViewBag.categories = db.Expense_Categories.ToList();
            var p = db.Expenses.Where(x => x.ExpenseId == id).FirstOrDefault();
            db.Remove(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public List<Expenses> Expenses { get; set; }
        public void search(DateTime startdate, DateTime enddate)
        {
            Expenses = (from x in db.Expenses where (x.Date <= startdate) && (x.Date >= enddate) select x).ToList();
        }

    }
}
