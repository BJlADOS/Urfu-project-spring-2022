using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Workshop.Web.Dtos.Public.RequestProposal;
using Workshop.Web.Dtos.Public.User;
using Workshop.Web.Features.Public.RequestProposal;
using Workshop.Web.Features.Public.RequestProposal.Command;
using Workshop.Web.Features.Public.RequestProposal.Query;

namespace Workshop.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RequestProposalController : BaseWebController
    {
        /// <summary>
        /// Создать заявку
        /// </summary>
        /// <param name="addProjectProposalDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("CreateRequest")]
        public async Task<IActionResult> AddRequestProposal(
            [FromBody] AddRequestProposalDto addProjectProposalDto,
            CancellationToken cancellationToken)
        {
            try
            {
                await Mediator.Send(new AddRequestProposalCommand { AddRequestProposalDto = addProjectProposalDto },
                    cancellationToken);
            }
            catch (Exception e)
            {
                return Conflict(e);
            }

            return StatusCode(200);
        }

        /// <summary>
        /// Обновить статус заявки по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateStatusDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(long id,
            [FromQuery] UpdateStatusRequestProposalDto updateStatusDto,
            CancellationToken cancellationToken)
        {
            try
            {
                await Mediator.Send(new UpdateRequestProposalStatucCommand
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
        /// Получить заявку по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetRequestProposalById(long id, CancellationToken cancellationToken)
        {
            var res = await Mediator.Send(new GetRequestProposalsByUser { Id = id }, cancellationToken);

            return Ok(res);
        }

        [HttpGet("GetRequestsByUser")]
        [Produces("application/json")]
        public async Task<ActionResult<ICollection<UserRequestProposalDto>>> GetRequestProposalByUser(CancellationToken cancellationToken)
        {
            var res = await Mediator.Send(new GetRequestProposalsByUser { Id = UserProfileProvider.GetProfile().User.Key }, cancellationToken);

            return Ok(res);
        }


        [HttpGet("GetUsersRequests")]
        [Produces("application/json")]
        public async Task<ActionResult<ICollection<ShortUserRequestDto>>> GetUsersRequestFromRequest(CancellationToken cancellationToken)
        {
            var res = await Mediator.Send(new GetUsersFromRequestCommand { Author = UserProfileProvider.GetProfile().User }, cancellationToken);

            return Ok(res);
        }


        /// <summary>
        /// Получить все заявки по id тимлида
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("teamlead/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllRequestProposalByTeamleadId(long id, CancellationToken cancellationToken)
        {
            var res = await Mediator.Send(new GetAllRequestProposalByTeamleadId { TeamleadId = id }, cancellationToken);

            return Ok(res);
        }

        /// <summary>
        /// Удалить заявку по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteRequestProposalById(long id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteRequestProposalCommand { UserId = id }, cancellationToken);

            return Ok();
        }

        [HttpPost("CreateRequestInTeam")]
        public async Task<IActionResult> CreateRequestForTeamlead(long teamId, long projectId, string roleName,CancellationToken cancellation)
        {
            try
            {
                await Mediator.Send(new AddRequestInTeamCommand() { UserId = UserProfileProvider.GetProfile().User.Key, TeamId = teamId, ProjectId = projectId, RoleName = roleName },cancellation);
            }
            catch(Exception e)
            {
                return StatusCode(403);
            }
            return NoContent();
        }
    }
}