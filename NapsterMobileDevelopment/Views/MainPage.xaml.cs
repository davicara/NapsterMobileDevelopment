

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
        ApiService apiService;


        public MainPage(ApiService api)
        {
            apiService = api;
            List<string> TopAlbumIds = ["b6b5b654-5c60-4520-b099-4a50d33e2bd0", "da13b81f-7b09-3fb6-b5c9-8551f22c797e", "eca6d001-35bb-4a1a-9bcb-8efd15814cc0", "80789e06-8449-45e7-92ca-d406b95738ed", "bc2b7291-11f1-4307-8191-df5639f96207", "6dd43823-4932-4b89-bdf2-968f463d6611", "8a5b0abf-f6a4-442c-8deb-478091d4523e", "08f54f68-7c89-4e22-8a0f-ac2b06e48568"];


            InitializeComponent();
            //LoadArtists();
            LoadTopAlbums(TopAlbumIds);

        }

        public async Task LoadArtists()
        {

            TopArtists.Add(await apiService.GetArtist("f90e8b26-9e52-4669-a5c9-e28529c47894"));

            BindingContext = null;
            BindingContext = this;
        }

        public async Task LoadTopAlbums(List<string> albumsIDs)
        {

            List<Task<Album>> aList = [];
            foreach (string ID in albumsIDs)
            {
                aList.Add(apiService.GetAlbum(ID));
            }

            await Task.WhenAll(aList);

            TopAlbums.AddRange(aList.Select((task) => task.Result));


            //TopAlbums.Add(await apiService.GetAlbum("da13b81f-7b09-3fb6-b5c9-8551f22c797e"));
            //TopAlbums.Add(await apiService.GetAlbum("12bd0263-9907-4fb4-964a-b94d8784bc30"));

            BindingContext = null;
            BindingContext = this;
        }

        public async void AlbumClicked(object? sender, EventArgs e)
        {

            Album album = (sender as Button)?.BindingContext as Album;

            var navigationParameter = new ShellNavigationQueryParameters
            {
                { "Album", album },
                { "ApiService", apiService}
            };



            //new Views.AlbumPage(album, apiService)

            await Shell.Current.GoToAsync($"//AlbumPage", navigationParameter);

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
