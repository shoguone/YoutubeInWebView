using Newtonsoft.Json;

namespace YoutubeInWebView.UI.Controls.Commands
{
    public class LoadVideoByIdCmd
    {
        [JsonProperty("videoId")]
        public string VideoId { get; set; }

        [JsonProperty("startSeconds")]
        public int StartSeconds { get; set; }

        [JsonProperty("endSeconds")]
        public int EndSeconds { get; set; }

        [JsonProperty("suggestedQuality")]
        public string SuggestedQuality { get; set; }
    }
}
