using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using NapsterMobileDevelopment.Models;
using NapsterMobileDevelopment.Services;
using NapsterMobileDevelopment.Views;


namespace NapsterMobileDevelopment.Views;

public partial class SearchPage : ContentPage
{

    ApiService apiService;

    List<Artist> Artists {  get; set; } = new List<Artist>();

    List<Album> Albums { get; set; } = new List<Album>();
    List<Track> Tracks { get; set; } = new List<Track>();

    public SearchPage(ApiService api)
	{
        apiService = api;
		InitializeComponent();


        filterPicker.ItemsSource = new List<string>
        {
        "Albums",
        "Artists",
        "Tracks"
        };
    }

	public async void SearchPressed(object sender, EventArgs e)
	{
        string query = searchBar.Text;

        if (string.IsNullOrWhiteSpace(query))
            return;

        string selected = filterPicker.SelectedItem as string;

        if (selected == null) { return; }
        
        Tracks.Clear();
        Albums.Clear();
        Artists.Clear();

        if (selected == "Albums")
        {
            Albums = await apiService.SearchAlbums(query);
        }
        else if (selected == "Artists")
        {
            Artists = await apiService.SearchArtists(query);
        }
        else if (selected == "Tracks")
        {
            Tracks = await apiService.SearchTracks(query);
        }

        BindingContext = null;
        BindingContext = this;
    }

}