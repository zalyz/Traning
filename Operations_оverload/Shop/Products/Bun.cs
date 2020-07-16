using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Products
{
    public class Bun : Product
    {
        public Bun(string name, double price, string type)
        {
            Name = name;
            Price = price;
            Type = type;
        }

        public static Bun operator +(Bun bun1, Bun bun2)
        {
            return new Bun(
                bun1.Name + " - " + bun2.Name,
                (bun1.Price + bun2.Price) / 2,
                bun1.Type + " - " + bun2.Type);
        }

        public static explicit operator Soda(Bun bun)
        {
            return new Soda(bun.Name, bun.Price, bun.Type);
        }

        public static explicit operator Vegetable(Bun bun)
        {
            return new Vegetable(bun.Name, bun.Price, bun.Type);
        }

        public override bool Equals(object obj)
        {
            return obj is Bun bun &&
                Name == bun.Name &&
                Price == bun.Price &&
                Type == bun.Type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Price);
        }

        public override string ToString()
        {
            return $"Bun: Name: {Name}, Price: {Price}, Type: {Type}";
        }
    }
}
