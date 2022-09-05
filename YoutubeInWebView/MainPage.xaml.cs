using System;
using System.Linq;
using Xamarin.Forms;
using YoutubeInWebView.Services;
using YoutubeInWebView.UI.Controls;

namespace YoutubeInWebView
{
    public partial class MainPage : ContentPage
    {
        private PlayerState _state;

        public MainPage()
        {
            InitializeComponent();

            PlayButton.Clicked += PlayPause;
            NextButton.Clicked += NextButton_Clicked;
            PreviousButton.Clicked += PreviousButton_Clicked;

            var repo = DependencyService.Get<VideoRepository>();
            var videos = repo.GetVideos();
            TimelineView.Init(videos.ToList(), YtPlayerWebview);

            YtPlayerWebview.OnPlayerStateChange += YoutubeWebView_OnPlayerStateChange;
        }

        private void PreviousButton_Clicked(object sender, EventArgs e)
        {
            TimelineView.Previous();
        }

        private void NextButton_Clicked(object sender, EventArgs e)
        {
            TimelineView.Next();
        }

        private void YoutubeWebView_OnPlayerStateChange(object sender, PlayerState playerState)
        {
            _state = playerState;
        }

        public void PlayPause(object sender, EventArgs e)
        {
            if (_state == PlayerState.PAUSED)
                YtPlayerWebview.PlayVideo();
            if (_state == PlayerState.PLAYING)
                YtPlayerWebview.PauseVideo();
        }
    }
}
