using NapsterMobileDevelopment.Models;
using NapsterMobileDevelopment.Services;

namespace NapsterMobileDevelopment.Views;

public partial class AlbumPage : ContentPage
{
	public Album ActiveAlbum;
	public string ArtistName { get; set; }

	public string CoverArt {  get; set; }

	public List<Track> Tracks { get; set; }


	public AlbumPage(Album album)
	{

		InitializeComponent();

        ActiveAlbum = album;
        ArtistName = album.ArtistName;
        Tracks = album.Tracks;
		CoverArt = album.CoverArt;

        BindingContext = null;
        BindingContext = this;
	}
}