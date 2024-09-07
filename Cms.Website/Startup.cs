using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cms.Data.EF;
using Cms.Data.Entities.Identity;
using Cms.Service.Mapping;
using Cms.Service.RegisterService;
using Cms.Website.Authorization;
using Cms.Website.Extensions;
using Cms.Website.Helper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PaulMiami.AspNetCore.Mvc.Recaptcha;

namespace Cms.Website
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json",
                             optional: false,
                             reloadOnChange: true)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<AppSettingConfig>(Configuration.GetSection("AppSettingConfig"));
            services.AddDbContext<AppDbContextDefault>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AppDbConnection"),
                    b => b.MigrationsAssembly("Cms.Data.EF")));
            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContextDefault>()
                .AddDefaultTokenProviders();
            services.AddMemoryCache();

            //  services.AddMinResponse();
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
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(2);
                options.Cookie.HttpOnly = true;
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/admin/login";
                options.SlidingExpiration = true;
            });
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DomainProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
           
            AutoMapperConfiguration.Init(mappingConfig);
            services.AddSingleton(mapper);

            services.AddImageResizer();
            ServiceRegister.Register(services);
            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, CustomClaimsPrincipalFactory>();

            services.AddTransient<DbInitializer>();
            services.AddMvc(options =>
            {
                options.CacheProfiles.Add("Default",
                    new CacheProfile()
                    {
                        Duration = 60
                    });
                options.CacheProfiles.Add("Never",
                    new CacheProfile()
                    {
                        Location = ResponseCacheLocation.None,
                        NoStore = true
                    });
            }).AddViewLocalization(
                  LanguageViewLocationExpanderFormat.Suffix,
                  opts => { opts.ResourcesPath = "Resources"; })
              .AddDataAnnotationsLocalization()
             .AddNewtonsoftJson(options =>
             {
                 options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                 options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                 options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                 options.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
             });
            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
            services.Configure<RequestLocalizationOptions>(
             opts =>
             {
                 var supportedCultures = new List<CultureInfo>
                 {
                        new CultureInfo("en-US"),
                        new CultureInfo("vi-VN")
                 };

                 opts.DefaultRequestCulture = new RequestCulture("en-US");
                 // Formatting numbers, dates, etc.
                 opts.SupportedCultures = supportedCultures;
                 // UI strings that we have localized.
                 opts.SupportedUICultures = supportedCultures;
             });
            services.AddTransient<IAuthorizationHandler, BaseResourceAuthorizationHandler>();
            services.AddRecaptcha(new RecaptchaOptions()
            {
                SiteKey = Configuration["Recaptcha:SiteKey"],
                SecretKey = Configuration["Recaptcha:SecretKey"]
            });
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
             //   app.UseHsts();
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //Perform middleware for custom 404 page
            app.Use(async (context, next) => {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Home/Error";
                    await next();
                }
            });


            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
         //   app.UseHttpsRedirection();
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    "admin", "admin",
                     "Admin/{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
