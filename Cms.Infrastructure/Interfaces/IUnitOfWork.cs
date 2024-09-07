using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Infrastructure.Interfaces
{
   public interface IUnitOfWork: IDisposable
    {
        void Commit();
    }
}
