using System;
using SOLID_Sandbox.Abstractions;

namespace SOLID_Sandbox.Implementations
{
    public class SimpleRandomGenerator : IRandomGenerator
    {
        public int GetNext(int min, int max)
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            return rnd.Next(min, max);
        }
    }
}
