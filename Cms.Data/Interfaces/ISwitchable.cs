using Cms.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Data.Interfaces
{
    public interface ISwitchable
    {
        Status Status { set; get; }
    }
}
