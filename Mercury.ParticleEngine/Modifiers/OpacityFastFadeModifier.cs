﻿namespace Mercury.ParticleEngine.Modifiers
{
    public unsafe sealed class OpacityFastFadeModifier : Modifier
    {
        protected internal override void Update(float elapsedSeconds, ref Particle particle, int count)
        {
	        var i = 0;
	        unchecked
	        {
		        while (count-- > 0)
		        {
			        particle.Opacity[i] = 1.0f - particle.Age[i];

			        i++;
		        }
	        }
        }
    }
}