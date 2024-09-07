using Cms.Service.Abstracts.Nails;
using Cms.Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cms.Website.Controllers.Components
{
    public class ServiceViewComponent : ViewComponent
    {
        private readonly INailCategoryService _nailCategoryService;
        private readonly AppSettingConfig _appSettingConfig;
        public ServiceViewComponent(IOptions<AppSettingConfig> appSettingConfig, INailCategoryService nailCategoryService)
        {
            _nailCategoryService = nailCategoryService;
            _appSettingConfig = appSettingConfig.Value;
        }
        public async Task<IViewComponentResult> InvokeAsync(string page = "index")
        {
            var storeId = 1;
            if (page == "index")
            {
                //var storeId = HttpContext.Request.RouteValues["id"];
        
                var modelHomePage = _nailCategoryService.GetListShowOnHomePage(Convert.ToInt32(storeId));
                return View(modelHomePage);
            }
            else if (page == "DropdowServiceMenu")
            {
                var modelMenu = _nailCategoryService.GetListShowOnMenu(Convert.ToInt32(storeId));
                return View("DropdowServiceMenu", modelMenu);
            }
            else if (page == "DropdowserviceGroupMenu")
            {
                string strError = "";
                List<serviceGroup> objserviceGroup = Getserviceslst(ref strError);
                return View("DropdowserviceGroupMenu", objserviceGroup);
            }
            else if (page == "serviceGroup")
            {
                string strError = "";
                List<serviceGroup> objserviceGroup = Getserviceslst(ref strError);
                return View("serviceGroup", objserviceGroup);
            }
            else
            {
                var model = _nailCategoryService.GetAll().Where(x => !x.IsDeleted && x.IsActive);
                return View("ServiceBooking", model);
            }
        }

        public List<serviceGroup> Getserviceslst(ref string strError)
        {
            string strURLApi = _appSettingConfig.DomainVBSUrl + "v2/booking/services";
          // string strURLApi = "https://apipos.vbsportals.com/v2/booking/services";
            List<serviceGroup> objserviceGroup = new List<serviceGroup>();
            using (HttpClient client = new HttpClient())
            {
                try
                {

                    client.BaseAddress = new Uri(strURLApi);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("apikey", _appSettingConfig.KeyVBS);
                    HttpResponseMessage response = client.GetAsync(strURLApi).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        var objWebApiResult = JsonConvert.DeserializeObject<ApiResult>(result);
                        if (objWebApiResult.status == 200)
                        {

                            objserviceGroup = JsonConvert.DeserializeObject<List<serviceGroup>>(objWebApiResult.data.ToString());
                        }

                    }
                    return objserviceGroup;
                }
                catch (System.Exception objEx)
                {
                    strError = "Send SMS failed !";
                    return objserviceGroup;
                }
            }
        }

    }

    public class ApiResult
    {
        public int status { get; set; }
        public string errors { get; set; }
        public string Message { get; set; }
        public object data { get; set; }
    }
}
