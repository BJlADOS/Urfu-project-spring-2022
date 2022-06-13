using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.Role;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Model.UserAuditorium;
using Workshop.Core.Domain.Specification;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Exceptions;

namespace Workshop.Web.Features.Public.ExpertUser.Command
{
    public class RemoveExpertAuditoriumCommand : IRequest
    {
        public long ExpertUserId { get; set; }
        public long AuditoriumId { get; set; }
    }

    public class RemoveExpertAuditoriumCommandHandler : AsyncRequestHandler<RemoveExpertAuditoriumCommand>
    {
        public UserRepository _userRepository;

        public RemoveExpertAuditoriumCommandHandler(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected override async Task Handle(RemoveExpertAuditoriumCommand request,
                                             CancellationToken cancellationToken)
        {
            var user = await _userRepository.SingleAsync(
                UserSpecification.GetById(request.ExpertUserId),
                cancellationToken
                );

            if (user.UserType != UserType.Expert)
            {
                throw new ForbiddenException();
            }

            var auditorium = user.Auditoriums.FirstOrDefault(a => a.AuditoriumId == request.AuditoriumId);

            if (auditorium == null)
            {
                throw new ArgumentException();
            }

            user.Auditoriums.Remove(auditorium);
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}