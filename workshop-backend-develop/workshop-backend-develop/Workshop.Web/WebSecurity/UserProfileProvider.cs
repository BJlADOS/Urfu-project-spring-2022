using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Workshop.Core.Services.Security;

namespace Workshop.Web.WebSecurity
{
    public class UserProfileProvider : IUserProfileProvider
    {
        private const string Key = "RequestUserProfile";

        private readonly IHttpContextAccessor _httpContextAccessor;

        private HttpContext HttpContext => _httpContextAccessor.HttpContext;

        public UserProfileProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public UserProfile GetProfile()
        {
            HttpContext.Items.TryGetValue(Key, out var userProfile);
            return userProfile as UserProfile;
        }

        public void SetProfile(UserProfile userProfile)
        {
            if (HttpContext.Items.ContainsKey(Key))
            {
                //Exception
            }
            HttpContext.Items[Key] = userProfile;
        }
    }
}
