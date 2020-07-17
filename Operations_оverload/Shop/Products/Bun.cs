using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Products
{
    /// <summary>
    /// Represents an instance of the bun.
    /// </summary>
    public class Bun : Product
    {
        /// <summary>
        /// Creates an instance of the Bun class.
        /// </summary>
        /// <param name="name"> Name of the bun.</param>
        /// <param name="price"> Price of the bun</param>
        /// <param name="type"> Type of bun.</param>
        public Bun(string name, double price, string type)
        {
            Name = name;
            Price = price;
            Type = type;
        }

        /// <summary>
        /// Sums two object of Bun class.
        /// </summary>
        /// <param name="bun1"> The first object of bun for the sum.</param>
        /// <param name="bun2"> The second object of bun for the sum</param>
        /// <returns> The sum of two objects of the Bun class.</returns>
        public static Bun operator +(Bun bun1, Bun bun2)
        {
            return new Bun(
                bun1.Name + " - " + bun2.Name,
                (bun1.Price + bun2.Price) / 2,
                bun1.Type + " - " + bun2.Type);
        }

        /// <summary>
        /// Converts an object of the Bun class to an object of the Soda class.
        /// </summary>
        /// <param name="bun"> Object of Sode class for the conversion.</param>
        public static explicit operator Soda(Bun bun)
        {
            return new Soda(bun.Name, bun.Price, bun.Type);
        }

        /// <summary>
        /// Converts an object of the Bun class to an object of the Vegetable class.
        /// </summary>
        /// <param name="bun"> Object of Soda class for the conversion.</param>
        public static explicit operator Vegetable(Bun bun)
        {
            return new Vegetable(bun.Name, bun.Price, bun.Type);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is Bun bun &&
                Name == bun.Name &&
                Price == bun.Price &&
                Type == bun.Type;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Price);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Bun: Name: {Name}, Price: {Price}, Type: {Type}";
        }
    }
}
