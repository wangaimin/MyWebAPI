using MyWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MyWebAPI.Controllers
{
    public class HomeController : Controller
    {
        Product[] products = new Product[] {
            new Product {  Id=1, Category="电瓶车", Name="迷你电瓶车", Price=2000},
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };

        // GET: Home
        public ActionResult Index()
        {
          


            return View();
        }

        public JsonResult GetAllProducts() {

            var data = products;
            return new JsonResult() { Data=data, JsonRequestBehavior=JsonRequestBehavior.AllowGet};
          //  return new JsonResult(new {data=data,success=true });
        }


        public JsonResult PostProduct(Product product)
        {

            //   products.Add(product);

            return new JsonResult() { Data = product, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }


        async void GetData()
        {

            string url = "http://localhost:52181/api/Products/GetAllProducts";
            var handler = new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.GZip };
            using (var http = new HttpClient(handler))
            {
                var response =await http.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string result=    await response.Content.ReadAsStringAsync();
               
            }
        }
    }
}