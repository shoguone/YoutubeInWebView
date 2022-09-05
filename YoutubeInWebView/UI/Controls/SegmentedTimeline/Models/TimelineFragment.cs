using System;

namespace YoutubeInWebView.UI.Controls.SegmentedTimeline.Models
{
    public class TimelineFragment
    {
        public int Index { get; set; }
        public float StartX { get; set; }
        public float EndX { get; set; }
        public float Width { get; set; }
        public TimeSpan Duration { get; set; }

        public bool IsSeparator => Index == -1;

        public bool Contains(float x)
        {
            return StartX <= x && x <= EndX;
        }
    }
}
