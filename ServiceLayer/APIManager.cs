using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using DataLayer;



namespace ServiceLayer
{
    public class APIManager
    {

        private List<Region> lstRegion = new List<Region>();
        private const string RegionURL = "http://dataservice.accuweather.com/locations/v1/regions?apikey=SKVmUv9OAhr6a7rvSy1sXiaEYC2gRYeO";

        public HttpResponseMessage AuthenicateandGetClient(string url)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);

            //string user = "vsreenivasaiah", password = "Rajkumar@143";
            string user = "sumith", password = "India@143";
            string userAndPasswordToken =
            Convert.ToBase64String(Encoding.UTF8.GetBytes(user + ":" + password));
            var authenticationBytes = Encoding.ASCII.GetBytes(userAndPasswordToken);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(authenticationBytes));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(url).Result;
            return response;
        }


        public List<Region> loadRegions()
        {
            HttpResponseMessage response = AuthenicateandGetClient(RegionURL);
            using (HttpContent content = response.Content)
            {

                Task<string> result = content.ReadAsStringAsync();
                string res = result.Result;
                JavaScriptSerializer javascriptserialilzer = new JavaScriptSerializer();
                lstRegion = (List<Region>)javascriptserialilzer.Deserialize(res, typeof(List<Region>));
            }

            return lstRegion;
        }

        public List<Country> GetAllCountrie(string strRegionId)
        {
            String strCountryURL = "http://dataservice.accuweather.com/locations/v1/countries/" + strRegionId + "?apikey=SKVmUv9OAhr6a7rvSy1sXiaEYC2gRYeO";
            //String strCountryURL = "http://dataservice.accuweather.com/locations/v1/countries/{ASI}?apikey=SKVmUv9OAhr6a7rvSy1sXiaEYC2gRYeO";

            List<Country> lstCountry = new List<Country>();
            HttpResponseMessage response = AuthenicateandGetClient(strCountryURL);
            using (HttpContent content = response.Content)
            {

                Task<string> result = content.ReadAsStringAsync();
                string res = result.Result;
                JavaScriptSerializer javascriptserialilzer = new JavaScriptSerializer();
                lstCountry = (List<Country>)javascriptserialilzer.Deserialize(res, typeof(List<Country>));
            }
            return lstCountry;
        }

        public Weather GetWeather(int StrloctionKey)
        {
            String StrLocation = "http://dataservice.accuweather.com/currentconditions/v1/" + StrloctionKey + "?apikey=SKVmUv9OAhr6a7rvSy1sXiaEYC2gRYeO";
            List<Weather> WeatherReport = new List<Weather>();
            HttpResponseMessage response = AuthenicateandGetClient(StrLocation);
            using (HttpContent content = response.Content)
            {

                Task<string> result = content.ReadAsStringAsync();
                string res = result.Result;
                JavaScriptSerializer javascriptserialilzer = new JavaScriptSerializer();
                WeatherReport = (List<Weather>)javascriptserialilzer.Deserialize(res, typeof(List<Weather>));
                return WeatherReport[0];
            }

        }

       public List<City> GetAllcities(String strCountryId,String StrCity)
        {
            string strCitiesURL = "http://dataservice.accuweather.com/locations/v1/cities/autocomplete?apikey=SKVmUv9OAhr6a7rvSy1sXiaEYC2gRYeO&q=" + StrCity ;
            List<City> lstCity = new List<City>();
            HttpResponseMessage response = AuthenicateandGetClient(strCitiesURL);
            {
                using (HttpContent content = response.Content)
                {

                    Task<string> result = content.ReadAsStringAsync();
                    string res = result.Result;
                    JavaScriptSerializer javascriptserialilzer = new JavaScriptSerializer();
                    lstCity = (List<City>)javascriptserialilzer.Deserialize(res, typeof(List<City>));
                    //Remove all other countries
                    lstCity = lstCity.Where(e => e.country.ID == strCountryId).ToList();
                    
                }
                return lstCity;
            }

        }
        

    }
}
 