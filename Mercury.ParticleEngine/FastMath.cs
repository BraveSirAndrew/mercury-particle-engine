using System;
using System.Runtime.CompilerServices;

namespace Mercury.ParticleEngine
{
	public static class FastMath
	{
		const float Coeff1 = (float)(Math.PI / 4f);
		const float Coeff2 = 3f * Coeff1;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Atan2(float y, float x)
		{
			var absY = Math.Abs(y);
			float angle;
			if (x >= 0d)
			{
				var r = (x - absY) / (x + absY);
				angle = Coeff1 - Coeff1 * r;
			}
			else
			{
				var r = (x + absY) / (absY - x);
				angle = Coeff2 - Coeff1 * r;
			}
			return y < 0f ? -angle : angle;
		}
	}
}