using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Application;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TracerApiController : ControllerBase, ITracerApplication
    {
        private readonly ILogger<TracerApiController> _logger;
        private readonly ITracerApplication _tracerApplication;

        public TracerApiController(ILogger<TracerApiController> logger
            , ITracerApplication tracerApplication)
        {
            _logger = logger;
            _tracerApplication = tracerApplication;
        }

        public InfoIP ByIP(string ip)
        {
            var infoIP = _tracerApplication.ByIP(ip);
            return infoIP;
        }
    }
}
