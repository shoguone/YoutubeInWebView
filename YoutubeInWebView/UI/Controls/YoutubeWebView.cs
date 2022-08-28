﻿using System;
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

        public void SeekTo()
        {
            MessagingCenter.Instance.Send(this, SeekToMessage);
        }

        public void ClearVideo()
        {
            MessagingCenter.Instance.Send(this, ClearVideoMessage);
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
    }
}
