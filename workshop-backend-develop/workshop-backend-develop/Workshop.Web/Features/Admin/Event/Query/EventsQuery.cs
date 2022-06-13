using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Web.Dtos.Expert.Event;

namespace Workshop.Web.Features.Admin.Event.Query
{
    public class EventsQuery : IRequest<ICollection<EventFullDto>>
    {
    }

    public class EventsQueryHandler : IRequestHandler<EventsQuery, ICollection<EventFullDto>>
    {
        public IReadOnlyRepository<Core.Domain.Model.Event.Event> _eventRepository;
        private IMapper _mapper;

        public EventsQueryHandler(IReadOnlyRepository<Core.Domain.Model.Event.Event> eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<EventFullDto>> Handle(EventsQuery request, CancellationToken cancellationToken)
        {
            var events = await _eventRepository.ListAsync(cancellationToken);
            return _mapper.Map<ICollection<EventFullDto>>(events);
        }
    }
}