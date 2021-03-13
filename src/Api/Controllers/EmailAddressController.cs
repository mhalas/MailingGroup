using Api.Extensions;
using Business.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmailAddressController : ControllerBase
    {
        private static ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IMediator _mediator;

        public EmailAddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmailAddress(CreateEmailAddressRequest request)
        {
            Logger.Trace($"Executing '{nameof(CreateEmailAddress)}'.");

            try
            {
                var userId = HttpContext.GetUserId();
                if (userId == null)
                    return Unauthorized();

                var result = await _mediator.Send(request);
                return result.GetResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Unexpected error.");
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmailAddress(int id, [FromBody]string emailAddress)
        {
            Logger.Trace($"Executing '{nameof(UpdateEmailAddress)}'.");

            try
            {
                var userId = HttpContext.GetUserId();
                if (userId == null)
                    return Unauthorized();

                var request = new UpdateEmailAddressRequest(id, emailAddress);
                request.SetUserId(userId.Value);

                var result = await _mediator.Send(request);
                return result.GetResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Unexpected error.");
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmailAddress(int id)
        {
            Logger.Trace($"Executing '{nameof(DeleteEmailAddress)}'.");

            try
            {
                var userId = HttpContext.GetUserId();
                if (userId == null)
                    return Unauthorized();

                var request = new DeleteEmailAddressRequest(id);
                request.SetUserId(userId.Value);

                var result = await _mediator.Send(request);
                return new OkResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Unexpected error.");
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> RetrieveEmailAddress(RetrieveMailsRequest request)
        {
            Logger.Trace($"Executing '{nameof(RetrieveEmailAddress)}'.");

            try
            {
                var userId = HttpContext.GetUserId();
                if (userId == null)
                    return Unauthorized();

                request.SetUserId(userId.Value);

                var result = await _mediator.Send(request);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Unexpected error.");
                throw;
            }
        }
    }
}
