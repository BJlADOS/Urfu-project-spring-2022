using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Public.Auditorium;

namespace Workshop.Web.Features.Public.Auditorium.Command
{
    public class AuditoriumAddCommand : IRequest
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public bool IsDefault { get; set; }
    }

    public class AuditoriumAddCommandHandler : AsyncRequestHandler<AuditoriumAddCommand>
    {
        private AuditoriumRepository _auditoriumRepository;
        private IUserProfileProvider _profileProvider;

        public AuditoriumAddCommandHandler(IUserProfileProvider profileProvider, AuditoriumRepository auditoriumRepository)
        {
            _auditoriumRepository = auditoriumRepository;
            _profileProvider = profileProvider;
        }

        protected override async Task Handle(AuditoriumAddCommand request, CancellationToken cancellationToken)
        {
            var eventId = _profileProvider.GetProfile().User.EventId;
            await _auditoriumRepository.AddAsync(
                new Core.Domain.Model.Auditorium.Auditorium(eventId, request.Name, request.Capacity, request.IsDefault),
                cancellationToken
                );
            await _auditoriumRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
