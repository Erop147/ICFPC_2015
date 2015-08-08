using System.Collections.Generic;

namespace ICFPC2015.Game.Logic
{
    public class RandomGenerator
    {
        private long seed;

        private const long Modulus = 4294967296L;
        private const long Multiplier = 1103515245L;
        private const long Increment = 12345L;

        public RandomGenerator(long seed)
        {
            this.seed = seed;
        }

        public IEnumerable<long> Generate()
        {
            while (true)
            {
                yield return GetSpecialBits(seed);
                seed = (seed * Multiplier + Increment) % Modulus;
            }
        }

        private static long GetSpecialBits(long x)
        {
            return (x >> 16) & ((1 << 15) - 1);
        }
    }
}