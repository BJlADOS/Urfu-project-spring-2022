using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Web.Dtos.Admin.Project;
using Workshop.Web.Dtos.Expert.Project;
using Workshop.Web.Features.Admin.GenerateData;
using Workshop.Web.Features.Admin.Project.Command;
using Workshop.Web.Features.Admin.Projects.Command;
using Workshop.Web.Features.Public.Project.Command;

namespace Workshop.Web.Controllers
{
    [Route("api/v1/admin/[controller]")]
    [ApiController]
    public class AdminProjectController : BaseWebController
    {
        /// <summary>
        /// Добавить проект
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Id созданного проекта</returns>
        [HttpPost("CreateProject")]
        public async Task<ActionResult<long>> CreateProject([FromBody] CreateProjectDto value, CancellationToken token)
        {
            try
            {
                var newProjectId = await Mediator.Send(new CreateProjectCommand { CreateProjectDto = value }, token);
                return Ok(newProjectId);
            }
            catch (Exception e)
            {
                return Conflict(e);
            }
        }

        /// <summary>
        /// Обновление информации о проекте
        /// </summary>
        /// <param name="value"></param>
        [HttpPut]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectDto value, CancellationToken token)
        {
            try
            {
                await Mediator.Send(new ProjectUpdateCommand()
                {
                    Id = value.Id,
                    Name = value.Name,
                    Description = value.Description,
                    Curator = value.Curator,
                    Contacts = value.Contacts,
                    Organization = value.Organization,
                    Purpose = value.Purpose, 
                    Result = value.Result,
                    CompetenciesIds = value.CompetenciesIds?.Distinct().ToArray(),
                    IsPromoted = value.IsPromoted,
                    IsAvailable = value.IsAvailable,
                    LifeScenarioId = value.LifeScenarioId,
                    KeyTechnologyId = value.KeyTechnologyId,
                    MaxTeamCount = value.MaxTeamCount
                   

                }, token);
            }
            catch (Exception e)
            {
                return Conflict(e);
            }
            return NoContent();
        }

        /// <summary>
        /// Удаление проекта
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(long id, CancellationToken token)
        {
            await Mediator.Send(new ProjectDeleteCommand() { Id = id }, token);
            return NoContent();
        }

        /// <summary>
        /// Загрузка Excel файла с проектами
        /// </summary>
        /// <param name="generateProjectsDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile([FromForm] GenerateProjectsDto generateProjectsDto,
            CancellationToken token)
        {
            try
            {
                var file = generateProjectsDto.File;
                if (file.Length > 20000000)
                {
                    return StatusCode(413, new { message = "FileLimitSize" });
                }
                if (file.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    return StatusCode(413, new { message = "IncorrectFormat" });
                }

                var attachCommand = new GenerateDataCommand
                {
                    GenerateProjectsDto = generateProjectsDto
                };
                await Mediator.Send(attachCommand, token);
                return Ok();
            }
            catch (Exception e)
            {
                return Conflict(e);
            }
        }

        /// <summary>
        /// Скачать пример таблицы с проектами
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetProjectsTableTemplate")]
        public async Task<ActionResult> GetProjectsTableTemplate(CancellationToken cancellationToken)
        {
            var data = await Mediator.Send(new DownloadTemplateTableCommand(), cancellationToken);

            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Template.xlsx");
        }

        /// <summary>
        /// Открыть все проекты
        /// </summary>
        /// <param name="token"></param>
        [HttpPost("OpenAll")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> OpenAll(CancellationToken token)
        {
            await Mediator.Send(new OpenCloseAllProjectsCommand() { IsAvailable = true }, token);
            return NoContent();
        }

        [HttpPut("OpenForEntry")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> OpenForEntry(CancellationToken token)
        {
            await Mediator.Send(new OpenProjectsForEnrtryCommand() { IsOpen = true },token);
            return NoContent();
        }

        [HttpPut("CloseForEntry")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> CloseForEntry(CancellationToken token)
        {
            await Mediator.Send(new OpenProjectsForEnrtryCommand() { IsOpen = false }, token);
            return NoContent();
        }

        /// <summary>
        /// Закрыть все проекты
        /// </summary>
        /// <param name="token"></param>
        [HttpPost("CloseAll")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> CloseAll(CancellationToken token)
        {
            await Mediator.Send(new OpenCloseAllProjectsCommand() { IsAvailable = false }, token);
            return NoContent();
        }

        /// <summary>
        /// Добавить проектную роль
        /// </summary>
        /// <param name="projectRoleDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("AddProjectRole")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> AddProjectRole([FromBody] AddProjectRoleDto projectRoleDto, CancellationToken token)
        {
            await Mediator.Send(new AddProjectRoleCommand
            {
                ProjectId = projectRoleDto.ProjectId,
                RoleName = projectRoleDto.RoleName
            }, token);
            return NoContent();
        }

        /// <summary>
        /// Изменить проектную роль
        /// </summary>
        /// <param name="projectRoleDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut("UpdateProjectRole")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateProjectRole([FromBody] UpdateProjectRoleDto projectRoleDto, CancellationToken token)
        {
            await Mediator.Send(new EditProjectRoleCommand
            {
                RoleId = projectRoleDto.Id,
                RoleName = projectRoleDto.Name
            }, token);
            return NoContent();
        }

        /// <summary>
        /// Удалить жизненный сценарий из системы
        /// </summary>
        /// <param name="id">Id жизненного сценария</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete("DeleteLifeScenario")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteLifeScenario(long id, CancellationToken token)
        {
            await Mediator.Send(new DeleteLifeScenarioCommand { LifeScenarioId = id }, token);
            return NoContent();
        }

        /// <summary>
        /// Удалить ключевую технологию из системы
        /// </summary>
        /// <param name="id">Id ключевой технологии</param>
        /// <param name="token"></param>
        [HttpDelete("DeleteKeyTechnology")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteKeyTechnology(long id, CancellationToken token)
        {
            await Mediator.Send(new DeleteKeyTechnologyCommand { KeyTechnologyId = id }, token);
            return NoContent();
        }

        /// <summary>
        /// Удалить проектную роль
        /// </summary>
        /// <param name="id">Id роли</param>
        /// <param name="token"></param>
        [HttpDelete("DeleteProjectRole/{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteProjectRole(long id, CancellationToken token)
        {
            await Mediator.Send(new DeleteProjectRoleCommand { RoleId = id }, token);
            return NoContent();
        }

        /// <summary>
        /// Скачать результаты защит
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetResults")]
        public async Task<ActionResult> GetResults(CancellationToken cancellationToken)
        {
            var data = await Mediator.Send(new DownloadResultsCommand(), cancellationToken);
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Results.xlsx");
        }

        /// <summary>
        /// Скачать расписание защит
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetSchedule")]
        public async Task<ActionResult> GetSchedule(CancellationToken cancellationToken)
        {
            var data = await Mediator.Send(new DownloadScheduleCommand(), cancellationToken);
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Schedule.xlsx");
        }
    }
}