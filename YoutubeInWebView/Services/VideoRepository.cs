using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using YoutubeInWebView.Dtos;
using YoutubeInWebView.Dtos.Api;

namespace YoutubeInWebView.Services
{
    public class VideoRepository
    {
        private IEnumerable<VideoDto> _videoDtos;
        public VideoRepository()
        {
            _videoDtos = LoadVideos();
        }

        public IEnumerable<VideoDto> GetVideos() => _videoDtos;

        private IEnumerable<VideoDto> LoadVideos()
        {
            var fileHelper = new FileHelper();
            var path = fileHelper.GetPathToResource("response-segments.json");
            var json = fileHelper.ReadResourceFile(path);
            var segmentDtos = JsonConvert.DeserializeObject<List<SegmentDto>>(json);
            var i = 0;
            var videos = segmentDtos.Select(s => VideoDto.FromSegmentDto(s, i++));
            return videos;
        }
    }
}
