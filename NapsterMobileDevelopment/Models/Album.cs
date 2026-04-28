
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using System.Linq.Expressions;


namespace NapsterMobileDevelopment.Models
{

    public class CoverArt { }
    public class Album
    {
        public Artist Artist { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("cover-art-archive")]
        public Dictionary<string, bool> CoverArt { get; set; }


    }
}
