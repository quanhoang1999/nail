using Cms.Service.ViewModel.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Website.Models
{
    public class BlogDetailViewModel
    {
        public PostViewModel Blog { get; set; }

        public List<PostViewModel> MostBlogs { get; set; }
    }
}
