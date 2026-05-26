using NapsterMobileDevelopment.Models;
using NapsterMobileDevelopment.Services;
using NapsterMobileDevelopment.Services.Responses;
namespace NapsterMobileDevelopment.Views;

public partial class ArtistPage : ContentPage
{

	public Artist ArtistClass { get; set; }
	public List<Track> PopularTracks { get; set; }

    public List<Album> Albums { get; set; }

    public Boolean FullTracks = false;

	public ApiService apiService { get; set; }

	public ArtistPage(Artist _artist, ApiService api)
	{

		InitializeComponent();

		apiService = api;
		PopularTracks = _artist.HalfTracks;
        ArtistClass = _artist;
		Albums = _artist.HalfAlbums;

		BindingContext = null;
		BindingContext = this;
    }

	public async void ShowMore(object sender, EventArgs e)
	{
		if (FullTracks == false)
		{
			FullTracks = true;
			PopularTracks = ArtistClass.Tracks;
			ShowMoreButton.Text = "Show Less";

			BindingContext = null;
			BindingContext = this;

		}
		else
		{
            FullTracks = false;
            PopularTracks = ArtistClass.HalfTracks;
            ShowMoreButton.Text = "Show More...";

            BindingContext = null;
            BindingContext = this;
        }

	}

    public async void AlbumClicked(object? sender, EventArgs e)
    {

        Album album = (sender as Button)?.BindingContext as Album;

		Album newAlbum = await apiService.GetAlbum(album.ID);

        await Navigation.PushAsync(new Views.AlbumPage(newAlbum, apiService));

    }

}