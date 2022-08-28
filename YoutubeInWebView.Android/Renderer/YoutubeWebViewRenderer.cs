using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using YoutubeInWebView.Droid.Renderer;
using YoutubeInWebView.Droid.Javascript;
using YoutubeInWebView.UI.Controls;
using System.ComponentModel;

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
        }

        private void UnsubscribeXView()
        {
            MessagingCenter.Instance.Unsubscribe<YoutubeWebView>(this, YoutubeWebView.PlayVideoMessage);
            MessagingCenter.Instance.Unsubscribe<YoutubeWebView>(this, YoutubeWebView.PauseVideoMessage);
            MessagingCenter.Instance.Unsubscribe<YoutubeWebView>(this, YoutubeWebView.StopVideoMessage);
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

    }
}
