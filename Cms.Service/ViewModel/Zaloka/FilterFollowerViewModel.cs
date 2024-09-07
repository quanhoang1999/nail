using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Zaloka
{
    public class FilterFollowerViewModel:FilterCommonViewModel
    {
        public bool IsFilter { get; set; } = true;
        public List<string> UserIds { get; set; }
    }
}
