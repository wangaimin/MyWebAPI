using MyWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyWebAPI.Controllers
{
    public class ProductsController : ApiController
    {
        List<Product> products = new List<Product>()
       {
            new Product {  Id=1, Category="电瓶车", Name="迷你电瓶车", Price=2000},
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };



        public IEnumerable<Product> GetAllProducts() {
            return products;
        }

        public Product GetProductById(int id) {

            var product = products.FirstOrDefault(m => m.Id.Equals(id));
            if (product==null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return product;
        }

        public IEnumerable<Product> GetProductsByCategory(string category) {
            return products.Where(m=>m.Category.Equals(category));
        }


       

        /// <summary>
        /// post请求创建商品
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public Product PostProduct(Product product) {

            products.Add(product);
            product.Id = 999;


            return product;
        }


        /// <summary>
        /// 对post请求修改了状态码为创建成功（201）
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public HttpResponseMessage PostProduct2(Product product)
        {

            products.Add(product);
            product.Id = 999;

            var response = Request.CreateResponse<Product>(HttpStatusCode.Created, product);
            string uri = Url.Link("Default",new {  id=999});
            response.Headers.Location =new Uri( uri);

            return response;
        }



        /// <summary>
        /// post请求创建多参数
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public Product PostProduct3(int id,string name)
        {

            Product product = new Product() {
                 Id=id,
                Name=name
            };


            return product;
        }



       
    }


}
