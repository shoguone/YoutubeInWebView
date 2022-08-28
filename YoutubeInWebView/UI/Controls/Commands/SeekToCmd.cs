using Newtonsoft.Json;

namespace YoutubeInWebView.UI.Controls.Commands
{
    public class SeekToCmd
    {
        [JsonProperty("seconds")]
        public int Seconds { get; set; }

        [JsonProperty("allowSeekAhead")]
        public bool AllowSeekAhead { get; set; }
    }
}
