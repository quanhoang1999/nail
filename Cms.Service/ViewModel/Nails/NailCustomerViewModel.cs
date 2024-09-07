using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cms.Service.ViewModel.Nails
{
    public class NailCustomerViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        [Required]
        [StringLength(250)]
        public string Email { set; get; }
        [Required]
        [StringLength(20)]
        public string Phone { set; get; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? Birthdate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeteted { get; set; } = false;
        public Guid UserCreated { get; set; }
        public Guid? UserModified { get; set; }
        public Guid? UserDeleted { get; set; }
        public virtual NailStoreViewModel NailStore { get; set; }
        public int NailStoreId { get; set; }
        public string NailStoreName { get; set; }
    }
}
