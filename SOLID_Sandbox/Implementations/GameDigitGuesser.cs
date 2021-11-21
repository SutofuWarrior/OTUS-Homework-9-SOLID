using SOLID_Sandbox.Abstractions;

namespace SOLID_Sandbox
{
    public class GameDigitGuesser : IGameEngine
    {
        private readonly IMessageWriter _writer;
        private readonly IMessageReader _reader;
        private readonly IRandomGenerator _random;

        protected int _min = 0, _max = 10, _maxGuessCount = 5;

        public GameDigitGuesser(IMessageWriter writer, IMessageReader reader, IRandomGenerator random)
        {
            _writer = writer;
            _random = random;
            _reader = reader;
        }

        /// <summary>
        /// Показать приветствие
        /// Показать меню
        /// Начать взаимодействие в меню
        /// </summary>
        public void StartGame()
        {
            WriteStartMessage();
            ShowMainMenu();
            MainMenuInteraction();
        }

        /// <summary>
        /// Взаимодействие в меню
        /// </summary>
        private void MainMenuInteraction()
        {
            const string StartKey = "1";
            const string AboutKey = "2";
            const string QuitKey = "0";

            string key;

            do
            {
                _writer.Write(">>> ", false);
                key = _reader.Read();

                switch (key)
                {
                    case StartKey:
                        Play();
                        ShowMainMenu();
                        break;

                    case AboutKey:
                        WriteIntro();
                        break;

                    case QuitKey:
                        break;
                }

            } while (key != QuitKey);
        }

        private void Play()
        {
            const string QuitKey = "Q";

            int digit = GetRandomDigit();

            int guess;
            int guessCount = 0;
            string input;

            bool quit, valid;

            _writer.Write("Число загадано. Попробуйте угадать!");
            _writer.Write($"Или введите {QuitKey}, чтобы выйти.");

            do
            {
                if (guessCount >= _maxGuessCount)
                {
                    _writer.Write($"Вы исчерпали лимит попыток, увы. Правильный ответ: {digit}");
                    return;
                }

                _writer.Write("Ваше число: ", false);
                input = _reader.Read();

                (quit, valid, guess) = Validate(input, QuitKey);

                if (quit)
                    return;

                if (!valid)
                {
                    _writer.Write("Это же вообще не число!");
                    continue;
                }

                guessCount++;

                if (guess == digit)
                {
                    ShowWinnerMessage(guessCount);
                    return;
                }
                else if (guess < digit)
                    _writer.Write("Не угадали. Ваше число меньше, чем загаданное");
                else
                    _writer.Write("Не угадали. Ваше число больше, чем загаданное");

            } while (true);
        }

        /// <summary>
        /// Обработка ввода
        /// </summary>
        /// <param name="input">Ввод</param>
        /// <param name="quitSign">Команда на выход</param>
        /// <returns>Выйти из игры (да/нет), корректный ввод (да/нет), число</returns>
        private (bool quit, bool valid, int digit) Validate(string input, string quitSign)
        {
            if (input == quitSign)
                return (true, true, 0);

            if (!int.TryParse(input, out int guess))
                return (false, false, 0);
            else
                return (false, true, guess);
        }

        protected virtual void WriteStartMessage()
        {
            _writer.Write("Игра начинается!");
            _writer.WriteEmptyLine();
        }

        protected virtual void WriteIntro()
        {
            _writer.Write("Игра загадывает число, а вы должны это число угадать.");
            _writer.Write("Если попытка не верная, игра скажет, введённое число больше или меньше отгадываемого.");
            _writer.Write("Чем меньше попыток - тем лучше! Вперёд!");
            _writer.WriteEmptyLine();
        }

        protected virtual void ShowMainMenu()
        {
            _writer.Write("1 - Начать игру");
            _writer.Write("2 - Об игре");
            _writer.Write("0 - Выход");
            _writer.WriteEmptyLine();
        }

        protected virtual void ShowWinnerMessage(int count)
        {
            _writer.Write($"ВЕРНО! Вы угадали число за {count} попыток");
            _writer.Write("Сыграем ещё раз? :)");
            _writer.WriteEmptyLine();
        }

        private int GetRandomDigit()
            => _random.GetNext(_min, _max);

    }
}
