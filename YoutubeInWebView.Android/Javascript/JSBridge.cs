using System;
using Android.Webkit;
using Java.Interop;
using YoutubeInWebView.Droid.Renderer;
using YoutubeInWebView.UI.Controls;

namespace YoutubeInWebView.Droid.Javascript
{
    public class JSBridge : Java.Lang.Object
    {
        readonly WeakReference<YoutubeWebViewRenderer> hybridWebViewRenderer;

        public JSBridge(YoutubeWebViewRenderer hybridRenderer)
        {
            hybridWebViewRenderer = new WeakReference<YoutubeWebViewRenderer>(hybridRenderer);
        }

        [JavascriptInterface]
        [Export("onApiReady")]
        public void OnApiReady()
        {
            YoutubeWebViewRenderer hybridRenderer;

            if (hybridWebViewRenderer != null && hybridWebViewRenderer.TryGetTarget(out hybridRenderer))
            {
                hybridRenderer.SetupPlayer();
            }
        }

        [JavascriptInterface]
        [Export("onPlayerReady")]
        public void OnPlayerReady()
        {
            YoutubeWebViewRenderer hybridRenderer;

            if (hybridWebViewRenderer != null && hybridWebViewRenderer.TryGetTarget(out hybridRenderer))
            {
                ((YoutubeWebView)hybridRenderer.Element).InvokeOnPlayerReady();
            }
        }

        [JavascriptInterface]
        [Export("onPlayerStateChange")]
        public void OnPlayerStateChange(int state)
        {
            YoutubeWebViewRenderer hybridRenderer;

            if (hybridWebViewRenderer != null && hybridWebViewRenderer.TryGetTarget(out hybridRenderer))
            {
                ((YoutubeWebView)hybridRenderer.Element).InvokeOnPlayerStateChange((PlayerState)state);
            }
        }
    }
}

