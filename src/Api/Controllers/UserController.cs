using Api.Extensions;
using Business.Handlers;
using Business.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// POST /api/User/register
        /// {
        ///     "username": "FooUsername"
        ///     "password": "BarPassword"
        /// }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="201">Registration success.</response>
        /// <response code="409">User already exists.</response>
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody]RegisterUserRequest request)
        {
            Logger.Trace($"Executing '{nameof(Register)}'.");

            try
            {
                var result = await _mediator.Send(request).ConfigureAwait(false);
                return result.GetResult();
            }
            catch(Exception ex)
            {
                Logger.Error(ex, $"Unexpected error thrown while executing '{nameof(RegisterUserHandler)}'.");
                throw;
            }
        }

        /// <summary>
        /// Login already existing user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// POST /api/User/login
        /// {
        ///     "username": "FooUsername"
        ///     "password": "BarPassword"
        /// }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Login success.</response>
        /// <response code="401">Password incorrect.</response>
        /// <response code="404">User not found.</response>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody]LoginUserRequest request)
        {
            Logger.Trace($"Executing '{nameof(Login)}'.");

            try
            {
                var result = await _mediator.Send(request).ConfigureAwait(false);
                return result.GetResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Unexpected error thrown while executing '{nameof(LoginUserHandler)}'.");
                throw;
            }
        }
    }
}
