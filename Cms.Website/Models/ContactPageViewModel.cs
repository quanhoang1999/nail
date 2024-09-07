using Cms.Service.ViewModel.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Website.Models
{
    public class ContactPageViewModel
    {
        public ContactViewModel Contact { set; get; }

        public FeedbackViewModel Feedback { set; get; }
    }
}
