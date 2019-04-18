using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.ServiceLocation;

namespace MadeToEngageTest.Business.Services
{
    [ServiceConfiguration(ServiceType = typeof(IProductService))]
    public class ProductService : IProductService
    {
        public bool GetProductBySku(int productSku, out Product product)
        {
            product = null;
            return false;
        }
    }

    public class Product
    {
        public decimal Price { get; set; }
        public decimal? MaxDiscount { get; set; }
        public bool OnlineDiscount { get; set; }
    }
}