using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Utilities.Contants
{
    public class CommonConstants
    {
        public static string DefaultContactId = "contact";
        public static string DefaultFooterId = "default";
        public const string ProductTag = "product";
        public const string BlogTag = "blog";

        public const string CartSession = "CartSession";
        public class AppRole
        {
            public const string AdminRole = "Admin";
        }

        public class UserClaims
        {
            public const string Roles = "Roles";
        }
    }
}
