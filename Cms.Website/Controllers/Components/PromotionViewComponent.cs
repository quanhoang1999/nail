using Cms.Service.Abstracts.Nails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Website.Controllers.Components
{
    public class PromotionViewComponent: ViewComponent
    {
        private readonly IConfiguration _configuration;
        private readonly INailPromotionService _nailPromotionService;
        private readonly AppSettingConfig _appSettingConfig;
        public PromotionViewComponent(IOptions<AppSettingConfig> appSettingConfig, INailPromotionService nailPromotionService, IConfiguration configuration)
        {
            _nailPromotionService = nailPromotionService;
            _configuration = configuration;
            _appSettingConfig = appSettingConfig.Value;
       
        }
        public async Task<IViewComponentResult> InvokeAsync(string page = "index")
        {
            ViewBag.urlImage = _appSettingConfig.CndUploadUrl;
            int pageSize = _configuration.GetValue<int>("PageSize");
          
            if (page == "index")
            {
                var modelHomePage = _nailPromotionService.GetAll().FirstOrDefault(x=>x.IsShowHomePage&& x.IsActive && x.NailStoreId==1);
             
                return View(modelHomePage);
            }
            return View();
            //else
            //{
            //    var model = _productService.GetTreeHandle(1, 20, productType, true, null, EDateFilter.None, null, null, true);
            //    return View("PromotionBooking", model);
            //}
        }
    }
}
