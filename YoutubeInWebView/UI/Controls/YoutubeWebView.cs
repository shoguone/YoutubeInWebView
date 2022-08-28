using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeInWebView.UI.Controls.Commands;

namespace YoutubeInWebView.UI.Controls
{
    public class YoutubeWebView : WebView
    {
        public const string PlayVideoMessage = "PlayVideo";
        public const string PauseVideoMessage = "PauseVideo";
        public const string StopVideoMessage = "StopVideo";
        public const string SeekToMessage = "SeekTo";
        public const string ClearVideoMessage = "ClearVideo";

        public const string NextVideoMessage = "NextVideo";
        public const string PreviousVideoMessage = "PreviousVideo";
        public const string PlayVideoAtMessage = "PlayVideoAt";

        public const string CueVideoByIdMessage = "CueVideoById";
        public const string LoadVideoByIdMessage = "LoadVideoById";
        public const string CueVideoByUrlMessage = "CueVideoByUrl";
        public const string LoadVideoByUrlMessage = "LoadVideoByUrl";
        public const string CuePlaylistMessage = "CuePlaylist";
        public const string LoadPlaylistMessage = "LoadPlaylist";

        public static readonly BindableProperty YoutubeVideoIdProperty = BindableProperty.Create(
            propertyName: nameof(YoutubeVideoId),
            returnType: typeof(string),
            declaringType: typeof(YoutubeWebView),
            defaultValue: default(string));

        public static readonly BindableProperty VolumeProperty = BindableProperty.Create(
            propertyName: nameof(Volume),
            returnType: typeof(int),
            declaringType: typeof(YoutubeWebView),
            defaultValue: 100);

        public static readonly BindableProperty IsMutedProperty = BindableProperty.Create(
            propertyName: nameof(IsMuted),
            returnType: typeof(bool),
            declaringType: typeof(YoutubeWebView),
            defaultValue: false);

        public static readonly BindableProperty IsLoopProperty = BindableProperty.Create(
            propertyName: nameof(IsLoop),
            returnType: typeof(bool),
            declaringType: typeof(YoutubeWebView),
            defaultValue: false);

        public static readonly BindableProperty IsShuffleProperty = BindableProperty.Create(
            propertyName: nameof(IsShuffle),
            returnType: typeof(bool),
            declaringType: typeof(YoutubeWebView),
            defaultValue: false);

        public static readonly BindableProperty PlaybackRateProperty = BindableProperty.Create(
            propertyName: nameof(PlaybackRate),
            returnType: typeof(float),
            declaringType: typeof(YoutubeWebView),
            defaultValue: 1f);

        public static readonly BindableProperty PlaybackQualityProperty = BindableProperty.Create(
            propertyName: nameof(PlaybackQuality),
            returnType: typeof(string),
            declaringType: typeof(YoutubeWebView),
            defaultValue: PlaybackQualityLevel.Default);

        public event EventHandler OnPlayerReady;
        public event EventHandler<PlayerState> OnPlayerStateChange;
        public event EventHandler<string> OnPlaybackQualityChange;
        public event EventHandler<int> OnPlaybackRateChange;
        /// <summary>
        /// 2 - запрос содержит недопустимое значение параметра. Например, ошибка возникает при указании идентификатора видео, состоящего из менее 11 символов или содержащего недопустимые символы (восклицательный знак, символ звездочки и т. д.).
        /// 5 - ошибка воспроизведения запрошенного содержимого в проигрывателе HTML или другая ошибка, связанная с работой проигрывателя HTML.
        /// 100 - запрошенное видео не найдено. Эта ошибка возникает, если видео было удалено (по любой причине) или помечено как частное.
        /// 101 - владелец запрошенного видео запретил его воспроизведение во встроенных проигрывателях.
        /// 150 - ошибка, аналогичная ошибке 101. Это другой код для ошибки 101.
        /// </summary>
        public event EventHandler<int> OnPlayerError;

        /// <summary>
        /// Don't call this directly
        /// </summary>
        public event EventHandler<TaskCompletionSource<float[]>> _GetAvailablePlaybackRatesHook;
        /// <summary>
        /// Don't call this directly
        /// </summary>
        public event EventHandler<TaskCompletionSource<string[]>> _GetAvailableQualityLevelsHook;
        /// <summary>
        /// Don't call this directly
        /// </summary>
        public event EventHandler<TaskCompletionSource<float>> _GetVideoLoadedFractionHook;
        /// <summary>
        /// Don't call this directly
        /// </summary>
        public event EventHandler<TaskCompletionSource<float>> _GetDurationHook;
        /// <summary>
        /// Don't call this directly
        /// </summary>
        public event EventHandler<TaskCompletionSource<string[]>> _GetPlaylistHook;
        /// <summary>
        /// Don't call this directly
        /// </summary>
        public event EventHandler<TaskCompletionSource<int>> _GetPlaylistIndexHook;

