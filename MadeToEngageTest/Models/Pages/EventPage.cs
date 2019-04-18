using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using MadeToEngageTest.Business.EditorDescriptors;
using MadeToEngageTest.Enums;
using MadeToEngageTest.Models.SelectionFactories;

namespace MadeToEngageTest.Models.Pages
{
    [SiteContentType(GroupName = Global.GroupNames.Default, DisplayName = "EventPage", GUID = "69120ec5-d3ba-4dd3-b2b2-bc0a1cde9baf", Description = "")]
    [SiteImageUrl]
    public class EventPage : StandardPage
    {
        [UIHint(UIHint.Textarea)]
        [Display(GroupName = "Content")]
        public virtual string Summary { get; set; }
        [Display(GroupName = "Content")]
        public virtual XhtmlString Description { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Title must be alphanumeric")]
        [Display(GroupName = "Event")]
        public virtual string Title { get; set; }
        [SelectOne(SelectionFactoryType = typeof(SpeakerSelectionFactory))]
        [Display(GroupName = "Event")]
        public virtual string Speaker { get; set; }
        [Range(1,100)]
        [Display(GroupName = "Event")]
        public  virtual int NoAttendees { get; set; }
        [Required]
        [Display(GroupName = "Event")]
        public virtual DateTime StartDate { get; set; }
        [Display(GroupName = "Event")]
        public virtual DateTime EndDate { get; set; }
        [UIHint(UIHint.Image)]
        [Display(GroupName = "Event")]
        public virtual ContentReference EventImage { get; set; }
        [Display(GroupName = "Content")]
        public virtual ContentArea AdditionalContent { get; set; }
        [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<EventStatus>))]
        [Display(Name = "Event", GroupName = "Event")]
        public virtual EventStatus EventStatus { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            this.Speaker = "Scott Allen";
        }
    }
}