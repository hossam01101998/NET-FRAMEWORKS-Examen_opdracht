using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Garage2.Data;
using NET_FRAMEWORKS_EXAMEN_OPDRACHT.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Garage2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Garage2Context") ?? throw new InvalidOperationException("Connection string 'Garage2Context' not found.")));



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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();