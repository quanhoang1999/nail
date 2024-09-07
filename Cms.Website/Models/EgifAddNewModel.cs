using Cms.Data.View;
using Cms.Service.ViewModel.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Cms.Website.Models
{
    public class EgifAddViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string Note { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string RecipientName { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        public string RecipientPhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string RecipientEmail { get; set; }
        public string RecipientNote { get; set; }

    
        public List<ProductViewModel> GiftCartModels { get; set; }
    }
    public class EgifAddNewModel
    {
        //public List<vw_GetAllProduct> services { get; set; }
       
        public CusGiftInfo cusInfo { get; set; }
    }
    public class CusGiftInfo
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public string RecipientName { get; set; }
        public string RecipientPhoneNumber { get; set; }
        public string RecipientEmail { get; set; }
        public string RecipientNote { get; set; }
        public DateTime DatePick { get; set; }

    }

}
