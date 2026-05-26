using NapsterMobileDevelopment.Models;
using NapsterMobileDevelopment.Services.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
            _client.Timeout = new TimeSpan(0, 0, 10); // for testing of slow response from server

        }

        public async Task<string> GetJson(string url, bool useBase=true)
        {

            string requestUrl = url;
            if (useBase)
            {
                requestUrl = $"{baseURL}/{url}fmt=json";
            }

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
                throw new HttpRequestException($"Failed to fetch Api response for query: {baseURL}/{url}fmt=json. Server responded: {response.StatusCode}");
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();


            
        }

        public async Task<string> GetURL(string url)
        {

            string requestUrl = url;

            //HttpRequestMessage request = new(HttpMethod.Get, requestUrl);

            HttpResponseMessage? response = null;

            response = await _client.GetAsync(requestUrl);



            if (response != null && !response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to fetch Api response for query: {url}. Server responded: {response.StatusCode}");
            }

            //response!.EnsureSuccessStatusCode();

            return response!.RequestMessage?.RequestUri?.ToString() ?? "";
        }

        public async Task<Artist> GetArtist(string artistURL)
        {

            string jsonData = await GetJson($"artist/{artistURL}?inc=release-groups+releases+artist-credits&");


            ArtistApiResponse artistResponse = JsonConvert.DeserializeObject<ArtistApiResponse>(jsonData);


            return new Artist(artistResponse);
        }

        public async Task<Album> GetAlbum(string albumURL)
        {
            string jsonData = await GetJson($"release/{albumURL}?inc=artist-credits+recordings&", true);
           

            AlbumApiResponse albumResponse = JsonConvert.DeserializeObject<AlbumApiResponse>(jsonData);
            

            if (albumResponse.CoverArt.Front)
            {
                try
                {
                    string coverData = await GetURL($"https://coverartarchive.org/release/{albumURL}/front");
                    return new Album(albumResponse, coverData);

                }
                catch(Exception ex)
                {
                    //debug log something
                    
                }

            }

            return new Album(albumResponse, null);

        }


        public async Task<List<Playlist>> GetPlaylist(ApiService api, string artist_slug)
        {

            string jsonData = await api.GetJson($"artist/{artist_slug}/playlists");

            List<Playlist> playlists = JsonConvert.DeserializeObject<List<Playlist>>(jsonData);

            return playlists;
        }

    }
}
