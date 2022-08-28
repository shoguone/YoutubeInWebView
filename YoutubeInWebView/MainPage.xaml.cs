using System;
using Xamarin.Forms;

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
    }
}
