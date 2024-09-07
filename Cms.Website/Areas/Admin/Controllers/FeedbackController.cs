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
    public class FeedbackController : BaseController
    {
        public IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FeedbackEdit(Guid id)
        {
            if (id != Guid.Empty)
            {
                var model = _feedbackService.GetById(id);
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public IActionResult SaveEntity(FeedbackViewModel viewModel)
        {

            if (viewModel.Id != Guid.Empty)
            {
                _feedbackService.UpdateV2(viewModel, viewModel.Id);

            }
            else
            {
                _feedbackService.Add(viewModel);
            }
            _feedbackService.Save();
            return new OkObjectResult(viewModel);

        }

        [HttpGet]
        public IActionResult GetById(Guid id)
        {
            var model = _feedbackService.GetById(id);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                _feedbackService.Delete(id);

                return new OkObjectResult(id);
            }
        }
        [HttpPost]
        public IActionResult GetAllPaging(FilterCommonViewModel viewModel)
        {
            var model = _feedbackService.Filter(viewModel);
            return new OkObjectResult(model);
        }
    }
}