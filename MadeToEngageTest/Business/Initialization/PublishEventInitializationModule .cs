using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using MadeToEngageTest.Models.Pages;

namespace MadeToEngageTest.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class PublishEventInitializationModule : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var contentEvents = ServiceLocator.Current.GetInstance<IContentEvents>();
            contentEvents.PublishingContent += contentEvents_PublishingContent;
        }

        void contentEvents_PublishingContent(object sender, EPiServer.ContentEventArgs e)
        {
            if (e.Content is EventPage eventPage)
            {
                if(eventPage.EndDate<eventPage.StartDate)
                {
                    e.CancelAction = true;
                    e.CancelReason = "End Date must not be before the Start Date";
                }
            }
        }

        public void Preload(string[] parameters)
        {

        }

        public void Uninitialize(InitializationEngine context)
        {
            var contentEvents = ServiceLocator.Current.GetInstance<IContentEvents>();
            contentEvents.PublishingContent -= contentEvents_PublishingContent;
        }
    }
}