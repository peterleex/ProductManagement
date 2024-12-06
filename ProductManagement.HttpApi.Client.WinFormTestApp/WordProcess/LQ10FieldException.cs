
namespace ProductManagement.HttpApi.Client.WinFormTestApp.WordProcess
{
    [Serializable]
    internal class LQ10FieldException : Exception
    {
        public LQ10FieldException()
        {
        }

        public LQ10FieldException(string? message) : base(message)
        {
        }

        public LQ10FieldException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}