using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Service.Abstracts.Content;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Content;
using Cms.Utilities.Contants;
using Microsoft.AspNetCore.Mvc;
using static Cms.Utilities.Contants.CommonProperties;

namespace Cms.Website.Areas.Admin.Controllers
{
    public class ReviewController : BaseController
    {
        public IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Edit(Guid id)
        {
            List<int> voteList = new List<int>();
            voteList.Add(1);
            voteList.Add(2);
            voteList.Add(3);
            voteList.Add(4);
            voteList.Add(5);
            ViewBag.ListVote = voteList;
            List<SocialType> socialList = new List<SocialType>();
            socialList.Add(new SocialType()
            {
                Id = 0,
                Name = "--Select--"
            });
            socialList.Add(new SocialType()
            {
                Id = 1,
                Name = "Instagram"
            });
            socialList.Add(new SocialType()
            {
                Id = 2,
                Name = "Facebook"
            });
            socialList.Add(new SocialType()
            {
                Id = 3,
                Name = "Google"
            });
            socialList.Add(new SocialType()
            {
                Id = 4,
                Name = "Twitter"
            });
            socialList.Add(new SocialType()
            {
                Id = 5,
                Name = "Other"
            });
            ViewBag.ListVote = voteList;
            ViewBag.ListSocial = socialList;
            if (id != Guid.Empty)
            {
                var model = _reviewService.GetById(id);
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public IActionResult SaveEntity(ReviewViewModel viewModel)
        {

            if (viewModel.Id != Guid.Empty)
            {
                _reviewService.UpdateReview(viewModel);

            }
            else
            {
                _reviewService.Add(viewModel);
            }
            _reviewService.Save();
            return new OkObjectResult(viewModel);

        }

        [HttpGet]
        public IActionResult GetById(Guid id)
        {
            var model = _reviewService.GetById(id);
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
                _reviewService.Delete(id);

                return new OkObjectResult(id);
            }
        }
        [HttpPost]
        public IActionResult GetAllPaging(FilterCommonViewModel viewModel)
        {
            var model = _reviewService.Filter(viewModel);
            return new OkObjectResult(model);
        }
    }
}