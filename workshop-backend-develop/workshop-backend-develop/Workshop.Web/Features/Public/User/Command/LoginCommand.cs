using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.Session;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Helpers;
using Workshop.Core.Services.Security;

namespace Workshop.Web.Features.Public.User.Command
{
    public class LoginCommand : IRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public long EventId { get; set; }
    }

    public class LoginCommandHandler : AsyncRequestHandler<LoginCommand>
    {
        private readonly IReadOnlyRepository<Core.Domain.Model.User.User> _userRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserProfileProvider _profileProvider;

        public LoginCommandHandler(IReadOnlyRepository<Core.Domain.Model.User.User> userRepository,
            ISessionRepository sessionRepository,
            IUserProfileProvider profileProvider)
        {
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
            _profileProvider = profileProvider;
        }

        protected override async Task Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository
                .SingleOrDefaultAsync(UserSpecification.GetByLoginWithEvent(request.Login, request.EventId), cancellationToken);

            if (user == null)
            {
                return;
            }

            //todo: переделать на объектную модель.
            var hashedPwd =
                CryptographyHelper.GenerateMD5Hash(user.Salt + CryptographyHelper.GenerateMD5Hash(request.Password));

            if (user.Password != hashedPwd)
                return;

            var session = new Session(user.Key);
            session = await _sessionRepository.AddAsync(session, cancellationToken);
            await _sessionRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            var userProfile = new UserProfile()
            {
                User = user,
                Session = session
            };

            _profileProvider.SetProfile(userProfile);
        }
    }
}
