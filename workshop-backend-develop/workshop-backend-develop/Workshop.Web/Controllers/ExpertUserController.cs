using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Workshop.Web.Dtos.Expert.User;
using Workshop.Web.Dtos.Public.User;
using Workshop.Web.Exceptions;
using Workshop.Web.Features.Admin.User.Query;
using Workshop.Web.Features.Public.ExpertUser.Command;
using Workshop.Web.Features.Public.ExpertUser.Query;

namespace Workshop.Web.Controllers
{
    [Route("api/v1/expert/[controller]")]
    [ApiController]
    public class ExpertUserController : BaseWebController
    {
        /// <summary>
        /// Получить информацию о профиле текущего эксперта
        /// </summary>
        /// <param name="token"></param>
        /// <returns>UserDto</returns>
        [HttpGet]
        public async Task<ActionResult<ExpertUserDto>> GetUser(CancellationToken token)
        {
            return await Mediator.Send(new ExpertUserGetQuery() { Id = UserProfileProvider.GetProfile().User.Key }, token);
        }

        /// <summary>
        /// Получить информацию о профиле эксперта по Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns>UserDto</returns>
        [HttpGet("{id}", Name = "GetExpertUser")]
        public async Task<ActionResult<ExpertUserDto>> GetUserById([FromRoute] long id, CancellationToken token)
        {
            return await Mediator.Send(new ExpertUserGetQuery() { Id = id }, token);
        }

        /// <summary>
        /// Обновление аудитории эксперта
        /// </summary>
        /// <param name="auditoriumDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("UpdateExpert")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateAuditorium([FromBody] ExpertUserAuditoriumDto auditoriumDto, CancellationToken token)
        {
            await Mediator.Send(new UpdateAuditoriumCommand()
            { ExpertUserId = auditoriumDto.ExpertId, AuditoriumId = auditoriumDto.AuditoriumId });
            return NoContent();
        }

        /// <summary>
        ///     Замена эксперта в аудитории
        /// </summary>
        /// <param name="changeAuditoriumDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("ChangeAuditoriumExpert")]
        public async Task<IActionResult> ChangeExpert(
            [FromBody] ExpertUserChangeAuditoriumDto changeAuditoriumDto,
            CancellationToken token)
        {
            try
            {
                await Mediator.Send(new ChangeAuditoriumCommand
                {
                    CurrentExpertId = changeAuditoriumDto.CurrentExpertId,
                    NewExpertId = changeAuditoriumDto.NewExpertId,
                    AuditoriumId = changeAuditoriumDto.AuditoriumId
                }, token);
            }
            catch (ForbiddenException)
            {
                return new ForbidResult();
            }

            return NoContent();
        }

        /// <summary>
        /// Получение списка экспертов и их аудиторий
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("GetAvailableExperts")]
        public async Task<ICollection<ExpertUserDto>> GetAvailableExperts(CancellationToken token)
        {
            var users = await Mediator.Send(new ExpertUsersGetAvailableQuery(), token);
            return users;
        }
    }
}
