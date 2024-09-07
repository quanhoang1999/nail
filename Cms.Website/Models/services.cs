using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Website.Models
{
    public class services
    {
        public int serviceId { get; set; }
        public string serviceName { get; set; }
        public string serviceIcon { get; set; }
        public decimal serviceTime { get; set; }
        public decimal servicePrice { get; set; }
        public bool isApproveByManager { get; set; }
        public int technicianId { get; set; }
        public int quantity { get; set; }
        public string description { get; set; }
        public string notePrice { get; set; }

    }
}
