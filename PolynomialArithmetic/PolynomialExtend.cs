using System;

namespace PolynomialArithmetic
{
    /// <summary>
    /// Extensions for Polynomial 
    /// </summary>
    public static class PolynomialExtend
    {
        /// <summary>
        /// Gets the coefficients of the Polynomial.
        /// </summary>
        /// <param name="polynomial">The polynomial.</param>
        /// <returns></returns>
        public static double[] GetCoefficients(this Polynomial polynomial)
        {
            double[] arrayOfCoefficients = new double[polynomial.Degree + 1];
            for (int i = 0; i <= polynomial.Degree; i++)
            {
                arrayOfCoefficients[i] = polynomial[i];
            }

            return arrayOfCoefficients;
        }

        /// <summary>
        /// Counts Polynomial in point x.
        /// </summary>
        /// <param name="polynomial">The polynomial.</param>
        /// <param name="x">The point x.</param>
        /// <returns></returns>
        public static double Count(this Polynomial polynomial, double x)
        {
            double result = 0;
            for (int i = 0; i <= polynomial.Degree; i++)
            {
                result += polynomial[i] * Math.Pow(x, i);
            }

            return result;
        }

        /// <summary>
        /// Counts Polynomial in point x by Gorner.
        /// </summary>
        /// <param name="polynomial">The polynomial.</param>
        /// <param name="x">The point x.</param>
        /// <returns></returns>
        public static double CountGorner(this Polynomial polynomial, double x)
        {
            return Gorner(x, polynomial.GetCoefficients());
        }

        private static double Gorner(double x, double[] a, int i = 0)
        {
            if (i >= a.Length)
            {
                return 0;
            }

            return a[i] + (x * Gorner(x, a, i + 1));
        }
    }
}
