using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using NapsterMobileDevelopment.Services.Responses;
using Newtonsoft.Json;

namespace NapsterMobileDevelopment.Services.Responses
{
    public class ArtistAlbumResponse
    {

        [JsonProperty("releases")]
        public Release[] Releases { get; set; }

        public class Release
        {
            [JsonProperty("id")]
            public string ID { get; set; }
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("cover-art-archive")]
            public AlbumApiResponse.CoverArtArchive? CoverArtArchive { get; set; } = null;
        }


    }
}

