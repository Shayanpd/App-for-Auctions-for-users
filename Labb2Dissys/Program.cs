using Labb2Dissys.Core;
using Labb2Dissys.Core.Interfaces;
using Labb2Dissys.Persistence;
using Microsoft.EntityFrameworkCore;
using Labb2Dissys.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register AuctionDbContext for auction-related entities
builder.Services.AddDbContext<AuctionDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("AuctionDbConnection")));

// Register Labb2DissysContext for ASP.NET Identity
builder.Services.AddDbContext<Labb2DissysContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("Labb2DissysContextConnection")));

// Add Identity services, tied to Labb2DissysContext
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<Labb2DissysContext>();

// Force users to authorize
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});


// Register application services
builder.Services.AddScoped<IAuctionService, AuctionService>();
builder.Services.AddScoped<IAuctionPersistence, MySqlAuctionPersistence>();

// AutoMapper registration
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Ensure Authentication Middleware is included
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Enable Razor Pages for Identity

app.Run();