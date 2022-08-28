using System;
using Xamarin.Forms;

namespace YoutubeInWebView.UI.Controls
{
    public class YoutubeWebView : WebView
    {
        public const string PlayVideoMessage = "PlayVideo";
        public const string PauseVideoMessage = "PauseVideo";
        public const string StopVideoMessage = "StopVideo";

        public static readonly BindableProperty YoutubeVideoIdProperty = BindableProperty.Create(
            propertyName: nameof(YoutubeVideoId),
            returnType: typeof(string),
            declaringType: typeof(YoutubeWebView),
            defaultValue: default(string));

        public event EventHandler OnPlayerReady;
        public event EventHandler<PlayerState> OnPlayerStateChange;

        public string YoutubeVideoId
        {
            get { return (string)GetValue(YoutubeVideoIdProperty); }
            set { SetValue(YoutubeVideoIdProperty, value); }
        }

        /// <summary>
        /// Don't call this directly
        /// </summary>
        public void InvokeOnPlayerReady()
        {
            OnPlayerReady?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Don't call this directly
        /// </summary>
        public void InvokeOnPlayerStateChange(PlayerState playerState)
        {
            OnPlayerStateChange?.Invoke(this, playerState);
        }

        public void PlayVideo()
        {
            MessagingCenter.Instance.Send(this, PlayVideoMessage);
        }

        public void PauseVideo()
        {
            MessagingCenter.Instance.Send(this, PauseVideoMessage);
        }

        public void StopVideo()
        {
            MessagingCenter.Instance.Send(this, StopVideoMessage);
        }
    }
}
