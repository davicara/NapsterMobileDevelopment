using NapsterMobileDevelopment.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NapsterMobileDevelopment.Services.Responses
{
    public class AlbumApiResponse
    {

        public class Medium
        {

            [JsonProperty("track-count")]
            public int TrackCount { get; set; }

            [JsonProperty("tracks")]
            public List<Track> Tracks { get; set; }


        }


        public class CoverArtArchive
        {
            [JsonProperty("artwork")]
            public bool Artwork { get; set; }

            [JsonProperty("front")]
            public bool Front {  get; set; }
        }

        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("media")]
        public List<Medium> Media { get; set; }

        [JsonProperty("cover-art-archive")]
        public CoverArtArchive CoverArt {  get; set; }



    }
}