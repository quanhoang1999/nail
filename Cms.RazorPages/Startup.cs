using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cms.Data.EF;
using Cms.Data.Entities.Identity;
using Cms.Infrastructure.Interfaces;
using Cms.Service;
using Cms.Service.Content.Posts;
using Cms.Service.Systems.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cms.RazorPages
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("AppDbConnection"),
            b => b.MigrationsAssembly("Cms.Data.EF")));
            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                // If the LoginPath isn't set, ASP.NET Core defaults 
                // the path to /Account/Login.
                options.LoginPath = "/Admin/Login";
                // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                // the path to /Account/AccessDenied.
                options.AccessDeniedPath = "/Admin/AccessDenied";
                options.SlidingExpiration = true;
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));
            services.AddTransient(typeof(IUnitOfWork), typeof(EFUnitOfWork));
            services.AddTransient(typeof(IRepository<,>), typeof(EFRepository<,>));

            services.AddScoped<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();

            services.AddTransient(typeof(IWebServiceBase<,,>), typeof(WebServiceBase<,,>));
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IFunctionService, FunctionService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<DbInitializer>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
