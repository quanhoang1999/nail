using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Cms.Infrastructure.Enums
{
    public class EnumManage
    {
        public enum EnumCustomerType
        {
            [Description("Sỉ")]
            WholeSale,
            [Description("Lẻ")]
            Retail
        }
        public enum EnumUserType
        {
            Customer,
            Employee,
            Manager,
            Owner
        }
        public enum CacheKeys
        {
            ProductCategories
        }
        public enum PictureType
        {
            /// <summary>
            /// Entities (products, categories, manufacturers)
            /// </summary>
            Entity = 1,

            /// <summary>
            /// Avatar
            /// </summary>
            Avatar = 10
        }
    }
}
