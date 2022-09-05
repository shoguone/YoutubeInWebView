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

        private static readonly TimeSpan UpdateDurationInterval = TimeSpan.FromSeconds(1);

        private bool _doUpdateCurrentTime = true;
        private bool _isPlayerReady = false;

        public YoutubeWebView()
        {
            Device.StartTimer(UpdateDurationInterval, UpdateCurrentTime);
        }

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
        public event EventHandler<float> OnCurrentTimeUpdate;

        /// <summary>
        /// From iframe_api_reference:
        /// <br/>2 – The request contains an invalid parameter value. For example, this error occurs if you specify a video ID that does not have 11 characters, or if the video ID contains invalid characters, such as exclamation points or asterisks.
        /// <br/>5 – The requested content cannot be played in an HTML5 player or another error related to the HTML5 player has occurred.
        /// <br/>100 – The video requested was not found. This error occurs when a video has been removed (for any reason) or has been marked as private.
        /// <br/>101 – The owner of the requested video does not allow it to be played in embedded players.
        /// <br/>150 – This error is the same as 101. It's just a 101 error in disguise!
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
        public event EventHandler<TaskCompletionSource<float>> _GetCurrentTimeHook;

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
            get => (string) GetValue(YoutubeVideoIdProperty);
            set => SetValue(YoutubeVideoIdProperty, value);
        }

        public int Volume
        {
            get => (int) GetValue(VolumeProperty);
            set => SetValue(VolumeProperty, value);
        }

        public bool IsMuted
        {
            get => (bool) GetValue(IsMutedProperty);
            set => SetValue(IsMutedProperty, value);
        }

        public bool IsLoop
        {
            get => (bool) GetValue(IsLoopProperty);
            set => SetValue(IsLoopProperty, value);
        }

        public bool IsShuffle
        {
            get => (bool) GetValue(IsShuffleProperty);
            set => SetValue(IsShuffleProperty, value);
        }

        public float PlaybackRate
        {
            get => (float) GetValue(PlaybackRateProperty);
            set => SetValue(PlaybackRateProperty, value);
        }

        public string PlaybackQuality
        {
            get => (string) GetValue(PlaybackQualityProperty);
            set => SetValue(PlaybackQualityProperty, value);
        }

        /// <summary>
        /// Don't call this directly
        /// </summary>
        public void InvokeOnPlayerReady()
        {
            _isPlayerReady = true;
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

        public void CuePlaylist(LoadPlaylistCmd command)
        {
            MessagingCenter.Instance.Send(this, CuePlaylistMessage, command);
        }

        public void LoadPlaylist(LoadPlaylistCmd command)
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

        public Task<float> GetCurrentTimeAsync()
        {
            var tcs = new TaskCompletionSource<float>();
            _GetCurrentTimeHook?.Invoke(this, tcs);
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

        private bool UpdateCurrentTime()
        {
            if (_isPlayerReady)
                GetCurrentTimeAsync().ContinueWith(t => OnCurrentTimeUpdate?.Invoke(this, t.Result));
            return _doUpdateCurrentTime;
        }
    }
}
