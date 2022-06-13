using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Helpers;
using Workshop.Web.Exceptions;
using Workshop.Web.Features.Public.HTML;

namespace Workshop.Web.Features.Public.User.Command
{
    public class RegistrationWithUrfuCommand : IRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public long EventId { get; set; }
    }

    public class RegistrationWithUrfuCommandHandler : AsyncRequestHandler<RegistrationWithUrfuCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IReadOnlyRepository<Core.Domain.Model.Event.Event> _eventRepository;
        private readonly IConfiguration Configuration;

        public RegistrationWithUrfuCommandHandler(IUserRepository userRepository,
            IReadOnlyRepository<Core.Domain.Model.Event.Event> eventRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            Configuration = configuration;
        }


        protected override async Task Handle(RegistrationWithUrfuCommand request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.FirstOrDefaultAsync(EventSpecification.GetById(request.EventId),
                cancellationToken);

            if (@event == null)
                throw new NotFoundException();

            var user = await _userRepository.FirstOrDefaultAsync(
                UserSpecification.GetByLoginWithEvent(request.Login, request.EventId), cancellationToken);

            var htmlCommands = new HtmlCommands(request.Login, request.Password, Configuration);

            var urfuUserData = await htmlCommands.GetUrfuUserData();

            var salt = CryptographyHelper.GenerateRandomString(4);
            var hashedPassword =
                CryptographyHelper.GenerateMD5Hash(salt + CryptographyHelper.GenerateMD5Hash(request.Password));

            var newUser = new Core.Domain.Model.User.User(request.Login, hashedPassword, salt,
                UserType.Student, request.EventId, urfuUserData.Email, urfuUserData.LastName, urfuUserData.FirstName,
                urfuUserData.MiddleName, urfuUserData.AcademicGroup);

            if (user == null)
            {
                await _userRepository.AddAsync(newUser, cancellationToken);
            }
            else
            {
                user.ResetPassword(hashedPassword, salt);
                user.UpdateEmail(urfuUserData.Email);
                user.UpdateLastName(urfuUserData.LastName);
                user.UpdateFirstName(urfuUserData.FirstName);
                user.UpdateMiddleName(urfuUserData.MiddleName);
                user.UpdateAcademicGroup(urfuUserData.AcademicGroup);
            }

            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}