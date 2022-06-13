using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.Specification;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Admin.ApiKey;

namespace Workshop.Web.Features.Admin.ApiKey.Query
{
    public class ApiKeysGetAllQuery : IRequest<ICollection<ApiKeyDto>>
    {
    }

    public class UsersGetQueryHandler : IRequestHandler<ApiKeysGetAllQuery, ICollection<ApiKeyDto>>
    {
        private readonly ApiKeyRepository _apiKeyRepository;
        private readonly IMapper _mapper;

        public UsersGetQueryHandler(ApiKeyRepository apiKeyRepository, IMapper mapper)
        {
            _apiKeyRepository = apiKeyRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<ApiKeyDto>> Handle(ApiKeysGetAllQuery request,
            CancellationToken cancellationToken)
        {
            var apiKeys = await _apiKeyRepository.ListAsync(ApiKeySpecification.GetAll(), cancellationToken);

            return apiKeys.Select(apiKey => _mapper.Map<ApiKeyDto>(apiKey)).ToList();
        }
    }
}