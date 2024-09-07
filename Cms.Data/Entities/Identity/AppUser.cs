using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text;
using Cms.Data.Interfaces;
using Cms.Data.Entities.Zaloka;
using Cms.Infrastructure.Enums;

namespace Cms.Data.Entities.Identity
{
    [Table("AppUsers")]
    public class AppUser : IdentityUser<Guid>, IDateTracking, IActive
    {
        //public AppUser()
        //{
        //}
        //public AppUser(string fullName, string userName, string email, string phoneNumber, string avatar)
        //{
        //    FullName = fullName;
        //    UserName = userName;
        //    Email = email;
        //    PhoneNumber = phoneNumber;
        //    Avatar = avatar;
           
        //}
        public string FullName { get; set; }

        public DateTime? BirthDay { set; get; }

        public decimal Balance { get; set; }

        public string Avatar { get; set; }

        public bool Gender { get; set; }
        public int UserType { get; set; }

        public string Address { get; set; }

        public DateTime DateCreated { set; get; }

        public DateTime? DateModified { set; get; }

        public DateTime? DateDeleted { set; get; }

        public int? SelectedOaId { get; set; }
        public bool IsActive { get; set; }      

        [ForeignKey("SelectedOaId")]
        public virtual OAInfomation OAInfomation { get; set; }

        //public int? MessageGroupId { get; set; }
        //[ForeignKey("MessageGroupId")]
        //public virtual MessageGroup MessageGroup { get; set; }

        //    public ICollection<string> Roles { get; set; }
        public Status Status { get; set; }
    }
}
