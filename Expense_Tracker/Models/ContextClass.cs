using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Models
{
    public class ContextClass
    {
        public class ExpenseDbContext : IdentityDbContext<ApplicationUser>
        {
            public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base(options)
            {

            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Expense_Category>()
                    .HasIndex(x => x.CategoryName).IsUnique();
                modelBuilder.Entity<SignUp>().HasNoKey();
                modelBuilder.Entity<Login>().HasNoKey();

            }
            public DbSet<Expense_Category> Expense_Categories { get; set; }
            public DbSet<Expenses> Expenses { get; set; }
            public DbSet<Expense_Tracker.Models.SignUp> Signup { get; set; }
            public DbSet<Expense_Tracker.Models.Login> Login { get; set; }
        }
    }
}
