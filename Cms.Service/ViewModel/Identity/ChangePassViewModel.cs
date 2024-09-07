using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Business.ViewModel
{
    public class ChangePassViewModel
    {
        public string UserId { get; set; }

        public string OldPass { get; set; }

        public string NewPass { get; set; }
    }
}
