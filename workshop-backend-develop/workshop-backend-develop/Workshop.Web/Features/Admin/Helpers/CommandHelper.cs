using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.Competency;
using Workshop.Core.Domain.Model.KeyTechnology;
using Workshop.Core.Domain.Model.LifeScenario;
using Workshop.Core.Domain.Model.ProjectCompetency;
using Workshop.Core.Domain.Model.Role;
using Workshop.Core.Domain.SharedKernel;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Helpers;
using Workshop.Infrastructure.Storage.Repositories;

namespace Workshop.Web.Features.Admin.Helpers
{
    public static class CommandHelper
    {
        private static async Task UpdateEntity<T>(IRepository<T> repository,
                                                  IEnumerable<string> namesToAdd,
                                                  IDictionary<string, T> existedEntities,
                                                  Func<string, T> constructor,
                                                  CancellationToken cancellationToken)
            where T : Entity, IAggregateRoot
        {
            foreach (var name in namesToAdd)
            {
                if (existedEntities.ContainsKey(name))
                    continue;
                var entity = constructor(name);
                existedEntities.Add(name, entity);
                await repository.AddAsync(entity, cancellationToken);
            }

            await repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public static async Task UpdateRoles(
            RoleRepository roleRepository,
            IEnumerable<string> roleNames,
            Core.Domain.Model.Project.Project project,
            CancellationToken cancellationToken)
        {
            if (roleNames == null)
                return;

            var roles = roleNames.Select(x => new Role(x, project)).ToList();
            await roleRepository.AddRangeAsync(roles, cancellationToken);
        }

        public static async Task<Dictionary<string, KeyTechnology>> UpdateKeyTechnologies(
            KeyTechnologyRepository keyTechnologyRepository,
            long eventId,
            IEnumerable<string> keyTechnologyNames,
            CancellationToken cancellationToken)
        {
            var entities = (await keyTechnologyRepository.ListAsync(
                 KeyTechnologySpecification.GetAll(eventId), cancellationToken))
                .ToNoneDuplicatedDictionary(x => x.Name, x => x);
            await UpdateEntity(keyTechnologyRepository,
                               keyTechnologyNames,
                               entities,
                               name => new KeyTechnology(name, eventId),
                               cancellationToken);
            return entities;
        }

        public static async Task<Dictionary<string, LifeScenario>> UpdateLifeScenarios(
            LifeScenarioRepository lifeScenarioRepository,
            long eventId,
            IEnumerable<string> lifeScenarioNames,
            CancellationToken cancellationToken)
        {
            var entities =
                (await lifeScenarioRepository.ListAsync(LifeScenarioSpecification.GetAll(eventId),
                                                        cancellationToken))
                .ToNoneDuplicatedDictionary(x => x.Name, x => x);
            await UpdateEntity(lifeScenarioRepository,
                               lifeScenarioNames,
                               entities,
                               name => new LifeScenario(name, eventId),
                               cancellationToken);
            return entities;
        }

        public static List<ProjectCompetency> GetProjectCompetencies(
            long key,
            IEnumerable<string> softSkills,
            IEnumerable<string> hardSkills,
            Dictionary<string, Competency> competencies)
        {
            var listCompetencies = new Dictionary<string, ProjectCompetency>();

            foreach (var item in softSkills.Concat(hardSkills))
            {
                var projectCompetency = new ProjectCompetency(key, competencies[item].Key);
                if (!listCompetencies.ContainsKey(item))
                {
                    listCompetencies.Add(item, projectCompetency);
                }
            }

            return listCompetencies.Values.ToList();
        }

        public static async Task<Dictionary<string, Competency>> UpdateCompetencies(
            CompetencyRepository competencyRepository,
            IEnumerable<string> softSkills,
            IEnumerable<string> hardSkills,
            CancellationToken cancellationToken)
        {
            var entities = (await competencyRepository.ListAsync(cancellationToken))
                .ToNoneDuplicatedDictionary(x => x.Name, x => x);
            await UpdateEntity(competencyRepository,
                               softSkills,
                               entities,
                               name => new Competency(name, CompetencyType.SoftSkill),
                               cancellationToken);
            await UpdateEntity(competencyRepository,
                               hardSkills,
                               entities,
                               name => new Competency(name, CompetencyType.HardSkill),
                               cancellationToken);
            return entities;
        }
    }
}