using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NapsterMobileDevelopment.Services.Responses
{
    public class SearchResponse<T>
    {

        [JsonProperty("releases")]
        public T[] releases { get; set; }


        [JsonProperty("artists")]
        public ArtistApiResponse[] Artists { get; set; }

        
    }
}
