using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Products
{
    public class Soda : Product
    {
        public Soda(string name, double price, string type)
        {
            Name = name;
            Price = price;
            Type = type;
        }

        public static Soda operator +(Soda soda1, Soda soda2)
        {
            return new Soda(
                soda1.Name + " - " + soda2.Name,
                (soda1.Price + soda2.Price) / 2,
                soda1.Type + " - " + soda2.Type);
        }

        public static explicit operator Bun(Soda soda)
        {
            return new Bun(soda.Name, soda.Price, soda.Type);
        }

        public static explicit operator Vegetable(Soda soda)
        {
            return new Vegetable(soda.Name, soda.Price, soda.Type);
        }

        public override bool Equals(object obj)
        {
            return obj is Soda soda &&
                Name == soda.Name &&
                Price == soda.Price &&
                Type == soda.Type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Price);
        }

        public override string ToString()
        {
            return $"Soda: Name: {Name}, Price: {Price}, Type: {Type}";
        }
    }
}
