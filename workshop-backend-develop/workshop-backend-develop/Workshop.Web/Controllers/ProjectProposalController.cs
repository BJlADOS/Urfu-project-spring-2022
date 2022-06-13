using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Workshop.Web.Dtos.Public.ProjectProposal;
using Workshop.Web.Features.Public.ProjectProposal;
using Workshop.Web.Features.Public.ProjectProposal.Command;
using Workshop.Web.Features.Public.ProjectProposal.Query;

namespace Workshop.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProjectProposalController : BaseWebController
    {
        /// <summary>
        /// Создание проектной заявки
        /// </summary>
        /// <param name="addProjectProposalDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostProjectProposal(
            [FromBody] AddProjectProposalDto addProjectProposalDto,
            CancellationToken cancellationToken)
        {
            try
            {
                await Mediator.Send(new AddProjectProposalCommand()
                {
                    AddProjectProposalDto = addProjectProposalDto
                }, cancellationToken);
            }
            catch (Exception e)
            {
                return Conflict(e);
            }

            return StatusCode(200);
        }
        [HttpGet("GetByUser")]
        [Produces("application/json")]
        public async Task<IActionResult> GetByUserProjectProposal(CancellationToken cancellationToken)
        {
              var result = await Mediator.Send(new GetProposalByAuthorIdCommand(), cancellationToken);
            return Ok(result);
        }
        /// <summary>
        /// Редактирование проектной заявки
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateProjectProposalDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProjectProposal(long id,
            [FromBody] AddProjectProposalDto updateProjectProposalDto,
            CancellationToken cancellationToken)
        {
            try
            {
                await Mediator.Send(new UpdateProjectProposalCommand()
                {
                    Id = id,
                    UpdateProjectProposalDto = updateProjectProposalDto
                }, cancellationToken);
            }
            catch (Exception e)
            {
                return Conflict(e);
            }

            return NoContent();
        }

        /// <summary>
        /// Обновление статуса проектной заявки
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateStatusDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateStatus(long id,
            [FromQuery] UpdateStatusDto updateStatusDto,
            CancellationToken cancellationToken)
        {
            try
            {
                await Mediator.Send(new UpdateStatusCommand
                {
                    Id = id,
                    UpdateStatusDto = updateStatusDto
                }, cancellationToken);
            }
            catch (Exception e)
            {
                return Conflict(e);
            }

            return NoContent();
        }

        /// <summary>
        /// Получение заявки проекта по Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetProjects(long id, CancellationToken cancellationToken)
        {
            var command = new GetProjectProposalById
                {Id = id};
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Получение списка проектных заявок
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<ProposalDto[]>> GetProjects(
            [FromQuery] QueryProposalDto dto, CancellationToken cancellationToken)
        {
            var command = new GetProposalsFilteredQuery {QueryProposalDto = dto};
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Удаление проектной заявки по Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteProposal(
            long id, CancellationToken cancellationToken)
        {
            var command = new DeleteProjectProposalCommand
            {
                Id = id
            };
            await Mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpDelete("DeleteByUser")]
        public async Task<IActionResult> DeleteProposalsByUser(CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteProjectProposalsByUserCommand(), cancellationToken);
            return Ok();

        }
    }
}