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
                hybridRenderer.UpdateSize();
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

        [JavascriptInterface]
        [Export("onPlaybackQualityChange")]
        public void OnPlaybackQualityChange(string quality)
        {
            YoutubeWebViewRenderer hybridRenderer;

            if (hybridWebViewRenderer != null && hybridWebViewRenderer.TryGetTarget(out hybridRenderer))
            {
                ((YoutubeWebView)hybridRenderer.Element).InvokeOnPlaybackQualityChange(quality);
            }
        }

        [JavascriptInterface]
        [Export("onPlaybackRateChange")]
        public void OnPlaybackRateChange(int rate)
        {
            YoutubeWebViewRenderer hybridRenderer;

            if (hybridWebViewRenderer != null && hybridWebViewRenderer.TryGetTarget(out hybridRenderer))
            {
                ((YoutubeWebView)hybridRenderer.Element).InvokeOnPlaybackRateChange(rate);
            }
        }

        [JavascriptInterface]
        [Export("onPlayerError")]
        public void OnPlayerError(int error)
        {
            YoutubeWebViewRenderer hybridRenderer;

            if (hybridWebViewRenderer != null && hybridWebViewRenderer.TryGetTarget(out hybridRenderer))
            {
                ((YoutubeWebView)hybridRenderer.Element).InvokeOnPlayerError(error);
            }
        }
    }
}

