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
    public class DeleteLifeScenarioCommand : IRequest
    {
        public long LifeScenarioId { get; set; }
    }

    public class DeleteLifeScenarioCommandHandler : AsyncRequestHandler<DeleteLifeScenarioCommand>
    {
        private readonly ProjectRepository _projectsRepository;
        private readonly LifeScenarioRepository _lifeScenarioRepository;
        private IUserProfileProvider _profileProvider;


        public DeleteLifeScenarioCommandHandler(ProjectRepository repository, IUserProfileProvider profileProvider, LifeScenarioRepository lifeScenarioRepository)
        {
            _projectsRepository = repository;
            _profileProvider = profileProvider;
            _lifeScenarioRepository = lifeScenarioRepository;
        }

        protected override async Task Handle(DeleteLifeScenarioCommand request, CancellationToken cancellationToken)
        {
            var eventId = _profileProvider.GetProfile().User.EventId;
            var projects = await _projectsRepository.ListAsync(
                ProjectSpecification.GetByScenario(request.LifeScenarioId, eventId),
                cancellationToken
                );

            if (projects.Length > 0)
            {
                throw new Exception("Удалить можно только элементы без проектов");
            }

            var scenario = await _lifeScenarioRepository.SingleAsync(LifeScenarioSpecification.GetById(request.LifeScenarioId, eventId), cancellationToken);
            await _lifeScenarioRepository.RemoveAsync(scenario);
            await _lifeScenarioRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
