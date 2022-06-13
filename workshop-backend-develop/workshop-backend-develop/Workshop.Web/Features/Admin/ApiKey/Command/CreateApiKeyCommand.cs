using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.Model.ApiKey;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Helpers;
using Workshop.Core.Services.Security;
using Workshop.Web.Dtos.Admin.ApiKey;

namespace Workshop.Web.Features.Admin.ApiKey.Command
{
    public class CreateApiKeyCommand : IRequest<ApiKeyDto>
    {
        public string Name { get; set; }
        public string UserType { get; set; }
    }

    public class CreateApiKeyCommandHandler : IRequestHandler<CreateApiKeyCommand, ApiKeyDto>
    {
        private readonly IApiKeyRepository _apiKeyRepository;
        private IUserProfileProvider _profileProvider;
        private IMapper _mapper;


        public CreateApiKeyCommandHandler(IApiKeyRepository repository, IUserProfileProvider profileProvider, IMapper mapper)
        {
            _apiKeyRepository = repository;
            _profileProvider = profileProvider;
            _mapper = mapper;
        }

        public async Task<ApiKeyDto> Handle(CreateApiKeyCommand request, CancellationToken cancellationToken)
        {
            var eventId = _profileProvider.GetProfile().User.EventId;
            var keyString = CryptographyHelper.GenerateMD5Hash(CryptographyHelper.GenerateRandomString(16));
            var parseResult = Enum.TryParse<UserType>(request.UserType, out var userType);
            var apiKey = new Core.Domain.Model.ApiKey.ApiKey(request.Name, keyString, parseResult ? userType : UserType.Student, eventId);

            await _apiKeyRepository.AddAsync(apiKey, cancellationToken);
            await _apiKeyRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ApiKeyDto>(apiKey);
            return dto;
        }
    }
}