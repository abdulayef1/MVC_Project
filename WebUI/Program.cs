using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//services
var constr = builder.Configuration["ConnectionStrings:Default"];
builder.Services.AddDbContext<AppDbContext>(opt=>
{
    opt.UseSqlServer(constr);
});

builder.Services.AddControllersWithViews();
var app = builder.Build();

//handle request

app.UseStaticFiles();

app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name:"default",
    pattern:"{controller=Home}/{action=Index}/{Id?}"
    );


app.Run();
