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
    public class AuditoriumUpdateCommand : IRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public bool IsDefault { get; set; }
    }

    public class AuditoriumUpdateCommandHandler : AsyncRequestHandler<AuditoriumUpdateCommand>
    {
        private AuditoriumRepository _auditoriumRepository;
        private IUserProfileProvider _profileProvider;

        public AuditoriumUpdateCommandHandler(AuditoriumRepository auditoriumRepository, IUserProfileProvider profileProvider)
        {
            _auditoriumRepository = auditoriumRepository;
            _profileProvider = profileProvider;
        }

        protected override async Task Handle(AuditoriumUpdateCommand request, CancellationToken cancellationToken)
        {
            var auditorium = await _auditoriumRepository.SingleAsync(AuditoriumSpecification.GetById(request.Id, _profileProvider.GetProfile().User.EventId), cancellationToken);
            auditorium.UpdateName(request.Name);
            auditorium.UpdateCapacity(request.Capacity);
            auditorium.UpdateDefaultAuditory(request.IsDefault);
            await _auditoriumRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
