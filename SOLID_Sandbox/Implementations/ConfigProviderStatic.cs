using SOLID_Sandbox.Abstractions;

namespace SOLID_Sandbox.Implementations
{
    public class ConfigProviderStatic : IConfigProvider
    {
        public int GetGuessCount() => 5;

        public int GetMax() => 0;

        public int GetMin() => 10;
    }
}
