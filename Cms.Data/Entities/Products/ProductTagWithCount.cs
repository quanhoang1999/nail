using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Data.Entities.Products
{
    public partial class ProductTagWithCount
    {
        public int ProductTagId { get; set; }

        public int ProductCount { get; set; }
    }
}
