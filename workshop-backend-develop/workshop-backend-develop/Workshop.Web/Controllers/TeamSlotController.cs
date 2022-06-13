using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Workshop.Core.Domain.Model.TeamSlot;
using Workshop.Web.Dtos.Public.TeamSlot;
using Workshop.Web.Features.Public.TeamSlot.Command;
using Workshop.Web.Features.Public.TeamSlot.Query;

namespace Workshop.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TeamSlotController : BaseWebController
    {
        /// <summary>
        /// Получение слотов для записи в аудитории по Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<TeamSlotDto[]>> GetByAuditoriumId(
            long id, CancellationToken cancellationToken)
        {
            var command = new GetTeamSlotsByAuditoriumId { Id = id };
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Запись в слот Id команды TeamId
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SignTeam([FromBody] ShortTeamSlotDto dto,
                                                  CancellationToken cancellationToken)
        {
            await Mediator.Send(new SignTeamInTeamSlotCommand
            {
                Dto = dto
            }, cancellationToken);
            return NoContent();
        }
    }
}