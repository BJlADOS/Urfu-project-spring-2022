using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Workshop.Web.Dtos.Expert.Team;
using Workshop.Web.Dtos.Public.Team;
using Workshop.Web.Exceptions;
using Workshop.Web.Features.Expert.Team.Command;
using Workshop.Web.Features.Expert.Team.Query;

namespace Workshop.Web.Controllers
{
    [Route("api/v1/expert/[controller]")]
    [ApiController]
    public class ExpertTeamController : BaseWebController
    {
        /// <summary>
        /// Получение информации о команде по Id
        /// </summary>
        /// <param name="id">Id команды</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetTeam")]
        public async Task<ActionResult<TeamDto>> GetTeam([FromRoute] long id, CancellationToken token)
        {
            try
            {
                return await Mediator.Send(new ExpertTeamGetQuery() { TeamId = id }, token);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Получить список команд
        /// </summary>
        /// <param name="getTeamsDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ICollection<TeamListItemDto>> GetTeams([FromQuery] GetTeamsDto getTeamsDto, CancellationToken token)
        {
            return await Mediator.Send(new ExpertTeamsGetQuery { Term = getTeamsDto.Term }, token);
        }

        /// <summary>
        /// Получить список команд для оценки
        /// </summary>
        /// <param name="getTeamsDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("GetReviewTeams")]
        public async Task<ICollection<TeamWithSlotListDto>> GetReviewTeams([FromQuery] GetTeamsDto getTeamsDto, CancellationToken token)
        {
            return await Mediator.Send(new ExpertReviewTeamsGetQuery { Term = getTeamsDto.Term }, token);
        }

        /// <summary>
        /// Вызвать команду к эксперту (в аудиторию) 
        /// </summary>
        /// <param name="callTeamDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("CallTeam")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> CallTeam([FromBody] CallTeamDto callTeamDto, CancellationToken token)
        {
            await Mediator.Send(new CallTeamCommand { TeamId = callTeamDto.TeamId, AuditoriumId = callTeamDto.AuditoriumId },
                token);
            return NoContent();
        }

        /// <summary>
        /// Закончить работу с командой
        /// </summary>
        /// <param name="finishTeamDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("FinishTeam")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> FinishTeam([FromBody] FinishTeamDto finishTeamDto, CancellationToken token)
        {
            await Mediator.Send(
                new FinishTeamCommand()
                { TeamId = finishTeamDto.TeamId, Comment = finishTeamDto.Comment, Mark = finishTeamDto.Mark },
                token);
            return NoContent();
        }

        /// <summary>
        /// Обновить оценку команды.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut("UpdateTeamReview")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateTeamReview([FromBody] UpdateTeamReviewDto dto, CancellationToken token)
        {
            try
            {
                await Mediator.Send(new UpdateTeamReviewCommand
                {
                    TeamId = dto.TeamId,
                    GoalsAndTasks = dto.Marks.GoalsAndTasks,
                    Solution = dto.Marks.Solution,
                    Impact = dto.Marks.Impact,
                    Presentation = dto.Marks.Presentation,
                    Technical = dto.Marks.Technical,
                    Result = dto.Marks.Result,
                    Knowledge = dto.Marks.Knowledge
                }, token);
            }
            catch (Exception e)
            {
                return Conflict(e);
            }
            return NoContent();
        }

        /// <summary>
        /// Обновить оценку компетенций команды.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut("UpdateTeamCompetencyReview")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateTeamCompetencyReview([FromBody] UpdateTeamCompetencyReviewDto dto, CancellationToken token)
        {
            try
            {
                await Mediator.Send(new UpdateTeamCompetenyReviewCommand
                {
                    TeamId = dto.TeamId,
                    CompetencyId = dto.CompetencyId,
                    Mark = dto.Mark
                }, token);
            }
            catch (Exception e)
            {
                return Conflict(e);
            }
            return NoContent();
        }
    }
}