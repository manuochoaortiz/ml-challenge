using System;
using System.Collections.Generic;
using System.Text;

namespace IpTracker.Domain.Common
{
    public interface IService<T> where T : IAggregateRoot
    {
    }
}
