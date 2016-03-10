using System;
using System.Runtime.CompilerServices;

namespace Mercury.ParticleEngine
{
	public unsafe class ParticleBuffer : IDisposable
	{
		private int _tail;
		private bool _disposed;
		
		public readonly int Size;
		public readonly Particle Particles;
		public int Index;

		public ParticleBuffer(int size)
		{
			Size = size;

			Particles = new Particle
			{
				X = new float[Size],
				Y = new float[Size],
				VX = new float[Size],
				VY = new float[Size],
				Inception = new float[Size],
				Age = new float[Size],
				R = new float[Size],
				G = new float[Size],
				B = new float[Size],
				Opacity = new float[Size],
				Scale = new float[Size],
				Rotation = new float[Size],
				Mass = new float[Size],
			};
		}

		public int Available
		{
			get { return Size - _tail; }
		}

		public int Count
		{
			get { return _tail; }
		}

		public int SizeInBytes
		{
			get { return Particle.SizeInBytes * Size; }
		}

		public int Release(int releaseQuantity)
		{
			var numToRelease = Math.Min(releaseQuantity, Available);

			var oldTail = _tail;

			_tail += numToRelease;

			Index = oldTail;

			return numToRelease;
		}

		public void Reclaim(int number)
		{
			_tail -= number;

			Reclaim(number, Particles.X);
			Reclaim(number, Particles.Y);
			Reclaim(number, Particles.VX);
			Reclaim(number, Particles.VY);
			Reclaim(number, Particles.Inception);
			Reclaim(number, Particles.Age);
			Reclaim(number, Particles.R);
			Reclaim(number, Particles.G);
			Reclaim(number, Particles.B);
			Reclaim(number, Particles.Opacity);
			Reclaim(number, Particles.Scale);
			Reclaim(number, Particles.Rotation);
			Reclaim(number, Particles.Mass);
		}

		public void Reset()
		{
			_tail = 0;
			Index = 0;
		}

		private void Reclaim(int number, float[] array)
		{
			Array.Copy(array, number, array, 0, _tail);
		}

		public void CopyTo(IntPtr buffer)
		{
			var pDst = (float*)buffer.ToPointer();

			Copy(ref pDst, Particles.Age);
			Copy(ref pDst, Particles.X);
			Copy(ref pDst, Particles.Y);
			Copy(ref pDst, Particles.R);
			Copy(ref pDst, Particles.G);
			Copy(ref pDst, Particles.B);
			Copy(ref pDst, Particles.Opacity);
			Copy(ref pDst, Particles.Scale);
			Copy(ref pDst, Particles.Rotation);
			Copy(ref pDst, Particles.VX);
			Copy(ref pDst, Particles.VY);
		}

		public void Dispose()
		{
			if (!_disposed)
			{
				_disposed = true;
			}

			GC.SuppressFinalize(this);
		}

		~ParticleBuffer()
		{
			Dispose();
		}

		/// <summary>
		/// Fastest possible copy to unmanaged memory. JIT compiles to SSE/AVX instructions on .Net > 4.5. Even faster than native memcpy!
		/// </summary>
		/// <param name="pDst"></param>
		/// <param name="particleData"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Copy(ref float* buffer, float[] particleData)
		{
			fixed (float* data = particleData)
			{
				var pSrc = data;
				var pDst = buffer;
				for (var i = 0; i < _tail; i++)
				{
					*pDst++ = *pSrc++;
				}
				buffer = pDst;
			}
		}
	}
}