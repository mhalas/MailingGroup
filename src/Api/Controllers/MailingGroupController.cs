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

        private int _userId = 1;

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
        public IActionResult RetrieveMailingGroups(RetrieveMailingGroupRequest request)
        {
            Logger.Trace($"Executing '{nameof(RetrieveMailingGroups)}'.");

            try
            {
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
