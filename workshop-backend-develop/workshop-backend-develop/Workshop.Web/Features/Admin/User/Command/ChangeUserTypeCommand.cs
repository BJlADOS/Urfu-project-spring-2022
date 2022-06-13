using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.ProjectProposal;
using Workshop.Core.Domain.Model.RequestProposal;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;

namespace Workshop.Web.Features.Admin.User.Command
{
    public class ChangeUserTypeCommand : IRequest
    {
        public UserType Type { get; set; }
        public long Id { get; set; }
    }

    public class ChangeUserTypeCommandHandler : AsyncRequestHandler<ChangeUserTypeCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRequestProposalRepository _requestProposalRepository;
        private readonly IProjectProposalRepository _proposalRepository;

        public ChangeUserTypeCommandHandler(IUserRepository repository,
            IRequestProposalRepository requestProposalRepository,
            IProjectProposalRepository proposals)
        {
            _userRepository = repository;
            _requestProposalRepository = requestProposalRepository;
            _proposalRepository = proposals;
        }

        protected override async Task Handle(ChangeUserTypeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.SingleAsync(UserSpecification.GetById(request.Id),
                cancellationToken);

            if (user.UserType == UserType.Teamlead && request.Type == UserType.Student
                || user.UserType == UserType.Student && request.Type == UserType.Teamlead)
            {
                var projectProposal = await _proposalRepository.FirstOrDefaultAsync(
                    ProjectProposalSpecification.GetByAuthorId(user.EventId, user.Key), cancellationToken);
                if (projectProposal != null)
                {
                    await _proposalRepository.RemoveAsync(projectProposal);
                    await _proposalRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                } 
                var requestProposals = await _requestProposalRepository
                    .ListAsync(
                        request.Type == UserType.Teamlead
                            ? RequestProposalSpecification.GetByUserId(user.Key,user.EventId)
                            : RequestProposalSpecification.GetByTeamleadId(user.Key,user.EventId),
                        cancellationToken);

                await _requestProposalRepository.RemoveRangeAsync(requestProposals);
                await _requestProposalRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }

            user.UpdateUserType(request.Type);
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}