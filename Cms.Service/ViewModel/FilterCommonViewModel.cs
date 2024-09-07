using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel
{
    public class FilterCommonViewModel
    {
        public int PageSize { get; set; }
        public int SearchType { get; set; }
        public int PageIndex { get; set; }

        public int OAId { get; set; }
        public int CategoryId { get; set; }
        public string FullName { get; set; }

        public string KeyWord { get; set; }
        public int StoreId { get; set; }

        public Guid BusinessId { get; set; }

        public int Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
