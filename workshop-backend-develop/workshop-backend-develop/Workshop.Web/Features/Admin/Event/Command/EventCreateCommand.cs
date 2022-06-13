using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;

namespace Workshop.Web.Features.Admin.Command
{
    public class EventCreateCommand : IRequest
    {
        public string Name { get; set; }
    }

    public class EventCreateCommandHandler : AsyncRequestHandler<EventCreateCommand>
    {
        private EventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserProfileProvider _profileProvider;

        public EventCreateCommandHandler(EventRepository eventRepository, IUserRepository userRepository, IUserProfileProvider profileProvider)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
            _profileProvider = profileProvider;
        }

        protected override async Task Handle(EventCreateCommand request, CancellationToken cancellationToken)
        {
            var newEvent = new Core.Domain.Model.Event.Event(request.Name);
            await _eventRepository.AddAsync(newEvent, cancellationToken);
            await _eventRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            await CreateAdminUserForNewEvent(cancellationToken);
        }

        private async Task CreateAdminUserForNewEvent(CancellationToken cancellationToken)
        {
            var currentUser = _profileProvider.GetProfile().User;
            if (currentUser == null)
                return;
            var createdEvent = await _eventRepository.ListAsync(cancellationToken);
            var newUser = CreateNewAdminUser(currentUser, createdEvent.Last().Key);
            await _userRepository.AddAsync(newUser, cancellationToken);
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        private Workshop.Core.Domain.Model.User.User CreateNewAdminUser(Workshop.Core.Domain.Model.User.User currentUser, long eventId)
        {
            var targetUser = new Workshop.Core.Domain.Model.User.User(currentUser.Login, currentUser.Password, currentUser.Salt, currentUser.UserType, eventId);
            targetUser.UpdateFirstName(currentUser.FirstName);
            targetUser.UpdateLastName(currentUser.LastName);
            targetUser.UpdateMiddleName(currentUser.MiddleName);
            targetUser.UpdateEmail(currentUser.Email);
            targetUser.UpdateAcademicGroup(currentUser.AcademicGroup);
            targetUser.UpdatePhoneNumber(currentUser.PhoneNumber);
            targetUser.UpdateAbout(currentUser.About);
            targetUser.UpdateProfileFilled(currentUser.ProfileFilled);
            return targetUser;
        }
    }
}