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


        public string ID { get; set; }
        public string Title { get; set; }

        public List<Track> Tracks { get; set; } = new List<Track>();
        public string CoverArt {  get; set; }

        public string ArtistName { get; set; }

        public string ArtistID { get; set; }

        public Album(AlbumApiResponse apiResponse, string? image) 
        { 

            foreach (TrackApiResponse track in apiResponse.Media[0].Tracks)
            {
                Tracks.Add(new Track(track));
            }

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
