
using NapsterMobileDevelopment.Models;
using NapsterMobileDevelopment.Services.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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

        public async Task<string> GetJson(string url, bool useBase = true)
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
                throw new HttpRequestException($"Failed to fetch Api response for query: {requestUrl}. Server responded: {response.StatusCode}");
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

        public async Task<Artist> GetArtist(string artistURL, bool getRelease = false)
        {

            string jsonData = await GetJson($"artist/{artistURL}?inc=releases+artist-credits&");


            ArtistApiResponse artistResponse = JsonConvert.DeserializeObject<ArtistApiResponse>(jsonData);

            Artist artist = new Artist(artistResponse);

            if (getRelease == true)
            {
                artist = await GetArtistReleases(artist);
            }


            return artist;
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
                catch (Exception ex)
                {
                    //debug log something

                }

            }

            return new Album(albumResponse, null);

        }

        public async Task<Artist> GetArtistReleases(Artist artist)
        {
            string jsonData = await GetJson($"release?artist={artist.ID}&type=album&", true);

            ArtistAlbumResponse albumResponse = JsonConvert.DeserializeObject<ArtistAlbumResponse>(jsonData);

            artist.AddAlbums(albumResponse);

            return artist;
        }

        public async Task<List<Track>> SearchTracks(string search)
        {

            string jsonData = await GetJson($"release?query=type:single%20AND%20{search}&", true);

            List<Track> tracks = new List<Track>();



            SearchTrackResponse response = JsonConvert.DeserializeObject<SearchTrackResponse>(jsonData);

            foreach (TrackApiResponse track in response.Tracks)
            {
                Track newTrack = new Track(track);
                tracks.Add(newTrack);
            }

            return tracks;
        }

        public async Task<List<Album>> SearchAlbums(string search)
        {

            string jsonData = await GetJson($"release?query=type:album%20AND%20{search}&", true);

            List<Album> albums = new List<Album>();

            List<AlbumApiResponse> response = JsonConvert.DeserializeObject<List<AlbumApiResponse>>(jsonData);

            foreach (AlbumApiResponse album in response)
            {

                Album newAlbum = new Album(album, null);
                albums.Add(newAlbum);
            }

            return albums;
        }

        public async Task<List<Artist>> SearchArtists(string search)
        {

            string jsonData = await GetJson($"artist?query={search}&", true);

            List<Artist> artists = new List<Artist>();

            List<ArtistApiResponse> response = JsonConvert.DeserializeObject<List<ArtistApiResponse>>(jsonData);

            foreach (ArtistApiResponse artist in response)
            {
                Artist newArtist = new Artist(artist);
                artists.Add(newArtist);
            }

            return artists;
        }


        //public async Task<object?> Search(ApiService api, string url, ref)
        //{
        //    string jsonData = await api.GetJson(url);



        //}

    }
}
