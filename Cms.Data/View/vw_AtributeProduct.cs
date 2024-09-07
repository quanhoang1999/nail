using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Data.View
{
    public class vw_AtributeProduct
    {
        public System.Guid ProductId { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
        public Guid PropertyTypeId { get; set; }
        public string PropertyTypeName { get; set; }
    }
}
