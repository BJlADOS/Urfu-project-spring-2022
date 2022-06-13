using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;

namespace Workshop.Web.Features.Admin.Projects.Command
{
    public class DeleteKeyTechnologyCommand : IRequest
    {
        public long KeyTechnologyId { get; set; }
    }

    public class DeleteKeyTechnologyCommandHandler : AsyncRequestHandler<DeleteKeyTechnologyCommand>
    {
        private readonly ProjectRepository _projectsRepository;
        private readonly KeyTechnologyRepository _keyTechnologyRepository;
        private IUserProfileProvider _profileProvider;


        public DeleteKeyTechnologyCommandHandler(ProjectRepository repository, IUserProfileProvider profileProvider, KeyTechnologyRepository keyTechnologyRepository)
        {
            _projectsRepository = repository;
            _profileProvider = profileProvider;
            _keyTechnologyRepository = keyTechnologyRepository;
        }

        protected override async Task Handle(DeleteKeyTechnologyCommand request, CancellationToken cancellationToken)
        {
            var eventId = _profileProvider.GetProfile().User.EventId;
            var projects = await _projectsRepository.ListAsync(
                ProjectSpecification.GetByTechnology(request.KeyTechnologyId, eventId),
                cancellationToken);

            if (projects.Length > 0)
            {
                throw new Exception("Удалить можно только элементы без проектов");
            }

            var technology = await _keyTechnologyRepository.SingleAsync(KeyTechnologySpecification.GetById(request.KeyTechnologyId, eventId), cancellationToken);
            await _keyTechnologyRepository.RemoveAsync(technology);
            await _keyTechnologyRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
