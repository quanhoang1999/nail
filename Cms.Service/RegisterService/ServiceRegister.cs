using Cms.Data.EF;
using Cms.Data.Entities.Identity;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Common;
using Cms.Service.Abstracts.Content;
using Cms.Service.Abstracts.Countries;
using Cms.Service.Abstracts.Crm;
using Cms.Service.Abstracts.Identiy;
using Cms.Service.Abstracts.Media;
using Cms.Service.Abstracts.Nails;
using Cms.Service.Abstracts.Products;
using Cms.Service.Implement;
using Cms.Service.Implement.Common;
using Cms.Service.Implement.Content;
using Cms.Service.Implement.Countries;
using Cms.Service.Implement.Crm;
using Cms.Service.Implement.Identity;
using Cms.Service.Implement.Media;
using Cms.Service.Implement.Nails;
using Cms.Service.Implement.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Cms.Service.RegisterService
{
    public static class ServiceRegister
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(EFUnitOfWork));
            services.AddTransient(typeof(IRepository<,>), typeof(EFRepository<,>));

            services.AddScoped<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();

            services.AddTransient(typeof(IWebServiceBase<,,>), typeof(WebServiceBase<,,>));
            services.AddTransient(typeof(IServiceBase<,>), typeof(ServiceBase<,>));
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IUIElementService, UIElementService>();
            services.AddScoped<IFunctionService, FunctionService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICustomersService, CustomersService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostCategoryService, PostCategoryService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddScoped<IUploadService, UploadService>();
            services.AddScoped<ISlideService, SlideService>();
            services.AddScoped<INailServiceService, NailServiceService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IGalleryService, GalleryService>();
            services.AddScoped<INailCustomerService, NailCustomerService>();
            services.AddScoped<INailEmployeeService, NailEmployeeService>();
            services.AddScoped<ICustomerCouponService, CustomerCouponService>();
            services.AddScoped<INailPromotionService, NailPromotionService>();
            services.AddScoped<INailCategoryService, NailCategoryService>();
            services.AddScoped<INailOrderService, NailOrderService>();
            services.AddScoped<INailStoreService, NailStoreService>();
            services.AddScoped<INailEgiftService, NailEgiftService>();
        }



    }

}
