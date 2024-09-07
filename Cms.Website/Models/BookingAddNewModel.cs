using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Website.Models
{
    public class BookingAddNewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string Note { get; set; }
        public List<ServiceCartModel> ServiceCartModels { get; set; }
    }
    public class ServiceCartModel
    {
        public Guid? ServiceID { get; set; }
        public Guid? EmployeeID { get; set; }
    }
}
