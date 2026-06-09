
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
                //throw new HttpRequestException($"Failed to fetch Api response for query: {url}. Server responded: {response.StatusCode}");
                return "";
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

        public async Task<Track> GetTrack(string trackURL)
        {

            string jsonData = await GetJson($"release/{trackURL}?inc=artist-credits&");


            TrackApiResponse trackResponse = JsonConvert.DeserializeObject<TrackApiResponse>(jsonData);

            Track track = new Track(trackResponse);

            track.Image = await this.GetTrackImage(track);

            return track;
        }

        public async Task<string> GetTrackImage(Track track)
        {
            string image = await GetURL($"https://coverartarchive.org/release/{track.ID}/front");

            if (image != null)
            {
                track.Image = image;
            }

            return image;

        }

        public async Task<string> GetAlbumImage(Album album)
        {
            string image = await GetURL($"https://coverartarchive.org/release/{album.ID}/front");

            if (image != null)
            {
                album.CoverArt = image;
            }

            return image;

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

            string jsonData = await GetJson($"release?limit=10&query=type:single%20AND%20{search}&", true);

            List<Track> tracks = new List<Track>();



            SearchResponse<TrackApiResponse> response = JsonConvert.DeserializeObject<SearchResponse<TrackApiResponse>>(jsonData);

            foreach (TrackApiResponse track in response.releases)
            {
                Track newTrack = new Track(track);
                tracks.Add(newTrack);
            }

            return tracks;
        }

        public async Task<List<Album>> SearchAlbums(string search)
        {

            string jsonData = await GetJson($"release?limit=10&query=type:album%20AND%20{search}&", true);

            List<Album> albums = new List<Album>();

            SearchResponse<AlbumApiResponse> response = JsonConvert.DeserializeObject<SearchResponse<AlbumApiResponse>>(jsonData);

            foreach (AlbumApiResponse album in response.releases)
            {

                Album newAlbum = new Album(album, null);
                albums.Add(newAlbum);
            }

            return albums;
        }

        public async Task<List<Artist>> SearchArtists(string search)
        {

            string jsonData = await GetJson($"artist?limit=10&query={search}&", true);

            List<Artist> artists = new List<Artist>();

            SearchResponse<TrackApiResponse> response = JsonConvert.DeserializeObject<SearchResponse<TrackApiResponse>>(jsonData);

            foreach (ArtistApiResponse artist in response.Artists)
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
