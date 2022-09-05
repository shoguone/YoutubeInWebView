using System;
using System.Collections.Generic;

namespace YoutubeInWebView.UI.Controls.SegmentedTimeline.Models
{
    public class FragmentedTimelineFactory
    {
        private readonly float _canvasWidth;
        private readonly float _separatorWidth;

        public FragmentedTimelineFactory(float canvasWidth, float separatorWidth)
        {
            _canvasWidth = canvasWidth;
            _separatorWidth = separatorWidth;
        }

        public List<TimelineFragment> Create(float totalDurationS, TimeSpan[] fragmentsDurations)
        {
            var separatorsCount = fragmentsDurations.Length - 1;
            var fragments = new List<TimelineFragment>();
            var fragmentsWidth = _canvasWidth - _separatorWidth * separatorsCount;

            var currentX = 0F;

            for (var i = 0; i < fragmentsDurations.Length; i++)
            {
                var duration = fragmentsDurations[i];
                var width = (float) (duration.TotalSeconds / totalDurationS) * fragmentsWidth;

                fragments.Add(CreateVideoFragment(i, currentX, width, duration));

                currentX += width;

                if (i >= fragmentsDurations.Length - 1)
                    continue;

                fragments.Add(CreateSeparatorFragment(currentX));

                currentX += _separatorWidth;
            }

            return fragments;
        }

        private TimelineFragment CreateVideoFragment(int index, float x, float width, TimeSpan duration) =>
            new TimelineFragment
            {
                Index = index,
                StartX = x,
                EndX = x + width,
                Width = width,
                Duration = duration,
            };

        private TimelineFragment CreateSeparatorFragment(float x) =>
            new TimelineFragment
            {
                Index = -1,
                StartX = x,
                Width = _separatorWidth,
                EndX = x + _separatorWidth,
                Duration = TimeSpan.Zero,
            };
    }
}
