using NapsterMobileDevelopment.Models;
using NapsterMobileDevelopment.Services;

namespace NapsterMobileDevelopment.Views;

public partial class AlbumPage : ContentPage
{
	public Album ActiveAlbum;
	public string ArtistName { get; set; }
	public string ArtistID { get; set; }

	public string CoverArt {  get; set; }

	public List<Track> Tracks { get; set; }

	ApiService ApiServiceObj { get; set; }


	public AlbumPage(Album album, ApiService api)
	{

		InitializeComponent();
		ApiServiceObj = api;
        ActiveAlbum = album;
        ArtistName = album.ArtistName;
        Tracks = album.Tracks;
		CoverArt = album.CoverArt;
		ArtistID = album.ArtistID;

        BindingContext = null;
        BindingContext = this;
	}

	public async void ArtistButtonClicked(object sender, EventArgs e)
	{
        Artist artist = await ApiServiceObj.GetArtist(ArtistID);

        await Navigation.PushAsync(new Views.ArtistPage(artist, ApiServiceObj));
    }
}