using Expense_Tracker.Models;
using Expense_Tracker.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Expense_Tracker.Models.ContextClass;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ExpenseDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("con")));
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ExpenseDbContext>();
builder.Services.AddScoped<IRepo, AccountRepo>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
});
builder.Services.ConfigureApplicationCookie(config => {
    config.LoginPath = builder.Configuration["Application:LoginPath"];
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
