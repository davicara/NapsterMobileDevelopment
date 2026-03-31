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
        string ID { get; set; }
        [JsonProperty("name")]
        string Name { get; set; }
        [JsonProperty("image")]
        string Image {  get; set; }
        [JsonProperty("bio")]
        string Bio { get; set; }
        [JsonProperty("url-slug")]
        string URLSlug { get; set; }


        public async Task<List<Playlist>> GetPlaylists(ApiService api)
        {

            string jsonData = await api.GetJson($"/artist/({URLSlug}/playlists");

            List<Playlist> playlists = JsonConvert.DeserializeObject<List<Playlist>>(jsonData);

            return playlists;
        }
    }
    
}
