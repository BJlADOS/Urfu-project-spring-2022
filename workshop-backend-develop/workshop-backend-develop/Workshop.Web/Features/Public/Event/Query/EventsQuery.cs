using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Web.Dtos.Public.Event;

namespace Workshop.Web.Features.Public.Event.Query
{
    public class EventsQuery : IRequest<ICollection<EventDto>>
    {
    }

    public class EventsQueryHandler : IRequestHandler<EventsQuery, ICollection<EventDto>>
    {
        public IReadOnlyRepository<Core.Domain.Model.Event.Event> _eventRepository;
        private IMapper _mapper;

        public EventsQueryHandler(IReadOnlyRepository<Core.Domain.Model.Event.Event> eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<EventDto>> Handle(EventsQuery request, CancellationToken cancellationToken)
        {
            var events = await _eventRepository.ListAsync(EventSpecification.GetAllActive, cancellationToken);
            return _mapper.Map<ICollection<EventDto>>(events);
        }
    }
}