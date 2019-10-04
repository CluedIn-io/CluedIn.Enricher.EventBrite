using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.EventBrite.Model
{
	public class Name
	{
		[JsonProperty("text")]
		public string Text { get; set; }

		[JsonProperty("html")]
		public string Html { get; set; }
	}
}