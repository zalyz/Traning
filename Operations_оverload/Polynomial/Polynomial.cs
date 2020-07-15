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

        //--------------------------------------------------------------------------------------------
        /*
        public static Polynomial operator /(Polynomial firstPolynomial, Polynomial secondPolynomial)
        {
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

        static Polynomial PolyLongDiv(double[] n, double[] d)
        {
            if (n.Length != d.Length)
            {
                throw new ArgumentException("Numerator and denominator vectors must have the same size");
            }
            int nd = PolyDegree(n);
            int dd = PolyDegree(d);
            if (dd < 0)
            {
                throw new ArgumentException("Divisor must have at least one one-zero coefficient");
            }
            if (nd < dd)
            {
                throw new ArgumentException("The degree of the divisor cannot exceed that of the numerator");
            }
            double[] n2 = new double[n.Length];
            n.CopyTo(n2, 0);
            double[] q = new double[n.Length];
            while (nd >= dd)
            {
                double[] d2 = PolyShiftRight(d, nd - dd);
                q[nd - dd] = n2[nd] / d2[nd];
                PolyMultiply(d2, q[nd - dd]);
                PolySubtract(n2, d2);
                nd = PolyDegree(n2);
            }
            return new Polynomial(q);
        }

        static double[] PolyShiftRight(double[] p, int places)
        {
            if (places <= 0) return p;
            int pd = PolyDegree(p);
            if (pd + places >= p.Length)
            {
                throw new ArgumentOutOfRangeException("The number of places to be shifted is too large");
            }

            double[] d = new double[p.Length];
            p.CopyTo(d, 0);
            for (int i = pd; i >= 0; --i)
            {
                d[i + places] = d[i];
                d[i] = 0.0;
            }

            return d;
        }

        static int PolyDegree(double[] p)
        {
            for (int i = p.Length - 1; i >= 0; --i)
            {
                if (p[i] != 0.0) return i;
            }

            return int.MinValue;
        }

        static void PolyMultiply(double[] p, double m)
        {
            for (int i = 0; i < p.Length; ++i)
            {
                p[i] *= m;
            }
        }

        static void PolySubtract(double[] p, double[] s)
        {
            for (int i = 0; i < p.Length; ++i)
            {
                p[i] -= s[i];
            }
        }
        */
        //------------------------------------------------------------------------

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
                        outputString.Append(" + ");
                    }
                    else
                    {
                        outputString.Append(" - ");
                    }

                    outputString.Append(Math.Abs(Coefficient[i]).ToString() + "x^" + (Coefficient.Length - 1 - i));
                }
            }

            return outputString.ToString();
        }
    }
}
