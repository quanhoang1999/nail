using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.System
{
    [Table("AuditLog")]
    public class AuditLog : DomainEntity<Guid>
    {
        public string BrowserInfo { get; set; }
        public string ClientIpAddress { get; set; }

        public string ClientName { get; set; }
        public string CustomData { get; set; }

        public string Exception { get; set; }

        public int ExecutionDuration { get; set; }

        public DateTime ExecutionTime { get; set; }

        public Guid? ImpersonatorTenantId { get; set; }

        public Guid? ImpersonatorUserId { get; set; }

        public string MethodName { get; set; }

        public string Parameters { get; set; }

        public string ServiceName { get; set; }

        public Guid? TenantId { get; set; }

        public Guid? UserId { get; set; }
    }
}
