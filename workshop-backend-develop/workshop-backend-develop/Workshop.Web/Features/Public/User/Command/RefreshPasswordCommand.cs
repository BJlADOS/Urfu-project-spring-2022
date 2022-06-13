using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MediatR;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Helpers;
using Workshop.Web.Exceptions;
using Workshop.Web.Features.Public.HTML;

namespace Workshop.Web.Features.Public.User.Command
{
    public class RefreshPasswordCommand : IRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public long EventId { get; set; }
    }

    public class RefreshPasswordCommandHandler : AsyncRequestHandler<RefreshPasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IReadOnlyRepository<Core.Domain.Model.Event.Event> _eventRepository;
        private readonly IConfiguration Configuration;

        public RefreshPasswordCommandHandler(IUserRepository userRepository,
            IReadOnlyRepository<Core.Domain.Model.Event.Event> eventRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            Configuration = configuration;
        }

        protected override async Task Handle(RefreshPasswordCommand request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.FirstOrDefaultAsync(EventSpecification.GetById(request.EventId),
                cancellationToken);

            if (@event == null)
                throw new NotFoundException();

            var user = await _userRepository.FirstOrDefaultAsync(
                UserSpecification.GetByLoginWithEvent(request.Login, request.EventId), cancellationToken);

            if (user == null)
                throw new NotFoundException();

            var htmlCommands = new HtmlCommands(request.Login, request.Password, Configuration);

            if (!htmlCommands.IsAuth())
                throw new ArgumentException("неправильный логин или пароль");

            var salt = CryptographyHelper.GenerateRandomString(4);
            var hashedPassword =
                CryptographyHelper.GenerateMD5Hash(salt + CryptographyHelper.GenerateMD5Hash(request.Password));
            user.ResetPassword(hashedPassword, salt);
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}