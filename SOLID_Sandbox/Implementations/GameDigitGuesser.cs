using SOLID_Sandbox.Abstractions;

namespace SOLID_Sandbox
{
    public class GameDigitGuesser : IGameEngine
    {
        private readonly IMessageWriter mWriter;
        private readonly IMessageReader mReader;
        private readonly IRandomGenerator mRandom;

        protected int mMin = 0, mMax = 10, mMaxGuessCount = 5;

        public GameDigitGuesser(IMessageWriter writer, IMessageReader reader, IRandomGenerator random)
        {
            mWriter = writer;
            mRandom = random;
            mReader = reader;
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
            const string cStart = "1";
            const string cAbout = "2";
            const string cQuit = "0";

            string key;

            do
            {
                mWriter.Write(">>> ", false);
                key = mReader.Read();

                switch (key)
                {
                    case cStart:
                        Play();
                        ShowMainMenu();
                        break;

                    case cAbout:
                        WriteIntro();
                        break;

                    case cQuit:
                        break;
                }

            } while (key != cQuit);
        }

        private void Play()
        {
            const string cQuit = "Q";

            int digit = GetRandomDigit();

            int guess;
            int guessCount = 0;
            string input;

            bool quit, valid;

            mWriter.Write("Число загадано. Попробуйте угадать!");
            mWriter.Write($"Или введите {cQuit}, чтобы выйти.");

            do
            {
                if (guessCount >= mMaxGuessCount)
                {
                    mWriter.Write($"Вы исчерпали лимит попыток, увы. Правильный ответ: {digit}");
                    return;
                }

                mWriter.Write("Ваше число: ", false);
                input = mReader.Read();

                (quit, valid, guess) = Validate(input, cQuit);

                if (quit)
                    return;

                if (!valid)
                {
                    mWriter.Write("Это же вообще не число!");
                    continue;
                }

                guessCount++;

                if (guess == digit)
                {
                    ShowWinnerMessage(guessCount);
                    return;
                }
                else if (guess < digit)
                    mWriter.Write("Не угадали. Ваше число меньше, чем загаданное");
                else
                    mWriter.Write("Не угадали. Ваше число больше, чем загаданное");

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
            mWriter.Write("Игра начинается!");
            mWriter.Write(string.Empty);
        }

        protected virtual void WriteIntro()
        {
            mWriter.Write("Игра загадывает число, а вы должны это число угадать.");
            mWriter.Write("Если попытка не верная, игра скажет, введённое число больше или меньше отгадываемого.");
            mWriter.Write("Чем меньше попыток - тем лучше! Вперёд!");
            mWriter.Write(string.Empty);
        }

        protected virtual void ShowMainMenu()
        {
            mWriter.Write("1 - Начать игру");
            mWriter.Write("2 - Об игре");
            mWriter.Write("0 - Выход");
            mWriter.Write(string.Empty);
        }

        protected virtual void ShowWinnerMessage(int count)
        {
            mWriter.Write($"ВЕРНО! Вы угадали число за {count} попыток");
            mWriter.Write("Сыграем ещё раз? :)");
            mWriter.Write(string.Empty);
        }

        private int GetRandomDigit()
            => mRandom.GetNext(mMin, mMax);

    }
}
