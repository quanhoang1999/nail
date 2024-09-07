
using Cms.Infrastructure.Enums;
using Cms.Service.ViewModel.Zaloka;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cms.Service.ViewModel.Identity
{
    public class RegisterViewModel
    {
        public Guid? Id { get; set; }

        public string Email { get; set; }

        public string Avatar { get; set; }
        public string AvatarFullUrl { get; set; }
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string PhoneNumber { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Address { get; set; }

        public bool Gender { get; set; }

        public ICollection<string> Roles { get; set; }
        public int SelectedOaId { get; set; }

        public virtual OAInfomationViewModel OAInfomation { get; set; }

        public DateTime DateCreated { get; set; }

        public Status Status { get; set; }

    }
}
