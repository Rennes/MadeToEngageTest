using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using MadeToEngageTest.Models.Pages;
using MadeToEngageTest.Models.ViewModels;

namespace MadeToEngageTest.Controllers
{
    public class EventPageController : PageControllerBase<EventPage>
    {
        public ActionResult Index(EventPage currentPage)
        {
            var model = PageViewModel.Create(currentPage);
            return View(model);
        }
    }
}