using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Garage2.Data;
using NET_FRAMEWORKS_EXAMEN_OPDRACHT.Data;
using Microsoft.AspNetCore.Identity;
using NET_FRAMEWORKS_EXAMEN_OPDRACHT.Areas.Identity.Data;
using System.Globalization;
using Microsoft.AspNetCore.Localization;




namespace NET_FRAMEWORKS_EXAMEN_OPDRACHT
{
    public class Startup
    {

        // para las cookies



        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {


        //    services.AddDbContext<Garage2Context>(options =>
        //        options.UseSqlServer(Configuration.GetConnectionString("Garage2Context") ?? throw new InvalidOperationException("Connection string 'Garage2Context' not found.")));

        //    services.AddDefaultIdentity<Garage2User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<Garage2Context>();

        //    services.AddControllersWithViews();

        //    // Configuración de la internacionalización
        //    services.AddLocalization(options => options.ResourcesPath = "Resources");

        //    services.Configure<RequestLocalizationOptions>(options =>
        //    {
        //        var supportedCultures = new[]
        //        {
        //        new CultureInfo("en-US"),
        //        new CultureInfo("es-ES"),
        //        // Agrega más culturas según sea necesario
        //    };

        //        options.DefaultRequestCulture = new RequestCulture("en-US");
        //        options.SupportedCultures = supportedCultures;
        //        options.SupportedUICultures = supportedCultures;
        //    });
        }

        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {

        //        app.UseDeveloperExceptionPage();
        //        app.UseMigrationsEndPoint();
        //    }
        //    else
        //    {

        //    }
        //    {
        //        app.UseExceptionHandler("/Home/Error");
        //        app.UseHsts();
        //    }

        //    app.UseStaticFiles();

        //    app.UseRouting();

        //    // Configuración de internacionalización
        //    var supportedCultures = new[]
        //    {
        //    new CultureInfo("en-US"),
        //    new CultureInfo("es-ES"),
        //    // Agrega más culturas según sea necesario
        //};
        //    app.UseRequestLocalization(new RequestLocalizationOptions
        //    {
        //        DefaultRequestCulture = new RequestCulture("en-US"),
        //        SupportedCultures = supportedCultures,
        //        SupportedUICultures = supportedCultures
        //    });

        //    app.UseAuthorization();

        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapControllerRoute(
        //            name: "default",
        //            pattern: "{controller=Home}/{action=Index}/{id?}");
        //        endpoints.MapRazorPages();
        //    });
        //}

        // para los cookies :



    }
}
