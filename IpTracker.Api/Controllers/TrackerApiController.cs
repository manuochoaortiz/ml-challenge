using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IpTracker.Api.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IpTracker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrackerApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TrackerApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("TrackByIp")]
        public async Task<ActionResult<GetIpTrackByIpViewModel>> TrackByIp(string ip)
        {
            try
            {
                var request = new GetIpTrackByIpQuery() { Ip = ip };
                request.ValidateRequest();
                var response = await _mediator.Send(request);
                return response;
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }

        [HttpGet("GetCounterCountry")]
        public async Task<ActionResult<GetCounterCountryViewModel>> GetCounterCountry()
        {
            try
            {
                var response = await _mediator.Send(new GetCounterCountryQuery());
                return response;
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }

    }
}
