using System;
using System.Linq;
using System.Text;

namespace Polynomial
{
    public class Polynomial
    {
        public int[] Coefficient { get; private set; }

        public Polynomial(params int[] correspondingСoefficient)
        {
            Coefficient = correspondingСoefficient;
        }

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

        public static Polynomial operator +(Polynomial polynomial, int number)
        {
            var resultСoefficient = (int[])polynomial.Coefficient.Clone();
            resultСoefficient[^1] += number;
            return new Polynomial(resultСoefficient);
        }

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

        public static Polynomial operator -(Polynomial polynomial, int number)
        {
            var resultСoefficient = (int[])polynomial.Coefficient.Clone();
            resultСoefficient[^1] -= number;
            return new Polynomial(resultСoefficient);
        }

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

        public static Polynomial operator *(Polynomial polynomial, int number)
        {
            var resultСoefficient = new int[polynomial.Coefficient.Length];
            for (int i = 0; i < polynomial.Coefficient.Length; i++)
            {
                resultСoefficient[i] = polynomial.Coefficient[i] * number;
            }

            return new Polynomial(resultСoefficient);
        }

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
        
        public static Polynomial operator /(Polynomial polynomial, int number)
        {
            var resultСoefficient = new int[polynomial.Coefficient.Length];
            for (int i = 0; i < polynomial.Coefficient.Length; i++)
            {
                resultСoefficient[i] = polynomial.Coefficient[i] / number;
            }

            return new Polynomial(resultСoefficient);
        }

        private static int[] CreateZeroArray(int sizeOfArray)
        {
            var zeroArray = new int[sizeOfArray];

            for (int i = 0; i < zeroArray.Length; i++)
            {
                zeroArray[i] = 0;
            }

            return zeroArray;
        }

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

        public override bool Equals(object obj)
        {
            return obj is Polynomial polynomial && Equals(this.ToString(), polynomial.ToString());
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Coefficient);
        }

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
