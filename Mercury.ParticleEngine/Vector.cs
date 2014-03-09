﻿namespace Mercury.ParticleEngine
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Defines a data structure representing a Euclidean vector facing a particular direction,
    /// including a magnitude value.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector
    {
        public Vector(Axis axis, float magnitude)
        {
            _x = axis._x * magnitude;
            _y = axis._y * magnitude;
        }

        public Vector(float x, float y)
        {
            _x = x;
            _y = y;
        }

        internal readonly float _x;
        internal readonly float _y;

        /// <summary>
        /// Gets the length or magnitude of the Euclidean vector.
        /// </summary>
        public float Magnitude
        {
            get { return (float)Math.Sqrt((_x * _x) + (_y * _y)); }
        }

        /// <summary>
        /// Gets the axis in which the vector is facing.
        /// </summary>
        /// <returns>A <see cref="Axis"/> value representing the direction the vector is facing.</returns>
        public Axis Axis
        {
            get { return new Axis(_x, _y); }
        }

        static public Vector operator*(Vector value, float multiplier)
        {
            return new Vector(value._x * multiplier, value._y * multiplier);
        }
    }
}