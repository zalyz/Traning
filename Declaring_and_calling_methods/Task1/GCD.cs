using System;
using System.Diagnostics;

namespace Task1
{
    /// <summary>
    /// Represents a class for calculating greatest common divisor.
    /// </summary>
    public static class GCD
    {
        /// <summary>
        /// Calculates greatest common divisor of two numbers.
        /// </summary>
        /// <param name="firstNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="secondNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="executionTime"> Algorithm execution time.</param>
        /// <returns> Greatest common divisor of two numbers.</returns>
        /// <exception cref="ArgumentException">Thrown if the parameters are less than or equal to zero</exception>
        public static int GreatestCommonDivisorOf(int firstNumber, int secondNumber, out TimeSpan executionTime)
        {
            if (IsPositiveNumbers(firstNumber, secondNumber))
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var gcdOfTwoNumbers = MethodOfEuclid(firstNumber, secondNumber);
                stopWatch.Stop();
                executionTime = stopWatch.Elapsed;
                return gcdOfTwoNumbers;
            }

            throw new ArgumentException("Parameters can't be less then or equal to zero");
        }

        /// <summary>
        /// Calculates greatest common divisor of three numbers.
        /// </summary>
        /// <param name="firstNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="secondNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="thirdNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="executionTime"> Algorithm execution time.</param>
        /// <returns> Greatest common divisor of three numbers.</returns>
        /// <exception cref="ArgumentException">Thrown if the parameters are less than or equal to zero</exception>
        public static int GreatestCommonDivisorOf(int firstNumber, int secondNumber, int thirdNumber, out TimeSpan executionTime)
        {
            if (IsPositiveNumbers(firstNumber, secondNumber, thirdNumber))
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var gcdOfTwoNumbers = MethodOfEuclid(firstNumber, secondNumber);
                var result = MethodOfEuclid(gcdOfTwoNumbers, thirdNumber);
                stopWatch.Stop();
                executionTime = stopWatch.Elapsed;
                return result;
            }

