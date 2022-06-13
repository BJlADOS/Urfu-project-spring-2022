using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.ApiKey;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Helpers;
using Workshop.Web.Dtos.Admin.ApiKey;

namespace Workshop.Web.Features.Admin.ApiKey.Command
{
    public class DeleteApiKeyCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteApiKeyCommandHandler : AsyncRequestHandler<DeleteApiKeyCommand>
    {
        private readonly IApiKeyRepository _apiKeyRepository;


        public DeleteApiKeyCommandHandler(IApiKeyRepository apiKeyRepository)
        {
            _apiKeyRepository = apiKeyRepository;
        }

        protected override async Task Handle(DeleteApiKeyCommand request, CancellationToken cancellationToken)
        {
            var apiKey = await _apiKeyRepository.SingleOrDefaultAsync(ApiKeySpecification.GetById(request.Id), cancellationToken);
            if (apiKey == null)
            {
                throw new ArgumentException("No key with id " + request.Id);
            }

            await _apiKeyRepository.RemoveAsync(apiKey);
            await _apiKeyRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
