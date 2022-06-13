using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Workshop.Web.Dtos.Public.Filters;
using Workshop.Web.Dtos.Public.KeyTechnology;
using Workshop.Web.Dtos.Public.LifeScenario;
using Workshop.Web.Dtos.Public.Project;
using Workshop.Web.Exceptions;
using Workshop.Web.Features.Public.Filters;
using Workshop.Web.Features.Public.KeyTechnologies;
using Workshop.Web.Features.Public.LifeScenarios.Query;
using Workshop.Web.Features.Public.Project.Query;

namespace Workshop.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProjectController : BaseWebController
    {
        /// <summary>
        /// Получение списка жизненных сценариев
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetLifeScenarios")]
        [Produces("application/json")]
        public async Task<ActionResult<ICollection<LifeScenarioDto>>> GetLifeScenarios(CancellationToken cancellationToken)
        {
            var allLifeScenarios = new GetAllLifeScenariosQuery();
            var scenarios = await Mediator.Send(allLifeScenarios, cancellationToken);

            return Ok(scenarios);
        }

        /// <summary>
        /// Получение списка ключевых технологий
        /// Если передан id, то будет получен список для жизненного сценария с выбранным id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetKeyTechnologies", Name = "GetKeyTechnologies")]
        [Produces("application/json")]
        public async Task<ActionResult<ICollection<KeyTechnologyDto>>> GetKeyTechnologies(long? id, CancellationToken cancellationToken)
        {
            var keyTechnologies = new GetKeyTechnologiesQuery { LifeScenarioId = id };
            var keyTechnologiesDto = await Mediator.Send(keyTechnologies, cancellationToken);
            return Ok(keyTechnologiesDto);
        }

        /// <summary>
        /// Получение списка заказчиков
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCustomers")]
        [Produces("application/json")]
        public async Task<ActionResult<ICollection<CustomerDto>>> GetCustomers(CancellationToken cancellationToken)
        {
            var allCustomers = new GetAllCustomersQuery();
            var customers = await Mediator.Send(allCustomers, cancellationToken);

            return Ok(customers);
        }

        /// <summary>
        /// Получение списка кураторов
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCurators")]
        [Produces("application/json")]
        public async Task<ActionResult<ICollection<CuratorDto>>> GetCurators(CancellationToken cancellationToken)
        {
            var allCurators = new GetAllCuratorsQuery();
            var curators = await Mediator.Send(allCurators, cancellationToken);

            return Ok(curators);
        }

        /// <summary>
        /// Получение списка проектов 
        /// </summary>
        /// <param name="getProjectsDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<ShortProjectDto[]>> Get([FromQuery] GetProjectsDto getProjectsDto, CancellationToken cancellationToken)
        {
            var project = new GetProjectsFilteredQuery { GetProjectsDto = getProjectsDto };
            var projectDto = await Mediator.Send(project, cancellationToken);
            return Ok(projectDto);
        }

        /// <summary>
        /// Получение списка закрепленных проектов 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetPromoted")]
        [Produces("application/json")]
        public async Task<ActionResult<ShortProjectDto[]>> GetPromoted(CancellationToken cancellationToken)
        {
            var projectDto = await Mediator.Send(new GetPromotedProjectsQuery(), cancellationToken);
            return Ok(projectDto);
        }

        /// <summary>
        /// Получение информации о проекте по Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetProject")]
        [Produces("application/json")]
        public async Task<ActionResult<ProjectDto>> Get(long id, CancellationToken cancellationToken)
        {
            var project = new GetProjectByIdQuery { Id = id };
            var projectDto = await Mediator.Send(project, cancellationToken);

            return Ok(projectDto);
        }
    }
}