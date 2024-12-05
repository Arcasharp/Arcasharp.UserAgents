using System;

namespace Arcasharp.UserAgents;

internal static class ThreadSafeRandom
{
    [ThreadStatic] private static Random? _local;
    private static readonly Random Global = new Random();

    private static Random Instance
    {
        get
        {
            if (_local is null)
            {
                int seed;
                lock (Global)
                {
                    seed = Global.Next();
                }

                _local = new Random(seed);
            }

            return _local;
        }
    }

    public static int Next(int minValue, int maxValue) => Instance.Next(minValue, maxValue);
    public static int Next() => Instance.Next();
}