using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Cells;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workshop.Core.Helpers;
using Workshop.Web.Dtos.Expert.Event;
using Workshop.Web.Dtos.Expert.Project;
using Workshop.Web.Dtos.Admin.Statistic;
using Workshop.Web.Dtos.Public.User;
using Workshop.Web.Features.Admin.Command;
using Workshop.Web.Features.Admin.Event.Query;
using Workshop.Web.Features.Admin.GenerateData;
using Workshop.Web.Features.Admin.Statistic.Query;
using Workshop.Web.Features.Public.Event;
using Workshop.Web.Features.Public.User.Generate;
using Workshop.Web.Features.Admin.Team.Command;
using Workshop.Web.Features.Admin.User.Query;

namespace Workshop.Web.Controllers
{
    [Route("api/v1/admin/[controller]")]
    [ApiController]
    public class AdminTeamController : BaseWebController
    {
        /// <summary>
        /// Добавление участника в команду вручную администратором
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUserAtTeam([FromBody] UserAddAtTeamDto request, CancellationToken token)
        {
            await Mediator.Send(
                new AddUserAtTeamCommand()
                { UserId = request.UserId, TeamId = request.TeamId, RoleId = request.RoleId }, token);
            return NoContent();
        }

        /// <summary>
        /// Добавление новой команды к проекту вручную администратором
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("AddNewTeam")]
        public async Task<IActionResult> AddNewTeam(long projectId, CancellationToken token)
        {
            await Mediator.Send(new AddNewTeamCommand() { ProjectId = projectId }, token);
            return NoContent();
        }

        /// <summary>
        /// Удаление команды
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete("DeleteTeam")]
        public async Task<IActionResult> DeleteTeam(long teamId, CancellationToken token)
        {
            await Mediator.Send(new DeleteTeamCommand() { TeamId = teamId }, token);
            return NoContent();
        }

        /// <summary>
        /// Удаление участника из команды в ручную администратором
        /// </summary> 
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete("RemoveUser")]
        public async Task<IActionResult> RemoveUserFromTeam(long userId, CancellationToken token)
        {
            await Mediator.Send(new RemoveUserFromTeamCommand() { UserId = userId }, token);
            return NoContent();
        }

        /// <summary>
        /// Получение списка пользователей, которые не состоят в командах или находятся в неполной команде
        /// </summary>
        /// <param name="term"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("GetFreeUsers")]
        public async Task<ICollection<ProfileUserDto>> GetFreeUsers([FromQuery] string term, CancellationToken token)
        {
            return await Mediator.Send(new FreeUsersGetQuery() { Term = term }, token);
        }

        /// <summary>
        /// Изменить проектную роль пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut("ChangeUserRole")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> ChangeUserRole([FromBody] UserRoleChangeDto request, CancellationToken token)
        {
            await Mediator.Send(new ChangeUserRoleCommand
            {
                UserId = request.UserId,
                RoleId = request.RoleId
            }, token);

            return NoContent();
        }
        [HttpPut("UpdateTeamEntried")]
        [ProducesResponseType(403)]
        public async Task<IActionResult> UpdateTeamEntried(long id, CancellationToken cancellationToken)
        {
            try
            {
                await Mediator.Send(new UpdateTeamEntriedCommand { TeamId = id }, cancellationToken);
            }
            catch(Exception e)
            {
                return StatusCode(403);
            }
            return NoContent();
        }

        [HttpPut]
        [Route("UpdateTeamProject")]

            public async Task<IActionResult> UpdateTeamProject(long projectId, long teamId,
            CancellationToken cancellationToken)
        {
            try
            {
                await Mediator.Send(new UpdateTeamProject() {ProjectId = projectId, TeamId = teamId},
                    cancellationToken);
            }
            catch (Exception e)
            {
                return StatusCode(403);
            }

            return NoContent();
        }
    }
}