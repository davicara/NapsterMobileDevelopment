using NapsterMobileDevelopment.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using static NapsterMobileDevelopment.Services.Responses.AlbumApiResponse;

namespace NapsterMobileDevelopment.Services.Responses
{
    public class TrackApiResponse
    {

        public class Credit
        {
            [JsonProperty("joinphrase")]
            public string JoinPhrase { get; set; }
            [JsonProperty("artist")]
            public Artist Artist { get; set; }
        }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("length")]
        public int Length { get; set; }

        [JsonProperty("artist-credit")]
        public List<Credit> Credits { get; set; }

    }
}
