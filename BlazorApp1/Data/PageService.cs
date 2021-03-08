using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
namespace BlazorApp1.Data
{
    public class PageService
    {
        private IWebHostEnvironment _WebHostEnvironment;
        private string wwwroot;
        private string jsonString;

       private List<IdName> pageList;
        public PageService(IWebHostEnvironment WebEnvironment)
        {
            _WebHostEnvironment = WebEnvironment;
            wwwroot = _WebHostEnvironment.WebRootPath;



            var file = wwwroot + "/json/pagesList.json";

             jsonString = File.ReadAllText(file);
            pageList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<IdName>>(jsonString);





        }


        public string GetPageTitle(string id)
        {
            string result = id;
            var pageList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<IdName>>(jsonString);
            var x2 = pageList.Where(o => o.Id == id).FirstOrDefault();
            if (x2 != null)
            {
                result = x2.Name;
            }
            return result;
        }


        public List<IdName> GetPageList(string cat)
        {
            return pageList.Where(o => o.Id.StartsWith(cat)).OrderBy(o => o.Id).ToList();
          
        }
        public List<IdName> GetPageList()
        {
            return  pageList.OrderBy(o => o.Id).ToList();
        
        }


        public IEnumerable<IdName> GetEntireList()
        {
            return pageList;
        }
       

       

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray());
        }
    }
}
