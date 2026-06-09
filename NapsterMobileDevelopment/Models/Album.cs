using NapsterMobileDevelopment.Services;
using NapsterMobileDevelopment.Services.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;


namespace NapsterMobileDevelopment.Models
{

    public class Album
    {

        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("tracks")]
        public List<Track> Tracks { get; set; } = new List<Track>();
        public string CoverArt {  get; set; }

        public string ArtistName { get; set; }

        public string ArtistID { get; set; }

        public Album(AlbumApiResponse apiResponse, string? image) 
        {
            if (apiResponse == null) return;

            if(apiResponse.Media[0].Tracks != null) {

                foreach (TrackApiResponse track in apiResponse.Media[0].Tracks)
                {
                    Tracks.Add(new Track(track));
                }
            }
            ;

            ID = apiResponse.ID;
            Title = apiResponse.Title;
            ArtistName = apiResponse.ArtistCredits[0].Name;
            ArtistID = apiResponse.ArtistCredits[0].ArtistInfo.ID;

            if (image != null)
            {
                CoverArt = image;
            }
            else
            {
                CoverArt = "NapsterMobileDevelopment/Resources/Images/no_image.png";
            }
        }

    }
}
