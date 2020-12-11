using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ApiService;
using Domain.Application;
using Domain.Application.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TracerApiController : ControllerBase, ITracerApiController
    {
        private readonly ILogger<TracerApiController> _logger;
        private readonly ITracerApplication _tracerApplication;

        public TracerApiController(ILogger<TracerApiController> logger
            , ITracerApplication tracerApplication)
        {
            _logger = logger;
            _tracerApplication = tracerApplication;
        }
        
        [HttpGet]
        public async Task<InfoIP> ByIP(string ip)
        //ActionResult?
        {
            var infoIP = await _tracerApplication.ByIP(ip);
            return infoIP;
        }
    }
}
