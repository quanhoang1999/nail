using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Products
{
    public class PrepareProductTagViewModel
    {
        public PrepareProductTagViewModel()
        {
            Tags = new List<ProductTagViewModel>();
        }
        public int TotalTags { get; set; }

        public IList<ProductTagViewModel> Tags { get; }
    }
}
