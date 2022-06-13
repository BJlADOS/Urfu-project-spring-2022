using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Workshop.Core.Helpers;
using Workshop.Web.Dtos.Admin.Statistic;
using Workshop.Web.Features.Admin.Statistic.Query;

namespace Workshop.Web.Controllers
{
    [Route("api/v1/admin/[controller]")]
    [ApiController]
    public class AdminStatisticController : BaseWebController
    {
        /// <summary>
        ///     Получение статистики по жизненым сценариям и ключевым технологиям
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("GetStatistic")]
        public async Task<ICollection<StatisticDto>> GetStatistic(CancellationToken token)
        {
            return await Mediator.Send(new StatisticGetQuery(), token);
        }


        /// <summary>
        ///     Получение статистики по студенчиским проектам. Получение их назввнаий и описаний
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("GetStudentsStatistic")]
        public async Task<ActionResult> GetUserStatistic(CancellationToken token)
        {
            var data =  await Mediator.Send(new StudentStatisticGetQuery(), token);
            
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "StudentsProjects.xlsx");
        }

        /// <summary>
        ///     Получение результата игры
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("GetEventResult")]
        public async Task<IActionResult> GetResult(CancellationToken token)
        {
            var data = await Mediator.Send(new GetEventResultQuery(), token);

            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"EventResults_{DateTimeHelper.GetCurrentTime():g}.xlsx");
        }

        /// <summary>
        ///     Выгрузить данные о студентах и проектах в виде таблицы Excel
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetEventData")]
        public async Task<ActionResult> GetEventData(CancellationToken cancellationToken)
        {
            var data = await Mediator.Send(new GetEventDataQuery(), cancellationToken);
            var today = DateTime.UtcNow.AddHours(5).ToString("dd.MM.yy");

            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Event Data ({today}).xlsx");
        }

        /// <summary>
        ///     Получение списка текущих компетенций студентов.
        /// </summary>
        /// <param name="eventId">Id события</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("StudentsCountOnCompetencies")]
        public async Task<ActionResult<CompetenciesCountDto[]>> GetStudentsCountOnCompetencies(long eventId,
            CancellationToken token)
        {
            var studentsCountOnCompetencies =
                await Mediator.Send(new GetStudentsCountOnCompetencyQuery {EventId = eventId}, token);

            return Ok(studentsCountOnCompetencies);
        }

        /// <summary>
        ///     Получение списка компетенций из проектов.
        /// </summary>
        /// <param name="eventId">Id события</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("ProjectsCountOnCompetencies")]
        public async Task<ActionResult<CompetenciesCountDto[]>> GetProjectsCountOnCompetencies(long eventId,
            CancellationToken token)
        {
            var projectsCountOnCompetencies =
                await Mediator.Send(new GetProjectsCountOnCompetencyQuery {EventId = eventId}, token);

            return Ok(projectsCountOnCompetencies);
        }

        /// <summary>
        ///     Получение списка компетенций по количеству команд на определенном мероприятии
        /// </summary>
        /// <param name="eventId">Id события</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("TeamsCountOnCompetencies")]
        public async Task<ActionResult<TeamsCountOnCompetencyDto[]>> GetTeamsCountOnCompetencies(long eventId,
            CancellationToken token)
        {
            var projectsCountOnCompetencies =
                await Mediator.Send(new GetTeamsCountOnCompetencyQuery {EventId = eventId}, token);

            return Ok(projectsCountOnCompetencies);
        }

        /// <summary>
        ///     Получение топа заказчиков
        /// </summary>
        /// <param name="eventId">Id события</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetCustomersTop")]
        public async Task<ActionResult<CustomersTopDto[]>> GetCustomerTop(long eventId, CancellationToken token)
        {
            var customersTop = await Mediator.Send(new GetCustomersTopQuery {EventId = eventId}, token);

            return Ok(customersTop);
        }

        /// <summary>
        ///     Получить список участников типичной команды
        /// </summary>
        /// ///
        /// <param name="eventId">Id события</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("GetProjectRolesTop")]
        public async Task<ActionResult<string[]>> GetProjectRolesTop(long eventId, CancellationToken token)
        {
            var projectRoles = await Mediator.Send(new GetProjectRolesTopQuery {EventId = eventId}, token);

            return Ok(projectRoles);
        }

        /// <summary>
        ///     Получает суммарную статистику: количество студентов, проектов, команд, кураторов и заказчиков для определленного
        ///     мероприятия
        /// </summary>
        /// <param name="eventId">Id события</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("SummaryStatistics")]
        public async Task<ActionResult<SummaryStatisticsDto>> GetSummaryStatistics(long eventId,
            CancellationToken token)
        {
            var summaryStatistics = await Mediator.Send(new GetSummaryStatisticsQuery {EventId = eventId}, token);

            return Ok(summaryStatistics);
        }
    }
}