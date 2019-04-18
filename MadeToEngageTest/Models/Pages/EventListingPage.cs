using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace MadeToEngageTest.Models.Pages
{
    [SiteContentType(GroupName = Global.GroupNames.Default, DisplayName = "EventListingPage", GUID = "f2e332f3-b5bc-44e5-a579-8bfd6ebce8bb", Description = "")]
    [SiteImageUrl]
    [AvailableContentTypes(
        Availability.Specific,
        Include = new[] { typeof(EventPage) })]
    public class EventListingPage : SitePageData
    {

    }
}