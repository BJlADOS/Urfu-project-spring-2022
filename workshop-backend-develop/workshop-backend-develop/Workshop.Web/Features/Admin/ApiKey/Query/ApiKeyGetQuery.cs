using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.Specification;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Admin.ApiKey;
using Workshop.Web.Dtos.Public.User;

namespace Workshop.Web.Features.Admin.ApiKey.Query
{
    public class ApiKeyGetQuery : IRequest<ApiKeyDto>
    {
        public long Id { get; set; }
    }

    public class ApiKeyQueryHandler : IRequestHandler<ApiKeyGetQuery, ApiKeyDto>
    {
        private readonly ApiKeyRepository _apiKeyRepository;
        private readonly IMapper _mapper;

        public ApiKeyQueryHandler(ApiKeyRepository apiKeyRepository, IMapper mapper)
        {
            _apiKeyRepository = apiKeyRepository;
            _mapper = mapper;
        }

        public async Task<ApiKeyDto> Handle(ApiKeyGetQuery request, CancellationToken cancellationToken)
        {
            var apiKey = await _apiKeyRepository.SingleAsync(ApiKeySpecification.GetById(request.Id), cancellationToken);

            var apiKeyDto = _mapper.Map<ApiKeyDto>(apiKey);

            return apiKeyDto;
        }
    }
}