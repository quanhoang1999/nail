using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Service.ViewModel.Nails
{
    public class NailEmployeeViewModel
    {
        public Guid Id { get; set; }
        [MaxLength(256)]
        public string Name { get; set; }
        [Column(TypeName = "varchar(3)")]
        public string ShortName { get; set; }
        public string Avatar { get; set; }
        public string AvatarFullUrl { get; set; }

        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public virtual NailStoreViewModel NailStore { get; set; }
        public int NailStoreId { get; set; }
        public string NailStoreName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeteted { get; set; } = false;
        public Guid UserCreated { get; set; }
        public Guid? UserModified { get; set; }
        public Guid? UserDeleted { get; set; }
    }
}
