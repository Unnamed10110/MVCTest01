using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCTest01.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// misma instancia en todos los casos
//builder.Services.AddSingleton<IAmigoAlmacen, MockAmigoRepositorio>();// origen de datos mock

// misma instancia en todos los casos
//builder.Services.AddSingleton<IAmigoAlmacen, SQLAmigoRepositorio>(); // origen de la base de datos

// una instancia diferente para cada solicitud
builder.Services.AddTransient<IAmigoAlmacen, SQLAmigoRepositorio>();

builder.Services.AddDbContextPool<AppDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("ConexionSQL")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
var app = builder.Build();
 
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");// ? indica que es opcional

app.Run();
