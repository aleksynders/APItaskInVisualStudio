using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiGromov.Models
{
    public class ModelShop
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public string Image { get; set; }

        public ModelShop(Shop shop)
        {
            ID = shop.ID;
            Title = shop.Title;
            Price = shop.Price;
            Count = shop.Count;
            Image = shop.Image;
        }
    }
}