// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClearBitPersonVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the ClearBitPersonVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.ExternalSearch.Providers.EventBrite.Vocabularies
{
    /// <summary>The clear bit Person vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class EventBriteEventVocabulary : SimpleVocabulary
    {
        public EventBriteEventVocabulary()
        {
            this.VocabularyName = "Everbrite Person"; // TODO: Set value
            this.KeyPrefix      = "everbrite.person"; // TODO: Set value
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Infrastructure.User; // TODO: Set value

            this.AddGroup("Everbrite Details", group =>
            {
                this.Id                 = group.Add(new VocabularyKey("Id", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Hidden));
                this.Name               = group.Add(new VocabularyKey("Name", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                this.Capacity           = group.Add(new VocabularyKey("Capacity", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                this.CapacityIsCustom   = group.Add(new VocabularyKey("CapacityIsCustom", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                this.Changed            = group.Add(new VocabularyKey("Changed", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                this.Currency           = group.Add(new VocabularyKey("Currency", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                this.End                = group.Add(new VocabularyKey("End", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                this.HideEndDate        = group.Add(new VocabularyKey("HideEndDate", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Hidden));
                this.HideStartDate      = group.Add(new VocabularyKey("HideStartDate", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Hidden));
                this.FormatId           = group.Add(new VocabularyKey("FormatId", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Hidden));
                this.IsFree             = group.Add(new VocabularyKey("IsFree", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                this.IsLocked           = group.Add(new VocabularyKey("IsLocked", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                this.IsReservedSeating  = group.Add(new VocabularyKey("IsReservedSeating", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                this.IsSeries           = group.Add(new VocabularyKey("IsSeries", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                this.IsSeriesParent     = group.Add(new VocabularyKey("IsSeriesParent", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                this.Listed             = group.Add(new VocabularyKey("Listed", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                this.LogoUrl            = group.Add(new VocabularyKey("LogoUrl", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Hidden));
                this.LogoId             = group.Add(new VocabularyKey("LogoId", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Hidden));
                this.Locale             = group.Add(new VocabularyKey("Locale", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                this.OnlineEvent        = group.Add(new VocabularyKey("OnlineEvent", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                this.PrivacySetting     = group.Add(new VocabularyKey("PrivacySetting", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                this.Shareable          = group.Add(new VocabularyKey("Shareable", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                this.Source             = group.Add(new VocabularyKey("Source", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                this.Start              = group.Add(new VocabularyKey("Start", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                this.Status             = group.Add(new VocabularyKey("Status", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                this.TimeLimit          = group.Add(new VocabularyKey("TimeLimit", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                this.VanityUrl          = group.Add(new VocabularyKey("VanityUrl", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                this.VenueId            = group.Add(new VocabularyKey("VenueId", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Hidden));

            });

            // Mappings
            //AddMapping(this.City,          CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AddressCity);
        }

        public VocabularyKey Id { get; private set; }
        public VocabularyKey Name { get; private set; }
        public VocabularyKey Capacity { get; internal set; }
        public VocabularyKey CapacityIsCustom { get; internal set; }
        public VocabularyKey Changed { get; internal set; }
        public VocabularyKey Currency { get; internal set; }
        public VocabularyKey End { get; internal set; }
        public VocabularyKey HideEndDate { get; internal set; }
        public VocabularyKey HideStartDate { get; internal set; }
        public VocabularyKey FormatId { get; internal set; }
        public VocabularyKey IsFree { get; internal set; }
        public VocabularyKey IsLocked { get; internal set; }
        public VocabularyKey IsReservedSeating { get; internal set; }
        public VocabularyKey IsSeries { get; internal set; }
        public VocabularyKey IsSeriesParent { get; internal set; }
        public VocabularyKey Listed { get; internal set; }
        public VocabularyKey LogoUrl { get; internal set; }
        public VocabularyKey LogoId { get; internal set; }
        public VocabularyKey Locale { get; internal set; }
        public VocabularyKey OnlineEvent { get; internal set; }
        public VocabularyKey PrivacySetting { get; internal set; }
        public VocabularyKey Shareable { get; internal set; }
        public VocabularyKey Source { get; internal set; }
        public VocabularyKey Start { get; internal set; }
        public VocabularyKey Status { get; internal set; }
        public VocabularyKey TimeLimit { get; internal set; }
        public VocabularyKey VanityUrl { get; internal set; }
        public VocabularyKey VenueId { get; internal set; }
    }
}
