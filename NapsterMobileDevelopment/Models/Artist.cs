using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using NapsterMobileDevelopment.Services;
using Newtonsoft.Json;

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



    }
    
}
