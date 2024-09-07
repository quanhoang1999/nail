using Cms.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cms.Service.ViewModel.Content
{
    public class FeedbackViewModel
    {
        public Guid Id { get; set; }
        [StringLength(250)]
        [Required]
        public string Name { set; get; }

        [StringLength(250)]
        public string Email { set; get; }

        [StringLength(500)]
        public string Message { set; get; }

        public string Phone { set; get; }

        public Status Status { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime? DateModified { set; get; }
        public DateTime? DateDeleted { set; get; }
    }
}
