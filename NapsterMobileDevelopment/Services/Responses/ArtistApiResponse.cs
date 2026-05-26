using NapsterMobileDevelopment.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NapsterMobileDevelopment.Services.Responses
{
    public class ArtistApiResponse
    {

        public class AlbumInfo()
        {

            [JsonProperty("primary-type-id")]
            public string ID { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("primary-type")]
            public string Type { get; set; }
        }

        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("releases")]
        public List<TrackApiResponse> Tracks { get; set; }

        [JsonProperty("release-groups")]
        public List<AlbumInfo> Albums { get; set; }


    }
}
