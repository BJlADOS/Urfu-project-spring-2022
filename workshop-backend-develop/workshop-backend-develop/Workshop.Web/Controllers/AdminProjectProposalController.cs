using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Workshop.Web.Dtos.Public.ProjectProposal;
using Workshop.Web.Features.Public.ProjectProposal.Command;
using Workshop.Web.Features.Public.ProjectProposal.Query;

namespace Workshop.Web.Controllers
{
    [Route("api/v1/admin/[controller]")]
    [ApiController]
    public class AdminProjectProposalController : BaseWebController
    {
        [HttpPost]
        public async Task<IActionResult> AcceptProposal([FromQuery] long id, [FromBody] CreateProjectFromProposalDto dto, CancellationToken cancellation)
        {
            try
            {
                await Mediator.Send(new CreateProjectFromProposalByIdCommand()
                {
                    RoleNames = dto.RoleNames,
                    ProposalId = id,
                    CreateProjectDto = dto

                }, cancellation) ;
            }
            catch(Exception e)
            {
                return Conflict(e);
            }
            return StatusCode(200);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        public async Task<IActionResult> RejectProposal([FromQuery]long id, CancellationToken cancellation)
        {
            try
            {
                await Mediator.Send(new RejectProjectProposalCommand() { ProposalId = id }, cancellation);
            }
            catch (Exception e)
            {
                return Conflict(e);
            }
            return NoContent();

        }


        [HttpGet("GetPendingProposals")]
        [Produces("application/json")]
        public async Task<ICollection<GetPendingProposals>> GetProposals([FromQuery] string term, CancellationToken token)
        {
            return await Mediator.Send(new ProposalsGetQuery() { Term = term }, token);
        }
    }
}
