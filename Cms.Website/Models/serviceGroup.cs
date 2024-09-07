using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Website.Models
{
    public class serviceGroup
    {
        public int serviceGroupId { get; set; }
        public int orderIndexGroup { get; set; }
        public string serviceGroupName { get; set; }
        public string serviceGroupIcon { get; set; }
        public string serviceGroupIcon1 { get; set; }
        public List<services> services { get; set; }
    }
}
