using System;

namespace Vector
{
    public class Vector
    {
        public double X { get; private set; }

        public double Y { get; private set; }

        public double Z { get; private set; }

        public double Length
        {
            get
            {
                return Math.Sqrt(X * X + Y * Y + Z * Z);
            }
        }

        public Vector(double coordinateX, double coordinateY, double coordinateZ)
        {
            X = coordinateX;
            Y = coordinateY;
            Z = coordinateZ;
        }

        public static double ScalarProduct(Vector firstVector, Vector secondVector)
        {
            return firstVector.X * secondVector.X + firstVector.Y * secondVector.Y + firstVector.Z * secondVector.Z;
        }

        public static double ScalarProduct(Vector firstVector, Vector secondVector, double angleBetweenInDegrees)
        {
            return firstVector.Length * secondVector.Length * Math.Cos(angleBetweenInDegrees);
        }

        public static double MixedProduct(Vector firstVector, Vector secondVector, Vector thirdVector)
        {
            return ( firstVector.X * secondVector.Y * thirdVector.Z +
                     firstVector.Y * secondVector.Z * thirdVector.X +
                     firstVector.Z * secondVector.X * thirdVector.Y) -
                   ( firstVector.Z * secondVector.Y * thirdVector.X +
                     firstVector.X * secondVector.Z * thirdVector.Y +
                     firstVector.Y * secondVector.X * thirdVector.Z);
        }

        public static bool operator ==(Vector vector1, Vector vector2)
        {
            return vector1.X == vector2.X &&
                   vector1.Y == vector2.Y &&
                   vector1.Z == vector2.Z;
        }
        
        public static bool operator !=(Vector vector1, Vector vector2)
        {
            return !(vector1.X == vector2.X &&
                     vector1.Y == vector2.Y &&
                     vector1.Z == vector2.Z);
        }

        public static Vector operator +(Vector vector1, Vector vector2)
        {
            return new Vector(
                vector1.X + vector2.X,
                vector1.Y + vector2.Y,
                vector1.Z + vector2.Z);
        }

        public static Vector operator -(Vector vector1, Vector vector2)
        {
            return new Vector(
                vector1.X - vector2.X,
                vector1.Y - vector2.Y,
                vector1.Z - vector2.Z);
        }
        
        public static Vector operator *(Vector vector1, Vector vector2)
        {
            var resultX = vector1.Y * vector2.Z - vector2.Y * vector1.Z;
            var resultY = (-1) * (vector1.X * vector2.Z - vector2.X * vector1.Z);
            var resultZ = vector1.X * vector2.Y - vector2.X * vector1.Y;
            return new Vector(resultX, resultY, resultZ);
        }

        public static Vector operator *(Vector vector1, double number)
        {
            return new Vector(
                vector1.X * number,
                vector1.Y * number,
                vector1.Z * number);
        }

        public override bool Equals(object obj)
        {
            return obj is Vector vector &&
                   X == vector.X &&
                   Y == vector.Y &&
                   Z == vector.Z;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }
}