using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.EventBrite.Model
{
	public class Description
	{
		[JsonProperty("text")]
		public string Text { get; set; }

		[JsonProperty("html")]
		public string Html { get; set; }

	}
}