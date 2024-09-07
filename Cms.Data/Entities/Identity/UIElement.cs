using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Identity
{
    [Table("UIElement")]
    public class UIElement : DomainEntity<Guid>
    {
        public string Name { get; set; }

        public string Caption { get; set; }

        public string TargetURL { get; set; }

        public Guid? ParentId { get; set; }

        public string IconPath { get; set; }

        public int Order { get; set; }

        public DateTime? DateModified { set; get; }

        public string Type { get; set; }

        public virtual IEnumerable<RoleUIElement> RoleUIElements { get; set; }
    }
}
