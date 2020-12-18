using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IpTracker.Api.Application.Queries
{
    public class GetCounterCountryQuery : IRequest<GetCounterCountryViewModel>
    {
    }
}
