using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.User;
using Workshop.Web.Dtos.Public.User;
using Workshop.Web.Features.Admin.User.Command;
using Workshop.Web.Features.Public.User.GetProfile;
using Workshop.Web.Features.Public.User.Query;
using Workshop.Web.Features.Public.User.Update;

namespace Workshop.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : BaseWebController
    {
        /// <summary>
        /// Получить информацию о профиле текущего пользователя
        /// </summary>
        /// <param name="token"></param>
        /// <returns>UserDto</returns>
        [HttpGet]
        public async Task<ActionResult<ProfileUserDto>> GetUser(CancellationToken token)
        {
            return await Mediator.Send(new UserGetProfileQuery() {Id = UserProfileProvider.GetProfile().User.Key},
                token);
        }

        /// <summary>
        /// Получить информацию о профиле пользователя по Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns>UserDto</returns>
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<UserDto>> GetUserById([FromRoute] long id, CancellationToken token)
        {
            return await Mediator.Send(new UserGetQuery() {Id = id}, token);
        }

        /// <summary>
        /// Обновление профиля пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <response code="204"></response>
        /// <response code="403"></response>   
        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto user, CancellationToken token)
        {
            if (user.Id != UserProfileProvider.GetProfile().User.Key)
                return StatusCode(403);

            await Mediator.Send(new UserUpdateCommand() {User = user}, token);

            return NoContent();
        }

        /// <summary>
        /// Стать тимлидом
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut("UpdateUserType")]
        public async Task<IActionResult> UpdateUserType (UserType userType, CancellationToken token)
        {
            await Mediator.Send(new ChangeUserTypeCommand
            {
                Id = new UserGetProfileQuery { Id = UserProfileProvider.GetProfile().User.Key }.Id,
                Type = userType
            }, token) ;

            return NoContent();
        }
    }
}