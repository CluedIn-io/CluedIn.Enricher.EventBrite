// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClearBitExternalSearchProvider.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the ClearBitExternalSearchProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;

using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Data.Parts;
using CluedIn.Core.Utilities;

using RestSharp;

using CluedIn.ExternalSearch.Providers.EventBrite.Vocabularies;
using CluedIn.Crawling.Helpers;
using CluedIn.ExternalSearch.Providers.EventBrite.Model;
using CluedIn.Core.ExternalSearch;

namespace CluedIn.ExternalSearch.Providers.EventBrite
{
    /// <summary>The clear bit external search provider.</summary>
    /// <seealso cref="CluedIn.ExternalSearch.ExternalSearchProviderBase" />
    public class EventBriteExternalSearchProvider : ExternalSearchProviderBase
    {
        private class TemporaryTokenProvider : IExternalSearchTokenProvider
        {
            public string ApiToken { get; private set; }

            public TemporaryTokenProvider(string token)
            {
                ApiToken = token;
            }
        }

        /**********************************************************************************************************
         * CONSTRUCTORS
         **********************************************************************************************************/
        // TODO: Move Magic GUID to constants
        /// <summary>
        /// Initializes a new instance of the <see cref="KloutExternalSearchProvider" /> class.
        /// </summary>
        public EventBriteExternalSearchProvider()
            : base(new Guid("{fb963df5-5fa8-4d88-a8b3-75cce2760f6a}"), EntityType.Organization)
        {
            TokenProvider           = new TemporaryTokenProvider("OESK3LAHG3N2ZW6GPXSX");
            TokenProviderIsRequired = true;
        }

        public EventBriteExternalSearchProvider(IExternalSearchTokenProvider tokenProvider)
            : base(new Guid("{fb963df5-5fa8-4d88-a8b3-75cce2760f6a}"), EntityType.Organization)
        {
            TokenProvider           = tokenProvider;
            TokenProviderIsRequired = true;
        }

        /**********************************************************************************************************
         * METHODS
         **********************************************************************************************************/

        /// <summary>Builds the queries.</summary>
        /// <param name="context">The context.</param>
        /// <param name="request">The request.</param>
        /// <returns>The search queries.</returns>
        public override IEnumerable<IExternalSearchQuery> BuildQueries(ExecutionContext context, IExternalSearchRequest request)
        {
            if (!this.Accepts(request.EntityMetaData.EntityType))
                yield break;

            var existingResults = request.GetQueryResults<Event>(this).ToList();

            Func<string, bool> nameFilter = value => existingResults.Any(r => string.Equals(r.Data.Name.Text, value, StringComparison.InvariantCultureIgnoreCase));

            // Query Input
            var entityType = request.EntityMetaData.EntityType;
            var organizationName = request.QueryParameters.GetValue(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.OrganizationName, new HashSet<string>());

            organizationName.Add(request.EntityMetaData.Name);
            organizationName.Add(request.EntityMetaData.DisplayName);

            if (organizationName != null)
            {
                var values = organizationName;

                foreach (var value in values.Where(v => !nameFilter(v)))
                    yield return new ExternalSearchQuery(this, entityType, ExternalSearchQueryParameter.Name, value);
            }
        }

        /// <summary>Executes the search.</summary>
        /// <param name="context">The context.</param>
        /// <param name="query">The query.</param>
        /// <returns>The results.</returns>
        public override IEnumerable<IExternalSearchQueryResult> ExecuteSearch(ExecutionContext context, IExternalSearchQuery query)
        {
            var name = query.QueryParameters[ExternalSearchQueryParameter.Name].FirstOrDefault();

            if (string.IsNullOrEmpty(name))
                yield break;

            var client = new RestClient(string.Format("https://www.eventbriteapi.com/v3"));
            var request = new RestRequest("events/search?token=" + TokenProvider.ApiToken, Method.GET);
            request.AddParameter("q", name);
            request.AddParameter("start_date.range_start", DateTimeOffset.Now.ToString("yyyy-MM-ddThh:mm:ssZ", CultureInfo.InvariantCulture));

            var response = client.Execute<EventsReponse>(request);
            if (response.Data != null)
            {
                foreach (var @event in response.Data.Events)
                    yield return new ExternalSearchQueryResult<Event>(query, @event);
            }

            else if (response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.NotFound)
                yield break;
            else if (response.ErrorException != null)
                throw new AggregateException(response.ErrorException.Message, response.ErrorException);
            else
                throw new ApplicationException("Could not execute external search query - StatusCode:" + response.StatusCode);
        }

