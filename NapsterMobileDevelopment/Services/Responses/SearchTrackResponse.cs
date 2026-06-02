using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NapsterMobileDevelopment.Services.Responses
{
    public class SearchTrackResponse
    {

        [JsonProperty("releases")]
        public TrackApiResponse[] Tracks { get; set; }

    }
}
