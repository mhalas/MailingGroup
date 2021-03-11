using Api.Extensions;
using Business.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MailController : ControllerBase
    {
        private static ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IMediator _mediator;

        public MailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMails(CreateMailsRequest request)
        {
            Logger.Trace($"Executing '{nameof(CreateMails)}'.");

            try
            {
                var userId = GetUserId();
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
        public async Task<IActionResult> UpdateMail(int id, [FromBody]string emailAddress)
        {
            Logger.Trace($"Executing '{nameof(UpdateMail)}'.");

            try
            {
                var userId = GetUserId();
                if (userId == null)
                    return Unauthorized();

                var request = new UpdateMailRequest(id, emailAddress)
                {
                    UserId = userId.Value
                };

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
        public async Task<IActionResult> DeleteMail(int id)
        {
            Logger.Trace($"Executing '{nameof(DeleteMail)}'.");

            try
            {
                var userId = GetUserId();
                if (userId == null)
                    return Unauthorized();

                var request = new DeleteMailRequest(id)
                {
                    UserId = userId.Value
                };

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
        public async Task<IActionResult> RetrieveMails(RetrieveMailsRequest request)
        {
            Logger.Trace($"Executing '{nameof(RetrieveMails)}'.");

            try
            {
                var userId = GetUserId();
                if (userId == null)
                    return Unauthorized();

                request.UserId = userId.Value;

                var result = await _mediator.Send(request);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Unexpected error.");
                throw;
            }
        }

        private int? GetUserId()
        {
            if (HttpContext.User.HasClaim(x => x.Type == "UserId") &&
                int.TryParse(HttpContext.User.Claims.First(x => x.Type == "UserId").Value, out int userId))
            {
                return userId;
            }

            return null;
        }
    }
}
