using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Products
{
    /// <summary>
    /// Represents an instance of the product.
    /// </summary>
    public abstract class Product
    {
        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the product price.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the product type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Converts a product to a number.
        /// </summary>
        /// <param name="product"> Product for conversion.</param>
        public static explicit operator int(Product product)
        {
            return (int)product.Price;
        }

        /// <summary>
        /// Converts a product to a floating-point number.
        /// </summary>
        /// <param name="product"> Product for conversion.</param>
        public static explicit operator double(Product product)
        {
            return product.Price;
        }
    }
}
