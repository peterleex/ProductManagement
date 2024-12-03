
namespace ProductManagement.HttpApi.Client.WinFormTestApp.WordProcess
{
    [Serializable]
    internal class FieldException : Exception
    {
        public FieldException()
        {
        }

        public FieldException(string? message) : base(message)
        {
        }

        public FieldException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}