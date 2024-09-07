using Cms.Data.Entities.Content;
using Cms.Data.Entities.Identity;
using Cms.Data.Entities.System;
using Cms.Infrastructure.Enums;
using Cms.Utilities.Contants;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Data.EF
{
    public class DbInitializer
    {
        private readonly AppDbContextDefault _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public DbInitializer(AppDbContextDefault context,
          UserManager<AppUser> userManager,
          RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Description = "Site manager"
                });
            }
            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new AppUser()
                {
                    Id = Guid.NewGuid(),
                    UserName = "duckaka",
                    FullName = "Administrator",
                    Email = "admin@gmail.com",
                    Balance = 0,
                }, "duckaka@89");
                var user = await _userManager.FindByNameAsync("admin");
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            //if (!_context.Post.Any())
            //{
            //    List<Post> slides = new List<Post>()
            //    {
            //        new Post() { Id = Guid.NewGuid(),Name="Tin tuc 1",Image="/client-side/images/slider/slide-1.jpg",Description="Noi dung mo ta",Content="Nooi dung content",SeoAlias="tin-tuc-1", Status=Infrastructure.Enums.Status.Actived, DateCreated = DateTime.Now },
            //        new Post() { Id = Guid.NewGuid(),Name="Tin tuc 2",Image="/client-side/images/slider/slide-1.jpg",Description="Noi dung mo ta",Content="Nooi dung content",SeoAlias="tin-tuc-2", Status=Infrastructure.Enums.Status.Actived, DateCreated = DateTime.Now },
            //    };
            //    _context.Post.AddRange(slides);
            //}

            //if (!_context.SystemConfig.Any(x => x.UniqueCode == "HomeTitle"))
            //{
            //    _context.SystemConfig.Add(new Setting()
            //    {
            //        Id = Guid.NewGuid(),
            //        UniqueCode = "HomeTitle",
            //        Name = "Tiêu đề trang chủ",
            //        TextValue = "Trang chủ CMS",
            //        Status = Status.Actived
            //    });
            //}
            //if (!_context.SystemConfig.Any(x => x.UniqueCode == "HomeMetaKeyword"))
            //{
            //    _context.SystemConfig.Add(new Setting()
            //    {
            //        Id = Guid.NewGuid(),
            //        UniqueCode = "HomeMetaKeyword",
            //        Name = "Từ khoá trang chủ",
            //        TextValue = "Trang chủ CMS",
            //        Status = Status.Actived
            //    });
            //}
            //if (!_context.SystemConfig.Any(x => x.UniqueCode == "HomeMetaDescription"))
            //{
            //    _context.SystemConfig.Add(new Setting()
            //    {
            //        Id = Guid.NewGuid(),
            //        UniqueCode = "HomeMetaDescription",
            //        Name = "Mô tả trang chủ",
            //        TextValue = "Trang chủ CMS",
            //        Status = Status.Actived
            //    });
            //}

            if (_context.Functions.Count() == 0)
            {
                _context.Functions.AddRange(new List<Function>()
                {
                    new Function() {Id = "SYSTEM", Name = "System",ParentId = null,SortOrder = 1,Status = Status.Active,URL = "/",IconCss = "fa-desktop"  },
                    new Function() {Id = "ROLE", Name = "Role",ParentId = "SYSTEM",SortOrder = 1,Status = Status.Active,URL = "/admin/role/index",IconCss = "fa-home"  },
                    new Function() {Id = "FUNCTION", Name = "Function",ParentId = "SYSTEM",SortOrder = 2,Status = Status.Active,URL = "/admin/function/index",IconCss = "fa-home"  },
                    new Function() {Id = "USER", Name = "User",ParentId = "SYSTEM",SortOrder =3,Status = Status.Active,URL = "/admin/user/index",IconCss = "fa-home"  },
                    new Function() {Id = "ACTIVITY", Name = "Activity",ParentId = "SYSTEM",SortOrder = 4,Status = Status.Active,URL = "/admin/activity/index",IconCss = "fa-home"  },
                    new Function() {Id = "ERROR", Name = "Error",ParentId = "SYSTEM",SortOrder = 5,Status = Status.Active,URL = "/admin/error/index",IconCss = "fa-home"  },
                    new Function() {Id = "SETTING", Name = "Configuration",ParentId = "SYSTEM",SortOrder = 6,Status = Status.Active,URL = "/admin/setting/index",IconCss = "fa-home"  },

                    new Function() {Id = "PRODUCT",Name = "Product Management",ParentId = null,SortOrder = 2,Status = Status.Active,URL = "/",IconCss = "fa-chevron-down"  },
                    new Function() {Id = "PRODUCT_CATEGORY",Name = "Category",ParentId = "PRODUCT",SortOrder =1,Status = Status.Active,URL = "/admin/productcategory/index",IconCss = "fa-chevron-down"  },
                    new Function() {Id = "PRODUCT_LIST",Name = "Product",ParentId = "PRODUCT",SortOrder = 2,Status = Status.Active,URL = "/admin/product/index",IconCss = "fa-chevron-down"  },
                    new Function() {Id = "BILL",Name = "Bill",ParentId = "PRODUCT",SortOrder = 3,Status = Status.Active,URL = "/admin/bill/index",IconCss = "fa-chevron-down"  },

                    new Function() {Id = "CONTENT",Name = "Content",ParentId = null,SortOrder = 3,Status = Status.Active,URL = "/",IconCss = "fa-table"  },
                    new Function() {Id = "BLOG",Name = "Blog",ParentId = "CONTENT",SortOrder = 1,Status = Status.Active,URL = "/admin/blog/index",IconCss = "fa-table"  },
                    new Function() {Id = "PAGE",Name = "Page",ParentId = "CONTENT",SortOrder = 2,Status = Status.Active,URL = "/admin/page/index",IconCss = "fa-table"  },

                    new Function() {Id = "UTILITY",Name = "Utilities",ParentId = null,SortOrder = 4,Status = Status.Active,URL = "/",IconCss = "fa-clone"  },
                    new Function() {Id = "FOOTER",Name = "Footer",ParentId = "UTILITY",SortOrder = 1,Status = Status.Active,URL = "/admin/footer/index",IconCss = "fa-clone"  },
                    new Function() {Id = "FEEDBACK",Name = "Feedback",ParentId = "UTILITY",SortOrder = 2,Status = Status.Active,URL = "/admin/feedback/index",IconCss = "fa-clone"  },
                    new Function() {Id = "ANNOUNCEMENT",Name = "Announcement",ParentId = "UTILITY",SortOrder = 3,Status = Status.Active,URL = "/admin/announcement/index",IconCss = "fa-clone"  },
                    new Function() {Id = "CONTACT",Name = "Contact",ParentId = "UTILITY",SortOrder = 4,Status = Status.Active,URL = "/admin/contact/index",IconCss = "fa-clone"  },
                    new Function() {Id = "SLIDE",Name = "Slide",ParentId = "UTILITY",SortOrder = 5,Status = Status.Active,URL = "/admin/slide/index",IconCss = "fa-clone"  },
                    new Function() {Id = "ADVERTISMENT",Name = "Advertisment",ParentId = "UTILITY",SortOrder = 6,Status = Status.Active,URL = "/admin/advertistment/index",IconCss = "fa-clone"  },

                    new Function() {Id = "REPORT",Name = "Report",ParentId = null,SortOrder = 5,Status = Status.Active,URL = "/",IconCss = "fa-bar-chart-o"  },
                    new Function() {Id = "REVENUES",Name = "Revenue report",ParentId = "REPORT",SortOrder = 1,Status = Status.Active,URL = "/admin/report/revenues",IconCss = "fa-bar-chart-o"  },
                    new Function() {Id = "ACCESS",Name = "Visitor Report",ParentId = "REPORT",SortOrder = 2,Status = Status.Active,URL = "/admin/report/visitor",IconCss = "fa-bar-chart-o"  },
                    new Function() {Id = "READER",Name = "Reader Report",ParentId = "REPORT",SortOrder = 3,Status = Status.Active,URL = "/admin/report/reader",IconCss = "fa-bar-chart-o"  },
                });
            }


            //if (_context.Footer.Count(x => x.Id == CommonConstants.DefaultFooterId) == 0)
            //{
            //    string content = "Footer";
            //    _context.Footer.Add(new Footer()
            //    {
            //        Id = CommonConstants.DefaultFooterId,
            //        Content = content
            //    });
            //}
            //if (_context.Contact.Count(x => x.Id == CommonConstants.DefaultFooterId) == 0)
            //{
            //    _context.Contact.Add(new Contact()
            //    {
            //        Id = CommonConstants.DefaultFooterId,
            //        Name = "Panda Shop",
            //        Status = Status.Actived,
            //        Address = "Số 123 Đường xxx TPHCM",
            //        Email = "vietducit@gmail.com",
            //        Phone = "0903051412",
            //        Website = "http://cms.com",
            //        Lng = 21.0435483,
            //        Lat = 105.790058,
            //    });
            //}

            await _context.SaveChangesAsync();
        }
    }
}