using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Web.Dtos.Public.Competency;
using Workshop.Web.Features.Public.Competency.Query;

namespace Workshop.Web.Controllers
{
    [Route("api/v1/admin/[controller]")]
    [ApiController]
    public class AdminCompetenciesController : BaseWebController
    {
        /// <summary>
        /// Добавить новую компетенцию в систему
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(204)]
        public async Task<IActionResult> CreateCompetency([FromBody] NewCompetencyDto request, CancellationToken token)
        {
            await Mediator.Send(new CreateCompetencyQuery() { Name = request.Name, CompetencyType = request.CompetencyType }, token);
            return NoContent();
        }

        /// <summary>
        /// Обновить данные компетенции
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateCompetency([FromBody] CompetencyDto request, CancellationToken token)
        {
            await Mediator.Send(new UpdateCompetencyQuery() { Id = request.Id, Name = request.Name, CompetencyType = request.CompetencyType }, token);
            return NoContent();
        }

        /// <summary>
        /// Удалить компетенцию из системы
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(long id, CancellationToken token)
        {
            await Mediator.Send(new DeleteCompetencyQuery() { Id = id }, token);
            return NoContent();
        }
    }
}
