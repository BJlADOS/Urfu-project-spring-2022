using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Workshop.Web.Dtos.Public.Competency;
using Workshop.Web.Features.Public.Competency.Query;

namespace Workshop.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CompetencyController : BaseWebController
    {
        /// <summary>
        /// Получение списка компетенций
        /// </summary>
        /// <param name="term"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ICollection<CompetencyDto>> GetCompetencies([FromQuery]string term, CancellationToken token)
        {
            return await Mediator.Send(new CompetenciesGetQuery() { Term = term }, token);
        }
    }
}
