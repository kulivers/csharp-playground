using System;

namespace TAP.Exercises
{
    public class ThreadSafeRandom
    {
        private static readonly Random _global = new Random();
        [ThreadStatic] private static Random _local;

        public int Next(int minValue, int maxValue)
        {
            if (_local == null)
            {
                lock (_global)
                {
                    if (_local == null)
                    {
                        int seed = _global.Next(minValue, maxValue);
                        _local = new Random(seed);
                    }
                }
            }

            return _local.Next();
        }

    }
}