﻿namespace Mercury.ParticleEngine.Modifiers
{
    public class HueInterpolator2 : Modifier
    {
        public float InitialHue;
        public float FinalHue;

        protected internal override unsafe void Update(float elapsedSeconds, ref Particle particle, int index, int count)
        {
            var delta = FinalHue - InitialHue;

	        var i = index;
	        unchecked
	        {
		        while (count-- > 0)
		        {
			        particle.R[i] = (delta * particle.Age[i]) + InitialHue;

			        i++;
		        }
	        }
        }
    }
}