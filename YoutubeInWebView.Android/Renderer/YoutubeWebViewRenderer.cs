using System.ComponentModel;
using Android.Content;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using YoutubeInWebView.Droid.Renderer;
using YoutubeInWebView.Droid.Javascript;
using YoutubeInWebView.UI.Controls;
using YoutubeInWebView.UI.Controls.Commands;

[assembly: ExportRenderer(typeof(YoutubeWebView), typeof(YoutubeWebViewRenderer))]
namespace YoutubeInWebView.Droid.Renderer
{
    public class YoutubeWebViewRenderer : WebViewRenderer
    {
        const string htmlUrl = "file:///android_asset/Content/index.html";

        private readonly Context _context;

        public YoutubeWebViewRenderer(Context context) : base(context)
        {
            _context = context;
        }

        private YoutubeWebView _HybridWebView => Element as YoutubeWebView;

        public void SetupPlayer()
        {
            Device.BeginInvokeOnMainThread(() =>
                Control.EvaluateJavascript($"setUpPlayer('{_HybridWebView.YoutubeVideoId}', {(int)_HybridWebView.Width}, {(int)_HybridWebView.Height})", null));
        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                UnloadControl();
            }
            if (e.NewElement != null)
            {
                LoadControl();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(YoutubeWebView.Width))
            {
                ExecuteSetSizeJs((int)_HybridWebView.Width, (int)_HybridWebView.Height);
            }
            else if (e.PropertyName == nameof(YoutubeWebView.Height))
            {
                ExecuteSetSizeJs((int)_HybridWebView.Width, (int)_HybridWebView.Height);
            }

            else if (e.PropertyName == nameof(YoutubeWebView.Volume))
            {
                ExecuteSetVolumeJs(_HybridWebView.Volume);
            }
            else if (e.PropertyName == nameof(YoutubeWebView.IsMuted))
            {
                SetIsMutedJs(_HybridWebView.IsMuted);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnloadControl();
            }

            base.Dispose(disposing);
        }

        private void LoadControl()
        {
            Control.AddJavascriptInterface(new JSBridge(this), "jsBridge");
            Control.LoadUrl(htmlUrl);
            SubscribeXView();
        }

        private void UnloadControl()
        {
            Control.RemoveJavascriptInterface("jsBridge");
            UnsubscribeXView();
        }

        private void SubscribeXView()
        {
            MessagingCenter.Instance.Subscribe<YoutubeWebView>(this, YoutubeWebView.PlayVideoMessage, v => ExecutePlayVideoJs());
            MessagingCenter.Instance.Subscribe<YoutubeWebView>(this, YoutubeWebView.PauseVideoMessage, v => ExecutePauseVideoJs());
            MessagingCenter.Instance.Subscribe<YoutubeWebView>(this, YoutubeWebView.StopVideoMessage, v => ExecuteStopVideoJs());
            MessagingCenter.Instance.Subscribe<YoutubeWebView, SeekToCmd>(
                this, YoutubeWebView.SeekToMessage, (v, cmd) => ExecuteSeekToJs(cmd));
            MessagingCenter.Instance.Subscribe<YoutubeWebView>(this, YoutubeWebView.ClearVideoMessage, v => ExecuteClearVideoJs());

            MessagingCenter.Instance.Subscribe<YoutubeWebView>(this, YoutubeWebView.NextVideoMessage, v => ExecuteNextVideoJs());
            MessagingCenter.Instance.Subscribe<YoutubeWebView>(this, YoutubeWebView.PreviousVideoMessage, v => ExecutePreviousVideoJs());
            MessagingCenter.Instance.Subscribe<YoutubeWebView, int>(
                this, YoutubeWebView.PlayVideoAtMessage, (v, i) => ExecutePlayVideoAtJs(i));

            MessagingCenter.Instance.Subscribe<YoutubeWebView, LoadVideoByIdCmd>(
                this, YoutubeWebView.CueVideoByIdMessage, (v, cmd) => ExecuteCueVideoByIdJs(cmd));
            MessagingCenter.Instance.Subscribe<YoutubeWebView, LoadVideoByIdCmd>(
                this, YoutubeWebView.LoadVideoByIdMessage, (v, cmd) => ExecuteLoadVideoByIdJs(cmd));
            MessagingCenter.Instance.Subscribe<YoutubeWebView, LoadVideoByUrlCmd>(
                this, YoutubeWebView.CueVideoByUrlMessage, (v, cmd) => ExecuteCueVideoByUrlJs(cmd));
            MessagingCenter.Instance.Subscribe<YoutubeWebView, LoadVideoByUrlCmd>(
                this, YoutubeWebView.LoadVideoByUrlMessage, (v, cmd) => ExecuteLoadVideoByUrlJs(cmd));
            MessagingCenter.Instance.Subscribe<YoutubeWebView, LoadPlaylistCmd>(
                this, YoutubeWebView.CuePlaylistMessage, (v, cmd) => ExecuteCuePlaylistJs(cmd));
            MessagingCenter.Instance.Subscribe<YoutubeWebView, LoadPlaylistCmd>(
                this, YoutubeWebView.LoadPlaylistMessage, (v, cmd) => ExecuteLoadPlaylistJs(cmd));
        }

