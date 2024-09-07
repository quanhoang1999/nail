using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Service.Abstracts.Content;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Content;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Website.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        public IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        public IActionResult Index()
        {
            var model = _contactService.GetAll().FirstOrDefault();
            if (model != null)
                return View(model);
            return View();
        }

        //[HttpPost]

        //public IActionResult Filter(FilterCommonViewModel viewModel)
        //{
        //    var filterModel = _contactService.Filter(viewModel);
        //    return new OkObjectResult(filterModel);
        //}
        [HttpPost]
        public IActionResult SaveEntity(ContactViewModel contactView)
        {

            if (!string.IsNullOrEmpty(contactView.Id))
            {
                _contactService.UpdateV2(contactView, contactView.Id);

            }
            else
            {
                contactView.Id = "contact";
                _contactService.Add(contactView);
            }
            _contactService.Save();
            return new OkObjectResult(contactView);

        }

        [HttpGet]
        public IActionResult GetById(string id)
        {
            var model = _contactService.GetById(id);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                _contactService.Delete(id);

                return new OkObjectResult(id);
            }
        }

    }
}