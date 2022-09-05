using Newtonsoft.Json;

namespace YoutubeInWebView.Dtos.Api
{
    public class SegmentDto
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("video_source")]
        public VideoSourceDto VideoSource { get; set; }
    }
}
