using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProfileManager.Data;
using ProfileManager.Areas.Identity.Data;
using Microsoft.AspNetCore.Builder;
using ProfileManager.Repository;
using ProfileManager.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ProfileManagerContextConnection") ?? throw new InvalidOperationException("Connection string 'ProfileManagerContextConnection' not found.");

builder.Services.AddDbContext<ProfileManagerContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<ProfileManagerDataDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ProfileManagerUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ProfileManagerContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

//di
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileServcie, ProfileServcie>();



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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.Run();
