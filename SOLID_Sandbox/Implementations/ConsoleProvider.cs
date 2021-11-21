using System;
using SOLID_Sandbox.Abstractions;

namespace SOLID_Sandbox.Implementations
{
    public class ConsoleProvider : IMessageWriter, IMessageReader
    {
        public void Write(string message, bool newLine = true)
        {
            if (newLine)
                Console.WriteLine(message);
            else
                Console.Write(message);
        }

        public void WriteEmptyLine()
        {
            Console.WriteLine();
        }

        public string Read()
        {
            return Console.ReadLine();
        }
    }
}
