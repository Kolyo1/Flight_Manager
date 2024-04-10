using Data;
using Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace FlightManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<FmDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();


            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<FmDbContext>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
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
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
            
        }

        public Program(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FmDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<dbUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;

                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;

            })
                .AddEntityFrameworkStores<FmDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        /*
        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Program>();
        })
        .ConfigureServices((hostContext, services) =>
        {
            // Retrieve the service provider to access the DbContext
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<FmDbContext>();

                // Check if the Admin user exists
                var adminUser = dbContext.Users.FirstOrDefault(u => u.Role == "Admin");

                // If Admin user doesn't exist, create it
                if (adminUser == null)
                {
                    var admin = new dbUser
                    {
                        UserName = "Admin",
                        Password = "adminpassword",
                        Email = "admin@admin.admin",
                        EmailConfirmed = true,
                        FirstName = "Admin",
                        LastName = "Admin",
                        EGN = "0000000000",
                        Address = "Admin/NoAddress",
                        PhoneNumber = "0000000000",
                        Role = "Admin",
                    };

                    // Add the admin user to the DbContext and save changes
                    dbContext.Users.Add(admin);
                    dbContext.SaveChanges();
                }
            }
        });
        */

        private async System.Threading.Tasks.Task Seed(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<dbUser>>();
            string[] roleNames = { "Admin", "Employee" };
            IdentityResult result;

            foreach (var roleName in roleNames)
            {
                var roleCheck = await RoleManager.RoleExistsAsync(roleName);
                if (!roleCheck)
                {
                    result = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            var admin = new dbUser
            {
                UserName = "Administrator",
                Password = "admin123",
                NormalizedUserName = "Admin",
                Email = "admin@admin.admin",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "Admin",
                EGN = "0000000000",
                Address = "Admin/NoAddress",
                PhoneNumber = "0000000000",
                Role = "Admin",
                LockoutEnabled = false
            };

            string password = "password";
            var _user = await UserManager.FindByNameAsync(admin.UserName);
            if (_user == null)
            {
                IdentityResult checkUser = await UserManager.CreateAsync(admin, password);

                if (checkUser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}