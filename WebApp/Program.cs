using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using RazorClassLibrary;
using WebApp.Data;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.UrlRout("/About");

builder.Services
    .AddDbContext<UserContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddDbContext<WebApp.Data.AppContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("VendedorConnection")));

builder.Services
    .AddIdentity<Users, IdentityRole>(
        options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredUniqueChars = 8;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;

            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
        })
    .AddEntityFrameworkStores<UserContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");    
    app.UseHsts();
}
app.UseHttpsRedirection();

var arquivosPath = Path.Combine(builder.Environment.ContentRootPath, "arquivos");
if (!Directory.Exists(arquivosPath))
{
    Directory.CreateDirectory(arquivosPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(arquivosPath),
    RequestPath = "/static"
});

app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}").WithStaticAssets();
app.MapRazorPages();
app.Run();