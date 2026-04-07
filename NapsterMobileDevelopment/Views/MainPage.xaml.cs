

using NapsterMobileDevelopment.Models;
using NapsterMobileDevelopment.Services;
using System.Threading.Tasks;

namespace NapsterMobileDevelopment
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        List<Task<Artist>> topArtists = [];
        ApiService apiService = new ApiService();


        public MainPage()
        {
            topArtists.Add(apiService.GetArtist("the-weeknd"));
            Console.WriteLine(topArtists);

            InitializeComponent();
        }

        public async void OnArtistClicked(object? sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.ArtistPage());
        }
    }
}
