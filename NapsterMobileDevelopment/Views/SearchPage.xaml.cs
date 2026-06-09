using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using NapsterMobileDevelopment.Models;
using NapsterMobileDevelopment.Services;
using NapsterMobileDevelopment.Views;
using System.Collections.Generic;


namespace NapsterMobileDevelopment.Views;

public partial class SearchPage : ContentPage
{

    ApiService apiService;

    List<Artist> Artists {  get; set; } = new List<Artist>();

    List<Album> Albums { get; set; } = new List<Album>();
    List<Track> Tracks { get; set; } = new List<Track>();

    public List<SearchViewModel> ViewModels { get; set; } = new List<SearchViewModel>();



    public SearchPage(ApiService api)
	{
        apiService = api;
		InitializeComponent();

        BindingContext = this;

        filterPicker.ItemsSource = new List<string>
        {
        "Albums",
        "Artists",
        "Tracks"
        };


    }

    public async void ItemClicked(object sender, EventArgs e)
    {

        SearchViewModel viewModel = (sender as Button)?.BindingContext as SearchViewModel;

        switch (viewModel.Type)
        {
            case "Album":
                Album newAlbum = await apiService.GetAlbum(viewModel.ID);

                var navigationParameter1 = new ShellNavigationQueryParameters
                {
                    { "Album", newAlbum }
                };

                await Shell.Current.GoToAsync($"//AlbumPage", navigationParameter1);
                break;

            case "Artist":
                Artist artist = await apiService.GetArtist(viewModel.ID, true);

                var navigationParameter2 = new ShellNavigationQueryParameters
                {
                    { "Artist", artist }
                };

                await Shell.Current.GoToAsync($"//ArtistPage", navigationParameter2);
                break;
        }
    }

	public async void SearchPressed(object sender, EventArgs e)
	{
        string query = searchBar.Text;

        if (string.IsNullOrWhiteSpace(query))
            return;

        string selected = filterPicker.SelectedItem as string;

        if (selected == null) { return; }

        ViewModels.Clear();
        Tracks.Clear();
        Albums.Clear();
        Artists.Clear();

        switch (selected)
        {
            case "Albums":
                Albums = await apiService.SearchAlbums(query);

                List<Task> tasks1 = [];
                foreach (Album album in Albums)
                {
                    tasks1.Add(apiService.GetAlbumImage(album));
                }

                await Task.WhenAll(tasks1);


                foreach (Album album in Albums)
                {
                    ViewModels.Add(new SearchViewModel("Album", album.ID, album.Title, album.CoverArt, album.ArtistName));
                }
                break;

            case "Artists":
                Artists = await apiService.SearchArtists(query);

                foreach (Artist artist in Artists)
                {
                    ViewModels.Add(new SearchViewModel("Artist", artist.ID, artist.Name, "", "Artist"));
                }
                break;

            case "Tracks":
                Tracks = await apiService.SearchTracks(query);

                List<Task> tasks = [];
                foreach (Track track in Tracks)
                {
                    tasks.Add(apiService.GetTrackImage(track));
                }

                await Task.WhenAll(tasks);

                foreach (Track track in Tracks)
                {

                    //string image = await apiService.GetTrackImage(track);
                    //track.Image = image;
                    ViewModels.Add(new SearchViewModel("Tracks", track.ID, track.Title, track.Image, track.StringCredits));

                }
                break;
        }

        BindingContext = null;
        BindingContext = this;
    }

}