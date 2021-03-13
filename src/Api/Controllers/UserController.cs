using Api.Extensions;
using Business.Handlers;
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
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private static ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("register")]

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

        [HttpPost]
        [Route("login")]
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
