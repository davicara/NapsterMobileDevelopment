using System;
using System.Collections.Generic;
using System.Text;
using NapsterMobileDevelopment.Models;
using Newtonsoft.Json;

namespace NapsterMobileDevelopment.Services
{

    public class ApiService
    {
        const string baseURL = "https://musicbrainz.org/ws/2";
        HttpClient _client;

        public ApiService() 
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("MusicApp/0.0.1 (20148847@tafe.wa.edu.au)");

        }

        public async Task<string> GetJson(string url)
        {
            string requestUrl = $"{baseURL}/{url}?fmt=json";
            //HttpRequestMessage request = new(HttpMethod.Get, requestUrl);

            HttpResponseMessage? response = null;

            try
            {
                response = await _client.GetAsync(requestUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
            }

            if (response != null && !response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to fetch Api response for query: {url}. Server responded: {response.StatusCode}");
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();


            
        }
        public async Task<Artist> GetArtist(string artistURL)
        {

            string jsonData = await GetJson($"artist/{artistURL}");

            Artist artist = JsonConvert.DeserializeObject<Artist>(jsonData);

            return artist;
        }

        public async Task<List<Playlist>> GetPlaylist(ApiService api, string artist_slug)
        {

            string jsonData = await api.GetJson($"artist/{artist_slug}/playlists");

            List<Playlist> playlists = JsonConvert.DeserializeObject<List<Playlist>>(jsonData);

            return playlists;
        }

    }
}
