using Api.Extensions;
using Business.Handlers;
using Business.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;

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

            var userId = HttpContext.GetUserId();
            if (userId == null)
                return Unauthorized();

            request.SetUserId(userId.Value);

            try
            {
                var id = _mediator.Send(request);
                return new CreatedResult("", id);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Unexpected error thrown while executing '{nameof(CreateMailingGroupHandler)}'.");
                throw;
            }
        }

        [HttpPut]
        public IActionResult UpdateMailingGroup(UpdateMailingGroupRequest request)
        {
            Logger.Trace($"Executing '{nameof(UpdateMailingGroup)}'.");

            var userId = HttpContext.GetUserId();
            if (userId == null)
                return Unauthorized();

            request.SetUserId(userId.Value);

            try
            {
                var id = _mediator.Send(request);
                return new OkResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Unexpected error thrown while executing '{nameof(UpdateMailingGroupHandler)}'.");
                throw;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMailingGroup(DeleteMailingGroupRequest request)
        {
            Logger.Trace($"Executing '{nameof(DeleteMailingGroup)}'.");

            var userId = HttpContext.GetUserId();
            if (userId == null)
                return Unauthorized();

            request.SetUserId(userId.Value);

            try
            {
                var id = _mediator.Send(request);
                return new OkResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Unexpected error thrown while executing '{nameof(DeleteMailingGroupHandler)}'.");
                throw;
            }
        }

        [HttpGet]
        public IActionResult RetrieveMailingGroups(RetrieveMailingGroupsRequest request)
        {
            Logger.Trace($"Executing '{nameof(RetrieveMailingGroups)}'.");

            var userId = HttpContext.GetUserId();
            if (userId == null)
                return Unauthorized();

            request.SetUserId(userId.Value);

            try
            {
                var result = _mediator.Send(request);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Unexpected error thrown while executing '{nameof(RetrieveMailingGroupsHandler)}'.");
                throw;
            }
        }
    }
}
