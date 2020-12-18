using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IpTracker.Api.Application.Queries
{
    public class GetIpTrackByIpQuery : IRequest<GetIpTrackByIpViewModel>
    {
        public string Ip { get; set; }

        public void ValidateRequest()
        {
            bool isValidIpAddress = IPAddress.TryParse(Ip, out _);
            if (string.IsNullOrEmpty(Ip) || string.IsNullOrWhiteSpace(Ip) || !isValidIpAddress)
            {
                throw new ApplicationException("Invalid Ip Request");
            }
        }
    }
}
