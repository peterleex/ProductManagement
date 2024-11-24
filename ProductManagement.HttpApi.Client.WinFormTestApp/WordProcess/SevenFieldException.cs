
namespace ProductManagement.HttpApi.Client.WinFormTestApp.WordProcess
{
    [Serializable]
    internal class SevenFieldException : Exception
    {
        public SevenFieldException()
        {
        }

        public SevenFieldException(string? message) : base(message)
        {
        }

        public SevenFieldException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}