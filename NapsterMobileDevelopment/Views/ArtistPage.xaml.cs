
using NapsterMobileDevelopment.Models;
using NapsterMobileDevelopment.Services;
using NapsterMobileDevelopment.Services.Responses;
namespace NapsterMobileDevelopment.Views;

[QueryProperty(nameof(ArtistClass), "Artist")]
[QueryProperty(nameof(ApiServiceObj), "ApiService")]
public partial class ArtistPage : ContentPage
{
    Artist artistClass;
	public Artist ArtistClass
	{
        get => artistClass;
        set
        {
            artistClass = value;
            PopularTracks = artistClass.HalfTracks;
            Albums = artistClass.HalfAlbums;

            OnPropertyChanged(nameof(Albums));
            OnPropertyChanged(nameof(PopularTracks));
            OnPropertyChanged();
        }
    }
	public List<Track> PopularTracks { get; set; }

    public List<Album> Albums { get; set; }

    public Boolean FullTracks = false;

    ApiService apiService;
	public ApiService ApiServiceObj
    {
        get => apiService;
        set
        {
            apiService = value;
            OnPropertyChanged();
        }
    }

    public ArtistPage()
	{

		InitializeComponent();



        // _artist = (Artist)NavigationDataService.Get("Artist");
        //ApiService api = (ApiService)NavigationDataService.Get("ApiService");
        BindingContext = null;
		BindingContext = this;
    }

	public async void ShowMore(object sender, EventArgs e)
	{
		if (FullTracks == false)
		{
			FullTracks = true;
			PopularTracks = artistClass.Tracks;
			ShowMoreButton.Text = "Show Less";

			BindingContext = null;
			BindingContext = this;

		}
		else
		{
            FullTracks = false;
            PopularTracks = artistClass.HalfTracks;
            ShowMoreButton.Text = "Show More...";

            BindingContext = null;
            BindingContext = this;
        }

	}

    public async void AlbumClicked(object? sender, EventArgs e)
    {

        Album album = (sender as Button)?.BindingContext as Album;

		Album newAlbum = await apiService.GetAlbum(album.ID);

        // new Views.AlbumPage(newAlbum, apiService)
        var navigationParameter = new ShellNavigationQueryParameters
            {
                { "Album", newAlbum },
                { "ApiService", apiService}
            };

        await Shell.Current.GoToAsync($"//AlbumPage", navigationParameter);

    }

}