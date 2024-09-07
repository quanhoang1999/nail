using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Identity
{
   public class FilterUserViewModel
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public string FullName { get; set; }

        public Guid BusinessId { get; set; }

        public int Status { get; set; }
        public int OaId { get; set; }
        public int SearchType { get; set; }
        public string Keyword { get; set; }
        public int CategoryId { get; set; }
       
    }
}
