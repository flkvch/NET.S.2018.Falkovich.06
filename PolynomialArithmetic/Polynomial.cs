using System;

namespace PolynomialArithmetic
{
    /// <summary>
    /// Math Polynomial
    /// </summary>
    public class Polynomial
    {
        /// <summary>
        /// The accuracy of counting
        /// </summary>
        public const double ACCURACY = 0.001;

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class.
        /// </summary>
        /// <param name="coefficients">The coefficients.</param>
        /// <remarks>
        /// While counting polynomial can get 0 value. But it will be represented as array of zeros. 
        /// Nulcoef is added to avoid this problem.
        /// </remarks>
        public Polynomial(params double[] coefficients)
        {
            Degree = coefficients.Length - 1;
            Coefficients = new double[Degree + 1];
            int nullCoef = 0;
            for (int i = 0; i <= Degree; i++)
            {
                if (coefficients[i] == 0)
                {
                    nullCoef++;
                }

                Coefficients[i] = coefficients[i];
            }

            if (nullCoef == Degree + 1) 
            {
                Degree = 0;
                Coefficients = new double[Degree + 1];
                Coefficients[0] = 0;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class in pravate arithmetical operations by its <paramref name="degree"/>.
        /// </summary>
        /// <param name="degree">
        /// The degree.
        /// </param>
        private Polynomial(int degree)
        {
            Degree = degree;
            Coefficients = new double[Degree + 1];
        }

        /// <summary>
        /// Gets the coefficients of the polynomial.
        /// </summary>
        /// <value>
        /// The coefficients.
        /// </value>
        public double[] Coefficients { get; private set; }

        /// <summary>
        /// Gets the degree.
        /// </summary>
        /// <value>
        /// The degree of polynomial.
        /// </value>
        public int Degree { get; private set; }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="leftOperand">The left operand.</param>
        /// <param name="rightOperand">The right operand.</param>
        /// <returns>
        /// The sum of polynomials
        /// </returns>
        public static Polynomial operator +(Polynomial leftOperand, Polynomial rightOperand)
        {
            int numOfOperations = leftOperand.Degree;
            int size = rightOperand.Degree;
            if (leftOperand.Degree > rightOperand.Degree)
            {
                numOfOperations = rightOperand.Degree;
                size = leftOperand.Degree;
            }

            for (int i = 0; i <= numOfOperations; i++)
            {
                leftOperand.Coefficients[i] += rightOperand.Coefficients[i];
            }

            Polynomial resultPolynomial = new Polynomial(leftOperand.Coefficients);
            return resultPolynomial;
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="leftOperand">The left operand.</param>
        /// <param name="rightOperand">The right operand.</param>
        /// <returns>
        /// The substract of polynomials.
        /// </returns>
        public static Polynomial operator -(Polynomial leftOperand, Polynomial rightOperand)
        {
            int numOfOperations = leftOperand.Degree;
            int size = rightOperand.Degree;
            if (leftOperand.Degree > rightOperand.Degree)
            {
                numOfOperations = rightOperand.Degree;
                size = leftOperand.Degree;
            }

            for (int i = 0; i <= numOfOperations; i++)
            {
                leftOperand.Coefficients[i] -= rightOperand.Coefficients[i];
            }

            Polynomial resultPolynomial = new Polynomial(leftOperand.Coefficients);
            return resultPolynomial;            
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="leftOperand">The left operand.</param>
        /// <param name="rightOperand">The right operand.</param>
        /// <returns>
        /// The multiplication of polynomials.
        /// </returns>
        public static Polynomial operator *(Polynomial leftOperand, Polynomial rightOperand)
        {
            Polynomial resultPolynomial = new Polynomial(leftOperand.Degree + rightOperand.Degree);
            for (int i = 0; i <= leftOperand.Degree; i++)
            {
                for (int j = 0; j <= rightOperand.Degree; j++)
                {
                    resultPolynomial.Coefficients[i + j] += leftOperand.Coefficients[i] * rightOperand.Coefficients[j];
                }
            }

            return resultPolynomial;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="leftOperand">The left operand.</param>
        /// <param name="rightOperand">The right operand.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Polynomial leftOperand, Polynomial rightOperand)
        {
            if (leftOperand.Degree != rightOperand.Degree)
            {
                return false;
            }
            else
            {
                for (int i = 0; i <= leftOperand.Degree; i++)
                {
                    if (Math.Abs(leftOperand.Coefficients[i] - rightOperand.Coefficients[i]) >= ACCURACY)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="leftOperand">The left operand.</param>
        /// <param name="rightOperand">The right operand.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Polynomial leftOperand, Polynomial rightOperand)
        {
            return !(leftOperand.Degree == rightOperand.Degree);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string stringRepresentation = null;
            for (int i = 0; i <= Degree; i++) 
            {
                if (Coefficients[i] == 0)
                {
                    continue;
                }

                if (stringRepresentation != null)
                {
                    if (Coefficients[i] > 0) 
                    {
                        stringRepresentation += "+";
                    }
                }

                if (i == 0) 
                {
                    stringRepresentation += Coefficients[i];
                    continue;
                }

                if (i == 1)
                {
                    stringRepresentation += Coefficients[i];
                    stringRepresentation += $"x";
                    continue;
                }

                if (Coefficients[i] != 1)
                {
                    if (Coefficients[i] == -1)
                    {
                        stringRepresentation += "-";
                    }
                    else
                    {
                        stringRepresentation += Coefficients[i];
                    }
                }

                stringRepresentation += $"x^{i}";
            }

            return stringRepresentation;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is Polynomial))
            {
                return false;
            }

            return this == (Polynomial)obj;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            int hashCode = 0;
            for (int i = 0; i <= Degree; i++)
            {
                hashCode += Coefficients[i].GetHashCode() * i.GetHashCode();
            }

            return hashCode;
        }
    }
}
