using Cms.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cms.Service.ViewModel.Content
{
    public class PostViewModel
    {
        public Guid Id { get; set; }
        
        public string Name { set; get; }

       
        public string Image { set; get; }

        public string FullImageUrl { set; get; }
        public string Description { set; get; }

        public string Content { set; get; }

        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; } = 0;

        public string Tags { get; set; }

        public DateTime DateCreated { set; get; }
      
        public Status Status { set; get; }

    
        public string SeoPageTitle { set; get; }     
        public string SeoAlias { set; get; }    
        public string SeoKeywords { set; get; }
        public string SeoDescription { set; get; }    

        public string Categories { get; set; }
        // public virtual ICollection<PostTagViewModel> PostTags { set; get; }
        // public virtual ICollection<PostCategoryViewModel> PostCategories { set; get; }
    }
}
