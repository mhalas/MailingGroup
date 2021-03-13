using Api.Extensions;
using Business.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Linq;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MailingGroupController : ControllerBase
    {
        private static ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IMediator _mediator;

        public MailingGroupController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public IActionResult CreateMailingGroup(CreateMailingGroupRequest request)
        {
            Logger.Trace($"Executing '{nameof(CreateMailingGroup)}'.");
            try
            {
                var userId = HttpContext.GetUserId();
                if (userId == null)
                    return Unauthorized();

                request.SetUserId(userId.Value);

                var id = _mediator.Send(request);
                return new CreatedResult("", id);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Unexpected error.");
                throw;
            }
        }

        [HttpPut]
        public IActionResult UpdateMailingGroup(UpdateMailingGroupRequest request)
        {
            Logger.Trace($"Executing '{nameof(UpdateMailingGroup)}'.");

            try
            {
                var userId = HttpContext.GetUserId();
                if (userId == null)
                    return Unauthorized();

                request.SetUserId(userId.Value);

                var id = _mediator.Send(request);
                return new OkResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Unexpected error.");
                throw;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMailingGroup(DeleteMailingGroupRequest request)
        {
            Logger.Trace($"Executing '{nameof(DeleteMailingGroup)}'.");

            try
            {
                var userId = HttpContext.GetUserId();
                if (userId == null)
                    return Unauthorized();

                request.SetUserId(userId.Value);

                var id = _mediator.Send(request);
                return new OkResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Unexpected error.");
                throw;
            }
        }

        [HttpGet]
        public IActionResult RetrieveMailingGroups(RetrieveMailingGroupsRequest request)
        {
            Logger.Trace($"Executing '{nameof(RetrieveMailingGroups)}'.");

            try
            {
                var userId = HttpContext.GetUserId();
                if (userId == null)
                    return Unauthorized();

                request.SetUserId(userId.Value);

                var result = _mediator.Send(request);
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
