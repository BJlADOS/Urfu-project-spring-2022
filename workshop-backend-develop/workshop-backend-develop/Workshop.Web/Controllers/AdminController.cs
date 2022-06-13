using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Cells;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Workshop.Core.Helpers;
using Workshop.Web.Dtos.Admin.ApiKey;
using Workshop.Web.Dtos.Expert.Event;
using Workshop.Web.Dtos.Expert.Project;
using Workshop.Web.Dtos.Admin.Statistic;
using Workshop.Web.Dtos.Admin.TeamSlot;
using Workshop.Web.Dtos.Public.User;
using Workshop.Web.Features.Admin.ApiKey.Command;
using Workshop.Web.Features.Admin.ApiKey.Query;
using Workshop.Web.Features.Admin.Command;
using Workshop.Web.Features.Admin.Event.Query;
using Workshop.Web.Features.Admin.GenerateData;
using Workshop.Web.Features.Admin.Statistic.Query;
using Workshop.Web.Features.Public.Event;
using Workshop.Web.Features.Public.User.Generate;
using Workshop.Web.Features.Admin.Team.Command;
using Workshop.Web.Features.Admin.TeamSlot.Command;
using Workshop.Web.Features.Admin.User.Query;
using Workshop.Web.Features.Admin.User.Command;

namespace Workshop.Web.Controllers
{
    [Route("api/v1/admin")]
    [ApiController]
    public class AdminController : BaseWebController
    {
        /// <summary>
        /// Получение списка всех событий
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("Events")]
        public async Task<ActionResult<ICollection<EventFullDto>>> GetEvents(CancellationToken token)
        {
            var events = await Mediator.Send(new EventsQuery(), token);
            return Ok(events);
        }

        /// <summary>
        /// Создание мероприятия, TestDuration - длительность тестирования в минутах
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("AddEvent")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> AddEvent([FromBody] EventCreateDto request, CancellationToken token)
        {
            await Mediator.Send(
                new EventCreateCommand()
                {
                    Name = request.Name,
                }, token);
            return NoContent();
        }

        /// <summary>
        /// Создать мероприятие
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut("UpdateEvent")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateEvent([FromBody] EventFullDto request, CancellationToken token)
        {
            await Mediator.Send(
                new EventUpdateCommand()
                {
                    Id = request.Id,
                    Name = request.Name,
                    IsActive = request.IsActive

                }, token);
            return NoContent();
        }

        /// <summary>
        /// Генерация аккаунтов пользователей
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns>Excel файл со списком логинов и паролей</returns>
        [HttpPost("GenerateUsers")]
        public async Task<IActionResult> GenerateUsers([FromBody] UserGenerationParametersDto request,
            CancellationToken token)
        {
            var data = await Mediator.Send(
                new UsersGenerateCommand()
                { Parameters = request }, token);

            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"{request.UserType}_{DateTimeHelper.GetCurrentTime():g}.xlsx");
        }


        /// <summary>
        /// Получение списка всех пользователей
        /// </summary>
        /// <param name="term"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("GetUsers")]
        public async Task<ICollection<UsersListItemDto>> GetUsers([FromQuery] string term, CancellationToken token)
        {
            return await Mediator.Send(new UsersGetQuery() { Term = term }, token);
        }

        /// <summary>
        /// Получить подробную информацию о пользователе.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("GetUser/{id}")]
        public async Task<UserDetailedDto> GetUser(long id, CancellationToken token)
        {
            return await Mediator.Send(new UserGetQuery() { Id = id }, token);
        }

        /// <summary>
        /// Изменить роль пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut("ChangeUserType")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> ChangeUserType([FromBody] UserTypeChangeDto request, CancellationToken token)
        {
            await Mediator.Send(new ChangeUserTypeCommand
            {
                Id = request.Id,
                Type = request.Type
            }, token);

            return NoContent();
        }

        /// <summary>
        /// Добавить новый Api-ключ
        /// </summary>
        /// <param name="name">Название ключа</param>
        /// <param name="userType">Тип пользователя</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("ApiKey")]
        public async Task<ActionResult<ApiKeyDto>> CreateApiKey([FromBody] CreateKeyDto dto, CancellationToken token)
        {
            var apiKey = await Mediator.Send(new CreateApiKeyCommand
            {
                Name = dto.Name,
                UserType = dto.UserType,
            }, token);

            return Ok(apiKey);
        }

        /// <summary>
        /// Получить все Api-ключи
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("ApiKey")]
        public async Task<ActionResult<ICollection<ApiKeyDto>>> GetApiKeys(CancellationToken token)
        {
            var apiKeys =
                await Mediator.Send(new ApiKeysGetAllQuery(),
                    token);
            return Ok(apiKeys);
        }

        /// <summary>
        /// Удаление Api-ключа
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete("ApiKey/{id}")]
        public async Task<IActionResult> DeleteApiKey(long id, CancellationToken token)
        {
            await Mediator.Send(new DeleteApiKeyCommand() { Id = id }, token);
            return NoContent();
        }

        /// <summary>
        /// Добавить слот на защиту в аудиторию с заданным Id
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("AdminTeamSlot")]
        public async Task<IActionResult> PostTeamSlot([FromBody] AddTeamSlotDto dto,
                                                      CancellationToken cancellationToken)
        {
            await Mediator.Send(new AddTeamSlotCommand()
            {
                Dto = dto
            }, cancellationToken);
            return NoContent();
        }
        
        /// <summary>
        /// Добавить слоты на защиту в аудиторию с заданным Id и заданной продолжительностью
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("AdminTeamSlots")]
        public async Task<IActionResult> PostTeamSlots([FromBody] AddTeamSlotsDto dto,
            CancellationToken cancellationToken)
        {
            if (dto.StartTime.Date < DateTime.Now.Date || dto.EndTime.Date < DateTime.Now.Date)
                return StatusCode(400, new { message = "Нельзя создавать слоты ранее сегодняшней даты" });
            if (dto.StartTime >= dto.EndTime)
                return StatusCode(400, new { message = "Окончание события должно быть после его начала" });
            if(dto.StartTime.Date != dto.EndTime.Date)
                return StatusCode(400, new { message = "Создавать слоты можно только для одного дня" });
            if(dto.StartTime.Add(TimeSpan.FromMinutes(dto.Duration)) > dto.EndTime)
                return StatusCode(400, new { message = "Продолжительность защиты не укладывается в заданный промежуток времени" });
            if(dto.Duration <= 0)
                return StatusCode(400, new { message = "Продолжительность должна быть больше 0" });
            await Mediator.Send(new AddTeamSlotsCommand()
            {
                Dto = dto
            }, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Удалить слот на защиту с заданным Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("AdminTeamSlot/{id}")]
        public async Task<IActionResult> DeleteTeamSlot(long id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteTeamSlotCommand { Id = id }, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Обновить слот на защиту по Id
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("AdminTeamSlot")]
        public async Task<IActionResult> UpdateTeamSlot([FromBody] UpdateTeamSlotDto dto, CancellationToken cancellationToken)
        {
            await Mediator.Send(new UpdateTeamSlotCommand
            {
                Dto = dto
            }, cancellationToken);
            return NoContent();
        }
    }
}