using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Shell.ObjectEditing;

namespace MadeToEngageTest.Models.SelectionFactories
{
    public class SpeakerSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new List<SelectItem>()
            {
                new SelectItem()
                {
                    Text = "Scott Allen",
                    Value = (object) "Scott Allen"
                },
                new SelectItem()
                {
                    Text = "Damien Edwards",
                    Value = (object) "Damien Edwards"
                },
                new SelectItem()
                {
                    Text = "David Fowler",
                    Value = (object) "David Fowler"
                },
                new SelectItem()
                {
                    Text = "Scott Guthrie",
                    Value = (object) "Scott Guthrie"
                }
            };
        }
    }
}