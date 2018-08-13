using System;
using System.Collections.Generic;
using System.Text;




namespace DataLayer
{
    public class Commonattributes
    {
        public string ID { get; set; }
        public string LocalizedName { get; set; }
        public String EnglishName { get; set; }
    }


    public class Region : Commonattributes
    {

    }
    public class Country : Commonattributes
    {

    }

    public class City
    {
        public int Key { get; set; }
        public String LocalizedName { get; set; }
        public Country country;

    }

    public class Measure
    {
        public string Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }
    public class Temperature
    {
        public Metric metric;
        public Imperial imperial;
    }
    public class Metric: Measure
    {

    }
    public class Imperial : Measure
    {

    }

    public class Weather 
    {
        public string WeatherText { get; set; }
        public Temperature temperature { get; set; }
        public int Key { get; set; }
        public String LocalizedName { get; set; }


    }
    
}
