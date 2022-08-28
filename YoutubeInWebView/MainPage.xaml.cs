using System;
using Xamarin.Forms;
using YoutubeInWebView.UI.Controls;
using YoutubeInWebView.UI.Controls.Commands;

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
            LoadButton.Clicked += LoadButton_Clicked;
            ChangeSizeButton.Clicked += ChangeSizeButton_Clicked;
        }

        private void PlayButton_Clicked(object sender, EventArgs e)
        {
            Webview.PlayVideo();
        }

        private void PauseButton_Clicked(object sender, EventArgs e)
        {
            Webview.PauseVideo();
        }

        private void StopButton_Clicked(object sender, EventArgs e)
        {
            Webview.StopVideo();
        }

        private void LoadButton_Clicked(object sender, EventArgs e)
        {
            Webview.LoadVideoById(new LoadVideoByIdCmd {
                VideoId = "junBvKGZCDc",
                StartSeconds = 13,
                //EndSeconds = 20,
                SuggestedQuality = PlaybackQuality.Small,
            });
        }

        private void ChangeSizeButton_Clicked(object sender, EventArgs e)
        {
            Webview.WidthRequest = 200;
        }
    }
}
