using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shaper.DataAccess.Context;
using Shaper.DataAccess.IdentityContext;
using Shaper.Web;
using Shaper.Web.ApiService.IService;
using Shaper.Web.Areas.Admin.Services;
using Shaper.Web.Areas.Admin.Services.IService;
using Shaper.Web.Areas.User.Services;
using Shaper.Web.AuthenticationOptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Context implemented here so we can easily add migrations during development to DB since there is an issue
//with migrating with simultanious start-up programs. 
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ProductConnection")));

builder.Services.AddDbContext<IdentityAppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IdentityAppDbContext>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<IShaperApiService, ShaperApiService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClient();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/User/Account/RedirectLogin";
    options.LogoutPath = $"/User/Account/Logout";
    options.AccessDeniedPath = $"/User/Account/AccessDenied";
});


builder.AddShaperAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}");


app.Run();
