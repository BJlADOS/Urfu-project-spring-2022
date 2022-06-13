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
    public class UpdateAuditoriumCommand : IRequest
    {
        public long ExpertUserId { get; set; }
        public long AuditoriumId { get; set; }
    }

    public class UpdateAuditoriumCommandHandler : AsyncRequestHandler<UpdateAuditoriumCommand>
    {
        public UserRepository _userRepository;
        private readonly AuditoriumRepository _auditoriumRepository;

        public UpdateAuditoriumCommandHandler(UserRepository userRepository,
                                              AuditoriumRepository auditoriumRepository)
        {
            _userRepository = userRepository;
            _auditoriumRepository = auditoriumRepository;
        }

        protected override async Task Handle(UpdateAuditoriumCommand request,
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

            var auditorium = await _auditoriumRepository.SingleAsync(
                AuditoriumSpecification.GetById(request.AuditoriumId, user.EventId),
                cancellationToken
                );

            var userAuditorium = new UserAuditorium
            {
                AuditoriumId = auditorium.Key,
                UserId = user.Key
            };
            user.Auditoriums.Add(userAuditorium);
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}