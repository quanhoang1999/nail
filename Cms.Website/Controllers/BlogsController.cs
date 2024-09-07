using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Cms.Service.Abstracts.Content;
using Cms.Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Website.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IPostService _blogService;

        public BlogsController(IPostService blogService)
        {
            _blogService = blogService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("{alias}/{id}")]
        public IActionResult Details(Guid id)
        {
            var model = new BlogDetailViewModel();
            model.Blog = _blogService.GetById(id);
            //model.MostBlogs = _blogService.GetLastest(6);
            return View(model);
        }
    }
}