using System;
using System.Linq;
using Xamarin.Forms;
using YoutubeInWebView.Services;

namespace YoutubeInWebView
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            PlayButton.Clicked += PlayButton_Clicked;
            PauseButton.Clicked += PauseButton_Clicked;
            StopButton.Clicked += StopButton_Clicked;
            
            ChangeSizeButton.Clicked += ChangeSizeButton_Clicked;

            var repo = DependencyService.Get<VideoRepository>();
            var videos = repo.GetVideos();
            TimelineView.Init(videos.ToList(), YtPlayerWebview);

        }

        private void PlayButton_Clicked(object sender, EventArgs e)
        {
            YtPlayerWebview.PlayVideo();
        }

        private void PauseButton_Clicked(object sender, EventArgs e)
        {
            YtPlayerWebview.PauseVideo();
        }

        private void StopButton_Clicked(object sender, EventArgs e)
        {
            YtPlayerWebview.StopVideo();
        }

        private void ChangeSizeButton_Clicked(object sender, EventArgs e)
        {
            YtPlayerWebview.WidthRequest = 200;
        }
    }
}
