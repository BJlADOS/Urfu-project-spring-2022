using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;

namespace Workshop.Web.Features.Public.Auditorium.Command
{
    public class AuditoriumDeleteCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class AuditoriumDeleteCommandHandler : AsyncRequestHandler<AuditoriumDeleteCommand>
    {
        private AuditoriumRepository _auditoriumRepository;
        private IUserProfileProvider _profileProvider;

        public AuditoriumDeleteCommandHandler(AuditoriumRepository auditoriumRepository, IUserProfileProvider profileProvider)
        {
            _auditoriumRepository = auditoriumRepository;
            _profileProvider = profileProvider;
        }

        protected override async Task Handle(AuditoriumDeleteCommand request, CancellationToken cancellationToken)
        {
            var auditorium = await _auditoriumRepository.SingleAsync(AuditoriumSpecification.GetById(request.Id, _profileProvider.GetProfile().User.EventId), cancellationToken);
            await _auditoriumRepository.RemoveAsync(auditorium);
            await _auditoriumRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
