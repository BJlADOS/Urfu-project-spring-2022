using System;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model;
using Workshop.Core.Domain.Model.Session;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.SharedKernel.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Workshop.Core.Domain.Model.ApiKey;
using Workshop.Core.Domain.Model.Auditorium;
using Workshop.Core.Domain.Model.Competency;
using Workshop.Core.Domain.Model.Event;
using Workshop.Core.Domain.Model.KeyTechnology;
using Workshop.Core.Domain.Model.LifeScenario;
using Workshop.Core.Domain.Model.Project;
using Workshop.Core.Domain.Model.ProjectCompetency;
using Workshop.Core.Domain.Model.ProjectProposal;
using Workshop.Core.Domain.Model.RequestProposal;
using Workshop.Core.Domain.Model.Role;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.Model.UserCompetency;
using Workshop.Core.Domain.Model.TeamReview;
using Workshop.Core.Domain.Model.TeamCompetencyReview;
using Workshop.Core.Domain.Model.TeamSlot;
using Workshop.Core.Domain.Model.UserAuditorium;

namespace Workshop.Infrastructure.Storage
{
    public class WorkshopDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public WorkshopDbContext(DbContextOptions options) : base(options)
        {
        }

        public WorkshopDbContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RequestProposal> RequestProposals { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<UserCompetency> UserCompetencies { get; set; }
        public DbSet<UserAuditorium> UserAuditoriums { get; set; }
        public DbSet<ProjectCompetency> ProjectCompetencies { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<LifeScenario> LifeScenarios { get; set; }
        public DbSet<KeyTechnology> KeyTechnologies { get; set; }
        public DbSet<Competency> Competencies { get; set; }
        public DbSet<Auditorium> Auditoriums { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TeamReview> TeamReviews { get; set; }
        public DbSet<TeamCompetencyReview> TeamCompetencyReviews { get; set; }
        public DbSet<ProjectProposal> ProjectProposals { get; set; }
        public DbSet<TeamSlot> TeamSlots { get; set; }
        public DbSet<ApiKey> ApiKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ForSqlServerUseIdentityColumns();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkshopDbContext).Assembly);

            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                        property.SetValueConverter(dateTimeConverter);
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            await _mediator.DispatchDomainEventsAsync(this);
            var num = await base.SaveChangesAsync(cancellationToken);
            return num;
        }
    }
}