        private void UnsubscribeXView()
        {
            MessagingCenter.Instance.Unsubscribe<YoutubeWebView>(this, YoutubeWebView.PlayVideoMessage);
            MessagingCenter.Instance.Unsubscribe<YoutubeWebView>(this, YoutubeWebView.PauseVideoMessage);
            MessagingCenter.Instance.Unsubscribe<YoutubeWebView>(this, YoutubeWebView.StopVideoMessage);
            MessagingCenter.Instance.Unsubscribe<YoutubeWebView, SeekToCmd>(this, YoutubeWebView.SeekToMessage);
            MessagingCenter.Instance.Unsubscribe<YoutubeWebView>(this, YoutubeWebView.ClearVideoMessage);

            MessagingCenter.Instance.Unsubscribe<YoutubeWebView>(this, YoutubeWebView.NextVideoMessage);
            MessagingCenter.Instance.Unsubscribe<YoutubeWebView>(this, YoutubeWebView.PreviousVideoMessage);
            MessagingCenter.Instance.Unsubscribe<YoutubeWebView, int>(this, YoutubeWebView.PlayVideoAtMessage);

            MessagingCenter.Instance.Unsubscribe<YoutubeWebView, LoadVideoByIdCmd>(this, YoutubeWebView.CueVideoByIdMessage);
            MessagingCenter.Instance.Unsubscribe<YoutubeWebView, LoadVideoByIdCmd>(this, YoutubeWebView.LoadVideoByIdMessage);
            MessagingCenter.Instance.Unsubscribe<YoutubeWebView, LoadVideoByUrlCmd>(this, YoutubeWebView.CueVideoByUrlMessage);
            MessagingCenter.Instance.Unsubscribe<YoutubeWebView, LoadVideoByUrlCmd>(this, YoutubeWebView.LoadVideoByUrlMessage);
            MessagingCenter.Instance.Unsubscribe<YoutubeWebView, LoadPlaylistCmd>(this, YoutubeWebView.CuePlaylistMessage);
            MessagingCenter.Instance.Unsubscribe<YoutubeWebView, LoadPlaylistCmd>(this, YoutubeWebView.LoadPlaylistMessage);
        }

        private void ExecutePlayVideoJs()
        {
            Control.EvaluateJavascript("player.playVideo()", null);
        }

        private void ExecutePauseVideoJs()
        {
            Control.EvaluateJavascript("player.pauseVideo()", null);
        }

        private void ExecuteStopVideoJs()
        {
            Control.EvaluateJavascript("player.stopVideo()", null);
        }

        private void ExecuteSeekToJs(SeekToCmd command)
        {
            var json = JsonConvert.SerializeObject(command);
            Control.EvaluateJavascript($"player.seekTo({json})", null);
        }

        private void ExecuteClearVideoJs()
        {
            Control.EvaluateJavascript("player.clearVideo()", null);
        }


        private void ExecuteNextVideoJs()
        {
            Control.EvaluateJavascript("player.nextVideo()", null);
        }

        private void ExecutePreviousVideoJs()
        {
            Control.EvaluateJavascript("player.previousVideo()", null);
        }

        private void ExecutePlayVideoAtJs(int index)
        {
            Control.EvaluateJavascript($"player.playVideoAt({index})", null);
        }


        private void ExecuteCueVideoByIdJs(LoadVideoByIdCmd command)
        {
            var json = JsonConvert.SerializeObject(command);
            Control.EvaluateJavascript($"player.cueVideoById({json})", null);
        }

        private void ExecuteLoadVideoByIdJs(LoadVideoByIdCmd command)
        {
            var json = JsonConvert.SerializeObject(command);
            Control.EvaluateJavascript($"player.loadVideoById({json})", null);
        }

        private void ExecuteCueVideoByUrlJs(LoadVideoByUrlCmd command)
        {
            var json = JsonConvert.SerializeObject(command);
            Control.EvaluateJavascript($"player.cueVideoByUrl({json})", null);
        }

        private void ExecuteLoadVideoByUrlJs(LoadVideoByUrlCmd command)
        {
            var json = JsonConvert.SerializeObject(command);
            Control.EvaluateJavascript($"player.loadVideoByUrl({json})", null);
        }

        private void ExecuteCuePlaylistJs(LoadPlaylistCmd command)
        {
            var json = JsonConvert.SerializeObject(command);
            Control.EvaluateJavascript($"player.cuePlaylist({json})", null);
        }

        private void ExecuteLoadPlaylistJs(LoadPlaylistCmd command)
        {
            var json = JsonConvert.SerializeObject(command);
            Control.EvaluateJavascript($"player.loadPlaylist({json})", null);
        }


        private void ExecuteSetSizeJs(int width, int height)
        {
            Control.EvaluateJavascript($"player.setSize({width}, {height})", null);
        }

        private void ExecuteSetVolumeJs(int volume)
        {
            Control.EvaluateJavascript($"player.setVolume({volume})", null);
        }

        private void SetIsMutedJs(bool isMuted)
        {
            Control.EvaluateJavascript(isMuted ? "player.mute()" : "player.unMute()", null);
        }
    }
}
