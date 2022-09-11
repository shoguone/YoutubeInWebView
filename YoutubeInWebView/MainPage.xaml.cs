using System;
using System.Linq;
using Xamarin.Forms;
using YoutubeInWebView.Dtos.Api.Search;
using YoutubeInWebView.Services;
using YoutubeInWebView.UI.ViewModels;

namespace YoutubeInWebView
{
    public partial class MainPage : ContentPage
    {
        private SearchResponseDto _searchResponse;

        public MainPage()
        {
            InitializeComponent();

            PlayButton.Clicked += PlayButton_Clicked;
            PauseButton.Clicked += PauseButton_Clicked;
            StopButton.Clicked += StopButton_Clicked;

            SearchButton.Clicked += SearchButton_Clicked;
            PresetsList.ItemSelected += PresetsList_ItemSelected;
            
            var repo = DependencyService.Get<VideoRepository>();
            var videos = repo.GetVideos();
            TimelineView.Init(videos.ToList(), YtPlayerWebview);

        }

        private void PlayButton_Clicked(object sender, EventArgs e)
        {
            YtPlayerWebview.PlayVideo();
        }

        private void PauseButton_Clicked(object sender, EventArgs e)
        {
            YtPlayerWebview.PauseVideo();
        }

        private void StopButton_Clicked(object sender, EventArgs e)
        {
            YtPlayerWebview.StopVideo();
        }

        private async void SearchButton_Clicked(object sender, EventArgs e)
        {
            var apiService = DependencyService.Get<IApiService>();
            _searchResponse = await apiService.SearchAsync("test", "test", 0, "test");
            var presetsVms = _searchResponse.Items
                .Select(item => new PresetViewModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    PreviewImageUrl = item.Thumbnails.Default.Url,
                    //PreviewImage = item.Thumbnails.Default.Url,
                })
                .ToList();
            PresetsList.ItemsSource = presetsVms;
        }

        private async void PresetsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var presetVm = e.SelectedItem as PresetViewModel;
            var searchResultItem = _searchResponse.Items.FirstOrDefault(item => item.Id == presetVm.Id);

            var apiService = DependencyService.Get<IApiService>();
            var preset = (await apiService.PresetsAsync(searchResultItem.PresetId)).Items.FirstOrDefault();

            if (preset == null)
            {
                await DisplayAlert("No presets found!", "No presets found!", "Cancel");
                return;
            }

            var repo = DependencyService.Get<VideoRepository>();
            var videos = repo.UpdateVideos(preset.Segments);
            TimelineView.Reinit(videos.ToList(), YtPlayerWebview);

            var video = videos.FirstOrDefault();
            if (video != null)
            {
                YtPlayerWebview.CueVideoById(new UI.Controls.Commands.LoadVideoByIdCmd
                {
                    VideoId = video.Id,
                    StartSeconds = (float)video.Start.TotalSeconds,
                    EndSeconds = (float)video.Stop.TotalSeconds,
                });
            }
        }
    }
}
