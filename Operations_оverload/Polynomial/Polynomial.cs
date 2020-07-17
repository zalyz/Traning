using System;
using System.Linq;
using System.Text;

namespace Polynomial
{
    /// <summary>
    /// Represents the essence of a polynomial.
    /// </summary>
    public class Polynomial
    {
        /// <summary>
        /// Gets or sets the coefficients of the polynomial.
        /// </summary>
        public int[] Coefficient { get; private set; }

        /// <summary>
        /// Creates the entity of the polynomial class.
        /// </summary>
        /// <param name="correspondingСoefficient"> Сoefficient of the polynomial.</param>
        public Polynomial(params int[] correspondingСoefficient)
        {
            Coefficient = correspondingСoefficient;
        }

        /// <summary>
        /// Sums two polynomials.
        /// </summary>
        /// <param name="firstPolynomial"> The first polynomial for addition.</param>
        /// <param name="secondPolynomial"> The second polynomial for addition.</param>
        /// <returns> Result of adding two polynomials.</returns>
        public static Polynomial operator +(Polynomial firstPolynomial, Polynomial secondPolynomial)
        {
            int maxDergee = GetMaxDegree(firstPolynomial, secondPolynomial);
            int[] resultСoefficient = new int[maxDergee];
            for (int i = firstPolynomial.Coefficient.Length - 1, j = secondPolynomial.Coefficient.Length - 1, k = maxDergee - 1; i >= 0 || j >= 0; i--, j--, k--)
            {
                if (i >= 0 && j >= 0)
                {
                    resultСoefficient[k] = firstPolynomial.Coefficient[i] + secondPolynomial.Coefficient[j];
                }

                if (i == 0 && j < 0)
                {
                    resultСoefficient[k] = firstPolynomial.Coefficient[i];
                }

                if (i < 0 && j == 0)
                {
                    resultСoefficient[k] = secondPolynomial.Coefficient[j];
                }
            }

            return new Polynomial(resultСoefficient);
        }

        /// <summary>
        /// Sums a polynomial and a number.
        /// </summary>
        /// <param name="polynomial"> A polynomial for addition.</param>
        /// <param name="number"> A number for addition.</param>
        /// <returns> result of adding a polynomial and a number.</returns>
        public static Polynomial operator +(Polynomial polynomial, int number)
        {
            var resultСoefficient = (int[])polynomial.Coefficient.Clone();
            resultСoefficient[^1] += number;
            return new Polynomial(resultСoefficient);
        }

        /// <summary>
        /// Finds the difference between two polynomials.
        /// </summary>
        /// <param name="firstPolynomial"> The first polynomial for subtraction.</param>
        /// <param name="secondPolynomial"> The second polynomial for subtraction.</param>
        /// <returns> Result of subtracting two polynomials.</returns>
        public static Polynomial operator -(Polynomial firstPolynomial, Polynomial secondPolynomial)
        {
            int maxDergee = GetMaxDegree(firstPolynomial, secondPolynomial);
            int[] resultСoefficient = new int[maxDergee];
            for (int i = firstPolynomial.Coefficient.Length - 1, j = secondPolynomial.Coefficient.Length - 1, k = maxDergee - 1; i >= 0 || j >= 0; i--, j--, k--)
            {
                if (i >= 0 && j >= 0)
                {
                    resultСoefficient[k] = firstPolynomial.Coefficient[i] - secondPolynomial.Coefficient[j];
                }

                if (i == 0 && j < 0)
                {
                    resultСoefficient[k] = firstPolynomial.Coefficient[i];
                }

                if (i < 0 && j == 0)
                {
                    resultСoefficient[k] = secondPolynomial.Coefficient[j];
                }
            }

            return new Polynomial(resultСoefficient);
        }

        /// <summary>
        /// Finds the difference between polynomial and a number.
        /// </summary>
        /// <param name="polynomial"> A polynomial for subtraction.</param>
        /// <param name="number"> A number for subtraction.</param>
        /// <returns> Result of subtracting a polynomial and a number.</returns>
        public static Polynomial operator -(Polynomial polynomial, int number)
        {
            var resultСoefficient = (int[])polynomial.Coefficient.Clone();
            resultСoefficient[^1] -= number;
            return new Polynomial(resultСoefficient);
        }

        /// <summary>
        /// Multiplies two polynomials.
        /// </summary>
        /// <param name="firstPolynomial"> The first polynomial for multiplication.</param>
        /// <param name="secondPolynomial"> The second polynomial for multiplication.</param>
        /// <returns> The result of multiplying two polynomials.</returns>
        public static Polynomial operator *(Polynomial firstPolynomial, Polynomial secondPolynomial)
        {
            var degreeOfResultPolynomial = firstPolynomial.Coefficient.Length + secondPolynomial.Coefficient.Length - 1;
            var resultСoefficient = CreateZeroArray(degreeOfResultPolynomial);

            for (int j = 0; j < secondPolynomial.Coefficient.Length; j++)
            {
                if (secondPolynomial.Coefficient[j] != 0)
                {
                    for (int i = 0; i < firstPolynomial.Coefficient.Length; i++)
                    {
                        if (firstPolynomial.Coefficient[i] != 0)
                        {
                            resultСoefficient[i + j] += firstPolynomial.Coefficient[i] * secondPolynomial.Coefficient[j];
                        }
                    }
                }
            }

            return new Polynomial(resultСoefficient);
        }

