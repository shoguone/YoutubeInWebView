using Newtonsoft.Json;

namespace YoutubeInWebView.UI.Controls.Commands
{
    public abstract class LoadPlaylistCmd
    {
        [JsonProperty("listType")]
        public virtual string ListType { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("startSeconds")]
        public float StartSeconds { get; set; }

        [JsonProperty("suggestedQuality")]
        public string SuggestedQuality { get; set; }
    }

    public class LoadPlaylistByIdCmd : LoadPlaylistCmd
    {
        [JsonProperty("list")]
        public string PlaylistId { get; set; }

        public override string ListType => PlaylistType.Playlist;
    }

    public class LoadPlaylistByVideoIdsCmd : LoadPlaylistCmd
    {
        [JsonProperty("list")]
        public string[] VideoIds { get; set; }

        public override string ListType => PlaylistType.Playlist;
    }

    public class LoadPlaylistByUserCmd : LoadPlaylistCmd
    {
        [JsonProperty("list")]
        public string User { get; set; }

        public override string ListType => PlaylistType.UserUploads;
    }
}
