using System;

namespace Vector
{
    /// <summary>
    /// Represents a three-dimensional vector.
    /// </summary>
    public class Vector
    {
        /// <summary>
        /// Gets or sets x coordinate.
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        /// Gets or sets y coordinate.
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        /// Gets or sets z coordinate.
        /// </summary>
        public double Z { get; private set; }

        /// <summary>
        /// Gets the length of the vector.
        /// </summary>
        public double Length
        {
            get
            {
                return Math.Sqrt(X * X + Y * Y + Z * Z);
            }
        }
        
        /// <summary>
        /// Create instance of Class Vector.
        /// </summary>
        /// <param name="coordinateX"></param>
        /// <param name="coordinateY"></param>
        /// <param name="coordinateZ"></param>
        public Vector(double coordinateX, double coordinateY, double coordinateZ)
        {
            X = coordinateX;
            Y = coordinateY;
            Z = coordinateZ;
        }

        /// <summary>
        /// Calculates the skolar product of vectors.
        /// </summary>
        /// <param name="firstVector"> The first vector to calculate.</param>
        /// <param name="secondVector"> The second vector to calculate.</param>
        /// <returns> Result of a skolar product of two vectors.</returns>
        public static double ScalarProduct(Vector firstVector, Vector secondVector)
        {
            if (Equals(firstVector, null) || Equals(secondVector, null))
                throw new ArgumentNullException("Parameter can't be null");

            return firstVector.X * secondVector.X + firstVector.Y * secondVector.Y + firstVector.Z * secondVector.Z;
        }

        /// <summary>
        /// Calculates the skolar product of vectors and angle between.
        /// </summary>
        /// <param name="firstVector"> The first vector to calculate.</param>
        /// <param name="secondVector"> The second vector to calculate.</param>
        /// <param name="angleBetweenInDegrees"></param>
        /// <returns> Result of a skolar product of two vectors and angle between.</returns>
        public static double ScalarProduct(Vector firstVector, Vector secondVector, double angleBetweenInDegrees)
        {
            if (Equals(firstVector, null) || Equals(secondVector, null))
                throw new ArgumentNullException("Parameter can't be null");

            return firstVector.Length * secondVector.Length * Math.Cos(angleBetweenInDegrees);
        }

        /// <summary>
        /// Calculates the mixed product of three vectors.
        /// </summary>
        /// <param name="firstVector"> The first vector to calculate.</param>
        /// <param name="secondVector"> The second vector to calculate.</param>
        /// <param name="thirdVector"></param>
        /// <returns> Result of a mixed product of three vectors.</returns>
        public static double MixedProduct(Vector firstVector, Vector secondVector, Vector thirdVector)
        {
            if (Equals(firstVector, null) || Equals(secondVector, null) || Equals(thirdVector, null))
                throw new ArgumentNullException("Parameter can't be null");

            return ( firstVector.X * secondVector.Y * thirdVector.Z +
                     firstVector.Y * secondVector.Z * thirdVector.X +
                     firstVector.Z * secondVector.X * thirdVector.Y) -
                   ( firstVector.Z * secondVector.Y * thirdVector.X +
                     firstVector.X * secondVector.Z * thirdVector.Y +
                     firstVector.Y * secondVector.X * thirdVector.Z);
        }

        /// <summary>
        /// Checks objects for equality.
        /// </summary>
        /// <param name="vector1"> The first vector to calculate.</param>
        /// <param name="vector2"> The second vector to calculate.</param>
        /// <returns> True if vectors are equal, otherwise False.</returns>
        public static bool operator ==(Vector vector1, Vector vector2)
        {
            return vector1.X == vector2.X &&
                   vector1.Y == vector2.Y &&
                   vector1.Z == vector2.Z;
        }

        /// <summary>
        /// Checks vectors for inequality.
        /// </summary>
        /// <param name="vector1"> The first vector to calculate.</param>
        /// <param name="vector2"> The second vector to calculate.</param>
        /// <returns> False if vectors are equal, otherwise True.</returns>
        public static bool operator !=(Vector vector1, Vector vector2)
        {
            return !(vector1.X == vector2.X &&
                     vector1.Y == vector2.Y &&
                     vector1.Z == vector2.Z);
        }

        /// <summary>
        /// Sums two vectors.
        /// </summary>
        /// <param name="vector1"> The first vector to calculate.</param>
        /// <param name="vector2"> The second vector to calculate.</param>
        /// <returns> Result of the sum of two vectors.</returns>
        public static Vector operator +(Vector vector1, Vector vector2)
        {
            return new Vector(
                vector1.X + vector2.X,
                vector1.Y + vector2.Y,
                vector1.Z + vector2.Z);
        }

        /// <summary>
        /// Calculates the difference between two vectors.
        /// </summary>
        /// <param name="vector1"> The first vector to calculate.</param>
        /// <param name="vector2"> The second vector to calculate.</param>
        /// <returns> Result of the difference between two vectors.</returns>
        public static Vector operator -(Vector vector1, Vector vector2)
        {
            return new Vector(
                vector1.X - vector2.X,
                vector1.Y - vector2.Y,
                vector1.Z - vector2.Z);
        }

        /// <summary>
        /// Multiplies two vectors.
        /// </summary>
        /// <param name="vector1"> The first vector to calculate.</param>
        /// <param name="vector2"> The second vector to calculate.</param>
        /// <returns> The result of multiplying two vectors.</returns>
        public static Vector operator *(Vector vector1, Vector vector2)
        {
            var resultX = vector1.Y * vector2.Z - vector2.Y * vector1.Z;
            var resultY = (-1) * (vector1.X * vector2.Z - vector2.X * vector1.Z);
            var resultZ = vector1.X * vector2.Y - vector2.X * vector1.Y;
            return new Vector(resultX, resultY, resultZ);
        }

        /// <summary>
        /// Multiplies vectors and number.
        /// </summary>
        /// <param name="vector1"> The first vector to calculate.</param>
        /// <param name="number"> The number to multiply.</param>
        /// <returns> The result of multiplying a vector and a number.</returns>
        public static Vector operator *(Vector vector1, double number)
        {
            return new Vector(
                vector1.X * number,
                vector1.Y * number,
                vector1.Z * number);
        }

        ///<inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is Vector vector &&
                   X == vector.X &&
                   Y == vector.Y &&
                   Z == vector.Z;
        }

        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        ///<inheritdoc/>
        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }
}