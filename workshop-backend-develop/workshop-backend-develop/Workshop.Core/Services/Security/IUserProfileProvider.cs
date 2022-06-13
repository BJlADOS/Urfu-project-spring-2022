using System;
using System.Collections.Generic;
using System.Text;

namespace Workshop.Core.Services.Security
{
    public interface IUserProfileProvider
    {
        UserProfile GetProfile();
        void SetProfile(UserProfile userProfile);
    }
}
