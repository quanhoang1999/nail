using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Zaloka
{
    public class AppUserViewModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Avatar { get; set; }

        public string PhoneNumber { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }
    }
}
