using System.Net;

namespace Networking.Entities
{
    public interface INetworkResponse
    {
        bool IsSuccessful { get; }
        public HttpStatusCode StatusCode { get; }
        public string Message { get; }
    }
}