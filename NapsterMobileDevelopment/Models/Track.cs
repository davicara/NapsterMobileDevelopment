using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;

namespace NapsterMobileDevelopment.Models
{
    public class Track
    {

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("length")]
        public int Length { get; set; }
        

    }
}
