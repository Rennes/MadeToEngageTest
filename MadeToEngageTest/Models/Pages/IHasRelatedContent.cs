using EPiServer.Core;

namespace MadeToEngageTest.Models.Pages
{
    public interface IHasRelatedContent
    {
        ContentArea RelatedContentArea { get; }
    }
}
