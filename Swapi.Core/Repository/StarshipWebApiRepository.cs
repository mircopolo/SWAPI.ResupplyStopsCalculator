using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swapi.Core.Model;
using Swapi.Core.Model.Serialization;

namespace Swapi.Core.Repository
{
    /// <summary>
    /// Repository that returns details of Starships sourced from the SWAPI service.
    /// </summary>
    public class StarshipWebApiRepository : IStarshipRepository
    {
        private ILogger _logger;

        private HttpClient _client;

        public StarshipWebApiRepository(ILogger logger, IConfiguration config)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(config["apiurl"]);
            _logger = logger;
        }
        /// <summary>
        /// Get all Starships in repository.
        /// </summary>
        /// <returns>
        /// A Enumeration of starships.
        /// </returns>
        public IEnumerable<Starship> GetAllStarships()
        {
            string nextUrl = "starships";
            bool hasNext = true;
            do
            {
                var jsonResponse = GetPage(nextUrl);
                var results = jsonResponse["results"];
             
                foreach (var shipToken in results.Children())
                {
                    Starship ship = null;
                    try
                    {
                         ship = JsonConvert.DeserializeObject<Starship>(shipToken.ToString(), new StarshipConvertor());
                    }
                    catch (Exception ex)
                    {
                        //Log and try carry on
                        _logger.LogError("Error getting data for ship",ex);
                        continue;
                    }
                    yield return ship;

                }

                var nextPage = jsonResponse["next"];
                if (nextPage != null && nextPage.Type == JTokenType.String)
                {
                    nextUrl = nextPage.Value<string>();
                }
                else
                {
                    hasNext = false;
                }


            } while (hasNext);
        }

        private JObject GetPage(string url)
        {
            string responseString = _client.GetStringAsync(url).Result;
            return JObject.Parse(responseString);
        }

    }
}
