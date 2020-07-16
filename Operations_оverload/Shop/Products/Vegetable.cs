using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Products
{
    public class Vegetable : Product
    {
        public Vegetable(string name, double price, string type)
        {
            Name = name;
            Price = price;
            Type = type;
        }

        public static Vegetable operator +(Vegetable vegetable1, Vegetable vegetable2)
        {
            return new Vegetable(
                vegetable1.Name + " - " + vegetable2.Name,
                (vegetable1.Price + vegetable2.Price) / 2,
                vegetable1.Type + " - " + vegetable2.Type);
        }

        public static explicit operator Soda(Vegetable vegetable)
        {
            return new Soda(vegetable.Name, vegetable.Price, vegetable.Type);
        }

        public static explicit operator Bun(Vegetable vegetable)
        {
            return new Bun(vegetable.Name, vegetable.Price, vegetable.Type);
        }

        public override bool Equals(object obj)
        {
            return obj is Vegetable vegetable &&
                Name == vegetable.Name &&
                Price == vegetable.Price &&
                Type == vegetable.Type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Price);
        }

        public override string ToString()
        {
            return $"Vegetable: Name: {Name}, Price: {Price}, Type: {Type}";
        }
    }
}
