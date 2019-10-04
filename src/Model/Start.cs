using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.EventBrite.Model
{
	public class Start
	{
		[JsonProperty("timezone")]
		public string TimeZone { get; set; }

		[JsonProperty("local")]
		public string Local { get; set; }

		[JsonProperty("utc")]
		public string Utc { get; set; }
	}
}