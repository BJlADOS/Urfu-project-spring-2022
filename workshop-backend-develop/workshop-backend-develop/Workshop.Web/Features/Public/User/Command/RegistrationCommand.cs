using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Helpers;
using Workshop.Web.Exceptions;
using Workshop.Web.Features.Public.HTML;

namespace Workshop.Web.Features.Public.User.Command
{
    public class RegistrationCommand : IRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public long EventId { get; set; }
    }

    public class RegistrationCommandHandler : AsyncRequestHandler<RegistrationCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IReadOnlyRepository<Core.Domain.Model.Event.Event> _eventRepository;

        public RegistrationCommandHandler(IUserRepository userRepository,
            IReadOnlyRepository<Core.Domain.Model.Event.Event> eventRepository)
        {
            _userRepository = userRepository;
            _eventRepository = eventRepository;
        }

        protected override async Task Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.FirstOrDefaultAsync(EventSpecification.GetById(request.EventId),
                cancellationToken);

            if (@event == null)
                throw new NotFoundException();

            var user = await _userRepository.FirstOrDefaultAsync(
                UserSpecification.GetByLoginWithEvent(request.Login, request.EventId), cancellationToken);

            if (user != null)
                throw new ArgumentException("пользователь с таким логином уже зарегистрирован");

            var salt = CryptographyHelper.GenerateRandomString(4);
            var hashedPassword =
                CryptographyHelper.GenerateMD5Hash(salt + CryptographyHelper.GenerateMD5Hash(request.Password));

            user = new Core.Domain.Model.User.User(request.Login, hashedPassword, salt,
                UserType.Student, request.EventId);

            await _userRepository.AddAsync(user, cancellationToken);
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}