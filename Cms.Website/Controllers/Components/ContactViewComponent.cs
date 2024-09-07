using Cms.Service.Abstracts.Content;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Website.Controllers.Components
{
    public class ContactViewComponent : ViewComponent
    {
        private readonly IContactService _contactService;
        public ContactViewComponent(IContactService contactService)
        {
            _contactService = contactService;

        }
        public async Task<IViewComponentResult> InvokeAsync(string type)
        {
            var model = _contactService.GetAll().FirstOrDefault();
            if (type == "footer")
                return View(model);
            else
                return View("Header", model);

        }
    }
}
