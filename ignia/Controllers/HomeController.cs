using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using ServiceLayer;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace ignia.Controllers
{
    public class HomeController : Controller
    {

        
        public ActionResult Index()
        {
           List<Region> lstRegion = new List<Region>();                     
            APIManager apiManager = new APIManager();
            lstRegion= apiManager.loadRegions();
            ViewBag.Title = "Home Page";
            return View(lstRegion);
        }

        public ActionResult Country(string Id)
        {
            List<Country> lstCountry = new List<Country>();
            APIManager apiManager = new APIManager();
            lstCountry = apiManager.GetAllCountrie(Id);
            return View(lstCountry);
        }

        public ActionResult GetCityname(Country country)
        {
            
            return View(country);
        }

        public ActionResult Preference()
        {
            int key;
            string strCityName;
            CityPreference ctx = new CityPreference();
            string sqlString = "SELECT VALUE ct FROM CityPreference.cityDetails " + "AS ct";
            var objctx = (ctx as IObjectContextAdapter).ObjectContext;
            ObjectQuery<cityDetail> city = objctx.CreateQuery<cityDetail>(sqlString);
            cityDetail citypre = city.First<cityDetail>();
            key = citypre.Citykey;
            strCityName = citypre.CityNAME;
            return RedirectToAction("CitySelected", new { @key = key, @strCityName = strCityName });
                        
        }
                            
        public ActionResult SetPreference(int key, string strCityName)
        {
            cityDetail cityDetail = new cityDetail();
            CityPreference ctx = new CityPreference();

            var itemToRemove = ctx.cityDetails.SingleOrDefault(x => x.Citykey == key); //returns a single item.

            if (itemToRemove != null)
            {
                ctx.cityDetails.Remove(itemToRemove);
                ctx.SaveChanges();
            }
            cityDetail.CityNAME = strCityName;
            cityDetail.Citykey = key;

            //Call the function to save the 
            CityPreference dbcontext = new CityPreference();
            
            dbcontext.SaveChanges();
            dbcontext.cityDetails.Add(cityDetail);
            dbcontext.SaveChanges();
            return RedirectToAction("Index");
            //return View("Index");

        }
        public ActionResult City(Country commonattributes)
        {
            
            List<City> lstCity = new List<City>();
            APIManager apimanager = new APIManager();
            string StrCountryId = commonattributes.ID;
            string Name = commonattributes.LocalizedName;            
            
            lstCity = apimanager.GetAllcities(StrCountryId, Name);
            return View(lstCity);
        }

        public ActionResult CitySelected(int key, string strCityName)
        {
            Weather weatherReport = new Weather();
            APIManager apiManager = new APIManager();
            weatherReport = apiManager.GetWeather(key);
            weatherReport.Key = key;
            weatherReport.LocalizedName = strCityName;
            List<Weather> lstWeather = new List<Weather>();
            lstWeather.Add(weatherReport);
            return View(lstWeather);

        }
    }
}
