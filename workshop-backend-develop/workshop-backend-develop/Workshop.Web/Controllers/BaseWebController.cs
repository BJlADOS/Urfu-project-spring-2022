using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Workshop.Core.Services.Security;

namespace Workshop.Web.Controllers
{
    public abstract class BaseWebController : ControllerBase
    {
        protected IMediator Mediator => HttpContext.RequestServices.GetService<IMediator>();
        protected IUserProfileProvider UserProfileProvider => Request.HttpContext.RequestServices.GetService<IUserProfileProvider>();
    }
}
