using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Workshop.Web.Dtos.Expert.Team;
using Workshop.Web.Dtos.Public.Team;
using Workshop.Web.Exceptions;
using Workshop.Web.Features.Public.Team.Command;
using Workshop.Web.Features.Public.Project.Command;
using Workshop.Web.Features.Expert.Team.Command;
using System.Collections.Generic;
using Workshop.Web.Dtos.Public.Auditorium;
using Workshop.Web.Features.Public.Auditorium.Query;

namespace Workshop.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TeamController : BaseWebController
    {
        /// <summary>
        /// Начать тестирование команды
        /// (укомплектовать команду, несмотря на количество участников)
        /// </summary>
        /// <param name="startTestTeamDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("StartTesting")]
        [ProducesResponseType(204)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> StartTesting([FromBody] StartTestTeamDto startTestTeamDto,
            CancellationToken token)
        {
            try
            {
                await Mediator.Send(new StartTestTeamCommand { TeamId = startTestTeamDto.TeamId },
                    token);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
            return NoContent();
        }

        /// <summary>
        /// Открыть закрытую команду
        /// </summary>
        /// <param name="openTeamDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("OpenTeam")]
        [ProducesResponseType(204)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> OpenTeam([FromBody] StartTestTeamDto openTeamDto, CancellationToken token)
        {
            try
            {
                await Mediator.Send(new OpenTeamCommand { TeamId = openTeamDto.TeamId },
                    token);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
            return NoContent();
        }

        /// <summary>
        /// Завершение тестирования командой
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <response code="204"></response>
        /// <response code="403">У пользователя нет команды или команда не начала тестирование</response>
        [HttpPost("FinishTesting")]
        [ProducesResponseType(204)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> FinishTesting(CancellationToken token)
        {
            try
            {
                await Mediator.Send(new FinishTestingCommand(), token);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }

            return NoContent();
        }

        /// <summary>
        /// Присоединение пользователя к проекту
        /// </summary>
        /// <param name="joinTeamParameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="204">Success</response>
        /// <response code="404">Не удалось найти данные по переданным параметрам</response>       
        /// <response code="409">Не удалось присоедениться к проекту - было создано максимальное количество команд</response>       
        [HttpPost]
        [Route("Join")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Join([FromBody] JoinTeamDto joinTeamParameters,
            CancellationToken cancellationToken)
        {
            try
            {
                await Mediator.Send(new JoinTeamCommand()
                { TeamId = joinTeamParameters.TeamId, RoleId = joinTeamParameters.RoleId }, cancellationToken);
            }
            catch (ConflictException)
            {
                return Conflict();
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
            catch (NotFoundException)
            {
                return StatusCode(404);
            }

            return NoContent();
        }

        /// <summary>
        /// Переименовать команду
        /// </summary>
        /// <param name="value"></param>
        [HttpPost("Rename")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> RenameTeam([FromBody] RenameTeamDto value, CancellationToken cancellationToken)
        {
            try
            {
                await Mediator.Send(new RenameTeamCommand() { TeamId = value.TeamId, Name = value.Name },
                    cancellationToken);
            }
            catch (NotFoundException)
            {
                return new NotFoundResult();
            }
            catch (ForbiddenException)
            {
                return new ForbidResult();
            }
            return NoContent();
        }

        /// <summary>
        /// Выход пользователя из неполной команды
        /// </summary>
        /// <response code="204"></response>
        /// <response code="404">Не найден пользователь или команда</response>
        /// <response code="403">Если пытаться выйти из заполненной команды</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [HttpDelete("LeaveTeam", Name = "LeaveTeam")]
        public async Task<IActionResult> LeaveTeam(CancellationToken cancellationToken)
        {
            try
            {
                await Mediator.Send(new LeaveTeamCommand() { UserId = UserProfileProvider.GetProfile().User.Key },
                    cancellationToken);
            }
            catch (NotFoundException)
            {
                return new NotFoundResult();
            }
            catch (ForbiddenException)
            {
                return new ForbidResult();
            }

            return NoContent();
        }

        /// <summary>
        /// Получение списка аудиторий
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("GetAuditoriums")]
        public async Task<ICollection<AuditoriumDto>> GetAuditoriums(CancellationToken token)
        {
            return await Mediator.Send(new AuditoriumsGetCommand(), token);
        }
        
        /// <summary>
        /// Изменение ссылок на ресурсы связанные с проектом
        /// </summary>
        [HttpPost("UpdateLinks")]
        public async Task<IActionResult> UpdateLinks([FromBody] UpdateLinksDto updateLinksDto, CancellationToken cancellationToken)
        {
            try
            {
                await Mediator.Send(new UpdateLinksCommand()
                {
                    TeamId = updateLinksDto.TeamId,
                    PMSLink = updateLinksDto.PMSLink,
                    RepositoryLink = updateLinksDto.RepositoryLink,
                    AdditionalLink = updateLinksDto.AdditionalLink
                }, cancellationToken);
            }
            catch (NotFoundException)
            {
                return new NotFoundResult();
            }
            catch (ForbiddenException)
            {
                return new ForbidResult();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}