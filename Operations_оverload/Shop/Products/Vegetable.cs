using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Products
{
    /// <summary>
    /// Represents an instance of the vegetable.
    /// </summary>
    public class Vegetable : Product
    {
        /// <summary>
        /// Creates an instance of the Vegetable class.
        /// </summary>
        /// <param name="name"> Name of the soda.</param>
        /// <param name="price"> Price of the soda</param>
        /// <param name="type"> Type of soda.</param>
        public Vegetable(string name, double price, string type)
        {
            Name = name;
            Price = price;
            Type = type;
        }

        /// <summary>
        ///Sums two object of Vegetable class.
        /// </summary>
        /// <param name="vegetable1"> The first object of Vegetable for the sum.</param>
        /// <param name="vegetable2"> The second object of Vegetable for the sum.</param>
        /// <returns> The sum of two objects of the Vegetable class.</returns>
        public static Vegetable operator +(Vegetable vegetable1, Vegetable vegetable2)
        {
            return new Vegetable(
                vegetable1.Name + " - " + vegetable2.Name,
                (vegetable1.Price + vegetable2.Price) / 2,
                vegetable1.Type + " - " + vegetable2.Type);
        }

        /// <summary>
        /// Converts an object of the Vegetable class to an object of the Soda class.
        /// </summary>
        /// <param name="vegetable"> Object of Vegetable class for the conversion.</param>
        public static explicit operator Soda(Vegetable vegetable)
        {
            return new Soda(vegetable.Name, vegetable.Price, vegetable.Type);
        }

        /// <summary>
        /// Converts an object of the Vegetable class to an object of the Bun class.
        /// </summary>
        /// <param name="vegetable"> Object of Vegetable class for the conversion.</param>
        public static explicit operator Bun(Vegetable vegetable)
        {
            return new Bun(vegetable.Name, vegetable.Price, vegetable.Type);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is Vegetable vegetable &&
                Name == vegetable.Name &&
                Price == vegetable.Price &&
                Type == vegetable.Type;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Price);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Vegetable: Name: {Name}, Price: {Price}, Type: {Type}";
        }
    }
}
