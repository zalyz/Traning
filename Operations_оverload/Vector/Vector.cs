using System;

namespace Vector
{
    public class Vector
    {
        public double X { get; private set; }

        public double Y { get; private set; }

        public double Z { get; private set; }

        public Vector(double coordinateX, double coordinateY, double coordinateZ)
        {
            X = coordinateX;
            Y = coordinateY;
            Z = coordinateZ;
        }

        public void Multiply(int number)
        {

        }

        public static bool operator ==(Vector vector1, Vector vector2)
        {
            if (vector1.X == vector2.X && vector1.Y == vector2.Y && vector1.Z == vector2.z)
                return true;

            return false;
        }

        public static bool operator !=(Vector vector1, Vector vector2)
        {
            if (vector1.X == vector2.X && vector1.Y == vector2.Y && vector1.Z == vector2.z)
                return false;

            return true;
        }
    }
}
