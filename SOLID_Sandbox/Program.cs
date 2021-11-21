using System;
using SOLID_Sandbox.Abstractions;
using SOLID_Sandbox.Implementations;

namespace SOLID_Sandbox
{
    class Program
    {
        static void Main()
        {
            // Принцип единственной ответственности;
            //      Классы SimpleRandomGenerator, ConfigProviderStatic, ConfigProviderManual
            //      выполняют единственную задачу каждый
            //      Методы GameDigitGuesser также выполняют каждый единственную задачу
            // Принцип инверсии зависимостей;
            //      Класс GameDigitGuesser получает для использования абстракции, а не создаёт конкретные
            //      реализации внутри себя
            // Принцип разделения интерфейса;
            //      Разделены на разные интерфейсы IMessageReader и IMessageWriter
            // Принцип открытости/закрытости;
            //      Базовый класс игры GameDigitGuesser в GameDigitGuesserConfigurable дополнен
            //      возможностью конфигурирования без внесения изменений в исходный класс
            // Принцип подстановки Барбары Лисков;
            //      Инициализировать переменную game ниже можно экземпляром базового класса
            //      GameDigitGuesser, после чего базовое поведение класса не поменяется.
            //      И наоборот, можно использовать GameDigitGuesser, затем заменить на GameDigitGuesserConfigurable,
            //      и это только добавит возможность настраивать параметры игры, но в целом не изменит её
            //      изначального поведения

            var consoleProvider = new ConsoleProvider();
            var random = new SimpleRandomGenerator();
            var config = new ConfigProviderManual(writer: consoleProvider, reader: consoleProvider);

            //IGameEngineBase game = new GameDigitGuesser(consoleProvider, consoleProvider, random);
            IGameEngine game = new GameDigitGuesserConfigurable(consoleProvider, consoleProvider, random, config);

            game.StartGame();

            Console.ReadLine();
        }
    }

    
}
