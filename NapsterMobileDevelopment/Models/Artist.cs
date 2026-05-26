using NapsterMobileDevelopment.Services;
using NapsterMobileDevelopment.Services.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;
using static NapsterMobileDevelopment.Services.Responses.ArtistApiResponse;

namespace NapsterMobileDevelopment.Models
{


    public class Artist
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        //[JsonProperty("image")]
        //string Image {  get; set; }
        //  [JsonProperty("bio")]
        //string Bio { get; set; }
        //[JsonProperty("url-slug")]
        // string URLSlug { get; set; }
        public List<Track> Tracks { get; set; } = [];
        public List<Track> HalfTracks { get; set; } = [];

        public List<Album> HalfAlbums { get; set; } = [];
        public List<Album> Albums { get; set; } = [];


        public Artist(ArtistApiResponse apiResponse)
        {
            if (apiResponse == null) return;

            Name = apiResponse.Name;
            ID = apiResponse.ID;

            apiResponse.Tracks.Sort((a, b) => (a.Status != "Official").CompareTo(b.Status != "Official"));

            List<string> albumNames = [];
            foreach (AlbumInfo album in apiResponse.Albums)
            {
                if (Albums.Count < 10 && !albumNames.Contains(album.Title))
                {

                    Album newAlbum = new Album(null, null);
                    newAlbum.Title = album.Title;
                    newAlbum.ID = album.ID;

                    albumNames.Add(newAlbum.Title);;
                    Albums.Add(newAlbum);

                    if (HalfAlbums.Count < 3)
                    {
                        HalfAlbums.Add(newAlbum);
                    }

                }
            }

            List<string> trackNames = [];
            foreach (TrackApiResponse track in apiResponse.Tracks)
            {
                if (Tracks.Count < 10 && !trackNames.Contains(track.Title))
                {
                    trackNames.Add(track.Title);
                    Track new_track = new Track(track);
                    Tracks.Add(new_track);

                    if (HalfTracks.Count < 5)
                    {
                        HalfTracks.Add(new_track);
                    }

                }
            }
        }


    }
}
