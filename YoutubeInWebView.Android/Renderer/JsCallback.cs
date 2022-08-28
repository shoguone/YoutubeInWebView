using System;
using Android.Webkit;
using Newtonsoft.Json;

namespace YoutubeInWebView.Droid.Renderer
{
    public class JsCallback<T> : Java.Lang.Object, IValueCallback
    {
        public event EventHandler<T> OnResult;

        public void OnReceiveValue(Java.Lang.Object value)
        {
            var result = JsonConvert.DeserializeObject<T>(value.ToString());
            OnResult?.Invoke(this, result);
        }
    }
}