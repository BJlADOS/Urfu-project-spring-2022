using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Workshop.Web.WebSecurity
{
    public static class SecurityCookieExtensions
    {
        private const string SessionCookieName = "SID";
        public static bool TryGetSid(this IRequestCookieCollection cookies, out Guid userSid)
        {
            if (cookies.TryGetValue(SessionCookieName, out string sidRaw))
            {
                userSid = Guid.Parse(sidRaw);
                return true;
            }

            userSid = Guid.Empty;
            return false;
        }

        public static void SetSid(this IResponseCookies cookies, Guid sid, DateTime expiresDate)
        {
            cookies.Append(SessionCookieName, sid.ToString(), new CookieOptions()
            {
                Expires = expiresDate
            });
        }
    }
}
