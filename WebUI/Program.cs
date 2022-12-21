using DataAccess.Contexts;

var builder = WebApplication.CreateBuilder(args);

//services
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddControllersWithViews();
var app = builder.Build();




//handle requst

app.UseStaticFiles();

app.MapControllerRoute(
    name:"default",
    pattern:"{controller=Home}/{action=Index}/{Id?}"
    );
app.Run();
