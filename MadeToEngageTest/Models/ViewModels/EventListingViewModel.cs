using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MadeToEngageTest.Models.Pages;

namespace MadeToEngageTest.Models.ViewModels
{
    public class EventListingViewModel : PageViewModel<EventListingPage>
    {
        public EventListingViewModel(EventListingPage currentPage) : base(currentPage)
        {
        }

        public IEnumerable<EventPage> AllEvents { get; set; }
    }
}