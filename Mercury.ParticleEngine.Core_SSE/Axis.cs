﻿namespace Mercury.ParticleEngine
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    /// <summary>
    /// An immutable data structure representing a directed fixed axis.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Axis : IEquatable<Axis>, IFormattable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Axis"/> struct.
        /// </summary>
        /// <param name="x">The X component of the unit vector representing the axis.</param>
        /// <param name="y">The Y component of the unit vector representing the axis.</param>
        public Axis(float x, float y)
        {
            var length = (float)Math.Sqrt((x * x) + (y * y));

            _x = x / length;
            _y = y / length;
        }

        internal readonly float _x;
        internal readonly float _y;

        /// <summary>
        /// Gets a directed axis which points to the left.
        /// </summary>
        static public Axis Left
        {
            get { return new Axis(-1f, 0f); }
        }

        /// <summary>
        /// Gets a directed axis which points up.
        /// </summary>
        static public Axis Up
        {
            get { return new Axis(0f, 1f); }
        }

        /// <summary>
        /// Gets a directed axis which points to the right.
        /// </summary>
        static public Axis Right
        {
            get { return new Axis(1f, 0f); }
        }

        /// <summary>
        /// Gets a directed axis which points down.
        /// </summary>
        static public Axis Down
        {
            get { return new Axis(0f, -1f); }
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(Axis other)
        {
            return _x.Equals(other._x) &&
                   _y.Equals(other._y);
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        /// <param name="obj">Another object to compare to.</param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            
            return obj is Axis && Equals((Axis)obj);
        }

        /// <summary>
        /// Multiplies the fixed axis by a magnitude value resulting in a directed vector.
        /// </summary>
        /// <param name="magnitude">The magnitude of the vector.</param>
        /// <returns>A directed vector.</returns>
        public Vector Multiply(float magnitude)
        {
            return new Vector(this, magnitude);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _x.GetHashCode();

                hashCode = (hashCode * 397) ^ _y.GetHashCode();
                
                return hashCode;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return ToString("g", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Formats the value of the current instance using the specified format.
        /// </summary>
        /// <param name="format">The format to use.
        /// -or- A null reference (Nothing in Visual Basic) to use the default format defined for the type of the System.IFormattable implementation.</param>
        /// <param name="formatProvider">The provider to use to format the value.
        /// -or- A null reference (Nothing in Visual Basic) to obtain the numeric format information from the current locale setting of the operating system.</param>
        /// <returns>The value of the current instance in the specified format.</returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (formatProvider != null)
            {
                var formatter = formatProvider.GetFormat(GetType()) as ICustomFormatter;

                if (formatter != null)
                    return formatter.Format(format, this, formatProvider);
            }

            switch (format.ToLowerInvariant())
            {
                case "x": return _x.ToString("F4");
                case "y": return _y.ToString("F4");
                default:  return String.Format("({0:F4}, {1:F4})", _x, _y);
            }
        }

        public static bool operator ==(Axis x, Axis y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(Axis x, Axis y)
        {
            return !x.Equals(y);
        }

        public static Vector operator *(Axis axis, float magnitude)
        {
            return new Vector(axis, magnitude);
        }
    }
}