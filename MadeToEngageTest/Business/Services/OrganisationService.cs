using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.ServiceLocation;

namespace MadeToEngageTest.Business.Services
{
    [ServiceConfiguration(ServiceType = typeof(IOrganisationService))]
    public class OrganisationService : IOrganisationService
    {
        public bool GetOgranisationalDiscountForUser(int userId, out decimal discount)
        {
            discount = 0;
            return false;
        }
    }
}