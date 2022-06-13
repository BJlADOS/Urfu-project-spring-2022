using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Helpers;
using Workshop.Core.Services.Security;
using Workshop.Web.Exceptions;

namespace Workshop.Web.Features.Public.User.Command
{
    public class UpdatePasswordCommand : IRequest
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class UpdatePasswordCommandHandler : AsyncRequestHandler<UpdatePasswordCommand>
    {
        private IUserRepository _userRepository;
        private IUserProfileProvider _profileProvider;

        public UpdatePasswordCommandHandler(IUserRepository userRepository, IUserProfileProvider profileProvider)
        {
            _userRepository = userRepository;
            _profileProvider = profileProvider;
        }

        protected override async Task Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = _profileProvider.GetProfile().User.Key;
            var user = await _userRepository.SingleOrDefaultAsync(UserSpecification.GetById(userId), cancellationToken);
            var hashedOldPwd =
                CryptographyHelper.GenerateMD5Hash(user.Salt + CryptographyHelper.GenerateMD5Hash(request.OldPassword));

            if (hashedOldPwd != user.Password)
            {
                throw new ForbiddenException();
            }

            if (request.NewPassword == request.OldPassword)
            {
                throw new ConflictException("Same password");
            }

            var salt = CryptographyHelper.GenerateRandomString(4);
            var hashedPassword =
                CryptographyHelper.GenerateMD5Hash(salt + CryptographyHelper.GenerateMD5Hash(request.NewPassword));
            user.ResetPassword(hashedPassword, salt);
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}