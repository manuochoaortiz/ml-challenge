using System;
using System.Collections.Generic;
using System.Text;

namespace IpTracker.Domain.Common
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        //IUnitOfWork UnitOfWork { get; }
    }
}
