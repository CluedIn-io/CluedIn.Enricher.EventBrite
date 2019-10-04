using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.EventBrite.Model
{
	public class Pagination
	{
		[JsonProperty("object_count")]
		public int ObjectCount { get; set; }

		[JsonProperty("page_number")]
		public int PageNumber { get; set; }

		[JsonProperty("page_size")]
		public int PageSize { get; set; }

		[JsonProperty("page_count")]
		public int PageCount { get; set; }
	}
}