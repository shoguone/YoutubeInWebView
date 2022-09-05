using Newtonsoft.Json;

namespace YoutubeInWebView.Dtos.Api
{
    public class VideoSourceDto
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("start_ts")]
        public float StartTs { get; set; }

        [JsonProperty("stop_ts")]
        public float StopTs { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
