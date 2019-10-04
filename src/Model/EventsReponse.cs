using System.Collections.Generic;
using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.EventBrite.Model
{
    public class EventsReponse
    {
        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }

        [JsonProperty("events")]
        public List<Event> Events { get; set; }
    }
}
