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
            try
            {
                var result = JsonConvert.DeserializeObject<T>(value.ToString());
                OnResult?.Invoke(this, result);
            }
            catch (Exception)
            {
                // HACK : value == null check doesn't work with Java.Lang.Object so here is try/catch
            }
        }
    }
}