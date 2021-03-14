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
    public class EmailAddressController : ControllerBase
    {
        private static ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IMediator _mediator;

        public EmailAddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create new email address for mailing group
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// POST /api/EmailAddress
        /// {
        ///     "MailingGroupId": 1337,
        ///     "Addresses": [ "email1@address.com", "email2@address.com", "email3@address.com" ]
        /// }
        /// </remarks>
        /// <response code="201">Added new email addresses to mailing group.</response>
        /// <response code="400">Invalid email address.</response>
        /// <response code="409">Email address exists in mailing group.</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateEmailAddress([FromBody] CreateEmailAddressRequest request)
        {
            Logger.Trace($"Executing '{nameof(CreateEmailAddress)}'.");

            var userId = HttpContext.GetUserId();
            if (userId == null)
                return Unauthorized();

            try
            {
                var result = await _mediator.Send(request);
                return result.GetResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Unexpected error thrown while executing '{nameof(CreateEmailAddressHandler)}'.");
                throw;
            }
        }

        /// <summary>
        /// Update email address
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// PUT /api/EmailAddress/1337
        /// {
        ///     "Address": "address1@email.com"
        /// }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Added new email addresses to mailing group.</response>
        /// <response code="400">Invalid email address.</response>
        /// <response code="404">Email address not found.</response>
        /// <response code="409">Email address exists in mailing group.</response>
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateEmailAddress([FromRoute][Required] int id, [FromBody] UpdateEmailAddressDto dto)
        {
            Logger.Trace($"Executing '{nameof(UpdateEmailAddress)}'.");

            var userId = HttpContext.GetUserId();
            if (userId == null)
                return Unauthorized();

            var request = new UpdateEmailAddressRequest(id, dto.Address);
            request.SetUserId(userId.Value);

            try
            {
                var result = await _mediator.Send(request);
                return result.GetResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Unexpected error thrown while executing '{nameof(UpdateEmailAddressHandler)}'.");
                throw;
            }
        }

        /// <summary>
        /// Delete email address
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// DELETE /api/EmailAddress/1337
        /// 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Added new email addresses to mailing group.</response>
        /// <response code="400">Invalid email address.</response>
        /// <response code="404">Email address not found.</response>
        /// <response code="409">Email address exists in mailing group.</response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEmailAddress([FromRoute][Required] int id)
        {
            Logger.Trace($"Executing '{nameof(DeleteEmailAddress)}'.");

            var userId = HttpContext.GetUserId();
            if (userId == null)
                return Unauthorized();

            try
            {
                var request = new DeleteEmailAddressRequest(id);
                request.SetUserId(userId.Value);

                var result = await _mediator.Send(request);
                return new OkResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Unexpected error thrown while executing '{nameof(DeleteEmailAddressHandler)}'.");
                throw;
            }
        }

        /// <summary>
        /// Get existing email addresses from concrete mailingGroup
        /// </summary>
        /// <remarks>
        /// Sample requests:
        /// 
        /// GET /api/EmailAddress?mailingGroupId=1337
        /// 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Return emailAddresses list.</response>
        [HttpGet("{mailingGroupId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RetrieveEmailAddresses([FromRoute] int mailingGroupId)
        {
            Logger.Trace($"Executing '{nameof(RetrieveEmailAddresses)}'.");

            var userId = HttpContext.GetUserId();
            if (userId == null)
                return Unauthorized();

            var request = new RetrieveMailsRequest(mailingGroupId);
            request.SetUserId(userId.Value);

            try
            {
                var result = await _mediator.Send(request);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Unexpected error thrown while executing '{nameof(RetrieveEmailAddressesHandler)}'.");
                throw;
            }
        }

        /// <summary>
        /// Get all existing email addresses
        /// </summary>
        /// <remarks>
        /// Sample requests:
        /// 
        /// GET /api/EmailAddress
        /// 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Return emailAddresses list.</response>
        [HttpGet()]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RetrieveEmailAddresses()
        {
            Logger.Trace($"Executing '{nameof(RetrieveEmailAddresses)}'.");

            var userId = HttpContext.GetUserId();
            if (userId == null)
                return Unauthorized();

            var request = new RetrieveMailsRequest(null);
            request.SetUserId(userId.Value);

            try
            {
                var result = await _mediator.Send(request);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Unexpected error thrown while executing '{nameof(RetrieveEmailAddressesHandler)}'.");
                throw;
            }
        }
    }
}