            throw new ArgumentException("Parameters can't be less then or equal to zero");
        }

        /// <summary>
        /// Calculates greatest common divisor of four numbers.
        /// </summary>
        /// <param name="firstNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="secondNumber"> The number to calculategreatest common divisor.</param>
        /// <param name="thirdNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="fourthNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="executionTime"> Algorithm execution time.</param>
        /// <returns> Greatest common divisor of four numbers.</returns>
        /// <exception cref="ArgumentException">Thrown if the parameters are less than or equal to zero</exception>
        public static int GreatestCommonDivisorOf(int firstNumber, int secondNumber, int thirdNumber, int fourthNumber, out TimeSpan executionTime)
        {
            if (IsPositiveNumbers(firstNumber, secondNumber, thirdNumber, fourthNumber))
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var gcdOfTwoNumbers = MethodOfEuclid(firstNumber, secondNumber);
                gcdOfTwoNumbers = MethodOfEuclid(gcdOfTwoNumbers, thirdNumber);
                var result = MethodOfEuclid(gcdOfTwoNumbers, fourthNumber);
                stopWatch.Stop();
                executionTime = stopWatch.Elapsed;
                return result;
            }

            throw new ArgumentException("Parameters can't be less then or equal to zero");
        }

        /// <summary>
        /// Calculates greatest common divisor of five numbers.
        /// </summary>
        /// <param name="firstNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="secondNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="thirdNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="fourthNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="fifthNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="executionTime"> Algorithm execution time.</param>
        /// <returns> Greatest common divisor of five numbers.</returns>
        /// <exception cref="ArgumentException">Thrown if the parameters are less than or equal to zero</exception>
        public static int GreatestCommonDivisorOf(int firstNumber, int secondNumber, int thirdNumber, int fourthNumber, int fifthNumber, out TimeSpan executionTime)
        {
            if (IsPositiveNumbers(firstNumber, secondNumber, thirdNumber, fourthNumber, fifthNumber))
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var gcdOfTwoNumbers = MethodOfEuclid(firstNumber, secondNumber);
                gcdOfTwoNumbers = MethodOfEuclid(gcdOfTwoNumbers, thirdNumber);
                gcdOfTwoNumbers = MethodOfEuclid(gcdOfTwoNumbers, fourthNumber);
                var result = MethodOfEuclid(gcdOfTwoNumbers, fifthNumber);
                stopWatch.Stop();
                executionTime = stopWatch.Elapsed;
                return result;
            }

            throw new ArgumentException("Parameters can't be less then or equal to zero");
        }

        /// <summary>
        /// Calculates the greatest common divisor of two numbers using the Euclidian method
        /// </summary>
        /// <param name="firstNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="secondNumber"> The number to calculate greatest common divisor.</param>
        /// <returns> Greatest common divisor of two numbers.</returns>
        private static int MethodOfEuclid(int firstNumber, int secondNumber)
        {
            while (firstNumber != secondNumber)
            {
                if (firstNumber > secondNumber)
                {
                    firstNumber -= secondNumber;
                }
                else
                {
                    secondNumber -= firstNumber;
                }
            }

            return firstNumber;
        }

        /// <summary>
        /// Calculates the greatest common divisor of two numbers using the Stein method
        /// </summary>
        /// <param name="firstNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="secondNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="executionTime"> Algorithm execution time.</param>
        /// <returns> Greatest common divisor of two numbers.</returns>
        /// <exception cref="ArgumentException">Thrown if the parameters are less than or equal to zero</exception>
        public static int GCDByStein(int firstNumber, int secondNumber, out TimeSpan executionTime)
        {
            if (IsPositiveNumbers(firstNumber, secondNumber))
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var gcdOfTwoNumbers = MethodOfStain(firstNumber, secondNumber);
                stopWatch.Stop();
                executionTime = stopWatch.Elapsed;
                return gcdOfTwoNumbers;
            }

            throw new ArgumentException("Parameters can't be less then or equal to zero");
        }

        /// <summary>
        /// Stein's method for the two numbers to compute the greatest common divisor.
        /// </summary>
        /// <param name="firstNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="secondNumber"> The number to calculate greatest common divisor.</param>
        /// <returns> Greatest common divisor of two numbers.</returns>
        private static int MethodOfStain(int firstNumber, int secondNumber)
        {
            if (firstNumber == secondNumber)
            {
                return firstNumber;
            }

            if (firstNumber == 0)
            {
                return secondNumber;
            }

            if (secondNumber == 0)
            {
                return firstNumber;
            }

            if (IsEven(firstNumber))
            {
                if (!IsEven(secondNumber))
                {
                    return MethodOfStain(firstNumber >> 1, secondNumber);
                }
                else
                {
                    return MethodOfStain(firstNumber >> 1, secondNumber >> 1) << 1;
                }
            }

            if (IsEven(secondNumber))
            {
                return MethodOfStain(firstNumber, secondNumber >> 1);
            }

            if (firstNumber > secondNumber)
            {
                return MethodOfStain((firstNumber - secondNumber) >> 1, secondNumber);
            }

            return MethodOfStain((secondNumber - firstNumber) >> 1, firstNumber);
        }

        /// <summary>
        /// Checks the number for parity
        /// </summary>
        /// <param name="number"> Number for check.</param>
        /// <returns> True if the number is even or False otherwise.</returns>
        private static bool IsEven(int number)
        {
            if (number % 2 == 0)
                return true;

            return false;
        }

        /// <summary>
        /// Checks the number on the positivity.
        /// </summary>
        /// <param name="arrayOfNumbers"> Array of numbers for check.</param>
        /// <returns> True if the number is positive or False otherwise.</returns>
        private static bool IsPositiveNumbers(params int[] arrayOfNumbers)
        {
            foreach (var number in arrayOfNumbers)
            {
                if (number <= 0)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Calculates data for plotting graphs.
        /// </summary>
        /// <param name="firstNumber">The number to calculate greatest common divisor.</param>
        /// <param name="secondNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="thirdNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="fourthNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="fifthNumber"> The number to calculate greatest common divisor.</param>
        /// <returns> A set of results and execution times for each method</returns>
        public static (int[] arrayOfResults, TimeSpan[] arrayOfExecutionTime) GetStatisticalDataForGraph(
            int firstNumber, int secondNumber, int thirdNumber, int fourthNumber, int fifthNumber)
        {
            (int[] numberOfParameters, TimeSpan[] arrayOfExecutionTime) statisticalData;
            var numberOfMethodsForCalculatingGCD = 5;
            statisticalData.numberOfParameters = new int[numberOfMethodsForCalculatingGCD];
            statisticalData.arrayOfExecutionTime = new TimeSpan[numberOfMethodsForCalculatingGCD];
            FeedStatisticalData(firstNumber, secondNumber, thirdNumber, fourthNumber, fifthNumber, statisticalData);
            return statisticalData;
        }

        /// <summary>
        /// Fills in the set of results and execution times for each method.
        /// </summary>
        /// <param name="firstNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="secondNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="thirdNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="fourthNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="fifthNumber"> The number to calculate greatest common divisor.</param>
        /// <param name="statisticalData"> Set of results and execution times.</param>
        private static void FeedStatisticalData(int firstNumber, int secondNumber, int thirdNumber, int fourthNumber, int fifthNumber,
            (int[] numberOfParameters, TimeSpan[] arrayOfExecutionTime) statisticalData)
        {
            TimeSpan executionTime;
            GreatestCommonDivisorOf(firstNumber, secondNumber, out executionTime);
            statisticalData.numberOfParameters[0] = 2;
            statisticalData.arrayOfExecutionTime[0] = executionTime;

            GCDByStein(firstNumber, secondNumber, out executionTime);
            statisticalData.numberOfParameters[1] = 2;
            statisticalData.arrayOfExecutionTime[1] = executionTime;

            GreatestCommonDivisorOf(firstNumber, secondNumber, thirdNumber, out executionTime);
            statisticalData.numberOfParameters[2] = 3;
            statisticalData.arrayOfExecutionTime[2] = executionTime;

            GreatestCommonDivisorOf(firstNumber, secondNumber, thirdNumber, fourthNumber, out executionTime);
            statisticalData.numberOfParameters[3] = 4;
            statisticalData.arrayOfExecutionTime[3] = executionTime;

            GreatestCommonDivisorOf(firstNumber, secondNumber, thirdNumber, fourthNumber, fifthNumber, out executionTime);
            statisticalData.numberOfParameters[4] = 5;
            statisticalData.arrayOfExecutionTime[4] = executionTime;
        }
    }
}
