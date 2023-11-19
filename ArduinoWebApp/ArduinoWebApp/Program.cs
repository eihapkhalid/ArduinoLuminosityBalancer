using ArduinoWebApp.Acsses.Data;
using ArduinoWebApp.Acsses.UnitOfWork;
using ArduinoWebApp.library;
using Microsoft.EntityFrameworkCore;

namespace ArduinoWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            #region DefaultConnection
            builder.Services.AddDbContext<ArduinoAppDbContext>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            #endregion

            #region UnitOfWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            //  ”ÃÌ· SerialPortConnector ﬂŒœ„…
            builder.Services.AddSingleton<SerialPortConnector>();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=LdrSensor}/{action=Index}/{id?}");

            app.Run();
        }
    }
}