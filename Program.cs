using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MinaCoolProgekt.Data;
using MinaCoolProgekt.Areas.Identity.Data;
using MinaCoolProgekt.Hubs;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder
                           .Configuration
                           .GetConnectionString("ApplicationDbContextConnection") 
                       ?? 
                       throw 
                           new 
                               InvalidOperationException(
                                   "Connection string 'ApplicationDbContextConnection' not found."
                               );

builder.Services
    .AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services
    .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();;

// Add services to the container.
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapHub<ProjectChatHub>("/chatHub");

app.Run();