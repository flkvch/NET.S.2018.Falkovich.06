using System;

namespace PolynomialArithmetic
{
    /// <summary>
    /// Math Polynomial
    /// </summary>
    public sealed class Polynomial : ICloneable, IEquatable<Polynomial>
    {
        /// <summary>
        /// The accuracy of counting
        /// </summary>
        public static double epsilon = 0.001;

        private double[] coefficients;

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial" /> class.
        /// </summary>
        /// <param name="coef">The coef.</param>
        /// <remarks>
        /// While counting polynomial can get 0 value. But it will be represented as array of zeros. (Uncorrect: 0*x^3 + 0*x^2+ 0*x + 0)
        /// Nulcoef is added to avoid this problem.
        /// </remarks>
        /// <exception cref="ArgumentNullException">coef</exception>
        public Polynomial(params double[] coef)
        {
            if (coef == null)
            {
                throw new ArgumentNullException(nameof(coef));
            }
            Degree = coef.Length - 1;
            coefficients = new double[Degree + 1];
            int nullCoef = 0;
            for (int i = 0; i <= Degree; i++)
            {
                if (coef[i] == 0)
                {
                    nullCoef++;
                }

                this[i] = coef[i];
            }

            if (nullCoef == Degree + 1) 
            {
                Degree = 0;
                coefficients = new double[Degree + 1];
                coefficients[0] = 0;
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
            coefficients = new double[Degree + 1];
        }

        /// <summary>
        /// Gets the degree.
        /// </summary>
        /// <value>
        /// The degree of polynomial.
        /// </value>
        public int Degree { get; private set; }

        /// <summary>
        /// Gets the <see cref="System.Double"/> with the specified i.
        /// </summary>
        /// <value>
        /// The <see cref="System.Double"/>.
        /// </value>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public double this[int i]
        {
            get
            {
                if (Degree > coefficients.Length)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return coefficients[i];
            }

            private set
            {
                if (i < 0 && i >= coefficients.Length)
                {
                    throw new ArgumentOutOfRangeException();
                }

                coefficients[i] = value;
            }
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="lhs">The left operand.</param>
        /// <param name="rhs">The right operand.</param>
        /// <returns>
        /// The sum of polynomials
        /// </returns>
        public static Polynomial operator +(Polynomial lhs, Polynomial rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                throw new ArgumentNullException("lhs");
            }

            if (ReferenceEquals(rhs, null))
            {
                throw new ArgumentNullException("rhs");
            }

            Polynomial larger, smaller;
            if (lhs.Degree > rhs.Degree)
            {
                larger = lhs.Clone();
                smaller = rhs.Clone();
            }
            else
            {
                larger = rhs.Clone();
                smaller = lhs.Clone();
            }

            for (int i = 0; i <= smaller.Degree; i++)
            {
                larger[i] += smaller[i];
            }

            Polynomial resultPolynomial = new Polynomial(larger.coefficients);
            return resultPolynomial;
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="lhs">The left operand.</param>
        /// <param name="rhs">The right operand.</param>
        /// <returns>
        /// The substract of polynomials.
        /// </returns>
        public static Polynomial operator -(Polynomial lhs, Polynomial rhs)
        {
            if (ReferenceEquals(rhs, null))
            {
                throw new ArgumentNullException("rhs");
            }

            Polynomial invertRhs = -1 * rhs;
            return lhs + invertRhs;
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="lhs">The left operand.</param>
        /// <param name="rhs">The right operand.</param>
        /// <returns>
        /// The multiplication of polynomials.
        /// </returns>
        public static Polynomial operator *(Polynomial lhs, Polynomial rhs)
        {
            Polynomial resultPolynomial = new Polynomial(lhs.Degree + rhs.Degree);
            for (int i = 0; i <= lhs.Degree; i++)
            {
                for (int j = 0; j <= rhs.Degree; j++)
                {
                    resultPolynomial.coefficients[i + j] += lhs[i] * rhs[j];
                }
            }

            return resultPolynomial;
        }

        /// <summary>
        /// Implements the operator * between <paramref name="number"/> and <paramref name="polynomial"/>
        /// </summary>
        /// <param name="lhs">The left operand.</param>
        /// <param name="rhs">The right operand.</param>
        /// <returns>
        /// The multiplication of number and polynomial.
        /// </returns>
        public static Polynomial operator *(double number, Polynomial polynomial)
        {
            Polynomial resultPolynomial = new Polynomial(polynomial.Degree);
            for (int i = 0; i <= polynomial.Degree; i++)
            {
                resultPolynomial[i] = polynomial[i] * number;
            }

            return resultPolynomial;
        }

        /// <summary>
        /// Implements the operator * between <paramref name="polynomial"/> and <paramref name="number"/>
        /// </summary>
        /// <param name="polynomial">The left operand.</param>
        /// <param name="rhs">The right operand.</param>
        /// <returns>
        /// The multiplication of polynomial and number.
        /// </returns>
        public static Polynomial operator *(Polynomial polynomial, double number)
        {
            return number * polynomial;
        }

        public static Polynomial Add(Polynomial p1, Polynomial p2) => p1 + p2;

        public static Polynomial Subs(Polynomial p1, Polynomial p2) => p1 - p2;

        public static Polynomial Mult(Polynomial p1, Polynomial p2) => p1 * p2;

        public static Polynomial MultNumber(Polynomial p1, double d2) => p1 * d2;

        public static Polynomial MultNumber(double d1, Polynomial p2) => d1 * p2;

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="lhs">The left operand.</param>
        /// <param name="rhs">The right operand.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Polynomial lhs, Polynomial rhs)
        {
            if (ReferenceEquals(lhs, rhs))
            {
                return true;
            }

            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
            {
                return false;
            }

            if (lhs.Degree != rhs.Degree)
            {
                return false;
            }
            else
            {
                for (int i = 0; i <= lhs.Degree; i++)
                {
                    if (Math.Abs(lhs[i] - rhs[i]) >= epsilon)
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
        /// <param name="lhs">The left operand.</param>
        /// <param name="rhs">The right operand.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Polynomial lhs, Polynomial rhs)
        {
            return !(lhs.Degree == rhs.Degree);
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
                if (this[i] == 0)
                {
                    continue;
                }

                if (stringRepresentation != null)
                {
                    if (this[i] > 0) 
                    {
                        stringRepresentation += "+";
                    }
                }

                if (i == 0) 
                {
                    stringRepresentation += this[i];
                    continue;
                }

                if (i == 1)
                {
                    stringRepresentation += this[i];
                    stringRepresentation += $"x";
                    continue;
                }

                if (this[i] != 1)
                {
                    if (this[i] == -1)
                    {
                        stringRepresentation += "-";
                    }
                    else
                    {
                        stringRepresentation += this[i];
                    }
                }

                stringRepresentation += $"x^{i}";
            }

            return stringRepresentation;
        }

        /// <summary>
        /// Equalses the specified polynomials.
        /// </summary>
        /// <param name="other">The other polynomial.</param>
        /// <returns></returns>
        public bool Equals(Polynomial other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (coefficients.Length != other.coefficients.Length)
            {
                return false;
            }

            return this == other;
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
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((Polynomial)obj);
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
                hashCode += this[i].GetHashCode() * i.GetHashCode();
            }

            return hashCode;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        private Polynomial Clone()
        {
            return new Polynomial
            {
                coefficients = this.coefficients,
                Degree = this.Degree
            };
        }
    }
}
