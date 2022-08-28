using Newtonsoft.Json;

namespace YoutubeInWebView.UI.Controls.Commands
{
    public class LoadPlaylistCmd
    {
        [JsonProperty("list")]
        public string List { get; set; }

        [JsonProperty("listType")]
        public string ListType { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("startSeconds")]
        public int StartSeconds { get; set; }

        [JsonProperty("suggestedQuality")]
        public string SuggestedQuality { get; set; }
    }
}
