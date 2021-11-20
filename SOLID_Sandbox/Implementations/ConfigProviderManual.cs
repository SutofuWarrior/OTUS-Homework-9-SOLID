using SOLID_Sandbox.Abstractions;

namespace SOLID_Sandbox.Implementations
{
    public class ConfigProviderManual : IConfigProvider
    {
        private readonly IMessageWriter mWriter;
        private readonly IMessageReader mReader;

        public ConfigProviderManual(IMessageWriter writer, IMessageReader reader)
        {
            mWriter = writer;
            mReader = reader;
        }

        public int GetGuessCount()
        {
            mWriter.Write(string.Empty);
            mWriter.Write("Введите максимальное число попыток: ", false);

            if (int.TryParse(mReader.Read(), out int input))
                return input;
            else
                return 5;
        }

        public int GetMax()
        {
            mWriter.Write(string.Empty);
            mWriter.Write("Введите максимальное загадываемое число: ", false);

            if (int.TryParse(mReader.Read(), out int input))
                return input;
            else
                return int.MaxValue;
        }

        public int GetMin()
        {
            mWriter.Write(string.Empty);
            mWriter.Write("Введите минимальное загадываемое число: ", false);

            if (int.TryParse(mReader.Read(), out int input))
                return input;
            else
                return 0;
        }
    }
}
