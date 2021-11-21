using SOLID_Sandbox.Abstractions;

namespace SOLID_Sandbox.Implementations
{
    public class ConfigProviderManual : IConfigProvider
    {
        private readonly IMessageWriter _writer;
        private readonly IMessageReader _reader;

        public ConfigProviderManual(IMessageWriter writer, IMessageReader reader)
        {
            _writer = writer;
            _reader = reader;
        }

        public int GetGuessCount()
        {
            _writer.WriteEmptyLine();
            _writer.Write("Введите максимальное число попыток: ", false);

            if (int.TryParse(_reader.Read(), out int input))
                return input;
            else
                return 5;
        }

        public int GetMax()
        {
            _writer.WriteEmptyLine();
            _writer.Write("Введите максимальное загадываемое число: ", false);

            if (int.TryParse(_reader.Read(), out int input))
                return input;
            else
                return int.MaxValue;
        }

        public int GetMin()
        {
            _writer.WriteEmptyLine();
            _writer.Write("Введите минимальное загадываемое число: ", false);

            if (int.TryParse(_reader.Read(), out int input))
                return input;
            else
                return 0;
        }
    }
}
