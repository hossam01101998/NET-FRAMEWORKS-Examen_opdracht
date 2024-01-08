using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Garage2.Data;
using NET_FRAMEWORKS_EXAMEN_OPDRACHT.Data;
using Microsoft.AspNetCore.Identity;
using NET_FRAMEWORKS_EXAMEN_OPDRACHT.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Garage2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Garage2Context") ?? throw new InvalidOperationException("Connection string 'Garage2Context' not found.")));

builder.Services.AddDefaultIdentity<Garage2User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<Garage2Context>();



// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();





//probamos llamar al initializer
using (var scope = app.Services.CreateScope())
{
 
    var context = scope.ServiceProvider.GetRequiredService<Garage2Context>();

    
    Initializer.DbSetInitializer(context);
}
// fin llamada initializer





// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using(var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager =
        scope.ServiceProvider.GetRequiredService<UserManager<Garage2User>>();

    string email = "admin@admin.com";
    string password = "Admin123!";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new Garage2User();
        user.UserName = email;
        user.Email = email;
        user.EmailConfirmed = true;
        user.ConfirmEmail = email;
        user.Adress = "Adminstreet 1";
        user.DateOfBirth = DateTime.Now.AddYears(-100);
        user.Name = "Admin";
        user.Surname = "Admin";
        user.IsAdmin = true;


        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "Admin");
    }

    string userEmail = "user@user.com";
    string userPassword = "User123!";

    if (await userManager.FindByEmailAsync(userEmail) == null)
    {
        var normalUser = new Garage2User
        {
            UserName = userEmail,
            Email = userEmail,
            EmailConfirmed = true,
            ConfirmEmail = userEmail,
            Adress = "Userstreet 1",
            DateOfBirth = DateTime.Now.AddYears(-25),
            Name = "User",
            Surname = "User",
            IsAdmin = false
        };

        var userResult = await userManager.CreateAsync(normalUser, userPassword);

        if (userResult.Succeeded)
        {
            await userManager.AddToRoleAsync(normalUser, "User");
        }

    }
}

app.Run();