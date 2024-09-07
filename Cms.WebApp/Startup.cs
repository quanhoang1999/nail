using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cms.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Cms.Data.Entities.Identity;
using AutoMapper;
using Cms.Infrastructure.Interfaces;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Logging;

namespace Cms.WebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(hostingEnvironment.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();
            if (hostingEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContextDefault>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("AppDbConnection"),
               b => b.MigrationsAssembly("Cms.Data.EF")));

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromHours(2);
                options.Cookie.HttpOnly = true;
            });

            services.AddAuthentication()
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                }).AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                });
            //services.AddMinResponse();

            services.ConfigureApplicationCookie(options => options.LoginPath = "/dang-nhap");

            // Configure Identity
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(3);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // User settings
                options.User.RequireUniqueEmail = true;
            });
            services.AddAutoMapper();

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContextDefault>()
                .AddDefaultTokenProviders();

            //Register for DI
            services.AddScoped<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();

            // Add application services.
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));
            //services.AddTransient<IEmailSender, EmailSender>();
            // services.AddTransient<IViewRenderService, ViewRenderService>();

            // services.AddTransient<IAuthorizationHandler, BaseResourceAuthorizationHandler>();

            // Add application services.
            services.AddTransient(typeof(IUnitOfWork), typeof(EFUnitOfWork));
            services.AddScoped(typeof(IRepository<,>), typeof(EFRepository<,>));

            //Register for service
            //ServiceRegister.Register(services);

            services.AddTransient<DbInitializer>();

            //services.AddImageResizer();
            //services.AddRecaptcha(new RecaptchaOptions
            //{
            //    SiteKey = Configuration["Recaptcha:SiteKey"],
            //    SecretKey = Configuration["Recaptcha:SecretKey"]
            //});

            // Add Custom Claims processor
            //services.AddScoped<IUserClaimsPrincipalFactory<AppUser>,
            //   CustomClaimsPrincipalFactory>();
            services.AddMvc().AddJsonOptions(options =>
                options.SerializerSettings.ContractResolver = new DefaultContractResolver());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
             IHostingEnvironment env, AppDbContextDefault dbContext, DbInitializer dbInitializer,
             ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/cms-{Date}.txt");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //app.UseMinResponse();
            //app.UseImageResizer();

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(name: "areaRoute",
                      template: "{area:exists}/{controller=Login}/{action=Index}/{id?}");
            });

            ////dbInitializer.Seed().Wait();
        }
    }
}
