using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cms.Data.Entities.Identity;
using Cms.Service.Abstracts.Nails;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Nails;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cms.Website.Areas.Admin.Controllers
{
    public class CustomerCouponController : BaseController
    {
        private readonly ILogger _logger;

        private readonly IMapper _mapper;

        private readonly ICustomerCouponService _couponService;
        private readonly INailCustomerService _nailCustomerService;
        private readonly INailStoreService _nailStoreService;
        private readonly UserManager<AppUser> _userManager;

        public CustomerCouponController(ILoggerFactory loggerFactory, IMapper mapper, ICustomerCouponService couponService,
            INailStoreService nailStoreService,
            INailCustomerService nailCustomerService,
            UserManager<AppUser> userManager)
        {
            _couponService = couponService;
            _nailCustomerService = nailCustomerService;
            _nailStoreService = nailStoreService;
            _userManager = userManager;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<CustomerCouponController>();
        }
        [HttpPost]
        public IActionResult Filter(FilterCommonViewModel viewModel)
        {
            var filterModel = _couponService.Filter(viewModel);
            return new OkObjectResult(filterModel);
        }

        [HttpPost]
        public IActionResult SaveEntity(CustomerCouponViewModel viewModel)
        {

            if (viewModel.Id > 0)
            {
                _couponService.UpdateCoupon(viewModel);

            }
            else
            {
                _couponService.CreateCoupon(viewModel);
            }
            _couponService.Save();
            return new OkObjectResult(viewModel);

        }


        [HttpGet]
        public IActionResult GetById(int id)
        {
            string[] include = new string[] { "NailCustomer" };
            var model = _couponService.GetByIdInclude(id, include);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                _couponService.Delete(id);

                return new OkObjectResult(id);
            }
        }
        public IActionResult Index()
        {
            ViewBag.NailStore = _nailStoreService.GetAll().Where(x => !x.IsDeleted);
            return View();
        }
        public IActionResult Edit(int id)
        {
            ViewBag.NailStore = _nailStoreService.GetAll().Where(x => !x.IsDeleted);
            var customer = _nailCustomerService.GetAll().Where(x => !x.IsDeteted);
            ViewBag.AllCustomer = customer;
            if (id > 0)
            {
                string[] include = new string[] { "NailCustomer" };
                var model = _couponService.GetByIdInclude(id, include);
                return View(model);
            }
            return View();
        }
    }
}