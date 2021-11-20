namespace SOLID_Sandbox.Abstractions
{
    public interface IMessageWriter
    {
        void Write(string message, bool newLine = true);
    }
}
