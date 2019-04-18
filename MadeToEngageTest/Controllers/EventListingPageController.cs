using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using MadeToEngageTest.Business;
using MadeToEngageTest.Models.Pages;
using MadeToEngageTest.Models.ViewModels;

namespace MadeToEngageTest.Controllers
{
    public class EventListingPageController : PageController<EventListingPage>
    {
        private IContentLocator _iContentLocator;
        public EventListingPageController(IContentLocator iContentLocator)
        {
            _iContentLocator = iContentLocator;
        }
        public ActionResult Index(EventListingPage currentPage)
        {
            var model = new EventListingViewModel(currentPage)
            {
                AllEvents = _iContentLocator.GetEventPages(currentPage.ContentLink)
            };
            return View(model);
        }
    }
}