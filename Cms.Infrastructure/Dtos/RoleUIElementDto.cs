using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Infrastructure.Dtos
{
    public class RoleUIElementDto
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public string Name { get; set; }

        public bool Checked { get; set; }
    }
}
