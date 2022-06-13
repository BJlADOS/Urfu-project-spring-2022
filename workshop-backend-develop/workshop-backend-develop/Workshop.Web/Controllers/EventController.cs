using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workshop.Web.Dtos.Public.Event;
using Workshop.Web.Features.Public.Event.Query;

namespace Workshop.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EventController : BaseWebController
    {
        /// <summary>
        /// Получение списка проводимых событий
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ICollection<EventDto>>> GetEvents(CancellationToken token)
        {
            var events = await Mediator.Send(new EventsQuery(), token);
            return Ok(events);
        }
    }
}