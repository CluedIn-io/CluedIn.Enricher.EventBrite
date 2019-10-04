using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.EventBrite.Model
{
	public class Event
	{
		[JsonProperty("name")]
		public Name Name { get; set; }

		[JsonProperty("description")]
		public Description Description { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("start")]
		public Start Start { get; set; }

		[JsonProperty("end")]
		public End End { get; set; }

		[JsonProperty("created")]
		public string Created { get; set; }

		[JsonProperty("changed")]
		public string Changed { get; set; }

		[JsonProperty("capacity")]
		public int Capacity { get; set; }

		[JsonProperty("capacity_is_custom")]
		public bool CapacityIsCustom { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("listed")]
		public bool Listed { get; set; }

		[JsonProperty("shareable")]
		public bool Shareable { get; set; }

		[JsonProperty("online_event")]
		public bool OnlineEvent { get; set; }

		[JsonProperty("tx_time_limit")]
		public int TimeLimit { get; set; }

		[JsonProperty("hide_start_date")]
		public bool HideStartDate { get; set; }

		[JsonProperty("hide_end_date")]
		public bool HideEndDate { get; set; }

		[JsonProperty("locale")]
		public string Locale { get; set; }

		[JsonProperty("is_locked")]
		public bool IsLocked { get; set; }

		[JsonProperty("privacy_setting")]
		public string PrivacySetting { get; set; }

		[JsonProperty("is_series")]
		public bool IsSeries { get; set; }

		[JsonProperty("is_series_parent")]
		public bool IsSeriesParent { get; set; }

		[JsonProperty("is_reserved_seating")]
		public bool IsReservedSeating { get; set; }

		[JsonProperty("source")]
		public string Source { get; set; }

		[JsonProperty("is_free")]
		public bool IsFree { get; set; }

		[JsonProperty("logo_id")]
		public string LogoId { get; set; }

		[JsonProperty("organizer_id")]
		public string OrganizerId { get; set; }

		[JsonProperty("venue_id")]
		public string VenueId { get; set; }

		[JsonProperty("category_id")]
		public string CategoryId { get; set; }

		[JsonProperty("subcategory_id")]
		public string SubcategoryId { get; set; }

		[JsonProperty("format_id")]
		public string FormatId { get; set; }

		[JsonProperty("resource_uri")]
		public string ResourceUri { get; set; }

		[JsonProperty("logo")]
		public Logo Logo { get; set; }

		[JsonProperty("vanity_url")]
		public string VanityUrl { get; set; }
	}
}