using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Workshop.Web.Dtos.Expert.User;
using Workshop.Web.Dtos.Public.Auditorium;
using Workshop.Web.Features.Public.Auditorium.Command;
using Workshop.Web.Features.Public.ExpertUser.Command;

namespace Workshop.Web.Controllers
{
    [Route("api/v1/expert/[controller]")]
    [ApiController]
    public class ExpertAuditoriumController : BaseWebController
    {
        /// <summary>
        /// Добавление аудитории
        /// </summary>
        /// <param name="value">Название аудитории и её вместимость (в количестве команд)</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(204)]
        public async Task<IActionResult> AddAuditorium([FromBody] AuditoriumAddDto value, CancellationToken token)
        {
            await Mediator.Send(new AuditoriumAddCommand() { Name = value.Name, Capacity = value.Capacity, IsDefault = value.IsDefault }, token);
            return NoContent();
        }

        /// <summary>
        /// Обновление информации о аудитории
        /// </summary>
        /// <param name="value"></param>
        [HttpPut]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateAuditorium([FromBody] AuditoriumUpdateDto value, CancellationToken token)
        {
            await Mediator.Send(new AuditoriumUpdateCommand() { Id = value.Id, Name = value.Name, Capacity = value.Capacity, IsDefault = value.IsDefault }, token);
            return NoContent();
        }

        /// <summary>
        /// Удалить эксперта из аудитории
        /// </summary>
        /// <param name="dto"></param>
        [HttpPost("RemoveExpert")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> RemoveExpertAuditorium([FromBody] ExpertUserAuditoriumDto dto, CancellationToken token)
        {
            await Mediator.Send(new RemoveExpertAuditoriumCommand
            {
                ExpertUserId = dto.ExpertId,
                AuditoriumId = dto.AuditoriumId
            },
            token);
            return NoContent();
        }

        /// <summary>
        /// Удаление аудитории
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteAuditorium(long id, CancellationToken token)
        {
            await Mediator.Send(new AuditoriumDeleteCommand() { Id = id }, token);
            return NoContent();
        }
    }
}
