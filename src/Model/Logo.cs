using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.EventBrite.Model
{
	public class Logo
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("aspect_ratio")]
		public string AspectRatio { get; set; }

		[JsonProperty("edge_color")]
		public string EdgeColor { get; set; }

		[JsonProperty("edge_color_set")]
		public bool EdgeColorSet { get; set; }
	}
}