using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Data.Entities.Identity;
using Cms.Service.Abstracts.Nails;
using Cms.Service.ViewModel.Nails;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Website.Controllers
{
    public class BookingController : Controller
    {
        private readonly INailOrderService _nailOrderService;

        private readonly UserManager<AppUser> _userManager;
        public BookingController(INailOrderService nailOrderService,
            UserManager<AppUser> userManager)
        {
            _nailOrderService = nailOrderService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add([FromBody] NailOrderViewModel viewModel)
        {
            viewModel.IsActive = true;
            _nailOrderService.Add(viewModel);
            _nailOrderService.Save();
            return Ok(true);
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
                _nailOrderService.Delete(id);
                _nailOrderService.Save();
                return new OkObjectResult(id);
            }
        }
    }
}