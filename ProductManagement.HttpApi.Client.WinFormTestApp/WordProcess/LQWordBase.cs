using DocumentFormat.OpenXml.Packaging;

namespace ProductManagement.HttpApi.Client.WinFormTestApp.WordProcess
{
    public class LQWordBase
    {
        protected WordprocessingDocument wordDocument;

        public void Dispose()
        {
            if (wordDocument != null)
            {
                wordDocument.Dispose();
            }
        }
    }
}