        /// <summary>
        /// Multiplies a polynomial and a number.
        /// </summary>
        /// <param name="polynomial"> A polynomial for multiplication.</param>
        /// <param name="number"> A number for multiplication.</param>
        /// <returns> The result of multiplying a polynomial and a number.</returns>
        public static Polynomial operator *(Polynomial polynomial, int number)
        {
            var resultСoefficient = new int[polynomial.Coefficient.Length];
            for (int i = 0; i < polynomial.Coefficient.Length; i++)
            {
                resultСoefficient[i] = polynomial.Coefficient[i] * number;
            }

            return new Polynomial(resultСoefficient);
        }

        /// <summary>
        /// Divides the polynomial by the single term.
        /// </summary>
        /// <param name="firstPolynomial"> The first polynomial to divide.</param>
        /// <param name="secondPolynomial"> The second polynomial to divide.</param>
        /// <returns> Result of dividing two polynomials.</returns>
        public static Polynomial operator /(Polynomial firstPolynomial, Polynomial secondPolynomial)
        {
            if (secondPolynomial.Coefficient.Where(e => e > 0).Count() > 1)
                throw new ArgumentException("You can only divide it into monomial.");

            if (firstPolynomial.Coefficient.Length < secondPolynomial.Coefficient.Length)
                throw new ArgumentException("The first polynomial must be larger than the second.");

            var degreeOfResultPolynomial = firstPolynomial.Coefficient.Length - secondPolynomial.Coefficient.Length + 1;
            var resultСoefficient = CreateZeroArray(degreeOfResultPolynomial);

            for (int j = secondPolynomial.Coefficient.Length - 1; j >= 0; j--)
            {
                if (secondPolynomial.Coefficient[j] != 0)
                {
                    for (int i = firstPolynomial.Coefficient.Length - 1; i >= 0; i--)
                    {
                        if (firstPolynomial.Coefficient[i] != 0 && i >= j)
                        {
                            resultСoefficient[i - j] += firstPolynomial.Coefficient[i] / secondPolynomial.Coefficient[j];
                        }
                    }
                }
            }

            return new Polynomial(resultСoefficient);
        }

        /// <summary>
        /// Divides the polynomial by the number.
        /// </summary>
        /// <param name="polynomial"> A polynomial to divide.</param>
        /// <param name="number"> A number to divide.</param>
        /// <returns> Result of dividing a polynomial and a number.</returns>
        public static Polynomial operator /(Polynomial polynomial, int number)
        {
            var resultСoefficient = new int[polynomial.Coefficient.Length];
            for (int i = 0; i < polynomial.Coefficient.Length; i++)
            {
                resultСoefficient[i] = polynomial.Coefficient[i] / number;
            }

            return new Polynomial(resultСoefficient);
        }

        /// <summary>
        /// Create zero array for new coefficients.
        /// </summary>
        /// <param name="sizeOfArray"> Size of zero array.</param>
        /// <returns> An array containing the zeros.</returns>
        private static int[] CreateZeroArray(int sizeOfArray)
        {
            var zeroArray = new int[sizeOfArray];

            for (int i = 0; i < zeroArray.Length; i++)
            {
                zeroArray[i] = 0;
            }

            return zeroArray;
        }

        /// <summary>
        /// Gets max degree of two polynomials.
        /// </summary>
        /// <param name="firstPolynomial"> The first polynomial to calculat.</param>
        /// <param name="secondPolynomial"> The second polynomial to calculat.</param>
        /// <returns> Max degree of two polynomials.</returns>
        private static int GetMaxDegree(Polynomial firstPolynomial, Polynomial secondPolynomial)
        {
            if (firstPolynomial.Coefficient.Length >= secondPolynomial.Coefficient.Length)
            {
                return firstPolynomial.Coefficient.Length;
            }
            else
            {
                return secondPolynomial.Coefficient.Length;
            }
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is Polynomial polynomial && Equals(this.ToString(), polynomial.ToString());
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Coefficient);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            var outputString = new StringBuilder();
            for (int i = 0; i < Coefficient.Length; i++)
            {
                if (Coefficient[i] != 0)
                {
                    if (i > 0)
                    {
                        if (Coefficient[i] > 0)
                        {
                            outputString.Append(" + ");
                        }
                        else
                        {
                            outputString.Append(" - ");
                        }
                    }

                    outputString.Append(Math.Abs(Coefficient[i]).ToString() + "x^" + (Coefficient.Length - 1 - i));
                }
            }

            return outputString.ToString();
        }
    }
}
