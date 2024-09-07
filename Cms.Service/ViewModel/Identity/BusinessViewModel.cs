using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Identity
{
    public class BusinessViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public DateTime DateCreated
        {
            get; set;
        }

        public string Surrogate { get; set; }
        public DateTime? DateModified { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public double Funds { get; set; }

        public DateTime? DateDeleted { get; set; }

        public Guid? GroupBusinessId { get; set; }

        public Guid UserIdCreated { get; set; }
        
    }
}
