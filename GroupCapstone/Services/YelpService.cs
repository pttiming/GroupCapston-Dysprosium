using GroupCapstone.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GroupCapstone.Services
{
    public class YelpService
    {
        public YelpService()
        {

        }
        public async Task<YelpBusinesses> GetBusinesses(string searchlocation, string searchtype)
        {
            YelpBusinesses yelpBusinesses = new YelpBusinesses() { Error = "API Error" };
            string url = $"https://api.yelp.com/v3/businesses/search?location={searchlocation}&term={searchtype}";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{API_KEYS.yelpApi}");
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                yelpBusinesses = JsonConvert.DeserializeObject<YelpBusinesses>(jsonResult);
                return yelpBusinesses;
            }
            yelpBusinesses = new YelpBusinesses() { Error = "API Error" };
            return yelpBusinesses;
            ;
        }
<<<<<<< HEAD
=======

        public async Task<YelpBusiness> GetBusiness(string businessId)
        {
            YelpBusiness yelpBusiness = new YelpBusiness() { Error = "API Error" };
            string url = $"https://api.yelp.com/v3/businesses/{businessId}";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{API_KEYS.yelpApi}");
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                yelpBusiness = JsonConvert.DeserializeObject<YelpBusiness>(jsonResult);
                return yelpBusiness;
            }
            yelpBusiness = new YelpBusiness() { Error = "API Error" };
            return yelpBusiness;
            ;
        }
>>>>>>> 8e102d87b8494a8cfffe9712f1647cd46a2f1df7
    }
}
