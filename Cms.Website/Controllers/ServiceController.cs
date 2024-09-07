using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cms.Service.Abstracts.Nails;
using Cms.Website.Controllers.Components;
using Cms.Website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Cms.Website.Controllers
{
    public class ServiceController : Controller
    {
        private readonly INailCategoryService _nailCategoryService;
        private readonly INailServiceService _nailServiceService;
        private readonly INailEmployeeService _nailEmployeeService;
        private readonly AppSettingConfig _appSettingConfig;
        public ServiceController(IOptions<AppSettingConfig> appSettingConfig,INailCategoryService nailCategoryService,
            INailEmployeeService nailEmployeeService,
           INailServiceService nailServiceService)
        {
            _nailCategoryService = nailCategoryService;
            _nailEmployeeService = nailEmployeeService;
            _nailServiceService = nailServiceService;
            _appSettingConfig = appSettingConfig.Value;
        }
        public IActionResult request(int id)
        {

            HttpContext.Session.SetString("sIDReuqest", id.ToString());
            return RedirectToAction("IndexNew", id);
        }
        public IActionResult Index()
        {
            //ViewBag.IDReuqest = HttpContext.Session.GetInt32("sIDReuqest");
            ViewBag.IDReuqest =1;
            var model = _nailCategoryService.GetAllInclude(1);
            return View(model);
        }
        public IActionResult IndexNew(int id)
        {
            ViewBag.IDReuqest = 4;
            HttpContext.Session.Clear();
            string strError = "";
            List<serviceGroup> objserviceGroup = Getserviceslst(ref strError);
            return View(objserviceGroup);
        }
        public IActionResult GetByID(int id)
        {
            if (id > 0)
            {
                string[] includes = new string[] { "NailEmployeeServices" };
                var model = _nailServiceService.GetByIdInclude(id, includes);
                return Ok(model);
            }
            return BadRequest();
        }
        public IActionResult GetByCateID(int id)
        {
            if (id > 0)
            {
                var model = _nailServiceService.GetbyCategoryId(id);
                return Ok(model);
            }
            return BadRequest();
        }


        [HttpGet]
        public IActionResult GetByServiceGroupID(int id = 0)
        {
            try
            {
                string strError = "";
                List<serviceGroup> objserviceGroup = Getserviceslst(ref strError);
                return Ok(objserviceGroup);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    
        [HttpGet]
        public IActionResult GetEmployeelst()
        {
            try
            {
                string strError = "";
                List<technicians> objtechnicians = Gettechnicianslst(ref strError);
                return Ok(objtechnicians);
            }
            catch (Exception)
            {
                return BadRequest();

            }

        }
        [HttpGet]
        public IActionResult GetLisEmployee(int id)
        {
            try
            {

                var emp = _nailEmployeeService.GetByServiceId(id);
                return Ok(emp);
            }
            catch (Exception)
            {
                return BadRequest();

            }

        }

        public List<serviceGroup> Getserviceslst(ref string strError)
        {

           // string strURLApi = "https://apipos.vbsportals.com/v2/booking/services";
            string strURLApi = _appSettingConfig.DomainVBSUrl + "v2/booking/services";
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

        public List<technicians> Gettechnicianslst(ref string strError)
        {

            string strURLApi = _appSettingConfig.DomainVBSUrl+ "v2/booking/technicians";
            List<technicians> objtechnicians = new List<technicians>();
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

                            objtechnicians = JsonConvert.DeserializeObject<List<technicians>>(objWebApiResult.data.ToString());
                        }

                    }
                    return objtechnicians;
                }
                catch (System.Exception objEx)
                {
                    strError = "Send SMS failed !";
                    return objtechnicians;
                }
            }
        }

        public IActionResult Addbookingweb([FromBody] bookingweb model)
        {

          //  string strURLApi = "https://apipos.vbsportals.com/v2/booking";
            string strURLApi = _appSettingConfig.DomainVBSUrl + "v2/booking";
            List<technicians> objtechnicians = new List<technicians>();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    System.Net.Http.HttpContent data = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(strURLApi);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("apikey", _appSettingConfig.KeyVBS);
                    HttpResponseMessage response = client.PostAsync(strURLApi, data).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        var objWebApiResult = JsonConvert.DeserializeObject<ApiResult>(result);
                        if (objWebApiResult.status == 200)
                        {
                            return Ok(false);
                        }
                        return BadRequest();
                    }
                    else
                    {
                        return BadRequest();
                    }

                }
                catch (System.Exception objEx)
                {
                    return BadRequest();
                }
            }
        }
    }
    public class bookingweb
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phonenumber { get; set; }
        public DateTime bookingDate { get; set; }
        //public string Email { get; set; }
        public string note { get; set; }
        public List<services> services { get; set; }

    }
}