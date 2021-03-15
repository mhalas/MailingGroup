using Api.CustomRequests;
using Api.Extensions;
using Business.Handlers;
using Business.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MailingGroupController : ControllerBase
    {
        private static ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IMediator _mediator;

        public MailingGroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create new mailing group
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// POST /api/MailingGroup
        /// {
        ///     "name": "MySpamMailingGroup"
        /// }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="201">Added new mailing group for logged user.</response>
        /// <response code="409">Mailing group with passed name already exists for logged user.</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateMailingGroup([FromBody]CreateMailingGroupRequest request)
        {
            Logger.Trace($"Executing '{nameof(CreateMailingGroup)}'.");

            var userId = HttpContext.GetUserId();
            if (userId == null)
                return Unauthorized();

            request.SetUserId(userId.Value);

            try
            {
                var result = await _mediator.Send(request);
                return result.GetResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Unexpected error thrown while executing '{nameof(CreateMailingGroupHandler)}'.");
                throw;
            }
        }

        /// <summary>
        /// Update existing user mailing group
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// PUT /api/MailingGroup/1337
        /// {
        ///     "name": "MySpamMailingGroup"
        /// }
        /// </remarks>
        /// <response code="200">Update mailing group for logged user success.</response>
        /// <response code="404">Mailing group not found.</response>
        /// <response code="409">Mailing group with passed name already exists for logged user.</response>
        [HttpPut("{mailingGroupId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateMailingGroup([FromRoute][Required] int mailingGroupId, [FromBody]UpdateMailingGroupDto dto)
        {
            Logger.Trace($"Executing '{nameof(UpdateMailingGroup)}'.");

            var userId = HttpContext.GetUserId();
            if (userId == null)
                return Unauthorized();

            var request = new UpdateMailingGroupRequest(mailingGroupId, dto.Name);
            request.SetUserId(userId.Value);

            try
            {
                var result = await _mediator.Send(request);
                return result.GetResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Unexpected error thrown while executing '{nameof(UpdateMailingGroupHandler)}'.");
                throw;
            }
        }

        /// <summary>
        /// Delete existing user mailing group
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// DELETE /api/MailingGroup/1337
        /// 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Delete success.</response>
        /// <response code="404">Mailing group not found.</response>
        [HttpDelete("{mailingGroupId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMailingGroup([FromRoute][Required] int mailingGroupId)
        {
            Logger.Trace($"Executing '{nameof(DeleteMailingGroup)}'.");

            var userId = HttpContext.GetUserId();
            if (userId == null)
                return Unauthorized();

            var request = new DeleteMailingGroupRequest(mailingGroupId);
            request.SetUserId(userId.Value);

            try
            {
                var result = await _mediator.Send(request);
                return result.GetResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Unexpected error thrown while executing '{nameof(DeleteMailingGroupHandler)}'.");
                throw;
            }
        }

        /// <summary>
        /// Get existing user mailing groups
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// GET /api/MailingGroup
        /// 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Return mailing group list for logged user.</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RetrieveMailingGroups()
        {
            Logger.Trace($"Executing '{nameof(RetrieveMailingGroups)}'.");

            var userId = HttpContext.GetUserId();
            if (userId == null)
                return Unauthorized();

            var request = new RetrieveMailingGroupsRequest();
            request.SetUserId(userId.Value);

            try
            {
                var result = await _mediator.Send(request);
                return result.GetResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Unexpected error thrown while executing '{nameof(RetrieveMailingGroupsHandler)}'.");
                throw;
            }
        }

        /// <summary>
        /// Get existing single user mailing group by ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// GET /api/MailingGroup/1337
        /// 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Return mailing group list for logged user.</response>
        /// <response code="404">Mailing group not found.</response>
        [HttpGet("{mailingGroupId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RetrieveSingleMailingGroup(int mailingGroupId)
        {
            Logger.Trace($"Executing '{nameof(RetrieveSingleMailingGroup)}'.");

            var userId = HttpContext.GetUserId();
            if (userId == null)
                return Unauthorized();

            var request = new RetrieveSingleMailingGroupRequest(mailingGroupId);
            request.SetUserId(userId.Value);

            try
            {
                var result = await _mediator.Send(request);
                return result.GetResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Unexpected error thrown while executing '{nameof(RetrieveMailingGroupsHandler)}'.");
                throw;
            }
        }
    }
}
