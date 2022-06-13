using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workshop.Web.Dtos.Public.User;
using Workshop.Web.Exceptions;
using Workshop.Web.Features.Public.User.Command;

namespace Workshop.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SecurityController : BaseWebController
    {
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userDto, CancellationToken cancellationToken)
        {
            var loginCommand = new LoginCommand()
                {Login = userDto.Login.ToLower(), Password = userDto.Password, EventId = userDto.EventId};

            try
            {
                await Mediator.Send(loginCommand, cancellationToken);

                if (UserProfileProvider.GetProfile() == null)
                {
                    return Unauthorized();
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return Conflict(e.Message + " : " + e.InnerException);
            }
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            Response.Cookies.Delete("SID");
            return NoContent();
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="registrationDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Registration")]
        public async Task<IActionResult> Registration([FromBody] UserRegistrationDto registrationDto,
            CancellationToken token)
        {
            var registrationCommand = new RegistrationCommand()
            {
                Login = registrationDto.Login.ToLower(),
                Password = registrationDto.Password,
                EventId = registrationDto.EventId
            };

            try
            {
                await Mediator.Send(registrationCommand, token);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Регистрация нового пользователя через сайт урфу
        /// </summary>
        /// <param name="registrationDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RegistrationWithUrfu")]
        public async Task<IActionResult> RegistrationWithUrfu([FromBody] UserRegistrationDto registrationDto,
            CancellationToken token)
        {
            var registrationCommand = new RegistrationWithUrfuCommand
            {
                Login = registrationDto.Login.ToLower(),
                Password = registrationDto.Password,
                EventId = registrationDto.EventId
            };

            try
            {
                await Mediator.Send(registrationCommand, token);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Обновление пароля пользователя
        /// </summary>
        /// <param name="updatePasswordDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] UserUpdatePasswordDto updatePasswordDto,
            CancellationToken token)
        {
            await Mediator.Send(new UpdatePasswordCommand
                {OldPassword = updatePasswordDto.OldPassword, NewPassword = updatePasswordDto.NewPassword}, token);

            return NoContent();
        }

        /// <summary>
        /// Обновить пароль, после того, как обновил пароль на сайте урфу
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        [AllowAnonymous]
        [Route("RefreshPassword")]
        public async Task<IActionResult> RefreshPassword([FromBody] UserRegistrationDto registrationDto,
            CancellationToken token)
        {
            var refreshPasswordCommand = new RefreshPasswordCommand
            {
                Login = registrationDto.Login.ToLower(),
                Password = registrationDto.Password,
                EventId = registrationDto.EventId
            };

            try
            {
                await Mediator.Send(refreshPasswordCommand, token);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}