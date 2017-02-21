using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI;
using WebAPI.Controllers;
using MyWebAPI.Controllers;
using MyWebAPI.Models;

namespace WebAPI.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // 排列
            ValuesController controller = new ValuesController();

            // 操作
            IEnumerable<string> result = controller.Get();

            // 断言
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // 排列
            ValuesController controller = new ValuesController();

            // 操作
            string result = controller.Get(5);

            // 断言
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // 排列
            ValuesController controller = new ValuesController();

            // 操作
            controller.Post("value");

            // 断言
        }

        [TestMethod]
        public void Put()
        {
            // 排列
            ValuesController controller = new ValuesController();

            // 操作
            controller.Put(5, "value");

            // 断言
        }

        [TestMethod]
        public void Delete()
        {
            // 排列
            ValuesController controller = new ValuesController();

            // 操作
            controller.Delete(5);

            // 断言
        }


        [TestMethod]
        public void GetAllProducts()
        {
            ProductsController controller = new ProductsController();
            var data = controller.GetAllProducts();
        }


        [TestMethod]
        public void GetAllProductsByHttpClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:52181");
            // Add an Accept header for JSON format.
            // 为JSON格式添加一个Accept报头
            //并将Accept报头设置为“application/json”，这是告诉服务器，以JSON格式发送数据。
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/Products/GetAllProducts").Result;

            response.EnsureSuccessStatusCode();    // Throw if not a success code. 

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                // 解析响应体。阻塞！
                var products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            }
            else
            {

                int statusCode = (int)response.StatusCode;//错误码
                string msg = response.ReasonPhrase;//错误原因
            }

        }

        [TestMethod]
        public void GetProductById()
        {
            try
            {


                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:52181");
                // Add an Accept header for JSON format.
                // 为JSON格式添加一个Accept报头
                //并将Accept报头设置为“application/json”，这是告诉服务器，以JSON格式发送数据。
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("api/Products/GetProductById/1").Result;
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    // 解析响应体。阻塞！
                    var products = response.Content.ReadAsAsync<Product>().Result;
                }
                else
                {

                    int statusCode = (int)response.StatusCode;//错误码
                    string msg = response.ReasonPhrase;//错误原因
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        /// <summary>
        /// 异步
        /// </summary>
        [TestMethod]
        public  void GetProductByIdWithAsync()
        {
            GetProductByIdWithAsync2();

        }


        public async void GetProductByIdWithAsync2()
        {
            try
            {


                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:52181");
                // Add an Accept header for JSON format.
                // 为JSON格式添加一个Accept报头
                //并将Accept报头设置为“application/json”，这是告诉服务器，以JSON格式发送数据。
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("api/Products/GetProductById/1");

                response.EnsureSuccessStatusCode();// Throw on error code（有错误码时报出异常）.

                var product = await response.Content.ReadAsAsync<Product>();
                Console.WriteLine(product);

            }
            catch (Exception ex)
            {

                throw;
            }

        }


        [TestMethod]
        public void PostProduct()
        {

            Product product = new Product { Id = 5, Name = "面包" };


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:52181");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsJsonAsync("api/Products/PostProduct", product).Result;

            if (response.IsSuccessStatusCode)
            {

            }
            else
            {

                int statusCode = (int)response.StatusCode;//错误码
                string msg = response.ReasonPhrase;//错误原因
            }

        }



        [TestMethod]
        public void PostProduct2()
        {
            Product product = new Product { Id = 5, Name = "面包" };


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:52181");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsJsonAsync("api/Products/PostProduct2", product).Result;

            if (response.IsSuccessStatusCode)
            {

            }
            else
            {

                int statusCode = (int)response.StatusCode;//错误码
                string msg = response.ReasonPhrase;//错误原因
            }

        }
    }
}
