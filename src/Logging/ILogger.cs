namespace gsharp.Logging
{
    public interface ILogger
    {
        void Log(string message, string? className = null);
    }
}
