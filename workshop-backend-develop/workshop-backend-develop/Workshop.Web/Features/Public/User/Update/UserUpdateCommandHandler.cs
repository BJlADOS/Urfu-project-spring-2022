using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.Competency;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Model.UserCompetency;
using Workshop.Core.Domain.Specification;

namespace Workshop.Web.Features.Public.User.Update
{
    public class UserUpdateCommandHandler : AsyncRequestHandler<UserUpdateCommand>
    {
        private readonly IUserRepository _userRepository;

        public UserUpdateCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected override async Task Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.SingleAsync(UserSpecification.GetById(request.User.Id), cancellationToken);
            user.UpdateFirstName(request.User.FirstName);
            user.UpdateLastName(request.User.LastName);
            user.UpdateMiddleName(request.User.MiddleName);
            user.UpdateAbout(request.User.About);
            user.UpdateEmail(request.User.Email);
            user.UpdatePhoneNumber(request.User.PhoneNumber);
            user.UpdateAcademicGroup(request.User.AcademicGroup);
            user.UpdateIsShowContactsToExpert(request.User.IsShowContactsToExpert);

            var userCompetencies = request.User.Competencies?
                .Select(x => new UserCompetency(user.Key, x.Id, x.UserCompetencyType));

            if (userCompetencies != null)
                user.UpdateCompetencies(userCompetencies.ToHashSet());

            if (string.IsNullOrWhiteSpace(user.FirstName)
                || string.IsNullOrWhiteSpace(user.LastName)
                || string.IsNullOrWhiteSpace(user.PhoneNumber)
                || string.IsNullOrWhiteSpace(user.AcademicGroup))
            {
                user.UpdateProfileFilled(false);
            }
            else
            {
                user.UpdateProfileFilled(true);
            }

            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
