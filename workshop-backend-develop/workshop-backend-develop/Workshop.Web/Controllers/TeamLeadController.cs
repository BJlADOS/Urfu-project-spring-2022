using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Workshop.Web.Dtos.Public.User;
using Workshop.Web.Dtos.TeamLead;
using Workshop.Web.Exceptions;
using Workshop.Web.Features.TeamLead;
using Workshop.Web.Features.TeamLead.Command;

namespace Workshop.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TeamLeadController : BaseWebController
    {
        /// <summary>
        /// Получение списка всех пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetStudentsFiltered")]
        public async Task<ActionResult<ICollection<ShortUserDto>>> GetStudentsFiltered([FromQuery] string term,
            [FromQuery] string competenciesIds, CancellationToken token, int pageNumber, long? lastItemId)

        {
            var result = await Mediator.Send(
                new GetStudentsFilteredByQuery
                {
                    Term = term,
                    CompetenciesIds = competenciesIds != null
                        ? new List<long>(competenciesIds.Split(',').Select(long.Parse))
                        : new List<long>(),
                    PageNumber = pageNumber, LastElement = lastItemId
                }, token);
            return result;
        }


        [HttpPost("AddNewTeam")]
        public async Task<IActionResult> AddNewTeam(long projectId, CancellationToken token)
        {
            try
            {
                await Mediator.Send(
                    new AddNewTeamCommand {ProjectId = projectId}, token);
            }
            catch (Exception e)
            {
                return Conflict(e);
            }

            return NoContent();
        }

        [HttpDelete("DeleteTeam")]
        public async Task<IActionResult> LeaveTeam(CancellationToken cancellationToken)
        {
            try
            {
                await Mediator.Send(new DeleteTeamCommand() {User = UserProfileProvider.GetProfile().User},
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

        [HttpPut("UpdateProjectsForEntry")]
        public async Task<IActionResult> UpdateProjectsForEntry(long id, CancellationToken cancellationToken)
        {
            try
            {
                await Mediator.Send(new UpdateProjectsForEntryCommand() {TeamId = id}, cancellationToken);
            }
            catch (ForbiddenException)
            {
                return new ForbidResult();
            }

            return NoContent();
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUserAtTeam([FromBody] UserAddAtTeamFromRequestDto request,
            CancellationToken token)
        {
            try
            {
                await Mediator.Send(
                    new AddUserInTeamCommand()
                        {UserId = request.UserId, TeamId = request.TeamId, RoleName = request.RoleName}, token);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }

            return NoContent();
        }
    }
}