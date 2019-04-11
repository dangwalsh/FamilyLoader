namespace Gensler.Revit.FamilyLoader.Views
{
    using System.Threading;

    public enum RequestId : int
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// "GetPoint" request
        /// </summary>
        GetPoint = 1,
        /// <summary>
        /// "Load" request
        /// </summary>
        Load = 2
    }

    public class Request
    {
        private RequestData _request = null;

        public RequestData Take()
        {
            return Interlocked.Exchange<RequestData>(ref _request, null);
        }

        public void Make(RequestData request)
        {
            Interlocked.Exchange<RequestData>(ref _request, request);
        }
    }

    public class RequestData
    {
        public string Text { get; set; }

        public RequestId Id { get; set; }

    }
}
