using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.ServiceLocation;

namespace MadeToEngageTest.Business.Services
{
    [ServiceConfiguration(ServiceType = typeof(IAuthService))]
    public class AuthService : IAuthService
    {
        public bool IsAuthorized(int userId)
        {
            if (userId == 1 || userId == 2)
            {
                return true;
            }

            return false;
        }
    }
}