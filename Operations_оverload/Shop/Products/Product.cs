using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Products
{
    public abstract class Product
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string Type { get; set; }

        public static explicit operator int(Product product)
        {
            return (int)product.Price;
        }

        public static explicit operator double(Product product)
        {
            return product.Price;
        }
    }
}
