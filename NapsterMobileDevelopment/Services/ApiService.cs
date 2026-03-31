using System;
using System.Collections.Generic;
using System.Text;
using NapsterMobileDevelopment.Models;
using Newtonsoft.Json;

namespace NapsterMobileDevelopment.Services
{

    public class ApiService
    {
        const string baseURL = "https://api.audiomack.com/v1";
        HttpClient _client = new HttpClient();

        public async Task<string> GetJson(string url)
        {
            string requestUrl = $"{baseURL}{url}";
            HttpRequestMessage request = new(HttpMethod.Get, requestUrl);
            //request.Headers.Add("apiKey", "asuasfaadasd");
            HttpResponseMessage response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to fetch Api response for query: {url}. Server responded: {response.StatusCode}");
            }

            return await response.Content.ReadAsStringAsync();


            
        }
        public async Task<Artist> GetArtist(string artistURL)
        {
            string jsonData = await GetJson(artistURL);

            Artist artist = JsonConvert.DeserializeObject<Artist>(jsonData);

            return artist;
        }

    }
}
