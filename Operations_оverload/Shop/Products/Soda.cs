using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Products
{
    /// <summary>
    /// Represents an instance of the soda.
    /// </summary>
    public class Soda : Product
    {
        /// <summary>
        /// Creates an instance of the Soda class.
        /// </summary>
        /// <param name="name"> Name of the soda.</param>
        /// <param name="price"> Price of the soda</param>
        /// <param name="type"> Type of soda.</param>
        public Soda(string name, double price, string type)
        {
            Name = name;
            Price = price;
            Type = type;
        }

        /// <summary>
        /// Sums two object of Soda class.
        /// </summary>
        /// <param name="soda1"> The first object of soda for the sum.</param>
        /// <param name="soda2"> The second object of soda for the sum.</param>
        /// <returns> The sum of two objects of the Soda class.</returns>
        public static Soda operator +(Soda soda1, Soda soda2)
        {
            return new Soda(
                soda1.Name + " - " + soda2.Name,
                (soda1.Price + soda2.Price) / 2,
                soda1.Type + " - " + soda2.Type);
        }

        /// <summary>
        /// Converts an object of the Soda class to an object of the Bun class.
        /// </summary>
        /// <param name="soda"> Object of Soda class for the conversion.</param>
        public static explicit operator Bun(Soda soda)
        {
            return new Bun(soda.Name, soda.Price, soda.Type);
        }

        /// <summary>
        /// Converts an object of the Soda class to an object of the Vegetable class.
        /// </summary>
        /// <param name="soda"> Object of Soda class for the conversion.</param>
        public static explicit operator Vegetable(Soda soda)
        {
            return new Vegetable(soda.Name, soda.Price, soda.Type);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is Soda soda &&
                Name == soda.Name &&
                Price == soda.Price &&
                Type == soda.Type;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Price);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Soda: Name: {Name}, Price: {Price}, Type: {Type}";
        }
    }
}
