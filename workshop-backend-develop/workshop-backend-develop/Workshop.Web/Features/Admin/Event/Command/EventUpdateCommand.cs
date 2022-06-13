using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Specification;
using Workshop.Infrastructure.Storage.Repositories;

namespace Workshop.Web.Features.Admin.Command
{
    public class EventUpdateCommand : IRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    public class EventUpdateCommandHandler : AsyncRequestHandler<EventUpdateCommand>
    {
        private EventRepository _eventRepository;

        public EventUpdateCommandHandler(EventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        protected override async Task Handle(EventUpdateCommand request, CancellationToken cancellationToken)
        {
            var eventToUpdate = await _eventRepository.SingleAsync(EventSpecification.GetById(request.Id), cancellationToken);
            eventToUpdate.UpdateName(request.Name);
            eventToUpdate.UpdateActivation(request.IsActive);
            await _eventRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}