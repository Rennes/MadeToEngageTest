using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Core;
using MadeToEngageTest.Models.Pages;

namespace MadeToEngageTest.Business
{
    public interface IContentLocator
    {
        IEnumerable<T> GetAll<T>(ContentReference rootLink)
            where T : PageData;
        IEnumerable<PageData> FindPagesByPageType(PageReference pageLink, bool recursive, int pageTypeId);
        IEnumerable<ContactPage> GetContactPages();
        IEnumerable<EventPage> GetEventPages(ContentReference parent);
    }
}