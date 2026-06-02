
using NapsterMobileDevelopment.Models;
using NapsterMobileDevelopment.Services;

namespace NapsterMobileDevelopment.Views;

[QueryProperty(nameof(ActiveAlbum), "Album")]
[QueryProperty(nameof(ApiServiceObj), "ApiService")]
public partial class AlbumPage : ContentPage
{
    private Album activeAlbum;
    public Album ActiveAlbum
    {
        get => activeAlbum;
        set
        {
            activeAlbum = value;
            OnPropertyChanged();
        }
    }
   

    private ApiService apiService;
    public ApiService ApiServiceObj
    {
        get => apiService;
        set
        {
            apiService = value;
            OnPropertyChanged();
        }
    }


    public AlbumPage()
	{

        InitializeComponent();


        BindingContext = null;
        BindingContext = this;
	}

	public async void ArtistButtonClicked(object sender, EventArgs e)
	{
        Artist artist = await apiService.GetArtist(activeAlbum.ArtistID, true);

        var navigationParameter = new ShellNavigationQueryParameters
        {
            { "Artist", artist },
            { "ApiService", apiService}
        };

        await Shell.Current.GoToAsync($"//ArtistPage", navigationParameter);
    }
}