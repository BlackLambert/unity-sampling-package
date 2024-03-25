using System;

namespace PCGToolkit.Sampling
{
    public class SimplexNoise1D : Noise1D
    {
        private byte[] _permutations;

        private SimplexNoise1D(byte[] permutations)
        {
            _permutations = permutations;
        }

        public static SimplexNoise1D Create(Random random)
        {
            return new SimplexNoise1D(CreatePermutations(random));
        }

        private static byte[] CreatePermutations(Random random)
        {
            byte[] result = new byte[512];
            random.NextBytes(result);
            return result;
        }

        /// <summary>
        /// Returns a simplex noise 1D value at the position of value.
        /// </summary>
        /// <param name="x"></param>
        /// <returns>A simplex noise 1D value ranging from -1f to 1f</returns>
        public float Evaluate(float x)
        {
            int i0 = FastFloor(x);
            int i1 = i0 + 1;
            float x0 = x - i0;
            float x1 = x0 - 1.0f;
            
            float t0 = 1.0f - x0 * x0;
            t0 *= t0;
            var n0 = t0 * t0 * Grad(_permutations[i0 & 0xff], x0);

            var t1 = 1.0f - x1 * x1;
            t1 *= t1;
            var n1 = t1 * t1 * Grad(_permutations[i1 & 0xff], x1);
            // The maximum value of this noise is 8*(3/4)^4 = 2.53125
            // A factor of 0.395 scales to fit exactly within [-1,1]
            float result = 0.395f * (n0 + n1);
            return result;
        }

        private int FastFloor(float x)
        {
            return x > 0 ? (int)x : (int)x - 1;
        }

        private static float Grad(int hash, float x)
        {
            int h = hash & 15;
            float grad = 1.0f + (h & 7);   // Gradient value 1.0, 2.0, ..., 8.0
            if ((h & 8) != 0) 
            {
                grad = -grad;         // Set a random sign for the gradient
            }
            return grad * x;           // Multiply the gradient with the distance
        }
    }
}