        public string YoutubeVideoId
        {
            get { return (string)GetValue(YoutubeVideoIdProperty); }
            set { SetValue(YoutubeVideoIdProperty, value); }
        }

        public int Volume
        {
            get { return (int)GetValue(VolumeProperty); }
            set { SetValue(VolumeProperty, value); }
        }

        public bool IsMuted
        {
            get { return (bool)GetValue(IsMutedProperty); }
            set { SetValue(IsMutedProperty, value); }
        }

        public bool IsLoop
        {
            get { return (bool)GetValue(IsLoopProperty); }
            set { SetValue(IsLoopProperty, value); }
        }

        public bool IsShuffle
        {
            get { return (bool)GetValue(IsShuffleProperty); }
            set { SetValue(IsShuffleProperty, value); }
        }

        public float PlaybackRate
        {
            get { return (float)GetValue(PlaybackRateProperty); }
            set { SetValue(PlaybackRateProperty, value); }
        }

        public string PlaybackQuality
        {
            get { return (string)GetValue(PlaybackQualityProperty); }
            set { SetValue(PlaybackQualityProperty, value); }
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

        /// <summary>
        /// Don't call this directly
        /// </summary>
        public void InvokeOnPlaybackQualityChange(string quality)
        {
            OnPlaybackQualityChange?.Invoke(this, quality);
        }

        /// <summary>
        /// Don't call this directly
        /// </summary>
        public void InvokeOnPlaybackRateChange(int rate)
        {
            OnPlaybackRateChange?.Invoke(this, rate);
        }

        /// <summary>
        /// Don't call this directly
        /// </summary>
        public void InvokeOnPlayerError(int error)
        {
            OnPlayerError?.Invoke(this, error);
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

        public void SeekTo(SeekToCmd command)
        {
            MessagingCenter.Instance.Send(this, SeekToMessage, command);
        }

        public void ClearVideo()
        {
            MessagingCenter.Instance.Send(this, ClearVideoMessage);
        }


        public void NextVideo()
        {
            MessagingCenter.Instance.Send(this, NextVideoMessage);
        }

        public void PreviousVideo()
        {
            MessagingCenter.Instance.Send(this, PreviousVideoMessage);
        }

        public void PlayVideoAt(int index)
        {
            MessagingCenter.Instance.Send(this, PlayVideoAtMessage, index);
        }


        public void CueVideoById(LoadVideoByIdCmd command)
        {
            MessagingCenter.Instance.Send(this, CueVideoByIdMessage, command);
        }

        public void LoadVideoById(LoadVideoByIdCmd command)
        {
            MessagingCenter.Instance.Send(this, LoadVideoByIdMessage, command);
        }

        public void CueVideoByUrl(LoadVideoByUrlCmd command)
        {
            MessagingCenter.Instance.Send(this, CueVideoByUrlMessage, command);
        }

        public void LoadVideoByUrl(LoadVideoByUrlCmd command)
        {
            MessagingCenter.Instance.Send(this, LoadVideoByUrlMessage, command);
        }

        public void CuePlaylist(LoadVideoByUrlCmd command)
        {
            MessagingCenter.Instance.Send(this, CuePlaylistMessage, command);
        }

        public void LoadPlaylist(LoadVideoByUrlCmd command)
        {
            MessagingCenter.Instance.Send(this, LoadPlaylistMessage, command);
        }


        public Task<float[]> GetAvailablePlaybackRatesAsync()
        {
            var tcs = new TaskCompletionSource<float[]>();
            _GetAvailablePlaybackRatesHook?.Invoke(this, tcs);
            return tcs.Task;
        }

        public Task<string[]> GetAvailableQualityLevelsAsync()
        {
            var tcs = new TaskCompletionSource<string[]>();
            _GetAvailableQualityLevelsHook?.Invoke(this, tcs);
            return tcs.Task;
        }

        public Task<float> GetVideoLoadedFractionAsync()
        {
            var tcs = new TaskCompletionSource<float>();
            _GetVideoLoadedFractionHook?.Invoke(this, tcs);
            return tcs.Task;
        }

        public Task<float> GetDurationAsync()
        {
            var tcs = new TaskCompletionSource<float>();
            _GetDurationHook?.Invoke(this, tcs);
            return tcs.Task;
        }

        public Task<string[]> GetPlaylistAsync()
        {
            var tcs = new TaskCompletionSource<string[]>();
            _GetPlaylistHook?.Invoke(this, tcs);
            return tcs.Task;
        }

        public Task<int> GetPlaylistIndexAsync()
        {
            var tcs = new TaskCompletionSource<int>();
            _GetPlaylistIndexHook?.Invoke(this, tcs);
            return tcs.Task;
        }
    }
}
