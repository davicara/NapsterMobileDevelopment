using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using NapsterMobileDevelopment.Services.Responses;
using Newtonsoft.Json;

namespace NapsterMobileDevelopment.Models
{
    public class Track
    {


        public string Title { get; set; }

        public int Length { get; set; }

        public List<Artist> Credits { get; set; } = new List<Artist>();

        public string StringCredits { get; set; }

        public Track(TrackApiResponse apiData)
        {
            
            Title = apiData.Title;
            Length = apiData.Length;

            string credits = string.Empty;
            foreach (var credit in apiData.Credits)
            {
                Credits.Add(credit.Artist);
                credits +=credit.Artist.Name+credit.JoinPhrase;
            }

            StringCredits = credits;
        }
    }
}
