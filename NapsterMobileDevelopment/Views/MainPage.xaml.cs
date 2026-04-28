

using NapsterMobileDevelopment.Models;
using NapsterMobileDevelopment.Services;
using System.Threading.Tasks;

namespace NapsterMobileDevelopment
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public List<Artist> TopArtists { get; set; } = [];
        public List<Album> TopAlbums { get; set; } = [];
        ApiService apiService = new ApiService();


        public MainPage()
        {
  

            InitializeComponent();
            LoadArtists();


        }

        public async Task LoadArtists()
        {

            TopArtists.Add(await apiService.GetArtist("f90e8b26-9e52-4669-a5c9-e28529c47894"));

            BindingContext = null;
            BindingContext = this;
        }

        public async Task LoadTopAlbums()
        {
            //https://musicbrainz.org/ws/2/release/da13b81f-7b09-3fb6-b5c9-8551f22c797e?inc=aliases%2Bartist-credits%2Blabels%2Bdiscids%2Brecordings&fmt=json

        }
        public async void OnArtistClicked(object? sender, EventArgs e)
        {

            //await Navigation.PushAsync(new Views.ArtistPage());

            //HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.UserAgent.ParseAdd("MusicApp/0.0.1 (20148847@tafe.wa.edu.au)");
            //HttpRequestMessage request = new(HttpMethod.Get, "https://musicbrainz.org/ws/2/artist/f90e8b26-9e52-4669-a5c9-e28529c47894?fmt=json");
            //HttpResponseMessage response = await client.SendAsync(request);


            //if (response != null && !response.IsSuccessStatusCode)
            //{
            //    throw new HttpRequestException($"Failed to fetch Api response for query. Server responded: {response.StatusCode}");
            //}


        }
    }
}