        /// <summary>Builds the clues.</summary>
        /// <param name="context">The context.</param>
        /// <param name="query">The query.</param>
        /// <param name="result">The result.</param>
        /// <param name="request">The request.</param>
        /// <returns>The clues.</returns>
        public override IEnumerable<Clue> BuildClues(ExecutionContext context, IExternalSearchQuery query, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {
            var resultItem = result.As<Event>();

            var clues = new List<Clue>();

            var code = new EntityCode(EntityType.Calendar.Event, "EventBrite", resultItem.Data.Id);

            var clue = new Clue(code, context.Organization);

            clue.Data.OriginProviderDefinitionId = this.Id;

            this.PopulateMetadata(clue.Data.EntityData, resultItem);

            //Update url to get a bigger version of the image.
            using (var webClient = new WebClient())
            using (var stream = webClient.OpenRead(resultItem.Data.Logo.Url))
            {
                var inArray = StreamUtilies.ReadFully(stream);
                if (inArray != null)
                {
                    var rawDataPart = new RawDataPart()
                    {
                        Type = "/RawData/PreviewImage",
                        MimeType = CluedIn.Core.FileTypes.MimeType.Jpeg.Code,
                        FileName = "preview_{0}".FormatWith(resultItem.Data.Logo.Id),
                        RawDataMD5 = FileHashUtility.GetMD5Base64String(inArray),
                        RawData = Convert.ToBase64String(inArray)
                    };

                    clue.Details.RawData.Add(rawDataPart);

                    clue.Data.EntityData.PreviewImage = new ImageReferencePart(rawDataPart, 256, 256); //This is from the url size I requested above
                }
            }

            clues.Add(clue);

            return clues;
        }

        /// <summary>Gets the primary entity metadata.</summary>
        /// <param name="context">The context.</param>
        /// <param name="result">The result.</param>
        /// <param name="request">The request.</param>
        /// <returns>The primary entity metadata.</returns>
        public override IEntityMetadata GetPrimaryEntityMetadata(ExecutionContext context, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {
            var resultItem = result.As<Event>();
            return this.CreateMetadata(request);
        }

        /// <summary>Gets the preview image.</summary>
        /// <param name="context">The context.</param>
        /// <param name="result">The result.</param>
        /// <param name="request">The request.</param>
        /// <returns>The preview image.</returns>
        public override IPreviewImage GetPrimaryEntityPreviewImage(ExecutionContext context, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {
            return null;
        }

        /// <summary>Creates the metadata.</summary>
        /// <param name="resultItem">The result item.</param>
        /// <returns>The metadata.</returns>
        private IEntityMetadata CreateMetadata(IExternalSearchRequest request)
        {
            var metadata = new EntityMetadataPart();

            this.PopulatePrimaryMetadata(metadata, request);

            return metadata;
        }

        private void PopulatePrimaryMetadata(IEntityMetadata metadata, IExternalSearchRequest request)
        {
            var code = new EntityCode(EntityType.Organization, "EventBrite", request.QueryParameters[ExternalSearchQueryParameter.Name].FirstOrDefault());

            metadata.EntityType = EntityType.Organization;

            metadata.OriginEntityCode = code;

            metadata.Codes.Add(code);

            var data = metadata;

            data.Name = request.QueryParameters[ExternalSearchQueryParameter.Name].FirstOrDefault();
        }

        /// <summary>Populates the metadata.</summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="resultItem">The result item.</param>
        private void PopulateMetadata(IEntityMetadata metadata, IExternalSearchQueryResult<Event> resultItem)
        {
            var code = new EntityCode(EntityType.Calendar.Event, "EventBrite", resultItem.Data.Id);

            metadata.EntityType = EntityType.Calendar.Event;

            metadata.OriginEntityCode = code;

            metadata.Codes.Add(code);

            var data = metadata;
            var input = resultItem.Data;

            data.Name = input.Name.Text;
            data.Description = input.Description.Text.ToString();

            data.Properties[EventBriteVocabulary.Event.Id] = input.Id.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.Name] = input.Name.Text.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.Capacity] = input.Capacity.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.CapacityIsCustom] = input.CapacityIsCustom.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.Changed] = input.Changed.PrintIfAvailable();

            DateTimeOffset createdDate;

            if (DateTimeOffset.TryParse(input.Created, out createdDate))
            {
                data.CreatedDate = createdDate;
            }

            data.Properties[EventBriteVocabulary.Event.Currency] = input.Currency.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.End] = input.End.Local.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.FormatId] = input.FormatId.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.HideEndDate] = input.HideEndDate.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.HideStartDate] = input.HideStartDate.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.IsFree] = input.IsFree.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.IsLocked] = input.IsLocked.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.IsReservedSeating] = input.IsReservedSeating.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.IsSeries] = input.IsSeries.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.IsSeriesParent] = input.IsSeriesParent.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.Listed] = input.Listed.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.LogoUrl] = input.Logo?.Url.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.LogoId] = input.LogoId.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.Locale] = input.Locale.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.OnlineEvent] = input.OnlineEvent.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.PrivacySetting] = input.PrivacySetting.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.Shareable] = input.Shareable.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.Source] = input.Source.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.Start] = input.Start.Local.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.Status] = input.Status.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.TimeLimit] = input.TimeLimit.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.VanityUrl] = input.VanityUrl.PrintIfAvailable();
            data.Properties[EventBriteVocabulary.Event.VenueId] = input.VenueId.PrintIfAvailable();

            data.Uri = new Uri(input.Url);
        }
    }
}
