﻿namespace Mercury.ParticleEngine
{
    using System;

    /// <summary>
    /// Defines a random number generator which uses the FastRand algorithm to generate random values.
    /// </summary>
    internal static class FastRand
    {
        static int _state = 1;

        static public void Seed(int seed)
        {
            if (seed < 1)
                throw new ArgumentOutOfRangeException("seed", "seed must be greater than zero");
            
            _state = seed;
        }
        
        /// <summary>
        /// Gets the next random integer value.
        /// </summary>
        /// <returns>A random positive integer.</returns>
        static public int NextInteger()
        {
            _state = 214013 * _state + 2531011;
            return (_state >> 16) & 0x7FFF;
        }

        /// <summary>
        /// Gets the next random integer value which is greater than zero and less than or equal to
        /// the specified maxmimum value.
        /// </summary>
        /// <param name="max">The maximum random integer value to return.</param>
        /// <returns>A random integer value between zero and the specified maximum value.</returns>
        static public int NextInteger(int max)
        {
            return (int)(max * NextSingle());
        }

        /// <summary>
        /// Gets the next random integer between the specified minimum and maximum values.
        /// </summary>
        /// <param name="min">The inclusive minimum value.</param>
        /// <param name="max">The inclusive maximum value.</param>
        static public int NextInteger(int min, int max)
        {
            return (int)((max - min) * NextSingle()) + min;
        }

        /// <summary>
        /// Gets the next random integer between the specified range of values.
        /// </summary>
        /// <param name="range">A range representing the inclusive minimum and maximum values.</param>
        /// <returns>A random integer between the specified minumum and maximum values.</returns>
        static public int NextInteger(Range range)
        {
            return NextInteger(range.X, range.Y);
        }

        /// <summary>
        /// Gets the next random single value.
        /// </summary>
        /// <returns>A random single value between 0 and 1.</returns>
        static public float NextSingle()
        {
            return NextInteger() / (float)Int16.MaxValue;
        }

        /// <summary>
        /// Gets the next random single value which is greater than zero and less than or equal to
        /// the specified maxmimum value.
        /// </summary>
        /// <param name="max">The maximum random single value to return.</param>
        /// <returns>A random single value between zero and the specified maximum value.</returns>
        static public float NextSingle(float max)
        {
            return max * NextSingle();
        }

        /// <summary>
        /// Gets the next random single value between the specified minimum and maximum values.
        /// </summary>
        /// <param name="min">The inclusive minimum value.</param>
        /// <param name="max">The inclusive maximum value.</param>
        /// <returns>A random single value between the specified minimum and maximum values.</returns>
        static public float NextSingle(float min, float max)
        {
            return ((max - min) * NextSingle()) + min;
        }

        /// <summary>
        /// Gets the next random single value between the specified range of values.
        /// </summary>
        /// <param name="range">A range representing the inclusive minimum and maximum values.</param>
        /// <returns>A random single value between the specified minimum and maximum values.</returns>
        static public float NextSingle(RangeF range)
        {
            return NextSingle(range.X, range.Y);
        }

        /// <summary>
        /// Gets the next random angle value.
        /// </summary>
        /// <returns>A random angle value.</returns>
        static public float NextAngle()
        {
            return NextSingle((float)Math.PI * -1f, (float)Math.PI);
        }

        static public unsafe void NextUnitVector(Vector* vector)
        {
            var angle = NextAngle();

            *vector = new Vector((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

		static public void NextUnitVector(ref float x, ref float y)
		{
			var angle = NextAngle();

			x = (float) Math.Cos(angle);
			y = (float) Math.Sin(angle);
		}

        static public unsafe void NextColour(Colour* colour, ColourRange range)
        {
            *colour = new Colour(NextSingle(range.Min.H, range.Max.H),
                                 NextSingle(range.Min.S, range.Max.S),
                                 NextSingle(range.Min.L, range.Max.L));
        }

		static public void NextColour(ref float R, ref float G, ref float B, ColourRange range)
		{
			R = NextSingle(range.Min.H, range.Max.H);
			G = NextSingle(range.Min.S, range.Max.S);
			B = NextSingle(range.Min.L, range.Max.L);
		}
    }
}